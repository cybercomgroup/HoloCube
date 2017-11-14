using System;
using System.Collections.Generic;
using OpenCVForUnity;
using OpenCVForUnityExample;
using UnityEngine;
using ColorMine.ColorSpaces;

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
        imgTexture = new Texture2D(webcamTexture.width, webcamTexture.height);
        imgTexture.SetPixels(webcamTexture.GetPixels());
        imgTexture.Apply();

        Mat imgMat = new Mat(imgTexture.height, imgTexture.width, CvType.CV_8UC3);
        Utils.texture2DToMat(imgTexture, imgMat);
        //Debug.Log ("imgMat dst ToString " + imgMat.ToString ());

        Mat grayMat = new Mat();
        Imgproc.cvtColor(imgMat, grayMat, Imgproc.COLOR_RGB2GRAY);

        Imgproc.Canny(grayMat, grayMat, 50, 200);

        // Apply the Hough Transform to find the lines
        Mat lines = new Mat();
        Imgproc.HoughLinesP(grayMat, lines, Imgproc.CV_HOUGH_GRADIENT, 3, grayMat.rows() / 100, 200, 100);

        /*
        Mat linesCol = new Mat();
        Imgproc.HoughLinesP(grayMat, linesCol, Imgproc.CV_HOUGH_GRADIENT, 3, grayMat.cols() / 8, 20, 100);
        */
        for (int i = 0; i < lines.cols(); i++)
        {
            //double[] rho = lines.get(i, 0);
            //double rho1 = rho[0];
            //float rho = lines.get(i, 0);
            //float theta = lines[i][1];
            //double a = cos(theta), b = sin(theta);
            //double x0 = a * rho, y0 = b * rho;

            //double[] points2 = linesCol.get(0, i);
            double[] points = lines.get(0, i);
            if(points != null)
            {
                double x1, y1, x2, y2;
                x1 = points[0];
                y1 = points[1];
                x2 = points[2];
                y2 = points[3];

                /*
                double a1, b1, a2, b2;
                a1 = points[0];
                b1 = points[1];
                a2 = points[2];
                b2 = points[3];
                */
                Point pt1 = new Point(x1, y1);
                Point pt2 = new Point(x2, y2);
                /*
                Point pta = new Point(a1, b1);
                Point ptb = new Point(a2, b2);
                Imgproc.line(imgMat, pta, ptb, new Scalar(255, 0, 0), 10);
                */
                Imgproc.line(imgMat, pt1, pt2, new Scalar(255, 0, 0), 10);

                
            }
           
        }

        Texture2D texture = new Texture2D(imgMat.cols(), imgMat.rows(), TextureFormat.RGBA32, false);
        Utils.matToTexture2D(imgMat, texture);

        gameObject.GetComponent<Renderer>().material.mainTexture = texture;
    }


    

    private IRgb GetValue(double[] col)
    {
        var rgb = new Rgb(col[0], col[1], col[2]);
//        var hsv = rgb.To<Hsv>();
//        hsv.V = 100;
//        return hsv.ToRgb();
        return rgb;
    }
    }