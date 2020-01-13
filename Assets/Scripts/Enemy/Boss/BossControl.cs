using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossControl : EnemyControl
{
    private GameObject BossBlood;
    private void Start()
    {
        BossBlood = GameObject.Find("BossBlood");
    }
    public override void Hitted(float hp)
    {
        Debug.Log("hhh");
        HP -= hp;
        BossBlood.GetComponent<Slider>().value = Mathf.Clamp01(HP / maxHP);
        if (HP <= 0) RecycleNow();
    }
}
