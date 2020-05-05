using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardToPoint : EnemyBehaviours
{
    private Vector3 target;
    private float NowSpeed;

    private Vector3 direction;

    public MoveForwardToPoint(GameObject Obj, float NowSpeed, Vector3 tar)
    {
        gameObject = Obj;
        this.NowSpeed = NowSpeed;
        this.target = tar;
        time = float.MaxValue;
    }
    
    public override Vector3 Calculate(float Dt)
    {
        return Vector3.down * (NowSpeed * Dt);
    }

    public override void Start()
    {
        direction = gameObject.transform.up;
        var temp = target - gameObject.transform.position;
        gameObject.transform.up = -temp.normalized;
    }

    public override void Run(float Dt)
    {
        gameObject.transform.Translate(Calculate(Dt));
        if (Vector3.Distance(gameObject.transform.position, target) <= 0.01f)
            gameObject.GetComponent<EnemyBehaviourController>().couldChangeBeh = true;
    }

    public override void End()
    {
        gameObject.transform.up = direction;
    }
}
