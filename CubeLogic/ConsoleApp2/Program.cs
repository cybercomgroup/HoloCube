using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Solve();
        }

        private static void Solve()
        {
            RubikCube cube = new RubikCube();
            cube.Scramble();
            List<Move> moves = Solver.Solve(cube);
            Console.WriteLine(cube);
            Console.WriteLine("\nTotal number of moves: " + moves.Count);
            /*foreach(Move move in moves)
            {
                Console.WriteLine(move);
            }*/
            Console.ReadKey();
        }
        
    }
}
