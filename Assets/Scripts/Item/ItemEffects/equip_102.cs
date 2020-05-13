using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equip_102 : ItemEffects
{
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
        if (bullet.name.StartsWith("Clone"))
        {
            Debug.Log(bullet.name);
            bullet.name = bullet.name.Remove(0, 5);
            Debug.Log("remo: " + bullet.name);
        }
        else
        {
            var result = ObjectPool.GetInstance().GetObj(bullet.name, "Bullets");
            result.name = "Clone" + result.name;
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
