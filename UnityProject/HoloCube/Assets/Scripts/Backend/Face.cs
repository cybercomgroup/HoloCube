using System.Collections.Generic;
using ColorThings;

namespace Backend
{
    public class Face
    {
        public RubicColors TopColor;

        public RubicColors MiddleColor;

        /*
         * This is how the colors are index in the list
         *  _ _ _
         * |0 1 2|
         * |7 X 3|
         * |6 5 4|
         */
        public List<RubicColors> Colors;

        public Face()
        {
            Colors = new List<RubicColors>();
        }
    }
}