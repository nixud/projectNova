using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : EnemyBehaviours
{
    private float NowSpeed;
    private Vector3 Dir;

    /// <summary>
    /// 第二个参数是速度，第三个是方向，第四个是时间。直线运动。
    /// </summary>
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

    public override void Start()
    {
        
    }

    public override void Run(float Dt) {
        gameObject.transform.Translate(Calculate(Dt));
    }

    public override void End()
    {
        
    }
}