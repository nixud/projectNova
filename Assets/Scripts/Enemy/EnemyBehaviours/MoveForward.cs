using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : EnemyBehaviours
{
    private float NowSpeed;

    public MoveForward(GameObject Obj, float NowSpeed, float t)
    {
        time = t;
        this.NowSpeed = NowSpeed;
        this.gameObject = Obj;
        Debug.Log("向前移动");
    }

    public override Vector3 Calculate(float Dt) {
        return new Vector3(0,-1f,0) * NowSpeed * Dt;
    }
    public override void Run(float Dt) {
        gameObject.transform.Translate(Calculate(Dt));
    }
}