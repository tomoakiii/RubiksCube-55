using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Diagnostics;
public class LogScript : MonoBehaviour
{
    public GameObject LogText;
    private Text logstr;
    private int MAXLOGLINE = 15;
    private string logFilePath = "./LOG/";
    private string FileName;
    // Start is called before the first frame update
    void Start()
    {
        logstr = LogText.GetComponent<Text>();
        Clear();
        DateTime dt = DateTime.Now;
        FileName = logFilePath + dt.ToString("yyyyMMddHHmmss") + ".txt";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeLog(List<string> GameLog)
    {
        logstr.text = "";
        for (int n = GameLog.Count - 1; (n >= 0) && (n >= GameLog.Count - MAXLOGLINE); n--)
        {
            logstr.text = GameLog[n] + "\n" + logstr.text;
        }
    }

    [Conditional("UNITY_EDITOR")]
    public void MakeLog_FileWrite(List<string> GameLog)
    {
        using (System.IO.FileStream fs = new System.IO.FileStream(FileName, FileMode.Create))
        using (StreamWriter streamWriter = new StreamWriter (fs))
        {
            fs.SetLength(0);     
            for (int n = 0; n < GameLog.Count; n++)
            {
                streamWriter.WriteLine(GameLog[n]);
            }
        }
    }

    public void Clear()
    {
        logstr.text = "";
    }

    public void set(string str)
    {
        logstr.text = str;
    }

    public string get(string str)
    {
        return logstr.text;
    }

    public void copy()
    {
        GUIUtility.systemCopyBuffer = logstr.text;
    }


}
