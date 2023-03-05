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
    public void Auto3CallBack()
    {
        isAutoMode = AutoMode.AutoSequenceMode;
        AutoModeStage = 3;
        SolveScript.Clear();

        YtoDisignedColor(Colors.White);
        if (SolveScript.Count > 0)
        {
            return;
        }

        WhiteFlowerToTop_Side();
        if (SolveScript.Count > 0)
        {
            return;
        }
        AutoModeStage = 4;        
    }

    private void WhiteFlowerToTop_Side()
    {
        // make 0,1 on -Y being white if there is any white at cross line
        if (RK_col.GetCellColor("-Y", 0, 1) == Colors.White)
        {
        }
        else if (RK_col.GetCellColor("-Y", 1, 0) == Colors.White)
        {
            SolveScript.Add("Y, -90");
            return;
        }
        else if (RK_col.GetCellColor("-Y", 1, 2) == Colors.White)
        {
            SolveScript.Add("Y, 90");
            return;
        }
        else if (RK_col.GetCellColor("-Y", 2, 1) == Colors.White)
        {
            SolveScript.Add("Y, 180");
            return;
        }
        else
        { // there is not White color in cross line at -Y side --> Complete this step
            return;
        }

        
        if (RK_col.GetCellColor("+X", 2, 1) == RK_col.GetCellColor("+X", 1, 1))
        {
            SolveScript.Add("X, 1, 180");
        }
        else if (RK_col.GetCellColor("+X", 2, 1) == RK_col.GetCellColor("+Z", 1, 1))
        {
            SolveScript.Add("Y, -1, -90");
            SolveScript.Add("Z, 1, 180");
        }
        else if (RK_col.GetCellColor("+X", 2, 1) == RK_col.GetCellColor("-Z", 1, 1))
        {
            SolveScript.Add("Y, -1, 90");
            SolveScript.Add("Z, -1, 180");
        }
        else if (RK_col.GetCellColor("+X", 2, 1) == RK_col.GetCellColor("-X", 1, 1))
        {
            SolveScript.Add("Y, -1, 180");
            SolveScript.Add("X, -1, 180");
        }
        else
        {
            EmergencyStop("Auto3 Error");
        }
    }
}
