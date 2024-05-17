using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_1115_조별실습_얼굴_추출_프로그램_
{
    [DataContract]
    public class Image_Each_Person
    {
        [DataMember]
        public byte[] Each_Single_Image { get; set; } //각 추출된 한 사람의 이미지 

        [DataMember]
        public string Image_Name { get; set; }     

        [DataMember]
        public string Expectancy_age { get; set; }
        [DataMember]
        public string Gender { get; set; }

        public Image_Each_Person(string image_name, byte[] each_Single_Image, string expectancy_age, string gender)
        {
            Image_Name = image_name;
            Each_Single_Image = each_Single_Image;
            Expectancy_age = expectancy_age;
            Gender = gender;
        }
    }
}
