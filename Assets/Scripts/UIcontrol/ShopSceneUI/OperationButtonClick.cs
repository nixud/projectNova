using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperationButtonClick : MonoBehaviour
{
    public Text OperateName;
    public void OnPointerDown()
    {
        OperateName.color = Color.black;
    }

    public void OnPointerUp()
    {
        OperateName.color = Color.white;
    }
}
