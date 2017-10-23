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


        public Tuple<RubikCube, Dictionary<CubeSide, Face>> setSides()
        {
            var front = new Face();
            var top = new Face();
            var back = new Face();
            var right = new Face();
            var bot = new Face();
            var left = new Face();
            //Front
            ScannedSides.Add(new Face(CubeColor.White, CubeColor.Orange));
            //Top
            ScannedSides.Add(new Face(CubeColor.Green, CubeColor.Red));
            //Back
            ScannedSides.Add(new Face(CubeColor.Yellow, CubeColor.Orange));
            //Bot
            ScannedSides.Add(new Face(CubeColor.Blue, CubeColor.Red));
            //Right
            ScannedSides.Add(new Face(CubeColor.Orange, CubeColor.Yellow));
            //Left
            ScannedSides.Add(new Face(CubeColor.Red, CubeColor.White));

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
                {CubeSide.Left, left},
                {CubeSide.Right, right}
            };
            
            setColorsOnFaces(dict);
            
            //Todo remove this vvvvvvvvvv 
          

            //MiddleMiddleCubies
            //Front
            cube.Cubies[1, 1, 0] = new Cubie(CubeColor.White, CubeColor.Empty, CubeColor.Empty);
            //Top
            cube.Cubies[1, 2, 1] = new Cubie(CubeColor.Empty, CubeColor.Empty, CubeColor.Blue);
            //Back
            cube.Cubies[1, 1, 2] = new Cubie(CubeColor.Yellow, CubeColor.Empty, CubeColor.Empty);
            //Bottom
            cube.Cubies[1, 0, 1] = new Cubie(CubeColor.Empty, CubeColor.Empty, CubeColor.Green);
            //Left
            cube.Cubies[0, 1, 1] = new Cubie(CubeColor.Empty, CubeColor.Orange, CubeColor.Empty);
            //Right
            cube.Cubies[2, 1, 1] = new Cubie(CubeColor.Empty, CubeColor.Red, CubeColor.Empty);

            var cubeAndFaceDict = new Tuple<RubikCube, Dictionary<CubeSide, Face>>(cube, dict);
            return cubeAndFaceDict;
        }

        private void setColorsOnFaces(Dictionary<CubeSide, Face> dict)
        {
            foreach (var face in dict.Values)
            {
                switch (face.MiddleColor)
                {
                        case CubeColor.White:
                            face.Colors = new List<CubeColor>
                            {
                                CubeColor.White,
                                CubeColor.White,
                                CubeColor.White,
                                CubeColor.White,
                                CubeColor.White,
                                CubeColor.White,
                                CubeColor.White,
                               
                            };
                            break;
                        case CubeColor.Blue:
                            face.Colors = new List<CubeColor>
                            {
                                CubeColor.Blue,
                                CubeColor.Blue,
                                CubeColor.Blue,
                                CubeColor.Blue,
                                CubeColor.Blue,
                                CubeColor.Blue,
                                CubeColor.Blue,
                               
                           };
                            break;
                        case CubeColor.Yellow:
                            face.Colors = new List<CubeColor>
                            {
                                CubeColor.Yellow,
                                CubeColor.Yellow,
                                CubeColor.Yellow,
                                CubeColor.Empty,
                                CubeColor.Yellow,
                                CubeColor.Yellow,
                                CubeColor.Yellow,
                               
                            };
                            break;
                    case CubeColor.Green:
                        face.Colors = new List<CubeColor>
                            {
                                CubeColor.Green,
                                CubeColor.Green,
                                CubeColor.Green,
                                CubeColor.Green,
                                CubeColor.Green,
                                CubeColor.Green,
                                CubeColor.Green,
                                
                            };
                            break;
                    case CubeColor.Orange:
                        face.Colors = new List<CubeColor>
                        {
                            CubeColor.Orange,
                            CubeColor.Orange,
                            CubeColor.Orange,
                            CubeColor.Orange,
                            CubeColor.Orange,
                            CubeColor.Orange,
                            CubeColor.Orange,
                            
                        };
                        break;
                    case CubeColor.Red:
                        face.Colors = new List<CubeColor>
                        {
                            CubeColor.Red,
                            CubeColor.Red,
                            CubeColor.Red,
                            CubeColor.Red,
                            CubeColor.Red,
                            CubeColor.Red,
                            CubeColor.Red,
                            
                        };
                        break;
                }
            }
        }


        public static Face GetFaceByColor(CubeColor color, List<Face> scannedFaces)
        {
            return scannedFaces.FirstOrDefault(f => f.MiddleColor == color);
        }
    }
}