using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ConsoleApp2
{
    public class Translator
    {
        private RubikCube testCube = new RubikCube();
        public List<Face> ScannedSides = new List<Face>();
        
        
        public Cubie[,,] translatedCubies = new Cubie[3,3,3];

        public Translator()
        {
            
        }

        public void setSides()
        {
            for (int i = 0; i < ScannedSides.Count; i++)
            {
                switch (ScannedSides[i].MiddleColor)
                {
                    case CubeColor.White:
                        var whiteSide = GetFaceByColor(CubeColor.White,ScannedSides);
                        break;
                    case CubeColor.Green:
                        var greenSide = GetFaceByColor(CubeColor.Green,ScannedSides);
                        break;
                    case CubeColor.Yellow:
                        var yellowSide = GetFaceByColor(CubeColor.Yellow,ScannedSides);

                        break;
                    case CubeColor.Orange:
                        var orangeSide = GetFaceByColor(CubeColor.Orange,ScannedSides);

                        break;
                    case CubeColor.Blue:
                        var blueSide = GetFaceByColor(CubeColor.Blue,ScannedSides);

                        break;
                    case CubeColor.Red:
                        var redSide = GetFaceByColor(CubeColor.Red,ScannedSides);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }


        
        public static Face GetFaceByColor(CubeColor color, List<Face> scannedFaces)
        {
            return scannedFaces.FirstOrDefault(f => f.MiddleColor == color);            
        }

        
        
      
        
        public void SetCubies()
        {

            var dict = new Dictionary<CubeSide,CubeColor >
            {
                {CubeSide.Front,CubeColor.White},
                {CubeSide.Back,CubeColor.Yellow},
                {CubeSide.Right,CubeColor.Red},
                {CubeSide.Left,CubeColor.Orange},
                {CubeSide.Top,CubeColor.Blue},
                {CubeSide.Bottom,CubeColor.Green},
            };
            
            
            var whiteCubei = new Cubie(CubeColor.White,CubeColor.Empty,CubeColor.Empty);
            var orange = new Cubie(CubeColor.Empty,CubeColor.Orange,CubeColor.Empty);
            var green = new Cubie(CubeColor.Empty,CubeColor.Empty,CubeColor.Green);
            

            whiteCubei.xColor = CubeColor.White;
            orange.yColor = CubeColor.Orange;
            green.zColor = CubeColor.Green;

            
            Cubie middleCubie = new Cubie();
            

            var whiteFront = GetFaceByColor(CubeColor.White, ScannedSides);
            var currentTop = GetFaceByColor(whiteFront.TopColor, ScannedSides);

            
            /*var bottomLeftFront = new Cubie(whiteFront.Colors[6],);
                
            var red = GetFaceByColor(CubeColor.Red, ScannedSides);
            
            
            
            var topFace = GetFaceByColor(currentFace.TopColor, ScannedSides);
            
            

            middleCubie.xColor = currentFace.Colors[1];
            middleCubie.yColor = topFace.Colors[5];
            middleCubie.zColor = CubeColor.Empty;
            */
            
            
         
            




            




            /*translatedCubies[1, 1, 2] = new Cubie(scannedSides, CubeColor.Empty, CubeColor.Empty);
            translatedCubies[1, 2, 1] = new Cubie(CubeColor.Empty, CubeColor.Empty, CubeColor.Blue);
            translatedCubies[1, 1, 0] = new Cubie(CubeColor.White, CubeColor.Empty, CubeColor.Empty);
            translatedCubies[0, 1, 1] = new Cubie(CubeColor.Empty, CubeColor.Orange, CubeColor.Empty);
            translatedCubies[2, 1, 1] = new Cubie(CubeColor.Empty, CubeColor.Red, CubeColor.Empty);
            translatedCubies[1, 0, 1] = new Cubie(CubeColor.Empty, CubeColor.Empty, CubeColor.Green);
            */

        }
    
    }
    
    
}