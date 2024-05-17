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
    [KnownType(typeof(List<Image_Data>))]

    public class Image_Data
    {


        [DataMember]
        public string Image_Name { get; set; }  //이미지 이름 

        [DataMember]
        public byte[] Analyzed_image { get; set; } //분석된 전체 이미지(1장)        

        [DataMember]
        public List<Image_Each_Person> imageLists { get; set; }

        //생성자 
        public Image_Data() 
        {
            imageLists = new List<Image_Each_Person>();     
        }

        public Image_Data(string name, byte[] image)
        {
            Image_Name = name;
            Analyzed_image = image;

        }

    }

   
}
