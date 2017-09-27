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

        /// <summary>
        /// Calculates the angle of a line.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static double Theta(LineSegmentPoint line)
        {
            double dX = line.P2.X - line.P1.X;
            double dY = line.P2.Y - line.P1.Y;
            return Math.Atan2(dY, dX) * 180 / Math.PI;
        }

        /// <summary>
        /// Calculates the average angle of a list of lines.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        private static double AvgTheta(List<LineSegmentPoint> lines)
        {
            double mean = 0;
            foreach (LineSegmentPoint l in lines)
            {
                mean += Theta(l);
            }

            return mean / lines.Count;
        }

        /// <summary>
        /// Calculates the angle difference between a line and a list of lines.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="lines"></param>
        /// <returns></returns>
        private static double DiffTheta(LineSegmentPoint line, List<LineSegmentPoint> lines)
        {
            return Math.Abs(AvgTheta(lines) - Theta(line));
        }

        /// <summary>
        /// Creates a matrix of lines, where each row represents all lines with
        /// similar angles.
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        private static List<List<LineSegmentPoint>> MakeLineCluster(Mat mat)
        {

            double dTheta = Cv2.PI / 12f;

            LineSegmentPoint[] lines = Cv2.HoughLinesP(mat, 1, Cv2.PI / 100, 150);
            List<List<LineSegmentPoint>> lineCluster = new List<List<LineSegmentPoint>>();
            
            foreach(LineSegmentPoint line in lines)
            {
                if (line.Length(line) < 35)
                    continue;

                if (lineCluster.Count < 1)
                {
                    lineCluster.Add(new List<LineSegmentPoint> { line });
                } else
                {
                    bool clusterFound = false;
                    foreach(List<LineSegmentPoint> cluster in lineCluster)
                    {
                        if(DiffTheta(line,cluster) < dTheta)
                        {
                            cluster.Add(line);
                            clusterFound = true;
                            break;
                        }
                    }
                    if(!clusterFound)
                    {
                        lineCluster.Add(new List<LineSegmentPoint> { line });
                    }
                }
                
            }
            
            return lineCluster;
        }

        /// <summary>
        /// Tries to find a grid representing´a Rubik's cube using a given matrix
        /// of lines.
        /// </summary>
        /// <param name="clusters"></param>
        /// <param name="mat"></param>
        /// <returns></returns>
        private static List<LineSegmentPoint> findGrid(List<List<LineSegmentPoint>> clusters, Mat mat)
        {

            LineSegmentPoint horizontal = new LineSegmentPoint(
                new Point(0, mat.Height / 2),
                new Point(mat.Width, mat.Height/2));


            foreach (List<LineSegmentPoint> lines in clusters)
            {
                if (lines.Count < 4)
                    continue;
                
                double distance = -1;

                List<LineSegmentPoint> candidates = new List<LineSegmentPoint>();
                candidates.Add(lines[0]);

                for(int i = 0; i < lines.Count - 1; i++)
                {
                    for (int j = i + 1; j < lines.Count - 1; j++)
                    {
                        LineSegmentPoint p1 = lines[j];
                        LineSegmentPoint p2 = lines[j + 1];

                        if (candidates.Count == 1)
                        {
                            distance = Point.Distance(p1.P1, p2.P1);
                            candidates.Add(p2);
                        }
                        else if (
                            Point.Distance(p1.P1, p2.P1) < 50 && Point.Distance(p1.P1, p2.P1) > 10 &&
                            Point.Distance(p1.P2, p2.P2) < 50 && Point.Distance(p1.P2, p2.P2) > 10)
                        {
                            candidates.Add(p2);
                        }
                        else
                        {
                            //Console.WriteLine("Start distance was: " + distance + ", but found " + distance);
                            distance = -1;
                            candidates.Clear();
                            candidates.Add(p2);
                        }

                        if (candidates.Count == 4)
                        {
                            //Console.WriteLine("Found one!!");
                            return candidates;
                        }
                    }
                }
                
            }

            return new List<LineSegmentPoint>();
        }

        private static void FindCube(Mat view)
        {
            int rectWidth = 100;
            view.Rectangle(
                new Point(view.Width / 2 - rectWidth / 2, view.Height / 2 - rectWidth / 2),
                new Point(view.Width / 2 + rectWidth / 2, view.Height / 2 + rectWidth / 2),
                new Scalar(255, 0, 255), 2);


            Rect[] squares = new Rect[9];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    squares[3 * i + j] = new Rect(
                        view.Width / 2 - rectWidth / 2 + j * rectWidth / 3,
                        view.Height / 2 - rectWidth / 2 + i * rectWidth / 3,
                        rectWidth / 3, rectWidth / 3);

                    view.Rectangle(squares[3 * i + j], new Scalar(100, 0, 255), 1);
                }
            }

            // Find out if the object is a cube. If so, determine colors.

            using (MatOfByte3 mat3 = new MatOfByte3(view))
            {
                var indexer = mat3.GetIndexer();

                string[] colors = new string[9];
                

                //foreach (Point[] ps in squares)
                for (int i = 0; i < squares.Length; i++)
                {
                    Rect R = squares[i];
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

                    Scalar blue = new Scalar(109, 45, 65);
                    Scalar green = new Scalar(58, 50, 60);
                    Scalar white = new Scalar(165, 107, 135);
                    Scalar yellow = new Scalar(48, 107, 130);
                    Scalar red = new Scalar(45, 30, 112);
                    Scalar orange = new Scalar(35, 50, 140);
 
                    if (InColorRange(c, green))
                    {
                        colors[i] = "Green";
                    }
                    else if (InColorRange(c, white))
                    {
                        colors[i] = "White";
                    }
                    else if (InColorRange(c, yellow))
                    {
                        colors[i] = "Yellow";
                    }
                    else if (InColorRange(c, blue))
                    {
                        colors[i] = "Blue";
                    }
                    else if (InColorRange(c, orange))
                    {
                        colors[i] = "Orange";
                    }
                    else if (InColorRange(c, red))
                    {
                        colors[i] = "Red";
                    }
                    else
                    {
                        colors[i] = "Undefined";
                    }

                }

                bool found = true;
                int nr = 0;

                foreach (string c in colors)
                {
                    if (c == "Undefined")
                    {
                        found = false;
                        break;
                    } else
                    {
                        nr ++;
                    }
                }

                if (found)
                {
                    Console.WriteLine("Found grid!");
                    for (int i = 0; i < colors.Length / 3; i++)
                    {
                        for (int j = 0; j < colors.Length / 3; j++)
                        {
                            Console.Write(colors[3 * i + j] + " ");
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine();
                }
            }
        }


        private static void MyEventMethod(object sender, NewFrameEventArgs eventargs)
        {
            Mat src = BitmapConverter.ToMat(eventargs.Frame);

            var gray = src.CvtColor(ColorConversionCodes.BGR2GRAY);
            var gray2 = new Mat(src.Size(), src.Type());
            Cv2.Erode(gray,gray2,new Mat());

            var dilated = new Mat(src.Size(), src.Type());
            Cv2.Dilate(gray2, dilated, new Mat(), null, 3);
            //var test = new Mat(src.Size(), src.Type());
            //Cv2.Canny(dilated, test, 20, 100);
            //gray.Erode(new Mat());
            //gray = gray.GaussianBlur(new Size(11, 11), 0);
            //gray = gray.AdaptiveThreshold(255, AdaptiveThresholdTypes.MeanC, ThresholdTypes.BinaryInv, 15, 2);
            //Mat gray2 = new Mat(src.Size(), src.Type());
            //Cv2.Dilate(gray, gray2, new Mat(), null, 3);
            //gray = gray.Canny(20, 60);

            // Make clusters candidates
            //List<List<LineSegmentPoint>> parLines = MakeLineCluster(dilated);
            
            FindCube(src);
            
            // Find possible Rubik grid
            //var grids = findGrid(parLines, gray);

            // If grid found, select the best one

            // Determine location of grid by intersection

            // Final output
            using (new Window("src image", src))
            //using (new Window("dst image", test))
                Cv2.WaitKey(10);
            
            /*gray.FindContours(
                out Point[][] contours,
                out HierarchyIndex[] hierarchy,
                mode: RetrievalModes.Tree,
                method: ContourApproximationModes.ApproxSimple
            );*/

            
        }

        private static bool InColorRange(Scalar s, Scalar c)
        {
            int diff = 30;
            return (Math.Abs(s.Val0 - c.Val0) < diff && Math.Abs(s.Val1 - c.Val1) < diff && Math.Abs(s.Val2 - c.Val2) < diff);
        }

        private static bool IsSquareElem(double x, double y)
        {
            double diff = 0.2;
            return x / y > 1 - diff && x / y < 1 + diff;
        }

        private static List<Rect> FilterSquares(Mat view)
        {
            List<Rect> pss = new List<Rect>();
            Cv2.FindContours(
                view,
                out Point[][] contours,
                out HierarchyIndex[] hierarchy,
                mode: RetrievalModes.CComp,
                method: ContourApproximationModes.ApproxSimple
            );

            Rect bigSq = new Rect();
            double maxArea = 0;
            bool foundRect = false;
            

            // Iterates through all contours
            foreach(Point[] ps in contours)
            {
                //ps = Cv2.ApproxPolyDP(ps, 3, true);
                Rect R = Cv2.BoundingRect(ps);
                double area = Cv2.ContourArea(ps);
                if (area > 100)
                {
                    double peri = Cv2.ArcLength(ps, true);
                    Point[] approx = Cv2.ApproxPolyDP(ps, 0.02 * peri, true);
                    if (area > maxArea && approx.Length == 4 && ApproxSquare(approx))
                    {
                        maxArea = area;
                        bigSq = Cv2.BoundingRect(approx);
                        foundRect = true;
                    }

                }
            }

            if (foundRect)
            {
                pss.Add(bigSq);
            }

            return pss;
        }

        /*private static Point[] BiggestSquare(){

        }*/
        
        private static bool ApproxSquare(Point[] ps)
        {
            return ps.Length != 4 || Math.Abs(Cv2.BoundingRect(ps).Width / Cv2.BoundingRect(ps).Height) < 1.1 && Math.Abs(Cv2.BoundingRect(ps).Width / Cv2.BoundingRect(ps).Height) > 0.9;
        }

        private static List<Point[]> FindGrid(List<Point[]> pss)
        {

            if(pss.Count < 8)
            {
                return pss;
            }

            List<Point> centers = new List<Point>();

            foreach(Point[] ps in pss)
            {
                centers.Add(FindCenter(ps));
            }

            // EVEN BETTER, calc 9 squares with the same shape

            // Calculate the closest set of 9 squares

            Dictionary<int, double> dist = new Dictionary<int, double>();

            
            for(int i=0; i < centers.Count; i++)
            {
                dist.Add(i, 0);
                for (int j = 0; j < centers.Count; j++)
                {
                    dist[i] += (Point.Distance(centers[i],centers[j]));
                }

                Console.Write(Math.Round(dist[i],2) + ", ");
            }

            Console.WriteLine();

            // Sort by distance and return the best.

            List<Point[]> candidates = new List<Point[]>();

            foreach(int key in dist.Keys)
            {
                if(dist[key] < 800)
                {
                    candidates.Add(pss[key]);
                }
            }

            
            return pss;
        }

        private static Point FindCenter(Point[] ps)
        {
            double totX = 0;
            double totY = 0;

            foreach (Point p in ps)
            {
                totX += p.X;
                totY += p.Y;
            }

            return new Point(totX / ps.Length, totY / ps.Length);
        }

    }
}