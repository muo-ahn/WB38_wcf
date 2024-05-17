using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Dnn;
using OpenCvSharp.Extensions;
using Point = OpenCvSharp.Point;
using Size = OpenCvSharp.Size;


namespace ConsoleApp_1115_조별실습_얼굴_추출_프로그램_
{
    internal class Control
    {
        private Mat _image;
        private Net _faceNet;
        private Net _ageNet;
        private Net _genderNet;
        private const int LineThickness = 2;
        private const int _Padding = 10;
        private readonly List<string> _genderList = new List<string> { "Male", "Female" };
        private readonly List<string> _ageList = new List<string> { "(0-2)", "(4-6)", "(8-12)", "(15-20)", "(25-32)", "(38-43)", "(48-53)", "(60-100)" };

        private List<Image_Each_Person> lists = new List<Image_Each_Person>();

        public static Control Instance { get; private set; } = null;

        //DB 레퍼런스 
        private WbDB db = new WbDB();

        #region 싱글톤(생성자에서 파일로드) - 수정X
        static Control()
        {
            Instance = new Control();
        }

        private Control()
        {
            const string faceProto = @"deploy.prototxt";
            const string faceModel = @"res10_300x300_ssd_iter_140000_fp16.caffemodel";
            const string ageProto = @"age_deploy.prototxt";
            const string ageModel = @"age_net.caffemodel";
            const string genderProto = @"gender_deploy.prototxt";
            const string genderModel = @"gender_net.caffemodel";
            _ageNet = CvDnn.ReadNetFromCaffe(ageProto, ageModel);
            _genderNet = CvDnn.ReadNetFromCaffe(genderProto, genderModel);
            _faceNet = CvDnn.ReadNetFromCaffe(faceProto, faceModel);

            if (Load() == true)
            {
                Console.WriteLine("Message : DB connect");
            }
        }
        #endregion

        #region DB 서버 연결 
        public bool Load()
        {
            return db.Connect();
        }

        public bool FormClosed()
        {
            return db.Close();
        }
        #endregion

        #region 기능

        //Db에 이미지 저장(Image_Data) 
        public bool DB_Insert_Analyzed_Image(Image_Data image_Data)
        {
            return db.DB_Insert_Analyzed_Image(image_Data);
        }

        //Db에 이미지 저장(Image_Each_Person) 
        public bool DB_Insert_ImageList(List<Image_Each_Person> images) 
        {
            return db.DB_Insert_ImageList(images);      
        }
        
        //DB정보 삭제 
        public bool DB_Delete_ImageData(string strFileName)
        {
            return db.DB_Delete_ImageData(strFileName);
        }

        public bool DB_Delete_Imgae_Each_Person(string strFileName)
        {
            return db.DB_Delete_Imgae_Each_Person(strFileName);
        }

        //DB 데이터 가져오기 
        public List<Image_Data> DB_PrintAll_Image()
        {
            return db.DB_PrintAll_Image();
        }

        #endregion

        #region 이미지 얼굴 추출
        public Image_Data Image_extraction_Face(string strFileName, byte[] image)
        {
            //Image_Data에 데이터(얼굴 분석된 이미지) 객체 생성 
            Image_Data Image = new Image_Data();

            //파일 이름 저장 
            Image.Image_Name = strFileName;

            //byte[] -> Mat 
            //Mat _image = Mat.FromImageData(image, ImreadModes.Color);
            Mat _image = Cv2.ImDecode(image, ImreadModes.Color);
            //이미지 분석 및 추론 (Mat, List<Image_Each_Person>) 
            DetectFace(strFileName, _image, _image);

            ////크기 줄이기 
            //Mat dst = new Mat();
            //Cv2.Resize(_image, dst, new Size(200, 150));

            //Mat -> byte[] 
            byte[] Face_Analyzed_byte;
            Cv2.ImEncode(".jpg", _image, out Face_Analyzed_byte);

            Image.Analyzed_image = Face_Analyzed_byte; //분석 이미지 저장 
            Image.imageLists = lists; //추론 이미지 list<Image_Each_Person> 저장

            //DB에 저장 (분석 이미지 저장) 
            if (DB_Insert_Analyzed_Image(Image) == true)
                Console.WriteLine("DB_Insert_Analyzed_Image 성공");
            else
                Console.WriteLine("DB_Insert_Analyzed_Image 실패");

            //DB에 저장 (추출 이미지 저장) 
            if (DB_Insert_ImageList(Image.imageLists) == true)
            {
                Console.WriteLine("DB_Insert_ImageList 성공");
                //Image_Data 데이터 전송 
                return Image;
            }

            else
            {
                Console.WriteLine("DB_Insert_ImageList 실패");
                return null;
            } 
        }
        #endregion

        #region 메서드

        //(OpenCV 이미지 추출)
        private void DetectFace(string strFileName, Mat newImage, Mat imageRes)
        {
            //데이터 정리 (Image_Each_Person)
            lists.Clear();

            int frameHeight = newImage.Rows;
            int frameWidth = newImage.Cols;

            var blob = CvDnn.BlobFromImage(newImage, 1.0, new Size(300, 300), new Scalar(104, 117, 123), false, false);
            _faceNet.SetInput(blob, "data");

            var detection = _faceNet.Forward("detection_out");
            var detectionMat = new Mat(detection.Size(2), detection.Size(3), MatType.CV_32F, detection.Ptr(0));

            for (int i = 0; i < detectionMat.Rows; i++)
            {
                float confidence = detectionMat.At<float>(i, 2);

                if (confidence > 0.7)
                {
                    int x1 = (int)(detectionMat.At<float>(i, 3) * frameWidth);
                    int y1 = (int)(detectionMat.At<float>(i, 4) * frameHeight);
                    int x2 = (int)(detectionMat.At<float>(i, 5) * frameWidth);
                    int y2 = (int)(detectionMat.At<float>(i, 6) * frameHeight);

                    Cv2.Rectangle(newImage, new Point(x1, y1), new Point(x2, y2), Scalar.Green, LineThickness);

                    //AI추론(성별, 나이) 
                    AnalyzeAgeAndGender(x1, y1, x2, y2, imageRes, newImage, out Mat face_result, out string gender_result, out string age_result);

                    Mat dst = new Mat();

                    Cv2.Resize(face_result, dst, new Size(500, 250));

                    //Mat -> byte[] 
                    byte[] Face_Result_byte;

                    Cv2.ImEncode(".jpg", dst, out Face_Result_byte);

                    lists.Add(new Image_Each_Person(strFileName, Face_Result_byte, age_result, gender_result));
                }
            }
        }

        //AI추론(성별, 나이) 
        private void AnalyzeAgeAndGender(int x1, int y1, int x2, int y2, Mat imageRes, Mat newImage, out Mat face_result, out string gender_result, out string age_result)
        {
            var x = x1 - _Padding;
            var y = y1 - _Padding;
            var w = (x2 - x1) + _Padding * 3;
            var h = (y2 - y1) + _Padding * 3;

            Rect roiNew = new Rect(x, y, w, h);

            //face : 얼굴 좌표 저장
            var face = imageRes[roi: roiNew];
            face_result = face;

            var meanValues = new Scalar(78.4263377603, 87.7689143744, 114.895847746);
            var blobGender = CvDnn.BlobFromImage(face, 1.0, new Size(227, 227), mean: meanValues, swapRB: false);
            _genderNet.SetInput(blobGender);
            var genderPreds = _genderNet.Forward();

            GetMaxClass(genderPreds, out int classId, out double classProbGender);
            //classProbGender = 성별 적중 확률;

            //gender 분석 결과 저장
            var gender = _genderList[classId];
            //gender_result = gender.ToString() + " 확률 : " + (classProbGender * 100).ToString();
            gender_result = gender.ToString() + " 확률 : " + Math.Round((classProbGender * 100), 2) .ToString();

            _ageNet.SetInput(blobGender);
            var agePreds = _ageNet.Forward();
            GetMaxClass(agePreds, out int classIdAge, out double classProbAge);
            //classProbAge = 나이 적중 확률

            //age 분석 결과 저장
            var age = _ageList[classIdAge];
            age_result = age.ToString() + " 확률 : " + Math.Round((classProbAge * 100),2).ToString();

            var label = $"{gender},{age}";
            Cv2.PutText(newImage, label, new Point(x1 - 10, y2 + 20), HersheyFonts.HersheyComplexSmall, 1, Scalar.Yellow, 1);
        }

        private void GetMaxClass(Mat probBlob, out int classId, out double classProb)
        {
            // reshape the blob to 1×1000 matrix
            var probMat = probBlob.Reshape(1, 1);
            Cv2.MinMaxLoc(probMat, out _, out classProb, out _, out var classNumber);
            classId = classNumber.X;
            Debug.WriteLine($"X: {classNumber.X} – Y: {classNumber.Y} ");
        }
        #endregion
    }
}
