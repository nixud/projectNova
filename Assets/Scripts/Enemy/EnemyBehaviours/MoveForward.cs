using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : EnemyBehaviours
{
    private float NowSpeed;
    private Vector3 Dir;

    private Vector3 direction;

    public MoveForward(GameObject Obj, float NowSpeed, Vector3 dir, float t)
    {
        time = t;
        this.NowSpeed = NowSpeed;
        this.gameObject = Obj;
        Dir = dir;

        // if (Mathf.Abs(dir.x) > 0.001f)
        // {
        //     gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Asin(dir.y / dir.x));
        //     Debug.Log(Mathf.Asin(dir.y / dir.x));
        // }

    }

    public override Vector3 Calculate(float Dt) {
        return Vector3.down * (NowSpeed * Dt);
    }

    public override void Start()
    {
        direction = gameObject.transform.up;
        gameObject.transform.up = -Dir;
    }

    public override void Run(float Dt) {
        gameObject.transform.Translate(Calculate(Dt));
    }

    public override void End()
    {
        gameObject.transform.up = direction;
    }
}