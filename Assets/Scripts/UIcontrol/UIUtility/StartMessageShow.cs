using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMessageShow : MonoBehaviour
{
    private float endTime = 1f;


    public void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(MoveToShow());
    }

    private IEnumerator MoveToShow()
    {
        yield return new WaitForSeconds(endTime);
        gameObject.SetActive(false);
    }
}
