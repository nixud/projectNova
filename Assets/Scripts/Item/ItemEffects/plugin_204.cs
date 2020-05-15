using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 子弹分裂
public class plugin_204 : ItemEffects
{
    private readonly string catString = "plugin_204";
    private readonly float distance = 0.1f;
    private readonly float angle = 35f;

    private CharacterBulletControl _characterBulletControl;
    
    private Dictionary<GameObject, float> bulletGone;

    public override void Run()
    {
        _characterBulletControl = GameObject.Find("Player").GetComponent<CharacterBulletControl>();
        _characterBulletControl.OnAddBullet += OnAddBullet;
        _characterBulletControl.OnRemoveBullet += OnRemoveBullet;
        
        bulletGone = new Dictionary<GameObject, float>();
    }

    public override void Update()
    {
        foreach (var k in bulletGone.Keys.ToList())
        {
            if (bulletGone[k] >= distance)
            {
                Debug.Log("askjdaksjd");
                var bullet1 = ObjectPool.GetInstance().GetObj(k.name, "Bullet");
                var bullet2 = ObjectPool.GetInstance().GetObj(k.name, "Bullet");

                bullet1.transform.position = k.transform.position;
                bullet2.transform.position = k.transform.position;

                var dir = k.GetComponent<BulletHelper>().bulletNew.dir;
                bullet1.transform.rotation = k.transform.rotation;
                bullet1.transform.Rotate(new Vector3(0, 0, angle));
                bullet1.GetComponent<BulletHelper>().bulletNew.dir = Quaternion.Euler(0, 0, angle) * dir;
                bullet2.transform.rotation = k.transform.rotation;
                bullet2.transform.Rotate(new Vector3(0, 0, -angle));
                bullet2.GetComponent<BulletHelper>().bulletNew.dir = Quaternion.Euler(0, 0, -angle) * dir;

                bullet1.name = catString + bullet1.name;
                bullet2.name = catString + bullet2.name;
                
                bulletGone.Remove(k);
                _characterBulletControl.AddBullet(bullet1);
                _characterBulletControl.AddBullet(bullet2);
            }
            else
            {
                Debug.Log(bulletGone[k]);
                bulletGone[k] += k.GetComponent<BulletHelper>().bulletNew.GetSpeed * Time.deltaTime;
            }
        }
    }

    public override void End()
    {
        _characterBulletControl.OnAddBullet -= OnAddBullet;
        _characterBulletControl.OnRemoveBullet -= OnRemoveBullet;
    }

    private void OnAddBullet(GameObject bullet)
    {
        var index = bullet.name.IndexOf(catString, StringComparison.Ordinal);
        if (index != -1)
        {
            bullet.name = bullet.name.Remove(index, catString.Length);
        }
        else
        {
            bulletGone.Add(bullet, 0f);
        }
    }

    private void OnRemoveBullet(GameObject bullet)
    {
        try
        {
            bulletGone.Remove(bullet);
        }
        catch (Exception e)
        {
            // ignore
        }
    }

    public override bool Condition()
    {
        return true;
    }
}
