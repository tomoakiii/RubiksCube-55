using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using StandardRK;

public partial class RubiksCube : MonoBehaviour
{
    public void Auto1CallBack()
    {
        isAutoMode = AutoMode.AutoSequenceMode;
        AutoModeStage = 1;
        SolveScript.Clear();


        AutoModeStage = 2;
    }

    private void YtoDisignedColor(Colors inCol)
    {
        if(RK_col.GetCellColor("+X", 2, 2) == inCol)
        {
            SolveScript.Add("Z, 90");
        }
        else if(RK_col.GetCellColor("-X", 2, 2) == inCol)
        {
            SolveScript.Add("Z, -90");
        }
        else if(RK_col.GetCellColor("-Y", 2, 2) == inCol)
        {
            SolveScript.Add("Z, 180");
        }
        else if(RK_col.GetCellColor("+Z", 2, 2) == inCol)
        {
            SolveScript.Add("X, -90");
        }
        else if(RK_col.GetCellColor("-Z", 2, 2) == inCol)
        {
            SolveScript.Add("X, 90");
        }
    }


    private void XtoDesignedColor(Colors inCol)
    {
        if(RK_col.GetCellColor("-X", 2, 2) == inCol)
        {
            SolveScript.Add("Y, 180");
        }
        else if(RK_col.GetCellColor("+Z", 2, 2) == inCol)
        {
            SolveScript.Add("Y, 90");
        }
        else if(RK_col.GetCellColor("-Z", 2, 2) == inCol)
        {
            SolveScript.Add("Y, -90");
        }
    }

    private void Solve_OperationB()
    {
        SolveScript.Add("X, 1, -90");
        SolveScript.Add("Y, 1, -90");
        SolveScript.Add("Z, -1, 90");
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, -1, -90");
        SolveScript.Add("X, 1, 90");
    }

    private void Solve_OperationC1()
    {
        SolveScript.Add("Z, -1, 90");
        SolveScript.Add("Y, 1, -90");
        SolveScript.Add("Z, -1, -90");
        SolveScript.Add("Y, 1, -90");
        SolveScript.Add("Z, -1, 90");
        SolveScript.Add("Y, 1, 180");
        SolveScript.Add("Z, -1, -90");
    }

    private void Solve_OperationC2()
    {
        SolveScript.Add("Z, 1, 90");
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, 1, -90");
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, 1, 90");
        SolveScript.Add("Y, 1, 180");
        SolveScript.Add("Z, 1, -90");
    }

    private void Solve_OperationD()
    {
        SolveScript.Add("Z, -1, 90");
        SolveScript.Add("Y, 1, -90");
        SolveScript.Add("Z, -1, -90");
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, 1, 90");
        SolveScript.Add("Y, 1, -90");
        SolveScript.Add("Z, -1, 90");
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, -1, -90");
        SolveScript.Add("Z, 1, -90");
    }

    private void Solve_OperationE()
    {
        SolveScript.Add("Z, 1, 90");
        SolveScript.Add("Z, -1, 90");
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, 1, -90");
        SolveScript.Add("Y, 1, -90");
        SolveScript.Add("Z, -1, -90");
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, 1, 90");
        SolveScript.Add("Y, 1, -90");
        SolveScript.Add("Z, 1, -90");
    }
}
