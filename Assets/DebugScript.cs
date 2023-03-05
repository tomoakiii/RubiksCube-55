using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScript : MonoBehaviour
{
    public GameObject DebugLogText;
    private Text debugstr;
    // Start is called before the first frame update
    void Start()
    {
        debugstr = DebugLogText.GetComponent<Text>();
        Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clear()
    {
        debugstr.text = "";
    }

    public void set(string str)
    {
        debugstr.text = str;
    }

    public string get(string str)
    {
        return debugstr.text;
    }

    public void copy()
    {
        GUIUtility.systemCopyBuffer = debugstr.text;
    }

    

}
