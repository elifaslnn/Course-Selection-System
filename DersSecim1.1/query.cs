using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace DersSecim1._1
{
    public class query
    {
        public NpgsqlConnection conn = new NpgsqlConnection("server=localhost; port=5432; Database=dbDersSecim ; Username =postgres; Password=123 ");

        public List<int> getCoursesIdTheSutendCanTake(int studentId)
        {

            string query = "select id from ders except select dersid from ogrenciders where ogrenciid=" + studentId + "";
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
            string query = "select id from ders except select dersid from ogrenciders where ogrenciid=" + studentId + " except select dersid from talepogrenci where ogrenciid= " + studentId + "";
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

            string courseName= "sss";
            if (ds.Tables[0].Rows.Count != 0)
            {
                courseName=ds.Tables[0].Rows[0][0].ToString();
            }
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

        public List<int> getTeachersIdCanRequest(int courseId, int studentId)
        {
            List<int> teachersId = new List<int>();
            string query = "select hocaid from hocaders where dersid=" + courseId + " except select hocaid from talepogrenci where dersid=" + courseId + " and ogrenciid=" + studentId + "";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                teachersId.Add(int.Parse(ds.Tables[0].Rows[i][0].ToString()));
            }

            return teachersId;
        }

        public List<string> getTeachersNameCanRequest(int courseId, int studentId)
        {
            List<string> teachersName = new List<string>();
            string query = "select ad from hoca where id IN(select hocaid from hocaders where dersid=" + courseId + " except select hocaid from talepogrenci where dersid=" + courseId + " and ogrenciid=" + studentId + ")";
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

        public List<int> getTeacherIdforInterest(int interest)//ilgi alanına göre hocalar
        {
            List<int> teachersId = new List<int>();
            string query = "select hocaid from ilgialanihoca where ilgialaniid ="+interest+"";
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
            List<string> coursesName = new List<string>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
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
            string sorgu = "select code from ders where id=" + courseId + "";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            code = ds.Tables[0].Rows[0][0].ToString();
            return code;
        }

        public string getNoteofCourseForOgrenciIdandCourseId(int ogrenciId, int courseId)
        {
            string sorgu = "select note from ogrenciders where ogrenciid = " + ogrenciId + " and durum = 'aldı' and dersid= " + courseId + "";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            string note = ds.Tables[0].Rows[0][0].ToString();
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

        public void setRequestCourse(int id, int studentId, int teacherId, int courseId)
        {
            conn.Close();
            conn.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into talepogrenci (id, ogrenciid,dersid,hocaid) values (@p1, @p2,@p3,@p4)", conn);
            komut.Parameters.AddWithValue("@p1", id);
            komut.Parameters.AddWithValue("@p2", studentId);
            komut.Parameters.AddWithValue("@p3", courseId);
            komut.Parameters.AddWithValue("@p4", teacherId);
            komut.ExecuteNonQuery();
            conn.Close();
        }



        public int getLastIdFromTable(string tableName)
        {
            string query = "select id from " + tableName + "";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);


            List<int> ids = new List<int>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ids.Add(int.Parse(ds.Tables[0].Rows[i][0].ToString()));
            }
            int max = 0;
            if (ds.Tables[0].Rows.Count != 0)
            {


                max = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (max < int.Parse(ds.Tables[0].Rows[i][0].ToString()))
                    {
                        max = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                    }
                }
            }
            return max;

        }

        public bool checkRequest(int studentid, int teacherId, int courseId)//örencinin daha önce talep oluşturup oluşturmadığını kontrol et
        {
            bool check = true;
            string query = "select * from talepogrenci";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (int.Parse(ds.Tables[0].Rows[i][1].ToString()) == studentid && int.Parse(ds.Tables[0].Rows[i][2].ToString()) == courseId && int.Parse(ds.Tables[0].Rows[i][3].ToString()) == teacherId)
                {
                    check = false;
                }
            }

            return check;
        }

        public List<List<int>> getCurrentCoursesIdRequest(int studentid)
        {
            string sorgu = "select dersid, hocaid from talepogrenci where ogrenciid=" + studentid + "";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<List<int>> courses = new List<List<int>>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                List<int> coursesInfo = new List<int>();
                coursesInfo.Add(int.Parse(ds.Tables[0].Rows[i][0].ToString()));
                coursesInfo.Add(int.Parse(ds.Tables[0].Rows[i][1].ToString()));
                courses.Add(coursesInfo);
            }

            return courses;
        }

        public string getNameSurname(int studentid)
        {
            string sorgu = "select ad, soyad from ogrenci where id=" + studentid + "";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            //List<string> names= new List<string>();
            string nameSurname = ds.Tables[0].Rows[0][0].ToString() + " " + ds.Tables[0].Rows[0][1].ToString();
            //names.Add(nameSurname);
            return nameSurname;
        }

        public void setMessage(int id, int studentId, int teacherId, string messsage, string tableName)
        {
            conn.Close();
            conn.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into " + tableName + " (id, ogrenciid,hocaid,mesaj) values (@p1, @p2,@p3,@p4)", conn);
            komut.Parameters.AddWithValue("@p1", id);
            komut.Parameters.AddWithValue("@p2", studentId);
            komut.Parameters.AddWithValue("@p3", teacherId);
            komut.Parameters.AddWithValue("@p4", messsage);
            komut.ExecuteNonQuery();
            conn.Close();
        }

        public void deleteRequest(int studentid, int courseId, int teachId)
        {
            conn.Close();
            conn.Open();
            NpgsqlCommand komut = new NpgsqlCommand("Delete from talepogrenci where ogrenciid= @p2 and dersid=@p3 and hocaid= @p4", conn);
            komut.Parameters.AddWithValue("@p2", studentid);
            komut.Parameters.AddWithValue("@p3", courseId);
            komut.Parameters.AddWithValue("@p4", teachId);
            komut.ExecuteNonQuery();
            conn.Close();
        }
        public void deleteRequestTeacher(int studentid, int courseId, int teachId)
        {
            conn.Close();
            conn.Open();
            NpgsqlCommand komut = new NpgsqlCommand("Delete from hocatalep where ogrenciid= @p2 and dersid=@p3 and hocaid= @p4", conn);
            komut.Parameters.AddWithValue("@p2", studentid);
            komut.Parameters.AddWithValue("@p3", courseId);
            komut.Parameters.AddWithValue("@p4", teachId);
            komut.ExecuteNonQuery();
            conn.Close();
        }

        public List<List<int>> showRequestToTeach(int teacherId)
        {
            string sorgu = "select ogrenciid, dersid from talepogrenci where hocaid=" + teacherId + "";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<List<int>> requestInfo = new List<List<int>>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                List<int> info = new List<int>();
                info.Add(int.Parse(ds.Tables[0].Rows[i][0].ToString()));
                info.Add(int.Parse(ds.Tables[0].Rows[i][1].ToString()));
                requestInfo.Add(info);
            }

            return requestInfo;
        }



        public void acceptStudentsRequest(int id, int courseId, int studentId, string situation, int teachId)
        {
            conn.Close();
            conn.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into ogrenciders (id, dersid,ogrenciid,durum,hocaid) values (@p1, @p2,@p3,@p4,@p5)", conn);
            komut.Parameters.AddWithValue("@p1", id);
            komut.Parameters.AddWithValue("@p2", courseId);
            komut.Parameters.AddWithValue("@p3", studentId);
            komut.Parameters.AddWithValue("@p4", situation);
            komut.Parameters.AddWithValue("@p5", teachId);
            komut.ExecuteNonQuery();
            conn.Close();
        }

        public List<string> approvedCourses(int ogrenciId)
        {
            string sorgu = "select ad from ders where id IN (select dersid from ogrenciders where durum='onaylandi' and ogrenciid=" + ogrenciId + ") ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<string> names = new List<string>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                names.Add(ds.Tables[0].Rows[i][0].ToString());
            }
            return names;
        }

        public List<string> approvedTeacher(int ogrenciId)
        {

            string sorgu = "select ad from hoca where id IN (select hocaid from ogrenciders where durum='onaylandi' and ogrenciid=" + ogrenciId + ") ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<string> names = new List<string>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                names.Add(ds.Tables[0].Rows[i][0].ToString());
            }
            return names;
        }

        public string getInterestsName(int id)
        {
            string sorgu = "select ad from ilgialani where id=" + id + "";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            string name = ds.Tables[0].Rows[0][0].ToString();

            return name;
        }

        public List<int> getAllInterestId(int teacherId)
        {
            string sorgu = "select id from ilgialani except (select ilgialaniid from ilgialanihoca where hocaid=" + teacherId + ")";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<int> interest = new List<int>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                interest.Add(int.Parse(ds.Tables[0].Rows[i][0].ToString()));
            }
            return interest;
        }

        public List<int> getInterest()
        {
            string sorgu = "select id from ilgialani";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<int> interest = new List<int>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                interest.Add(int.Parse(ds.Tables[0].Rows[i][0].ToString()));
            }
            return interest;
        }
        public void setIntrestToTeacher(int id, int interestId, int teachId)
        {
            conn.Close();
            conn.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into ilgialanihoca (id,ilgialaniid,hocaid) values (@p1, @p2,@p3)", conn);
            komut.Parameters.AddWithValue("@p1", id);
            komut.Parameters.AddWithValue("@p2", interestId);
            komut.Parameters.AddWithValue("@p3", teachId);
            komut.ExecuteNonQuery();
            conn.Close();
        }

        public List<int> getTeachsInterest(int teachId)
        {
            string sorgu = "select ilgialaniid from ilgialanihoca where hocaid=" + teachId + "";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<int> interest = new List<int>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                interest.Add(int.Parse(ds.Tables[0].Rows[i][0].ToString()));
            }
            return interest;
        }

        public void deleteInterest(int teacherId, int ilgialaniID)
        {
            conn.Close();
            conn.Open();
            NpgsqlCommand komut = new NpgsqlCommand("Delete from ilgialanihoca where ilgialaniid=" + ilgialaniID + " and hocaid=" + teacherId + "", conn);
            komut.ExecuteNonQuery();
            conn.Close();
        }

        public List<int> getTeachersCourse(int teachId)
        {
            string sorgu = "select dersid from hocaders where hocaid=" + teachId + "";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<int> courses = new List<int>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                courses.Add(int.Parse(ds.Tables[0].Rows[i][0].ToString()));
            }
            return courses;
        }

        public List<int> getNotCorfirmedStudent(int dersId)
        {

            string sorgu = "select id from ogrenci except(select ogrenciid from ogrenciders where dersid=" + dersId + ")";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<int> courses = new List<int>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                courses.Add(int.Parse(ds.Tables[0].Rows[i][0].ToString()));
            }
            return courses;
        }

        public List<string> allCoursesName()
        {

            string sorgu = "select ad from ders";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<string> courses = new List<string>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                courses.Add(ds.Tables[0].Rows[i][0].ToString());
            }
            return courses;
        }

        //3 dersi de alan öğrenciler
        public List<int> allStudent(int dersId1, int dersId2, int dersId3)
        {
            List<int> students = new List<int>();
            string sorgu = "SELECT ogrenciid FROM ogrenciders WHERE dersid IN (" + dersId1 + "," + dersId2 + "," + dersId3 + ") GROUP BY ogrenciid HAVING COUNT(DISTINCT dersid) = 3";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                students.Add(int.Parse(ds.Tables[0].Rows[i][0].ToString()));
            }

            return students;
        }

        //3 dersi de alan öğrencilerin notlarını çek
        public double getAverageNot(int studentid,int dersId1, int dersId2, int dersId3,List<int> akts)
        {
            string sorgu = "SELECT note from ogrenciders where ogrenciId="+studentid+" and dersid="+dersId1+ "; SELECT note from ogrenciders where ogrenciId="+studentid+" and dersid="+dersId2+";SELECT note from ogrenciders where ogrenciId="+studentid+" and dersid="+dersId2+"";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            int count = 0;
            for(int i = 0; i < akts.Count; i++)
            {
                count += akts[i];
            }

            List<double> nots= new List<double>();
            for(int i = 0; i < 3; i++)
            {
                if (ds.Tables[i].Rows[0][0].ToString() == "AA")
                {
                    nots.Add(4);
                }else if(ds.Tables[i].Rows[0][0].ToString() == "BA")
                {
                    nots.Add(3.5);
                }
                else if (ds.Tables[i].Rows[0][0].ToString() == "BB")
                {
                    nots.Add(3);
                }
                else if (ds.Tables[i].Rows[0][0].ToString() == "CB")
                {
                    nots.Add(2.50);
                }
                else if (ds.Tables[i].Rows[0][0].ToString() == "CC")
                {
                    nots.Add(2);
                }
                else if (ds.Tables[i].Rows[0][0].ToString() == "DD")
                {
                    nots.Add(1.5);
                }
                else {
                    nots.Add(2);
                }
            }
            double not1= nots[0] *  akts[0];
            double not2 = nots[1] * akts[1];
            double not3 = nots[2] *  akts[2];

            double average =(not1 + not2 + not3)/count; 

            return average;
        }

        public void teachersRequest(int id,int hocaid,int studentId,int dersid)
        {
            conn.Close();
            conn.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into hocatalep (id, dersid,ogrenciid,hocaid) values (@p1, @p2,@p3,@p4)", conn);
            komut.Parameters.AddWithValue("@p1", id);
            komut.Parameters.AddWithValue("@p2", dersid);
            komut.Parameters.AddWithValue("@p3", studentId);
            komut.Parameters.AddWithValue("@p4", hocaid);

            komut.ExecuteNonQuery();
            conn.Close();
        }

        public bool checkTeachsRequest(int studentId, int courseId,int teachId)
        {
            bool check = true;

            string sorgu = "select ogrenciid from hocatalep where dersid="+courseId+" and hocaid="+teachId+" and ogrenciid="+studentId+"";// hocatalep sayfasında bu hoca bu ders için bu öğrenciye talerp 
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count != 0)
            {
                check = false;
            }
            return check;   
        }

        public List<List<int>> getTeachersRequestCourse(int studentId)
        {
            List<List<int>> request = new List<List<int>>();   
            
            string sorgu = "select dersid, hocaid from hocatalep where ogrenciid="+studentId+"";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                List<int> list = new List<int>();
                list.Add(int.Parse(ds.Tables[0].Rows[i][0].ToString()));
                list.Add(int.Parse(ds.Tables[0].Rows[i][1].ToString()));
                request.Add(list);
            }

            return request;
        }

        public bool checkStudentTakeCourses(int courseId,int studentId)
        {
            bool check = true;

            string sorgu = "select ogrenciid from ogrenciders where dersid=" + courseId + " and ogrenciid="+studentId+"";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count != 0)//0 değilse dersi almış demektir yazdırma
            {
                check = false;
            }
            return check;
        }


        public void setTranskriptInfoCourses(int id,string ad, string code)
        {
            conn.Close();
            conn.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into ders (id,ad, code) values (@p1, @p2,@p3)", conn);
            komut.Parameters.AddWithValue("@p1", id);
            komut.Parameters.AddWithValue("@p2", ad);
            komut.Parameters.AddWithValue("@p3", code);

            komut.ExecuteNonQuery();
            conn.Close();

        }

        public void setTranskriptInfoOgrenciDers(int id, int dersid, int ogrenciid, string durum, string not, int hocaid)
        {
            conn.Close();
            conn.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into ogrenciders (id, dersid,ogrenciid,durum,note,hocaid) values (@p1, @p2,@p3,@p4,@p5,@p6)", conn);
            komut.Parameters.AddWithValue("@p1", id);
            komut.Parameters.AddWithValue("@p2", dersid);
            komut.Parameters.AddWithValue("@p3", ogrenciid);
            komut.Parameters.AddWithValue("@p4", durum);
            komut.Parameters.AddWithValue("@p5", not);
            komut.Parameters.AddWithValue("@p6", hocaid);

            komut.ExecuteNonQuery();
            conn.Close();
        }

        public List<int> getStudentsMessages(int ogrenciid)
        {
            string sorgu = "select id from ogrenciyemesaj where ogrenciid="+ogrenciid+"";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<int> messages = new List<int>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                messages.Add(int.Parse(ds.Tables[0].Rows[i][0].ToString()));
            }
            return messages;
        }

        public int senderTeachId(int id)
        {
            string sorgu = "select hocaid from ogrenciyemesaj where id=" + id + "";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            int teacherId =int.Parse( ds.Tables[0].Rows[0][0].ToString());
            return teacherId;
        }
        public string getmessages(int id)
        {
            string sorgu = "select mesaj from ogrenciyemesaj where id=" + id + "";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            string message = ds.Tables[0].Rows[0][0].ToString();
            return message;
        }
        ////////////////////
        public List<int> getTeachsMessages(int hocaid)
        {
            string sorgu = "select id from hocayamesaj where hocaid=" + hocaid + "";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<int> messages = new List<int>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                messages.Add(int.Parse(ds.Tables[0].Rows[i][0].ToString()));
            }
            return messages;
        }

        public int senderStudentId(int id)
        {
            string sorgu = "select ogrenciid from hocayamesaj where id=" + id + "";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            int teacherId = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            return teacherId;
        }
        public string getmessagesStudent(int id)
        {
            string sorgu = "select mesaj from hocayamesaj where id=" + id + "";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            string message = ds.Tables[0].Rows[0][0].ToString();
            return message;
        }




    }
    // <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3 <3
}
