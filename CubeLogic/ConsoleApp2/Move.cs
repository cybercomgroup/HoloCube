using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{

    /// <summary>
    /// All possible cube rotations.
    /// </summary>
    public enum CubeAction
    {
        // Face rotations
        U, UI,
        L, LI,
        F, FI,
        R, RI,
        B, BI,
        D, DI,
        // Slice rotations
        M, MI,
        E, EI,
        S, SI,
        // Whole Cube
        X, XI,
        Y, YI,
        Z, ZI
    }

    /// <summary>
    /// Represents a move performed on the cube.
    /// </summary>
    public class Move
    {
        public CubeAction Action;
        public CubeColor FrontColor;
        public CubeColor TopColor;

        public Move(CubeAction action, CubeColor frontColor, CubeColor topColor)
        {
            this.Action = action;
            this.FrontColor = frontColor;
            this.TopColor = topColor;
        }

        /// <summary>
        /// Trims a list of moves by replacing sequencees of three equal moves
        /// with the corresponding inverse.
        /// </summary>
        /// <param name="moves">List of moves.</param>
        /// <returns>A trimmed version of the original list.</returns>
        public static List<Move> Trim(List<Move> moves)
        {
            List<Move> trimmed = new List<Move>();
            Move latestMove = null;
            int seq = 0;
            foreach(Move move in moves)
            {
                if(latestMove == null)
                {
                    seq++;
                    latestMove = move;
                }
                else if (latestMove.Action == move.Action)
                {
                    if(seq == 3)
                    {
                        trimmed.Add(InvertedMove(move));
                        seq = 0;
                        latestMove = null;
                    }
                }
                else
                {
                    trimmed.Add(latestMove);
                    trimmed.Add(move);
                    seq = 0;
                }
            }

            return trimmed;
        }

        /// <summary>
        /// Finds the inverse of every CubeAction.
        /// </summary>
        /// <param name="move">Move to be inverted.</param>
        /// <returns>A move with the inverted CubeAction.</returns>
        public static Move InvertedMove(Move move)
        {
            string ms = move.ToString();
            if(ms.Length == 1)
            {
                return new Move((CubeAction)Enum.Parse(typeof(CubeAction), ms + 'I'), move.FrontColor, move.TopColor);
            }
            else
            {
                return new Move((CubeAction)Enum.Parse(typeof(CubeAction), ms.Substring(0,1)), move.FrontColor, move.TopColor);
            }
        }

        public override String ToString()
        {
            return "With " + TopColor + " on top and " + FrontColor + " in front, " + Action.ToString() + " 90 degrees.";
        }
        
    }
}
