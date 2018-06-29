public class URtoDFClass {

    // Move table for the permutation of six U-face and D-face edges in phase2. The positions of the DL and DB edges are
    // determined by the parity.
    // URtoDF < 665280 in phase 1
    // URtoDF < 20160 in phase 2
    // URtoDF = 0 for solved cube.

    public static short[,] URtoDF_Move = new short[CoordCube.N_URtoDF, CoordCube.N_MOVE];
    static URtoDFClass()
    {
        CubieCube a = new CubieCube();
        for (short i = 0; i < CoordCube.N_URtoDF; i++)
        {
            a.setURtoDF(i);
            for (int j = 0; j < 6; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    a.edgeMultiply(CubieCube.moveCube[j]);
                    URtoDF_Move[i, 3 * j + k] = (short)a.getURtoDF();
                    // Table values are only valid for phase 2 moves!
                    // For phase 1 moves, casting to short is not possible.
                }
                a.edgeMultiply(CubieCube.moveCube[j]);
            }
        }
    }
}
