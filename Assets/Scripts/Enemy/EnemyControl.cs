using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float HP = 10;
    public float maxHP = 10;

    public string DestoryEffect;

    public bool IsNotRecycled = true;

    public virtual void Hitted(float hp)
    {
        HP -= hp;
        if (HP <= 0 && IsNotRecycled) { 
            RecycleNow();
            IsNotRecycled = false;
        }
    }

    public void Awake()
    {
        HP = maxHP;
        IsNotRecycled = true;
    }
    /*
    public void RecycleNow()
    {
        if (IsNotRecycled)
        {
            if (DestoryEffect != null && DestoryEffect!="")
            {
                GameObject shootHitEffect = ObjectPool.GetInstance().GetObj(DestoryEffect, "EnemyDestoryEffects");
                shootHitEffect.transform.position = transform.position;
            }
            ObjectPool.GetInstance().RecycleObj(gameObject);
            HP = maxHP;
            IsNotRecycled = false;
        }
    }*/
    public void RecycleNow()
    {
        Destroy(gameObject);
        Camera.main.GetComponent<StageIniter>().KilledOneEnemy();
        ScoreData.Instance.levelScore++;
    }
}
