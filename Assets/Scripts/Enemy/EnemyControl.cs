﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float HP = 10;
    public float maxHP = 10;

    public string DestoryEffect;

    public bool IsNotRecycled = true;

    List<WeaponControlEnemy> weaponControls = new List<WeaponControlEnemy>();
    public List<GameObject> shootPoints = new List<GameObject>();

    public List<string> WeaponName = new List<string>();

    public virtual void Hitted(float hp)
    {
        HP -= hp;
        if (HP <= 0 && IsNotRecycled) {
            RecycleNow();
            IsNotRecycled = false;
        }
    }

    public void Awake()
    {
        HP = maxHP;
        IsNotRecycled = true;

        for (int i = 0; i < WeaponName.Count; i++)
        {
            weaponControls.Add(gameObject.AddComponent<WeaponControlEnemy>());
            weaponControls[i].WeaponName = WeaponName[i];
            weaponControls[i].LoadWeapon(WeaponName[i]);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterControl>().DecHP();
            RecycleNow();
        }
    }

    public void RecycleNow()
    {
        Destroy(gameObject);
        Camera.main.GetComponent<StageIniter>().KilledOneEnemy();
        ScoreData.Instance.levelScore++;
    }

    public void Shoot(Vector3 dir)
    {
        for (int i = 0; i < weaponControls.Count; i++)
            weaponControls[i].Shoot(shootPoints[i].transform.position, dir);
    }
}
