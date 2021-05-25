
namespace AADS.Views.Polygon
{
    partial class RestrictedAreaCreation
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
            this.panelLabels = new System.Windows.Forms.Panel();
            this.panelEditDel = new System.Windows.Forms.Panel();
            this.btnEditConfirm = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lbPoints = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.panelLabels.SuspendLayout();
            this.panelEditDel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLabels
            // 
            this.panelLabels.Controls.Add(this.panelEditDel);
            this.panelLabels.Controls.Add(this.label2);
            this.panelLabels.Controls.Add(this.label1);
            this.panelLabels.Font = new System.Drawing.Font("TH SarabunPSK", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelLabels.Location = new System.Drawing.Point(1, 31);
            this.panelLabels.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panelLabels.Name = "panelLabels";
            this.panelLabels.Size = new System.Drawing.Size(132, 1112);
            this.panelLabels.TabIndex = 0;
            // 
            // panelEditDel
            // 
            this.panelEditDel.Controls.Add(this.btnEditConfirm);
            this.panelEditDel.Controls.Add(this.btnEdit);
            this.panelEditDel.Controls.Add(this.btnDel);
            this.panelEditDel.Location = new System.Drawing.Point(0, 81);
            this.panelEditDel.Name = "panelEditDel";
            this.panelEditDel.Size = new System.Drawing.Size(113, 184);
            this.panelEditDel.TabIndex = 29;
            this.panelEditDel.Visible = false;
            // 
            // btnEditConfirm
            // 
            this.btnEditConfirm.Location = new System.Drawing.Point(0, 104);
            this.btnEditConfirm.Name = "btnEditConfirm";
            this.btnEditConfirm.Size = new System.Drawing.Size(113, 46);
            this.btnEditConfirm.TabIndex = 29;
            this.btnEditConfirm.Text = "ยืนยัน";
            this.btnEditConfirm.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(0, 52);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(113, 46);
            this.btnEdit.TabIndex = 28;
            this.btnEdit.Text = "แก้ไข";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(0, 0);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(113, 46);
            this.btnDel.TabIndex = 27;
            this.btnDel.Text = "ลบ";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "พิกัดสร้างอาณาเขต : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "ชื่ออาณาเขต : ";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(115, 35);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(188, 32);
            this.txtName.TabIndex = 1;
            // 
            // lbPoints
            // 
            this.lbPoints.FormattingEnabled = true;
            this.lbPoints.ItemHeight = 26;
            this.lbPoints.Location = new System.Drawing.Point(115, 72);
            this.lbPoints.Name = "lbPoints";
            this.lbPoints.Size = new System.Drawing.Size(188, 238);
            this.lbPoints.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("TH SarabunPSK", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(77, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 36);
            this.label3.TabIndex = 3;
            this.label3.Text = "พื้นที่ความเข้มงวดสูง";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(115, 324);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(188, 46);
            this.btnConfirm.TabIndex = 30;
            this.btnConfirm.Text = "ยืนยัน";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // RestrictedAreaCreation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbPoints);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.panelLabels);
            this.Font = new System.Drawing.Font("TH SarabunPSK", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "RestrictedAreaCreation";
            this.Size = new System.Drawing.Size(562, 1124);
            this.Load += new System.EventHandler(this.RestrictedAreaCreation_Load);
            this.panelLabels.ResumeLayout(false);
            this.panelLabels.PerformLayout();
            this.panelEditDel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelLabels;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelEditDel;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnEditConfirm;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDel;
        public System.Windows.Forms.ListBox lbPoints;
    }
}
