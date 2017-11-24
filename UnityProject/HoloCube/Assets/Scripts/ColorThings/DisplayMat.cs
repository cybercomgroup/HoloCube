using System;
using System.Collections.Generic;
using OpenCVForUnity;
using OpenCVForUnityExample;
using UnityEngine;
using ColorMine.ColorSpaces;
using Rect = OpenCVForUnity.Rect;
using System.Collections;

public class DisplayMat : MonoBehaviour
{

    public WebCamTextureToMat WebCamTextureToMat;
    public ColorMap ColorMap;

    public int MinCubeX = 186;
    public int MaxCubeX = 500;

    public int MinCubeY = 63;
    public int MaxCubeY = 380;

    public List<Double[]> Colors;


    private Texture2D _texture;

    Point pt;
    Texture2D imgTexture;
    Renderer renderer;
    WebCamTexture webcamTexture;

    // Use this for initialization
    void Start()
    {
        webcamTexture = new WebCamTexture();
        renderer = GetComponent<Renderer>();
        renderer.enabled = true;

        renderer.material.mainTexture = webcamTexture;
        webcamTexture.Play();
        imgTexture = new Texture2D(webcamTexture.width, webcamTexture.height);
        imgTexture.SetPixels(webcamTexture.GetPixels());
        imgTexture.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        using (var mat = WebCamTextureToMat.GetMat())
        {
            if (mat == null) return;

            if (_texture == null) _texture = new Texture2D(mat.width(), mat.height());

            using (var dst = mat.clone())
            {
                var scalarColor = new Scalar(0, 255, 0);

                var cubeWidth = MaxCubeX - MinCubeX;
                var cubeHeight = MaxCubeY - MinCubeY;

                var piceWidth = cubeWidth / 3;
                var piceHeight = cubeHeight / 3;
                
                for (int i = 0; i < 3; i++)
                {
                    var p1 = new Point(MinCubeX + (piceWidth * i), MaxCubeY);
                    var p2 = new Point(MinCubeX + (piceWidth * i), MinCubeY);
                    Imgproc.line(dst, p1, p2, scalarColor, 5);
                }
                for (int i = 0; i < 3; i++)
                {
                    var p1 = new Point(MinCubeX, MinCubeY + (piceHeight * i));
                    var p2 = new Point(MaxCubeX, MinCubeY + (piceHeight * i));
                    Imgproc.line(dst, p1, p2, scalarColor, 5);
                }
                
                Colors = new List<double[]>();
                for (int y = 0; y < 3; y++)
                for (int x = 0; x < 3; x++)
                {
                    var row = MinCubeY + (piceHeight * y + (piceHeight / 2));
                    var col = MinCubeX + (piceWidth * x + (piceWidth / 2));
                    var color = dst.get(row, col);
                    var rgb = GetValue(color);
                    Colors.Add(new double[] {rgb.R , rgb.G , rgb.B });
                }


                ColorMap.Colors = Colors;
                ColorMap.Redraw();

                Utils.matToTexture2D(dst, _texture);

                GetComponent<Renderer>().material.mainTexture = _texture;
                GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Transparent");
            }
        }
        */



        //Get new picture from camera
        imgTexture = new Texture2D(webcamTexture.width, webcamTexture.height);
        imgTexture.SetPixels(webcamTexture.GetPixels());
        imgTexture.Apply();

        Mat imgMat = new Mat(imgTexture.height, imgTexture.width, CvType.CV_8UC3);
        Utils.texture2DToMat(imgTexture, imgMat);

        Mat grayMat = new Mat();

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
        try
        {
            for (int idx = 0; idx >= 0; idx = (int)hierarchy.get(0, idx)[0])
            {


                
                    MatOfPoint contour = contours[idx];
                    Rect rect = Imgproc.boundingRect(contour);
                    double contourArea = Imgproc.contourArea(contour);
                    matOfPoint2f.fromList(contour.toList());
                    Imgproc.approxPolyDP(matOfPoint2f, approxCurve, Imgproc.arcLength(matOfPoint2f, true) * 0.02, true);
                    long total = approxCurve.total();


                    if (total >= 4 && total <= 6)
                    {
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
                        if (isRect)
                        {
                        /*
                        double maxVal = 0;
                        int maxValIdx = 0;
                        for (int contourIdx = 0; contourIdx < contours.Count; contourIdx++)
                        {
                            double biggestContour = Imgproc.contourArea(contours[contourIdx]);
                            if (maxVal < biggestContour)
                            {
                                maxVal = biggestContour;
                                maxValIdx = contourIdx;
                            }
                        }

                        Imgproc.drawContours(imgMat, contours, maxValIdx, new Scalar(0, 255, 0), 5);
                        */
                        Imgproc.drawContours(imgMat, contours, idx, new Scalar(255, 0, 0), 10);
                        Resources.UnloadUnusedAssets(); //Fixes the memory leak
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





    /*
    private IRgb GetValue(double[] col)
    {
        var rgb = new Rgb(col[0], col[1], col[2]);
        return rgb;
    }
    */
