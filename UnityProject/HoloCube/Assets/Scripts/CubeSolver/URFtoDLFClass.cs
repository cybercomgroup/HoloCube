public class URFtoDLFClass {

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // Move table for permutation of six corners. The positions of the DBL and DRB corners are determined by the parity.
    // URFtoDLF < 20160 in phase 1
    // URFtoDLF < 20160 in phase 2
    // URFtoDLF = 0 for solved cube.

    public static short[,] URFtoDLF_Move = new short[CoordCube.N_URFtoDLF, CoordCube.N_MOVE];
    static URFtoDLFClass()
    {
        CubieCube a = new CubieCube();
        for (short i = 0; i < CoordCube.N_URFtoDLF; i++)
        {
            a.setURFtoDLF(i);
            for (int j = 0; j < 6; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    a.cornerMultiply(CubieCube.moveCube[j]);
                    URFtoDLF_Move[i, 3 * j + k] = a.getURFtoDLF();
                }
                a.cornerMultiply(CubieCube.moveCube[j]);
            }
        }
    }
}
