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






        /*  for (int i = 0; i < imgTexture.width; i++)
          {
              for (int j = 0; j < imgTexture.height; j++)
              {

                  imgTexture.








                  Color _color = imgTexture.GetPixel(i, j);
                  if (imgTexture.GetPixel(i, j).b > 0.3 && imgTexture.GetPixel(i, j).r < 0.5 && imgTexture.GetPixel(i, j).g < 0.5 &&
                      imgTexture.GetPixel(i-1, j-1).b > 0.3 && imgTexture.GetPixel(i-1, j-1).r < 0.5 && imgTexture.GetPixel(i-1, j-1).g < 0.5 &&
                      imgTexture.GetPixel(i+1, j+1).b > 0.3 && imgTexture.GetPixel(i+1, j+1).r < 0.5 && imgTexture.GetPixel(i+1, j+1).g < 0.5)
                  {
                      imgTexture.SetPixel(i, j, new Color(1, 1, 0));
                          imgTexture.SetPixel(i+1, j+1, new Color(1, 1, 0));
                          imgTexture.SetPixel(i-1, j-1, new Color(1, 1, 0));
                  }
                  //imgTexture.SetPixel(i, j, new Color((float)(0.5),(float)0.5,(float)0.5));
  */



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


                //if (total >= 4 && total <= 6)
                if (total == 4)
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

                    if (isRect)
                    {
                        if (rect.width > 20) rectPre.Add(rect);
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

                        Point[] asd = contour.toArray();
                        double basoX = 0;
                        double basoY = 0;
                        for (int a = 0; a < asd.Length; a++)
                        {
                            basoX = basoX + asd[a].x;
                            basoY = basoY + asd[a].y;
                            if (a == asd.Length - 1)
                            {
                                basoX = basoX / a;
                                basoY = basoY / a;
                            }
                        }
                        if (rectPre.Count == 9) print("9 st");
                        Point median = new Point(basoX, basoY);
                        //String _colores = imgTexture.GetPixel((int)basoX, (int)basoY).ToString();
                        //if (imgTexture.GetPixel((int)basoX, (int)basoY).g > 0.7 && imgTexture.GetPixel((int)basoX, (int)basoY).b > 0.7 && imgTexture.GetPixel((int)basoX, (int)basoY).r > 0.7) _colores = "white";
                        //    print(_colores);

                        //Rgb rgb = new Rgb(imgTexture.GetPixel((int)basoX, (int)basoY).r, imgTexture.GetPixel((int)basoX, (int)basoY).g, imgTexture.GetPixel((int)basoX, (int)basoY).b);
                        //Rgb rgb = new Rgb(imgTexture.GetPixel(rect.x + 10, rect.y + 10).r, imgTexture.GetPixel(rect.x + 10, rect.y + 10).g, imgTexture.GetPixel(rect.x + 10, rect.y + 10).b);
                        //var hsv = rgb.To<Hsv>();
                        //String _colores = rgb.ToString();
                        //var satur = (int)(hsv.S * 100);
                        //print(total);
                        List<double[]> Colors = new List<double[]>();
                        for (int op = 0; op < 10; op++) {
                            if (rectPre.Count == 9) {
                                var punkt = imgTexture.GetPixel(rect.x + rect.width / 2, rect.y + rect.height / 2);
                                Imgproc.putText(imgMat, op.ToString(), new Point(rectPre[op].x + 20, rectPre[op].y + 30), Core.FONT_HERSHEY_DUPLEX, 3, new Scalar(200));
                                Rgb rgb = new Rgb(punkt.r, punkt.g, punkt.b);
                                var hsv = rgb.To<Hsv>();
                                String farg = "Ingen farg";

                                if (hsv.H >= 45 && hsv.H <= 70) farg = "Gul";
                                if (hsv.H >= 10 && hsv.H <= 45) farg = "Orange";
                                //String rgbs = " " +hsv.H + " " + hsv.S + " " + hsv.V;
                                print(farg);

                                Colors.Add(new double[] {rgb.R, rgb.G, rgb.B});
                                print(Colors.Count);
                                if(Colors.Count == 9) {
                                    ColorMap.Colors = Colors;
                                    ColorMap.Redraw();
                                }
                                

                            }
                            
                        }
                        Imgproc.drawContours(imgMat, contours, idx, new Scalar(255, 100, 155), 4);
                        
                        Resources.UnloadUnusedAssets(); //Fixes the memory leak

                        
                    }
                }
            }
            //ColorMap.Colors = Colors;
            //if(Colors.Count == 9) ColorMap.Redraw();
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
