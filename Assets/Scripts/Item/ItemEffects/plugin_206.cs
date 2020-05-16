using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 大号子弹
public class plugin_206 : ItemEffects
{
    private float scaleRate;
    private float speedRate;
    private float damageRate;
    
    private CharacterBulletControl characterBulletControl;
    
    public plugin_206()
    {
        scaleRate = Mathf.Sqrt(2f);
        speedRate = 0.5f;
        damageRate = 2f;
        
    }
    
    public override void Run()
    {
        characterBulletControl = GameObject.Find("Player").GetComponent<CharacterBulletControl>();

        characterBulletControl.OnAddBullet += OnAddBullet;
        characterBulletControl.OnRemoveBullet += OnRemoveBullet;
        Debug.Log("206 run");
    }

    public override void Update()
    {
        return;
    }

    public override void End()
    {
        try
        {
            characterBulletControl.OnAddBullet -= OnAddBullet;
            characterBulletControl.OnRemoveBullet -= OnRemoveBullet;
            foreach (var bullet in characterBulletControl.bullets)
            {
                OnRemoveBullet(bullet);
            }
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

    private void OnAddBullet(GameObject bullet)
    {
        bullet.transform.localScale = bullet.transform.localScale * scaleRate;
        BulletHelper bulletHelper = bullet.GetComponent<BulletHelper>();
        bulletHelper.bulletNew.SpeedRate *= speedRate;
        bulletHelper.bulletNew.Damage *= damageRate;
    }

    private void OnRemoveBullet(GameObject bullet)
    {
        bullet.transform.localScale = bullet.transform.localScale / scaleRate;
        BulletHelper bulletHelper = bullet.GetComponent<BulletHelper>();
        bulletHelper.bulletNew.SpeedRate /= speedRate;
        bulletHelper.bulletNew.Damage /= damageRate;
    }
}
