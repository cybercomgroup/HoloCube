﻿using Backend;

public class Cube
{
    public Face Front { get; set; }
    public Face Back { get; set; }
    public Face Left { get; set; }
    public Face Right { get; set; }
    public Face Top { get; set; }
    public Face Bottom { get; set; }


    public Cube()
    {
        Front = new Face();
        Top = new Face();
        Left = new Face();
        Right = new Face();
        Back = new Face();
        Bottom = new Face();
    }
}