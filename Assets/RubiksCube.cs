using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using StandardRK;
using UnityEditor;


public partial class RubiksCube : MonoBehaviour
{
    public GameObject cube_prefab;
    private GameObject[] cubes;
    private GameObject clickedGameObject1 = null;
    private Vector3 mouseClick1;
    private Vector3 mouseClick2;
    private bool WaitRotation;
    private GameObject MainCamera;
    private int RandomIndex = -1;
    private GameObject[,,] RK;
    private RubiksCubeColorMap RK_col;
    private List<string> GameLog = new List<String>();
    private List<string> SolveScript = new List<String>();
    public GameObject LogPanel;
    private LogScript logscript;
    private DebugScript debugscript;
    private InputScript inputscript;
    private int AutoModeStage = 0;
    private string DebugKeyword = "";

    System.Random r1 = new System.Random();
    // Start is called before the first frame update
    private enum AutoMode
    {
        None,
        AutoResolveMode,
        AutoSequenceMode,
        Random10Mode,
    }
    AutoMode isAutoMode = AutoMode.None;

    void Start()
    {
        logscript = LogPanel.GetComponent<LogScript>();
        debugscript = LogPanel.GetComponent<DebugScript>();
        inputscript = LogPanel.GetComponent<InputScript>();
        isAutoMode = AutoMode.None;
        if (cubes != null)
        {
            for (int n = 0; n < 125; n++)
            {
                Destroy(cubes[n], 0f);
            }
        }
        cubes = new GameObject[125];
        DebugKeyword = "";
        try{
            RK = new GameObject[5,5,5];
            RK_col = new RubiksCubeColorMap();
            WaitRotation = false;
            MainCamera = Camera.main.gameObject;
            int ind = 0;
            for (int x = -2; x <= 2; x++)
            {
                for (int y = -2; y <= 2; y++)
                {
                    for (int z = -2; z <= 2; z++)
                    {
                        cubes[ind] = Instantiate(cube_prefab, new Vector3(x, y, z), Quaternion.identity);
                        cubes[ind].GetComponent<CubeRotation>().Initialize();
                        RK[x+2,y+2,z+2] = cubes[ind];
                        ind++;
                    }
                }
            }
            SetRK();
            ClearLog();
            debug();
        }   
        catch (System.Exception e)
        {
            EmergencyStop("System mulfunction at main");
            Debug.Log(e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EmergencyStop("Stop by Return key");
        }
        if (!WaitRotation)
        {
            if (isAutoMode == AutoMode.AutoSequenceMode
             || isAutoMode == AutoMode.AutoResolveMode)
            {
                if (SolveScript.Any())
                {
                    RotateCube_Command(SolveScript[0]);
                    SolveScript.RemoveAt(0);
                }
                else if (isAutoMode == AutoMode.AutoSequenceMode)
                {
                    if (AutoModeStage == 0)
                    {
                        isAutoMode = AutoMode.None;
                    }
                    else if (AutoModeStage == 1)
                    {
                        Auto1CallBack();
                    }
                    else if (AutoModeStage == 2)
                    {
                        Auto2CallBack();
                    }
                    else if (AutoModeStage == 3)
                    {
                        Auto3CallBack();
                    }
                    else if (AutoModeStage == 4)
                    {
                        Auto4CallBack();
                    }
                    else if (AutoModeStage == 5)
                    {
                        Auto5CallBack();
                    }
                    else if (AutoModeStage == 6)
                    {
                        Auto6CallBack();
                    }
                    else if (AutoModeStage == 7)
                    {
                        Auto7CallBack();
                    }
                    else if (AutoModeStage == 8)
                    {
                        Auto8CallBack();
                    }
                    else if (AutoModeStage == 9)
                    {
                        Auto9CallBack();
                    }
                    else
                    {
                        EmergencyStop("Auto Mode Stage Error");
                    }
                }
                else
                {
                    isAutoMode = AutoMode.None;
                }
            }
            else if (isAutoMode == AutoMode.Random10Mode)
            {
                if (RandomIndex > 0)
                {
                    int randNext = r1.Next(0,3);
                    RotateCube( // randomize rotate for x, y or z + 90 or -90 deg
                        ((randNext==0)?Vector3.right:((randNext==1)?Vector3.up:Vector3.forward)),
                        r1.Next(-2,3),  // either of -2, -1, 0, 1, 2
                        90 * r1.Next(1,3) ); // either of 90, 180
                    RandomIndex--;
                }
                else
                {
                    isAutoMode = AutoMode.None;
                }
            }
            else if (Input.GetMouseButtonDown(0)) 
            {    
                mouseClick1 = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mouseClick1);
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit, MainCamera.transform.position.magnitude))
                {
                    clickedGameObject1 = hit.collider.gameObject;
                }
            }
            else if (Input.GetMouseButtonUp(0) && clickedGameObject1 != null)
            {
                mouseClick2 = Input.mousePosition;
                Vector3 diff = mouseClick2 - mouseClick1;
                Vector3 worldMov;
                if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
                {
                    worldMov = Camera.main.transform.TransformDirection((diff.x > 0) ? Vector3.down : Vector3.up);
                    // When mouse moves to right, the rotation axis should be vertical
                    // Convert camera's vertical direction vector to global world coordinate
                }
                else
                {
                    worldMov = Camera.main.transform.TransformDirection((diff.y > 0) ? Vector3.right : Vector3.left);
                    // When mouse moves to up, the rotation axis should be horizontal
                    // Convert camera's horizontal direction vector to global world coordinate
                }
                worldMov.Normalize();
                float[] XYZdot = new float[3];
                XYZdot[0] = Vector3.Dot(worldMov, Vector3.right);
                XYZdot[1] = Vector3.Dot(worldMov, Vector3.up);
                XYZdot[2] = Vector3.Dot(worldMov, Vector3.forward);
                Vector3 rotAxis = new Vector3(0,0,0);
                int rotangle=0, linenum = 0;
                bool rotateFlag = true;
                if (diff.magnitude < 30)
                {
                    rotateFlag = false;
                }
                else if (Mathf.Abs(XYZdot[0]) > Mathf.Abs(XYZdot[1])/1.5f && Mathf.Abs(XYZdot[0]) > Mathf.Abs(XYZdot[2])/1.5f)
                {
                    rotangle = (XYZdot[0] > 0) ? 90 : -90;
                    rotAxis = new Vector3(1, 0, 0);
                    linenum = Mathf.RoundToInt(clickedGameObject1.transform.position.x);
                }
                else if (Mathf.Abs(XYZdot[1]) > Mathf.Abs(XYZdot[0])/1.5f && Mathf.Abs(XYZdot[1]) > Mathf.Abs(XYZdot[2])/1.5f)
                {
                    rotangle = (XYZdot[1] > 0) ? 90 : -90;
                    rotAxis = new Vector3(0, 1, 0);
                    linenum = Mathf.RoundToInt(clickedGameObject1.transform.position.y);

                }
                else if (Mathf.Abs(XYZdot[2]) > Mathf.Abs(XYZdot[0])/1.5f && Mathf.Abs(XYZdot[2]) > Mathf.Abs(XYZdot[1])/1.5f)
                {
                    rotangle = (XYZdot[2] > 0) ? 90 : -90;
                    rotAxis = new Vector3(0, 0, 1);
                    linenum = Mathf.RoundToInt(clickedGameObject1.transform.position.z);
                }
                else
                {
                    rotateFlag = false;
                }
                if (rotateFlag)
                {
                    RotateCube(rotAxis, linenum, rotangle);
                }
                clickedGameObject1 = null;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                debug();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                AutoModeStage = 1;
                Auto1CallBack();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                AutoModeStage = 2;
                Auto2CallBack();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                AutoModeStage = 3;
                Auto3CallBack();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                AutoModeStage = 4;
                Auto4CallBack();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                AutoModeStage = 5;
                Auto5CallBack();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                AutoModeStage = 6;
                Auto6CallBack();
            }
        }
        else
        {
            bool tmp_isRot = false;
            foreach (GameObject cube in cubes)
            {
                CubeRotation sc = cube.GetComponent<CubeRotation>();
                if (sc.isRotating)
                {
                    tmp_isRot = true; // still rotating
                    break;
                }
            }
            WaitRotation = tmp_isRot;
            if (!tmp_isRot)
            {
                // Finish Rotation
                SetRK();
            }
            debug();
        }
    }

    private void EmergencyStop(string inputstr)
    {
        SolveScript.Clear();
        isAutoMode = AutoMode.None;
        AutoModeStage = 0;
        Debug.Log(inputstr);
        //EditorUtility.DisplayDialog("Error", inputstr, "OK");
    }

    private void RotateCube_Command(string command)
    {
        string[] currentOperation = command.Split(',');
        
        if (currentOperation.Length == 3)
        {
            RotateCube(XYZToVector3(currentOperation[0]),
                Convert.ToInt32(currentOperation[1]), 
                Convert.ToInt32(currentOperation[2]));
        }
        else if (currentOperation.Length == 2)
        {
            AllRotation(XYZToVector3(currentOperation[0]),
                Convert.ToInt32(currentOperation[1]));
        }
        else
        {
            EmergencyStop("Command cannot be converted to format");
        }
    }

    private void RotateCube(Vector3 rotAxis, int linenum, float rotangle)
    {
        Vector3 tmprot = rotangle * rotAxis;
        Quaternion quat = Quaternion.Euler(tmprot.x, tmprot.y, tmprot.z); // x axis 90 deg turn is defined as (90, 0, 0) in Euler Angles
        if (linenum == 0) /* Prohibit center cell rotation to keep uniqueness */
        {
            foreach (GameObject cube in cubes)
            {
                CubeRotation sc = cube.GetComponent<CubeRotation>();
                if (Mathf.RoundToInt(Vector3.Dot(cube.transform.position, rotAxis)) == 0) // when rotAxis=(1,0,0), find (0,*,*)
                {
                    sc.RotationStart(Vector3.zero, rotAxis, rotangle, 
                        V_RoundToInt(quat * cube.transform.position), 
                        Q_RoundToInt(quat * cube.transform.rotation) );
                }
            }
        }
        else
        {
            foreach (GameObject cube in cubes)
            {
                CubeRotation sc = cube.GetComponent<CubeRotation>();
                if (Mathf.RoundToInt(Vector3.Dot(cube.transform.position, rotAxis * linenum)/linenum/linenum) == 1)
                {
                    sc.RotationStart(linenum * rotAxis, rotAxis, rotangle, 
                        V_RoundToInt(quat * cube.transform.position), 
                        Q_RoundToInt(quat * cube.transform.rotation) );
                }
            }
        }
        WaitRotation = true;        
        MakeLog(rotAxis, linenum, rotangle);
    }


    private void AllRotation(Vector3 rotAxis, float rotangle)
    {
        Vector3 tmprot = rotangle * rotAxis;
        Quaternion quat = Quaternion.Euler(tmprot.x, tmprot.y, tmprot.z); // x axis 90 deg turn is defined as (90, 0, 0) in Euler Angles
        foreach (GameObject cube in cubes)
        {
            CubeRotation sc = cube.GetComponent<CubeRotation>();
            sc.RotationStart(
                new Vector3(rotAxis.x * cube.transform.position.x, rotAxis.y * cube.transform.position.y, 
                rotAxis.z * cube.transform.position.z), 
                rotAxis, rotangle, 
                V_RoundToInt(quat * cube.transform.position), 
                Q_RoundToInt(quat * cube.transform.rotation) );
        }
        WaitRotation = true;
        MakeLog_AllRot(rotAxis, rotangle);
    }

    private void ClearLog()
    {
        GameLog.Clear();
        inputscript.Clear();
        debugscript.Clear();
        logscript.Clear();
        
    }

    private void MakeLog(Vector3 rotAxis, int linenum, float rotangle)
    {
        GameLog.Add(Vector3ToXYZ(rotAxis)  + "," + linenum.ToString("D1") + "," + rotangle.ToString("F0"));
        logscript.MakeLog(GameLog);
    }

    private void MakeLog_AllRot(Vector3 rotAxis, float rotangle)
    {
        GameLog.Add(Vector3ToXYZ(rotAxis)  + "," + rotangle.ToString("F0"));
        logscript.MakeLog(GameLog);
    }


    private Vector3 V_RoundToInt(Vector3 inv)
    {
        return new Vector3(Mathf.RoundToInt(inv.x), Mathf.RoundToInt(inv.y), Mathf.RoundToInt(inv.z));
    }

    private Quaternion Q_RoundToInt(Quaternion inq)
    {
        return Quaternion.Euler(
                Mathf.RoundToInt(inq.eulerAngles.x),
                Mathf.RoundToInt(inq.eulerAngles.y),
                Mathf.RoundToInt(inq.eulerAngles.z));
    }


    private void SetRK()
    {
        foreach (GameObject cube in cubes)
        {
             RK[Mathf.RoundToInt(cube.transform.position.x)+2,
                Mathf.RoundToInt(cube.transform.position.y)+2,
                Mathf.RoundToInt(cube.transform.position.z)+2] = cube;
        }
        RK_col.SetRK(RK);
    }



    private string Vector3ToXYZ(Vector3 vc)
    {
        return ((vc.x > 0.8)?"X":((vc.y > 0.8)?"Y":"Z"));
    }

    private Vector3 XYZToVector3(string xyz)
    {
        return (xyz.Equals("X")?Vector3.right:(xyz.Equals("Y")?Vector3.up:Vector3.forward));
    }

    void debug()
    {
        string output = "";
        string[] xyz = new string[]{"+X", "-X", "+Y", "-Y", "+Z", "-Z"};

        // output = output + DebugKeyword + "\n";
        foreach (string xyz_ind in xyz)
        {
            output = output + "Plane " + xyz_ind + ": ";
            for (int y = 0 ; y < 5 ; y ++)
            {
                for (int x = 0 ; x < 5 ; x ++)
                {
                    output = output + RK_col.GetCellColorStr(xyz_ind, y, x) + ", ";
                }
            }
            output = output + "\n";
        }

        /*
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int z = 0; z < 3; z++)
                {
                    string tmpout = "(" + x.ToString("D0") + "," + y.ToString("D0") + "," + z.ToString("D0") + "):" + RK[x,y,z].name;
                    output = output + tmpout + " | ";
                }
            }
        }
        */

        debugscript.set(output);

    }

}

