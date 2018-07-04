public class UBtoDFClass {

    // Move table for the three edges UB,DR and DF in phase1.
    public static short[,] UBtoDF_Move = new short[CoordCube.N_UBtoDF, CoordCube.N_MOVE];
    static UBtoDFClass()
    {
        CubieCube a = new CubieCube();
        for (short i = 0; i < CoordCube.N_UBtoDF; i++)
        {
            a.setUBtoDF(i);
            for (int j = 0; j < 6; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    a.edgeMultiply(CubieCube.moveCube[j]);
                    UBtoDF_Move[i, 3 * j + k] = a.getUBtoDF();
                }
                a.edgeMultiply(CubieCube.moveCube[j]);
            }
        }
    }
}
