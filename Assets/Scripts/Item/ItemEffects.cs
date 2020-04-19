using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//负责道具效果的抽象类。
public abstract class ItemEffects
{
    public GameObject gameObject;
    public float time = 0;

    public abstract void Run();
    public abstract void Update();
    public abstract void End();
    public abstract bool Condition();
}
