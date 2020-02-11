using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : EnemyBehaviours
{
    private float NowSpeed;
    private Vector3 Dir;

    public Move(GameObject Obj, float NowSpeed, Vector3 dir, float t)
    {
        time = t;
        this.NowSpeed = NowSpeed;
        this.gameObject = Obj;
        Dir = dir;
    }

    public override Vector3 Calculate(float Dt) {
        return Dir * NowSpeed * Dt;
    }
    public override void Run(float Dt) {
        gameObject.transform.Translate(Calculate(Dt));
    }
}