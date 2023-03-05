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
    public void Auto9CallBack()
    {
        isAutoMode = AutoMode.AutoSequenceMode;
        AutoModeStage = 9;
        SolveScript.Clear();

        YPlusCornerFix();
        if (SolveScript.Count > 0)
        {
            return;
        }
        DebugKeyword = DebugKeyword + "\n ";
        AutoModeStage = 0;
    }

    private void YPlusCornerFix() // 
    {
        string[] scanDirection = new string[]{"+X", "+Z", "-X", "-Z"}; // Have the target on back side
        int is01dir = -1;
        bool isComplete = true;
        int tempRot;
        for (int n = 0; n < 4; n++)
        {
            if (RK_col.GetCellColor(scanDirection[n], 0, 1) == RK_col.GetCellColor(scanDirection[n], 1, 1))
            {
                is01dir = n;
            }
            else
            {
                isComplete = false;
            }
        }
        if (isComplete)
        {
            return;
        }


        if (is01dir == -1)
        { // There is not matching side
            // Operation C1
            Solve_OperationC1();
            // turn to right 90deg
            SolveScript.Add("Y, -90");
            // Operation C2
            Solve_OperationC2();
            return;
        }
        else if (is01dir != 2)
        {
            tempRot = (is01dir == 3) ? 90 : ((is01dir == 1) ? -90 : 180);
            SolveScript.Add("Y, " + tempRot.ToString());
        }

        if (RK_col.GetCellColor(scanDirection[(is01dir + 2)%4], 1, 1) == RK_col.GetCellColor(scanDirection[(is01dir + 1)%4], 0, 1))
        { // turning left
            Solve_OperationC1();
            // turn to right 90deg
            SolveScript.Add("Y, -90");
            // Operation C2
            Solve_OperationC2();
            return;
        }
        else if (RK_col.GetCellColor(scanDirection[(is01dir + 2)%4], 1, 1) == RK_col.GetCellColor(scanDirection[(is01dir + 3)%4], 0, 1))
        { // turning right
            Solve_OperationC2();
            // turn to left 90deg
            SolveScript.Add("Y, 90");
            Solve_OperationC1();
            return;
        }
        
        
        DebugKeyword = DebugKeyword + "step9; ";
        EmergencyStop("Auto9 Error");
    }

}
