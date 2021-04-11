
namespace AADS.Views.ShowCategory
{
    partial class Track
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnShowFaker = new System.Windows.Forms.Button();
            this.btnShowTrackTable = new System.Windows.Forms.Button();
            this.btnShowFakerCreation = new System.Windows.Forms.Button();
            this.panelShowDetail = new System.Windows.Forms.Panel();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnShowFaker);
            this.panelTop.Controls.Add(this.btnShowTrackTable);
            this.panelTop.Controls.Add(this.btnShowFakerCreation);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(366, 88);
            this.panelTop.TabIndex = 5;
            // 
            // btnShowFaker
            // 
            this.btnShowFaker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowFaker.Location = new System.Drawing.Point(188, 13);
            this.btnShowFaker.Name = "btnShowFaker";
            this.btnShowFaker.Size = new System.Drawing.Size(91, 61);
            this.btnShowFaker.TabIndex = 2;
            this.btnShowFaker.Text = "Faker";
            this.btnShowFaker.UseVisualStyleBackColor = true;
            // 
            // btnShowTrackTable
            // 
            this.btnShowTrackTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowTrackTable.Location = new System.Drawing.Point(12, 13);
            this.btnShowTrackTable.Name = "btnShowTrackTable";
            this.btnShowTrackTable.Size = new System.Drawing.Size(91, 61);
            this.btnShowTrackTable.TabIndex = 0;
            this.btnShowTrackTable.Text = "Track";
            this.btnShowTrackTable.UseVisualStyleBackColor = true;
            this.btnShowTrackTable.Click += new System.EventHandler(this.btnShowTrackTable_Click);
            // 
            // btnShowFakerCreation
            // 
            this.btnShowFakerCreation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowFakerCreation.Location = new System.Drawing.Point(100, 13);
            this.btnShowFakerCreation.Name = "btnShowFakerCreation";
            this.btnShowFakerCreation.Size = new System.Drawing.Size(91, 61);
            this.btnShowFakerCreation.TabIndex = 1;
            this.btnShowFakerCreation.Text = "Faker";
            this.btnShowFakerCreation.UseVisualStyleBackColor = true;
            this.btnShowFakerCreation.Click += new System.EventHandler(this.btnShowFakerCreation_Click);
            // 
            // panelShowDetail
            // 
            this.panelShowDetail.BackColor = System.Drawing.Color.Transparent;
            this.panelShowDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelShowDetail.Location = new System.Drawing.Point(0, 88);
            this.panelShowDetail.Name = "panelShowDetail";
            this.panelShowDetail.Size = new System.Drawing.Size(366, 617);
            this.panelShowDetail.TabIndex = 6;
            // 
            // Track
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelShowDetail);
            this.Controls.Add(this.panelTop);
            this.Name = "Track";
            this.Size = new System.Drawing.Size(366, 705);
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnShowFaker;
        private System.Windows.Forms.Button btnShowTrackTable;
        private System.Windows.Forms.Button btnShowFakerCreation;
        private System.Windows.Forms.Panel panelShowDetail;
    }
}
