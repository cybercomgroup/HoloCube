using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;

namespace ConsoleApp2
{
    public class Side
    {
        public CubeColor TopLeft
        {
            get => _topLeft;
            set => _topLeft = value;
        }

        public CubeColor TopMid
        {
            get => _topMid;
            set => _topMid = value;
        }

        public CubeColor TopRight
        {
            get => _topRight;
            set => _topRight = value;
        }

        public CubeColor Left
        {
            get => _left;
            set => _left = value;
        }

        public CubeColor Middle
        {
            get => _middle;
            set => _middle = value;
        }

        public CubeColor Right
        {
            get => _right;
            set => _right = value;
        }

        public CubeColor BottomLeft
        {
            get => _botLeft;
            set => _botLeft = value;
        }

        public CubeColor BottomMid
        {
            get => _botMid;
            set => _botMid = value;
        }

        public CubeColor BottomRight
        {
            get => _botRight;
            set => _botRight = value;
        }

        private CubeColor _topLeft;
        private CubeColor _topMid;
        private CubeColor _topRight;
        private CubeColor _left;
        private CubeColor _middle;
        private CubeColor _right;
        private CubeColor _botLeft;
        private CubeColor _botMid;
        private CubeColor _botRight;
        
        

        public Side(List<CubeColor> faceColorList)
        {
            _topLeft = faceColorList[0];
            _topMid = faceColorList[1];
            _topRight = faceColorList[2];
            _right = faceColorList[3];
            _botRight = faceColorList[4];
            _botMid = faceColorList[5];
            _botLeft = faceColorList[6];
            _left = faceColorList[7];

            _middle = faceColorList[8];

           
        }
    }
}