public class CoordCube
{
    public static readonly short N_TWIST = 2187;// 3^7 possible corner orientations
    public static readonly short N_FLIP = 2048;// 2^11 possible edge flips
    public static readonly short N_SLICE1 = 495;// 12 choose 4 possible positions of FR,FL,BL,BR edges
    public static readonly short N_SLICE2 = 24;// 4! permutations of FR,FL,BL,BR edges in phase2
    public static readonly short N_PARITY = 2; // 2 possible corner parities
    public static readonly short N_URFtoDLF = 20160;// 8!/(8-6)! permutation of URF,UFL,ULB,UBR,DFR,DLF corners
    public static readonly short N_FRtoBR = 11880; // 12!/(12-4)! permutation of FR,FL,BL,BR edges
    public static readonly short N_URtoUL = 1320; // 12!/(12-3)! permutation of UR,UF,UL edges
    public static readonly short N_UBtoDF = 1320; // 12!/(12-3)! permutation of UB,DR,DF edges
    public static readonly short N_URtoDF = 20160; // 8!/(8-6)! permutation of UR,UF,UL,UB,DR,DF edges in phase2
    public static readonly int N_URFtoDLB = 40320;// 8! permutations of the corners
    public static readonly int N_URtoBR = 479001600;// 8! permutations of the corners

    public static readonly short N_MOVE = 18;

    // All coordinates are 0 for a solved cube except for UBtoDF, which is 114
    public short twist;
    public short flip;
    public short parity;
    public short FRtoBR;
    public short URFtoDLF;
    public short URtoUL;
    public short UBtoDF;
    public int URtoDF;

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // Generate a CoordCube from a CubieCube
    public CoordCube(CubieCube c)
    {
        twist = c.getTwist();
        flip = c.getFlip();
        parity = c.cornerParity();
        FRtoBR = c.getFRtoBR();
        URFtoDLF = c.getURFtoDLF();
        URtoUL = c.getURtoUL();
        UBtoDF = c.getUBtoDF();
        URtoDF = c.getURtoDF();// only needed in phase2
    }

    // A move on the coordinate level
    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void move(int m)
    {
        twist = TwistMoveClass.twistMove[twist, m];
        flip = FlipMoveClass.flipMove[flip, m];
        parity = parityMove[parity, m];
        FRtoBR = FRtoBRClass.FRtoBR_Move[FRtoBR, m];
        URFtoDLF = URFtoDLFClass.URFtoDLF_Move[URFtoDLF, m];
        URtoUL = URtoULClass.URtoUL_Move[URtoUL, m];
        UBtoDF = UBtoDFClass.UBtoDF_Move[UBtoDF, m];
        if (URtoUL < 336 && UBtoDF < 336)// updated only if UR,UF,UL,UB,DR,DF
                                         // are not in UD-slice
            URtoDF = MergeURtoULandUBtoDFClass.MergeURtoULandUBtoDF[URtoUL, UBtoDF];
    }

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // Parity of the corner permutation. This is the same as the parity for the edge permutation of a valid cube.
    // parity has values 0 and 1
    public static short[,] parityMove = { { 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1 },
            { 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0 } };

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // Set pruning value in table. Two values are stored in one int.
    /*
    public static void setPruning(int[] table, int index, int value)
    {
        if ((index & 1) == 0)
            table[index / 2] &= 0xf0 | value;
        else
            table[index / 2] &= 0x0f | (value << 4);
    }*/
    public static void setPruning(int[] table, int index, int value)
    {
        if ((index & 1) == 1) { table[index / 2] &= (0x0f | (value << 4)); }
        else { table[index / 2] &= (value | 0xf0); }
    }

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // Extract pruning value
    /*
    public static int getPruning(int[] table, int index)
    {
        if ((index & 1) == 0)
            return (table[index / 2] & 0x0f);
        else
            return ((table[index / 2] & 0xf0) >> 4);
    }*/
    public static int getPruning(int[] table, int index)
    {
        if ((index & 1) == 1) { return ((table[index / 2] & 0xf0) >> 4); }
        else { return (table[index / 2] & 0x0f); }
    }
}
