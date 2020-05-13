using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 角色子弹管理
public class CharacterBulletControl : MonoBehaviour
{
    [HideInInspector]public List<GameObject> bullets;

    public event Action<GameObject> OnAddBullet;
    public event Action<GameObject> OnRemoveBullet;

    // 子弹添加时调用
    public void AddBullet(GameObject bullet)
    {
        bullets.Add(bullet);
        OnAddBullet?.Invoke(bullet);
    }

    // 子弹回收时调用
    public void RemoveBullet(GameObject bullet)
    {
        if (!bullets.Contains(bullet))
            return;
        bullets.Remove(bullet);
        OnRemoveBullet?.Invoke(bullet);
    }
}
