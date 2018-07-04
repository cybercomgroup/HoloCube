using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCVForUnity;
using Backend;
using ColorThings;


public class DispNoAuto : MonoBehaviour
{
    private WebCamTexture webcamTexture;
    private Texture2D _texture;
    public int MinCubeX = 186;
    public int MaxCubeX = 500;
    public int MinCubeY = 63;
    public int MaxCubeY = 380;
    private int CubeWidth = 314;
    private int CubeHeight = 317;
    private int PieceWidth = 105;
    private int PieceHeight = 105;
    private Mat rgbMat, hsvMat;
    Renderer renderer;
    Texture2D imgTexture;
    private ColorDetection _colorDetection;

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

        _colorDetection = new ColorDetection();


    }

    // Update is called once per frame  
    void Update()
    {
        Resources.UnloadUnusedAssets();
        Mat mat = new Mat(webcamTexture.height, webcamTexture.width, CvType.CV_8UC3);

        //Utils.texture2DToMat(imgTexture, mat);
        Utils.webCamTextureToMat(webcamTexture, mat);
        using (var matCopy = mat.clone())
        {
            //if (_texture == null) _texture = new Texture2D(imgTexture.width, imgTexture.height);
            for (int i = 0; i < 3; i++)
            {
                var p1 = new Point(MinCubeX + (PieceWidth * i), MaxCubeY);
                var p2 = new Point(MinCubeX + (PieceWidth * i), MinCubeY);
                Imgproc.line(matCopy, p1, p2, new Scalar(0, 255, 0), 5);
            }
            for (int i = 0; i < 3; i++)
            {
                var p1 = new Point(MinCubeX, MinCubeY + (PieceHeight * i));
                var p2 = new Point(MaxCubeX, MinCubeY + (PieceHeight * i));
                Imgproc.line(matCopy, p1, p2, new Scalar(0, 255, 0), 5);
            }

            List<Color> Colors = new List<Color>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var row = MinCubeY + (PieceHeight * i + (PieceHeight / 2));
                    var col = MinCubeX + (PieceWidth * j + (PieceWidth / 2));
                    Imgproc.drawMarker(matCopy, new Point(row+20, col+20), new Scalar(0, 255, 0), 1, 1, 1, 1);
                    //Imgproc.
                    var color = webcamTexture.GetPixel(row, col);
                    var eColor = _colorDetection.ColorEnumFromScalarColor(new double[] { color.r * 255, color.g * 255, color.b * 255 });
                    var _color = ColorDetection.UnityColorFromEnum(eColor);
                    Colors.Add(_color);
                }
                if (Colors.Count == 9)
                {
                /*    for (int w = 0; w < 9; w++)
                    {
                        Debug.Log("Color at [" + w + "] is " + Colors[w].ToString());
                    }*/
                    ColorTracker.Instance.addToTemp(Colors);
                }
                if (_texture == null) _texture = new Texture2D(matCopy.cols(), matCopy.rows(), TextureFormat.RGBA32, false);
                Utils.matToTexture2D(matCopy, _texture);
                GetComponent<Renderer>().material.mainTexture = _texture;
                //GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Transparent");
            }
        }
    }
}

