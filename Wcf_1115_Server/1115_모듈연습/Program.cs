using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp_1115_조별실습_얼굴_추출_프로그램_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //addr_file
            //Address
            Control control = Control.Instance;
            Uri uri = new Uri(ConfigurationManager.AppSettings["addr"]);

            //Contract-> Setting
            //Binding -> App.Config
            ServiceHost host = new ServiceHost(typeof(ConsoleApp_1115_조별실습_얼굴_추출_프로그램_.FaceService), uri);
            //오픈
            host.Open();
            Console.WriteLine("얼굴 분산 처리 서비스를 시작합니다. {0}", uri.ToString());
            Console.WriteLine("멈추시려면 엔터를 눌러주세요..");

            Console.ReadLine();
            //서비스
            control.FormClosed();
            host.Abort();
            host.Close();
        }
    }
}
