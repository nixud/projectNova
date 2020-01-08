using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEffects
{
    public GameObject gameObject;
    public float time = 0;

    public abstract void Run();
    public abstract void End();
}
