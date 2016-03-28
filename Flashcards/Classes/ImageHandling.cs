using System;
using System.Windows.Media.Imaging;
using System.IO;

namespace Flashcards
{
    public class ImageHandling
    {

        /// <summary>
        /// Setzt die größe eines Bildes neu, auf 250x250 Pixle
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        public static BitmapImage ResizePicture(BitmapImage picture)
        {
            double heightRatio = 200 / picture.Height;  
            double widthRatio = 200/ picture.Width;
            double ratio = Math.Min(heightRatio, widthRatio);
            picture.DecodePixelHeight = (int)(heightRatio * ratio);
            picture.DecodePixelWidth = (int)(widthRatio * ratio);
           
            return picture;
        }

        /// <summary>
        /// Konvertiert ein BitmapImage in ein ByteArray, zugelassene Formate für das
        /// Bild sind dabei bmp, gif, jpg, png, tiff
        /// </summary>
        /// <param name="picture">Umzuwandelndes Bild</param>
        /// <returns></returns>
        public static byte[] BitmapImageToByteArray(BitmapImage picture)       //Bearbeiten: wenn beim Bearbeiten auf ok, statt Abbrechen gedrückt wird, darf das Bild nicht null gesetzt werden
        {
            byte[] imageArray = null;

            if (picture != null)
            {
                string format = picture.UriSource.ToString().ToUpper();
                BitmapEncoder encoder;

                if (format.EndsWith("BMP")) { encoder = new BmpBitmapEncoder(); }
                else if (format.EndsWith("GIF")) { encoder = new GifBitmapEncoder(); }
                else if (format.EndsWith("JPG")) { encoder = new JpegBitmapEncoder(); }
                else if (format.EndsWith("PNG")) { encoder = new PngBitmapEncoder(); }
                else if (format.EndsWith("TIFF")) { encoder = new TiffBitmapEncoder(); }
                else
                {
                    FormatException ex = new FormatException();
                    throw (ex);
                }

                try
                {
                    encoder.Frames.Add(BitmapFrame.Create(picture));
                    using (MemoryStream stream = new MemoryStream())
                    {
                        encoder.Save(stream);
                        imageArray = stream.ToArray();
                    }
                }
                catch
                {
                    imageArray = new byte[0];
                }
            }            
            return imageArray;
        }

        /// <summary>
        /// Konvertiert ein Byte-Array zu einem BitmapImage-Object
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static BitmapImage ByteArrayToBitmapImage(byte[] data)
        {
            BitmapImage picture = new BitmapImage();
            try
            {
                using (var ms = new System.IO.MemoryStream(data))
                {
                    picture.BeginInit();
                    picture.CacheOption = BitmapCacheOption.OnLoad;
                    picture.StreamSource = ms;
                    picture.EndInit();
                }
            }
            catch { }
            return picture;
        }

        /// <summary>
        /// Erzeugt anhand einer absoluten Pfadangabe ein BitmapImage
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static BitmapImage ImageFromPath(string path)
        {
            BitmapImage pic = new BitmapImage();
            pic.BeginInit();
            pic.UriSource = new Uri(path, UriKind.Absolute);
            pic.EndInit();
            pic = ResizePicture(pic);
            return pic;
        }

        

        /// <summary>
        /// Zum ablegen von Bildern, die Bildern werden verkleinert und im Zielordner abgelegt
        /// </summary>
        /// <param name="path"></param>
        /// <param name="targetDirectory"> muss ein absoluter Pfad sein</param>
        public static void ImageToStock(string path, string targetDirectory)
        {
            double targetsize = 200;
            System.Drawing.Image img = new System.Drawing.Bitmap(path, true);
            double faktor = targetsize / (double)img.Height;
            int width = Convert.ToInt32(faktor * (double)img.Width);
            System.Drawing.Image resizedImage = new System.Drawing.Bitmap(img, width, Convert.ToInt32(targetsize));

            if (File.Exists(targetDirectory))
            {
                File.Delete(targetDirectory);   
            }
            resizedImage.Save(targetDirectory); 
        }        
    }
}
