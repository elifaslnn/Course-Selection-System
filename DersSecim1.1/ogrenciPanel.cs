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
        }

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
            label3.Location = Location = new System.Drawing.Point(295, 47);
            label4.Location = Location = new System.Drawing.Point(374, 47);

            ////////////////////////

            //infoOfTakedCourses();
            selectCoursesPanel.Visible = false;
            infoCourses.Visible = false;
            messagePanel.Visible = false;
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
                teachersNameforCourse = queries.getTeachersNameCanRequest(id);
                teachersIdforCourse = queries.getTeachersIdCanRequest(id);
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
                int x = 120;
                List<CheckBox> cbox = new List<CheckBox>();
                List<Button> buttons = new List<Button>();

                    for (int j = 0; j < teachersAllId[i].Count; j++)
                    {
                        cbox.Add(new CheckBox() { Text = teachersAllName[i][j], Location = new System.Drawing.Point(x, y), Size = new System.Drawing.Size(90, 30) });
                        x += 90;
                        buttons.Add(new Button() { Text = "**", Location = new System.Drawing.Point(x, y), Size = new System.Drawing.Size(30, 30), BackColor = Color.Green });
                        
                    x += 40;
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
                    courses.Add(new Label() { Text = courseName, Location = new System.Drawing.Point(10, y) });
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
                int note = queries.getNoteofCourseForOgrenciIdandCourseId(ogrenciId, coursesId);
                string code = queries.getCoursesCode(coursesId);
                int akts = queries.getCoursesAkts(coursesId);
                infoCourses.Controls.Add(new Label() { Text = coursesName[coursesName.IndexOf(course)], Location = new System.Drawing.Point(181, y) });//aldığı derslerin adınu tutacak labelların oluşturulması
                infoCourses.Controls.Add(new Label() { Text = note.ToString(), Location = new System.Drawing.Point(374, y) });//aldığı derslerin notunu tutacak labelların oluşturulması
                infoCourses.Controls.Add(new Label() { Text = code, Location = new System.Drawing.Point(48, y) });//aldığı derslerin notunu tutacak labelların oluşturulması
                infoCourses.Controls.Add(new Label() { Text = akts.ToString(), Location = new System.Drawing.Point(295, y) });//aldığı derslerin notunu tutacak labelların oluşturulması
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
            infoCourses.Visible = true;
            selectCoursesPanel.Visible = false;
            infoOfTakedCourses();
        }


        public NpgsqlConnection conn = new NpgsqlConnection("server=localhost; port=5432; Database=dbDersSecim ; Username =postgres; Password=123 ");

        private void courseRequestBtn_Click(object sender, EventArgs e)
        {
            //removeControls();
            infoCourses.Visible = false;
            selectCoursesPanel.Visible = true;
            createCheckBoxesforTeachers();
            createLabelforCourses();
            requestCourses();
        }
        private void requestPanel()
        {
            infoCourses.Visible = false;
            selectCoursesPanel.Visible = true;
            createCheckBoxesforTeachers();
            createLabelforCourses();
            requestCourses();
        }

        private void removeControls()
        {
            for(int i=0;i<checkBoxes.Count;i++) {
                for(int j = 0; j < checkBoxes[i].Count;j++) {
                    courseSelection.Controls.Remove(checkBoxes[i][j]);
                    courseSelection.Controls.Remove(messagesBtn[i][j]);
                }
            }
            for(int i=0;i< courses.Count;i++)
            {
                courseSelection.Controls.Remove(courses[i]);
            }
            for(int i = 0; i < coursesRequested.Count; i++)
            {
                panel6.Controls.Remove(requestCourseNames[i]);
                panel6.Controls.Remove(requestCoursesTeacher[i]);
                panel6.Controls.Remove(deleteRequestBtn[i]);
            }

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
            List<string> studentNameSurname = new List<string>();
            studentNameSurname = queries.getNameSurname(ogrenciId);
            messageFromLabel.Text="Gönderen: "+ studentNameSurname[0] +" " + studentNameSurname[1];

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

    }
}
