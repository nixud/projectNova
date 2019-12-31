using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviours
{
    public GameObject gameObject;

    public abstract Vector3 Calculate();
    public abstract void Run();
}
