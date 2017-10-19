using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
//          Solve();
            Translator t = new Translator();
            var cubeAndFaceDict = t.setSides();
            var cube = cubeAndFaceDict.Item1;
            var faces = cubeAndFaceDict.Item2;
//            int zRotations;

            foreach (CubeSide side in faces.Keys)
            {
                var zRotations = cube.rotateCubeToChosenColor(faces[side].MiddleColor, faces[side].TopColor);
                cube.rotateCubeToChosenColor(CubeColor.White, CubeColor.Blue);
                faces[side].RotateColorsClockwise(zRotations);
             }

            foreach (var kvp in faces  )
            {
                Console.WriteLine("\nFaceColor: {0} FaceKey: {1} FaceTopColor {2}", kvp.Value.MiddleColor, kvp.Key, kvp.Value.TopColor);
                foreach (var color in kvp.Value.Colors)
                {
                    Console.WriteLine(color);
                }
                
            }
            Console.WriteLine();
            

           /* Console.WriteLine("\n Before ///////////// \n");
            Console.WriteLine(cube.ToString());

            for (int i = 0; i < faces[CubeSide.Front].Colors.Count; i++)
            {
                Console.WriteLine(faces[CubeSide.Front].Colors[i]);
            }

            Console.WriteLine("TopColor of frontface: " + faces[CubeSide.Front].TopColor + "\n");

            Console.WriteLine("Rotating... \n");
            try
            {
                zRotations = cube.rotateCubeToChosenColor(CubeColor.White, CubeColor.Green);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            faces[CubeSide.Front].RotateColorsClockwise(zRotations);

            Console.WriteLine("After /////////////\n");
            for (int i = 0; i < faces[CubeSide.Front].Colors.Count; i++)
            {
                Console.WriteLine(faces[CubeSide.Front].Colors[i]);
            }
            Console.WriteLine(cube.ToString());*/

            //            cube.ExecuteMove(new Move(CubeAction.U,CubeColor.White,CubeColor.Blue));
            //            cube.ExecuteMove(new Move(CubeAction.F,CubeColor.White,CubeColor.Blue));
        }


        private static void Solve()
        {
            RubikCube cube = new RubikCube();

            cube.Scramble();
            List<Move> moves = Solver.Solve(cube);
            Console.WriteLine(cube);
            Console.WriteLine("\nTotal number of moves: " + moves.Count + "\n");
            foreach (Move move in moves)
            {
                Console.WriteLine(move);
            }
            Console.ReadKey();
        }
    }
}