namespace Driving_Licenses_Managment
{
    partial class frmPersonDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPersonDetails));
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCloseX = new System.Windows.Forms.Button();
            this.ctrPersonInfo1 = new Driving_Licenses_Managment.CtrPersonInfo();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Image = global::Driving_Licenses_Managment.Properties.Resources.Close_322;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(1098, 600);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(187, 56);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(501, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(349, 52);
            this.label1.TabIndex = 2;
            this.label1.Text = "Person  Details";
            // 
            // btnCloseX
            // 
            this.btnCloseX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseX.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnCloseX.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCloseX.BackgroundImage")));
            this.btnCloseX.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCloseX.FlatAppearance.BorderSize = 0;
            this.btnCloseX.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseX.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCloseX.Location = new System.Drawing.Point(1274, 3);
            this.btnCloseX.Name = "btnCloseX";
            this.btnCloseX.Size = new System.Drawing.Size(33, 33);
            this.btnCloseX.TabIndex = 140;
            this.btnCloseX.UseVisualStyleBackColor = false;
            this.btnCloseX.Click += new System.EventHandler(this.btnCloseX_Click);
            // 
            // ctrPersonInfo1
            // 
            this.ctrPersonInfo1.Location = new System.Drawing.Point(38, 114);
            this.ctrPersonInfo1.Name = "ctrPersonInfo1";
            this.ctrPersonInfo1.Size = new System.Drawing.Size(1247, 463);
            this.ctrPersonInfo1.TabIndex = 0;
            // 
            // frmPersonDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1313, 679);
            this.Controls.Add(this.btnCloseX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ctrPersonInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPersonDetails";
            this.Text = "frmPersonDetails";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmPersonDetails_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmPersonDetails_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmPersonDetails_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CtrPersonInfo ctrPersonInfo1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCloseX;
    }
}