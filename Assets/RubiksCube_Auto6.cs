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
    public void Auto6CallBack()
    {
        isAutoMode = AutoMode.AutoSequenceMode;
        AutoModeStage = 6;
        SolveScript.Clear();

        YPlusYellowCross();
        if (SolveScript.Count > 0)
        {
            return;
        }
       DebugKeyword = DebugKeyword + "\n ";
       AutoModeStage = 7;
    }

 


    private void YPlusYellowCross() // 
    {
        bool actionFlag = false;
        if (RK_col.GetCellColor("+Y", 0, 1) == Colors.Yellow && RK_col.GetCellColor("+Y", 1, 0) == Colors.Yellow 
            && RK_col.GetCellColor("+Y", 1, 2) == Colors.Yellow && RK_col.GetCellColor("+Y", 2, 1) == Colors.Yellow)
        { // completed
            DebugKeyword = DebugKeyword + "step6-1; ";
            return;
        }
        else if (RK_col.GetCellColor("+Y", 0, 1) == Colors.Yellow && RK_col.GetCellColor("+Y", 1, 0) != Colors.Yellow 
            && RK_col.GetCellColor("+Y", 1, 2) == Colors.Yellow && RK_col.GetCellColor("+Y", 2, 1) != Colors.Yellow)
        { // L shape
            DebugKeyword = DebugKeyword + "step6-2; ";
            actionFlag = true;
        }
        else if (RK_col.GetCellColor("+Y", 0, 1) != Colors.Yellow && RK_col.GetCellColor("+Y", 1, 0) == Colors.Yellow 
            && RK_col.GetCellColor("+Y", 1, 2) != Colors.Yellow && RK_col.GetCellColor("+Y", 2, 1) == Colors.Yellow)
        { // reverse L shape
            DebugKeyword = DebugKeyword + "step6-3; ";
            SolveScript.Add("Y, 1, 180");
            actionFlag = true;
        }
        else if (RK_col.GetCellColor("+Y", 0, 1) == Colors.Yellow && RK_col.GetCellColor("+Y", 1, 0) == Colors.Yellow 
            && RK_col.GetCellColor("+Y", 1, 2) != Colors.Yellow && RK_col.GetCellColor("+Y", 2, 1) != Colors.Yellow)
        { // -90deg L shape
            DebugKeyword = DebugKeyword + "step6-4; ";
            SolveScript.Add("Y, 1, 90");
            actionFlag = true;
        }
        else if (RK_col.GetCellColor("+Y", 0, 1) != Colors.Yellow && RK_col.GetCellColor("+Y", 1, 0) != Colors.Yellow 
            && RK_col.GetCellColor("+Y", 1, 2) == Colors.Yellow && RK_col.GetCellColor("+Y", 2, 1) == Colors.Yellow)
        { // 90deg L shape
            DebugKeyword = DebugKeyword + "step6-5; ";
            SolveScript.Add("Y, 1, -90");
            actionFlag = true;
        }
        else if (RK_col.GetCellColor("+Y", 0, 1) == Colors.Yellow && RK_col.GetCellColor("+Y", 1, 0) != Colors.Yellow 
            && RK_col.GetCellColor("+Y", 1, 2) != Colors.Yellow && RK_col.GetCellColor("+Y", 2, 1) == Colors.Yellow)
        { // vertical bar
            DebugKeyword = DebugKeyword + "step6-6; ";
            actionFlag = true;
        }
        else if (RK_col.GetCellColor("+Y", 0, 1) != Colors.Yellow && RK_col.GetCellColor("+Y", 1, 0) == Colors.Yellow 
            && RK_col.GetCellColor("+Y", 1, 2) == Colors.Yellow && RK_col.GetCellColor("+Y", 2, 1) != Colors.Yellow)
        { // horizontal bar
            DebugKeyword = DebugKeyword + "step6-7; ";
            SolveScript.Add("Y, 1, 90");
            actionFlag = true;
        }
        else if (RK_col.GetCellColor("+Y", 0, 1) != Colors.Yellow && RK_col.GetCellColor("+Y", 1, 0) != Colors.Yellow 
            && RK_col.GetCellColor("+Y", 1, 2) != Colors.Yellow && RK_col.GetCellColor("+Y", 2, 1) != Colors.Yellow)
        { // no yellow at cross line
            DebugKeyword = DebugKeyword + "step6-8; ";
            actionFlag = true;
        }
        else
        {
            EmergencyStop("Auto6 Error");
        }
        
        if (actionFlag)
        {
            Solve_OperationB();
        }
    }
}
