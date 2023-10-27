using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace DersSecim1._1
{
    public partial class ogrenciPage : Form
    {
        public int ogrenciId;
        query queries = new query();
        List<Label> courses = new List<Label>();
        List<List<string>> teachersAllName = new List<List<string>>();//öğrencinin alabileceği tüm derslerin hocalarının adını tutar
        List<List<int>> teachersAllId = new List<List<int>>();//öğrencinin alabileceği tüm derslerin hocalarının idsini tutar
        List<int> studentsCoursesIdcanTake = new List<int>();
        List<string> studentsCoursesNamecanTake = new List<string>();


        public ogrenciPage()
        {
            InitializeComponent();
            panel1.Visible = false;
            panel2.Visible = false;

        }

        private void ogrenciPage_Load(object sender, EventArgs e)
        {
            studentsCoursesIdcanTake = queries.getCoursesIdTheSutendCanTake(ogrenciId);//öğrencinin alması gereken derslerin idsi
            foreach (int id in studentsCoursesIdcanTake)//her bir id ye göre öğretmen idsi ve adını tut
            {
                List<string> teachersNameforCourse = new List<string>();
                List<int> teachersIdforCourse = new List<int>();
                teachersNameforCourse = queries.getTeachersNameForCourseId(id);
                teachersIdforCourse = queries.getTeachersIdForCourseId(id);
                teachersAllName.Add(teachersNameforCourse);
                teachersAllId.Add(teachersIdforCourse);
                studentsCoursesNamecanTake.Add(queries.getNameofCoursesId(id));

            }
        }


        List<List<CheckBox>> checkBoxes = new List<List<CheckBox>>();
        public void createCheckBoxesforTeachers()
        {
            int y = 50;
            for(int i = 0; i < teachersAllId.Count; i++)
            {
                int x = 120;
                List<CheckBox> cbox = new List<CheckBox>();
                for (int j = 0; j < teachersAllId[i].Count; j++)
                {
                    cbox.Add(new CheckBox() { Text = teachersAllName[i][j], Location = new System.Drawing.Point(x, y) });
                    x += 120;
                }
                checkBoxes.Add(cbox);
                y += 40;
            }
            for (int i = 0; i < teachersAllId.Count; i++)
            {
                for (int j = 0; j < teachersAllId[i].Count; j++)
                {
                    panel2.Controls.Add(checkBoxes[i][j]);
                }
            }
        }

        public void createLabelforCourses()//derslerin yazdığı label
        {
            int y = 50;
            foreach(var courseName in studentsCoursesNamecanTake)
            {
                courses.Add(new Label() { Text = courseName, Location = new System.Drawing.Point(10, y) });
                y += 40;
            }
            foreach(var course in courses)
            {
                panel2.Controls.Add(course);
            }
        }

        private void button1_Click(object sender, EventArgs e) //aldığı dersleri ve notları gösteren buton  
        {
            int y = 60;
            panel1.Visible = true;
            panel2.Visible = false;

            List<string> coursesName = new List<string>();
            coursesName = queries.getStudentsTakedCoursesName(ogrenciId);//öğrencinin aldığı dersler (labelda tutulacak)
            foreach (var course in coursesName)
            {
                panel1.Controls.Add(new Label() { Text = coursesName[coursesName.IndexOf(course)], Location = new System.Drawing.Point(20, y) });//aldığı derslerin adınu tutacak labelların oluşturulması
                int coursesId = queries.getCourseIdForCoursesName(course);
                int note = queries.getNoteofCourseForOgrenciIdandCourseId(ogrenciId, coursesId);
                panel1.Controls.Add(new Label() { Text = note.ToString(), Location = new System.Drawing.Point(160, y) });//aldığı derslerin adınu tutacak labelların oluşturulması
                y += 40;
            }
        }

        private void button2_Click(object sender, EventArgs e)   //alabileceği tüm dersler

        {
            panel2.Visible = true;
            panel1.Visible = false;

            createCheckBoxesforTeachers();
            createLabelforCourses();
        }

        private void talep_Click(object sender, EventArgs e) //talep ettiği derslerin tabloya kayıdı
        {
            int id = queries.getLastIdFromTable("talepogrenci")+1;
            int teacherId;
            int courseId;
            bool check=false;
            for(int i=0;i<checkBoxes.Count;i++)
            {
                for(int j = 0; j < checkBoxes[i].Count; j++)
                {
                    teacherId = teachersAllId[i][j];
                    courseId = studentsCoursesIdcanTake[i];
                    check = queries.checkRequest(ogrenciId, teacherId, courseId);
                    if (check==true && checkBoxes[i][j].Checked)
                    {
                        queries.setRequestCourse(id,ogrenciId,teacherId,courseId);
                        id++;
                    }

                } 
            }
            if (check == false)
            {
                MessageBox.Show("bu ders ve seçili hoca için tekrar talep oluşturamazsınız");
            }
        }

    }
}
