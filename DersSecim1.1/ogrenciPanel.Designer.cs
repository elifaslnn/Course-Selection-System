﻿namespace DersSecim1._1
{
    partial class ogrenciPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ogrenciPanel));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.agno = new System.Windows.Forms.Label();
            this.NameSurname = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.courseRequestBtn = new System.Windows.Forms.Button();
            this.takedCourses = new System.Windows.Forms.Button();
            this.infoCourses = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.selectCoursesPanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.courseSelection = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.infoCourses.SuspendLayout();
            this.selectCoursesPanel.SuspendLayout();
            this.panel6.SuspendLayout();
            this.courseSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.agno);
            this.panel1.Controls.Add(this.NameSurname);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1437, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(338, 912);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 332);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(338, 580);
            this.panel2.TabIndex = 3;
            // 
            // agno
            // 
            this.agno.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.agno.AutoSize = true;
            this.agno.Location = new System.Drawing.Point(131, 286);
            this.agno.Name = "agno";
            this.agno.Size = new System.Drawing.Size(44, 16);
            this.agno.TabIndex = 2;
            this.agno.Text = "label1";
            // 
            // NameSurname
            // 
            this.NameSurname.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NameSurname.AutoSize = true;
            this.NameSurname.Location = new System.Drawing.Point(130, 261);
            this.NameSurname.Name = "NameSurname";
            this.NameSurname.Size = new System.Drawing.Size(44, 16);
            this.NameSurname.TabIndex = 1;
            this.NameSurname.Text = "label1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(94, 91);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 150);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.courseRequestBtn);
            this.panel3.Controls.Add(this.takedCourses);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1437, 128);
            this.panel3.TabIndex = 1;
            // 
            // courseRequestBtn
            // 
            this.courseRequestBtn.Location = new System.Drawing.Point(361, 63);
            this.courseRequestBtn.Name = "courseRequestBtn";
            this.courseRequestBtn.Size = new System.Drawing.Size(321, 32);
            this.courseRequestBtn.TabIndex = 3;
            this.courseRequestBtn.Text = "Ders Talebi Oluştur";
            this.courseRequestBtn.UseVisualStyleBackColor = true;
            this.courseRequestBtn.Click += new System.EventHandler(this.courseRequestBtn_Click);
            // 
            // takedCourses
            // 
            this.takedCourses.Location = new System.Drawing.Point(12, 63);
            this.takedCourses.Name = "takedCourses";
            this.takedCourses.Size = new System.Drawing.Size(321, 32);
            this.takedCourses.TabIndex = 2;
            this.takedCourses.Text = "Aldığım Dersler";
            this.takedCourses.UseVisualStyleBackColor = true;
            this.takedCourses.Click += new System.EventHandler(this.takedCourses_Click);
            // 
            // infoCourses
            // 
            this.infoCourses.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.infoCourses.AutoScroll = true;
            this.infoCourses.BackColor = System.Drawing.Color.White;
            this.infoCourses.Controls.Add(this.label4);
            this.infoCourses.Controls.Add(this.label3);
            this.infoCourses.Controls.Add(this.label2);
            this.infoCourses.Controls.Add(this.label1);
            this.infoCourses.Location = new System.Drawing.Point(176, 177);
            this.infoCourses.Name = "infoCourses";
            this.infoCourses.Size = new System.Drawing.Size(1124, 673);
            this.infoCourses.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(181, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 31);
            this.label4.TabIndex = 3;
            this.label4.Text = "Ders Adı";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(295, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 31);
            this.label3.TabIndex = 2;
            this.label3.Text = "AKTS";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(374, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "Not";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(48, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ders Kodu";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // selectCoursesPanel
            // 
            this.selectCoursesPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.selectCoursesPanel.BackColor = System.Drawing.Color.RosyBrown;
            this.selectCoursesPanel.Controls.Add(this.button1);
            this.selectCoursesPanel.Controls.Add(this.panel6);
            this.selectCoursesPanel.Controls.Add(this.courseSelection);
            this.selectCoursesPanel.Location = new System.Drawing.Point(176, 177);
            this.selectCoursesPanel.Name = "selectCoursesPanel";
            this.selectCoursesPanel.Size = new System.Drawing.Size(1124, 711);
            this.selectCoursesPanel.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(399, 638);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel6
            // 
            this.panel6.AutoScroll = true;
            this.panel6.BackColor = System.Drawing.Color.Silver;
            this.panel6.Controls.Add(this.label8);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Location = new System.Drawing.Point(649, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(475, 711);
            this.panel6.TabIndex = 2;
            this.panel6.Paint += new System.Windows.Forms.PaintEventHandler(this.panel6_Paint);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(11, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "Güncel Taleplerin";
            // 
            // panel7
            // 
            this.panel7.Location = new System.Drawing.Point(579, 38);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(542, 630);
            this.panel7.TabIndex = 1;
            // 
            // courseSelection
            // 
            this.courseSelection.AutoScroll = true;
            this.courseSelection.BackColor = System.Drawing.Color.White;
            this.courseSelection.Controls.Add(this.label5);
            this.courseSelection.Location = new System.Drawing.Point(0, 0);
            this.courseSelection.Name = "courseSelection";
            this.courseSelection.Size = new System.Drawing.Size(654, 632);
            this.courseSelection.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Talep Oluştur";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 16);
            this.label7.TabIndex = 2;
            this.label7.Text = "Ders Adı";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(147, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 16);
            this.label8.TabIndex = 3;
            this.label8.Text = "Hoca Adı";
            // 
            // ogrenciPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1775, 912);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.selectCoursesPanel);
            this.Controls.Add(this.infoCourses);
            this.Name = "ogrenciPanel";
            this.Text = "ogrenciPanel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ogrenciPanel_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.infoCourses.ResumeLayout(false);
            this.infoCourses.PerformLayout();
            this.selectCoursesPanel.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.courseSelection.ResumeLayout(false);
            this.courseSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label agno;
        private System.Windows.Forms.Label NameSurname;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button courseRequestBtn;
        private System.Windows.Forms.Button takedCourses;
        private System.Windows.Forms.Panel infoCourses;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel selectCoursesPanel;
        private System.Windows.Forms.Panel courseSelection;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}