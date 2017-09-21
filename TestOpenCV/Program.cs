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

            Cv2.FindContours(
                dilated, 
                out Point[][] contours, 
                out HierarchyIndex[] hierarchy,
                mode: RetrievalModes.CComp,
                method: ContourApproximationModes.ApproxSimple
            );

            
            // Filter the best candidate squares for the grid
            List<Point[]> grid = FindGrid(FilterSquares(dilated));

            //if(grid.Count == 9)
            if(true)
            {
                using (MatOfByte3 mat3 = new MatOfByte3(src))
                {
                    var indexer = mat3.GetIndexer();

                    foreach (Point[] ps in grid)
                    {
                        Rect R = Cv2.BoundingRect(ps);
                        int r = 0, g = 0, b = 0;

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
                    }
                }
            }
            
            // Final output

            Cv2.Canny(dilated, dst, 20, 60);
            using (new Window("src image", src))
            using (new Window("dst image", dilated))
                Cv2.WaitKey(10);
        }

        private static bool IsSquareElem(double x, double y)
        {
            double diff = 0.2;
            return x / y > 1 - diff && x / y < 1 + diff;
        }

        private static List<Point[]> FilterSquares (Mat view)
        {
            List<Point[]> pss = new List<Point[]>();
            Cv2.FindContours(
                view,
                out Point[][] contours,
                out HierarchyIndex[] hierarchy,
                mode: RetrievalModes.CComp,
                method: ContourApproximationModes.ApproxSimple
            );

            // Iterates through all contours
            for (int i = 0; i >= 0;)
            {
                Point[] ps = Cv2.ApproxPolyDP(contours[i], 3, true);
                Rect R = Cv2.BoundingRect(ps);

                if (ps.Length == 4)
                {
                    
                    var d01 = Point.Distance(ps[0], ps[1]);
                    var d02 = Point.Distance(ps[0], ps[2]);
                    var d13 = Point.Distance(ps[1], ps[3]);
                    var d23 = Point.Distance(ps[2], ps[3]);

                    if (IsSquareElem(d01, d23) && IsSquareElem(d02, d13))
                    {
                        pss.Add(ps);
                    }
                }

                i = hierarchy[i].Next;
            }
            return pss;
        }

        private static List<Point[]> FindGrid(List<Point[]> ps)
        {

            return ps;
        }
        
    }
}