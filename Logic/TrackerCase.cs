using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ImpRec
{
    /// <summary>
    /// A TrackerCase represents an issue report.
    /// </summary>
    [Serializable()]
    public class TrackerCase : SoftwareArtifact
    {
        private String description;
        private String IAReport;
        private Boolean visited;
        private Boolean safetyCritical;

        private LinkedList<TrackerCase> relatedCases;
        private LinkedList<ImpactItem> needsUpdateLinks;
        private LinkedList<ImpactItem> impactedHWLinks;
        private LinkedList<ImpactItem> specifiedByLinks;
        private LinkedList<ImpactItem> verifiedByLinks;
        private LinkedList<ImpactItem> unspecifiedLinks;

        // Parameter tuning, based on training set
        public const int MAX_LEVELS_FOLLOW = 3;
        public const double CENT_WEIGHT = 1;
        public const double SIM_WEIGHT = 1;
        public const double LEVEL_PENELTY = 0.73;
        public const double ALPHA = 0.83;

        public TrackerCase()
        {
        }

        public TrackerCase(/*KnowlBase an, */string iCaseID)
            : base(/*an, */iCaseID, "TrackerCase")
        {
            IAReport = "";
            visited = false;
            safetyCritical = false;
            relatedCases = new LinkedList<TrackerCase>();
            needsUpdateLinks = new LinkedList<ImpactItem>();
            impactedHWLinks = new LinkedList<ImpactItem>();
            specifiedByLinks = new LinkedList<ImpactItem>();
            verifiedByLinks = new LinkedList<ImpactItem>();
            unspecifiedLinks = new LinkedList<ImpactItem>();
        }

        #region Serialization
        public TrackerCase(SerializationInfo info, StreamingContext ctxt)
        {
            this.title = (String)info.GetValue("Title", typeof(String));
            this.description = (String)info.GetValue("Description", typeof(String));
            this.IAReport = (String)info.GetValue("IAReport", typeof(String));
            this.visited = (Boolean)info.GetValue("Visited", typeof(Boolean));
            this.safetyCritical = (Boolean)info.GetValue("SafetyCritical", typeof(Boolean));
            this.relatedCases = (LinkedList<TrackerCase>)info.GetValue("RelatedCases", typeof(LinkedList<TrackerCase>));
            this.needsUpdateLinks = (LinkedList<ImpactItem>)info.GetValue("NeedsUpdateLinks", typeof(LinkedList<ImpactItem>));
            this.impactedHWLinks = (LinkedList<ImpactItem>)info.GetValue("ImpactedHWLinks", typeof(LinkedList<ImpactItem>));
            this.specifiedByLinks = (LinkedList<ImpactItem>)info.GetValue("SpecifiedByLinks", typeof(LinkedList<ImpactItem>));
            this.verifiedByLinks = (LinkedList<ImpactItem>)info.GetValue("VerifiedByLinks", typeof(LinkedList<ImpactItem>));
            this.unspecifiedLinks = (LinkedList<ImpactItem>)info.GetValue("UnspecifiedLinks", typeof(LinkedList<ImpactItem>));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Title", this.title);
            info.AddValue("Description", this.description);
            info.AddValue("IAReport", this.IAReport);
            info.AddValue("Visited", this.visited);
            info.AddValue("SafetyCritical", this.safetyCritical);
            info.AddValue("RelatedCases", this.relatedCases);
            info.AddValue("NeedsUpdateLinks", this.needsUpdateLinks);
            info.AddValue("ImpactedHWLinks", this.impactedHWLinks);
            info.AddValue("SpecifiedByLinks", this.specifiedByLinks);
            info.AddValue("VerifiedByLinks", this.verifiedByLinks);
            info.AddValue("UnspecifiedLinks", this.unspecifiedLinks);
        }
        #endregion

        public String Description
        {
            get { return description; }
            set { description = value; }
        }


        public String IAREPORT
        {
            get { return IAReport; }
            set { IAReport = value; }
        }


        public Boolean Visited
        {
            get { return visited; }
            set { visited = value; }
        }


        public Boolean SafetyCritical
        {
            get { return safetyCritical; }
            set { safetyCritical = value; }
        }

        public void AddRelatedCase(TrackerCase tc)
        {
            relatedCases.AddLast(tc);
        }

        public void AddNeedsUpdateLink(ImpactItem ii)
        {
            if (!needsUpdateLinks.Contains(ii))
            {
                needsUpdateLinks.AddLast(ii);
            }
        }

        public void AddImpactedHWLink(ImpactItem ii)
        {
            if (!impactedHWLinks.Contains(ii))
            {
                impactedHWLinks.AddLast(ii);
            }
        }

        public void AddSpecifiedByLink(ImpactItem ii)
        {
            if (!specifiedByLinks.Contains(ii))
            {
                specifiedByLinks.AddLast(ii);
            }
        }

        public void AddVerifiedByLink(ImpactItem ii)
        {
            if (!verifiedByLinks.Contains(ii))
            {
                verifiedByLinks.AddLast(ii);
            }
        }

        public void AddUnspecifiedLink(ImpactItem ii)
        {
            if (!unspecifiedLinks.Contains(ii))
            {
                unspecifiedLinks.AddLast(ii);
            }
        }

        public LinkedList<TrackerCase> GetRelatedCases()
        {
            return relatedCases;
        }

        public LinkedList<ImpactItem> GetNeedsUpdateLinks()
        {
            return needsUpdateLinks;
        }

        public LinkedList<ImpactItem> GetImpactedHWLinks()
        {
            return impactedHWLinks;
        }

        public LinkedList<ImpactItem> GetSpecifiedByLinks()
        {
            return specifiedByLinks;
        }

        public LinkedList<ImpactItem> GetVerifiedByLinks()
        {
            return verifiedByLinks;
        }

        public LinkedList<ImpactItem> GetUnspecifiedLinks()
        {
            return unspecifiedLinks;
        }

        public LinkedList<ImpactItem> GetAllLinks()
        {
            LinkedList<ImpactItem> mergedList = new LinkedList<ImpactItem>();

            foreach (ImpactItem ii in needsUpdateLinks)
            {
                if (!mergedList.Contains(ii))
                {
                    mergedList.AddLast(ii);
                }
            }

            foreach (ImpactItem ii in impactedHWLinks)
            {
                if (!mergedList.Contains(ii))
                {
                    mergedList.AddLast(ii);
                }
            }

            foreach (ImpactItem ii in specifiedByLinks)
            {
                if (!mergedList.Contains(ii))
                {
                    mergedList.AddLast(ii);
                }
            }

            foreach (ImpactItem ii in verifiedByLinks)
            {
                if (!mergedList.Contains(ii))
                {
                    mergedList.AddLast(ii);
                }
            }

            foreach (ImpactItem ii in unspecifiedLinks)
            {
                if (!mergedList.Contains(ii))
                {
                    mergedList.AddLast(ii);
                }
            }
            return mergedList;
        }

        /// <summary>
        /// Recursive function to find impact starting from a specific TC. 
        /// </summary>
        /// <param name="impactList"></param>
        /// <param name="level"></param>
        /// <param name="startSimilarity"></param>
        /// <returns></returns>
        public LinkedList<ImpactItem> FindImpact(LinkedList<ImpactItem> impactList, double level, double startSimilarity)
        {
            if (Visited)
            {
                return impactList;
            }
            else
            {
                Visited = true;
            }
            if (level > MAX_LEVELS_FOLLOW)
            {
                return impactList;
            }

            // Deal with the current TrackerCase
            LinkedList<ImpactItem> tmpList = GetAllLinks();
            foreach (ImpactItem tmp in tmpList)
            {
                if (tmp.LevelsFromSource == -1)
                {
                    tmp.LevelsFromSource = level;
                }
                else if (tmp.LevelsFromSource > level)
                {
                    tmp.LevelsFromSource = level;
                }
              
                if (impactList.Contains(tmp))
                {
                    tmp.RankingValue += (ALPHA * tmp.Centrality + (1 - ALPHA) * startSimilarity) / (1 + level * LEVEL_PENELTY);
                    tmp.NbrInLinks++;
                }
                else
                {
                    tmp.RankingValue = (ALPHA * tmp.Centrality + (1 - ALPHA) * startSimilarity) / (1 + level * LEVEL_PENELTY);
                    impactList.AddLast(tmp);
                }
            }

            // Call the next TrackerCase to analyze
            foreach (TrackerCase next in relatedCases)
            {
                if (!next.Visited)
                {
                    impactList = next.FindImpact(impactList, level + 1, startSimilarity);
                }
            }
            return impactList;
        }

        public override String ToXmlElement()
        {
            return "<TrackerCase>" + artifactID + "</TrackerCase>";
        }
    }
}
