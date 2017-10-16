using System;
using System.Collections.Generic;
using System.Linq;
using ColorMine.ColorSpaces;
using UnityEngine;

namespace ColorThings
{
    public class ColorDetection
    {
        public bool PauseOnColorNotFound { get; set; }
        
        public ColorRange Yellow;
        public ColorRange White;
        public ColorRange Red;
        public ColorRange Orange;
        public ColorRange Blue;
        public ColorRange Green;

        
        public ColorDetection()
        {
            White = new ColorRange(0, 35); // check Saturation

            Yellow = new ColorRange(40, 70);
            Red = new ColorRange(340, 11);
            Orange = new ColorRange(10, 40);
            Blue = new ColorRange(195, 260);
            Green = new ColorRange(80, 180);
        }
        
        public static Color UnityColorFromEnum(RubicColors c)
        {
            switch (c)
            {
                case RubicColors.White: return Color.white;
                case RubicColors.Red: return Color.red;
                case RubicColors.Green: return Color.green;
                case RubicColors.Blue: return Color.blue;
                case RubicColors.Yellow: return Color.yellow;
                case RubicColors.Orange: return new Color(1f, 0.39f, 0f);
                case RubicColors.Black: return Color.black;
                default:
                    throw new ArgumentOutOfRangeException("c", c, null);
            }
        }

        public RubicColors ColorEnumFromScalarColor(double[] scalar)
        {
            var red = (int) scalar[0];
            var green = (int) scalar[1];
            var blue = (int) scalar[2];

            var fromHsvColor = ColorFromHsvColor(red, green, blue);
            var fromRgbColor = ColorFromRgbColor(red, green, blue);

            if (fromHsvColor == fromRgbColor)
            {
                return fromHsvColor;
            }
            //rgb is way better at detecting white..
            if (fromRgbColor == RubicColors.White) return RubicColors.White;

            return fromHsvColor;
        }

        private RubicColors ColorFromRgbColor(int red, int green, int blue)
        {
            if ((red + green + blue) / 3 > 200) return RubicColors.White;


            var dominantColor = GetDominantColorFromScalar(red, green, blue);

            switch (dominantColor)
            {
                case RgbEnums.Red:
                    return CalculateRed(red, green, blue);
                case RgbEnums.Green:
                    return CalculateGreen(red, green, blue);
                case RgbEnums.Blue:
                    return CalculateBlue(red, green, blue);
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private RubicColors ColorFromHsvColor(int red, int green, int blue)
        {
            var rgb = new Rgb(red, green, blue);

            var hsv = rgb.To<Hsv>();

            var saturation = (int) (hsv.S * 100);
            if (White.IsInRange(saturation)) return RubicColors.White;

            if (Yellow.IsInRange((int) hsv.H)) return RubicColors.Yellow;
            if (Red.IsInRange((int) hsv.H)) return RubicColors.Red;
            if (Orange.IsInRange((int) hsv.H)) return RubicColors.Orange;
            if (Blue.IsInRange((int) hsv.H)) return RubicColors.Blue;
            if (Green.IsInRange((int) hsv.H)) return RubicColors.Green;

            Debug.Log("Can't find color." + string.Format("h:{0} s:{1} v:{2}", hsv.H, hsv.S, hsv.V));
            #if UNITY_EDITOR
            if (PauseOnColorNotFound) UnityEditor.EditorApplication.isPaused = true;
            #endif
            return RubicColors.Black;
        }

        //red,orange and yellow
        private RubicColors CalculateRed(int red, int green, int blue)
        {
            var deltaGreenBlue = Math.Abs(green - blue);
            var deltaGreenRed = Math.Abs(green - red);


            if (deltaGreenBlue > 70 && deltaGreenRed < 60)
                return RubicColors.Yellow;

            if (Math.Min(green, blue) * 1.7 < Math.Max(green, blue))
                return RubicColors.Orange;

            return RubicColors.Red;
        }

        //Green
        private RubicColors CalculateGreen(int red, int green, int blue)
        {
            if (red > 100) return RubicColors.Yellow;
            return RubicColors.Green;
        }

        //blue and white
        private RubicColors CalculateBlue(int red, int green, int blue)
        {
            return RubicColors.Blue;
        }


        private RgbEnums GetDominantColorFromScalar(int red, int green, int blue)
        {
            var dict = new Dictionary<RgbEnums, int>
            {
                {RgbEnums.Blue, blue},
                {RgbEnums.Green, green},
                {RgbEnums.Red, red}
            };

            var max = Math.Max(red, Math.Max(green, blue));
            var key = dict.FirstOrDefault(kvp => kvp.Value == max).Key;
            return key;
        }
    }
}