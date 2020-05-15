using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[Hotfix]
public class WeaponNormalGun : WeaponNew
{
    GameObject bullet;

    public float FireSpeed;
    public bool PreAiming;
    private string BulletNumber;
    private Vector3 attackTargetPos;

    public override void LoadInfomation(WeaponInformation weapon) {
        Number = weapon.Number;
        WeaponName = weapon.WeaponName;
        Description = weapon.Description;

        FireSpeed = weapon.WeaponFloats[0];
        PreAiming = weapon.WeaponBools[0];
        BulletNumber = weapon.WeaponStrings[0];

        IconPath = weapon.IconPath;
        PicPath = weapon.PicPath;

        rareLevel = weapon.rareLevel;
        if (PreAiming)
        {
            StartCoroutine(CheckAttackTarget());
        }
    }

    public override void Shoot(Vector3 shootPosition, Vector3 shootForward)
    {
        if (!IsInCD)
        {

            bullet = ObjectPool.GetInstance().GetObj(BulletNumber, "Bullets");
            if (PreAiming)
            {
                bullet.GetComponent<BulletHelper>().bulletNew.dir = (attackTargetPos - shootPosition).normalized;
                bullet.GetComponent<BulletHelper>().bulletNew.transform.position = shootPosition;
            }
            else
            {
                bullet.GetComponent<BulletHelper>().bulletNew.dir = shootForward;
                bullet.GetComponent<BulletHelper>().bulletNew.transform.position = shootPosition;
            }
                
            // normalGun子弹朝向
            float eulers = -Mathf.Atan(shootForward.x / shootForward.y) * 180 / Mathf.PI;
            bullet.GetComponent<BulletHelper>().bulletNew.transform.Rotate(new Vector3(0, 0, eulers));
            bullet.GetComponent<BulletHelper>().bulletNew.eulers = eulers;
            
            // 添加子弹到角色子弹管理
            if (gameObject.CompareTag("Player"))
                gameObject.GetComponent<CharacterBulletControl>().AddBullet(bullet);

            StartCoroutine(WeaponCD());
        }
    }

    IEnumerator WeaponCD()
    {
        IsInCD = true;
        yield return new WaitForSeconds(FireSpeed);
        IsInCD = false;
    }

    /// <summary>
    /// 检测最近的Boss与敌人进行预瞄
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckAttackTarget()
    {
        while (true)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 20f);

            if (cols.Length > 0)
            {
                List<float> distanceList = new List<float>();
                Dictionary<GameObject, float> colsDic = new Dictionary<GameObject, float>();
                for (int i = 0; i < cols.Length; i++)
                {
                    if (cols[i].transform.CompareTag("Boss") || cols[i].transform.CompareTag("Enemy"))
                    {
                        float distance = (cols[i].transform.position - transform.position).magnitude;
                        if (colsDic.ContainsKey(cols[i].gameObject))
                        {
                            colsDic.Add(cols[i].gameObject, distance);
                        }
                        else
                        {
                            colsDic[cols[i].gameObject] = distance;
                        }
                        if (!distanceList.Contains(distance))
                        {
                            distanceList.Add(distance);
                        }
                    }
                }
                if (distanceList.Count > 0)
                {
                    distanceList.Sort();
                    GameObject o;
                    foreach (KeyValuePair<GameObject, float> c in colsDic)
                    {
                        if (c.Value == distanceList[0])
                            attackTargetPos = c.Key.transform.position;
                        else
                            o = gameObject;
                    }
                }
                else
                {
                    attackTargetPos = Vector3.zero;
                }

            }
            else
            {
                attackTargetPos = Vector3.zero;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
