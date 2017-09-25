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
            var blur = gray.GaussianBlur(new Size(11,11),0);

            var test = blur.AdaptiveThreshold(255, AdaptiveThresholdTypes.MeanC, ThresholdTypes.BinaryInv, 15,2 );

            //test.Dilate(test);
            
            Point[][] contours;
            HierarchyIndex[] hierarchyIndices;
            test.FindContours(out contours, out hierarchyIndices,RetrievalModes.CComp,ContourApproximationModes.ApproxSimple );

            var cannny = test.Canny(20, 40);

            var rectImg = new Mat(src.Size(),src.Type());
            
            for (int i = 0; i < contours.Length; i++)
            {
                var rect = Cv2.BoundingRect(contours[i]);
                Cv2.Rectangle(rectImg,rect, new Scalar(100,100,100));
            }
            
            using (new Window("src image", test))    
            using (new Window("canny image", cannny))    
            using (new Window("rect image", rectImg))    
            Cv2.WaitKey(10);
        }
    }
}