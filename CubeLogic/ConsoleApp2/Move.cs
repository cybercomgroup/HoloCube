using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{

    public enum CubeAction
    {
        Up, UpI, Down, DownI, Front, FrontI, Right, RightI, Left, LeftI
    }

    public class Move
    {
        

        public CubeAction Action;

        public Move(CubeAction action)
        {
            this.Action = action;
        }

        public override String ToString()
        {
            return Action.ToString() + " 90 degrees.";
        }
        
    }
}
