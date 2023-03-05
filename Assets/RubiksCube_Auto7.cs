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
    public void Auto7CallBack()
    {
        isAutoMode = AutoMode.AutoSequenceMode;
        AutoModeStage = 7;
        SolveScript.Clear();

        YPlusAllYellow();
        if (SolveScript.Count > 0)
        {
            return;
        }
        DebugKeyword = DebugKeyword + "\n ";
        AutoModeStage = 8;
    }

    // Make Y Plus side all yellow color
    private void YPlusAllYellow() // 
    {
        // Just in case debug check...
        if (RK_col.GetCellColor("+Y", 0, 1) == Colors.Yellow && RK_col.GetCellColor("+Y", 1, 0) == Colors.Yellow 
            && RK_col.GetCellColor("+Y", 1, 2) == Colors.Yellow && RK_col.GetCellColor("+Y", 2, 1) == Colors.Yellow)
        {
        }
        else
        {
            EmergencyStop("Auto7 Error");
            return;
        }

        if (RK_col.GetCellColor("+Y", 0, 0) == Colors.Yellow && RK_col.GetCellColor("+Y", 0, 2) == Colors.Yellow 
        && RK_col.GetCellColor("+Y", 2, 0) == Colors.Yellow && RK_col.GetCellColor("+Y", 2, 2) == Colors.Yellow)
        { // Complete
            return;
        }
        else if (RK_col.GetCellColor("+Y", 2, 2) == Colors.Yellow && RK_col.GetCellColor("+X", 0, 0) == Colors.Yellow 
            && RK_col.GetCellColor("-X", 0, 0) == Colors.Yellow && RK_col.GetCellColor("-Z", 0, 0) == Colors.Yellow)
        {
            DebugKeyword = DebugKeyword + "step7-1; ";
            Solve_OperationC1();
        }
        else if (RK_col.GetCellColor("+Y", 2, 0) == Colors.Yellow && RK_col.GetCellColor("+X", 0, 2) == Colors.Yellow 
            && RK_col.GetCellColor("+Z", 0, 2) == Colors.Yellow && RK_col.GetCellColor("-X", 0, 2) == Colors.Yellow)
        {
            DebugKeyword = DebugKeyword + "step7-2; ";
            Solve_OperationC2();
        }
        else if (RK_col.GetCellColor("+Y", 0, 0) == Colors.Yellow && RK_col.GetCellColor("+Y", 2, 0) == Colors.Yellow 
            && RK_col.GetCellColor("+X", 0, 2) == Colors.Yellow && RK_col.GetCellColor("-X", 0, 0) == Colors.Yellow)
        {
            DebugKeyword = DebugKeyword + "step7-3; ";
            Solve_OperationD();
        }
        else if (RK_col.GetCellColor("+Y", 0, 0) == Colors.Yellow && RK_col.GetCellColor("+Y", 2, 2) == Colors.Yellow 
            && RK_col.GetCellColor("+X", 0, 0) == Colors.Yellow && RK_col.GetCellColor("+Z", 0, 2) == Colors.Yellow)
        {
            DebugKeyword = DebugKeyword + "step7-4; ";
            Solve_OperationE();
        }
        else if (RK_col.GetCellColor("+Y", 0, 0) == Colors.Yellow && RK_col.GetCellColor("+Y", 0, 2) == Colors.Yellow 
            && RK_col.GetCellColor("+X", 0, 0) == Colors.Yellow && RK_col.GetCellColor("+X", 0, 2) == Colors.Yellow)
        {
            DebugKeyword = DebugKeyword + "step7-5; ";
            Solve_OperationC1();
        }
        else if (RK_col.GetCellColor("+Z", 0, 2) == Colors.Yellow && RK_col.GetCellColor("-Z", 0, 0) == Colors.Yellow 
            && RK_col.GetCellColor("+X", 0, 0) == Colors.Yellow && RK_col.GetCellColor("+X", 0, 2) == Colors.Yellow)
        {
            DebugKeyword = DebugKeyword + "step7-6; ";
            Solve_OperationC1();
        }
        else if (RK_col.GetCellColor("+X", 0, 0) == Colors.Yellow && RK_col.GetCellColor("+X", 0, 2) == Colors.Yellow 
            && RK_col.GetCellColor("-X", 0, 0) == Colors.Yellow && RK_col.GetCellColor("-X", 0, 2) == Colors.Yellow)
        {
            DebugKeyword = DebugKeyword + "step7-7; ";
            Solve_OperationC1();
        }
        else
        {
            DebugKeyword = DebugKeyword + "step7-8; ";
            SolveScript.Add("Y, 1, 90");
        }
    }
}
