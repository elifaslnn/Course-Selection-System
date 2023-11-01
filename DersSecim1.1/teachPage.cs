using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DersSecim1._1.ogrenciPanel;

namespace DersSecim1._1
{
    public partial class teachPage : Form
    {
        public int teachId;
        query queries= new query();
        public teachPage()
        {
            InitializeComponent();
        }

        public NpgsqlConnection conn = new NpgsqlConnection("server=localhost; port=5432; Database=dbDersSecim ; Username =postgres; Password=123 ");




        private void teachPage_Load(object sender, EventArgs e)
        {

            System.Drawing.Drawing2D.GraphicsPath obj = new System.Drawing.Drawing2D.GraphicsPath();
            obj.AddEllipse(0, 0, pictureBox1.Width, pictureBox1.Height);
            Region rg = new Region(obj);
            pictureBox1.Region = rg;


            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("C:/Users/Elif Aslan/Desktop/courseSelection/SpaceMono-Regular.ttf");
            foreach (Control c in this.Controls)
            {
                c.Font = new Font(pfc.Families[0], 8, FontStyle.Regular);
            }

            ///////////////
            infoPanel.Visible = false;
            messagePanel.Visible = false;
            interestPanel.Visible = false;
            RequestStudent.Visible = false;
            selectCourse.DroppedDown = true;
        }

        List<List<int>> studentRequestInfo = new List<List<int>>();
        List<string> coursesName = new List<string>();
        List<string> studentName = new List<string>();


        private void getQueries()
        {
            studentRequestInfo = queries.showRequestToTeach(teachId);//0. ogrenci id tutuyor,1. ders id tutuyor

            for (int i = 0; i < studentRequestInfo.Count; i++)
            {
                coursesName.Add(queries.getNameofCoursesId(studentRequestInfo[i][1]));
                studentName.Add(queries.getNameSurname(studentRequestInfo[i][0]));
            }
        }

        List<Button> studentBtn = new List<Button>();
        List<Label> courseLabel = new List<Label>();
        List<Button> acceptBtns= new List<Button>();
        List<Button> messageForRequestBtn = new List<Button>();
        private void createInfoLabel()
        {
            getQueries();

            int y = 80;
            for (int i = 0; i < studentRequestInfo.Count; i++)
            {
                studentBtn.Add(new Button() { Text = studentName[i], Location = new System.Drawing.Point(50, y), Size = new System.Drawing.Size(90, 30) });
                courseLabel.Add(new Label (){ Text = coursesName[i], Location = new System.Drawing.Point(170, y+10), Size = new System.Drawing.Size(90, 30) });
                messageForRequestBtn.Add(new Button() { Text = "*", Location = new System.Drawing.Point(270, y), Size = new System.Drawing.Size(30, 30), BackColor = Color.Blue });
                acceptBtns.Add(new Button() { Text = "Onay", Location = new System.Drawing.Point(330, y), Size = new System.Drawing.Size(90, 30), BackColor=Color.Green });
                y += 60;

            }
            for (int i = 0; i < studentRequestInfo.Count; i++)
            {
                approvePanel.Controls.Add(studentBtn[i]);
                approvePanel.Controls.Add(courseLabel[i]);

                acceptBtns[i].Click += new System.EventHandler(acceptBtn_click);
                approvePanel.Controls.Add(acceptBtns[i]);

                messageForRequestBtn[i].Click += new System.EventHandler(msgBtn_click);
                approvePanel.Controls.Add(messageForRequestBtn[i]);

            }

        }
        //hocaya yapılan talepleri gösteren panel
        private void requestedCoursesBtn_Click(object sender, EventArgs e)
        {
            createInfoLabel();
            interestPanel.Visible = false;
            approvePanel.Visible = true;
            RequestStudent.Visible = false;
        }

        private void removeControls()
        {

            for(int i=0;i<studentRequestInfo.Count;i++)
            {
                approvePanel.Controls.Remove(studentBtn[i]);
                approvePanel.Controls.Remove(courseLabel[i]);
                approvePanel.Controls.Remove(acceptBtns[i]);
                approvePanel.Controls.Remove(messageForRequestBtn[i]);
         
            }
            studentName.Clear();
            studentRequestInfo.Clear();
            acceptBtns.Clear();
            courseLabel.Clear();
            studentBtn.Clear();
            messageForRequestBtn.Clear();
        }
        private void acceptBtn_click(object sender, EventArgs e)
        {
            Button button= (Button) sender;
            int i = acceptBtns.IndexOf(button);
            int id = queries.getLastIdFromTable("ogrenciders")+1;
            queries.acceptStudentsRequest(id, studentRequestInfo[i][1], studentRequestInfo[i][0], "onaylandi",teachId);
            queries.deleteRequest(studentRequestInfo[i][0], studentRequestInfo[i][1],teachId);
            removeControls();
            createInfoLabel();
        }

        private void msgBtn_click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int i = messageForRequestBtn.IndexOf(clickedButton);
            messagePanel.Visible = true;
            messageFromLabel.Text = "Gönderen: " + queries.getTeacherNameForTeachId(teachId);

            messageToLabel.Text = "Alıcı: " + studentName[i];
            sendMsjBtn.Tag = new ButtonIndices(i,0);

        }

        private void sendMsjBtn_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            ButtonIndices buttonIndices = (ButtonIndices)clickedButton.Tag;
            int i = buttonIndices.I;
            int j = buttonIndices.J;
            string message = messageTextBox.Text;
            int id = queries.getLastIdFromTable("ogrenciyemesaj") + 1;
            if (message.Length == 0)
            {
                MessageBox.Show("Lütfen Mesajı Giriniz");
            }
            else
            {
                queries.setMessage(id, studentRequestInfo[i][0],teachId, message,"ogrenciyemesaj");
                MessageBox.Show("mesajınız gönderildi!");
                messagePanel.Visible = false;
            }
        }

        private void ExitMsjBtn_Click(object sender, EventArgs e)
        {
            messagePanel.Visible = false;
        }
        /// <summary>
        /// ///İLGİ ALANI
        /// </summary>

        List<CheckBox> interestsCb = new List<CheckBox>();
        List<int> interestId=new List<int>();
        List<int> teachersInterest = new List<int>();
        List<Label> teachersInterstLabel = new List<Label>();
        List<Button> deleteInterestBtn = new List<Button>();
        private void settingBtn_Click(object sender, EventArgs e)//İLGİ ALANI SEÇME PANELİ
        {

            selectSettings();

        }
        public void getTeachersInterest()//SEÇTİĞİN İLGİ ALANLARINI EKLE
        {
            int y = 80;
            teachersInterest = queries.getTeachsInterest(teachId);
            for (int i = 0; i < teachersInterest.Count; i++)
            {
                teachersInterstLabel.Add(new Label() { Text = queries.getInterestsName(teachersInterest[i]), Location = new System.Drawing.Point(20, y) });
                deleteInterestBtn.Add(new Button() { Text = "Sil", Location = new System.Drawing.Point(150, y), BackColor = Color.Red });
                y += 40;
            }
            for (int i = 0; i < teachersInterest.Count; i++)
            {
                showInterestPanel.Controls.Add(teachersInterstLabel[i]);
                deleteInterestBtn[i].Click += new System.EventHandler(deleteBtn_click);
                showInterestPanel.Controls.Add(deleteInterestBtn[i]);
            }
        }

        public void selectSettings()
        {
            RequestStudent.Visible = false;
            approvePanel.Visible = false;
            interestPanel.Visible = true;
            interestId = queries.getAllInterestId(teachId);
            int y = 80;
            for (int i = 0; i < interestId.Count; i++)
            {
                interestsCb.Add(new CheckBox() { Text = queries.getInterestsName(interestId[i]), Location = new System.Drawing.Point(50, y) });
                y += 40;
            }

            for (int i = 0; i < interestId.Count; i++)
            {
                selectInterestPanel.Controls.Add(interestsCb[i]);
            }
            getTeachersInterest();

        }

        private void approveInterestBtn_Click(object sender, EventArgs e)//ilgi alanı EKLE ONAY
        {
            for (int i = 0; i < interestsCb.Count; i++)
            {
                int id = queries.getLastIdFromTable("ilgialanihoca") + 1;
                if (interestsCb[i].Checked)
                {
                    queries.setIntrestToTeacher(id, interestId[i],teachId);
                }
            }
            removeLabelFromInterest();

            selectSettings();
            
        }

        public void removeLabelFromInterest()
        {
            for(int i=0;i<deleteInterestBtn.Count;i++)
            {
                showInterestPanel.Controls.Remove(deleteInterestBtn[i]);
                showInterestPanel.Controls.Remove(teachersInterstLabel[i]);
            }
            teachersInterest.Clear();
            deleteInterestBtn.Clear();
            teachersInterstLabel.Clear();

            for(int i = 0; i < interestsCb.Count; i++)
            {
                selectInterestPanel.Controls.Remove(interestsCb[i]);
            }
            interestId.Clear();
            interestsCb.Clear();
        }

        public void deleteBtn_click(object sender, EventArgs e)
        {
            selectSettings();
            Button button =(Button)sender;
            int i= deleteInterestBtn.IndexOf(button);

            queries.deleteInterest(teachId, teachersInterest[i]);

            removeLabelFromInterest();
            selectSettings();
        }

        /// <summary>
        /// TALEBİ ONAYLANMAYAN ÖĞRENCİLERİ GÖSTER 
        /// </summary>

        List<int> notCorfirmedStudent= new List<int>();
        private void ShowStudentBtn_Click(object sender, EventArgs e)
        {
            List<int> coursesId = new List<int>();
            RequestStudent.Visible = true; 
            //hocanın verdiği dersleri seç
            coursesId = queries.getTeachersCourse(teachId);
            for(int i=0; i<coursesId.Count; i++)
            {
                selectCourse.Items.Add(queries.getNameofCoursesId(coursesId[i]));
            }

            //notCorfirmedStudent = queries.getNotCorfirmedStudent(teachId,);

        }

        private void showStudentForCourse()
        {
        }


    }
}
