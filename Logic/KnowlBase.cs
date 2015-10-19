using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Xml;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

// Lucene
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Directory = Lucene.Net.Store.Directory;
using Version = Lucene.Net.Util.Version;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;

namespace ImpRec
{
    /// <summary>
    /// Represents the knowledge base, the project memory as parsed from the input files.
    /// </summary>
    [Serializable()]
    public class KnowlBase : ISerializable
    {
        private string LUCENEDIR = "LuceneIndex";

        private TextWriter textWriter;
        private XmlWriter xmlWriter;
        private XmlWriter graphMLWriter;

        public Hashtable caseMap;
        public Hashtable artifactMap;

        private int nbrRelatedCaseLinks;
        private int nbrSpecifiedByLinks;
        private int nbrVerifiedByLinks;
        private int nbrNeedsUpdateLinks;
        private int nbrImpactedHWLinks;
        private int nbrUnspecifiedLinks;

        private Directory luceneDirectory;
        private Analyzer luceneAnalyzer;
        private IndexSearcher luceneIndexSearcher;

        public KnowlBase()
        {
            caseMap = new Hashtable();
            artifactMap = new Hashtable();

            // Make sure there is a directory for the Lucene index
            if (!System.IO.Directory.Exists(LUCENEDIR))
            {
                System.IO.Directory.CreateDirectory(LUCENEDIR);
            }

            luceneDirectory = FSDirectory.Open(new DirectoryInfo(LUCENEDIR));
            luceneAnalyzer = new StandardAnalyzer(Version.LUCENE_30);
            luceneIndexSearcher = null;
        }

        #region Serialization
        public KnowlBase(SerializationInfo info, StreamingContext ctxt)
        {
            this.textWriter = (TextWriter)info.GetValue("TextWriter", typeof(TextWriter));
            this.xmlWriter = (XmlWriter)info.GetValue("XmlWriter", typeof(XmlWriter));
            this.graphMLWriter = (XmlWriter)info.GetValue("GraphMLWriter", typeof(XmlWriter));
            this.caseMap = (Hashtable)info.GetValue("CaseMap", typeof(Hashtable));
            this.artifactMap = (Hashtable)info.GetValue("ArtifactMap", typeof(Hashtable));
            this.nbrRelatedCaseLinks = (int)info.GetValue("NbrRelatedCaseLinks", typeof(int));
            this.nbrSpecifiedByLinks = (int)info.GetValue("NbrSpecifiedByLinks", typeof(int));
            this.nbrVerifiedByLinks = (int)info.GetValue("NbrVerifiedByLinks", typeof(int));
            this.nbrNeedsUpdateLinks = (int)info.GetValue("NbrNeedsUpdateLinks", typeof(int));
            this.nbrImpactedHWLinks = (int)info.GetValue("NbrImpactedHWLinks", typeof(int));
            this.nbrUnspecifiedLinks = (int)info.GetValue("NbrUnspecifiedLinks", typeof(int));
            luceneDirectory = FSDirectory.Open(new DirectoryInfo("LuceneIndex"));
            luceneAnalyzer = new StandardAnalyzer(Version.LUCENE_30);
            luceneIndexSearcher = null;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("TextWriter", this.textWriter);
            info.AddValue("XmlWriter", this.xmlWriter);
            info.AddValue("GraphMLWriter", this.graphMLWriter);
            info.AddValue("CaseMap", this.caseMap);
            info.AddValue("ArtifactMap", this.artifactMap);
            info.AddValue("NbrRelatedCaseLinks", this.nbrRelatedCaseLinks);
            info.AddValue("NbrSpecifiedByLinks", this.nbrSpecifiedByLinks);
            info.AddValue("NbrVerifiedByLinks", this.nbrVerifiedByLinks);
            info.AddValue("NbrNeedsUpdateLinks", this.nbrNeedsUpdateLinks);
            info.AddValue("NbrImpactedHWLinks", this.nbrImpactedHWLinks);
            info.AddValue("NbrUnspecifiedLinks", this.nbrUnspecifiedLinks);
        }
        #endregion

        /// <summary>
        /// Add a new TrackerCase to the knowledge base.
        /// </summary>
        /// <param name="tc"></param>
        /// <returns></returns>
        public bool AddTrackerCase(TrackerCase tc)
        {
            bool found = false;

            foreach (TrackerCase tmp in caseMap.Values)
            {
                if (tc.ArtifactID.Equals(tmp.ArtifactID))
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                caseMap.Add(tc.ArtifactID, tc);
            }

            return !found;
        }

        /// <summary>
        /// Add a new ImpactItem to the knowledge base.
        /// </summary>
        /// <param name="sa"></param>
        /// <returns></returns>
        public bool AddArtifact(ImpactItem sa)
        {
            bool found = false;
            foreach (SoftwareArtifact tmp in artifactMap.Values)
            {
                // If we have encountered the artifact before, but not been able to determine its type, do it now.
                if (sa.ArtifactID.Equals(tmp.ArtifactID))
                {
                    ++sa.NbrInLinks;
                    if (tmp.Type.Equals("UNKNOWN"))
                    {
                        tmp.Type = sa.Type;
                    }
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                sa.NbrInLinks = 1;
                artifactMap.Add(sa.ArtifactID, sa);
            }
            return !found;
        }

        public TrackerCase GetTrackerCase(string id, bool evalMode)
        {
            if (id[0] == '#')
            {
                id = id.Remove(0, 1);
            }

            TrackerCase tc = (TrackerCase)caseMap[id];
            return (TrackerCase)caseMap[id];
        }

        public ImpactItem GetImpactItem(string id)
        {
            return (ImpactItem)artifactMap[id];
        }

        /// <summary>
        /// Analyze how a specific tracker case impacted artifacts in the past.
        /// </summary>
        /// <param name="caseID">ID of tracker case to analyze</param>
        /// <param name="similarity">How similar this tracker case is to the input case as calculated by Lucene</param>
        /// <param name="evalMode">Toggle evaluation mode, using only 90% of the available data</param>
        /// <returns>A linked list of impacted artifacts</returns>
        public LinkedList<ImpactItem> AnalyzeImpact(string caseID, double similarity, bool evalMode)
        {
            // Clear all visits
            foreach (TrackerCase tc in caseMap.Values)
            {
                tc.Visited = false;
            }

            LinkedList<ImpactItem> impactList = new LinkedList<ImpactItem>();
            TrackerCase startCase = GetTrackerCase(caseID, evalMode);        

            if (startCase != null)
            {
                impactList = startCase.FindImpact(impactList, 1, similarity);
            }
            return impactList;
        }

        public void IndexKnowlBase()
        {
            var writer = new IndexWriter(luceneDirectory, luceneAnalyzer, true, IndexWriter.MaxFieldLength.LIMITED);

            foreach (TrackerCase tc in caseMap.Values)
            {
                // Create Lucene document
                var trackerDocument = new Document();
                Field idField = new Field("ID", tc.ArtifactID, Field.Store.YES, Field.Index.NOT_ANALYZED);
                trackerDocument.Add(idField);
                Field titleField = new Field("Title", tc.Title, Field.Store.YES, Field.Index.ANALYZED);
                //titleField.Boost = 1.2F;
                trackerDocument.Add(titleField);
                Field descField = new Field("Description", tc.Description, Field.Store.YES, Field.Index.ANALYZED);
                trackerDocument.Add(descField);

                // Add Lucene document to index
                writer.AddDocument(trackerDocument);
            }

            writer.Optimize();
            writer.Dispose();
        }

        public ScoreDoc[] IdentifySimilarIssue(string queryText)
        {
            IndexReader indexReader = IndexReader.Open(luceneDirectory, true);
            luceneIndexSearcher = new IndexSearcher(indexReader);

            var queryParser = new MultiFieldQueryParser(Version.LUCENE_30, new string[] { "Title", "Description" }, luceneAnalyzer);
            Query query = null;
            try
            {
                query = queryParser.Parse(queryText);
            }
            catch (Exception exc)
            {
                return new ScoreDoc[0]; 
            }

            TopDocs resultDocs = luceneIndexSearcher.Search(query, indexReader.MaxDoc);

            luceneIndexSearcher.Dispose();

            return resultDocs.ScoreDocs;
        }

        public IndexSearcher GetIndexSearcher()
        {
            return luceneIndexSearcher;
        }

        public int GetNbrTrackerCases()
        {
            return caseMap.Count;
        }

        public int GetNbrImpactItems()
        {
            return artifactMap.Count;
        }

        public int GetTotLinks()
        {
            return nbrRelatedCaseLinks + nbrSpecifiedByLinks + nbrVerifiedByLinks + nbrNeedsUpdateLinks + nbrImpactedHWLinks + nbrUnspecifiedLinks;
        }

        public int NbrRelatedCaseLinks
        {
            get { return nbrRelatedCaseLinks; }
            set { nbrRelatedCaseLinks = value; }
        }
        public int NbrSpecifiedByLinks
        {
            get { return nbrSpecifiedByLinks; }
            set { nbrSpecifiedByLinks = value; }
        }
        public int NbrVerifiedByLinks
        {
            get { return nbrVerifiedByLinks; }
            set { nbrVerifiedByLinks = value; }
        }
        public int NbrNeedsUpdateLinks
        {
            get { return nbrNeedsUpdateLinks; }
            set { nbrNeedsUpdateLinks = value; }
        }
        public int NbrImpactedHWLinks
        {
            get { return nbrImpactedHWLinks; }
            set { nbrImpactedHWLinks = value; }
        }
        public int NbrUnspecifiedLinks
        {
            get { return nbrUnspecifiedLinks; }
            set { nbrUnspecifiedLinks = value; }
        }
    }

}
