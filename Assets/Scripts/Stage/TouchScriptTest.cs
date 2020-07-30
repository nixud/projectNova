using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TouchScriptTest : MonoBehaviour
{
    public Text enemyDesc;
    
    public StageSceneNew stageSceneNew;
    public float range = 0.3f;

    public GameObject Light;
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
            if (Vector2.Distance(position, gameObject.transform.position) < range)
            {
                if (Vector2.Distance(stageSceneNew.PlayerPlane.transform.position, gameObject.transform.position) <
                    stageSceneNew.Range)
                    stageSceneNew.StagePointPressed(gameObject);
            }

            enemyDesc.text = "描仪显示该星域内存在帝国守军";

            // StageDiffculty diff;
            // try
            // {
            //     diff = gameObject.GetComponent<StageDiffculty>();
            //     Debug.Log(diff.Diffculty);
            //     switch (diff.Diffculty)
            //     {
            //         case "Easy":
            //             enemyDesc.text = "扫描仪显示该星域内存在小股帝国守军";
            //             break;
            //         case "Medium":
            //             enemyDesc.text = "扫描仪显示该星域内存在大股帝国守军";
            //             break;
            //         case "Hard":
            //             enemyDesc.text = "扫描仪显示该星域内存在帝国要塞";
            //             break;
            //     }
            // }
            // catch (Exception e)
            // {
            //     enemyDesc.text = "扫描仪显示该星域内存在帝国星系中枢";
            // }
        }
    }

}
