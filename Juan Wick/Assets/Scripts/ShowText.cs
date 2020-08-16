using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour
{

    //Stores text so it can be output within the OnGUI function.
    private string say;


    //When true, text is displayed each frame, when off it iss't displayed
    private bool displaying;

    //This text style can can be easily changed in the inspector
    public GUIStyle myStyle;

    //This function can be called from other scripts. You must provide the text, the starting positions
    //and the time the text will last.
    public void DisplayTextHereFor(string text, float distFromLeft, float distFromBottom, float time)
    {
        say = text;
        displaying = true;
        Invoke("StopDisplaying", time);
    }

    //Draws the text on the GUI while "displaying" is true
    void OnGUI()
    {
        if (displaying)
        {
            myStyle.fontSize = 50;
            myStyle.alignment = TextAnchor.MiddleCenter;
            myStyle.fontStyle = FontStyle.Bold;
            GUI.Label(new Rect(0, Screen.height - 180, Screen.width, 150), say, myStyle);
            Debug.Log(myStyle.alignment);
        }

    }

    //Simply says to stop displaying
    void StopDisplaying()
    {
        displaying = false;
    }
}
