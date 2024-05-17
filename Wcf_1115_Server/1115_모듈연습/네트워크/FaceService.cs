using OpenCvSharp.ImgHash;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_1115_조별실습_얼굴_추출_프로그램_
{
    public class FaceService:IFace
    {
        private Control control = Control.Instance;
        //콜백 레퍼런스 

        private  IFaceCallback callback = null;

        #region 1. 이미지 추출 
        public void Image_Send(string strFileName, byte[] bytePicture)
        {
            string msg = string.Empty;      
            try
            {
                //CallBack 사용자에게 보내 줄 채널을 설정.
                callback = OperationContext.Current.GetCallbackChannel<IFaceCallback>();

                //해당 사진 얼굴 추출
                Image_Data ImageData= control.Image_extraction_Face(strFileName, bytePicture);

                if (ImageData != null)
                    msg = "분석 성공";

                //CallBack: 얼굴 추출 및 추론 데이터 반환
                callback.Image_Send_Result_Single(ImageData, msg);

                //CallBack: 얼굴 추출 및 추론 리스트 출력
                List<Image_Data> list = control.DB_PrintAll_Image();
                Image_Data[] list_Array = list.ToArray();

                //콜백 함수
                callback.Image_Send_Result(list_Array);
                Console.WriteLine("callback함수 호출"); 
            }

            catch (Exception e)
            {
                // 업로드 실패
                Console.WriteLine(e.Message);
                callback.Image_Send_Result_Single(null, e.Message);
            }
        }
        #endregion

        #region 이미지 삭제 
        public void Image_Delete(string strFileName) 
        {
            //CallBack 사용자에게 보내 줄 채널을 설정.
            callback = OperationContext.Current.GetCallbackChannel<IFaceCallback>();
            Console.WriteLine("Image_Delete");

            if (control.DB_Delete_ImageData(strFileName) == true)
            {
                if (control.DB_Delete_Imgae_Each_Person(strFileName) == true)
                {
                    Console.WriteLine("Image_Delete 성공");

                    //CallBack: 얼굴 추출 및 추론 리스트 출력
                    List<Image_Data> list = control.DB_PrintAll_Image();
                    Image_Data[] list_Array = list.ToArray();

                    //콜백 함수
                    callback.Image_Delete_Result(list_Array);
                    Console.WriteLine("콜백 함수 호출");
                }
            }
        }   
        #endregion

        // 초기 Image_Data 리스트값 가져오기 
        public Image_Data[] Init_Send_Image_Data()
        {
            //CallBack: 얼굴 추출 및 추론 리스트 출력
            List<Image_Data> list = control.DB_PrintAll_Image();
            Image_Data[] list_Array = list.ToArray();
            return list_Array;          
        }
    }
}
