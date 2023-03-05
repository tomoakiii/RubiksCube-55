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
    private int AutoModeStage2Index = 0;
    public void Auto2CallBack()
    {
        isAutoMode = AutoMode.AutoSequenceMode;
        AutoModeStage = 2;
        SolveScript.Clear();

        Colors[] scanColor = new Colors[] { Colors.White, Colors.Yellow, Colors.Orange, Colors.Green, Colors.Red, Colors.Blue };
        for(int n = AutoModeStage2Index; n < 6; n++)
        {
            YtoDisignedColor(scanColor[n]);
            if (SolveScript.Count > 0)
            {
                return;
            }
            TopFlower_ToMakeXP33(scanColor[n]);
            if (SolveScript.Count > 0)
            {
                return;
            }
            TopFlower_ToMakeXP23(scanColor[n]);
            if (SolveScript.Count > 0)
            {
                return;
            }
            AutoModeStage2Index++;
        }
        AutoModeStage2Index = 0;
        AutoModeStage = 0;
    }
    private void TopFlower_ToMakeXP33(Colors incol)
    {
        bool isComplete = true;
        for (int n = 1; n <= 3; n+=2)
        {
            for (int m = 1; m <= 3; m+=2)
            {
                if (RK_col.GetCellColor("+Y", n, m) != incol)
                {
                    isComplete = false;
                    break;
                }
            }
        }
        if (isComplete)
        {
            return;
        }

        string[] scanDirection = new string[] { "+X", "+Z", "-X", "-Z" };
        int[,] XPCoord = new int[,] { { 3, 3 }, { 1, 3 }, { 1, 1 }, { 3, 1 } };
        string[] edgeScript = new string[] { "X, 2, ", "Z, 2, ", "X, -2, ", "Z, -2, " };
        int tempRot;
        bool is33 = false;

        for (int n = 0; n < 4; n++)
        { // look for each side
            for (int m = 0; m < 4; m++)
            { // look for each corner cube on one side
                if (RK_col.GetCellColor(scanDirection[n], XPCoord[m, 0], XPCoord[m, 1]) == incol)
                {
                    if (n != 0)
                    {
                        tempRot = (n == 3) ? -90 : n * 90;
                        SolveScript.Add("Y, " + tempRot.ToString());
                        return;
                    }
                    if (m != 0)
                    {
                        tempRot = (m == 3) ? -90 : m * 90;
                        SolveScript.Add("X, 2, " + tempRot.ToString());
                    }
                    is33 = true;
                    break;
                }
            }
            if (is33)
            {
                break;
            }
        }

        int[,] YPCoord = new int[,] { { 1, 1 }, { 3, 1 }, { 3, 3 }, { 1, 3 }};
        for (int n = 0; n < 4; n++)
        { // need to make Y33 white
            if (RK_col.GetCellColor("+Y", YPCoord[n, 0], YPCoord[n, 1]) != incol)
            { // Y33 should not be target color at beginning
                if (n != 0)
                {
                    tempRot = (n == 3) ? -90 : n * 90;
                    SolveScript.Add("Y, 2, " + tempRot.ToString());
                }
                break;
            }
        }
        if (is33)
        { // X33 is white:
            SolveScript.Add("Z, 1, 90");
            SolveScript.Add("Y, 2, 90");
            SolveScript.Add("Z, 1, -90");
        }
        else
        { // X33 is not white: look for white on -X side
            int[,] YNCoord = new int[,] { { 3, 3 }, { 3, 1 }, { 1, 1 }, { 1, 3 } };
            for (int n = 0; n < 4; n++)
            { // need to make -Y33 white
                if (RK_col.GetCellColor("-Y", YNCoord[n, 0], YNCoord[n, 1]) == incol)
                { // -Y33 should be target color
                    if (n != 0)
                    {
                        tempRot = (n == 3) ? -90 : n * 90;
                        SolveScript.Add("Y, -2, " + tempRot.ToString());
                    }
                    break;
                }
            } // make -X33 white
            SolveScript.Add("Z, 1, 180");
            SolveScript.Add("Y, 2, 90");
            SolveScript.Add("Z, 1, -180");
        }
    }

    private void TopFlower_ToMakeXP23(Colors incol)
    {
        if (RK_col.GetCellColor("+Y", 1, 2) == incol &&
            RK_col.GetCellColor("+Y", 2, 1) == incol &&
            RK_col.GetCellColor("+Y", 2, 3) == incol &&
            RK_col.GetCellColor("+Y", 3, 2) == incol)
        {
            return;
        }

        string[] scanDirection = new string[] { "+X", "+Z", "-X", "-Z" };
        int[,] XPCoord = new int[,] { { 2, 3 }, { 1, 2 }, { 2, 1 }, { 3, 2 } };
        string[] edgeScript = new string[] { "X, 2, ", "Z, 2, ", "X, -2, ", "Z, -2, " };
        int tempRot;
        bool is33 = false;

        for (int n = 0; n < 4; n++)
        { // look for each side
            for (int m = 0; m < 4; m++)
            { // look for each corner cube on one side
                if (RK_col.GetCellColor(scanDirection[n], XPCoord[m, 0], XPCoord[m, 1]) == incol)
                {
                    if (n != 0)
                    {
                        tempRot = (n == 3) ? -90 : n * 90;
                        SolveScript.Add("Y, " + tempRot.ToString());
                        return;
                    }
                    if (m != 0)
                    {
                        tempRot = (m == 3) ? -90 : m * 90;
                        SolveScript.Add("X, 2, " + tempRot.ToString());
                    }
                    is33 = true;
                    break;
                }
            }
            if (is33)
            {
                break;
            }
        }

        int[,] YPCoord = new int[,] { { 2, 3 }, { 1, 2 }, { 2, 1 }, { 3, 2 } };
        for (int n = 0; n < 4; n++)
        { // need to make Y33 white
            if (RK_col.GetCellColor("+Y", YPCoord[n, 0], YPCoord[n, 1]) != incol)
            { // Y23 should not be target color at beginning
                if (n != 0)
                {
                    tempRot = (n == 3) ? -90 : n * 90;
                    SolveScript.Add("Y, 2, " + tempRot.ToString());
                }
                break;
            }
        }
        if (is33)
        { // X33 is white:
            SolveScript.Add("Z, 1, 90");
            SolveScript.Add("Y, 2, 90");
            SolveScript.Add("Z, 0, 90");
            SolveScript.Add("Y, 2, -90");
            SolveScript.Add("Z, 1, -90");
            SolveScript.Add("Y, 2, 90");
            SolveScript.Add("Z, 0, -90");
        }
        else
        { // X33 is not white: look for white on -X side
            int[,] YNCoord = new int[,] { { 2, 3 }, { 3, 2 }, { 2, 1 }, { 1, 2 } };
            for (int n = 0; n < 4; n++)
            { // need to make -Y33 white
                if (RK_col.GetCellColor("-Y", YNCoord[n, 0], YNCoord[n, 1]) == incol)
                { // -Y33 should be target color
                    if (n != 0)
                    {
                        tempRot = (n == 3) ? -90 : n * 90;
                        SolveScript.Add("Y, -2, " + tempRot.ToString());
                    }
                    break;
                }
            } // make -X33 white
            SolveScript.Add("Z, 1, 180");
            SolveScript.Add("Y, 2, 90");
            SolveScript.Add("Z, 0, 180");
            SolveScript.Add("Y, 2, -90");
            SolveScript.Add("Z, 1, -180");
            SolveScript.Add("Y, 2, 90");
            SolveScript.Add("Z, 0, -180");
        }
    }
}