using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubsScript : MonoBehaviour
{
    public GameObject textBox;

    string[] script = new string[5] 
    {
        "Yeehaw",
        "Mothefrigger",
        "You want some of this cowboy ass?",
        "*walks over Yeehaw like*",
        "Gimme all your lassos",
    };

    void Start()
    {
        StartCoroutine(TheSequence());
    }

    IEnumerator TheSequence()
    {
        for (int i = 0; i < script.Length; i++)
        {
            yield return new WaitForSeconds(1);
            textBox.GetComponent<Text>().text = script[i];
            yield return new WaitForSeconds(3);
            textBox.GetComponent<Text>().text = "";
        }
        

    }

}
