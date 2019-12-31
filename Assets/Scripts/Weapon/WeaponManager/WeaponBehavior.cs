using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

public class WeaponBehavior : MonoBehaviour
{
    Weapon weapon;
    GameObject bullet;
    GameObject ray;

    bool IsInCD;
    bool IsRayhiting;
    bool IsRayOn;

    public WeaponBehavior(Weapon w) {
        weapon = w;
    }

    public virtual void Shoot(Vector3 shootPosition, Vector3 shootForward) {
        if (!IsInCD)
        {
            if (!weapon.IsAShotgun)
            {
                bullet = ObjectPool.GetInstance().GetObj(weapon.BulletNumber, "Bullets");
                bullet.GetComponent<Bullet>().dir = shootForward;
                bullet.GetComponent<Bullet>().transform.position = shootPosition;
            }
            else
            {
                float maxdegree = weapon.Accuracy * Mathf.PI / 2;
                int BulletNumber = weapon.ShotgunNum;
                for (int i = 0; i < BulletNumber; i++)
                {
                    float angle = -maxdegree + i * (2 * maxdegree) / (BulletNumber - 1);

                    bullet = ObjectPool.GetInstance().GetObj(weapon.BulletNumber, "Bullets");
                    Vector3 dir = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);
                    dir.Normalize();
                    bullet.GetComponent<Bullet>().dir = dir;
                    bullet.GetComponent<Bullet>().transform.position = shootPosition;
                }
            }

            StartCoroutine(WeaponCD());
        }
    }

    IEnumerator WeaponCD()
    {
        IsInCD = true;
        yield return new WaitForSeconds(weapon.FireSpeed);
        IsInCD = false;
    }
}

