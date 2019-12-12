using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewBehaviourScript : MonoBehaviour
{
    public AnimationCurve test;
    bool isMoveToTarget = true;
    // Start is called before the first frame update
    void Start()
    {
        //DOTween.Init();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(test.Evaluate(Time.time));
    }
}
