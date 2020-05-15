using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingmanAction : MonoBehaviour
{
    public float movingSpeed;
    private Rigidbody2D rigidBody;
    private WeaponControlWingman weaponControlWingman;
    private Wingman wingman;
    // Start is called before the first frame update

    public void Init()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        weaponControlWingman = GetComponent<WeaponControlWingman>();
        weaponControlWingman.Init();
    }

    /// <summary>
    /// 移动至目标点坐标
    /// </summary>
    /// <param name="targetPos">目标点的二维坐标</param>
    public void Move(Vector2 targetPos)
    {
        rigidBody.MovePosition(Vector3.Lerp(transform.position,targetPos,movingSpeed));
    }
    /// <summary>
    /// 向前发射子弹
    /// </summary>
    public void Attack(Vector3 targetPos)
    {
        if (targetPos == Vector3.zero)
        {
            weaponControlWingman.Shoot(transform.position, Vector3.up);
        }
        else
        {
            weaponControlWingman.Shoot(transform.position, (targetPos - transform.position).normalized);
        }
    }
}
