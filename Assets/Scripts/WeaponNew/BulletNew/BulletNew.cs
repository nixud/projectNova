using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNew : MonoBehaviour
{
    public float Damage;//伤害（敌人的子弹也要有该值，但敌人子弹伤害固定为一点）
    public AnimationCurve Speed;//速度曲线

    [HideInInspector]public float SpeedRate = 1f;        // 速度倍率，调整子弹射速

    protected float NowSpeed;//当前速度
    protected float Nowtime = 0;//当前飞行的时间

    public float AutoRecycleTime = 10f;//最长生存时间（超过该时间会被自动回收）

    public string BulletBody;
    public string HitEffect;
    public string DestoryEffect;
    public string ShootGunEffect;

    public bool isPlayerBullet;

    public AudioClip FireAudio;//声音
    public AudioClip HitAudio;

    public Vector3 dir;//方向
    public Vector3 Scale = new Vector3(1, 1, 1);

    protected bool IsNotRecycled;//标记是否被回收的开关

    public float eulers;

    public virtual void ActiveIt()
    {
        transform.localScale = Scale;
    }

    protected virtual void FixedUpdate()
    {
        if (IsNotRecycled)
        {
            Nowtime += Time.deltaTime;
            NowSpeed = Speed.Evaluate(Nowtime);

            transform.Translate(dir * (NowSpeed * SpeedRate), Space.World);
        }
    }

    public virtual void OnEnable()
    {
        StartCoroutine(AutoRecycle());
        IsNotRecycled = true;
    }

    public virtual IEnumerator AutoRecycle()
    {
        yield return new WaitForSeconds(AutoRecycleTime);
        RecycleNow();
    }

    public virtual void RecycleNow()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        if (IsNotRecycled)
        {
            if (DestoryEffect != null || DestoryEffect != "")
            {
                GameObject shootHitEffect = ObjectPool.GetInstance().GetObj(DestoryEffect, "BulletEffects");
                shootHitEffect.transform.position = transform.position;
            }
            Nowtime = 0;
            ObjectPool.GetInstance().RecycleObj(gameObject);
            IsNotRecycled = false;
            
            // 回收时从角色子弹控制删除
            OnRecycle();
        }
    }
    public virtual void HitRecycleNow()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        if (IsNotRecycled)
        {
            if (HitEffect != null || HitEffect != "")
            {
                GameObject shootHitEffect = ObjectPool.GetInstance().GetObj(HitEffect, "BulletEffects");
                shootHitEffect.transform.position = transform.position;
            }
            Nowtime = 0;
            ObjectPool.GetInstance().RecycleObj(gameObject);
            IsNotRecycled = false;
            
            // 回收时从角色子弹控制删除
            OnRecycle();
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (isPlayerBullet && (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss"))
        {
            collision.gameObject.GetComponent<EnemyControl>().Hitted(Damage);
            HitRecycleNow();
        }
        else if (!isPlayerBullet && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterControl>().DecHP();
            HitRecycleNow();
        }
    }

    // 从角色子弹控制删除
    protected void OnRecycle()
    {
        if (isPlayerBullet)
            GameObject.Find("Player").GetComponent<CharacterBulletControl>().RemoveBullet(gameObject);
    }
}