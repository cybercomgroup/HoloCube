using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public class Solver
    {
        public static List<Move> Solve(RubikCube cube)
        {
            List<Move> moves = new List<Move>();
            moves.AddRange(WhiteCross(cube));
            return moves;
        }

        private static List<Move> WhiteCross(RubikCube cube)
        {
            List<Move> moves = new List<Move>();
            ExecuteMove(cube, CubeAction.Up, moves);
            ExecuteMove(cube, CubeAction.Down, moves);
            ExecuteMove(cube, CubeAction.Left, moves);
            return moves;
        }

        private static void ExecuteMove(RubikCube cube, CubeAction action, List<Move> moves)
        {
            Move move = new Move(action);
            cube.ExecuteMove(move);
            moves.Add(move);
        }
        
    }
}
