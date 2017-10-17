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
        private Solver solver = new Solver();

        private static void makeUpScannedSides()
        {
        }

        public Translator()
        {
            makeUpScannedSides();
            //faceDict = setSides();

            // Back
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


        public RubikCube setSides()
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
                    case CubeColor.Blue:
                        top = GetFaceByColor(CubeColor.Blue, ScannedSides);
                        break;
                    case CubeColor.Yellow:
                        back = GetFaceByColor(CubeColor.Yellow, ScannedSides);
                        break;
                    case CubeColor.Green:
                        bot = GetFaceByColor(CubeColor.Green, ScannedSides);
                        break;
                    case CubeColor.Orange:
                        left = GetFaceByColor(CubeColor.Orange, ScannedSides);
                        break;
                    case CubeColor.Red:
                        right = GetFaceByColor(CubeColor.Red, ScannedSides);
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

            
            
            //CornerCubies            
            cube.Cubies[0, 0, 0] = new Cubie(front.Colors[6], left.Colors[4], bot.Colors[0]);
            cube.Cubies[2, 0, 0] = new Cubie(CubeColor.White, CubeColor.Red, CubeColor.Green);
            cube.Cubies[0, 2, 0] = new Cubie(CubeColor.White, CubeColor.Orange, CubeColor.Blue);
            cube.Cubies[2, 2, 0] = new Cubie(CubeColor.White, CubeColor.Red, CubeColor.Blue);
            cube.Cubies[0, 0, 2] = new Cubie(CubeColor.Yellow, CubeColor.Orange, CubeColor.Green);
            cube.Cubies[2, 0, 2] = new Cubie(CubeColor.Yellow, CubeColor.Red, CubeColor.Green);
            cube.Cubies[0, 2, 2] = new Cubie(CubeColor.Yellow, CubeColor.Orange, CubeColor.Blue);
            cube.Cubies[2, 2, 2] = new Cubie(CubeColor.Yellow, CubeColor.Red, CubeColor.Blue);

            //MiddleMiddleCubies
            //Front
            cube.Cubies[1, 1, 0] = new Cubie(dict[CubeSide.Front].MiddleColor, CubeColor.Empty, CubeColor.Empty);
            //Top
            cube.Cubies[1, 2, 1] = new Cubie(CubeColor.Empty, CubeColor.Empty, dict[CubeSide.Top].MiddleColor);
            //Back
            cube.Cubies[1, 1, 2] = new Cubie(dict[CubeSide.Back].MiddleColor, CubeColor.Empty, CubeColor.Empty);
            //Bottom
            cube.Cubies[1, 0, 1] = new Cubie(CubeColor.Empty, CubeColor.Empty, dict[CubeSide.Bottom].MiddleColor);
            //Left
            cube.Cubies[0, 1, 1] = new Cubie(CubeColor.Empty, dict[CubeSide.Left].MiddleColor, CubeColor.Empty);
            //Right
            cube.Cubies[2, 1, 1] = new Cubie(CubeColor.Empty, dict[CubeSide.Right].MiddleColor, CubeColor.Empty);

            return cube;
        }


        public static Face GetFaceByColor(CubeColor color, List<Face> scannedFaces)
        {
            return scannedFaces.FirstOrDefault(f => f.MiddleColor == color);
        }
    }
}