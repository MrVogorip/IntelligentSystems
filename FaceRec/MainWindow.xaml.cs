using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.Win32;
using Rectangle = System.Drawing.Rectangle;

namespace FaceRec
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static CascadeClassifier _classifier = new CascadeClassifier("haarcascade_frontalface_alt_tree.xml");
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OpenBnt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.DefaultExt = ".png";
                openFileDialog.Filter = "PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";
                openFileDialog.ShowDialog();
                string path = openFileDialog.FileName;
                ImageSource imageSource = new BitmapImage(new Uri(path));
                ImageViewGrid.Source = imageSource;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void FindFaceBnt_Click(object sender, RoutedEventArgs e)
        {
            Bitmap bitmap = Convert(ImageViewGrid.Source);
            Image<Bgr, byte> grayImage = new Image<Bgr, byte>(bitmap);
            Rectangle[] rectangles = _classifier.DetectMultiScale(grayImage, 1.4, 0);
            foreach (var face in rectangles)
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Yellow, 3))
                    {
                        graphics.DrawRectangle(pen, face);
                    }
                }
            }
            ImageViewGrid.Source = Convert(bitmap);
        }
        private Bitmap Convert(ImageSource imageSource)
        {
            Bitmap bitmap = null;
            using (MemoryStream ms = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)imageSource));
                encoder.Save(ms);
                using (Bitmap bmp = new Bitmap(ms))
                {
                    bitmap = new Bitmap(bmp);
                }
            }
            return bitmap;
        }
        private BitmapImage Convert(Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }
}
