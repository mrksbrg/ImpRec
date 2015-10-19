using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ImpRec
{
    /// <summary>
    /// Logic for parsing both textual descriptions and the semantic network (xml).
    /// </summary>
    class KnowlParser
    {
        private ImpRecGUI gui;
        private KnowlBase kb;
        private short nodeMode;
        private TrackerCase currentTrackerCase;

        private int count;

        public KnowlParser(ImpRecGUI gui, KnowlBase kb)
        {
            this.gui = gui;
            this.kb = kb;
            nodeMode = -1;
            currentTrackerCase = null;
        }

        /// <summary>
        /// Parses the input file containing all textual descriptions of issues.
        /// </summary>
        public void ParseIssueText()
        {
            int lineType = 1; // 1 = id, 2 = title, 3 = desc, 4 = blank
            string line;
            TrackerCase tmpCase = null;
            int counter = 0;

            // Read the file, do cyclic parsing
            String issueText = ImpRec.Properties.Resources.issueText;

            System.IO.StringReader issueReader = new System.IO.StringReader(issueText);
            while ((line = issueReader.ReadLine()) != null)
            {
                switch (lineType)
                {
                    case 1:
                        {
                            tmpCase = new TrackerCase(line);
                            ++lineType;
                            break;
                        }
                    case 2:
                        {
                            tmpCase.Title = line;
                            ++lineType;
                            break;
                        }
                    case 3:
                        {
                            tmpCase.Description = line;
                            ++lineType;
                            kb.AddTrackerCase(tmpCase);
                            ++counter;
                            break;
                        }
                    default:
                        {
                            lineType = 1;
                            break;
                        }
                }
            }
            issueReader.Close();
        }

        /// <summary>
        /// Parses the input file containing all translations from ID to title.
        /// </summary>
        public void ParseTranslation()
        {
            string line;
            ImpactItem tmpItem = null;

            // Read the file line by line
            String translation = ImpRec.Properties.Resources.translation;

            System.IO.StringReader transReader = new System.IO.StringReader(translation);
            String[] splitLine = new String[2];
            while ((line = transReader.ReadLine()) != null)
            {
                splitLine = line.Split(',');
                tmpItem = kb.GetImpactItem(splitLine[0]);
                if (tmpItem != null)
                {
                    tmpItem.Title = splitLine[1];
                }
            }
            transReader.Close();
        }

        /// <summary>
        /// Parse IA reports to add info to TCs.
        /// </summary>
        public void ParseImpactAnalyses()
        {
            string line;

            // Read the file line by line
            String impactAnalyses = ImpRec.Properties.Resources.impactAnalyses;

            System.IO.StringReader impactReader = new System.IO.StringReader(impactAnalyses);
            bool parsingIAReport = false;
            string currentIAReport = "";

            while ((line = impactReader.ReadLine()) != null)
            {
                // set the current tracker case
                if (line.Length == 27 && Char.IsDigit(line[17]) && Char.IsDigit(line[18]) && Char.IsDigit(line[19]) && 
                    Char.IsDigit(line[20]) && Char.IsDigit(line[21]))
                {
                    currentTrackerCase = kb.GetTrackerCase(line.Substring(17, 5), false);
                    currentIAReport = "";
                    continue;
                }

                // start of IA report
                else if (line.Length >= 75)
                {
                    if (line.Substring(0, 18).Equals("** Impact Analysis"))
                    {
                        parsingIAReport = true;
                        currentIAReport += line + "\n\r\n\r";
                    }
                }

                // end of IA report
                else if (line.Length == 19)
                {
                    if (line.Substring(0, 19).Equals("</IA></TrackerCase>"))
                    {
                        parsingIAReport = false;
                        if (currentTrackerCase != null)
                        {
                            currentTrackerCase.IAREPORT = currentIAReport;
                        }
                    }
                }
                else if (parsingIAReport)
                {
                    currentIAReport += line + "\n\r\n\r";
                }

            }
            impactReader.Close();
        }

        /// <summary>
        /// Parses the input semantic network.
        /// </summary>
        public void ParseSemNet()
        {
            try
            {
                XDocument doc = XDocument.Parse(ImpRec.Properties.Resources.semanticNetwork);
                    //new XDocument(ImpRec.Properties.Resources.semanticNetwork);
                XmlReader reader = doc.CreateReader();

                //XmlTextReader reader = new XmlTextReader("input\\" + SEMANTICNETWORK);
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            Hashtable attributes = new Hashtable();
                            string strURI = reader.NamespaceURI;
                            string strName = reader.Name;
                            if (reader.AttributeCount == 2)
                            {
                                attributes.Add("ID", reader.GetAttribute("ID"));
                                attributes.Add("Safety", reader.GetAttribute("Safety"));
                            }
                            else if (reader.AttributeCount == 1)
                            {
                                attributes.Add("ID", reader.GetAttribute("ID"));
                            }

                            StartElement(strURI, strName, strName, attributes);
                            break;
                        //
                        //you can handle other cases here
                        //
                        case XmlNodeType.EndElement:
                            string strNameEnd = reader.Name;
                            break;
                        case XmlNodeType.Text:
                            string strText = reader.Value;
                            StartTextElement(strText);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (XmlException exc)
            {
            }
        }

        private void StartElement(string strURI, string strName, string strName_2, Hashtable attributes)
        {
            if (strName == "TrackerCase")
            {
                nodeMode = 1;
                currentTrackerCase = kb.GetTrackerCase(attributes["ID"].ToString(), false);
                if (currentTrackerCase == null)
                {
                    currentTrackerCase = new TrackerCase(attributes["ID"].ToString());
                    kb.AddTrackerCase(currentTrackerCase);
                }           
            }
            else if (strName == "RelatedCase")
            {
                nodeMode = 2;
            }
            else if (strName == "SpecifiedBy")
            {
                nodeMode = 3;
            }
            else if (strName == "VerifiedBy")
            {
                nodeMode = 4;
            }
            else if (strName == "NeedsUpdate")
            {
                nodeMode = 5;
            }
            else if (strName == "ImpactedHW")
            {
                nodeMode = 6;
            }
            else if (strName == "UnspecifiedLink")
            {
                nodeMode = 7;
            }
            else if (strName == "REQ")
            {
                ImpactItem newItem = new ImpactItem(attributes["ID"].ToString(), "Requirement");
                kb.AddArtifact(newItem);
            }
            else if (strName == "TEST")
            {
                ImpactItem newItem = new ImpactItem(attributes["ID"].ToString(), "Test description");
                kb.AddArtifact(newItem);
            }
            else if (strName == "HARDWARE")
            {
                ImpactItem newItem = new ImpactItem(attributes["ID"].ToString(), "Hardware library");
                kb.AddArtifact(newItem);
            }
            else if (strName == "UNSPECIFIED")
            {
                ImpactItem newItem = new ImpactItem(attributes["ID"].ToString(), "Unspecified artifact");
                kb.AddArtifact(newItem);
            }
        }

        private void StartTextElement(string strText)
        {
            if (nodeMode == 1) // Tracker case
            {
                if (currentTrackerCase == null)
                {
                    currentTrackerCase = new TrackerCase(strText);
                    kb.AddTrackerCase(currentTrackerCase);
                }              
            }
            else if (nodeMode == 2) // Related case
            {
                TrackerCase dest = kb.GetTrackerCase(strText, false);

                if (dest == null)
                {
                    dest = new TrackerCase(strText);
                    kb.AddTrackerCase(dest);
                }

                if (currentTrackerCase == null)
                {
                    currentTrackerCase = new TrackerCase(strText);
                    kb.AddTrackerCase(currentTrackerCase);
                }

                dest.NbrInLinks++;
                if (currentTrackerCase != null)
                {
                    currentTrackerCase.AddRelatedCase(dest);
                }
                ++kb.NbrRelatedCaseLinks;
            }
            else if (nodeMode == 3) // SpecifiedBy
            {
                ImpactItem dest = kb.GetImpactItem(strText);
                try
                {
                    dest.NbrInLinks++;
                    currentTrackerCase.AddSpecifiedByLink(dest);
                }
                catch (Exception exc)
                {
                }
                ++kb.NbrSpecifiedByLinks;

            }
            else if (nodeMode == 4) // VerifiedBy
            {
                ImpactItem dest = kb.GetImpactItem(strText);
                try
                {
                    dest.NbrInLinks++;
                    currentTrackerCase.AddVerifiedByLink(dest);
                }
                catch (Exception exc)
                {
                }
                ++kb.NbrVerifiedByLinks;               
            }
            else if (nodeMode == 5) // NeedsUpdate
            {
                ImpactItem dest = kb.GetImpactItem(strText);
                try
                {
                    dest.NbrInLinks++;
                    currentTrackerCase.AddNeedsUpdateLink(dest);
                }
                catch (Exception exc)
                {
                }
                ++kb.NbrNeedsUpdateLinks;             
            }
            else if (nodeMode == 6) // ImpactedHW
            {
                ImpactItem dest = kb.GetImpactItem(strText);
                try
                {
                    dest.NbrInLinks++;
                    currentTrackerCase.AddImpactedHWLink(dest);
                }
                catch (Exception exc)
                {
                }
                ++kb.NbrImpactedHWLinks;
            }
            else if (nodeMode == 7) // UnspecifiedLink
            {
                count++;
                ImpactItem dest = kb.GetImpactItem(strText);
                try
                {
                    dest.NbrInLinks++;
                    currentTrackerCase.AddUnspecifiedLink(dest);
                }
                catch (Exception exc)
                {
                }
                ++kb.NbrUnspecifiedLinks;
            }
        }

        /// <summary>
        /// Calculate normalized centrality measures for all nodes, based on inlinks.
        /// </summary>
        public void CalcCentralities()
        {
            // Find maximum number of inlinks
            int maxInLinks = -1;
            foreach (TrackerCase tc in kb.caseMap.Values)
            {
                if (tc.NbrInLinks > maxInLinks)
                {
                    maxInLinks = tc.NbrInLinks;
                }
            }
            foreach (ImpactItem ii in kb.artifactMap.Values)
            {
                if (ii.NbrInLinks > maxInLinks)
                {
                    maxInLinks = ii.NbrInLinks;
                }
            }
            // Normalize inlinks to get centrality
            foreach (TrackerCase tc in kb.caseMap.Values)
            {
                tc.Centrality = tc.NbrInLinks / (double)maxInLinks;
            }
            foreach (ImpactItem ii in kb.artifactMap.Values)
            {
                ii.Centrality = ii.NbrInLinks / (double)maxInLinks;
            }
        }


    }
}
