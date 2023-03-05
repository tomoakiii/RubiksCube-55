using System.Collections;
using System.Collections.Generic;
using TMPro;
//using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class InputScript : MonoBehaviour
{
    public GameObject InputText;
    private TMP_InputField inputstr;
    // Start is called before the first frame update
    void Start()
    {
        inputstr = InputText.GetComponent<TMP_InputField>();
        Clear();
    }

    public void Clear()
    {
        inputstr.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string get()
    {
        return inputstr.text;
    }
}
