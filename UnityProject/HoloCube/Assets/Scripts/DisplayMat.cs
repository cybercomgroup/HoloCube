using System;
using System.Collections.Generic;
using OpenCVForUnity;
using OpenCVForUnityExample;
using UnityEngine;
using ColorMine.ColorSpaces;
using Rect = OpenCVForUnity.Rect;
using System.Collections;
using ColorThings;
using Backend;

public class DisplayMat : MonoBehaviour
{ 
public static bool sideScaned = false;
//public WebCamTextureToMat WebCamTextureToMat;
public ColorMap ColorMap;

public List<Double[]> Colors;

private Texture2D _texture;

private ColorDetection _colorDetection;

Point pt;
Texture2D imgTexture;
Renderer renderer;
public static WebCamTexture webcamTexture;

// Use this for initialization
void Start()
{
    _colorDetection = new ColorDetection();
    webcamTexture = new WebCamTexture();
    renderer = GetComponent<Renderer>();
    renderer.enabled = true;
    SkipScan.GenerateList();

    renderer.material.mainTexture = webcamTexture;
    webcamTexture.Play();
    imgTexture = new Texture2D(webcamTexture.width, webcamTexture.height);
    imgTexture.SetPixels(webcamTexture.GetPixels());
    imgTexture.Apply();
}

    // Update is called once per frame
    void Update()
    {
        Resources.UnloadUnusedAssets(); //Fixes the memory leak

        //Get new picture from camera
        imgTexture = new Texture2D(webcamTexture.width, webcamTexture.height);
        imgTexture.SetPixels(webcamTexture.GetPixels());
        imgTexture.Apply();

        Mat imgMat = new Mat(imgTexture.height, imgTexture.width, CvType.CV_8UC3);
        Utils.texture2DToMat(imgTexture, imgMat);

        Mat maskMat = new Mat();
        Mat maskMatOP = new Mat();

        Mat grayMat = new Mat();

        Imgproc.dilate(imgMat, imgMat, Imgproc.getStructuringElement(Imgproc.MORPH_RECT, new Size(1, 1)));

        //Grayscale the picture
        Imgproc.cvtColor(imgMat, grayMat, Imgproc.COLOR_RGB2GRAY);

        //Blur the picture
        Imgproc.GaussianBlur(grayMat, grayMat, new Size(3, 3), 1);

        Imgproc.equalizeHist(grayMat, grayMat);

        //Find Edges
        Mat edgesOfPicture = new Mat();
        Imgproc.Canny(grayMat, edgesOfPicture, 75, 225);

        List<MatOfPoint> contours = new List<MatOfPoint>();
        Mat hierarchy = new Mat();
        Imgproc.findContours(edgesOfPicture, contours, hierarchy, Imgproc.RETR_EXTERNAL, Imgproc.CHAIN_APPROX_SIMPLE);

    
        

        MatOfPoint2f matOfPoint2f = new MatOfPoint2f();
        MatOfPoint2f approxCurve = new MatOfPoint2f();
        List<Rect> rectPre = new List<Rect>();
        List<Rect> rectAfter = new List<Rect>();


        try
        {
        List<MatOfPoint2f> kvadrater = new List<MatOfPoint2f>();
        for (int idx = 0; idx >= 0; idx = (int)hierarchy.get(0, idx)[0])
        {

            MatOfPoint contour = contours[idx];
            Rect rect = Imgproc.boundingRect(contour);
            double contourArea = Imgproc.contourArea(contour);
            matOfPoint2f.fromList(contour.toList());

            Imgproc.approxPolyDP(matOfPoint2f, approxCurve, Imgproc.arcLength(matOfPoint2f, true) * 0.02, true);
            long total = approxCurve.total();


            if (total > 0)
            {
                kvadrater.Add(approxCurve);
                ArrayList cos = new ArrayList();
                Point[] points = approxCurve.toArray();

                for (int j = 2; j < total + 1; j++)
                {
                    cos.Add(angle(points[(int)(j % total)], points[j - 2], points[j - 1]));
                }

                cos.Sort();
                Double minCos = (Double)cos[0];
                Double maxCos = (Double)cos[cos.Count - 1];
                bool isRect = total == 4 && minCos >= -0.1 && maxCos <= 0.3;
                //List<double[]> Colors = new List<double[]>();



                if (isRect)
                {
                    if (rect.width > 20) rectPre.Add(rect);
                    List<Color> Colors = new List<Color>();
                    List<double[]> colorDoubles = new List<double[]>();
                    for (int op = 0; op < 9; op++)
                    {
                        if (rectPre.Count == 9)
                        {
                            // print("Pre verify: " + rectPre.ToString());
                            //rectPre = CoordinateVerifier.Verify(rectPre); Använd inte LINQ !! ! ! ! !
                            // print("After verify: " + rectPre.ToString());
                            var punkt = imgTexture.GetPixel(rect.x + (rect.width / 2), rect.y + (rect.height / 2));
                            Imgproc.putText(imgMat, op.ToString(), new Point(rectPre[op].x + 20, rectPre[op].y + 30), Core.FONT_HERSHEY_DUPLEX, 3, new Scalar(200));
                            Rgb rgb = new Rgb(punkt.r, punkt.g, punkt.b);
                            // print("rect[" + op + "] was found at" + rect.x.ToString() + "and y: " + rect.y.ToString());
                            var hsv = rgb.To<Hsv>();
                            String farg = "Ingen farg";

                            if (hsv.H >= 45 && hsv.H <= 70) farg = "Gul";
                            if (hsv.H >= 10 && hsv.H <= 45) farg = "Orange";

                            // print(farg);
                            Colors.Clear();
                            for (int q = 0; q < rectPre.Count; q++)
                            {
                                Color[] blockOfColour = imgTexture.GetPixels(rectPre[q].x + (rectPre[q].width / 2), rectPre[q].y + (rectPre[q].height / 2), rectPre[q].width / 3, rectPre[q].height / 3, 0);

                                float r = 0, g = 0, b = 0;
                                foreach (Color pixelBlock in blockOfColour)
                                {
                                    r += pixelBlock.r;
                                    g += pixelBlock.g;
                                    b += pixelBlock.b;


                                }
                                r = r / blockOfColour.Length;
                                g = g / blockOfColour.Length;
                                b = b / blockOfColour.Length;

                                var eColor = _colorDetection.ColorEnumFromScalarColor(new double[] { r * 255, g * 255, b * 255 });
                                var color = ColorDetection.UnityColorFromEnum(eColor);
                                Colors.Add(color);
                            }

                            if (Colors.Count == 9)
                            {
                                ColorTracker.Instance.addToTemp(Colors);
                                foreach (Color c in Colors)
                                {
                                    // print(c.ToString());
                                }
                            }
                        }
                    }
                    Imgproc.drawContours(imgMat, contours, idx, new Scalar(255, 100, 155), 4);
                }
            }
        }
        }
        catch (ArgumentOutOfRangeException e)
        {
        }

        Texture2D texture = new Texture2D(imgMat.cols(), imgMat.rows(), TextureFormat.RGBA32, false);
        Utils.matToTexture2D(imgMat, texture);
        gameObject.GetComponent<Renderer>().material.mainTexture = texture;
        }

    

        private double angle(Point pt1, Point pt2, Point pt0)
{
    double dx1 = pt1.x - pt0.x;
    double dy1 = pt1.y - pt0.y;
    double dx2 = pt2.x - pt0.x;
    double dy2 = pt2.y - pt0.y;
    return (dx1 * dx2 + dy1 * dy2) / Math.Sqrt((dx1 * dx1 + dy1 * dy1) * (dx2 * dx2 + dy2 * dy2) + 1e-10);
}
}


