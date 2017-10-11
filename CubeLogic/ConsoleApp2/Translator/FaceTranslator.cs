using System.Collections.Generic;
using System.Linq.Expressions;

namespace ConsoleApp2
{
    public class Translator
    {
        public Side[] ScannedSides = new Side[5];
        public List<CubeColor> ScannedColors = new List<CubeColor>();


        public void setSides()
        {
            for (int i = 0; i < ScannedSides.Length; i++)
            {
                switch (ScannedSides[i].Colors[8])
                {
                    case CubeColor.Yellow:
                        Side yellowSide = new Side(ScannedSides[i]);
                        break;
                    case CubeColor.Blue:
                        Side blueSide = new Side(ScannedSides[i]);
                }
            }
        }


        //Takes in 6 faces
        //Each face contains 9 colours
        //The middle face is the face identifier
    }
}