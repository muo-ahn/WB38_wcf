using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp_1115_조별실습_얼굴_추출_프로그램_
{
    internal class WbDB
    {
        private SqlConnection scon;
        const string constr = @"Data Source=DESKTOP-0I86BTV\SQLEXPRESS;Initial Catalog=WB_38; User ID=aaaa;Password=1234;";

        public SqlConnection Scon { get { return scon; } }

        public bool Connect()
        {
            try
            {
                scon = new SqlConnection();
                scon.ConnectionString = constr;
                scon.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Close()
        {
            try
            {
                scon.Close();
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }

        //분석된 이미지 저장 
        public bool DB_Insert_Analyzed_Image(Image_Data image_Data)
        {
            string name = image_Data.Image_Name;

            byte[] bytes = image_Data.Analyzed_image;

            List<Image_Each_Person> lists = image_Data.imageLists;

            //INSERT  INTO Image VALUES('이름',0,'1','여자'); 
            string qury = string.Format("insert into Image values(@Image_Name, @Image_Analyzed_Image);");

            //방법3 
            SqlCommand cmd = new SqlCommand(null, scon);


            //-- 파라미터 등록
            SqlParameter param_name = new SqlParameter("@Image_Name", name);
            cmd.Parameters.Add(param_name);

            SqlParameter param_analyed_image = new SqlParameter("@Image_Analyzed_Image", bytes);
            cmd.Parameters.Add(param_analyed_image);

            cmd.CommandText = qury;

            if (cmd.ExecuteNonQuery() == 1)
                return true;

            else
                return false;
        }

        //얼굴 추출 리스트 정보 저장
        public bool DB_Insert_ImageList(List<Image_Each_Person> imageLists)
        {
            try
            {
                //방법3 
                SqlCommand cmd = new SqlCommand(null, scon);

                foreach (Image_Each_Person person in imageLists)
                {
                    //INSERT  INTO Image VALUES('이름',0,'1','여자'); 
                    string qury = string.Format("INSERT  INTO Each_Person VALUES(@ImageName, @Image_image, @Expectancy_age, @Gender);");

                    SqlParameter param_name = new SqlParameter("@ImageName", person.Image_Name);
                    cmd.Parameters.Add(param_name);

                    SqlParameter param_image = new SqlParameter("@Image_image", person.Each_Single_Image);
                    cmd.Parameters.Add(param_image);

                    SqlParameter param_ExpectancyAge = new SqlParameter("@Expectancy_age", person.Expectancy_age);
                    cmd.Parameters.Add(param_ExpectancyAge);

                    SqlParameter param_Gender = new SqlParameter("@Gender", person.Gender);
                    cmd.Parameters.Add(param_Gender);

                    cmd.CommandText = qury;
                    ExecCommand(cmd);

                    cmd.Parameters.Clear();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }
        //데이터 삭제 
        public bool DB_Delete_ImageData(string strFileName)
        {
            string qury = string.Format("delete from Image where Image_Name=@Image_Name;");

            //방법3 
            SqlCommand cmd = new SqlCommand(null, scon);


            //-- 파라미터 등록
            SqlParameter param_name = new SqlParameter("@Image_Name", strFileName);
            cmd.Parameters.Add(param_name);

            cmd.CommandText = qury;

            if (cmd.ExecuteNonQuery() == 1)
                return true;

            else
                return false;
        }

        public bool DB_Delete_Imgae_Each_Person(string strFileName)
        {
            string qury = string.Format("delete from Each_Person where ImageName=@ImageName;");

            //방법3 
            SqlCommand cmd = new SqlCommand(null, scon);

            //-- 파라미터 등록
            SqlParameter param_name = new SqlParameter("@ImageName", strFileName);
            cmd.Parameters.Add(param_name);

            cmd.CommandText = qury;

            if (cmd.ExecuteNonQuery() >= 1)
                return true;
            else
                return false;
        }
        //데이터 가져오기 
        public List<Image_Data> DB_PrintAll_Image()
        {
            List<Image_Data> image_Datas = new List<Image_Data>();

            Image_Data imageData = null;
            try
            {
                string qury = "select *from Image;";

                SqlCommand cmd = new SqlCommand(qury, scon);

                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())   //0...N RowData를 순차적 접근
                {
                    string Image_name = (string)reader[0];
                    byte[] Analyed_image = (byte[])reader[1];

                    imageData = new Image_Data(Image_name, Analyed_image);
                    image_Datas.Add(imageData);
                }

                reader.Close();
                
                foreach(Image_Data data in image_Datas) 
                {
                    //Image_name로 Image_Each_Person list값 가져오기 
                    string qury_single_person = string.Format("select ImageName, Image_image, Expectancy_age, Gender from  Each_Person where Each_Person.ImageName = '{0}';", data.Image_Name);

                    SqlCommand cmd_single_person = new SqlCommand(qury_single_person, scon);

                    SqlDataReader reader_single_person = cmd_single_person.ExecuteReader();

                    List<Image_Each_Person> list = new List<Image_Each_Person>();
                    while (reader_single_person.Read())   //0...N RowData를 순차적 접근
                    {
        

                        Image_Each_Person person = new Image_Each_Person((string)reader_single_person[0], (byte[])reader_single_person[1], (string)reader_single_person[2], (string)reader_single_person[3]);
                        list.Add(person);
                    }
                    data.imageLists=list;       
                    reader_single_person.Close();
                }
                return image_Datas;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }

        }

        private bool ExecCommand(SqlCommand comm)
        {
            try
            {
                if (comm.ExecuteNonQuery() >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }


    }
}
