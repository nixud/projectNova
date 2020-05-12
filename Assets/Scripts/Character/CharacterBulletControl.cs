using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 角色子弹管理
public class CharacterBulletControl : MonoBehaviour
{
    [HideInInspector]public List<GameObject> bullets;

    public event Action<GameObject> AddBullet;
    public event Action<GameObject> RemoveBullet;

    // 子弹添加时调用
    public void OnAddBullet(GameObject bullet)
    {
        bullets.Add(bullet);
        AddBullet?.Invoke(bullet);
    }

    // 子弹回收时调用
    public void OnRemoveBullet(GameObject bullet)
    {
        if (!bullets.Contains(bullet))
            return;
        bullets.Remove(bullet);
        RemoveBullet?.Invoke(bullet);
    }
}
