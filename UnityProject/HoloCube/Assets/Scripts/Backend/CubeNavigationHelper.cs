using System;

namespace Backend
{
    public static class CubeNavigationHelper
    {
        public static Face GetFaceRightOfCurrentFace(Face masterFace, Cube cube)
        {
            switch (masterFace.GetType())
            {
                case Faces.Front: return cube.Right;
                case Faces.Left: return cube.Front;
                case Faces.Bottom: return cube.Right;
                case Faces.Right: return cube.Back;
                case Faces.Top: return cube.Right;
                case Faces.Back: return cube.Left;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public static Face GetFaceDownOfCurrentFace(Face masterFace, Cube cube)
        {
            switch (masterFace.GetType())
            {
                case Faces.Front: return cube.Bottom;
                case Faces.Left: return cube.Bottom;
                case Faces.Bottom: return cube.Back;
                case Faces.Right: return cube.Bottom;
                case Faces.Top: return cube.Front;
                case Faces.Back: return cube.Top;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public static Face GetFaceLeftOfCurrentFace(Face masterFace, Cube cube)
        {
            switch (masterFace.GetType())
            {
                case Faces.Front: return cube.Left;
                case Faces.Left: return cube.Back;
                case Faces.Bottom: return cube.Left;
                case Faces.Right: return cube.Front;
                case Faces.Top: return cube.Left;
                case Faces.Back: return cube.Right;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public static Face GetFaceUpOfCurrentFace(Face masterFace, Cube cube)
        {
            switch (masterFace.GetType())
            {
                case Faces.Front: return cube.Top;
                case Faces.Left: return cube.Top;
                case Faces.Bottom: return cube.Front;
                case Faces.Right: return cube.Top;
                case Faces.Top: return cube.Back;
                case Faces.Back: return cube.Bottom;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}