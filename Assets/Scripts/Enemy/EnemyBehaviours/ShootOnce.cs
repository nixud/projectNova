using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootOnce : EnemyBehaviours
{
    public ShootOnce(GameObject Obj,float t)
    {
        time = t;
        this.gameObject = Obj;
    }

    public override Vector3 Calculate(float Dt) {
        return new Vector3(0,0,0);
    }

    public override void Start()
    {
        
    }

    public override void Run(float Dt) {
        var dir = -gameObject.transform.up;
        gameObject.GetComponent<EnemyControl>().Shoot(dir);
    }

    public override void End()
    {
        
    }
}