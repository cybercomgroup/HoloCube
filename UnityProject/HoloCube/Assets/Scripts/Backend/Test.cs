namespace Backend
{
    public class Test
    {
        
        /*
         
                 T
         _ _ _ _ _ _ _ _ _ _ _ _ _
         |     |     |     |     |
         |  L  |  F  |  R  |   B |
         |_ _ _|_ _ _|_ _ _|_ _ _|
                  Bo
          
         */
        
        
        public Face Front { get; set; }
        public Face Top { get; set; }
        public Face Back { get; set; }
        public Face Bottom { get; set; }
        public Face Left { get; set; }
        public Face Right { get; set; }

        public Test(Face front, Face top, Face back, Face bottom, Face left, Face right)
        {
            Front = front;
            Top = top;
            Back = back;
            Bottom = bottom;
            Left = left;
            Right = right;
        }
    }
}