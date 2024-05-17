using _1115_Client.ServiceReference1;
using Microsoft.Win32;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace _1115_Client
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public partial class MainWindow : System.Windows.Window
    {
        //사진 바이트 
        byte[] Image_byte= null;
        //사진 이름 
        FileInfo fi = null;

        public BitmapSource img = new BitmapImage(); //그림파일 담을 객체 선언

        //서비스 클라이언트 객체 생성 
        private Network Net_control = Network.Singleton;

		public MainWindow()
		{
			InitializeComponent();
		}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Net_control.mainWindow=this;
            //초기 DB리스트 업로드 
            Net_control.Init_Get_Image_Data_List();
        }

        #region 버튼 핸들러
        //파일 선택 버튼
        private void Button_Click(object sender, RoutedEventArgs e)
		{
            try
            {
                // 읽어오는 스트림클래스를 선언
                Stream readStream;

                // 파일열기	대화상자를 생성
                OpenFileDialog dlg = new OpenFileDialog();

                // 확장자를 제한한다.
                dlg.Filter = "그림파일 (*.bmp;*.jpg;*.gif;*.jpeg;*.png;*.tiff)|*.bmp;*.jpg;*.gif;*.jpeg;*.png;*.tiff)";
                dlg.RestoreDirectory = true;    // 현재 디렉토리를 저장해놓는다.

                // OK 버튼을 누르면
                if (dlg.ShowDialog() == true)
                {
                    if ((readStream = dlg.OpenFile()) != null)
                    {
                        BinaryReader picReader = new BinaryReader(readStream);

                        //이미지 정보 저장 
                        Image_byte = picReader.ReadBytes(Convert.ToInt32(readStream.Length));
                        fi = new FileInfo(dlg.FileName);

                        //사진 출력 
                        BitmapImage bti_Image = new BitmapImage();
                        bti_Image.BeginInit();
                        bti_Image.StreamSource=new MemoryStream(Image_byte);
                        bti_Image.EndInit();
                        before_image.Source = bti_Image;        

                        readStream.Close();
                        MessageBox.Show("성공");
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

		//파일 전송 버튼
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
            try
            {
                Net_control.Image_Send(fi.Name, Image_byte);
            }

            catch(Exception)
            {
                MessageBox.Show("사진을 고르세요");
            }
		}

        //이미지 객체 삭제 
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ServiceReference1.Image_Data imageData = (ServiceReference1.Image_Data)Analyzed_Image_List.SelectedItem;
            if (imageData != null)
            { Net_control.Image_Delete(imageData.Image_Name); }

            else
                MessageBox.Show("객체를 선택하시오");

        }

        //캠 찍기
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Cam_Window win = new Cam_Window(this);
            win.ShowDialog();
        }


        private void list_person_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = (ListBox)sender;
            ServiceReference1.Image_Each_Person person = (ServiceReference1.Image_Each_Person)item.SelectedItem;

            //사진 출력 
            BitmapImage bti_Image = new BitmapImage();
            bti_Image.BeginInit();
            bti_Image.StreamSource = new MemoryStream(person.Each_Single_Image);
            bti_Image.EndInit();
            Image_Person_Face.Source = bti_Image;

            //이름 및 나이 출력 
            Gender.Content = person.Gender;
            Age.Content = person.Expectancy_age;
        }

        private void Analyzed_Image_List_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = (ListBox)sender;
            ServiceReference1.Image_Data imageData = (ServiceReference1.Image_Data)item.SelectedItem;

            //사진 출력 
            BitmapImage bti_Image = new BitmapImage();
            bti_Image.BeginInit();
            bti_Image.StreamSource = new MemoryStream(imageData.Analyzed_image);
            bti_Image.EndInit();
            Image_Analyzed_image.Source = bti_Image;

            //리스트 출력 
            Person_Face_List.ItemsSource = imageData.imageLists;
        }

        private void Person_Face_List_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = (ListBox)sender;
            ServiceReference1.Image_Each_Person person = (ServiceReference1.Image_Each_Person)item.SelectedItem;

            //사진 출력 
            BitmapImage bti_Image = new BitmapImage();
            bti_Image.BeginInit();
            bti_Image.StreamSource = new MemoryStream(person.Each_Single_Image);
            bti_Image.EndInit();
            Image_Person_Face2.Source = bti_Image;

            //이름 및 나이 출력 
            Gender2.Content = person.Gender;
            Age2.Content = person.Expectancy_age;
        }
        #endregion


        #region CallBack 함수 

        //Image_Send CallBack 
        public void Image_Send_Result_Single(ServiceReference1.Image_Data ImageData)
        {
            //사진 출력 
            BitmapImage bti_Image = new BitmapImage();
            bti_Image.BeginInit();
            bti_Image.StreamSource = new MemoryStream(ImageData.Analyzed_image);
            bti_Image.EndInit();
            after_image.Source = bti_Image;

            list_person.ItemsSource = ImageData.imageLists;
        }

        //Image_Send CallBack 
        public void Image_Send_Result(ServiceReference1.Image_Data[] Image_List)
        {
            Analyzed_Image_List.ItemsSource = Image_List;
        }

        //Image_Delete CallBack
        public void Image_Delete_Result(ServiceReference1.Image_Data[] Image_List)
        {
            Analyzed_Image_List.ItemsSource = Image_List;

            //컨트롤 초기화 
            Image_Analyzed_image.Source = null;
            Person_Face_List.ItemsSource = null;
            Image_Person_Face2.Source = null;
            Gender2.Content = "";
            Age2.Content = "";
        }
        #endregion

        public void UpdateImage()
        {
            before_image.Source = img;

            //사진 이름 만들기 
            string save_name = DateTime.Now.ToString("yyyy-MM-dd-hh시mm분ss초");
            string Image_Name = save_name + ".jpg";

             fi = new FileInfo(Image_Name);
            Image_byte = ConvertBitmapSourceToByteArray(img);
        }

        public byte[] ConvertBitmapSourceToByteArray(BitmapSource bitmapSource)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(stream);

                return stream.ToArray();
            }
        }
    }
}
