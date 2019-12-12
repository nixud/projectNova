using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float HP = 100;

    public string DestoryEffect;

    private bool IsNotRecycled = false;

    public void Hitted(int hp)
    {
        HP -= hp;
        if (HP <= 0) RecycleNow();
    }

    void RecycleNow()
    {
        if (IsNotRecycled)
        {
            if (DestoryEffect != null)
            {
                GameObject shootHitEffect = ObjectPool.GetInstance().GetObj(DestoryEffect, "EnemyDestoryEffects");
                shootHitEffect.transform.position = transform.position;
            }
            ObjectPool.GetInstance().RecycleObj(gameObject);
            IsNotRecycled = false;
        }
    }
}
