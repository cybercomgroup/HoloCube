using UnityEngine;

namespace Backend
{
    public class Face
    {
        public Color[,] Colors { get; set; }

        private readonly Faces _faceType;

        
        public Face(Faces faceType)
        {
            _faceType = faceType;
        }
        
        public new Faces GetType()
        {
            return _faceType;
        }
            
    }
}