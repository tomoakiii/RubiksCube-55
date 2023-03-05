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
    public void Auto4CallBack()
    {
        isAutoMode = AutoMode.AutoSequenceMode;
        AutoModeStage = 4;
        SolveScript.Clear();

        WhiteCorner_1();
        if (SolveScript.Count > 0)
        {
            return;
        }
        WhiteCorner_2();
        if (SolveScript.Count > 0)
        {
            return;
        }
        
        WhiteCorner2_1();
        if (SolveScript.Count > 0)
        {
            return;
        }

        WhiteCorner2_2();
        if (SolveScript.Count > 0)
        {
            return;
        }

        WhiteCorner3();
        if (SolveScript.Count > 0)
        {
            return;
        }
        AutoModeStage = 5;
    }

    private void WhiteCorner_1() // pattern1: 2,0 == white then move it to +Z direction
    {
        Colors temp_color;
        if (RK_col.GetCellColor("+X", 2, 0) == Colors.White) // -z direction
        {
            temp_color = RK_col.GetCellColor("-Z", 2, 2);
            SolveScript.Add("Y, -1, -90");
        }
        else if (RK_col.GetCellColor("+Z", 2, 0) == Colors.White) // +x direction
        {
            temp_color = RK_col.GetCellColor("+X", 2, 2);
        }
        else if (RK_col.GetCellColor("-Z", 2, 0) == Colors.White) // +z direction
        {
            temp_color = RK_col.GetCellColor("-X", 2, 2);
            SolveScript.Add("Y, -1, 180");
        }
        else if (RK_col.GetCellColor("-X", 2, 0) == Colors.White) // -x direction
        {
            temp_color = RK_col.GetCellColor("+Z", 2, 2);
            SolveScript.Add("Y, -1, 90");
        }
        else
        {
            return; // Nothing on list
        }
    

        if (temp_color == RK_col.GetCellColor("-Z", 1, 1))
        {
            SolveScript.Add("Z, -1, -90");
            SolveScript.Add("Y, -1, 90");
            SolveScript.Add("Z, -1, 90");
        }
        else if (temp_color == RK_col.GetCellColor("+Z", 1, 1))
        {
            SolveScript.Add("Y, 1, 180");
            SolveScript.Add("Z, -1, -90");
            SolveScript.Add("Y, -1, 90");
            SolveScript.Add("Z, -1, 90");
            SolveScript.Add("Y, 1, 180");
        }
        else if (temp_color == RK_col.GetCellColor("+X", 1, 1))
        {
            SolveScript.Add("Y, 1, 90");
            SolveScript.Add("Z, -1, -90");
            SolveScript.Add("Y, -1, 90");
            SolveScript.Add("Z, -1, 90");
            SolveScript.Add("Y, 1, -90");
        }
        else if (temp_color == RK_col.GetCellColor("-X", 1, 1))
        {
            SolveScript.Add("Y, 1, -90");
            SolveScript.Add("Z, -1, -90");
            SolveScript.Add("Y, -1, 90");
            SolveScript.Add("Z, -1, 90");
            SolveScript.Add("Y, 1, 90");
        }
        else
        {
            EmergencyStop("Auto4 Error");
        }
    }

    private void WhiteCorner_2() // pattern1: 2,2 == white then move it to -Z direction
    {
        Colors temp_color;
        if (RK_col.GetCellColor("+X", 2, 2) == Colors.White) // -z direction
        {
            temp_color = RK_col.GetCellColor("+Z", 2, 0);
            SolveScript.Add("Y, -1, 90");            
        }
        else if (RK_col.GetCellColor("+Z", 2, 2) == Colors.White) // +x direction
        {
            temp_color = RK_col.GetCellColor("-X", 2, 0);
            SolveScript.Add("Y, -1, 180");
        }
        else if (RK_col.GetCellColor("-Z", 2, 2) == Colors.White) // +z direction
        {
            temp_color = RK_col.GetCellColor("+X", 2, 0);
        }
        else if (RK_col.GetCellColor("-X", 2, 2) == Colors.White) // -x direction
        {
            temp_color = RK_col.GetCellColor("-Z", 2, 0);
            SolveScript.Add("Y, -1, -90");
        }
        else
        {
            return; // Nothing on list
        }
    

        if (temp_color == RK_col.GetCellColor("+Z", 1, 1))
        {
            SolveScript.Add("Z, 1, -90");
            SolveScript.Add("Y, -1, -90");
            SolveScript.Add("Z, 1, 90");
        }
        else if (temp_color == RK_col.GetCellColor("-Z", 1, 1))
        {
            SolveScript.Add("Y, 1, 180");
            SolveScript.Add("Z, 1, -90");
            SolveScript.Add("Y, -1, -90");
            SolveScript.Add("Z, 1, 90");
            SolveScript.Add("Y, 1, 180");
        }
        else if (temp_color == RK_col.GetCellColor("+X", 1, 1))
        {
            SolveScript.Add("Y, 1, -90");
            SolveScript.Add("Z, 1, -90");
            SolveScript.Add("Y, -1, -90");
            SolveScript.Add("Z, 1, 90");
            SolveScript.Add("Y, 1, 90");
        }
        else if (temp_color == RK_col.GetCellColor("-X", 1, 1))
        {
            SolveScript.Add("Y, 1, 90");
            SolveScript.Add("Z, 1, -90");
            SolveScript.Add("Y, -1, -90");
            SolveScript.Add("Z, 1, 90");
            SolveScript.Add("Y, 1, -90");
        }
        else
        {
            EmergencyStop("Auto4-2 Error");
        }
    }


    private void WhiteCorner2_1() // if 0,0 or 0,2 on side is white
    {
        if (RK_col.GetCellColor("+X", 0, 0) == Colors.White) // -z direction
        {
            SolveScript.Add("Z, -1, -90");
            SolveScript.Add("Y, -1, 90");
            SolveScript.Add("Z, -1, 90");
        }
        else if (RK_col.GetCellColor("+Z", 0, 0) == Colors.White) // +x direction
        {
            SolveScript.Add("X, 1, 90");
            SolveScript.Add("Y, -1, 90");
            SolveScript.Add("X, 1, -90");
        }
        else if (RK_col.GetCellColor("-X", 0, 0) == Colors.White) // +z direction
        {
            SolveScript.Add("Z, 1, 90");
            SolveScript.Add("Y, -1, 90");
            SolveScript.Add("Z, 1, -90");
        }
        else if (RK_col.GetCellColor("-Z", 0, 0) == Colors.White) // -x direction
        {
            SolveScript.Add("X, -1, -90");
            SolveScript.Add("Y, -1, 90");
            SolveScript.Add("X, -1, 90");
        }
        else if (RK_col.GetCellColor("+X", 0, 2) == Colors.White) // -z direction
        {
            SolveScript.Add("Z, 1, -90");
            SolveScript.Add("Y, -1, -90");
            SolveScript.Add("Z, 1, 90");
        }
        else if (RK_col.GetCellColor("+Z", 0, 2) == Colors.White) // +x direction
        {
            SolveScript.Add("X, -1, 90");
            SolveScript.Add("Y, -1, -90");
            SolveScript.Add("X, -1, -90");
        }
        else if (RK_col.GetCellColor("-X", 0, 2) == Colors.White) // +z direction
        {
            SolveScript.Add("Z, -1, 90");
            SolveScript.Add("Y, -1, -90");
            SolveScript.Add("Z, -1, -90");
        }
        else if (RK_col.GetCellColor("-Z", 0, 2) == Colors.White) // -x direction
        {
            SolveScript.Add("X, 1, -90");
            SolveScript.Add("Y, -1, -90");
            SolveScript.Add("X, 1, 90");
        }
    }

    private void WhiteCorner2_2() // if bottom corner is white
    {
        if (RK_col.GetCellColor("-Y", 0, 0) == Colors.White) // -z direction
        {
        }
        else if (RK_col.GetCellColor("-Y", 2, 2) == Colors.White) // -x direction
        {
            SolveScript.Add("Y, -1, 180");
        }
        else if (RK_col.GetCellColor("-Y", 0, 2) == Colors.White) // -x direction
        {
            SolveScript.Add("Y, -1, 90");
        }
        else if (RK_col.GetCellColor("-Y", 2, 0) == Colors.White) // -x direction
        {
            SolveScript.Add("Y, -1, -90");
        }
        else
        {
            return;
        }
        if (RK_col.GetCellColor("+Y", 2, 0) != Colors.White) // -z direction
        {
            SolveScript.Add("Z, -1, -90");
            SolveScript.Add("Y, -1, 90");
            SolveScript.Add("Z, -1, 90");
        }
        else if (RK_col.GetCellColor("+Y", 0, 0) != Colors.White) // +x direction
        {
            SolveScript.Add("Y, 1, -90");
            SolveScript.Add("Z, -1, -90");
            SolveScript.Add("Y, -1, 90");
            SolveScript.Add("Z, -1, 90");
            SolveScript.Add("Y, 1, 90");
        }
        else if (RK_col.GetCellColor("+Y", 2, 2) == Colors.White) // +z direction
        {
            SolveScript.Add("Y, 1, 90");
            SolveScript.Add("Z, -1, -90");
            SolveScript.Add("Y, -1, 90");
            SolveScript.Add("Z, -1, 90");
            SolveScript.Add("Y, 1, -90");
        }
        else if (RK_col.GetCellColor("+Y", 0, 2) == Colors.White) // -x direction
        {
            SolveScript.Add("Y, 1, 180");
            SolveScript.Add("Z, -1, -90");
            SolveScript.Add("Y, -1, 90");
            SolveScript.Add("Z, -1, 90");
            SolveScript.Add("Y, 1, 180");
        }
    }

    private void WhiteCorner3() // if top is all white but corner is not correct
    {
        if (RK_col.GetCellColor("+Y", 2, 0) == Colors.White && RK_col.GetCellColor("+X", 0, 0) != RK_col.GetCellColor("+X", 1, 1)) // -z direction
        {
            SolveScript.Add("Z, -1, -90");
            SolveScript.Add("Y, -1, -90");
            SolveScript.Add("Z, -1, 90");
        }
        else if (RK_col.GetCellColor("+Y", 2, 2) == Colors.White && RK_col.GetCellColor("+Z", 0, 0) != RK_col.GetCellColor("+Z", 1, 1)) // -z direction
        {
            SolveScript.Add("Y, 1, 90");
            SolveScript.Add("Z, -1, -90");
            SolveScript.Add("Y, -1, -90");
            SolveScript.Add("Z, -1, 90");
            SolveScript.Add("Y, 1, -90");
        }
        else if (RK_col.GetCellColor("+Y", 0, 2) == Colors.White && RK_col.GetCellColor("-X", 0, 0) != RK_col.GetCellColor("-X", 1, 1)) // -z direction
        {
            SolveScript.Add("Y, 1, 180");
            SolveScript.Add("Z, -1, -90");
            SolveScript.Add("Y, -1, -90");
            SolveScript.Add("Z, -1, 90");
            SolveScript.Add("Y, 1, 180");
        }
        else if (RK_col.GetCellColor("+Y", 0, 0) == Colors.White && RK_col.GetCellColor("-Z", 0, 0) != RK_col.GetCellColor("-Z", 1, 1)) // -z direction
        {
            SolveScript.Add("Y, 1, -90");
            SolveScript.Add("Z, -1, -90");
            SolveScript.Add("Y, -1, -90");
            SolveScript.Add("Z, -1, 90");
            SolveScript.Add("Y, 1, 90");
        }
        else
        {
            return;
        }
    }
}
