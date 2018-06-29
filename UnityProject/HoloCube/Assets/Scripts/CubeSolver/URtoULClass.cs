public class URtoULClass {

    // **************************helper move tables to compute URtoDF for the beginning of phase2************************

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // Move table for the three edges UR,UF and UL in phase1.
    public static short[,] URtoUL_Move = new short[CoordCube.N_URtoUL, CoordCube.N_MOVE];
    static URtoULClass()
    {
        CubieCube a = new CubieCube();
        for (short i = 0; i < CoordCube.N_URtoUL; i++)
        {
            a.setURtoUL(i);
            for (int j = 0; j < 6; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    a.edgeMultiply(CubieCube.moveCube[j]);
                    URtoUL_Move[i, 3 * j + k] = a.getURtoUL();
                }
                a.edgeMultiply(CubieCube.moveCube[j]);
            }
        }
    }
}
