using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float HP = 10;
    public float maxHP = 10;

    public string DestoryEffect;

    public bool IsNotRecycled = true;

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
}
