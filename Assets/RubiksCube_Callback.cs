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
 

    public void ResetButtonCallback()
    {
        Start();
    }

    public void RevButtonCallback()
    {
        isAutoMode = AutoMode.AutoResolveMode;
        for (int i = GameLog.Count - 1; i >= 0; i--)
        {
            SolveScript.Add(SingleRevOperation(GameLog[i]));
        }
    }

    public void UndoButtonCallback()
    {
        isAutoMode = AutoMode.AutoResolveMode;
        RotateCube_Command(SingleRevOperation(GameLog[GameLog.Count - 1]));
        GameLog.RemoveAt(GameLog.Count - 1); // Clear the log of Undo Rotation
        GameLog.RemoveAt(GameLog.Count - 1); // Clear the log of Target Rotation
        logscript.MakeLog(GameLog); // Synchronize display log
    }

    public string SingleRevOperation(string op)
    {
        string[] outop = op.Trim().Split(',');
        int tmpangle = -1 * Convert.ToInt32(outop[2]);
        return (outop[0] + ", " + outop[1] + ", " + tmpangle.ToString("F0"));
    }

    public void RandButtonCallback()
    {
        RandomIndex = 10;
        isAutoMode = AutoMode.Random10Mode;
    }

    public void RunButtonCallback()
    {
        StringReader sr = new StringReader(inputscript.get());
        string line;
        SolveScript.Clear();
        while ((line = sr.ReadLine()) != null) {
            SolveScript.Add(line);
        }
        inputscript.Clear();
        isAutoMode = AutoMode.AutoResolveMode;
    }

    public void CopyButtonCallback()
    {
        string str = "";
        for (int n = 0; n < GameLog.Count; n++)
        {
            str = str  + GameLog[n] + "\n";
        }
        GUIUtility.systemCopyBuffer = str;
    }
}

