﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchScriptTest : MonoBehaviour
{
    public StageSceneNew stageSceneNew;
    public float range;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(position, gameObject.transform.position) < range) {
                stageSceneNew.StagePointPressed(gameObject);
            }
        }
    }

}
