public class Slice_URtoDF_Parity_PrunClass {

    // Pruning table for the permutation of the edges in phase2.
    // The pruning table entries give a lower estimation for the number of moves to reach the solved cube.
    public static int[] Slice_URtoDF_Parity_Prun = new int[CoordCube.N_SLICE2 * CoordCube.N_URtoDF * CoordCube.N_PARITY / 2];
    static Slice_URtoDF_Parity_PrunClass()
    {
        for (int i = 0; i < CoordCube.N_SLICE2 * CoordCube.N_URtoDF * CoordCube.N_PARITY / 2; i++)
            Slice_URtoDF_Parity_Prun[i] = -1;   //SHOULD BE -1 changed to 0
        int depth = 0;
        CoordCube.setPruning(Slice_URtoDF_Parity_Prun, 0, 0);
        int done = 1;
        while (done != CoordCube.N_SLICE2 * CoordCube.N_URtoDF * CoordCube.N_PARITY)
        {
            for (int i = 0; i < CoordCube.N_SLICE2 * CoordCube.N_URtoDF * CoordCube.N_PARITY; i++)
            {
                int parity = i % 2;
                int URtoDF = (i / 2) / CoordCube.N_SLICE2;
                int slice = (i / 2) % CoordCube.N_SLICE2;
                if (CoordCube.getPruning(Slice_URtoDF_Parity_Prun, i) == depth)
                {
                    for (int j = 0; j < 18; j++)
                    {
                        switch (j)
                        {
                            case 3:
                            case 5:
                            case 6:
                            case 8:
                            case 12:
                            case 14:
                            case 15:
                            case 17:
                                continue;
                            default:
                                int newSlice = FRtoBRClass.FRtoBR_Move[slice, j];
                                int newURtoDF = URtoDFClass.URtoDF_Move[URtoDF, j];
                                int newParity = CoordCube.parityMove[parity, j];
                                if (CoordCube.getPruning(Slice_URtoDF_Parity_Prun, (CoordCube.N_SLICE2 * newURtoDF + newSlice) * 2 + newParity) == 0x0f)
                                {
                                    CoordCube.setPruning(Slice_URtoDF_Parity_Prun, (CoordCube.N_SLICE2 * newURtoDF + newSlice) * 2 + newParity,
                                    (depth + 1));
                                    done++;
                                }
                                break;
                        }
                    }
                }
            }
            depth++;
        }
    }
}
