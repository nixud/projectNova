using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponControl : MonoBehaviour
{
    [HideInInspector]
    public Weapon weapon;
    [HideInInspector]
    public GameObject bullet;
    [HideInInspector]
    public GameObject ray;

    bool IsInCD;
    bool IsRayhiting;
    bool IsRayOn;

    private void Start()
    {
        if (weapon.isRay)
        {
            ray = ObjectPool.GetInstance().GetObj(weapon.RayNumber, "Bullets");
        }
    }

    public void Shoot(Vector3 shootPosition, Vector3 shootForward) {
        if (weapon.isRay == false)
        {
            if (!IsInCD)
            {
                if (!weapon.IsAShotgun)
                {
                    bullet = ObjectPool.GetInstance().GetObj(weapon.BulletNumber, "Bullets");
                    bullet.GetComponent<Bullet>().dir = shootForward;
                    bullet.GetComponent<Bullet>().transform.position = shootPosition;
                }
                else {
                    float maxdegree = weapon.Accuracy * Mathf.PI/2;
                    int BulletNumber = weapon.ShotgunNum;
                    for (int i=0;i<BulletNumber;i++) {
                        float angle = -maxdegree + i *(2* maxdegree)/(BulletNumber-1);

                        bullet = ObjectPool.GetInstance().GetObj(weapon.BulletNumber, "Bullets");
                        Vector3 dir;
                        dir = new Vector3(shootForward.y * Mathf.Sin(angle), shootForward.y * Mathf.Cos(angle), 0);
                        dir.Normalize();
                        bullet.GetComponent<Bullet>().dir = dir;
                        bullet.GetComponent<Bullet>().transform.position = shootPosition;
                        //Debug.Log(Mathf.Atan(dir.x / dir.y));
                        bullet.GetComponent<Bullet>().transform.Rotate(new Vector3(0, 0, -Mathf.Atan(dir.x / dir.y)*180/Mathf.PI));
                    }
                }

                StartCoroutine(WeaponCD());
            }
        }
        else if(IsRayOn)
        {
            ray.GetComponent<LineRenderer>().SetPosition(0,shootPosition);

            RaycastHit2D hit = Physics2D.Raycast(shootPosition, shootForward);

            if (hit.collider != null && hit.distance <= 30)
            {
                ray.GetComponent<LineRenderer>().SetPosition(1, hit.point);
                if (!IsRayhiting)
                    StartCoroutine(RayCD(hit));
            }
            else {
                ray.GetComponent<LineRenderer>().SetPosition(1, shootPosition+new Vector3(0,20,0));
            }

        }
    }

    public void StartRay() {
        if (weapon.isRay)
        {
            IsRayOn = true;
        }
    }

    public void StopRay() {
        if (weapon.isRay)
        {
            IsRayOn = false;
            ray.GetComponent<LineRenderer>().SetPosition(0, new Vector3(60, 60, 0));
            ray.GetComponent<LineRenderer>().SetPosition(1, new Vector3(60, 60, 0));
        }
    }

    public Weapon LoadWeapon(string WeaponNumber)
    {
        WeaponJsonLoader jsonLoader = new WeaponJsonLoader();
        weapon =  jsonLoader.LoadData(WeaponNumber);
        jsonLoader = null;
        return weapon;
    }

    IEnumerator WeaponCD() {
        IsInCD = true;
        yield return new WaitForSeconds(weapon.FireSpeed);
        IsInCD = false;
    }
    IEnumerator RayCD(RaycastHit2D hit) {
        IsRayhiting = true;
        hit.collider.gameObject.GetComponent<EnemyControl>().Hitted(weapon.RayDamagePerSec/5);
        yield return new WaitForSeconds(0.2f);
        IsRayhiting = false;
    }
}
