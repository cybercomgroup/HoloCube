namespace Color
{
    public class ColorRange
    {
        public int Start1 { get; set; }
        public int End1 { get; set; }


        public ColorRange(int start1, int end1)
        {
            Start1 = start1;
            End1 = end1;
        }

        public bool IsInRange(int nr)
        {
            if (Start1 > End1)
            {
                if (nr > Start1) return true;
                if (nr < End1) return true;
                return false;
            }

            var b = nr > Start1 && nr < End1;
            return b;
        }
    }
}