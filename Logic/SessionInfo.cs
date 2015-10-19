using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImpRec
{
    /// <summary>
    /// Class describing all usage collected from a session.
    /// </summary>
    class SessionInfo
    {
        private String FILENAME = "usageStats.txt";
        private String VERSION;

        private DateTime startTime;
        private DateTime closeTime;
        private LinkedList<UserAction> actionList;
        private LinkedList<SoftwareArtifact> confirmedList;
        private string startTC;

        public SessionInfo(String version)
        {
            VERSION = version;
            actionList = new LinkedList<UserAction>();
            confirmedList = new LinkedList<SoftwareArtifact>();
            startTC = "";
            startTime = DateTime.Now;
        }

        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public DateTime CloseTime
        {
            get { return closeTime; }
            set { closeTime = value; }
        }

        public void RegisterClose()
        {
            closeTime = DateTime.Now;
        }
        
        /// <summary>
        /// Rebuild has been conducted.
        /// </summary>
        public void WriteRebuildAction()
        {
            TextWriter writer;
            if (!File.Exists(FILENAME))
            {
                writer = new StreamWriter(FILENAME);
            }
            else
            {
                writer = File.AppendText(FILENAME);
            }
            writer.WriteLine(new MainAction(1, null, null, -1, -1).ToString());
            writer.Close();
        }

        /// <summary>
        /// Search was clicked.
        /// </summary>
        /// <param name="startTC"></param>
        /// <param name="inputText"></param>
        /// <param name="iNbrRelatedCases"></param>
        /// <param name="iNbrRecommendedImpact"></param>
        public void WriteSearchAction(String startTC, String inputText, int iNbrRelatedCases, int iNbrRecommendedImpact)
        {
            TextWriter writer;
            if (!File.Exists(FILENAME))
            {
                writer = new StreamWriter(FILENAME);
            }
            else
            {
                writer = File.AppendText(FILENAME);
            }

            writer.WriteLine(new MainAction(2, startTC, inputText, iNbrRelatedCases, iNbrRecommendedImpact).ToString());
            writer.Close();
        }

        /// <summary>
        /// A checkbox was toggled.
        /// </summary>
        /// <param name="caseID"></param>
        /// <param name="pos"></param>
        /// <param name="sim"></param>
        /// <param name="relevant"></param>
        public void ToggleRelatedCase(String caseID, int pos, double sim, bool relevant)
        {
            TextWriter writer;
            if (!File.Exists(FILENAME))
            {
                writer = new StreamWriter(FILENAME);
            }
            else
            {
                writer = File.AppendText(FILENAME);
            }
            writer.WriteLine(new FeedbackAction(1, caseID, pos, sim, relevant).ToString());
            writer.Close();
        }

        /// <summary>
        /// A checkbox was toggled.
        /// </summary>
        /// <param name="artifactID"></param>
        /// <param name="pos"></param>
        /// <param name="conf"></param>
        /// <param name="relevant"></param>
        public void ToggleImpactedItem(string artifactID, int pos, double conf, bool relevant)
        {
            TextWriter writer;
            if (!File.Exists(FILENAME))
            {
                writer = new StreamWriter(FILENAME);
            }
            else
            {
                writer = File.AppendText(FILENAME);
            }
            writer.WriteLine(new FeedbackAction(2, artifactID, pos, conf, relevant).ToString());
            writer.Close();
        }

        /// <summary>
        /// A TC was clicked.
        /// </summary>
        /// <param name="artifactID"></param>
        /// <param name="pos"></param>
        /// <param name="sim"></param>
        public void TCSelected(string artifactID, int pos, double sim)
        {
            TextWriter writer;
            if (!File.Exists(FILENAME))
            {
                writer = new StreamWriter(FILENAME);
            }
            else
            {
                writer = File.AppendText(FILENAME);
            }
            writer.WriteLine(new SelectAction(1, artifactID, pos, sim).ToString());
            writer.Close();
        }

        /// <summary>
        /// An II was clicked.
        /// </summary>
        /// <param name="artifactID"></param>
        /// <param name="pos"></param>
        /// <param name="sim"></param>
        public void IISelected(string artifactID, int pos, double sim)
        {
            TextWriter writer;
            if (!File.Exists(FILENAME))
            {
                writer = new StreamWriter(FILENAME);
            }
            else
            {
                writer = File.AppendText(FILENAME);
            }
            writer.WriteLine(new SelectAction(2, artifactID, pos, sim).ToString());
            writer.Close();
        }

        /// <summary>
        /// The Done button was clicked.
        /// </summary>
        /// <param name="startTC"></param>
        /// <param name="confList"></param>
        public void ConfirmArtifacts(string startTC, LinkedList<SoftwareArtifact> confList)
        {
            this.startTC = startTC;
            confirmedList = confList;
            StoreConfirmedLinks();
        }

        /// <summary>
        /// Tool was started.
        /// </summary>
        public void StoreToolStart()
        {
            TextWriter writer;
            if (!File.Exists(FILENAME))
            {
                writer = new StreamWriter(FILENAME);
            }
            else
            {
                writer = File.AppendText(FILENAME);
            }

            writer.WriteLine("<Session>");
            writer.WriteLine("<Start>" + startTime + "</Start>");
            writer.WriteLine("<ToolVersion>" + VERSION + "</ToolVersion>");
            writer.Close();
        }

        /// <summary>
        /// Store the toggled links.
        /// </summary>
        private void StoreConfirmedLinks()
        {
            TextWriter writer;
            if (!File.Exists(FILENAME))
            {
                writer = new StreamWriter(FILENAME);
            }
            else
            {
                writer = File.AppendText(FILENAME);
            }

            writer.WriteLine("<ConfirmedArtifacts timeStamp=\"" + DateTime.Now + "\" startTC=" + startTC + ">");
            foreach (SoftwareArtifact sa in confirmedList)
            {
                if (sa != null)
                {
                    writer.WriteLine(sa.ToXmlElement());
                }
            }
            writer.WriteLine("</ConfirmedArtifacts>");

            writer.Close();
        }

        /// <summary>
        /// Tool was closed.
        /// </summary>
        public void StoreToolClose()
        {
            TextWriter writer;
            if (!File.Exists(FILENAME))
            {
                writer = new StreamWriter(FILENAME);
            }
            else
            {
                writer = File.AppendText(FILENAME);
            }

            writer.WriteLine("<Close>" + closeTime + "</Close>");
            writer.WriteLine("</Session>");

            writer.Close();
        }

        /// <summary>
        /// Super class for all actions by the user
        /// </summary>
        private abstract class UserAction
        {
            protected DateTime timeStamp;        
   
            public abstract String ToString();
        }

        /// <summary>
        /// The main actions involve clicking buttons: Search or Rebuild
        /// </summary>
        private class MainAction : UserAction
        {
            private short actionType;
            private String startTC;
            private String inputText;
            private int nbrRelatedCases;
            private int nbrRecommendedImpact;

            public MainAction(short iActionType, String iStartTC, String iInputText, int iNbrRelatedCases, int iNbrRecommendedImpact)
            {
                timeStamp = DateTime.Now;
                actionType = iActionType;
                startTC = iStartTC;
                inputText = iInputText;
                nbrRelatedCases = iNbrRelatedCases;
                nbrRecommendedImpact = iNbrRecommendedImpact;
            }

            public override String ToString()
            {
                StringBuilder tmpString = new StringBuilder();
                if (actionType == 1)
                {
                    tmpString = tmpString.Insert(tmpString.Length, "<Rebuild");
                    tmpString = tmpString.Insert(tmpString.Length, " timeStamp=\"" + timeStamp + "\"</Rebuild>");
                }
                else
                {
                    tmpString = tmpString.Insert(tmpString.Length, "<Search");
                    tmpString = tmpString.Insert(tmpString.Length, " timeStamp=\"" + timeStamp + "\"");
                    tmpString = tmpString.Insert(tmpString.Length, " startTC=\"" + startTC + "\"");
                    tmpString = tmpString.Insert(tmpString.Length, " relatedCases=\"" + nbrRelatedCases + "\"");
                    tmpString = tmpString.Insert(tmpString.Length, " recommendedItems=\"" + nbrRecommendedImpact + "\">");
                    tmpString = tmpString.Insert(tmpString.Length, inputText);
                    tmpString = tmpString.Insert(tmpString.Length, "</Search>");
                }
                return tmpString.ToString();
            }

        }

        /// <summary>
        /// Describes the actions involved in clicking checkboxes
        /// </summary>
        private class FeedbackAction : UserAction
        {
            private short feedbackType; // 1 = relevant TC, 2 = relevant II
            private string feedbackID;
            private int pos;
            private double sim;
            private bool relevant;

            public FeedbackAction(short iFeedbackType, string iFeedbackID, int iPos, double iSim, bool iRelevant)
            {
                timeStamp = DateTime.Now;
                feedbackType = iFeedbackType;
                feedbackID = iFeedbackID;
                pos = iPos;
                sim = iSim;
                relevant = iRelevant;
            }

            public override String ToString()
            {
                StringBuilder tmpString = new StringBuilder();
                if (feedbackType == 1)
                {
                    if (relevant)
                    {
                        tmpString = tmpString.Insert(tmpString.Length, "<RelevantTC");
                        tmpString = tmpString.Insert(tmpString.Length, " timeStamp=\"" + timeStamp + "\"");
                        tmpString = tmpString.Insert(tmpString.Length, " pos=\"" + pos + "\"");
                        tmpString = tmpString.Insert(tmpString.Length, " sim=\"" + sim + "\">");
                        tmpString = tmpString.Insert(tmpString.Length, feedbackID);
                        tmpString = tmpString.Insert(tmpString.Length, "</RelvantTC>");
                    }
                    else
                    {
                        tmpString = tmpString.Insert(tmpString.Length, "<CancelRelevantTC");
                        tmpString = tmpString.Insert(tmpString.Length, " timeStamp=\"" + timeStamp + "\"");
                        tmpString = tmpString.Insert(tmpString.Length, " pos=\"" + pos + "\"");
                        tmpString = tmpString.Insert(tmpString.Length, " sim=\"" + sim + "\">");
                        tmpString = tmpString.Insert(tmpString.Length, feedbackID);
                        tmpString = tmpString.Insert(tmpString.Length, "</CancelRelevantTC>");
                    }
                }
                else // feedbackType == 2 (Relvant II)
                {
                    if (relevant)
                    {
                        tmpString = tmpString.Insert(tmpString.Length, "<RelevantII");
                        tmpString = tmpString.Insert(tmpString.Length, " timeStamp=\"" + timeStamp + "\"");
                        tmpString = tmpString.Insert(tmpString.Length, " pos=\"" + pos + "\"");
                        tmpString = tmpString.Insert(tmpString.Length, " conf=\"" + sim + "\">");
                        tmpString = tmpString.Insert(tmpString.Length, feedbackID);
                        tmpString = tmpString.Insert(tmpString.Length, "</RelvantII>");
                    }
                    else
                    {
                        tmpString = tmpString.Insert(tmpString.Length, "<CancelRelevantII");
                        tmpString = tmpString.Insert(tmpString.Length, " timeStamp=\"" + timeStamp + "\"");
                        tmpString = tmpString.Insert(tmpString.Length, " pos=\"" + pos + "\"");
                        tmpString = tmpString.Insert(tmpString.Length, " conf=\"" + sim + "\">");
                        tmpString = tmpString.Insert(tmpString.Length, feedbackID);
                        tmpString = tmpString.Insert(tmpString.Length, "</CancelRelevantII>");
                    }
                }
                return tmpString.ToString();
            }

        }

        /// <summary>
        /// Describes selection actions in the two listviews.
        /// </summary>
        private class SelectAction : UserAction
        {
            private short selectionType; // 1 = TC selected, 2 = II selected
            private string selectedID;
            private int pos;
            private double sim;

            public SelectAction(short iSelectionType, string iSelectedID, int iPos, double iSim)
            {
                timeStamp = DateTime.Now;
                selectionType = iSelectionType;
                selectedID = iSelectedID;
                pos = iPos;
                sim = iSim;
            }

            public override String ToString()
            {
                StringBuilder tmpString = new StringBuilder();
                if (selectionType == 1)
                {
                    tmpString = tmpString.Insert(tmpString.Length, "<TCClicked");
                    tmpString = tmpString.Insert(tmpString.Length, " timeStamp=\"" + timeStamp + "\"");
                    tmpString = tmpString.Insert(tmpString.Length, " pos=\"" + pos + "\"");
                    tmpString = tmpString.Insert(tmpString.Length, " sim=\"" + sim + "\">");
                    tmpString = tmpString.Insert(tmpString.Length, selectedID);
                    tmpString = tmpString.Insert(tmpString.Length, "</TCClicked>");

                }
                else // feedbackType == 2 (Relvant II)
                {
                    tmpString = tmpString.Insert(tmpString.Length, "<IIClicked");
                    tmpString = tmpString.Insert(tmpString.Length, " timeStamp=\"" + timeStamp + "\"");
                    tmpString = tmpString.Insert(tmpString.Length, " pos=\"" + pos + "\"");
                    tmpString = tmpString.Insert(tmpString.Length, " conf=\"" + sim + "\">");
                    tmpString = tmpString.Insert(tmpString.Length, selectedID);
                    tmpString = tmpString.Insert(tmpString.Length, "</IIClicked>");
                }
                return tmpString.ToString();
            }
        }
    }
}
