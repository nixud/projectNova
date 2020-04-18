using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHelper : MonoBehaviour
{
    public BulletNew bulletNew;
    public Bullet bullet;

    public void ActiveIt() {
        if (bulletNew != null)
            bulletNew.ActiveIt();
        else if (bullet != null)
            bullet.ActiveIt();
        else throw new System.Exception("bulletPoolHelper没有被填充！");
    }
}
