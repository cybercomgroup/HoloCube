using Backend;

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
        Front = new Face(Faces.Front);
        Top = new Face(Faces.Top);
        Left = new Face(Faces.Left);
        Right = new Face(Faces.Right);
        Back = new Face(Faces.Back);
        Bottom = new Face(Faces.Bottom);
    }

    public void MoveFace(bool anticlock, Face face)
    {
        if (anticlock) MoveFaceAntiClockvise(face);
        else
        {
            MoveFaceClockvise(face);
        }
    }

    public void MoveFace(Face mainFace, Face right, Face left, Face top, Face bottom)
    {
        MoveFaceClockvise(mainFace);

        var temp = right.Colors[0, 0];
        var temp1 = right.Colors[0, 1];
        var temp2 = right.Colors[0, 2];

        right.Colors[0, 0] = top.Colors[0, 2];
        right.Colors[0, 1] = top.Colors[1, 2];
        right.Colors[0, 2] = top.Colors[2, 2];

        top.Colors[0, 2] = left.Colors[2, 0];
        top.Colors[1, 2] = left.Colors[2, 1];
        top.Colors[2, 2] = left.Colors[2, 2];

        left.Colors[2, 0] = bottom.Colors[0,0];
        left.Colors[2, 1] = bottom.Colors[1,0];
        left.Colors[2, 2] = bottom.Colors[2,0];
        
        bottom.Colors[0,0] = temp;
        bottom.Colors[1,0] = temp1;
        bottom.Colors[2,0] = temp2;
        
//        bottom.Colors[0, 0] = temp;
//        bottom.Colors[0, 1] = temp1;
//        bottom.Colors[0, 2] = temp2;
    }

    private void MoveFaceAntiClockvise(Face face)
    {
        Move(face);
        Move(face);
        Move(face);
    }

    private void MoveFaceClockvise(Face face)
    {
        Move(face);
    }

    private void Move(Face face)
    {
        var mat = face.Colors;
        for (int y = 0; y < 2; y++)
        {
            var temp = mat[0, y];
            mat[0, y] = mat[y, 2];
            mat[y, 2] = mat[2, 2 - y];
            mat[2, 2 - y] = mat[2 - y, 0];
            mat[2 - y, 0] = temp;
        }
    }
}