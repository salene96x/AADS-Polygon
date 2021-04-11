
namespace AADS.Views.Track
{
    partial class TrackTable
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
            this.label6 = new System.Windows.Forms.Label();
            this.cmbHeight = new System.Windows.Forms.ComboBox();
            this.cmbBearing = new System.Windows.Forms.ComboBox();
            this.cmbSpeed = new System.Windows.Forms.ComboBox();
            this.cmbPosition = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtBearing = new System.Windows.Forms.TextBox();
            this.txtSpeed = new System.Windows.Forms.TextBox();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lbScope = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(19, 272);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 16);
            this.label6.TabIndex = 32;
            this.label6.Text = "ข้อมูล";
            // 
            // cmbHeight
            // 
            this.cmbHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbHeight.FormattingEnabled = true;
            this.cmbHeight.Items.AddRange(new object[] {
            "ft",
            "km",
            "m"});
            this.cmbHeight.Location = new System.Drawing.Point(221, 219);
            this.cmbHeight.Name = "cmbHeight";
            this.cmbHeight.Size = new System.Drawing.Size(121, 24);
            this.cmbHeight.TabIndex = 31;
            this.cmbHeight.SelectedIndexChanged += new System.EventHandler(this.cmbHeight_SelectedIndexChanged);
            // 
            // cmbBearing
            // 
            this.cmbBearing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBearing.FormattingEnabled = true;
            this.cmbBearing.Items.AddRange(new object[] {
            "degree",
            "radian"});
            this.cmbBearing.Location = new System.Drawing.Point(221, 170);
            this.cmbBearing.Name = "cmbBearing";
            this.cmbBearing.Size = new System.Drawing.Size(121, 24);
            this.cmbBearing.TabIndex = 30;
            this.cmbBearing.SelectedIndexChanged += new System.EventHandler(this.cmbBearing_SelectedIndexChanged);
            // 
            // cmbSpeed
            // 
            this.cmbSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSpeed.FormattingEnabled = true;
            this.cmbSpeed.Items.AddRange(new object[] {
            "knots",
            "kmph"});
            this.cmbSpeed.Location = new System.Drawing.Point(221, 123);
            this.cmbSpeed.Name = "cmbSpeed";
            this.cmbSpeed.Size = new System.Drawing.Size(121, 24);
            this.cmbSpeed.TabIndex = 29;
            this.cmbSpeed.SelectedIndexChanged += new System.EventHandler(this.cmbSpeed_SelectedIndexChanged);
            // 
            // cmbPosition
            // 
            this.cmbPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPosition.FormattingEnabled = true;
            this.cmbPosition.Items.AddRange(new object[] {
            "Decimal Degree",
            "Degree Decimal Minutes",
            "Degree Minute Seconds",
            "UTM",
            "MGRS",
            "GEOREF"});
            this.cmbPosition.Location = new System.Drawing.Point(221, 73);
            this.cmbPosition.Name = "cmbPosition";
            this.cmbPosition.Size = new System.Drawing.Size(121, 24);
            this.cmbPosition.TabIndex = 28;
            this.cmbPosition.SelectedIndexChanged += new System.EventHandler(this.cmbPosition_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(19, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 16);
            this.label5.TabIndex = 27;
            this.label5.Text = "หมายเลข";
            // 
            // txtKey
            // 
            this.txtKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKey.Location = new System.Drawing.Point(75, 28);
            this.txtKey.Name = "txtKey";
            this.txtKey.ReadOnly = true;
            this.txtKey.Size = new System.Drawing.Size(140, 22);
            this.txtKey.TabIndex = 26;
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(75, 316);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(87, 31);
            this.btnEdit.TabIndex = 25;
            this.btnEdit.Text = "แก้ไข";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 16);
            this.label4.TabIndex = 24;
            this.label4.Text = "ความสูง";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 16);
            this.label3.TabIndex = 23;
            this.label3.Text = "ทิศทาง";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 22;
            this.label2.Text = "ความเร็ว";
            // 
            // txtHeight
            // 
            this.txtHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeight.Location = new System.Drawing.Point(75, 219);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(140, 22);
            this.txtHeight.TabIndex = 21;
            // 
            // txtBearing
            // 
            this.txtBearing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBearing.Location = new System.Drawing.Point(75, 170);
            this.txtBearing.Name = "txtBearing";
            this.txtBearing.Size = new System.Drawing.Size(140, 22);
            this.txtBearing.TabIndex = 20;
            // 
            // txtSpeed
            // 
            this.txtSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSpeed.Location = new System.Drawing.Point(75, 123);
            this.txtSpeed.Name = "txtSpeed";
            this.txtSpeed.Size = new System.Drawing.Size(140, 22);
            this.txtSpeed.TabIndex = 19;
            // 
            // txtPosition
            // 
            this.txtPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPosition.Location = new System.Drawing.Point(75, 73);
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.Size = new System.Drawing.Size(140, 22);
            this.txtPosition.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "ตำแหน่ง";
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(209, 316);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(87, 31);
            this.btnDelete.TabIndex = 34;
            this.btnDelete.Text = "ลบ";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lbScope
            // 
            this.lbScope.AutoSize = true;
            this.lbScope.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbScope.Location = new System.Drawing.Point(72, 272);
            this.lbScope.Name = "lbScope";
            this.lbScope.Size = new System.Drawing.Size(91, 16);
            this.lbScope.TabIndex = 35;
            this.lbScope.Text = "Server / Client";
            // 
            // TrackTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbScope);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbHeight);
            this.Controls.Add(this.cmbBearing);
            this.Controls.Add(this.cmbSpeed);
            this.Controls.Add(this.cmbPosition);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.txtBearing);
            this.Controls.Add(this.txtSpeed);
            this.Controls.Add(this.txtPosition);
            this.Controls.Add(this.label1);
            this.Name = "TrackTable";
            this.Size = new System.Drawing.Size(366, 617);
            this.Load += new System.EventHandler(this.TrackTable_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbHeight;
        private System.Windows.Forms.ComboBox cmbBearing;
        private System.Windows.Forms.ComboBox cmbSpeed;
        private System.Windows.Forms.ComboBox cmbPosition;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.TextBox txtBearing;
        private System.Windows.Forms.TextBox txtSpeed;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lbScope;
    }
}
