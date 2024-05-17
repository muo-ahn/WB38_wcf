using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp_1115_조별실습_얼굴_추출_프로그램_
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IFaceCallback))]
    //1. Contract InterFace(클라이언트->서버)
    public interface IFace
    {
        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void Image_Send(string strFileName, byte[] bytePicture);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void Image_Delete(string strFileName);

        //처음 초기 리스트 값 가져오기 
        [OperationContract]
        Image_Data[] Init_Send_Image_Data();
    }
 
    // 2. 클라이언트에 콜백할 CallBackContract(서버->클라이언트)
    public interface IFaceCallback
    {
        [OperationContract(IsOneWay = true)]
        void Image_Send_Result_Single(Image_Data ImageData, string msg); //Image_Data 데이터 반환 

        [OperationContract(IsOneWay = true)]
        void Image_Send_Result(Image_Data[] Image_List);                        //Image_Data 리스트 반환 

        [OperationContract(IsOneWay = true)]
        void Image_Delete_Result(Image_Data[] Image_List);                      //Image_Data 리스트 반환 
    }

}
