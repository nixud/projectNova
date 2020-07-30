using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Printer : MonoBehaviour
{
    public Text Text;

    public void Print(string s)
    {
        StartCoroutine(print(s));
    }

    IEnumerator print(string s)
    {
        for (int i = 0; i <= s.Length; i++)
        {
            Text.text = s.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
