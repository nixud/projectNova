using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Quit : MonoBehaviour
{
    public EitherOrBoxControl eitherOrBoxControl;
    public string eitherOrBoxText;

    private Vector2 _eitherOrBoxposition = new Vector2(0, -113);
    private void Start()
    {
    }

    public void OnClick()
    {
        eitherOrBoxControl.SetInfo(gameObject, eitherOrBoxText, onTrue:Function);
        eitherOrBoxControl.Show(gameObject);
    }
    
    public void Function()
    {
        // debug
        Debug.Log("quit");
        Application.Quit();
    }
}
