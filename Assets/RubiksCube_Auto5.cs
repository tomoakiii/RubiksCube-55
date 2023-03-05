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
    public void Auto5CallBack()
    {
        isAutoMode = AutoMode.AutoSequenceMode;
        AutoModeStage = 5;
        SolveScript.Clear();

        YtoDisignedColor(Colors.Yellow);
        if (SolveScript.Count > 0)
        {
            return;
        }
        
        if (1 != Check2ndRowComplete())
        {
            DebugKeyword = DebugKeyword + "step5-1; ";
            
            Align2ndRow();
            if (SolveScript.Count > 0)
            {
                return;
            }
        }

        DebugKeyword = DebugKeyword + "\n ";
        AutoModeStage = 6;
    }

 
    private int Check2ndRowComplete()
    {
        string[] scanDirection = new string[]{"+X", "+Z", "-X", "-Z"};

        bool completeFlag = true;
        for (int n = 0; n < 4; n++)
        {
            if (RK_col.GetCellColor(scanDirection[n], 1, 0) == RK_col.GetCellColor(scanDirection[n], 1, 1)
                && RK_col.GetCellColor(scanDirection[n], 1, 1) == RK_col.GetCellColor(scanDirection[n], 1, 2))
            {
            }
            else
            {
                completeFlag = false;
                break;
            }
        }
        if (completeFlag)
        {
            return 1;
        }
        return 0;        
    }

    private void Align2ndRow() // 
    {
        string[] scanDirection = new string[]{"+X", "+Z", "-X", "-Z"};
        int[,] scanYPlus = new int[,]{{2,1}, {1,2}, {0,1}, {1,0}};
        int is01dir = -1;
        for (int n = 0; n < 4; n++)
        {
            if (RK_col.GetCellColor(scanDirection[n], 0, 1) != Colors.Yellow
                && RK_col.GetCellColor("+Y", scanYPlus[n, 0], scanYPlus[n, 1]) != Colors.Yellow)
            {
                is01dir = n;
                if (RK_col.GetCellColor(scanDirection[n], 0, 1) == RK_col.GetCellColor(scanDirection[n], 1, 1))
                {
                    if (n != 0)
                    { // n is not zero --> need to get the cube to front
                        int tempRot = (n == 3)?-90:n*90;
                        SolveScript.Add("Y, " + tempRot.ToString());
                    }
                    if (RK_col.GetCellColor("+Y", scanYPlus[n, 0], scanYPlus[n, 1]) == RK_col.GetCellColor(scanDirection[(n+1)%4], 1, 1))
                    { // Need right turn
                        Align2ndRow_RightTurn();
                        return;
                    }
                    else if (RK_col.GetCellColor("+Y", scanYPlus[n, 0], scanYPlus[n, 1]) == RK_col.GetCellColor(scanDirection[(n+3)%4], 1, 1))
                    { // Need left turn
                        Align2ndRow_LeftTurn();
                        return;
                    }
                    else
                    { // 0,1 cell doesn't have Yellow but doesn't much neigher right nor left
                        EmergencyStop("Auto5 Error");
                        return;
                    }
                }
            }
        }
        
        if (is01dir > -1)
        {
            for (int m = is01dir + 1; m < is01dir + 4; m++)
            {
                if (RK_col.GetCellColor(scanDirection[is01dir%4], 0, 1) == RK_col.GetCellColor(scanDirection[m%4], 1, 1))
                { // have 0,1 and 1,1 same color on the m side
                    int tempRot = ((m-is01dir) == 3) ? 90 : (m-is01dir) * (-90);
                    SolveScript.Add("Y, 1, " + tempRot.ToString());
                    return;
                }
            }
        }
        else
        {
            for (int n = 0; n < 4; n++)
            {
                if (RK_col.GetCellColor(scanDirection[n], 1, 1) != RK_col.GetCellColor(scanDirection[n], 1, 0))
                { // replace left side
                    if (n != 0)
                    {
                        int tempRot = (n == 3) ? -90 : n * (-90);
                        SolveScript.Add("Y, " + tempRot.ToString());
                    }
                    Align2ndRow_LeftTurn();
                    return;
                }
                if (RK_col.GetCellColor(scanDirection[n], 1, 1) != RK_col.GetCellColor(scanDirection[n], 1, 2))
                { // replace right side
                    if (n != 0)
                    {
                        int tempRot = (n == 3) ? -90 : n * (-90);
                        SolveScript.Add("Y, " + tempRot.ToString());
                    }
                    Align2ndRow_RightTurn();
                    return;
                }
            }
        }
        // Must not be reached to here
        EmergencyStop("Auto5-2 Error");
    }

    private void Align2ndRow_RightTurn()
    {
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, 1, 90");
        SolveScript.Add("Y, 1, -90");
        SolveScript.Add("Z, 1, -90");
        SolveScript.Add("Y, 90");
        SolveScript.Add("Y, 1, -90");
        SolveScript.Add("Z, -1, 90");
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, -1, -90");
    }

    private void Align2ndRow_LeftTurn()
    {
        SolveScript.Add("Y, 1, -90");
        SolveScript.Add("Z, -1, 90");
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, -1, -90");
        SolveScript.Add("Y, -90");
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, 1, 90");
        SolveScript.Add("Y, 1, -90");
        SolveScript.Add("Z, 1, -90");

    }

}
