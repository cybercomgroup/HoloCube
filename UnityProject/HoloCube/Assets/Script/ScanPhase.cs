using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Backend;


public class ScanPhase : MonoBehaviour
{
private int _progress;
private Dictionary<string, List<Color>> sideMap;
private Dictionary<string, List<string>> PlaneIDs;
// private CubeSide asd; 
private List<Color> Instructions;
private List<string> WhiteSideID;
private List<string> OrangeSideID;
private List<string> TheSixColors;
private System.Random random;
PredefinedColors preDef;
private Animator anim;
private TextMesh instruction;
private Translator ts;

// Use this for initialization
void Start()
{

    preDef = new PredefinedColors();
    ///
    /// Init Instructionscene, sets sideMap #0, #1, #4 mid colors.
    /// _progess = 0
    /// 
    anim = GetComponent<Animator>();
    PlaneIDs = new Dictionary<string, List<string>>();
    Instructions = new List<Color>();
    sideMap = new Dictionary<string, List<Color>>();
    TheSixColors = new List<string>();

    random = new System.Random();


    string[] colorArray = { "F", "R", "B", "L", "U", "D" };
    TheSixColors.AddRange(colorArray);
    Color[] InstructionColors = { Color.white, new Color(1, 0.5f, 0), Color.yellow, Color.red, Color.green, Color.blue };
    Instructions.AddRange(InstructionColors);
    //sideMap.Add("white", null);
    //sideMap.Add("orange", null);
    string[] planeIdentities = { "W", "O", "Y", "R", "G", "B" };

    //print("PlaneIDS[white] pre list: " + PlaneIDs["white]"].Count);
    List<string> hvit = new List<string>();
    string[] hvitS = { "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9" };
    hvit.AddRange(hvitS);
    PlaneIDs["F"] = hvit;

    List<string> oransh = new List<string>();
    string[] oranshS = { "R1", "R2", "R3", "R4", "R5", "R6", "R7", "R8", "R9" };
    oransh.AddRange(oranshS);
    PlaneIDs["R"] = oransh;

    List<string> guhl = new List<string>();
    string[] guhlS = { "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9" };
    guhl.AddRange(guhlS);
    PlaneIDs["B"] = guhl;

    List<string> roehd = new List<string>();
    string[] roehdS = { "L1", "L2", "L3", "L4", "L5", "L6", "L7", "L8", "L9" };
    roehd.AddRange(roehdS);
    PlaneIDs["L"] = roehd;

    List<string> groen = new List<string>();
    string[] groenS = { "U1", "U2", "U3", "U4", "U5", "U6", "U7", "U8", "U9" };
    groen.AddRange(groenS);
    PlaneIDs["U"] = groen;

    List<string> blaa = new List<string>();
    string[] blaaS = { "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9" };
    blaa.AddRange(blaaS);
    PlaneIDs["D"] = blaa;

    ColorTracker.Instance.Start();
    //PlaneIDs["orange"] = planeIdentities2;

    ListResetter();

    instruction = GameObject.Find("instruktionfarg").GetComponent<TextMesh>();
    instruction.text = "the white side";
}

private void OnMouseDown()
{
    Destroy(GameObject.Find("targetArea"));
}



public void SetColor(List<Color> ls)
{

}

private Color ColorFromString(string s)
{
    switch (s)
    {
        case "black":
            return Color.black;
        case "white":
            return Color.white;
        case "red":
            return Color.red;
        case "yellow":
            return Color.yellow;
        case "blue":
            return Color.blue;
        case "orange":
            return new Color(1, 0.5f, 0);
        default:
            return Color.clear;
    }
}
void ListResetter()
{
    int i = 0;

    foreach (string s in TheSixColors)
    {
        List<Color> greyList = new List<Color>();
        for (int j = 0; j < 9; j++)
        {
            greyList.Add(Color.grey);
        }

        greyList[4] = Instructions[i];
        sideMap.Add(s, greyList);
        i++;
    }

}

void Colorizer(string color)
{
    List<Color> Colors = new List<Color>();
    Colors = sideMap[color];
    int i = 0;
    foreach (string idx in PlaneIDs[color])
    {
        var obj = GameObject.Find(idx);
        var colorRender = obj.GetComponent<Renderer>();
        colorRender.enabled = true;
        colorRender.material.color = Colors[i];
        i++;
    }
}
// Update is called once per frame
void Update()
{
    if (_progress == 6)
    {
        for (int j = 0; j < 6; j++)
        {
            foreach (string id in PlaneIDs[TheSixColors[j]])
            {
                var obj = GameObject.Find(id);
                var _render = obj.GetComponent<Renderer>();
                _render.enabled = false;
            }
        }
        //ColorTracker.Instance.SendToTranslate(); Få ut colors i stringformat(skicka direkt till solvern)
        Dictionary<string, Color> cDict = new Dictionary<string, Color>();        
            foreach (List<string> ls in PlaneIDs.Values)
        {
            foreach (string s in ls)
            {
                cDict.Add(s, GameObject.Find(s).GetComponent<Renderer>().material.color);
            }
        }
        ColorTracker.Instance.SavePlaneIDsAndCorrespondingColor(cDict);
        SceneManager.LoadScene("CubeVerifyThenSolve");
       // DisplayMat.webcamTexture.Stop();
        }
                

            
        
        


    

        
    int i = 0;
    List<Color> tempList = null;
    /*  if (ColorTracker.Instance.hasTempListChanged())
    {
        ColorTracker.Instance.setTempListBool(false);*/
        tempList = ColorTracker.Instance.GetTempList();
        if (tempList != null && tempList.Count > 8)
        {
            foreach (string id in PlaneIDs[TheSixColors[_progress]])
            {
                var obj = GameObject.Find(id);
                var _render = obj.GetComponent<Renderer>();
                _render.enabled = true;
                _render.material.color = tempList[i];
                // Debug.Log("Tried to set GameObject : " + id + " To color : " + tempList[i].ToString());
                i++;
            }
        }
        
        

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (ColorTracker.Instance.saveTempColorsToPerm(TheSixColors[_progress])) { 
                        int y = 0;
                        List<Color> c1 = new List<Color>();
                        c1 = ColorTracker.Instance.getPermList(TheSixColors[_progress]);

                            if (c1 != null)
                                {
                                foreach (string id in PlaneIDs[TheSixColors[_progress]])
                                    {
                                        var obj = GameObject.Find(id);
                                        var _render = obj.GetComponent<Renderer>();
                                        _render.enabled = true;
                                        _render.material.color = c1[y];
                                        y++;
                                            }
                                    }
                        ColorTracker.Instance.tempColors.Clear();
                                }
        anim.SetTrigger(_progress.ToString());
        _progress = ColorTracker.Instance.getProgress();
        instruction.text = "the " + TheSixColors[_progress].ToString() + " side";
        instruction.color = ColorFromString(TheSixColors[_progress]);
        Debug.Log("_progress variable set to " + _progress);
                }

            }
}


