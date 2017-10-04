using System;
using UnityEngine;
using System.Collections;

#if UNITY_5_3 || UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif
using OpenCVForUnity;
using CVRect = OpenCVForUnity.Rect;
using System.Collections.Generic;

namespace OpenCVForUnityExample
{

    /// <summary>
    /// WebCamTextureToMat Example
    /// An example of converting a WebCamTexture image to OpenCV's Mat format.
    /// </summary>
    public class CameraBehaviour : MonoBehaviour
    {
        public Scalar OrangeMin = new Scalar(95, 40, 8);
        public Scalar OrangeMax = new Scalar(120, 65, 50);
        public Scalar WhiteMin = new Scalar(75, 82, 131);
        public Scalar WhiteMax = new Scalar(105, 120, 175);
        public Scalar RedMin = new Scalar(65, 16, 16);
        public Scalar RedMax = new Scalar(85, 50, 105);
        public Scalar BlueMin = new Scalar(10, 27, 80);
        public Scalar BlueMax = new Scalar(40, 75, 165);
        public Scalar GreenMin = new Scalar(32, 39, 55);
        public Scalar GreenMax = new Scalar(74, 90, 145);
        public Scalar YellowMin = new Scalar(90, 99, 10);
        public Scalar YellowMax = new Scalar(115, 120, 80);
        public bool ToggleColorPrint = false;

        public class ColorRange{
            public Scalar Min;
            public Scalar Max;

            public ColorRange(Scalar min, Scalar max) {
                this.Min = min;
                this.Max = max;
            }

            public static bool InRange(Scalar color, ColorRange range) {
                return 
                    color.val[0] >= range.Min.val[0] && color.val[0] <= range.Max.val[0] &&
                    color.val[1] >= range.Min.val[1] && color.val[1] <= range.Max.val[1] &&
                    color.val[2] >= range.Min.val[2] && color.val[2] <= range.Max.val[2];
            }
        }
     
		private enum CubeColor {
			Blue, Green, Red, Yellow, White, Orange, Undefined
		}

        private static Dictionary<CubeColor, ColorRange> ColorRanges = new Dictionary<CubeColor, ColorRange>();

        /// <summary>
        /// Set this to specify the name of the device to use.
        /// </summary>
        public string requestedDeviceName = null;

        /// <summary>
        /// Set the requested width of the camera device.
        /// </summary>
        public int requestedWidth = 640;
        
        /// <summary>
        /// Set the requested height of the camera device.
        /// </summary>
        public int requestedHeight = 480;
        
        /// <summary>
        /// Set the requested to using the front camera.
        /// </summary>
        public bool requestedIsFrontFacing = false;

        /// <summary>
        /// The webcam texture.
        /// </summary>
        WebCamTexture webCamTexture;

        /// <summary>
        /// The webcam device.
        /// </summary>
        WebCamDevice webCamDevice;

        /// <summary>
        /// The rgba mat.
        /// </summary>
        Mat rgbaMat;

        /// <summary>
        /// The colors.
        /// </summary>
        Color32[] colors;

        /// <summary>
        /// The texture.
        /// </summary>
        Texture2D texture;

        /// <summary>
        /// Indicates whether this instance is waiting for initialization to complete.
        /// </summary>
        bool isInitWaiting = false;

        /// <summary>
        /// Indicates whether this instance has been initialized.
        /// </summary>
        bool hasInitDone = false;

        // Use this for initialization
        void Start ()
        {
			ColorRanges.Add(CubeColor.White, new ColorRange(WhiteMin, WhiteMax));
            ColorRanges.Add(CubeColor.Red, new ColorRange(RedMin, RedMax));
            ColorRanges.Add(CubeColor.Green, new ColorRange(GreenMin, GreenMax));
            ColorRanges.Add(CubeColor.Blue, new ColorRange(BlueMin, BlueMax));
            ColorRanges.Add(CubeColor.Orange, new ColorRange(OrangeMin, OrangeMax));
            ColorRanges.Add(CubeColor.Yellow, new ColorRange(YellowMin, YellowMax));

            Initialize ();
        }

        /// <summary>
        /// Initialize of web cam texture.
        /// </summary>
        private void Initialize ()
        {
            if (isInitWaiting)
                return;

            StartCoroutine (_Initialize ());
        }

        /// <summary>
        /// Initialize of webcam texture.
        /// </summary>
        /// <param name="deviceName">Device name.</param>
        /// <param name="requestedWidth">Requested width.</param>
        /// <param name="requestedHeight">Requested height.</param>
        /// <param name="requestedIsFrontFacing">If set to <c>true</c> requested to using the front camera.</param>
        private void Initialize (string deviceName, int requestedWidth, int requestedHeight, bool requestedIsFrontFacing)
        {
            if (isInitWaiting)
                return;

            this.requestedDeviceName = deviceName;
            this.requestedWidth = requestedWidth;
            this.requestedHeight = requestedHeight;
            this.requestedIsFrontFacing = requestedIsFrontFacing;

            StartCoroutine (_Initialize ());
        }

        /// <summary>
        /// Initialize of webcam texture by coroutine.
        /// </summary>
        private IEnumerator _Initialize ()
        {
            if (hasInitDone)
                Dispose ();

            isInitWaiting = true;

            if (!String.IsNullOrEmpty (requestedDeviceName)) {
                webCamTexture = new WebCamTexture (requestedDeviceName, requestedWidth, requestedHeight);
            } else {
                // Checks how many and which cameras are available on the device
                for (int cameraIndex = 0; cameraIndex < WebCamTexture.devices.Length; cameraIndex++) {
                    if (WebCamTexture.devices [cameraIndex].isFrontFacing == requestedIsFrontFacing) {

                        //Debug.Log (cameraIndex + " name " + WebCamTexture.devices [cameraIndex].name + " isFrontFacing " + WebCamTexture.devices [cameraIndex].isFrontFacing);
                        webCamDevice = WebCamTexture.devices [cameraIndex];
                        webCamTexture = new WebCamTexture (webCamDevice.name, requestedWidth, requestedHeight);

                        break;
                    }
                }
            }

            if (webCamTexture == null) {
                if (WebCamTexture.devices.Length > 0) {
                    webCamDevice = WebCamTexture.devices [0];
                    webCamTexture = new WebCamTexture (webCamDevice.name, requestedWidth, requestedHeight);
                } else {
                    webCamTexture = new WebCamTexture (requestedWidth, requestedHeight);
                }
            }

            // Starts the camera.
            webCamTexture.Play ();

            while (true) {
                // If you want to use webcamTexture.width and webcamTexture.height on iOS, you have to wait until webcamTexture.didUpdateThisFrame == 1, otherwise these two values will be equal to 16. (http://forum.unity3d.com/threads/webcamtexture-and-error-0x0502.123922/).
                #if UNITY_IOS && !UNITY_EDITOR && (UNITY_4_6_3 || UNITY_4_6_4 || UNITY_5_0_0 || UNITY_5_0_1)
                if (webCamTexture.width > 16 && webCamTexture.height > 16) {
                #else
                if (webCamTexture.didUpdateThisFrame) {
                    #if UNITY_IOS && !UNITY_EDITOR && UNITY_5_2                                    
                    while (webCamTexture.width <= 16) {
                        webCamTexture.GetPixels32 ();
                        yield return new WaitForEndOfFrame ();
                    } 
                    #endif
                    #endif

                    Debug.Log ("name " + webCamTexture.name + " width " + webCamTexture.width + " height " + webCamTexture.height + " fps " + webCamTexture.requestedFPS);
                    Debug.Log ("videoRotationAngle " + webCamTexture.videoRotationAngle + " videoVerticallyMirrored " + webCamTexture.videoVerticallyMirrored + " isFrongFacing " + webCamDevice.isFrontFacing);

                    isInitWaiting = false;
                    hasInitDone = true;

                    OnInited ();

                    break;
                } else {
                    yield return 0;
                }
            }
        }

        /// <summary>
        /// Releases all resource.
        /// </summary>
        private void Dispose ()
        {
            isInitWaiting = false;
            hasInitDone = false;

            if (webCamTexture != null) {
                webCamTexture.Stop ();
                webCamTexture = null;
            }
            if (rgbaMat != null) {
                rgbaMat.Dispose ();
                rgbaMat = null;
            }
        }

        /// <summary>
        /// Initialize completion handler of the webcam texture.
        /// </summary>
        private void OnInited ()
        {
            if (colors == null || colors.Length != webCamTexture.width * webCamTexture.height)
                colors = new Color32[webCamTexture.width * webCamTexture.height];
            if (texture == null || texture.width != webCamTexture.width || texture.height != webCamTexture.height)
                texture = new Texture2D (webCamTexture.width, webCamTexture.height, TextureFormat.RGBA32, false);

            rgbaMat = new Mat (webCamTexture.height, webCamTexture.width, CvType.CV_8UC4);

            gameObject.GetComponent<Renderer>().material.mainTexture = texture;

            gameObject.transform.localScale = new Vector3 (webCamTexture.width, webCamTexture.height, 1);
            Debug.Log ("Screen.width " + Screen.width + " Screen.height " + Screen.height + " Screen.orientation " + Screen.orientation);


            float width = rgbaMat.width ();
            float height = rgbaMat.height ();

            float widthScale = (float)Screen.width / width;
            float heightScale = (float)Screen.height / height;
            if (widthScale < heightScale) {
                Camera.main.orthographicSize = (width * (float)Screen.height / (float)Screen.width) / 2;
            } else {
                Camera.main.orthographicSize = height / 2;
            }
        }

        
		private OpenCVForUnity.Rect[] PrintGrid(Mat view) 
		{
			
			int rectWidth = 100;
            Imgproc.rectangle(
				view,
                new Point(view.width() / 2 - rectWidth / 2, view.height() / 2 - rectWidth / 2),
                new Point(view.width() / 2 + rectWidth / 2, view.height() / 2 + rectWidth / 2),
                new Scalar(255, 0, 255), 2);

            
            CVRect[] squares = new CVRect[9];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    squares[3 * i + j] = new CVRect(
                        view.width() / 2 - rectWidth / 2 + j * rectWidth / 3,
                        view.height() / 2 - rectWidth / 2 + i * rectWidth / 3,
                        rectWidth / 3, rectWidth / 3);
					Point p1 = new Point(squares[3 * i + j].x, squares[3 * i + j].y);
					Point p2 = new Point(p1.x + squares[3 * i + j].width, p1.y + squares[3 * i + j].height);
					
					Imgproc.rectangle(rgbaMat, p1, p2, new Scalar(255,0,255), 2);
                }
            }

			return squares;
		}

        private CubeColor DetColor(Scalar color) {
            CubeColor res = CubeColor.Undefined;

            foreach(CubeColor c in ColorRanges.Keys)
            {
                if (ColorRange.InRange(color, ColorRanges[c]))
                {
                    res = c;
                }
            }
            return res;
        }

		private CubeColor[] DetColors(Mat view, CVRect[] squares) 
		{
			CubeColor[] colors = new CubeColor[9];
            
            for (int i = 0; i < squares.Length; i++)
            {
                CVRect R = squares[i];
                float r = 0, g = 0, b = 0;

                for (int j = R.x; j < R.width + R.x; j++)
                {
                    for (int k = R.y; k < R.height + R.y; k++)
                    {
                        Color col = webCamTexture.GetPixel(j,k);
                        //Vec3b color = indexer[k, j];
                        r += col.r * 255;
                        g += col.g * 255;
                        b += col.b * 255;
                    }
                }

                int pixels = R.width * R.height;
                Scalar c = new Scalar(r / pixels, g / pixels, b / pixels);                
                
                colors[i] = DetColor(c);
                if(ToggleColorPrint)
                    Debug.Log(c);
                
            }

            bool found = true;
            
            foreach (CubeColor c in colors)
            {
                if (c == CubeColor.Undefined)
                {
                    return null;
                }
            }

            if (found)
            {
                Debug.Log("Found grid!");
                string s = "";
                for (int i = 0; i < colors.Length / 3; i++)
                {
                    for (int j = 0; j < colors.Length / 3; j++)
                    {
                        s += (colors[3 * i + j] + " ");
                    }
                    
                }
                Debug.Log(s);
            }
			return colors;
		}

        // Update is called once per frame
        void Update ()
        {
            if (hasInitDone && webCamTexture.isPlaying && webCamTexture.didUpdateThisFrame) {
                Utils.webCamTextureToMat (webCamTexture, rgbaMat, colors);
                Imgproc.putText (rgbaMat, "W:" + rgbaMat.width () + " H:" + rgbaMat.height () + " SO:" + Screen.orientation, new Point (5, rgbaMat.rows () - 10), Core.FONT_HERSHEY_SIMPLEX, 1.0, new Scalar (255, 255, 255, 255), 2, Imgproc.LINE_AA, false);
				//Imgproc.line(rgbaMat,new Point(0,0), new Point(150,150), new Scalar(255,0,255), 2);
				// Print the scanner grid
				CVRect[] rect = PrintGrid(rgbaMat);

				// Try determine colors of the grid
				CubeColor[] colorGrid = DetColors(rgbaMat ,rect);

				// Write to Camera texture
				Utils.matToTexture2D (rgbaMat, texture, colors);
            }
        }

        /// <summary>
        /// Raises the destroy event.
        /// </summary>
        void OnDestroy ()
        {
            Dispose ();
        }

    }
}