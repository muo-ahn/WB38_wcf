
using _1115_Client.ServiceReference1;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace _1115_Client
{
	internal class Network : IFaceCallback
	{
        public MainWindow mainWindow = null;

        //클라이언트 참조 
        private FaceClient FaceClient = null;

		#region 싱글톤
		public static Network Singleton { get; private set; }
		static Network()
		{
			Singleton = new Network();
		}
		private Network()
		{
            InstanceContext site = new InstanceContext(this);
            FaceClient = new FaceClient(site);
        }
        #endregion

        #region 인터페이스 메서드

        //초기 이미지 리스트 수신 

        public void Init_Get_Image_Data_List()
        {
            mainWindow.Image_Send_Result(FaceClient.Init_Send_Image_Data());      
        }

        //이미지 전송 
        public void Image_Send(string strFileName, byte[] bytes)
		{
            FaceClient.Image_Send(strFileName, bytes);
		}

        //이미지 삭제 
        public void Image_Delete(string strFileName)
        {
            FaceClient.Image_Delete(strFileName);   
        }
        #endregion

        #region CallBack
        //Image_Data 리스트 반환 
        public void Image_Send_Result(ServiceReference1.Image_Data[] Image_List)
        {
            mainWindow.Image_Send_Result(Image_List);
        }
        //Image_Data 리스트 반환 
        public void Image_Delete_Result(ServiceReference1.Image_Data[] Image_List)
        {
            mainWindow.Image_Delete_Result(Image_List);
        }


        //Image_Data 데이터 반환 
        public void Image_Send_Result_Single(ServiceReference1.Image_Data ImageData, string msg)
        {
            if(ImageData != null) 
                mainWindow.Image_Send_Result_Single(ImageData);  
            else
                MessageBox.Show(msg);       
        }

     
        #endregion
    }
}
