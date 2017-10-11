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
            switch(move.Action)
            {
                case CubeAction.U:
                    return new Move(CubeAction.UI, move.FrontColor, move.TopColor);
                case CubeAction.UI:
                    return new Move(CubeAction.U, move.FrontColor, move.TopColor);
                case CubeAction.L:
                    return new Move(CubeAction.LI, move.FrontColor, move.TopColor);
                case CubeAction.LI:
                    return new Move(CubeAction.L, move.FrontColor, move.TopColor);
                case CubeAction.F:
                    return new Move(CubeAction.FI, move.FrontColor, move.TopColor);
                case CubeAction.FI:
                    return new Move(CubeAction.F, move.FrontColor, move.TopColor);
                case CubeAction.R:
                    return new Move(CubeAction.RI, move.FrontColor, move.TopColor);
                case CubeAction.RI:
                    return new Move(CubeAction.R, move.FrontColor, move.TopColor);
                case CubeAction.B:
                    return new Move(CubeAction.BI, move.FrontColor, move.TopColor);
                case CubeAction.BI:
                    return new Move(CubeAction.B, move.FrontColor, move.TopColor);
                case CubeAction.D:
                    return new Move(CubeAction.DI, move.FrontColor, move.TopColor);
                case CubeAction.DI:
                    return new Move(CubeAction.D, move.FrontColor, move.TopColor);
                case CubeAction.M:
                    return new Move(CubeAction.MI, move.FrontColor, move.TopColor);
                case CubeAction.MI:
                    return new Move(CubeAction.M, move.FrontColor, move.TopColor);
                case CubeAction.E:
                    return new Move(CubeAction.EI, move.FrontColor, move.TopColor);
                case CubeAction.EI:
                    return new Move(CubeAction.E, move.FrontColor, move.TopColor);
                case CubeAction.S:
                    return new Move(CubeAction.SI, move.FrontColor, move.TopColor);
                case CubeAction.SI:
                    return new Move(CubeAction.S, move.FrontColor, move.TopColor);
                case CubeAction.X:
                    return new Move(CubeAction.XI, move.FrontColor, move.TopColor);
                case CubeAction.XI:
                    return new Move(CubeAction.X, move.FrontColor, move.TopColor);
                case CubeAction.Y:
                    return new Move(CubeAction.YI, move.FrontColor, move.TopColor);
                case CubeAction.YI:
                    return new Move(CubeAction.Y, move.FrontColor, move.TopColor);
                case CubeAction.Z:
                    return new Move(CubeAction.ZI, move.FrontColor, move.TopColor);
                case CubeAction.ZI:
                    return new Move(CubeAction.Z, move.FrontColor, move.TopColor);
                default:
                    return null;
            }

        }

        public override String ToString()
        {
            return "With " + TopColor + " on top and " + FrontColor + " in front, " + Action.ToString() + " 90 degrees.";
        }
        
    }
}
