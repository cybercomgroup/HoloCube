﻿using System;
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

        public Face(CubeColor middleColor, CubeColor topColor)
        {
            MiddleColor = middleColor;
            TopColor = topColor;
        }
        
        public void RotateColors(int nr)
        {
            
            
            for (int j = 0; j < nr; j++)
            {
                var temp = Colors.ToArray();
            
                for (int i = 0; i < Colors.Count; i++)
                {
                    // [Mod(i + 2 //*nr// , Colors.Count)];
                    
                    temp[i] = Colors[Mod(i + 2, Colors.Count)];
                }

                Colors = new List<CubeColor>(temp);
                
                //workAThome
            }
            
        }
        
        private int Mod(int x, int m)
        {
            return (x % m + m) % m;
        }
    }
}