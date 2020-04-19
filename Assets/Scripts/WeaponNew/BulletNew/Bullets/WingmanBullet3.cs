using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingmanBullet3 : BulletNew
{
    // 用于贯穿的多段攻击
    public int HurtTimePerHit;
    // private List<GameObject> HittedObjects;

    public override void RecycleNow()
    {
        /*
        if (HittedObjects != null &&  HittedObjects.Count > 0)
        {
            HittedObjects.Clear();
        }
        */
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        if (IsNotRecycled)
        {
            if (DestoryEffect != null)
            {
                GameObject shootHitEffect = ObjectPool.GetInstance().GetObj(DestoryEffect, "BulletEffects");
                shootHitEffect.transform.position = transform.position;
            }
            Nowtime = 0;
            ObjectPool.GetInstance().RecycleObj(gameObject);
            IsNotRecycled = false;
        }
    }

    public override void HitRecycleNow()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        if (IsNotRecycled)
        {
            if (HitEffect != null)
            {
                GameObject shootHitEffect = ObjectPool.GetInstance().GetObj(HitEffect, "BulletEffects");
                shootHitEffect.transform.position = transform.position;
            }
            Nowtime = 0;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (isPlayerBullet && (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss"))
        {
            for (int i = 0;i < HurtTimePerHit; i++)
            {
                collision.gameObject.GetComponent<EnemyControl>().Hitted(Damage);
            }
            // HittedObjects.Add(collision.gameObject);
        }
        else if (!isPlayerBullet && collision.gameObject.tag == "Player")
        {
            for (int i = 0; i < HurtTimePerHit; i++)
            {
                collision.gameObject.GetComponent<CharacterControl>().DecHP();
            }
            // HittedObjects.Add(collision.gameObject);
        }
    }


}
