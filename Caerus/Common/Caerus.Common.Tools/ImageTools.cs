using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Encoder = System.Text.Encoder;

namespace Caerus.Common.Tools
{
    public class ImageTools
    {
        public static byte[] CropImage(byte[] content, double x, double y, double width, double height)
        {
            using (MemoryStream stream = new MemoryStream(content))
            {
                return CropImage(stream, x, y, width, height);
            }
        }

        public static byte[] CropImage(Stream content, double x, double y, double width, double height)
        {
            //Parsing stream to bitmap
            using (Bitmap sourceBitmap = new Bitmap(content))
            {
                //Get new dimensions
                double sourceWidth = sourceBitmap.Size.Width;
                double sourceHeight = sourceBitmap.Size.Height;
                Rectangle cropRect = new Rectangle(Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(width), Convert.ToInt32(height));

                //Creating new bitmap with valid dimensions
                using (Bitmap newBitMap = new Bitmap(cropRect.Width, cropRect.Height))
                {
                    using (Graphics g = Graphics.FromImage(newBitMap))
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        g.CompositingQuality = CompositingQuality.HighQuality;

                        g.DrawImage(sourceBitmap, new Rectangle(0, 0, newBitMap.Width, newBitMap.Height), cropRect, GraphicsUnit.Pixel);

                        return GetBitmapBytes(newBitMap);
                    }
                }
            }
        }

        public static byte[] GetBitmapBytes(Bitmap source)
        {
            //Settings to increase quality of the image
            ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders()[4];
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

            //Temporary stream to save the bitmap
            using (MemoryStream tmpStream = new MemoryStream())
            {
                source.Save(tmpStream, codec, parameters);

                //Get image bytes from temporary stream
                byte[] result = new byte[tmpStream.Length];
                tmpStream.Seek(0, SeekOrigin.Begin);
                tmpStream.Read(result, 0, (int)tmpStream.Length);

                return result;
            }
        }


    }

    public class ImageCompress
    {
        private static volatile ImageCompress imageCompress;
        private Bitmap bitmap;
        private Image img;


        public ImageCompress(byte[] fileContents)
        {
            imageCompress = new ImageCompress();
            using (MemoryStream stream = new MemoryStream(fileContents))
            {
                this.bitmap = new Bitmap(stream);
            }

        }

        public ImageCompress()
        {
        }

        public int Height { get; set; }

        public int MaxHeight { get; set; }

        public int Width { get; set; }

        public int MaxWidth { get; set; }

        public int QualityPercentage { get; set; }

        public Bitmap GetImage
        {
            get { return bitmap; }
            set { bitmap = value; }
        }


        public byte[] Save(string sourceFileNameAndPath)
        {
            if (ISValidFileType(sourceFileNameAndPath))
            {
                var val = QualityPercentage;
                if (val == 0)
                    val = 60;
                return Save(val);
            }

            return null;
        }

        private void CalculateNewDimensions()
        {
            if (MaxWidth > 0)
                if (Width > MaxWidth)
                {
                    var ratio = Decimal.Divide(MaxWidth, Width);
                    Width = (int)(Width * ratio);
                    Height = (int)(Height * ratio);
                }

            if (MaxHeight > 0)
                if (Height > MaxHeight)
                {
                    var ratio = Decimal.Divide(MaxHeight, Height);
                    Width = (int)(Width * ratio);
                    Height = (int)(Height * ratio);
                }
        }

        private Image CompressImage()
        {
            if (GetImage != null)
            {
                Width = bitmap.Width;
                Height = bitmap.Height;
                CalculateNewDimensions();
                bitmap.SetResolution(80, 80);
                return bitmap.GetThumbnailImage(Width, Height, null, IntPtr.Zero);
            }
            else
            {
                throw new Exception("Please provide bitmap");
            }
        }

        /// <summary>
        /// This function is used to check the file Type
        /// </summary>
        /// <param name="fileName">String data type:contain the file name</param>
        /// <returns>true or false on the file extention</returns>
        private bool ISValidFileType(string fileName)
        {
            var fileExt = Path.GetExtension(fileName);
            if (fileExt == null)
                return false;
            switch (fileExt.ToLower())
            {
                case CommonImagesType.JPEG:
                case CommonImagesType.BMP:
                case CommonImagesType.JPG:
                case CommonImagesType.PNG:
                    return true;
                    break;
            }
            return false;
        }

        /// <summary>
        /// This function is used to get the imageCode info
        /// on the basis of mimeType
        /// </summary>
        /// <param name="mimeType">string data type</param>
        /// <returns>ImageCodecInfo data type</returns>
        private ImageCodecInfo GetImageCodeInfo(string mimeType)
        {
            ImageCodecInfo[] codes = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < codes.Length; i++)
            {
                if (codes[i].MimeType == mimeType)
                {
                    return codes[i];
                }
            }
            return null;
        }
        /// <summary>
        /// this function is used to save the image into a
        /// given path
        /// </summary>
        /// <param name="path">string data type</param>
        /// <param name="quality">int data type</param>
        private byte[] Save(int quality)
        {
            img = CompressImage();
            ////Setting the quality of the picture
            EncoderParameter qualityParam =
                new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            ////Seting the format to save
            ImageCodecInfo imageCodec = GetImageCodeInfo("image/jpeg");
            ////Used to contain the poarameters of the quality
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = qualityParam;
            ////Used to save the image to a  given path
            var ms = new MemoryStream();
            img.Save(ms, imageCodec, parameters);
            return ms.ToArray();
        }
    }
    public class CommonImagesType
    {
        public const string JPEG = ".jpeg";
        public const string PNG = ".png";
        public const string JPG = ".jpg";
        public const string BMP = ".bmp";
    }


}


