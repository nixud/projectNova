using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 子弹分裂
public class plugin_204 : ItemEffects
{
    private readonly string catString = "plugin_204";
    private readonly string limit102 = "equip_102";
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
        
        Debug.Log("204 run");
    }

    public override void Update()
    {
        foreach (var k in bulletGone.Keys.ToList())
        {
            if (bulletGone[k] >= distance)
            {
                var name = k.name.Split(',').Last();
                var bullet1 = ObjectPool.GetInstance().GetObj(name, "Bullet");
                var bullet2 = ObjectPool.GetInstance().GetObj(name, "Bullet");

                bullet1.transform.position = k.transform.position;
                bullet2.transform.position = k.transform.position;

                var dir = k.GetComponent<BulletHelper>().bulletNew.dir;
                bullet1.transform.localScale = Vector3.one;
                bullet1.transform.rotation = k.transform.rotation;
                bullet1.transform.Rotate(new Vector3(0, 0, angle));
                bullet1.GetComponent<BulletHelper>().bulletNew.dir = Quaternion.Euler(0, 0, angle) * dir;
                bullet2.transform.localScale = Vector3.one;
                bullet2.transform.rotation = k.transform.rotation;
                bullet2.transform.Rotate(new Vector3(0, 0, -angle));
                bullet2.GetComponent<BulletHelper>().bulletNew.dir = Quaternion.Euler(0, 0, -angle) * dir;

                bullet1.name = catString + "," + bullet1.name;
                bullet2.name = catString + "," + bullet2.name;
                // 放置备用发射架再次触发
                bullet1.name = limit102 + "," + bullet1.name;
                bullet2.name = limit102 + "," + bullet2.name;

                bulletGone.Remove(k);
                _characterBulletControl.AddBullet(bullet1);
                _characterBulletControl.AddBullet(bullet2);
            }
            else
            {
                bulletGone[k] += k.GetComponent<BulletHelper>().bulletNew.GetSpeed * Time.deltaTime;
            }
        }
    }

    public override void End()
    {
        try
        {
            _characterBulletControl.OnAddBullet -= OnAddBullet;
            _characterBulletControl.OnRemoveBullet -= OnRemoveBullet;

            foreach (var g in bulletGone.Keys.ToList())
            {
                OnRemoveBullet(g);
            }
            bulletGone.Clear();
        }
        catch
        {
            // ignore
        }
    }

    private void OnAddBullet(GameObject bullet)
    {
        if (bullet.name.Split(',').Contains(catString))
        {
            return;
            // bullet.name = bullet.name.Remove(index, catString.Length);
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
