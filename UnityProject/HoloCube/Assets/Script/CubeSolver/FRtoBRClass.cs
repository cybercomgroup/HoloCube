using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRtoBRClass {

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // Move table for the four UD-slice edges FR, FL, Bl and BR
    // FRtoBRMove < 11880 in phase 1
    // FRtoBRMove < 24 in phase 2
    // FRtoBRMove = 0 for solved cube

    //public static short[,] FRtoBR_Move = new short[,] { { CoordCube.N_FRtoBR, CoordCube.N_MOVE } };
    public static short[,] FRtoBR_Move = new short[CoordCube.N_FRtoBR, CoordCube.N_MOVE];
    static FRtoBRClass()
    {
        CubieCube a = new CubieCube();
        for (short i = 0; i < CoordCube.N_FRtoBR; i++)
        {
            a.setFRtoBR(i);
            for (int j = 0; j < 6; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    a.edgeMultiply(CubieCube.moveCube[j]);
                    FRtoBR_Move[i, 3 * j + k] = a.getFRtoBR();
                }
                a.edgeMultiply(CubieCube.moveCube[j]);
            }
        }
    }
}
