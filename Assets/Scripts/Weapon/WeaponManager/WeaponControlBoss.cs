using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControlBoss : MonoBehaviour
{
    private Vector3 shootPoint;
    public string WeaponName;

    public List<GameObject> firePoints = new List<GameObject>();
    private List<WeaponControl> weaponControls = new List<WeaponControl>();


    public delegate void BossBehaviour();
    /*
    private bool IsNotRecycled = false;
    private void OnEnable()
    {
        IsNotRecycled = true;
    }
    private void OnDisable()
    {
        IsNotRecycled = false;
    }*/

    private void Start()
    {
        for (int i = 0; i < firePoints.Count; i++)
        {
            weaponControls.Add(gameObject.AddComponent<WeaponControl>());
            weaponControls[i].LoadWeapon(WeaponName);

            if (weaponControls[i].weapon.isRay)
            {
                weaponControls[i].ray = ObjectPool.GetInstance().GetObj(weaponControls[i].weapon.RayNumber, "Bullets");
            }
        }
        
    }

    private void Update()
    {
        for (int i=0;i<firePoints.Count;i++) {
            ShootBehaviour(i);
        }
    }

    public void ShootBehaviour(int i)
    {
        weaponControls[i].Shoot(firePoints[i].transform.position - new Vector3(0, 0.4f, 0), Vector3.down);
    }

    public void FanBullet() { 
        
    }
}

