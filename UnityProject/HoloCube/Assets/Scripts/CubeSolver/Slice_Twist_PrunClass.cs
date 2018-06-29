public class Slice_Twist_PrunClass {

    // Pruning table for the twist of the corners and the position (not permutation) of the UD-slice edges in phase1
    // The pruning table entries give a lower estimation for the number of moves to reach the H-subgroup.
    public static int[] Slice_Twist_Prun = new int[CoordCube.N_SLICE1 * CoordCube.N_TWIST / 2 + 1];
    static Slice_Twist_PrunClass()
    {
        for (int i = 0; i < CoordCube.N_SLICE1 * CoordCube.N_TWIST / 2 + 1; i++)
            Slice_Twist_Prun[i] = -1; // SHOULD BE -1 changed to 0

        int depth = 0;
        CoordCube.setPruning(Slice_Twist_Prun, 0, 0);
        int done = 1;
        while (done != CoordCube.N_SLICE1 * CoordCube.N_TWIST)
        {
            for (int i = 0; i < CoordCube.N_SLICE1 * CoordCube.N_TWIST; i++)
            {
                int twist = i / CoordCube.N_SLICE1, slice = i % CoordCube.N_SLICE1;
                if (CoordCube.getPruning(Slice_Twist_Prun, i) == depth)
                {
                    for (int j = 0; j < 18; j++)
                    {
                        int newSlice = FRtoBRClass.FRtoBR_Move[slice * 24, j] / 24;
                        int newTwist = TwistMoveClass.twistMove[twist, j];
                        if (CoordCube.getPruning(Slice_Twist_Prun, CoordCube.N_SLICE1 * newTwist + newSlice) == 0x0f)
                        {
                            CoordCube.setPruning(Slice_Twist_Prun, CoordCube.N_SLICE1 * newTwist + newSlice, (depth + 1));
                            done++;
                        }
                    }
                }
            }
            depth++;
        }
    }
}
