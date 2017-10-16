using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp2
{
    public class Translator
    {
        private static List<Face> ScannedSides = new List<Face>();
        public Dictionary<CubeSide, Face> faceDict;
        public RubikCube cube = new RubikCube();

        private static void makeUpScannedSides()
        {
        }

        public Translator()
        {
            makeUpScannedSides();
            faceDict = setSides();
            checkSides(faceDict);
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
                s += "0 1 2 \n";

                //s += "{0} {1} {2}\n", face.Value.Colors[0].ToString(), face.Value.Colors[1].ToString(), face.Value.Colors[2].ToString();
                s += "7 " + ColorToString(face.Value.MiddleColor) + " 3" + "\n";
                s += "6 5 4 \n";
                s += "TopColor: " + ColorToString(face.Value.TopColor) + "\n";
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
            //Front
            ScannedSides.Add(new Face(CubeColor.White, CubeColor.Green));
            //Top
            ScannedSides.Add(new Face(CubeColor.Green, CubeColor.Yellow));
            //Back
            ScannedSides.Add(new Face(CubeColor.Yellow, CubeColor.Blue));
            //Bot
            ScannedSides.Add(new Face(CubeColor.Blue, CubeColor.White));
            //Right
            ScannedSides.Add(new Face(CubeColor.Orange, CubeColor.Green));
            //Left
            ScannedSides.Add(new Face(CubeColor.Red, CubeColor.Green));
            
            cube.Cubies[]

//            ScannedSides.Add(new Face(CubeColor.White, CubeColor.Green));
//            ScannedSides.Add(new Face(CubeColor.Green, CubeColor.Yellow));
//            ScannedSides.Add(new Face(CubeColor.Yellow, CubeColor.Blue));
//            ScannedSides.Add(new Face(CubeColor.Blue, CubeColor.White));
//            ScannedSides.Add(new Face(CubeColor.Red, CubeColor.Green));
//            ScannedSides.Add(new Face(CubeColor.Orange, CubeColor.Green));

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
                    case CubeColor.Blue:
                        bot = GetFaceByColor(CubeColor.Blue, ScannedSides);
                        break;
                    case CubeColor.Orange:
                        right = GetFaceByColor(CubeColor.Orange, ScannedSides);
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