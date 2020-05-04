using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoint : EnemyBehaviours
{
    private Vector3 target;
    private float NowSpeed;

    public MoveToPoint(GameObject Obj, float NowSpeed, Vector3 tar)
    {
        gameObject = Obj;
        this.NowSpeed = NowSpeed;
        this.target = tar;
        time = float.MaxValue;
    }

    public override Vector3 Calculate(float Dt)
    {
        return Vector3.MoveTowards(gameObject.transform.position, target, NowSpeed * Dt);
    }

    public override void Start()
    {
        
    }


    public override void Run(float Dt)
    {
        gameObject.transform.position = Calculate(Dt);
        if (Vector3.Distance(gameObject.transform.position, target) <= 0.01f)
            gameObject.GetComponent<EnemyBehaviourController>().couldChangeBeh = true;
    }

    public override void End()
    {
        
    }
}
