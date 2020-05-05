using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardChangeVelocity : EnemyBehaviours
{
    private float NowSpeed;
    private Vector3 Dir;

    private float changeRate;

    private Vector3 direction;

    public MoveForwardChangeVelocity(GameObject gobj, float startSpeed, float endSpeed, Vector3 dir, float t)
    {
        gameObject = gobj;
        time = t;
        NowSpeed = startSpeed;
        Dir = dir;

        changeRate = (endSpeed - startSpeed) / t;
    }
    
    public override Vector3 Calculate(float Dt)
    {
        return Vector3.down * (NowSpeed * Dt);
    }

    public override void Start()
    {
        direction = gameObject.transform.up;
        gameObject.transform.up = -Dir;
    }

    public override void Run(float Dt)
    {
        gameObject.transform.Translate(Calculate(Dt));
        NowSpeed += changeRate * Dt;
    }

    public override void End()
    {
        gameObject.transform.up = direction;
    }
}
