using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : EnemyBehaviours
{
    private float NowSpeed;
    private Vector3 Dir;

    private GameObject Player;

    /// <summary>
    /// 第二个参数是速度，第三个是时间
    /// </summary>
    public Kamikaze(GameObject Obj, float NowSpeed, float t)
    {
        time = t;
        this.NowSpeed = NowSpeed;
        this.gameObject = Obj;

        Player = GameObject.Find("Player");
        Dir = (Player.transform.position - gameObject.transform.position).normalized;

        Dir.Normalize();
        float eulers = -Mathf.Atan(Dir.x / Dir.y) * 180 / Mathf.PI;
        gameObject.transform.Rotate(new Vector3(0, 0, eulers));
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