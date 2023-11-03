using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
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
            filterPanel.Visible = false;
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
            infoPanel.Visible = false;
            filterPanel.Visible = false;
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
            infoPanel.Visible = false;
            filterPanel.Visible = false;


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

        List<int> coursesId = new List<int>(); //***************
        List<int> notConfirmedStudent = new List<int>();
        List<Button> notConfirmedStudentBtn = new List<Button>();
        private void ShowStudentBtn_Click(object sender, EventArgs e)
        {
            removeNotConfirmedStudentPanel();
            RequestStudent.Visible = true;
            infoPanel.Visible = false;
            interestPanel.Visible = false;
            filterPanel.Visible = false;
            coursesId = queries.getTeachersCourse(teachId);
            for(int i=0; i<coursesId.Count; i++)
            {
                selectCourse.Items.Add(queries.getNameofCoursesId(coursesId[i]));
            }

        }

        private void selectCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            notConfirmedStudent= queries.getNotCorfirmedStudent(coursesId[selectCourse.SelectedIndex]);
            int y = 20;
            for(int i=0;i< notConfirmedStudent.Count;i++)
            {
                notConfirmedStudentBtn.Add(new Button() { Text = queries.getNameSurname(notConfirmedStudent[i]), Location= new System.Drawing.Point(20,y), Size=new System.Drawing.Size(200,30)});
                y += 40;
            }
            for (int i = 0; i < notConfirmedStudent.Count; i++)
            {
                notConfirmedStudentBtn[i].Click += new System.EventHandler(student_click);
                panel4.Controls.Add(notConfirmedStudentBtn[i]);
            }
        }

        List<int> studentTakedCourses= new List<int>(); 
        List<Label> studentTakedCoursesInfo= new List<Label>(); 

        private void student_click(object sender, EventArgs e)
        {
            Button clikkedButton = (Button)sender;
            int studentId= notConfirmedStudent[notConfirmedStudentBtn.IndexOf(clikkedButton)];
            studentTakedCourses= queries.getStudentsTakedCoursesId(studentId);//aldığı derslerin idsi

            int y = 20;
            for(int i=0;i< studentTakedCourses.Count;i++)
            {
                studentTakedCoursesInfo.Add(new Label() { Text = queries.getNameofCoursesId(studentTakedCourses[i])+"     "+queries.getNoteofCourseForOgrenciIdandCourseId(studentId, studentTakedCourses[i]),Location=new System.Drawing.Point(20,y), Size = new System.Drawing.Size(300, 30) });
                panel5.Controls.Add(studentTakedCoursesInfo[i]);
            }

        }




        private void removeNotConfirmedStudentPanel()
        {

            for (int i = 0; i < notConfirmedStudentBtn.Count; i++)
            {
                panel4.Controls.Remove(notConfirmedStudentBtn[i]);
            }
            for(int i = 0; i < studentTakedCourses.Count; i++)
            {
                panel5.Controls.Remove(studentTakedCoursesInfo[i]);
            }
            notConfirmedStudentBtn.Clear();
            studentTakedCourses.Clear();
            coursesId.Clear();
            notConfirmedStudent.Clear();
            selectCourse.Items.Clear();
            studentTakedCoursesInfo.Clear();
        }

        /// <summary>
        /// /FILTER
        /// </summary>

        List<string> allCoursesName = new List<string>();
        List<CheckBox> courses= new List<CheckBox>();   
        List<Label> selectedCourses=new List<Label>();
        List<TextBox> aktsTextBox= new List<TextBox>();
        List<int> selectedCoursesId = new List<int>();

        private void filterBtn_Click(object sender, EventArgs e)
        {
            removeFilterPanel();
            filterBtnEvent();
        }

        private void filterBtnEvent()
        {
            filterPanel.Visible = true;
            infoPanel.Visible = false;
            interestPanel.Visible = false;
            RequestStudent.Visible = false;

            
            allCoursesName = queries.allCoursesName();
            coursesId = queries.getTeachersCourse(teachId);
            for(int i=0;i<coursesId.Count;i++)
            {
                comboBox1.Items.Add(queries.getNameofCoursesId(coursesId[i]));
            }

            int y = 20;
            for (int i = 0; i < allCoursesName.Count; i++)
            {
                courses.Add(new CheckBox() { Text = allCoursesName[i], Location = new System.Drawing.Point(20, y) });
                coursesPanel.Controls.Add(courses[i]);
                y += 40;
            }
        }

        private void button1_Click(object sender, EventArgs e) //filtrelemek için dersleri seç
        {

            int y = 20;
            bool check = true;
            int count = 0;
            for(int i = 0; i < courses.Count; i++)
            {
                if (courses[i].Checked)
                {
                    count++;
                }
            }
            if (count == 3)
            {
                for (int i = 0; i < courses.Count; i++)
                {
                    if (courses[i].Checked)
                    {
                        //MessageBox.Show(courses[i].Text + " seçilii");
                        selectedCourses.Add(new Label() { Text = courses[i].Text, Location = new System.Drawing.Point(20, y) });
                        aktsTextBox.Add(new TextBox() { Location = new System.Drawing.Point(140, y) });
                        selectedCoursesId.Add(queries.getCourseIdForCoursesName(allCoursesName[i]));
                        y += 40;
                    }
                }

                for (int i = 0; i < selectedCourses.Count; i++)
                {
                    aktsPanel.Controls.Add(selectedCourses[i]);
                    aktsPanel.Controls.Add(aktsTextBox[i]);
                }
            }
            else
            {
                MessageBox.Show("lütfen 3 tane ders seçiniz");
            }


        }

        private void removeFilterPanel()
        {

            for (int i = 0; i < allCoursesName.Count; i++)
            {
                coursesPanel.Controls.Remove(courses[i]);
            }

            allCoursesName.Clear();
            courses.Clear();
            coursesId.Clear();
            comboBox1.Items.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < selectedCourses.Count; i++)
            {
                aktsPanel.Controls.Remove(selectedCourses[i]);
                aktsPanel.Controls.Remove(aktsTextBox[i]);

            }
            for(int i = 0; i < studentsId.Count; i++)
            {
                resultPanel.Controls.Remove(students[i]);
                resultPanel.Controls.Remove(requestToStudent[i]);

            }
            selectedCourses.Clear();
            aktsTextBox.Clear();
            requestToStudent.Clear();
            students.Clear();
        }

        List<int> akts= new List<int>();
        List<Label> students= new List<Label>();    
        List<int> studentsId= new List<int>();  
        List<int> averageNot= new List<int>();
        List<Button> requestToStudent= new List<Button>();  

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < studentsId.Count; i++)
            {
                if (students.Count != 0)
                {
                    resultPanel.Controls.Remove(students[i]);
                    resultPanel.Controls.Remove(requestToStudent[i]);
                }

            }
            requestToStudent.Clear();
            students.Clear();
            studentsId.Clear();
            averageNot.Clear();
            akts.Clear();

            bool check = true;
            for(int i=0;i<selectedCourses.Count;i++)
            {
                if (aktsTextBox[i].Text.Length==0)
                {
                    MessageBox.Show("Lütfen tüm derslerin katsayısını giriniz");
                    check= false;
                }

            }
            if (check == true)
            {
                for (int i = 0; i < selectedCourses.Count; i++)
                {
                    akts.Add(int.Parse(aktsTextBox[i].Text));
                }

                studentsId = queries.allStudent(selectedCoursesId[0], selectedCoursesId[1], selectedCoursesId[2]);

                for(int i=0;i<studentsId.Count; i++)
                {
                    averageNot.Add(queries.getAverageNot(studentsId[i], selectedCoursesId[0], selectedCoursesId[1], selectedCoursesId[2], akts));

                }

                int max = averageNot[0];
                for(int i=0;i<averageNot.Count;i++)
                {
                    for(int j = i; j < averageNot.Count; j++)
                    {
                        int temp;
                        int temp2;
                        if (averageNot[i]< averageNot[j])
                        {
                            temp = averageNot[i];
                            averageNot[i] = averageNot[j];
                            averageNot[j] = temp;

                            temp2 = studentsId[i];
                            studentsId[i] =studentsId[j];
                            studentsId[j]= temp2;   

                        }
                    }
                }

                int y = 20;
                for (int i=0;i<studentsId.Count ;i++)// labelları yerleştir
                {
                    bool checkStudent = true;
                    //checkStudent = queries.checkStudentTakeCourses();
                    //if ()
                    students.Add(new Label() { Text = queries.getNameSurname(studentsId[i]) + "      Not:" + averageNot[i], Location = new System.Drawing.Point(20, y), Size = new System.Drawing.Size(350, 40) });
                    requestToStudent.Add(new Button() {Text="Talep", Location = new System.Drawing.Point(370, y) });
                    resultPanel.Controls.Add(students[i]);
                    requestToStudent[i].Click += new System.EventHandler(requestToStudent_click); 
                    resultPanel.Controls.Add(requestToStudent[i]);
                    y += 40;
                }
            }

        }


        private void requestToStudent_click(object sender, EventArgs e)
        {   
            Button clikkedButton= (Button)sender;
            int index= comboBox1.SelectedIndex;
            int id = queries.getLastIdFromTable("hocatalep")+1;
            bool check = queries.checkTeachsRequest(studentsId[requestToStudent.IndexOf(clikkedButton)], coursesId[index],teachId);
            bool checkStudent = queries.checkStudentTakeCourses(coursesId[index]);
            if (check == true && checkStudent==true)
            {
                queries.setRequestCourse(id, studentsId[requestToStudent.IndexOf(clikkedButton)], teachId, coursesId[index]);
            }
            else if(check== false)
            {
                MessageBox.Show("daha önce bu öğrenciye talepte bulundunuz!");
            }
            else if(checkStudent == false)
            {
                MessageBox.Show("daha önce bu öğrenciye talepte bulundunuz!");
            }

        }


    }
}
