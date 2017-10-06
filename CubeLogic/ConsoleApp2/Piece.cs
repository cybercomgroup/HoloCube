using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public enum CubeColor
    {
        White, Green, Yellow, Orange, Blue, Red, Empty
    }

    class Piece
    {
        public CubeColor xColor;
        public CubeColor yColor;
        public CubeColor zColor;

        public Piece(CubeColor xColor, CubeColor yColor, CubeColor zColor)
        {
            this.xColor = xColor;
            this.yColor = yColor;
            this.zColor = zColor;
        }

        public void RotateHorizontal()
        {
            CubeColor temp = xColor;
            xColor = yColor;
            yColor = temp;
        }

        public void RotateVertical()
        {
            CubeColor temp = xColor;
            xColor = zColor;
            zColor = temp;
        }

        public void RotateZ()
        {
            CubeColor temp = yColor;
            yColor = zColor;
            zColor = temp;
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
    }

}
