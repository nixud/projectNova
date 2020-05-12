using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plugin_205 : ItemEffects
{
    private Dictionary<GameObject, ChangeShapeInfo> bulletsAndTime;
    private float scaleRate;
    private float damageRate;
    private CharacterBulletControl characterBulletControl;

    public plugin_205()
    {
        bulletsAndTime = new Dictionary<GameObject, ChangeShapeInfo>();
        scaleRate = Mathf.Sqrt(1.4f);
        damageRate = 1.1f;
        
        characterBulletControl = GameObject.Find("Player").GetComponent<CharacterBulletControl>();
    }
    
    public override void Run()
    {
        characterBulletControl.AddBullet += OnAddBullet;
        characterBulletControl.RemoveBullet += OnRemoveBullet;
    }

    public override void Update()
    {
        foreach (var f in bulletsAndTime)
        {
            f.Value.existTime += Time.deltaTime;
            if (f.Value.existTime >= 0.3f)
            {
                ChangeBulletShape(f.Key);
                f.Value.existTime -= 0.3f;
            }
        }
    }

    public override void End()
    {
        try
        {
            characterBulletControl.AddBullet -= OnAddBullet;
            characterBulletControl.RemoveBullet -= OnRemoveBullet;

            foreach (var f in bulletsAndTime)
            {
                OnRemoveBullet(f.Key);
            }
            
            bulletsAndTime.Clear();
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
        bulletsAndTime.Add(bullet, new ChangeShapeInfo());
    }

    private void OnRemoveBullet(GameObject bullet)
    {
        for (int i = 0; i < bulletsAndTime[bullet].count; i++)
        {
            bullet.transform.localScale = bullet.transform.localScale / scaleRate;
            bullet.GetComponent<BulletHelper>().bulletNew.Damage /= damageRate;
        }

        bulletsAndTime.Remove(bullet);
    }

    private void ChangeBulletShape(GameObject bullet)
    {
        bullet.transform.localScale = bullet.transform.localScale * scaleRate;
        bullet.GetComponent<BulletHelper>().bulletNew.Damage *= damageRate;
    }
    
    private class ChangeShapeInfo
    {
        public int count;
        public float existTime;
        
        public ChangeShapeInfo()
        {
            count = 0;
            existTime = 0f;
        }
    }
}
