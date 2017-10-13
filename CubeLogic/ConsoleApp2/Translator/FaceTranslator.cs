using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;

namespace ConsoleApp2
{
    public class Translator
    {
        private static List<Face> ScannedSides = new List<Face>();
        public Dictionary<CubeSide, Face> faceDict;


        private static void makeUpScannedSides()
        {
            ScannedSides.Add(new Face(CubeColor.White, CubeColor.Green));
            ScannedSides.Add(new Face(CubeColor.Green, CubeColor.Yellow));
            ScannedSides.Add(new Face(CubeColor.Yellow, CubeColor.Blue));
            ScannedSides.Add(new Face(CubeColor.Blue, CubeColor.White));
            ScannedSides.Add(new Face(CubeColor.Red, CubeColor.Green));
            ScannedSides.Add(new Face(CubeColor.Orange, CubeColor.Green));
        }

        public Translator()
        {
            makeUpScannedSides();
            faceDict = setSides();
            Console.WriteLine(Print2Dmap());

            Console.WriteLine();
            // Back
        }

        public String Print2Dmap()
        {
            var s = "";

            foreach (var face in faceDict)
            {
                s += "\n" + face.Key + "\n";
                s += "\n";
                s += "X X X\n";
                s += "X " + ColorToString(face.Value.MiddleColor) + " X" + "\n";
                s += "X X X \n";
            }

            return s;
        }

        public static String ColorToString(CubeColor color)
        {
            switch (color)
            {
                case CubeColor.White:
                    return "W";
                case CubeColor.Red:
                    return "R";
                case CubeColor.Blue:
                    return "B";
                case CubeColor.Green:
                    return "G";
                case CubeColor.Orange:
                    return "O";
                case CubeColor.Yellow:
                    return "Y";
                case CubeColor.Empty:
                    return "?";
            }

            return "X";
        }


        public Dictionary<CubeSide, Face> setSides()
        {
            var front = new Face();
            var top = new Face();
            var back = new Face();
            var right = new Face();
            var bot = new Face();
            var left = new Face();

            for (int i = 0; i < ScannedSides.Count; i++)
            {
                switch (ScannedSides[i].MiddleColor)
                {
                    case CubeColor.White:
                        front = GetFaceByColor(CubeColor.White, ScannedSides);
                        break;
                    case CubeColor.Green:
                        top = GetFaceByColor(CubeColor.Green, ScannedSides);
                        break;
                    case CubeColor.Yellow:
                        back = GetFaceByColor(CubeColor.Yellow, ScannedSides);

                        break;
                    case CubeColor.Orange:
                        right = GetFaceByColor(CubeColor.Orange, ScannedSides);

                        break;
                    case CubeColor.Blue:
                        bot = GetFaceByColor(CubeColor.Blue, ScannedSides);

                        break;
                    case CubeColor.Red:
                        left = GetFaceByColor(CubeColor.Red, ScannedSides);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            var dict = new Dictionary<CubeSide, Face>
            {
                {CubeSide.Front, front},
                {CubeSide.Back, back},
                {CubeSide.Bottom, bot},
                {CubeSide.Top, top},
                {CubeSide.Right, right},
                {CubeSide.Left, left}
            };

            return dict;
        }


        public static Face GetFaceByColor(CubeColor color, List<Face> scannedFaces)
        {
            return scannedFaces.FirstOrDefault(f => f.MiddleColor == color);
        }
    }
}