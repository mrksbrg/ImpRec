using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ImpRec
{
    /// <summary>
    /// An impact item represents any software artifact that can be impacted by modifications related to an issue report (TrackerCase).
    /// </summary>
    [Serializable()]
    public class ImpactItem : SoftwareArtifact, IComparable
    {
        // Nbr of levels from source artifact to this ImpactItem
        private double levelsFromSource;
        // Value used when ranking impacted artifacts
        private double rankingValue;

        public ImpactItem()
        {
        }

        public ImpactItem(string iCaseID, string iType)
            : base(iCaseID, iType)
        {
            levelsFromSource = -1;
            rankingValue = 0;
        }

        public ImpactItem(string iCaseID, string iType, int iLevelsFromSource)
            : base(iCaseID, iType)
        {
            levelsFromSource = iLevelsFromSource;
        }

        #region Serialization
        public ImpactItem(SerializationInfo info, StreamingContext ctxt)
        {
            this.artifactID = (String)info.GetValue("ArtifactID", typeof(String));
            this.type = (String)info.GetValue("Type", typeof(String));
            this.title = (String)info.GetValue("Title", typeof(String));
            this.nbrInLinks = (int)info.GetValue("NbrInLinks", typeof(int));
            this.centrality = (double)info.GetValue("Centrality", typeof(double));
            this.levelsFromSource = (int)info.GetValue("LevelsFromSource", typeof(int));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("ArtifactID", this.artifactID);
            info.AddValue("Type", this.type);
            info.AddValue("Title", this.title);
            info.AddValue("NbrInLinks", this.nbrInLinks);
            info.AddValue("Centrality", this.centrality);
            info.AddValue("LevelsFromSource", this.levelsFromSource);
        }
        #endregion

        public double LevelsFromSource
        {
            get { return levelsFromSource; }
            set { levelsFromSource = value; }
        }

        public override string ToString()
        {
            return ArtifactID + " (" + Type + ") " + "\tLevel: " + levelsFromSource + " Inlinks: " + nbrInLinks + " Centrality: " + Centrality + " RankingValue: " + rankingValue;
        }

        public override string ToXmlElement()
        {
            return "<ImpactItem>" + artifactID + "</ImpactItem>";
        }

        public int CompareTo(object obj)
        {
            if (!(obj is ImpactItem))
            {
                throw new ArgumentException("An ImpactItem object is required for comparison.");
            }

            // Order of comparison: 1) RankingValue 2) Centrality 3) Name
            if (rankingValue != ((ImpactItem)obj).RankingValue)
            {
                return rankingValue < ((ImpactItem)obj).RankingValue ? 1 : -1;
            }
            else if (centrality != ((ImpactItem)obj).Centrality)
            {
                return centrality < ((ImpactItem)obj).Centrality ? 1 : -1;
            }
            else
            {
                return artifactID.CompareTo(((ImpactItem)obj).ArtifactID);
            }
        }

        public double RankingValue
        {
            get { return rankingValue; }
            set { rankingValue = value; }
        }
    }
}
