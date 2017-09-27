using System;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Collections.Generic;
//using OpenCvSharp.CPlusPlus;

namespace OpenCVPlayground
{
    internal class Program
    {

        private static int c = 0;

        public static void Main(string[] args)
        {
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            // create video source
            VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            // set NewFrame event handler
            videoSource.NewFrame += MyEventMethod;
            // start the video source
            videoSource.Start();
            
        }

        private static int x = 0;

        private static void MyEventMethod(object sender, NewFrameEventArgs eventargs)
        {
            Mat src = BitmapConverter.ToMat(eventargs.Frame);
            Mat dst = new Mat();

            var gray = src.CvtColor(ColorConversionCodes.BGR2GRAY);
            var blur = gray.GaussianBlur(new Size(3,3),0);
            var canny = blur.Canny(20, 60);
            var dilated = canny.Dilate(0);

            Cv2.FindContours(dilated, out Point[][] contours, out HierarchyIndex[] hierarchy, mode: RetrievalModes.CComp,
                method: ContourApproximationModes.ApproxSimple);

            using (MatOfByte3 mat3 = new MatOfByte3(src))
            {
                var indexer = mat3.GetIndexer();

                // Iterates through all contours
                for (int i = 0; i >= 0;)
                {
                    Point[] ps = Cv2.ApproxPolyDP(contours[i], 3, true);
                    Rect R = Cv2.BoundingRect(ps);

                    if (R.Width > 20 && R.Height > 20 && Math.Abs(R.Width - R.Height) < 2 && ps.Length == 4)
                    {
                        int r = 0, g = 0, b = 0;

                        // 
                        for (int j = R.X; j < R.Width + R.X; j++)
                        {
                            for (int k = R.Y; k < R.Height + R.Y; k++)
                            {
                                
                                Vec3b color = indexer[k, j];
                                r += color.Item0;
                                g += color.Item1;
                                b += color.Item2;
                            }
                        }

                        int pixels = R.Width * R.Height;
                        Scalar c = new Scalar(r / pixels, g / pixels, b / pixels);
                        
                        Cv2.Rectangle(src, R, c, Cv2.FILLED);

                        /*Cv2.DrawContours(
                            src,
                            contours,
                            i,
                            color: new Scalar(255, 0, 255),
                            thickness: -1,
                            lineType: LineTypes.Link8,
                            hierarchy: hierarchy,
                            maxLevel: int.MaxValue);*/
                    }

                    i = hierarchy[i].Next;

                }
            }

            // Final output

            Cv2.Canny(dilated, dst, 20, 60);
            using (new Window("src image", src))    
            Cv2.WaitKey(10);
        }
    }
}