using Backend;
using ColorThings;
using UnityEngine;

public class ScanedFace : MonoBehaviour
{
    public Face Face;
    //public CreateRubikSides crs;
   
   /*
    public ColorThings.RubicColors[] orange;
    public ColorThings.RubicColors[] blue;
    public ColorThings.RubicColors[] red;
    public ColorThings.RubicColors[] green;
    public ColorThings.RubicColors[] white;
    public ColorThings.RubicColors[] yellow;
    */



    // Use this for initialization
    void Start()
    {
        Face = new Face();
      
      /*
        orange = new ColorThings.RubicColors[8];
        blue = new ColorThings.RubicColors[8];
        red = new ColorThings.RubicColors[8];
        green = new ColorThings.RubicColors[8];
        white = new ColorThings.RubicColors[8];
        yellow = new ColorThings.RubicColors[8];
       */
    }

    public void Redraw()
    {
        for (int i = 0; i < Face.Colors.Count; i++)
        {
            SetColor("Pice" + i, Face.Colors[i], true);
            
            //Debug.Log(Face.Colors[i]);                                           //hej här skrev jag
        /*
            
            if( Face.MiddleColor == ColorThings.RubicColors.Orange) {              //Back = orange
                //Debug.Log(Face.Colors[i]);
                orange[i] = Face.Colors[i];
            }
            
            if( Face.MiddleColor == ColorThings.RubicColors.Blue) {              //Front = blue
                //Debug.Log(Face.Colors[i]);
                blue[i] = Face.Colors[i];
            }
            
            if( Face.MiddleColor == ColorThings.RubicColors.Red) {              //Right = red
                //Debug.Log(Face.Colors[i]);
                red[i] = Face.Colors[i];
            }
            
            if( Face.MiddleColor == ColorThings.RubicColors.Green) {              //Top = green
                //Debug.Log(Face.Colors[i]);
                green[i] = Face.Colors[i];
            }
            
            if( Face.MiddleColor == ColorThings.RubicColors.White) {              //Left = white
                //Debug.Log(Face.Colors[i]);
                white[i] = Face.Colors[i];
            }
            
            if( Face.MiddleColor == ColorThings.RubicColors.Yellow) {              //Bottom = yellow
                //Debug.Log(Face.Colors[i]);
                yellow[i] = Face.Colors[i];
            }
         
        */
        
        }

        SetColor("Middle", Face.MiddleColor, true);
        SetColor("TopColor", Face.TopColor, true);
        
    }
    
   /* public void SendSides() {
        new CreateRubikSides(yellow, blue, white, orange, red, green);
    } */
    
    public void Reset()
    {
        for (int i = 0; i < Face.Colors.Count; i++)
        {
            SetColor("Pice" + i, Face.Colors[i], false);
        }
        SetColor("Middle", Face.MiddleColor, true);
        
        //Debug.Log("hejhejehejhejeheh");
        //crs = new CreateRubikSides(yellow, blue, white, orange, red, green);    //huthuthut
    }

    private void SetColor(string name, RubicColors rubicColor, bool shouldBeEnabled)
    {
        var col = ColorDetection.UnityColorFromEnum(rubicColor);
        var obj = transform.Find(name);
        var render = obj.GetComponent<Renderer>();

        render.enabled = shouldBeEnabled;

        render.material.color = col;
    }
}
