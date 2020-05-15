using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 备用发射架
public class equip_102 : ItemEffects
{
    private readonly string catString = "equip_102";
    private CharacterBulletControl characterBulletControl;

    public equip_102()
    {
        time = 10f;
    }
    
    public override void Run()
    {
        characterBulletControl = GameObject.Find("Player").GetComponent<CharacterBulletControl>();
        characterBulletControl.OnAddBullet += OnAddBullet;
    }

    public override void Update()
    {
    }

    public override void End()
    {
        characterBulletControl.OnAddBullet -= OnAddBullet;
    }

    private void OnAddBullet(GameObject bullet)
    {
        var index = bullet.name.IndexOf(catString, StringComparison.Ordinal);
        if (index != -1)
        {
            // Debug.Log(bullet.name);
            bullet.name = bullet.name.Remove(index, catString.Length);
            // Debug.Log("remo: " + bullet.name);
        }
        else
        {
            var result = ObjectPool.GetInstance().GetObj(bullet.name, "Bullets");
            result.name = catString + result.name;
            result.transform.localScale = bullet.transform.localScale;
            result.transform.position = new Vector3(bullet.transform.position.x + 0.2f, bullet.transform.position.y + 0.3f);
            result.transform.rotation = bullet.transform.rotation;

            result.GetComponent<BulletHelper>().bulletNew.dir = bullet.GetComponent<BulletHelper>().bulletNew.dir;
            
            characterBulletControl.AddBullet(result);
        }
    }

    public override bool Condition()
    {
        return true;
    }
}
