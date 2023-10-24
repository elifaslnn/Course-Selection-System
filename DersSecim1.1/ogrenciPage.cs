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
        List<ComboBox> comboBoxes = new List<ComboBox>();
        List<Label> courses = new List<Label>();

        public ogrenciPage()
        {
            InitializeComponent();
            panel1.Visible = false;
            panel2.Visible = false;

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


        private void button2_Click(object sender, EventArgs e)  //alabileceği tüm dersler
        {
            panel2.Visible = true;
            panel1.Visible = false;
            int y = 60;

            List<int> coursesIdTheStudentCanTake = new List<int>();//alabileceği derslerin idsi
            List<string> teachersName = new List<string>();//alabileceği derslerin hocaları
            coursesIdTheStudentCanTake = queries.getCoursesIdTheSutendCanTake(ogrenciId);//öğrencinin alabileceği derslerin idsi

            for (int i = 0; i < comboBoxes.Count; i++)
            {
                panel2.Controls.Remove(comboBoxes[i]);
                comboBoxes[i].Items.Clear();
            }

            for (int i = 0; i < coursesIdTheStudentCanTake.Count; i++)
            {
                ComboBox combo = new ComboBox();
                comboBoxes.Add(combo);

                Label course = new Label();
                // courses.Add(course);

                panel2.Controls.Add(new Button() { Text = "Talep", Location = new System.Drawing.Point(180, y), BackColor=Color.Green });

                panel2.Controls.Add(comboBoxes[i]);
                panel2.Controls.Add(course);
                comboBoxes[i].Location = new System.Drawing.Point(90, y);
                course.Text = queries.getNameofCoursesId(coursesIdTheStudentCanTake[i]);
                course.Location = new System.Drawing.Point(10, y);
                teachersName = queries.getTeachersNameForClassId(coursesIdTheStudentCanTake[i]);//a dersinin hocaları // b dersinin hocaları

                for (int j = 0; j < teachersName.Count; j++)
                {
                    comboBoxes[i].Items.Add(teachersName[j]);
                }
                y += 40;
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<int> teachersId = new List<int>();
            string interest = comboBox2.Text;

            teachersId =queries.getTeacherIdforInterest(interest);

            for (int i = 0; i < comboBoxes.Count; i++)
            {
                panel2.Controls.Remove(comboBoxes[i]);
                panel2.Controls.Remove(courses[i]);
                comboBoxes[i].Items.Clear();
            }
            int y = 60;

            for (int i = 0; i < teachersId.Count; i++)
            {
                ComboBox combo = new ComboBox();
                comboBoxes.Add(combo);
                panel2.Controls.Add(comboBoxes[i]);
                comboBoxes[i].Text = queries.getTeacherNameForTeachId(teachersId[i]);
                comboBoxes[i].Location = new System.Drawing.Point(120, y);
                y += 40;
            }
        }

    }
}
