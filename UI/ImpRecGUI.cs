using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

// Lucene
using Lucene.Net.Search;

namespace ImpRec
{
    public partial class ImpRecGUI : Form
    {
        private const String VERSION = "1.8";
        /*******************************************/
        /* VERSION HISTORY                         */
        /* 1.0 - Released to Team A in Sweden      */
        /* 1.1 - Improved comparedTo in ImpactItem */
        /* 1.2 - Tuned parameters for Rc@20        */
        /* 1.3 - Corrected an IA parsing bug       */
        /* 1.4 - Auto-scroll to selected II        */
        /* 1.5 - Corrected time stamp in user log  */
        /* 1.6 - Removed characters from query     */
        /* 1.7 - Corrected Xml in user log         */
        /* 1.8 - Corrected unspec links in KB      */
        /*******************************************/

        private KnowlBase knowlBase;
        private KnowlParser knowlParser;
        private Serializer ser;
        private SessionInfo session;

        private String KNOWLBASE_FILENAME = "knowlBase.dat";

        private bool freshSearch; // attribute used to keep track of the checkboxes
        LinkedList<String> selectionList;
        
        public ImpRecGUI()
        {
            InitializeComponent();
            ser = new Serializer();
            knowlBase = new KnowlBase();
            knowlParser = new KnowlParser(this, knowlBase);
            freshSearch = false;
            selectionList = new LinkedList<string>();
        }

        /// <summary>
        /// Everything that is required at startup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImpRecGUI_Load(object sender, EventArgs e)
        {
            this.Text = "ImpRec " + VERSION;
            session = new SessionInfo(VERSION);
            session.StoreToolStart();
            try
            {
                knowlBase = ser.DeSerializeObject(KNOWLBASE_FILENAME);
            }
            catch (Exception exc)
            {
                DialogResult res = MessageBox.Show("First time the tool is started? Knowledge base needs to be built.", "Knowledge base missing", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (res.Equals(DialogResult.OK))
                {
                    MessageBox.Show("Give me a minute...");
                    BuildKnowledgebase();
                    MessageBox.Show("Done!");
                }
            }
        }

        /// <summary>
        /// Make sure we log the close time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImpRecGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            session.RegisterClose();
            session.StoreToolClose();
        }

        /// <summary>
        /// Build the knowledge base. Creates a Lucene index and parses the semantic network. Also stores IA reports and
        /// parses the translation file.
        /// </summary>
        private void BuildKnowledgebase()
        {
            // Build the knowledge base
            knowlBase = new KnowlBase();
            knowlParser = new KnowlParser(this, knowlBase);
            knowlParser.ParseIssueText();
            knowlBase.IndexKnowlBase();
            knowlParser.ParseSemNet();
            knowlParser.ParseTranslation();
            knowlParser.ParseImpactAnalyses();
            knowlParser.CalcCentralities();

            // serialize it to knowlbase.dat
            try
            {
                ser.SerializeObject(KNOWLBASE_FILENAME, knowlBase);
            }
            catch (Exception exc)
            {
            }
            session.WriteRebuildAction();
        }

        /// <summary>
        /// Perform search in knowledge base.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtImpact_Click(object sender, EventArgs e)
        {
            // Remove help text
            lstRelated.Visible = true;
            lstImpacted.Visible = true;
            lblHelp1.Visible = false;
            lblHelp2.Visible = false;
            freshSearch = true;
            selectionList.Clear();

            // Remove all characters that have special meaning in Lucene
            txtInput.Text = txtInput.Text.Replace('\"', ' ');
            txtInput.Text = txtInput.Text.Replace('[', ' ');
            txtInput.Text = txtInput.Text.Replace(']', ' ');
            txtInput.Text = txtInput.Text.Replace('!', ' ');
            txtInput.Text = txtInput.Text.Replace(':', ' ');
            txtInput.Text = txtInput.Text.Replace('(', ' ');
            txtInput.Text = txtInput.Text.Replace(')', ' ');

            lstRelated.Items.Clear();
            lstImpacted.Items.Clear();
            lstRelated.BeginUpdate();
            lstImpacted.BeginUpdate();
            knowlParser.CalcCentralities();

            // Get starting points based on textual similarity
            ScoreDoc[] luceneHits = knowlBase.IdentifySimilarIssue(txtInput.Text);
            IndexSearcher luceneIndexSearcher = knowlBase.GetIndexSearcher();

            if (luceneHits.Length == 0)
            {
                ListViewItem lsi = new ListViewItem("-");
                lsi.SubItems.Add("-");
                lsi.SubItems.Add("-");
                lsi.SubItems.Add("-");
                lsi.SubItems.Add("-");
                lsi.SubItems.Add("No similar tracker cases in knowledge base!");
                lstRelated.Items.Add(lsi);
            }

            int count = 0;
            LinkedList<ImpactItem> impactList = new LinkedList<ImpactItem>();

            // Consider the 76 first search hits from Lucene
            foreach (ScoreDoc searchHit in luceneHits)
            {
                ++count;
                var documentFromSearcher = luceneIndexSearcher.Doc(searchHit.Doc);

                // Conduct searches from the Lucene hit
                LinkedList<ImpactItem> tmpList = knowlBase.AnalyzeImpact(documentFromSearcher.Get("ID"), searchHit.Score, false);

                TrackerCase tmpTC = knowlBase.GetTrackerCase(documentFromSearcher.Get("ID"), false);
                
                ListViewItem lsi = new ListViewItem("");
                if (tmpTC.IAREPORT.Length > 0)
                {
                    lsi.SubItems.Add("X");
                }
                else
                {
                    lsi.SubItems.Add("");
                }
                if (tmpTC.GetAllLinks().Count > 0)
                {
                    lsi.SubItems.Add(tmpTC.GetAllLinks().Count.ToString());
                }
                else 
                {
                    lsi.SubItems.Add("");
                }

                lsi.SubItems.Add(Math.Round(searchHit.Score, 2).ToString());
                lsi.SubItems.Add("#" + documentFromSearcher.Get("ID"));
                lsi.SubItems.Add(documentFromSearcher.Get("Title"));
                lstRelated.Items.Add(lsi);

                // Add the hits to a merged impact list
                foreach (ImpactItem ii in tmpList)
                {
                    // if already found, increase the ranking rating. Otherwise add new impact item.
                    LinkedListNode<ImpactItem> tempNode = impactList.Find(ii);
                    if (tempNode != null)
                    {
                        tempNode.Value.RankingValue += ii.RankingValue;
                    }
                    else
                    {
                        impactList.AddLast(ii);
                    }
                }

                if (count == 76) 
                {
                    break;
                }
            }

            // sort and present
            ImpactItem[] impactArray = impactList.ToArray();
            Array.Sort(impactArray);
            count = 0;

            if (impactArray.Length == 0)
            {
                ListViewItem lsi = new ListViewItem("");
                lsi.SubItems.Add("-");
                lsi.SubItems.Add("-");
                lsi.SubItems.Add("-");
                lsi.SubItems.Add("No previous impact can be recommended based on similar tracker cases!");
                lstImpacted.Items.Add(lsi);
            }

            foreach (ImpactItem ii in impactArray)
            {
                ListViewItem lsi = new ListViewItem("");
                lsi.SubItems.Add(Math.Round(ii.RankingValue, 2).ToString());
                lsi.SubItems.Add(ii.ArtifactID);
                lsi.SubItems.Add(ii.Type);
                lsi.SubItems.Add(ii.Title);
                lstImpacted.Items.Add(lsi);
            }

            lstRelated.EndUpdate();
            lstImpacted.EndUpdate();

            session.WriteSearchAction(txtTracker.Text, txtInput.Text, luceneHits.Length, impactArray.Length);
        }

        /// <summary>
        /// Disable button if there is no input in the text field for investigated tracker case
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTracker_TextChanged(object sender, EventArgs e)
        {
            if (txtTracker.Text.Length > 0)
            {
                btnImpact.Enabled = true;
                btnDone.Enabled = true;
            }
            else
            {
                btnImpact.Enabled = false;
                btnDone.Enabled = false;
            }
        }

        /// <summary>
        /// Reflect selection in lstImpacted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstRelated_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset all selections in lstImpacted
            for (int k = 0; k < lstImpacted.Items.Count; k++)
            {
                lstImpacted.Items[k].BackColor = Color.White;
            }

            // Find impact items pointed out as impacted by selected tracker cases
            for (int i = 0; i < lstRelated.SelectedItems.Count; i++)
            {
                LinkedList<TrackerCase> selectedList = new LinkedList<TrackerCase>();
                TrackerCase tc = knowlBase.GetTrackerCase(lstRelated.SelectedItems[i].SubItems[4].Text, false);
                if (tc != null)
                {
                    LinkedList<ImpactItem> selectedImpactList = tc.GetAllLinks();

                    int posOfHighest = selectedImpactList.Count;
                    foreach (ImpactItem ii in selectedImpactList)
                    {
                        for (int j = 0; j < lstImpacted.Items.Count; j++)
                        {
                            if (ii.ArtifactID.Equals(lstImpacted.Items[j].SubItems[2].Text))
                            {
                                lstImpacted.Items[j].BackColor = Color.LightBlue;

                                if (j < posOfHighest)
                                {
                                    posOfHighest = j;
                                }
                            }
                        }
                    }

                    // Scroll the right listview to show the first selected item
                    lstImpacted.EnsureVisible(posOfHighest);
                }
            }      

        }

        /// <summary>
        /// Some changes made to selections, add to session info.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstRelated_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lstRelated.Items.Count != 1 && !lstRelated.Items[0].SubItems[1].Text.Equals("-"))
            {
                string id = e.Item.SubItems[4].Text;
                double sim = Double.Parse(e.Item.SubItems[3].Text);
                session.TCSelected(id, e.Item.Index + 1, sim); // 0-based index
            }
        }

        /// <summary>
        /// A check box is toggled in the list view of related tracker cases.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstRelated_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                freshSearch = false;
            }

            if (!freshSearch && lstRelated.Items.Count != 1 && !lstRelated.Items[0].SubItems[1].Text.Equals("-"))
            {
                string id = e.Item.SubItems[4].Text;
                double sim = Double.Parse(e.Item.SubItems[3].Text);
                session.ToggleRelatedCase(id, e.Item.Index + 1, sim, e.Item.Checked); // 0-based index
            }
        }

        /// <summary>
        /// A tracker case has been clicked, update TC description.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstRelated_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem theClickedOne = lstRelated.GetItemAt(e.X, e.Y);
            string id = theClickedOne.SubItems[4].Text;
            TrackerCase tc = knowlBase.GetTrackerCase(id, false);
            if (tc != null)
            {
                txtDesc.Text = tc.Title + "\n\r\n\r" + tc.Description;
                if (tc.IAREPORT.Length > 0)
                {
                    txtDesc.Text += "\n\r\n\r\n\r" + tc.IAREPORT;
                }
            }
        }

        /// <summary>
        /// A check box is toggled in the list view of impacted items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstImpacted_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                freshSearch = false;
            }

            // Make sure there is not a dummy list...
            if (!freshSearch && lstImpacted.Items.Count != 1 && !lstImpacted.Items[0].SubItems[1].Text.Equals("-"))
            {
                string id = e.Item.SubItems[2].Text;
                double sim = Double.Parse(e.Item.SubItems[1].Text);
                session.ToggleImpactedItem(id, e.Item.Index, sim, e.Item.Checked);
            }
        }

        /// <summary>
        /// Some changes made to selections, add to session info.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstImpacted_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            // Make sure there is not a dummy list...
            if (lstImpacted.Items.Count != 1 && !lstImpacted.Items[0].SubItems[1].Text.Equals("-"))
            {
                string id = e.Item.SubItems[2].Text;
                double sim = Double.Parse(e.Item.SubItems[1].Text);
                session.IISelected(id, e.Item.Index, sim);
            }
        }

        /// <summary>
        /// User has clicked Done to confirm the current selection of software artifacts.
        /// Store checked items in sessioninfo and copy IDs to clipboard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDone_Click(object sender, EventArgs e)
        {
            // Prepare a string to copy to clipboard
            String clipboardText = "### ImpRec (version " + VERSION + " output ###\r\n";

            LinkedList<SoftwareArtifact> resList = new LinkedList<SoftwareArtifact>();
            // The user has clicked done, we store the current check box choices as correct and copy to clipboard
            clipboardText += "Related tracker cases: ";
            foreach (ListViewItem lvi in lstRelated.CheckedItems)
            {
                if (lvi.SubItems[4].Text.Length > 1)
                {
                    String tempTC = lvi.SubItems[4].Text.Remove(0, 1);
                    resList.AddLast(new TrackerCase(tempTC));
                    clipboardText += "#" + tempTC + " ";
                }
            }
            clipboardText += "\r\nImpacted artifacts: ";
            foreach (ListViewItem lvi in lstImpacted.CheckedItems)
            {
                String tempArtifact = lvi.SubItems[2].Text;
                ImpactItem ii = knowlBase.GetImpactItem(tempArtifact);
                resList.AddLast(ii);
                clipboardText += tempArtifact + " ";
            }
            session.ConfirmArtifacts(txtTracker.Text, resList);

            MessageBox.Show("Thank you! Your answers have been saved and copied to the clipboard");
            Clipboard.SetText(clipboardText);            
        }

    }
}
