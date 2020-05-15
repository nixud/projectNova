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

    public bool isTrace;            //子弹是否追踪
    public float attackArrange;     //子弹追踪的范围
    public float rotateSpeed;       //子弹追踪时旋转角度
    protected GameObject attackTarget;

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
            if (isTrace)
            {
                CheckAttackTarget();
                if (attackTarget != null)
                {
                    Vector2 Direction = attackTarget.transform.position - gameObject.transform.position;
                    Vector2 lerp = Vector2.Lerp(gameObject.transform.up, Direction, rotateSpeed * 0.01f);
                    gameObject.transform.up = (new Vector2(lerp.x, lerp.y)).normalized;
                }
                transform.Translate(gameObject.transform.up * (NowSpeed * SpeedRate), Space.World);
            }
            else
            {
                transform.Translate(dir * (NowSpeed * SpeedRate));
            }
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

    // 获取当前速度
    public float GetSpeed
    {
        get => NowSpeed;
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

    protected void CheckAttackTarget()
    {
        if (isPlayerBullet)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, attackArrange);

            if (cols.Length > 0)
            {
                List<float> distanceList = new List<float>();
                Dictionary<GameObject, float> colsDic = new Dictionary<GameObject, float>();
                for (int i = 0; i < cols.Length; i++)
                {
                    if (cols[i].transform.CompareTag("Boss") || cols[i].transform.CompareTag("Enemy"))
                    {
                        float distance = (cols[i].transform.position - transform.position).magnitude;
                        if (colsDic.ContainsKey(cols[i].gameObject))
                        {
                            colsDic.Add(cols[i].gameObject, distance);
                        }
                        else
                        {
                            colsDic[cols[i].gameObject] = distance;
                        }
                        if (!distanceList.Contains(distance))
                        {
                            distanceList.Add(distance);
                        }
                    }
                }
                if (distanceList.Count > 0)
                {
                    distanceList.Sort();
                    foreach (KeyValuePair<GameObject, float> c in colsDic)
                    {

                        if (c.Value == distanceList[0])
                        {
                            attackTarget = c.Key.gameObject;
                            dir = attackTarget.transform.position - transform.position;
                            break;
                        }
                        else { attackTarget = null; }
                    }
                }
                else{ attackTarget = null; }
                
            }
            else { attackTarget = null; }
        }
        else
        {
            attackTarget = GameObject.Find("Player");
        }
    }
}