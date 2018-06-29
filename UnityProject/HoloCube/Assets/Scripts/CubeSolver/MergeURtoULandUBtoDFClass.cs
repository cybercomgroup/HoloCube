public class MergeURtoULandUBtoDFClass {

    // Table to merge the coordinates of the UR,UF,UL and UB,DR,DF edges at the beginning of phase2
	public static short[,] MergeURtoULandUBtoDF = new short[336, 336];
    static MergeURtoULandUBtoDFClass()
    {
		// for i, j <336 the six edges UR,UF,UL,UB,DR,DF are not in the
		// UD-slice and the index is <20160
		for (short uRtoUL = 0; uRtoUL< 336; uRtoUL++)
        {
			for (short uBtoDF = 0; uBtoDF< 336; uBtoDF++)
            {
				MergeURtoULandUBtoDF[uRtoUL, uBtoDF] = (short) CubieCube.getURtoDF(uRtoUL, uBtoDF);
			}
		}
	}
}
