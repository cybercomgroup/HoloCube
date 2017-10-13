using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;

namespace ConsoleApp2
{
    
        public class Face
        {
            public CubeColor TopColor;

            public CubeColor MiddleColor;

            /*
             * This is how the colors are index in the list
             *  _ _ _
             * |0 1 2|
             * |7 X 3|
             * |6 5 4|
             * 
             */
            public List<CubeColor> Colors;

            public Face()
            {
                Colors = new List<CubeColor>();
            }

           /* public List<Face> getSixHardCodedFaces()
            {
                var facesList = new List<Face>();
                var face = new Face();

                for (int i = 0; i < UPPER; i++)
                {
                    
                }
                face.Colors[0] = CubeColor.Yellow;
                face.Colors[1] = CubeColor.Red;
                face.Colors[2] = CubeColor.Blue;
                face.Colors[3] = CubeColor.White;
                face.Colors[4] = CubeColor.Orange;
                face.Colors[5] = CubeColor.Red;
                face.Colors[6] = CubeColor.Green;
                face.Colors[7] = CubeColor.Blue;
                face.MiddleColor = CubeColor.White;
                face.TopColor = CubeColor.Green;
                
                
                return 
            } */
            
        }
    
}