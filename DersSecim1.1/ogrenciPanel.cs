using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using IronOcr;
using IronOcr.Assets;
using System.Runtime.ExceptionServices;
using Spire.Pdf.Exporting.XPS.Schema;

namespace DersSecim1._1
{
    public partial class ogrenciPanel : Form
    {
      

        public int ogrenciId;
        query queries = new query();
        List<Label> courses = new List<Label>();
        List<List<string>> teachersAllName = new List<List<string>>();//öğrencinin alabileceği tüm derslerin hocalarının adını tutar
        List<List<int>> teachersAllId = new List<List<int>>();//öğrencinin alabileceği tüm derslerin hocalarının idsini tutar
        List<int> studentsCoursesIdcanTake = new List<int>();
        List<string> studentsCoursesNamecanTake = new List<string>();

        public ogrenciPanel()
        {
            InitializeComponent();
            //var Ocr = new AutoOcr();
        }

        List<int> interestsId= new List<int>(); 
        private void ogrenciPanel_Load(object sender, EventArgs e)
        {
            
            System.Drawing.Drawing2D.GraphicsPath obj=new System.Drawing.Drawing2D.GraphicsPath();
            obj.AddEllipse(0, 0,pictureBox1.Width,pictureBox1.Height);
            Region rg=new Region(obj);
            pictureBox1.Region = rg;


            PrivateFontCollection pfc=new PrivateFontCollection();
            pfc.AddFontFile("C:/Users/Elif Aslan/Desktop/courseSelection/SpaceMono-Regular.ttf");
            foreach(Control c in this.Controls)
            {
                c.Font = new Font(pfc.Families[0],10,FontStyle.Regular);
            }
            foreach (Control c in infoCourses.Controls)
            {
                c.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
            }
            label1.Location = Location = new System.Drawing.Point(48, 47);
            label2.Location = Location = new System.Drawing.Point(181, 47);
            label4.Location = Location = new System.Drawing.Point(295, 47);

            ////////////////////////

            //infoOfTakedCourses();
            selectCoursesPanel.Visible = false;
            infoCourses.Visible = false;
            messagePanel.Visible = false;
            approvePanel.Visible = false;
            panel4.Visible = false;
            transkriptPanel.Visible = false;

            interestsId=queries.getInterest();


            NameSurname.Text = queries.getNameSurname(ogrenciId);

            for (int i=0;i<interestsId.Count;i++)
            {
                ilgialanlariComboB.Items.Add(queries.getInterestsName(interestsId[i]));
            }

            if (queries.getStudentsMessages(ogrenciId).Count != 0)
            {
                List<Label> messagesLabel = new List<Label>();
                List<int> messages = new List<int>();
                messages = queries.getStudentsMessages(ogrenciId);//mesajların idsi
                int y = 0;
                for (int i = 0; i < messages.Count; i++)
                {
                    messagesLabel.Add(new Label() { Text = "Gönderen:" + queries.getTeacherNameForTeachId(queries.senderTeachId(messages[i])) + "\n\n" + queries.getmessages(messages[i]), Location = new System.Drawing.Point(0, y), BorderStyle = BorderStyle.FixedSingle, Size = new System.Drawing.Size(338, 100) }); ;
                    panel2.Controls.Add(messagesLabel[i]);
                    y += 100;
                }
            }




        }

        List<List<CheckBox>> checkBoxes = new List<List<CheckBox>>();
        List<List<Button>> messagesBtn= new List<List<Button>>();  


        private void getQueries()
        {
            int number = queries.getNumberOfCanTakeCourseFromDifferentTeachers();
            if (number == 1)
                studentsCoursesIdcanTake = queries.getCoursesIdTheSutendCanTakeIfNumberIsOne(ogrenciId);//öğrencinin alması gereken derslerin idsi
            else if (number > 1)
                studentsCoursesIdcanTake = queries.getCoursesIdTheSutendCanTake(ogrenciId);
            foreach (int id in studentsCoursesIdcanTake)//her bir id ye göre öğretmen idsi ve adını tut
            {
                List<string> teachersNameforCourse = new List<string>();
                List<int> teachersIdforCourse = new List<int>();
                teachersNameforCourse = queries.getTeachersNameCanRequest(id,ogrenciId);
                teachersIdforCourse = queries.getTeachersIdCanRequest(id,ogrenciId);
                teachersAllName.Add(teachersNameforCourse);
                teachersAllId.Add(teachersIdforCourse);
                studentsCoursesNamecanTake.Add(queries.getNameofCoursesId(id));
            }
        }


        public class ButtonIndices
        {
            public int I { get; }
            public int J { get; }

            public ButtonIndices(int i, int j)
            {
                I = i;
                J = j;
            }
        }

        //alabileceği derslerin hocalarını gösteren panel
        public void createCheckBoxesforTeachers()
        {
            getQueries();
            int y = 50;
            for (int i = 0; i < teachersAllId.Count; i++)
            {
                int x = 230;
                List<CheckBox> cbox = new List<CheckBox>();
                List<Button> buttons = new List<Button>();

                    for (int j = 0; j < teachersAllId[i].Count; j++)
                    {
                        cbox.Add(new CheckBox() { Text = teachersAllName[i][j], Location = new System.Drawing.Point(x, y), Size = new System.Drawing.Size(90, 30) });
                        x += 90;
                        buttons.Add(new Button() { Text = "**", Location = new System.Drawing.Point(x, y), Size = new System.Drawing.Size(30, 30), BackColor = Color.Green });
                        
                    x += 100;
                    }
                    checkBoxes.Add(cbox);
                    messagesBtn.Add(buttons);
                if (teachersAllId[i].Count != 0)
                {
                    y += 40;
                }
            }
            for (int i = 0; i < checkBoxes.Count; i++)
            {
                for (int j = 0; j < checkBoxes[i].Count; j++)
                {
                    //messagesBtn[i][j].Click += (sender, e) => messageBtn_Click(sender, e, i, j);
                    messagesBtn[i][j].Tag = new ButtonIndices(i, j);
                    messagesBtn[i][j].Click += new System.EventHandler(messageBtn_Click);
                    courseSelection.Controls.Add(checkBoxes[i][j]);
                    courseSelection.Controls.Add(messagesBtn[i][j]);
   
                }
            }
        }

        //alabileceği derslerin label 
        public void createLabelforCourses()//derslerin yazdığı label
        {
            //getQueries();
            int y = 50;
            foreach (var courseName in studentsCoursesNamecanTake)
            {
                if (checkBoxes[studentsCoursesNamecanTake.IndexOf(courseName)].Count != 0)
                {
                    courses.Add(new Label() { Text = courseName, Location = new System.Drawing.Point(10, y), Size=new System.Drawing.Size(200,40) });
                    y += 40;
                }
            }
            foreach (var course in courses)
            {
                courseSelection.Controls.Add(course);
            }
        }

        //aldığı derslerin bilgisi panel oluşturma
        public void infoOfTakedCourses()
        {
            int y = 97;

            List<string> coursesName = new List<string>();
            coursesName = queries.getStudentsTakedCoursesName(ogrenciId);//öğrencinin aldığı dersler (labelda tutulacak)
            foreach (var course in coursesName)
            {
                int coursesId = queries.getCourseIdForCoursesName(course);
                string note = queries.getNoteofCourseForOgrenciIdandCourseId(ogrenciId, coursesId);
                string code = queries.getCoursesCode(coursesId);
                infoCourses.Controls.Add(new Label() { Text = coursesName[coursesName.IndexOf(course)], Location = new System.Drawing.Point(295, y) ,Size =new System.Drawing.Size(400,40)});//aldığı derslerin adınu tutacak labelların oluşturulması
                infoCourses.Controls.Add(new Label() { Text = note, Location = new System.Drawing.Point(181, y) });//aldığı derslerin notunu tutacak labelların oluşturulması
                infoCourses.Controls.Add(new Label() { Text = code, Location = new System.Drawing.Point(48, y) });//aldığı derslerin notunu tutacak labelların oluşturulması
                y += 40;
            }
        }
        List<Label> requestCourseNames= new List<Label>();
        List<Label> requestCoursesTeacher = new List<Label>();
        List<Button> deleteRequestBtn = new List<Button>();

        //talep oluşturduğu derslerin bilgisini gösteren panel

        List<List<int>> coursesRequested = new List<List<int>>();

        private void requestCourses()
        {
            coursesRequested = queries.getCurrentCoursesIdRequest(ogrenciId);
            int y = 80;
            for(int i = 0; i < coursesRequested.Count; i++)
            {
                string coursesName = queries.getNameofCoursesId(coursesRequested[i][0]);
                string teacherName = queries.getTeacherNameForTeachId(coursesRequested[i][1]);  
                requestCourseNames.Add(new Label() { Text = coursesName, Location= new System.Drawing.Point(10, y) });
                requestCoursesTeacher.Add(new Label() { Text = teacherName, Location = new System.Drawing.Point(120, y) });
                deleteRequestBtn.Add(new Button() {Text="Sil", Location = new System.Drawing.Point(230, y) , BackColor=Color.Red, ForeColor=Color.White});
                y += 40;
            }

            for (int i=0;i< coursesRequested.Count; i++) {
                panel6.Controls.Add(requestCourseNames[i]);
                panel6.Controls.Add(requestCoursesTeacher[i]);
                deleteRequestBtn[i].Click += new System.EventHandler(deleteRequest);
                panel6.Controls.Add(deleteRequestBtn[i]);
            }

        }

        //aldığı derslerin bilgisi gösteren panel
        private void takedCourses_Click(object sender, EventArgs e)
        {
            approvePanel.Visible = false;
            panel4.Visible = false; 
            infoCourses.Visible = true;
            selectCoursesPanel.Visible = false;
            transkriptPanel.Visible = false;    
            infoOfTakedCourses();
        }


        public NpgsqlConnection conn = new NpgsqlConnection("server=localhost; port=5432; Database=dbDersSecim ; Username =postgres; Password=123 ");

        //talep ooluştur
        private void courseRequestBtn_Click(object sender, EventArgs e)
        {
             removeControls();
             removeilgialaniFilter();
             infoCourses.Visible = false;
             selectCoursesPanel.Visible = true;
             approvePanel.Visible = false;
             panel4.Visible = false;
             transkriptPanel.Visible = false;
             createCheckBoxesforTeachers();
             createLabelforCourses();
             requestCourses();


        }
        private void requestPanel()
        {
            createCheckBoxesforTeachers();
            createLabelforCourses();
            requestCourses();
        }

        private void removeControls()
        {
            if (checkBoxes.Count != 0)
            {
                for (int i = 0; i < checkBoxes.Count; i++)
                {
                    for (int j = 0; j < checkBoxes[i].Count; j++)
                    {
                        courseSelection.Controls.Remove(checkBoxes[i][j]);
                        courseSelection.Controls.Remove(messagesBtn[i][j]);
                    }
                }
            }

            if (courses.Count != 0)
            {
                for (int i = 0; i < courses.Count; i++)
                {
                    courseSelection.Controls.Remove(courses[i]);
                }
            }


            if (coursesRequested.Count != 0)
            {
                for(int i = 0; i < coursesRequested.Count; i++)
                {
                    panel6.Controls.Remove(requestCourseNames[i]);
                    panel6.Controls.Remove(requestCoursesTeacher[i]);
                    panel6.Controls.Remove(deleteRequestBtn[i]);
                }
            }
            if(approvedCourses.Count != 0)
            {
                for(int i = 0; i < approvedCourses.Count; i++)
            {
                    approvePanel.Controls.Remove(approvedCoursesLabel[i]);
                    approvePanel.Controls.Remove(approvedTeacherLabel[i]);
                }
            }

            coursesRequested.Clear();
            approvedCourses.Clear();
            checkBoxes.Clear();
            courses.Clear();
            messagesBtn.Clear();
            teachersAllName.Clear();
            teachersAllId.Clear();
            studentsCoursesNamecanTake.Clear();
            requestCourseNames.Clear();
            requestCoursesTeacher.Clear();
            deleteRequestBtn.Clear();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int id = queries.getLastIdFromTable("talepogrenci") + 1;
            int teacherId;
            int courseId;
            bool check = false;
            for (int i = 0; i < checkBoxes.Count; i++)
            {
                for (int j = 0; j < checkBoxes[i].Count; j++)
                {
                    teacherId = teachersAllId[i][j];
                    courseId = studentsCoursesIdcanTake[i];
                    check = queries.checkRequest(ogrenciId, teacherId, courseId);
                    if (check == true && checkBoxes[i][j].Checked)
                    {
                        queries.setRequestCourse(id, ogrenciId, teacherId, courseId);
                        id++;
                    }
                }
            }
            removeControls();
            requestPanel();
        }

        private void messageBtn_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            ButtonIndices buttonIndices = (ButtonIndices)clickedButton.Tag;
            int i = buttonIndices.I;
            int j= buttonIndices.J; 
            messagePanel.Visible = true;
            string studentNameSurname;
            studentNameSurname = queries.getNameSurname(ogrenciId);
            messageFromLabel.Text="Gönderen: "+ studentNameSurname;

            List<string> teachNameSurname= new List<string>();
            messageToLabel.Text="Alıcı: "+teachersAllName[i][j];
            sendMsjBtn.Tag = new ButtonIndices(i, j);

        }

        private void sendMsjBtn_Click(object sender, EventArgs e)
        {
            Button clickedButton=(Button)sender;
            ButtonIndices buttonIndices = (ButtonIndices)clickedButton.Tag;
            int i = buttonIndices.I;
            int j= buttonIndices.J;
            string message=messageTextBox.Text;
            int id = queries.getLastIdFromTable("hocayamesaj")+1;
            if (message.Length == 0)
            {
                MessageBox.Show("Lütfen Mesajı Giriniz");
            }
            else
            {
                queries.setMessage(id, ogrenciId, teachersAllId[i][j],message,"hocayamesaj");
                MessageBox.Show("mesajınız gönderildi!");
                messagePanel.Visible = false;
            }
        }

        private void ExitMsjBtn_Click(object sender, EventArgs e)
        {
            messagePanel.Visible = false;
        }

        private void deleteRequest(object sender,EventArgs e)
        {
            Button button = (Button)sender;
            int index = deleteRequestBtn.IndexOf(button);
            int id = queries.getLastIdFromTable("hocayamesaj") + 1;
            queries.deleteRequest(ogrenciId, coursesRequested[index][0], coursesRequested[index][1]);
            removeControls();
            requestPanel();
        }

        List<string> approvedCourses = new List<string>();
        List<string> approvedTeacher=new List<string>();
        List<Label> approvedCoursesLabel = new List<Label>();
        List<Label> approvedTeacherLabel = new List<Label>();


        //onaylanan dersleri göster

        private void approveBtn_Click(object sender, EventArgs e)
        {
            //removeControls();
            approvePanel.Visible = true;
            infoCourses.Visible = false;
            selectCoursesPanel.Visible = false;
            panel4.Visible = false;
            transkriptPanel.Visible = false;
            
            approvedCourses = queries.approvedCourses(ogrenciId);
            approvedTeacher=queries.approvedTeacher(ogrenciId);
            int y = 80;
            for (int i = 0;i<approvedCourses.Count;i++) {
                approvedCoursesLabel.Add(new Label(){ Text = approvedCourses[i],Location = new System.Drawing.Point(50, y) });
                approvedTeacherLabel.Add(new Label() { Text = approvedTeacher[i], Location = new System.Drawing.Point(170, y) });
                y += 40;
            }
            for(int i = 0; i < approvedCoursesLabel.Count; i++)
            {
                approvePanel.Controls.Add(approvedCoursesLabel[i]);
                approvePanel.Controls.Add(approvedTeacherLabel[i]);

            }
        }

        //HOCALARDAN GELEN TALEPLERİ GÖSTER
        List<List<int>> teachersRequestId= new List<List<int>>();//0=dersid, 1=hocaid
        List<Label> requestInfo= new List<Label>(); 
        List<Button> approveBtns= new List<Button>();
        private void button2_Click(object sender, EventArgs e)
        {
            infoCourses.Visible = false;
            selectCoursesPanel.Visible = false;
            approvePanel.Visible = false;
            panel4.Visible = true;
            transkriptPanel.Visible = false;
            button2Event();

        }

        private void button2Event()
        {
            teachersRequestId = queries.getTeachersRequestCourse(ogrenciId);
            int y = 70;
            for (int i = 0; i < teachersRequestId.Count; i++)
            {
                requestInfo.Add(new Label() { Text = queries.getNameofCoursesId(teachersRequestId[i][0]) + "        " + queries.getTeacherNameForTeachId(teachersRequestId[i][1]), Location = new System.Drawing.Point(20, y), Size = new System.Drawing.Size(700, 40) });
                approveBtns.Add(new Button() { Text = "Onayla", Location = new System.Drawing.Point(730, y), BackColor = Color.CornflowerBlue });
                approveBtns[i].Click += new System.EventHandler(studentApproveCourseBtn_click);
                panel4.Controls.Add(requestInfo[i]);
                panel4.Controls.Add(approveBtns[i]);
                y += 40;
            }
        }

        private void studentApproveCourseBtn_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;//
            int index= approveBtns.IndexOf(button);
            int id = queries.getLastIdFromTable("ogrenciders") + 1;
            queries.acceptStudentsRequest(id, teachersRequestId[index][0], ogrenciId, "onaylandı", teachersRequestId[index][1]);
            queries.deleteRequestTeacher(ogrenciId, teachersRequestId[index][0], teachersRequestId[index][1]);
            removeRequestTeacher();
            button2Event();
        }

        private void removeRequestTeacher()
        {

            for(int i = 0; i < teachersRequestId.Count; i++)
            {
                panel4.Controls.Remove(requestInfo[i]);
                panel4.Controls.Remove(approveBtns[i]);
            }
            teachersRequestId.Clear();
            requestInfo.Clear();
            approveBtns.Clear();
        }

        private void traskriptButton_Click(object sender, EventArgs e)
        {
            transkriptPanel.Visible = true;
            selectCoursesPanel.Visible = false;
            infoCourses.Visible = false;
            messagePanel.Visible = false;
            approvePanel.Visible = false;
            panel4.Visible = false;
        }

        string path;
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "pdf files|*.pdf";
            if(openFileDialog.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                path = openFileDialog.FileName;
                pdfDocumentViewer1.LoadFromFile(path);
            }
        }
        
       
        private void button4_Click(object sender, EventArgs e)
        {
            List<string> allLinesList = new List<string>();
            var ocr = new AutoOcr();
            var result = ocr.ReadPdf(path);
            string [] allLines= result.Text.Split('\n');

            
            foreach(string lines in allLines)
            {
                var random = new Random();

                //ders TABLOSUNA EKLEME
                string coursesCode =lines.Split(' ')[0];//bunun last ındexini al
                int idDers = queries.getLastIdFromTable("ders")+1;
                int idOgrenciDers = queries.getLastIdFromTable("ogrenciders") + 1;
                string ad;
                int hocaId = random.Next(1, 4);
                string not;

                int startIndex =lines.LastIndexOf(coursesCode);
                int finishIndex=lines.IndexOf("Z Tr");


                if (coursesCode.Length== 6 && int.TryParse(coursesCode.Substring(coursesCode.Length - 3),out int res))
                {
                    if (finishIndex > 0)
                    {
                        ad = lines.Substring(6, finishIndex - 7);
                        not = lines.Substring(finishIndex + 13, 2);
                        richTextBox1.Text += not+"\n";
                        //queries.setTranskriptInfoCourses(idDers, ad, coursesCode);
                        try
                        {
                            queries.setTranskriptInfoOgrenciDers(idOgrenciDers, queries.getCourseIdForCoursesName(ad), ogrenciId, "aldı", not, hocaId);
                        }
                        catch (Exception)
                        {

                        }
                          
                    }
                }
            }

        }

        List<int> teachersInterest = new List<int>();
        List<int> teachersCourses = new List<int>();
        List<int> studentTakedCourses = new List<int>();
        List<Label> teachersLabel = new List<Label>();
        List<Label> teachersCourseLabel = new List<Label>();
        private void ilgialanlariComboB_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeControls();
            removeilgialaniFilter();

            int index = ilgialanlariComboB.SelectedIndex+1;
            teachersInterest = queries.getTeacherIdforInterest(index);// bu ilgi alanındaki hocalar
            for (int i = 0; i < teachersInterest.Count; i++)
            {
                teachersCourses = queries.getTeachersCourse(teachersInterest[i]);// bu hocaların dersleri
            }
            studentTakedCourses = queries.getStudentsTakedCoursesId(ogrenciId);// bu öğrencinnin aldığı dersler   

            int y = 60;
            for(int i = 0; i < teachersInterest.Count; i++)
            {
                teachersLabel.Add(new Label() { Text = queries.getTeacherNameForTeachId(teachersInterest[i]), Location = new System.Drawing.Point(20, y) });
                teachersCourses = queries.getTeachersCourse(teachersInterest[i]);// bu hocaların dersleri
                List<int> demo= teachersCourses.Except(studentTakedCourses).ToList();
                courseSelection.Controls.Add(teachersLabel[i]);
                int x = 140;
                for(int  j = 0; j < demo.Count; j++)
                {
                    teachersCourseLabel.Add(new Label() { Text = queries.getNameofCoursesId(demo[j]), Location = new System.Drawing.Point(x, y) });
                    x += 140;
                    courseSelection.Controls.Add(teachersCourseLabel[i]);
                }

                y += 40;
            }

        }

        private void removeilgialaniFilter()
        {

            for(int i = 0; i < teachersLabel.Count; i++)
            {
                courseSelection.Controls.Remove(teachersLabel[i]);
            }
            for (int i = 0; i < teachersCourseLabel.Count; i++)
            {
                courseSelection.Controls.Remove(teachersCourseLabel[i]);
            }
            teachersInterest.Clear();
            teachersCourses.Clear();
            studentTakedCourses.Clear();
            teachersLabel.Clear();
            teachersCourseLabel.Clear();

        }
    }
}
