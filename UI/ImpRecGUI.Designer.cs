namespace ImpRec
{
    partial class ImpRecGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImpRecGUI));
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnImpact = new System.Windows.Forms.Button();
            this.lstRelated = new System.Windows.Forms.ListView();
            this.ok = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Links = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.similarity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstImpacted = new System.Windows.Forms.ListView();
            this.ok2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.confidence = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.docID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.docTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblHelp1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTracker = new System.Windows.Forms.TextBox();
            this.pctBox = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblHelp2 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDone = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctBox)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(131, 17);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(308, 127);
            this.txtInput.TabIndex = 0;
            this.txtInput.Text = "PASTE TEXTUAL DESCRIPTION OF THE TRACKER CASE HERE\r\n";
            // 
            // btnImpact
            // 
            this.btnImpact.Enabled = false;
            this.btnImpact.Location = new System.Drawing.Point(444, 69);
            this.btnImpact.Name = "btnImpact";
            this.btnImpact.Size = new System.Drawing.Size(61, 33);
            this.btnImpact.TabIndex = 2;
            this.btnImpact.Text = "Search";
            this.btnImpact.UseVisualStyleBackColor = true;
            this.btnImpact.Click += new System.EventHandler(this.txtImpact_Click);
            // 
            // lstRelated
            // 
            this.lstRelated.CheckBoxes = true;
            this.lstRelated.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ok,
            this.IA,
            this.Links,
            this.similarity,
            this.ID,
            this.title});
            this.lstRelated.FullRowSelect = true;
            this.lstRelated.HideSelection = false;
            this.lstRelated.Location = new System.Drawing.Point(6, 19);
            this.lstRelated.Name = "lstRelated";
            this.lstRelated.Size = new System.Drawing.Size(499, 339);
            this.lstRelated.TabIndex = 3;
            this.lstRelated.UseCompatibleStateImageBehavior = false;
            this.lstRelated.View = System.Windows.Forms.View.Details;
            this.lstRelated.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstRelated_ItemChecked);
            this.lstRelated.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstRelated_ItemSelectionChanged);
            this.lstRelated.SelectedIndexChanged += new System.EventHandler(this.lstRelated_SelectedIndexChanged);
            this.lstRelated.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstRelated_MouseClick);
            // 
            // ok
            // 
            this.ok.Text = "OK?";
            this.ok.Width = 40;
            // 
            // IA
            // 
            this.IA.Text = "IA";
            this.IA.Width = 24;
            // 
            // Links
            // 
            this.Links.Text = "Links";
            this.Links.Width = 50;
            // 
            // similarity
            // 
            this.similarity.Text = "Sim.";
            this.similarity.Width = 45;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 69;
            // 
            // title
            // 
            this.title.Text = "Title";
            this.title.Width = 448;
            // 
            // lstImpacted
            // 
            this.lstImpacted.CheckBoxes = true;
            this.lstImpacted.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ok2,
            this.confidence,
            this.docID,
            this.type,
            this.docTitle});
            this.lstImpacted.FullRowSelect = true;
            this.lstImpacted.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstImpacted.HideSelection = false;
            this.lstImpacted.Location = new System.Drawing.Point(6, 18);
            this.lstImpacted.Name = "lstImpacted";
            this.lstImpacted.Size = new System.Drawing.Size(458, 470);
            this.lstImpacted.TabIndex = 4;
            this.lstImpacted.UseCompatibleStateImageBehavior = false;
            this.lstImpacted.View = System.Windows.Forms.View.Details;
            this.lstImpacted.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstImpacted_ItemChecked);
            this.lstImpacted.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstImpacted_ItemSelectionChanged);
            // 
            // ok2
            // 
            this.ok2.Text = "OK?";
            this.ok2.Width = 40;
            // 
            // confidence
            // 
            this.confidence.Text = "Conf.";
            this.confidence.Width = 50;
            // 
            // docID
            // 
            this.docID.Text = "ID";
            this.docID.Width = 140;
            // 
            // type
            // 
            this.type.Text = "Type";
            this.type.Width = 137;
            // 
            // docTitle
            // 
            this.docTitle.Text = "Title";
            this.docTitle.Width = 272;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblHelp1);
            this.groupBox1.Controls.Add(this.lstRelated);
            this.groupBox1.Location = new System.Drawing.Point(6, 170);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(511, 363);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Most Similar Tracker Cases in the Knowledge Base";
            // 
            // lblHelp1
            // 
            this.lblHelp1.AutoSize = true;
            this.lblHelp1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblHelp1.Location = new System.Drawing.Point(128, 170);
            this.lblHelp1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHelp1.Name = "lblHelp1";
            this.lblHelp1.Size = new System.Drawing.Size(217, 17);
            this.lblHelp1.TabIndex = 18;
            this.lblHelp1.Text = "Here will be similar tracker cases!";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtTracker);
            this.groupBox2.Controls.Add(this.btnImpact);
            this.groupBox2.Controls.Add(this.txtInput);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(511, 158);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Input Issue Report";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 79);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "#";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 53);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Enter Issue Report ID:";
            // 
            // txtTracker
            // 
            this.txtTracker.Location = new System.Drawing.Point(44, 76);
            this.txtTracker.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTracker.Name = "txtTracker";
            this.txtTracker.Size = new System.Drawing.Size(53, 20);
            this.txtTracker.TabIndex = 1;
            this.txtTracker.TextChanged += new System.EventHandler(this.txtTracker_TextChanged);
            // 
            // pctBox
            // 
            this.pctBox.Image = ((System.Drawing.Image)(resources.GetObject("pctBox.Image")));
            this.pctBox.Location = new System.Drawing.Point(762, 68);
            this.pctBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pctBox.Name = "pctBox";
            this.pctBox.Size = new System.Drawing.Size(52, 54);
            this.pctBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctBox.TabIndex = 2;
            this.pctBox.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblHelp2);
            this.groupBox5.Controls.Add(this.lstImpacted);
            this.groupBox5.Location = new System.Drawing.Point(523, 170);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(468, 499);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Reported Impact in Similar Issue Reports";
            // 
            // lblHelp2
            // 
            this.lblHelp2.AutoSize = true;
            this.lblHelp2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblHelp2.Location = new System.Drawing.Point(130, 209);
            this.lblHelp2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHelp2.Name = "lblHelp2";
            this.lblHelp2.Size = new System.Drawing.Size(259, 17);
            this.lblHelp2.TabIndex = 19;
            this.lblHelp2.Text = "Here will be possible impact to consider!";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtDesc);
            this.groupBox6.Location = new System.Drawing.Point(6, 539);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(511, 130);
            this.groupBox6.TabIndex = 18;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Issue Report Details";
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(6, 19);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDesc.Size = new System.Drawing.Size(499, 100);
            this.txtDesc.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(529, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 91);
            this.label1.TabIndex = 19;
            this.label1.Text = "WORKFLOW\r\n1. Enter issue report # to investigate\r\n2. Type or paste textual descri" +
    "ption.\r\n3. Click \'Search\'\r\n4. Analyze recommendations, give feedback.\r\n5. Conclu" +
    "de feedback by pressing \'Done\'\r\n\r\n";
            this.label1.UseMnemonic = false;
            // 
            // btnDone
            // 
            this.btnDone.Enabled = false;
            this.btnDone.Location = new System.Drawing.Point(34, 69);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(74, 33);
            this.btnDone.TabIndex = 6;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDone);
            this.groupBox3.Location = new System.Drawing.Point(848, 6);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(137, 158);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Conclude Feedback";
            // 
            // ImpRecGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(994, 685);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.pctBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImpRecGUI";
            this.Text = "ImpRec";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImpRecGUI_FormClosing);
            this.Load += new System.EventHandler(this.ImpRecGUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctBox)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnImpact;
        private System.Windows.Forms.ListView lstRelated;
        private System.Windows.Forms.ColumnHeader similarity;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader title;
        private System.Windows.Forms.ListView lstImpacted;
        private System.Windows.Forms.ColumnHeader confidence;
        private System.Windows.Forms.ColumnHeader docID;
        private System.Windows.Forms.ColumnHeader type;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.PictureBox pctBox;
        private System.Windows.Forms.ColumnHeader docTitle;
        private System.Windows.Forms.Label lblHelp1;
        private System.Windows.Forms.Label lblHelp2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTracker;
        private System.Windows.Forms.ColumnHeader ok;
        private System.Windows.Forms.ColumnHeader ok2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ColumnHeader IA;
        private System.Windows.Forms.ColumnHeader Links;
    }
}

