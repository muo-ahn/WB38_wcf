using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _1115_Client
{
    /// <summary>
    /// Cam_Window.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Cam_Window : System.Windows.Window
    {
        private VideoCapture video;
        private bool isPlaying;
        BitmapSource bmpSource;
        MainWindow mainWindow = null;

        public Cam_Window(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
            Image1.Stretch = System.Windows.Media.Stretch.Fill;
            video = new VideoCapture();
            isPlaying = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.img = bmpSource;
            mainWindow.UpdateImage();
            this.Close();
        }
        private void DisplayFrames()
        {
            while (isPlaying)
            {
                using (Mat image = new Mat())
                {
                    if (!video.Read(image) || image.Empty())
                    {
                        break;
                    }

                    bmpSource = ConvertMatToBitmapSource(image);

                    // 메인 스레드에서 UI 업데이트
                    Dispatcher.Invoke(() =>
                    {
                        Image1.Source = bmpSource;
                    });
                }
            }

            // 비디오 재생이 끝나면 VideoCapture 객체 해제
            isPlaying = false;
            video.Dispose();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            if (video != null)
            {
                isPlaying = false;
                video.Dispose(); // 폼이 닫힐 때 VideoCapture 객체 해제
            }
        }

        private BitmapSource ConvertMatToBitmapSource(Mat image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Cv2.ImEncode(".bmp", image, out byte[] bytes);
                memoryStream.Write(bytes, 0, bytes.Length);
                memoryStream.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();  // 이미지가 다른 스레드에서 사용될 수 있도록 Freeze() 호출

                return bitmapImage;
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (!isPlaying)
            {
                int cameraIndex = 0; // 카메라 디바이스 번호 (0부터 시작)
                video.Open(cameraIndex);
                isPlaying = true;

                // 새 스레드에서 프레임을 읽고 표시하는 메서드 호출
                Thread videoThread = new Thread(DisplayFrames);
                videoThread.Start();
            }
            else
            {
                isPlaying = false;
                video = null; // VideoCapture 객체 해제
            }
        }
    }
}
