using System;

public class TwistMoveClass {

    // Move table for the twists of the corners
    // twist < 2187 in phase 2.
    // twist = 0 in phase 2.

    public static short[,] twistMove = new short[CoordCube.N_TWIST, CoordCube.N_MOVE];
    static TwistMoveClass()
    {
		CubieCube a = new CubieCube();
		for (short i = 0; i<CoordCube.N_TWIST; i++)
        {
			a.setTwist(i);
			for (int j = 0; j< 6; j++)
            {
				for (int k = 0; k< 3; k++)
                {
					a.cornerMultiply(CubieCube.moveCube[j]);
                    try
                    {
                        twistMove[i, (3 * j) + k] = a.getTwist();
                        //[2187, 17]
                    }
                    catch(Exception e)
                    {
                        break;
                    }


                }
                a.cornerMultiply(CubieCube.moveCube[j]);// 4. faceturn restores
				// a
			}
		}
	}
}
