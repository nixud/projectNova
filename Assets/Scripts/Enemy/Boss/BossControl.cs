using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossControl : EnemyControl
{
    private GameObject BossBlood;
    private void Start()
    {
        BossBlood = GameObject.Find("BossBlood");
        BossBlood.GetComponent<Slider>().value = Mathf.Clamp01(HP / maxHP);
    }
    public override void Hitted(float hp)
    {
        HP -= hp;
        BossBlood.GetComponent<Slider>().value = Mathf.Clamp01(HP / maxHP);
        if (HP <= 0) BossDestoryed();
    }

    public void BossDestoryed() {
        SceneManager.LoadScene("StartScene");
    }
}
