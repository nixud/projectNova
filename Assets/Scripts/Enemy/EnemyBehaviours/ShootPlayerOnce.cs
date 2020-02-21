using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayerOnce : EnemyBehaviours
{
    public ShootPlayerOnce(GameObject Obj,float t)
    {
        time = t;
        this.gameObject = Obj;
        //Debug.Log("静止");
    }

    public override Vector3 Calculate(float Dt) {
        return new Vector3(0,0,0);
    }
    public override void Run(float Dt) {
        Vector3 dir = -(gameObject.transform.position - GameObject.Find("Player").transform.position).normalized;
        gameObject.GetComponent<EnemyControl>().Shoot(dir);
    }
}