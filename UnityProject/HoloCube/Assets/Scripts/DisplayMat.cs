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

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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
                for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                {
                    var row = MinCubeY + (piceHeight * y + (piceHeight / 2));
                    var col = MinCubeX + (piceWidth * x + (piceWidth / 2));
                    var color = dst.get(row, col);
                    var rgb = GetValue(color);
                    Colors.Add(new double[] {(int) rgb.R / 100, (int) rgb.G / 100, (int) rgb.B / 100});
                }


                ColorMap.Colors = Colors;
                ColorMap.Redraw();

                Utils.matToTexture2D(dst, _texture);

                GetComponent<Renderer>().material.mainTexture = _texture;
                GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Transparent");
            }
        }
    }

    private IRgb GetValue(double[] col)
    {
        var rgb = new Rgb(col[0], col[1], col[2]);
        var hsv = rgb.To<Hsv>();
        hsv.V = 100;
        return hsv.ToRgb();
    }
}