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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Drawing2D.GraphicsPath obj = new System.Drawing.Drawing2D.GraphicsPath();
            obj.AddEllipse(0, 0, pictureBox1.Width, pictureBox1.Height);
            Region rg = new Region(obj);
            pictureBox1.Region = rg;


            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("C:/Users/Elif Aslan/Desktop/courseSelection/SpaceMono-Regular.ttf");
            foreach (Control c in this.Controls)
            {
                c.Font = new Font(pfc.Families[0], 7, FontStyle.Regular);
            }
            //////////////////////////////////
            infoPanel.Visible = false;
        }
        List<List<int>> studentRequestInfo = new List<List<int>>();
        List<string> coursesName = new List<string>();
        List<List<string>> studentName = new List<List<string>>();


        private void getQueries()
        {
            studentRequestInfo = queries.showRequestToTeach(teachId);

            for (int i = 0; i < studentRequestInfo.Count; i++)
            {
                List<string> name= new List<string>();
                coursesName.Add(queries.getNameofCoursesId(studentRequestInfo[i][1]));
                studentName.Add(queries.getNameSurname(studentRequestInfo[i][0]));
            }
        }

        List<Button> studentBtn = new List<Button>();
        List<Label> courseLabel = new List<Label>();
        List<Button> acceptBtns= new List<Button>();
        private void createInfoLabel()
        {
            getQueries();

            int y = 80;
            for (int i = 0; i < studentRequestInfo.Count; i++)
            {
                courseLabel.Add(new Label (){ Text = coursesName[i], Location = new System.Drawing.Point(170, y+10), Size = new System.Drawing.Size(90, 30) });
                studentBtn.Add(new Button() { Text = studentName[i][0], Location = new System.Drawing.Point(50, y), Size = new System.Drawing.Size(90, 30) });
                acceptBtns.Add(new Button() { Text = "Onay", Location = new System.Drawing.Point(290, y), Size = new System.Drawing.Size(90, 30), BackColor=Color.Green });
                y += 60;

            }
            for (int i = 0; i < studentRequestInfo.Count; i++)
            {
                selectCoursesPanel.Controls.Add(studentBtn[i]);
                selectCoursesPanel.Controls.Add(courseLabel[i]);
                acceptBtns[i].Click += new System.EventHandler(acceptBtn_click);
                selectCoursesPanel.Controls.Add(acceptBtns[i]);
            }

        }
        //hocaya yapılan talepleri gösteren panel
        private void requestedCoursesBtn_Click(object sender, EventArgs e)
        {
            createInfoLabel();
        }

        private void removeControls()
        {

            for(int i=0;i<studentRequestInfo.Count;i++)
            {
                selectCoursesPanel.Controls.Remove(studentBtn[i]);
                selectCoursesPanel.Controls.Remove(courseLabel[i]);
                selectCoursesPanel.Controls.Remove(acceptBtns[i]);
                //studentName.Remove(studentName[i]);
            }
            studentName.Clear();
            studentRequestInfo.Clear();
            acceptBtns.Clear();
            courseLabel.Clear();
            studentBtn.Clear();
        }
        private void acceptBtn_click(object sender, EventArgs e)
        {
            Button button= (Button) sender;
            int i = acceptBtns.IndexOf(button);
            int id = queries.getLastIdFromTable("ogrenciders")+1;
            queries.acceptStudentsRequest(id, studentRequestInfo[i][1], studentRequestInfo[i][0], "onaylandi");
            queries.deleteRequest(studentRequestInfo[i][0], studentRequestInfo[i][1],teachId);
            removeControls();
            createInfoLabel();
        }

    }
}
