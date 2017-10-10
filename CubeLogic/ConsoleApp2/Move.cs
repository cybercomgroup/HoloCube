using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{

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

    public class Move
    {
        public CubeAction Action;

        public Move(CubeAction action)
        {
            this.Action = action;
        }

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

        public static Move InvertedMove(Move move)
        {
            switch(move.Action)
            {
                case CubeAction.U:
                    return new Move(CubeAction.UI);
                case CubeAction.UI:
                    return new Move(CubeAction.U);
                case CubeAction.L:
                    return new Move(CubeAction.LI);
                case CubeAction.LI:
                    return new Move(CubeAction.L);
                case CubeAction.F:
                    return new Move(CubeAction.FI);
                case CubeAction.FI:
                    return new Move(CubeAction.F);
                case CubeAction.R:
                    return new Move(CubeAction.RI);
                case CubeAction.RI:
                    return new Move(CubeAction.R);
                case CubeAction.B:
                    return new Move(CubeAction.BI);
                case CubeAction.BI:
                    return new Move(CubeAction.B);
                case CubeAction.D:
                    return new Move(CubeAction.DI);
                case CubeAction.DI:
                    return new Move(CubeAction.D);
                case CubeAction.M:
                    return new Move(CubeAction.MI);
                case CubeAction.MI:
                    return new Move(CubeAction.M);
                case CubeAction.E:
                    return new Move(CubeAction.EI);
                case CubeAction.EI:
                    return new Move(CubeAction.E);
                case CubeAction.S:
                    return new Move(CubeAction.SI);
                case CubeAction.SI:
                    return new Move(CubeAction.S);
                case CubeAction.X:
                    return new Move(CubeAction.XI);
                case CubeAction.XI:
                    return new Move(CubeAction.X);
                case CubeAction.Y:
                    return new Move(CubeAction.YI);
                case CubeAction.YI:
                    return new Move(CubeAction.Y);
                case CubeAction.Z:
                    return new Move(CubeAction.ZI);
                case CubeAction.ZI:
                    return new Move(CubeAction.Z);
                default:
                    return null;
            }

        }

        public override String ToString()
        {
            return Action.ToString() + " 90 degrees.";
        }
        
    }
}
