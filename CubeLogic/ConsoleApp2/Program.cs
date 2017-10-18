using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
//            Solve();
        /*    Translator t = new Translator();
            var cube = t.setSides();*/
            var face = new Face();
            face.Colors = new List<CubeColor>
            {
                CubeColor.Blue,
                CubeColor.Yellow,
                CubeColor.Green,
                CubeColor.Orange,
                CubeColor.Yellow,
                CubeColor.White,
                CubeColor.White,
                CubeColor.Green,
            };
            
            face.RotateColorsClockwise(3);
           
            


            /* Console.WriteLine("Before /////////////");
             Console.WriteLine(cube.ToString());
             try
             {
                 cube.rotateCubeToChosenColor(CubeColor.Orange, CubeColor.White);
             }
             catch (Exception e)
             {
                 Console.WriteLine(e);
                 return;
             }
 
             
             
             
 //            cube.ExecuteMove(new Move(CubeAction.U,CubeColor.White,CubeColor.Blue));
 //            cube.ExecuteMove(new Move(CubeAction.F,CubeColor.White,CubeColor.Blue));
 
             Console.WriteLine("After /////////////");
             
             Console.WriteLine(cube.ToString());
 */

        }

        private static void Solve()
        {
            RubikCube cube = new RubikCube();
           
            cube.Scramble();
            List<Move> moves = Solver.Solve(cube);
            Console.WriteLine(cube);
            Console.WriteLine("\nTotal number of moves: " + moves.Count + "\n");
            foreach(Move move in moves)
            {
                Console.WriteLine(move);
            }
            Console.ReadKey();
        }
        
    }
}
