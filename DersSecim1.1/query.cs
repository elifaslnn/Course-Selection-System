using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DersSecim1._1
{
    public class query
    {
        public NpgsqlConnection conn = new NpgsqlConnection("server=localhost; port=5432; Database=dbDersSecim ; Username =postgres; Password=123 ");

        public List<int> getCoursesIdTheSutendCanTake(int studentId)
        {

        string query = "select id from ders where id IN (select id from ders except select dersid from ogrenciders where ogrenciid=" + studentId + ")";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<int> courses = new List<int>();


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                courses.Add(int.Parse(ds.Tables[0].Rows[i][0].ToString()));
            }
            return courses;
        }

        public string getNameofCoursesId(int coursesId)
        {
            string query = "select ad from ders where id=" + coursesId + "";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            string courseName = ds.Tables[0].Rows[0][0].ToString();
            return courseName;
        }

        public List<string> getTeachersNameForClassId(int classId)
        {
            string query = "select ad from hoca where id IN (select hocaid from hocaders where dersid=" + classId + ")";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);


            List<string> cellsString = new List<string>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                cellsString.Add(ds.Tables[0].Rows[i][0].ToString());
            }
            return cellsString;
        }

        public string getTeacherNameForTeachId(int teachId)
        {
            string query = "select ad from hoca where id= '" + teachId + "'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);


            string teacherName = ds.Tables[0].Rows[0][0].ToString();
            return teacherName;
        }

        public List<int> getTeacherIdforInterest(string interest)//ilgi alanına göre hocalar
        {
            List<int> teachersId = new List<int>();
            string query = "select hocaid from ilgialanihoca where ilgialaniid IN (select id from ilgialani where ad='" + interest + "')";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                teachersId.Add(int.Parse(ds.Tables[0].Rows[i][0].ToString()));
            }
            return teachersId;
        }

        public List<string> getStudentsTakedCoursesName(int ogrenciId)
        {
            string sorgu = "select ad from ders where id IN (select dersid from ogrenciders where ogrenciid=" + ogrenciId + " and durum= 'aldı')";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<string> coursesName= new List<string>();

            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                coursesName.Add(ds.Tables[0].Rows[i][0].ToString());
            }

            return coursesName;

        }
        //select note from ogrenciders where ogrenciid = " + ogrenciId + " and durum = 'aldı' order by dersid";         //ogrenci idsi, ders idsine göre not
        public int getNoteofCourseForOgrenciIdandCourseId(int ogrenciId, int courseId)
        {
            string sorgu = "select note from ogrenciders where ogrenciid = " + ogrenciId + " and durum = 'aldı' and dersid= "+courseId+"";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            int note = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            return note;

        }

        public int getCourseIdForCoursesName(string coursesName)
        {
            string query = "select id from ders where ad='" + coursesName + "'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            int id = int.Parse(ds.Tables[0].Rows[0][0].ToString());

            return id;  
        }
    }
}
