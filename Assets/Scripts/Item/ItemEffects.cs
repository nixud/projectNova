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
    
    /// <summary>
    /// 道具除法条件
    /// </summary>
    /// <returns></returns>
    public abstract bool Condition();
}
