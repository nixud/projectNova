using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayHere : EnemyBehaviours
{
    public StayHere(GameObject Obj) {
        this.gameObject = Obj;
    }

    public override Vector3 Calculate() {
        return new Vector3(0,0,0);
    }
    public override void Run() {
        gameObject.transform.Translate(Calculate());
    }
}