using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    public AnimationCurve Speed;

    public string BulletBody;
    public string HitEffect;
    public string DestoryEffect;
    public string ShootGunEffect;

    public AudioClip FireAudio;
    public AudioClip HitAudio;

    public Vector3 dir;
    public Vector3 Scale = new Vector3(1,1,1);

    public bool isPlayerBullet;

    public bool CanTrackEnemy;
    private float MaximumRotationSpeed = 120.0f;

    private float NowSpeed;
    private float Nowtime=0;

    private bool IsNotRecycled = false;

    public int SplitNum;
    public bool Issplit;
    public string SplitBulletNumber;
    public float splitRange;
    public float Accuracy;//0-1
    public int ShotgunNum;
    public int ShotgunRandomNum;

    [HideInInspector]
    public int SplitedNum = 0;
    public float splitedRange = 1;
    public float SplitedScale = 1;

    public Transform Target = null;

    public void ActiveIt()
    {
        //Debug.Log("这行倒霉代码已经被执行了");
        transform.localScale = Scale;
        SplitedNum = 0;
        splitedRange = 1;
        SplitedScale = 1;
    }

    private void Update() {
        if (IsNotRecycled) {
            Nowtime += Time.deltaTime;
            NowSpeed = Speed.Evaluate(Nowtime);

            if (!CanTrackEnemy)
            {
                transform.Translate(dir * NowSpeed);
            }
            else {
                float deltaTime = Time.deltaTime;

                NowSpeed *= 20;

                Vector2 offset = (Target.position - transform.position).normalized;

                float angle = Vector2.Angle(transform.forward, offset);

                float needTime = angle / (MaximumRotationSpeed * NowSpeed);

                transform.forward = Vector3.Slerp(transform.forward, offset, deltaTime / needTime).normalized;

                transform.position += transform.forward * NowSpeed * deltaTime;
            }

            if (Issplit && SplitNum > SplitedNum) {
                RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0,0.5f,0), transform.forward + dir);

                if (hit.collider != null && hit.distance <= splitRange * splitedRange && hit.collider.tag == "Enemy")
                {
                    float maxdegree = Accuracy * Mathf.PI / 2;
                    int BulletNumber = ShotgunNum;
                    for (int i = 0; i < BulletNumber; i++)
                    {
                        float angle = -maxdegree + i * (2 * maxdegree) / (BulletNumber - 1);

                        GameObject bullet = ObjectPool.GetInstance().GetObj(BulletBody, "Bullets");
                        bullet.GetComponent<Bullet>().SplitedNum = SplitedNum+1;
                        Vector3 dir = transform.forward + new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);
                        dir.Normalize();

                        bullet.GetComponent<Bullet>().SplitedScale = SplitedScale / 2;
                        bullet.transform.localScale *= bullet.GetComponent<Bullet>().SplitedScale;
                        bullet.GetComponent<Bullet>().splitedRange = splitedRange/2;
                        bullet.GetComponent<Bullet>().dir = dir;
                        bullet.GetComponent<Bullet>().transform.position = transform.position;
                    }
                    RecycleNow();
                }
            }
        }
    }

    private void OnEnable() {
        StartCoroutine(AutoRecycle());
        //Target = GameObject.Find("New Sprite").transform;
        IsNotRecycled = true;
    }

    IEnumerator AutoRecycle()
    {
        yield return new WaitForSeconds(15f);

        RecycleNow();
    }
    void RecycleNow() {
        if (IsNotRecycled)
        {
            if (DestoryEffect != null)
            {
                GameObject shootHitEffect = ObjectPool.GetInstance().GetObj(DestoryEffect, "BulletEffects");
                shootHitEffect.transform.position = transform.position;
            }
            ObjectPool.GetInstance().RecycleObj(gameObject);
            IsNotRecycled = false;
        }
    }
    void HitRecycleNow()
    {
        if (IsNotRecycled)
        {
            if (HitEffect != null)
            {
                GameObject shootHitEffect = ObjectPool.GetInstance().GetObj(HitEffect, "BulletEffects");
                shootHitEffect.transform.position = transform.position;
            }
            ObjectPool.GetInstance().RecycleObj(gameObject);
            IsNotRecycled = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isPlayerBullet && (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss"))
        {
            collision.gameObject.GetComponent<EnemyControl>().Hitted(Damage);
            HitRecycleNow();
        }
        else if (!isPlayerBullet && collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<CharacterControl>().DecHP();
            HitRecycleNow();
        }
    }

}
