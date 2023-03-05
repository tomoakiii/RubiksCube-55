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
    public void Auto8CallBack()
    {
        isAutoMode = AutoMode.AutoSequenceMode;
        AutoModeStage = 8;
        SolveScript.Clear();
        
        YPlusAllCornerLocation();
        if (SolveScript.Count > 0)
        {
            return;
        }
        DebugKeyword = DebugKeyword + "\n ";
        AutoModeStage = 9;
    }

    private void YPlusAllCornerLocation() // 
    {
        string[] scanDirection = new string[]{"+X", "+Z", "-X", "-Z"};
        bool isComplete = true;
        int is00and02dir = -1;
        int tempRot;
        for (int n = 0; n < 4; n++)
        {
            if (RK_col.GetCellColor(scanDirection[n], 0, 0) == RK_col.GetCellColor(scanDirection[n], 0, 2))
            {
                is00and02dir = n;
            }
            else
            {
                isComplete = false;
            }
        }
        if (isComplete)
        {
            if (RK_col.GetCellColor("+X", 0, 0) == RK_col.GetCellColor("+X", 1, 1))
            {
                return; // complete condition
            }
            else
            {
                for (int n = 1; n < 4; n++) // scanning from +Z to -Z
                {
                    if (RK_col.GetCellColor("+X", 1, 1) == RK_col.GetCellColor(scanDirection[n], 0, 0)) // will not search +X direction
                    {
                        DebugKeyword = DebugKeyword + "step8-1; ";
                        tempRot = (n == 3) ? -90 : n * 90;
                        SolveScript.Add("Y, 1, " + tempRot.ToString());
                        return; // will be completed at next cycle
                    }
                }
            }
        }

        if (is00and02dir == -1)
        {
            DebugKeyword = DebugKeyword + "step8-2; ";
            Solve_OperationE();
            SolveScript.Add("Y, 180");
            Solve_OperationD();
            return;
        }
        
        for (int m = is00and02dir ; m < is00and02dir + 4 ; m++)
        {
            if (RK_col.GetCellColor(scanDirection[is00and02dir%4], 0, 0) == RK_col.GetCellColor(scanDirection[m%4], 1, 1))
            {
                if (m != is00and02dir)
                {
                    tempRot = (m - is00and02dir == 3) ? 90 : (m - is00and02dir) * -90;
                    SolveScript.Add("Y, 1, " + tempRot.ToString());
                    if (m%4 != 1) // +Z then no need to rotate
                    { // Having the target side to be +Z
                        DebugKeyword = DebugKeyword + "step8-3; ";
                        tempRot = (m%4 == 0) ? -90 : (m%4 - 1) * 90; // +X to be -90deg, -X to be 90deg, -Z to be 180deg 
                        SolveScript.Add("Y, " + tempRot.ToString());
                    }
                    DebugKeyword = DebugKeyword + "step8-4; ";
                }
                Solve_OperationD();
                SolveScript.Add("Y, -90");
                Solve_OperationE();
                return;            
            }
        }   
        DebugKeyword = DebugKeyword + "step8-5; ";
        EmergencyStop("Auto8 Error");
    }
}
