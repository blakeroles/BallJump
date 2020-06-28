using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TakeScreenshot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #if UNITY_EDITOR
    [MenuItem("Tools/Take Screenshot")]
    #endif
    static public void OnTakeScreenShot()
    {
        #if UNITY_EDITOR
            ScreenCapture.CaptureScreenshot(EditorUtility.SaveFilePanel("Save Screenshot As", "test", "test", "png"));
        #endif
    }
}
