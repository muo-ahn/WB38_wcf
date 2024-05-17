using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_1115_조별실습_얼굴_추출_프로그램_
{
    internal class Data
    {
        public Mat Image { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }

        public override string ToString()
        {
            return  Gender + "\t" + Age + "\t" + Image.ToString();
        }
    }
}
