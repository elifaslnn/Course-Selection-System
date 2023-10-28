using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DersSecim1._1
{
    public class query
    {
        public NpgsqlConnection conn = new NpgsqlConnection("server=localhost; port=5432; Database=dbDersSecim ; Username =postgres; Password=123 ");

        public List<int> getCoursesIdTheSutendCanTake(int studentId)
        {

        string query = "select id from ders except select dersid from ogrenciders where ogrenciid=" + studentId +"";
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
        public List<int> getCoursesIdTheSutendCanTakeIfNumberIsOne(int studentId)
        {

            string query = "(select id from ders except select dersid from ogrenciders where ogrenciid=" + studentId + ") except select dersid from talepogrenci";
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

        public List<string> getTeachersNameForCourseId(int courseId)
        {
            string query = "select ad from hoca where id IN (select hocaid from hocaders where dersid=" + courseId + ")";
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


        public List<int> getTeachersIdForCourseId(int courseId)
        {
            string query = "select hocaid from hocaders where dersid=" + courseId + "";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);


            List<int> cellsString = new List<int>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                cellsString.Add(int.Parse(ds.Tables[0].Rows[i][0].ToString()));
            }
            return cellsString;
        }

        /// BURSDASINNNNDSFASDFA
        public List<int> getTeachersIdCanRequest(int courseId)
        {
            List<int> teachersId= new List<int>();
            string query = "select hocaid from hocaders where dersid=" + courseId +" except select hocaid from talepogrenci where dersid="+courseId+"";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            

            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                teachersId.Add(int.Parse(ds.Tables[0].Rows[i][0].ToString()));
            }

            return teachersId;
        }

        public List<string> getTeachersNameCanRequest(int courseId)
        {
            List<string> teachersName = new List<string>();
            string query = "select ad from hoca where id IN(select hocaid from hocaders where dersid=" + courseId + " except select hocaid from talepogrenci where dersid=" + courseId + ")";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                teachersName.Add(ds.Tables[0].Rows[i][0].ToString());
            }

            return teachersName;
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
        public List<int> getStudentsTakedCoursesId(int ogrenciId)
        {
            string sorgu = "select dersid from ogrenciders where ogrenciid=" + ogrenciId + " and durum= 'aldı'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<int> coursesId = new List<int>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                coursesId.Add(int.Parse(ds.Tables[0].Rows[i][0].ToString()));
            }

            return coursesId;
        }

        public string getCoursesCode(int courseId)
        {
            string code;
            string sorgu = "select code from ders where id="+courseId+"";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            code = ds.Tables[0].Rows[0][0].ToString();
            return code;
        }
        public int getCoursesAkts(int coursesAkts)
        {
            int akts;
            string sorgu = "select akts from ders where id=" + coursesAkts + "";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            akts = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            return akts;
        }


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

        public int getNumberOfCanTakeCourseFromDifferentTeachers()
        {
            int number;
            string query = "select number from yonetici";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            number = int.Parse(ds.Tables[0].Rows[0][0].ToString());

            return number;
        }

        public void setRequestCourse(int id,int studentId, int teacherId, int courseId)
        {
            conn.Close();
            conn.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into talepogrenci (id, ogrenciid,dersid,hocaid) values (@p1, @p2,@p3,@p4)",conn);
            komut.Parameters.AddWithValue("@p1",id);
            komut.Parameters.AddWithValue("@p2", studentId);
            komut.Parameters.AddWithValue("@p3", courseId);
            komut.Parameters.AddWithValue("@p4", teacherId);
            komut.ExecuteNonQuery();
            conn.Close();
        }

        public int getLastIdFromTable(string tableName)
        {
            int number;
            string query = "select id from "+tableName+"";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            int indexCount = ds.Tables[0].Rows.Count;
            if (indexCount > 0)
                number = int.Parse(ds.Tables[0].Rows[indexCount - 1][0].ToString());
            else number = 1;
            return number;
        }

        public bool checkRequest(int studentid, int teacherId, int courseId)//örencinin daha önce talep oluşturup oluşturmadığını kontrol et
        {
            bool check=true;
            string query = "select * from talepogrenci";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                 if (int.Parse(ds.Tables[0].Rows[i][1].ToString())==studentid && int.Parse(ds.Tables[0].Rows[i][2].ToString())==courseId && int.Parse(ds.Tables[0].Rows[i][3].ToString()) == teacherId)
                 {
                    check = false;
                 }
            }

            return check;
        }

        public List<List<int>> getCurrentCoursesIdRequest(int studentid){
            string sorgu = "select dersid, hocaid from talepogrenci where ogrenciid="+studentid+"";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<List<int>> courses = new List<List<int>>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                List<int> coursesInfo= new List<int>();
                coursesInfo.Add(int.Parse(ds.Tables[0].Rows[i][0].ToString()));
                coursesInfo.Add(int.Parse(ds.Tables[0].Rows[i][1].ToString()));
                courses.Add(coursesInfo);
            }

            return courses;
        }

        public List<string> getNameSurname(int studentid) {
            string sorgu = "select ad, soyad from ogrenci where id="+studentid+"";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<string> names= new List<string>();
            names.Add(ds.Tables[0].Rows[0][0].ToString());
            names.Add(ds.Tables[0].Rows[0][1].ToString());

            return names;
        }

        public void setMessage(int id, int studentId, int teacherId, string messsage,string tableName)
        {
            conn.Close();
            conn.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into "+tableName+" (id, ogrenciid,hocaid,mesaj) values (@p1, @p2,@p3,@p4)", conn);
            komut.Parameters.AddWithValue("@p1", id);
            komut.Parameters.AddWithValue("@p2", studentId);
            komut.Parameters.AddWithValue("@p3", teacherId);
            komut.Parameters.AddWithValue("@p4", messsage);
            komut.ExecuteNonQuery();
            conn.Close();
        }




    }
}
