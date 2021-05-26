
namespace AADS.Views.Polygon
{
    partial class ResourceCreation
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
            this.lbPoints = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbStatusEx = new System.Windows.Forms.ComboBox();
            this.cmbStatusIn = new System.Windows.Forms.ComboBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnEditConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbPoints
            // 
            this.lbPoints.Font = new System.Drawing.Font("TH SarabunPSK", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPoints.FormattingEnabled = true;
            this.lbPoints.ItemHeight = 22;
            this.lbPoints.Location = new System.Drawing.Point(128, 157);
            this.lbPoints.Margin = new System.Windows.Forms.Padding(5, 11, 5, 11);
            this.lbPoints.Name = "lbPoints";
            this.lbPoints.Size = new System.Drawing.Size(223, 290);
            this.lbPoints.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 157);
            this.label4.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 28);
            this.label4.TabIndex = 22;
            this.label4.Text = "พิกัดอาณาเขต :";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(128, 36);
            this.txtName.Margin = new System.Windows.Forms.Padding(5, 11, 5, 11);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(171, 35);
            this.txtName.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-1, 111);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 28);
            this.label3.TabIndex = 18;
            this.label3.Text = "สถานะการจัดการ :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 28);
            this.label2.TabIndex = 17;
            this.label2.Text = "สถานะบริหาร :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 39);
            this.label5.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 28);
            this.label5.TabIndex = 16;
            this.label5.Text = "ชื่ออาณาเขต :";
            // 
            // cmbStatusEx
            // 
            this.cmbStatusEx.FormattingEnabled = true;
            this.cmbStatusEx.Items.AddRange(new object[] {
            "Active",
            "Inactive"});
            this.cmbStatusEx.Location = new System.Drawing.Point(128, 73);
            this.cmbStatusEx.Name = "cmbStatusEx";
            this.cmbStatusEx.Size = new System.Drawing.Size(121, 36);
            this.cmbStatusEx.TabIndex = 24;
            // 
            // cmbStatusIn
            // 
            this.cmbStatusIn.FormattingEnabled = true;
            this.cmbStatusIn.Items.AddRange(new object[] {
            "Active",
            "Inactive"});
            this.cmbStatusIn.Location = new System.Drawing.Point(128, 111);
            this.cmbStatusIn.Name = "cmbStatusIn";
            this.cmbStatusIn.Size = new System.Drawing.Size(121, 36);
            this.cmbStatusIn.TabIndex = 25;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(58, 461);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(113, 46);
            this.btnConfirm.TabIndex = 26;
            this.btnConfirm.Text = "ยืนยัน";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(7, 215);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(113, 46);
            this.btnEdit.TabIndex = 28;
            this.btnEdit.Text = "แก้ไข";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Visible = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(7, 339);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(113, 46);
            this.btnDel.TabIndex = 27;
            this.btnDel.Text = "ลบ";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Visible = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("TH SarabunPSK", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(78, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 39);
            this.label1.TabIndex = 29;
            this.label1.Text = "พื้นที่กระจายทรัพยากร";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(216, 461);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(113, 46);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEditConfirm
            // 
            this.btnEditConfirm.Location = new System.Drawing.Point(7, 277);
            this.btnEditConfirm.Name = "btnEditConfirm";
            this.btnEditConfirm.Size = new System.Drawing.Size(113, 46);
            this.btnEditConfirm.TabIndex = 31;
            this.btnEditConfirm.Text = "ยืนยันแก้ไข";
            this.btnEditConfirm.UseVisualStyleBackColor = true;
            this.btnEditConfirm.Visible = false;
            this.btnEditConfirm.Click += new System.EventHandler(this.btnEditConfirm_Click);
            // 
            // ResourceCreation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnEditConfirm);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.cmbStatusIn);
            this.Controls.Add(this.cmbStatusEx);
            this.Controls.Add(this.lbPoints);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Font = new System.Drawing.Font("TH SarabunPSK", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "ResourceCreation";
            this.Size = new System.Drawing.Size(377, 1554);
            this.Load += new System.EventHandler(this.ResourceCreation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbStatusEx;
        private System.Windows.Forms.ComboBox cmbStatusIn;
        private System.Windows.Forms.Button btnConfirm;
        public System.Windows.Forms.ListBox lbPoints;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnEditConfirm;
    }
}
