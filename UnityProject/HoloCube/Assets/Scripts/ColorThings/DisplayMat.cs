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
    public static bool allCubiesScaned = false;
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
        Resources.UnloadUnusedAssets(); //Fixes the memory leak

        //Get new picture from camera
        imgTexture = new Texture2D(webcamTexture.width, webcamTexture.height);
        imgTexture.SetPixels(webcamTexture.GetPixels());
        imgTexture.Apply();


        Mat imgMat = new Mat(imgTexture.height, imgTexture.width, CvType.CV_8UC3);
        Utils.texture2DToMat(imgTexture, imgMat);

        Mat processedMat = new Mat();

        Imgproc.dilate(imgMat, imgMat, Imgproc.getStructuringElement(Imgproc.MORPH_RECT, new Size(1, 1)));

        //Grayscale the picture
        Imgproc.cvtColor(imgMat, processedMat, Imgproc.COLOR_RGB2GRAY);


        //Blur the picture
        Imgproc.GaussianBlur(processedMat, processedMat, new Size(3, 3), 1);

        Imgproc.equalizeHist(processedMat, processedMat);


        //Find Edges
        Mat edgesOfPicture = new Mat();
        Imgproc.Canny(processedMat, edgesOfPicture, 75, 225);

        List<MatOfPoint> contours = new List<MatOfPoint>();
        Mat hierarchy = new Mat();
        Imgproc.findContours(edgesOfPicture, contours, hierarchy, Imgproc.RETR_EXTERNAL, Imgproc.CHAIN_APPROX_SIMPLE);

        MatOfPoint2f matOfPoint2f = new MatOfPoint2f();
        MatOfPoint2f approxCurve = new MatOfPoint2f();
        List<Rect> rects = new List<Rect>();


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

                if (total == 4)
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
                        if (rect.width > 20) rects.Add(rect);

                        List<double[]> Colors = new List<double[]>();
                        for (int op = 0; op < 10; op++)
                        {
                            if (rects.Count == 9)
                            {

                                allCubiesScaned = true;

                                Color[] blockOfColour = imgTexture.GetPixels(rect.x + rect.width / 2, rect.y + rect.height, rect.width / 3, rect.height / 3, 0);

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

                                Rgb rgb = new Rgb(r, g, b);

                                Colors.Add(new double[] { rgb.R * 255, rgb.G * 255, rgb.B * 255 });
                                print(Colors.Count);
                                if (Colors.Count == 9)
                                {
                                    ColorMap.Colors = Colors;
                                    ColorMap.Redraw();
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
