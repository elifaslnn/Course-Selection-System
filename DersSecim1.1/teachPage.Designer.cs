namespace DersSecim1._1
{
    partial class teachPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(teachPage));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.requestedCoursesBtn = new System.Windows.Forms.Button();
            this.ShowStudentBtn = new System.Windows.Forms.Button();
            this.filterBtn = new System.Windows.Forms.Button();
            this.settingBtn = new System.Windows.Forms.Button();
            this.selectCoursesPanel = new System.Windows.Forms.Panel();
            this.NameSurname = new System.Windows.Forms.Label();
            this.agno = new System.Windows.Forms.Label();
            this.qourseNameL = new System.Windows.Forms.Label();
            this.studentNameL = new System.Windows.Forms.Label();
            this.infoPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.selectCoursesPanel.SuspendLayout();
            this.infoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.agno);
            this.panel1.Controls.Add(this.NameSurname);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1437, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(338, 909);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.settingBtn);
            this.panel2.Controls.Add(this.filterBtn);
            this.panel2.Controls.Add(this.ShowStudentBtn);
            this.panel2.Controls.Add(this.requestedCoursesBtn);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1437, 128);
            this.panel2.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 329);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(338, 580);
            this.panel3.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(94, 83);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 150);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // requestedCoursesBtn
            // 
            this.requestedCoursesBtn.Location = new System.Drawing.Point(21, 74);
            this.requestedCoursesBtn.Name = "requestedCoursesBtn";
            this.requestedCoursesBtn.Size = new System.Drawing.Size(321, 32);
            this.requestedCoursesBtn.TabIndex = 3;
            this.requestedCoursesBtn.Text = "Ders Talepleri";
            this.requestedCoursesBtn.UseVisualStyleBackColor = true;
            this.requestedCoursesBtn.Click += new System.EventHandler(this.requestedCoursesBtn_Click);
            // 
            // ShowStudentBtn
            // 
            this.ShowStudentBtn.Location = new System.Drawing.Point(378, 74);
            this.ShowStudentBtn.Name = "ShowStudentBtn";
            this.ShowStudentBtn.Size = new System.Drawing.Size(321, 32);
            this.ShowStudentBtn.TabIndex = 4;
            this.ShowStudentBtn.Text = "Talebi Onaylanmayan Öğrenciler";
            this.ShowStudentBtn.UseVisualStyleBackColor = true;
            // 
            // filterBtn
            // 
            this.filterBtn.Location = new System.Drawing.Point(740, 74);
            this.filterBtn.Name = "filterBtn";
            this.filterBtn.Size = new System.Drawing.Size(321, 32);
            this.filterBtn.TabIndex = 5;
            this.filterBtn.Text = "Filtrele";
            this.filterBtn.UseVisualStyleBackColor = true;
            // 
            // settingBtn
            // 
            this.settingBtn.Location = new System.Drawing.Point(1097, 74);
            this.settingBtn.Name = "settingBtn";
            this.settingBtn.Size = new System.Drawing.Size(321, 32);
            this.settingBtn.TabIndex = 6;
            this.settingBtn.Text = "Ayarlar";
            this.settingBtn.UseVisualStyleBackColor = true;
            // 
            // selectCoursesPanel
            // 
            this.selectCoursesPanel.BackColor = System.Drawing.Color.White;
            this.selectCoursesPanel.Controls.Add(this.infoPanel);
            this.selectCoursesPanel.Controls.Add(this.studentNameL);
            this.selectCoursesPanel.Controls.Add(this.qourseNameL);
            this.selectCoursesPanel.Location = new System.Drawing.Point(206, 166);
            this.selectCoursesPanel.Name = "selectCoursesPanel";
            this.selectCoursesPanel.Size = new System.Drawing.Size(1124, 711);
            this.selectCoursesPanel.TabIndex = 5;
            // 
            // NameSurname
            // 
            this.NameSurname.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NameSurname.AutoSize = true;
            this.NameSurname.Location = new System.Drawing.Point(130, 256);
            this.NameSurname.Name = "NameSurname";
            this.NameSurname.Size = new System.Drawing.Size(44, 16);
            this.NameSurname.TabIndex = 6;
            this.NameSurname.Text = "label1";
            // 
            // agno
            // 
            this.agno.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.agno.AutoSize = true;
            this.agno.Location = new System.Drawing.Point(131, 278);
            this.agno.Name = "agno";
            this.agno.Size = new System.Drawing.Size(44, 16);
            this.agno.TabIndex = 7;
            this.agno.Text = "label1";
            // 
            // qourseNameL
            // 
            this.qourseNameL.AutoSize = true;
            this.qourseNameL.BackColor = System.Drawing.Color.Gray;
            this.qourseNameL.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.qourseNameL.Location = new System.Drawing.Point(208, 51);
            this.qourseNameL.Name = "qourseNameL";
            this.qourseNameL.Size = new System.Drawing.Size(59, 16);
            this.qourseNameL.TabIndex = 0;
            this.qourseNameL.Text = "Ders Adı";
            // 
            // studentNameL
            // 
            this.studentNameL.AutoSize = true;
            this.studentNameL.BackColor = System.Drawing.Color.Gray;
            this.studentNameL.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.studentNameL.Location = new System.Drawing.Point(71, 51);
            this.studentNameL.Name = "studentNameL";
            this.studentNameL.Size = new System.Drawing.Size(77, 16);
            this.studentNameL.TabIndex = 2;
            this.studentNameL.Text = "Ogrenci Adı";
            // 
            // infoPanel
            // 
            this.infoPanel.BackColor = System.Drawing.Color.LightGray;
            this.infoPanel.Controls.Add(this.label2);
            this.infoPanel.Controls.Add(this.label1);
            this.infoPanel.Location = new System.Drawing.Point(586, 0);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(538, 711);
            this.infoPanel.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ders Adı";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Not";
            // 
            // teachPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1775, 909);
            this.Controls.Add(this.selectCoursesPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "teachPage";
            this.Text = "teachPage";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.selectCoursesPanel.ResumeLayout(false);
            this.selectCoursesPanel.PerformLayout();
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button settingBtn;
        private System.Windows.Forms.Button filterBtn;
        private System.Windows.Forms.Button ShowStudentBtn;
        private System.Windows.Forms.Button requestedCoursesBtn;
        private System.Windows.Forms.Panel selectCoursesPanel;
        private System.Windows.Forms.Label agno;
        private System.Windows.Forms.Label NameSurname;
        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.Label studentNameL;
        private System.Windows.Forms.Label qourseNameL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}