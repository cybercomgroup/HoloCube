public class Slice_Flip_PrunClass {

    // Pruning table for the flip of the edges and the position (not permutation) of the UD-slice edges in phase1
    // The pruning table entries give a lower estimation for the number of moves to reach the H-subgroup.
    public static int[] Slice_Flip_Prun = new int[CoordCube.N_SLICE1 * CoordCube.N_FLIP / 2]; // 495, 1024 = 506 880
    static Slice_Flip_PrunClass()
    {
        for (int i = 0; i < CoordCube.N_SLICE1 * CoordCube.N_FLIP / 2; i++)
            Slice_Flip_Prun[i] = -1;
        int depth = 0;

        CoordCube.setPruning(Slice_Flip_Prun, 0, 0);
        int done = 1;
        while (done != CoordCube.N_SLICE1 * CoordCube.N_FLIP)
        {
            for (int i = 0; i < CoordCube.N_SLICE1 * CoordCube.N_FLIP; i++)
            {
                int flip = i / CoordCube.N_SLICE1, slice = i % CoordCube.N_SLICE1; 
                if (CoordCube.getPruning(Slice_Flip_Prun, i) == depth)         
                {
                    for (int j = 0; j < 18; j++)
                    {
                        int newSlice = FRtoBRClass.FRtoBR_Move[slice * 24, j] / 24;
                        int newFlip = FlipMoveClass.flipMove[flip, j];
                        if (CoordCube.getPruning(Slice_Flip_Prun, CoordCube.N_SLICE1 * newFlip + newSlice) == 0x0f)
                        {
                            CoordCube.setPruning(Slice_Flip_Prun, CoordCube.N_SLICE1 * newFlip + newSlice, (depth + 1));
                            done++;
                        }
                    }
                }
            }
            depth++;
        }
    }
}
