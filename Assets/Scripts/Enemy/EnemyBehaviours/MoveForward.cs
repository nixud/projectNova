using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : EnemyBehaviours
{
    private float NowSpeed;
    private Vector3 Dir;

    public MoveForward(GameObject Obj, float NowSpeed, Vector3 dir, float t)
    {
        time = t;
        this.NowSpeed = NowSpeed;
        this.gameObject = Obj;
        if (Mathf.Abs(dir.x) > 0.001f)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Asin(dir.y / dir.x));
            Debug.Log(Mathf.Asin(dir.y / dir.x));
        }

        // if (dir == new Vector3(0, 1, 0))
        // {
        //     gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
        //     Debug.Log("掉头");
        // }
        // else if (dir == new Vector3(1, 0, 0))
        // {
        //     gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
        // }
        // else if (dir == new Vector3(-1, 0, 0))
        // {
        //     gameObject.transform.rotation = Quaternion.Euler(0, 0, -90);
        // }
        Dir = dir;
    }

    public override Vector3 Calculate(float Dt) {
        return Dir * NowSpeed * Dt;
    }
    public override void Run(float Dt) {
        gameObject.transform.Translate(Calculate(Dt));
    }
}