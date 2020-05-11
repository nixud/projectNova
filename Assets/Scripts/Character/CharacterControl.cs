using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//玩家的控制类。包括玩家移动，武器，ui和道具等。
public class CharacterControl : MonoBehaviour
{
    public GameObject Character;
    public Vector3 MoveDir;
    public float speed;

    public Vector3 PositionBasic = new Vector3(0, 0, 0);

    public GameCamera gameCamera;
    public GameObject ShootButton;

    public Ship ship;
    private float playerHP = 3;

    public UIcontroller uIcontroller;

    List<WeaponControl> weaponControl = new List<WeaponControl>();
    List<WeaponNew> weaponNews = new List<WeaponNew>();
    public List<GameObject> shootPoints = new List<GameObject>();

    ScoreData scoreData;

    float devWidth;
    float devHeight;

    public bool IsAutoFire;
    public List<string> WeaponName = new List<string>();

    public Item item;
    //float itemTime = 0;
    bool isUsingItem = false;

    public Sprite temp;
    //初始化
    void Start()
    {
        // item = new Item();
        // item.EffectNumber = 1;
        // item.LoadEffect();

        if (shootPoints.Count < WeaponName.Count)
            throw new Exception();

        scoreData = ScoreData.Instance;

        uIcontroller = GameObject.Find("Canvas").GetComponent<UIcontroller>();
        uIcontroller.Init((int)PlayerStatus.GetInstance().HP, (int)PlayerStatus.GetInstance().MaxHP);
        //        Debug.Log(PlayerStatus.GetInstance().HP);
        //uIcontroller.AddArmor();

        BattleUserConfig.Instance.SetAutoFire(IsAutoFire);

        gameCamera = Camera.main.GetComponent<GameCamera>();
        ShootButton = GameObject.Find("ShootButton");

        devWidth = gameCamera.GetdevWidth() * 0.5f;
        devHeight = gameCamera.GetdevHeight() * 0.5f;
        if (Character == null) Character = gameObject;

        PositionBasic = Character.transform.position;
        /*
        for (int i = 0; i < WeaponName.Count; i++)
        {
            weaponControl.Add(gameObject.AddComponent<WeaponControl>());
            weaponControl[i].LoadWeapon(WeaponName[i]);
        }*/

        //WeaponLoader weaponLoader = new WeaponLoader();
        for (int i = 0; i < WeaponName.Count; i++)
        {
            weaponNews.Add(WeaponLoader.LoadWeaponAndAttachToGO(WeaponName[i],gameObject));
        }

        if (BattleUserConfig.Instance.GetAutoFire() == true)
        {
            ShootButton.SetActive(false);
            StartRay();
        }
    }

    //用于移动。movedir会被改变，然后在这里进行移动。
    void FixedUpdate()
    {
        if (BattleUserConfig.Instance.GetAutoFire() == true)
        {
            Shoot();
        }

        Vector3 newPosi = Character.transform.position + MoveDir * speed;
        if (Mathf.Abs(newPosi.x) >= devWidth)
            MoveDir = new Vector3(0, MoveDir.y, MoveDir.z);
        if (Mathf.Abs(newPosi.y) >= devHeight)
            MoveDir = new Vector3(MoveDir.x, 0, MoveDir.z);
        gameObject.transform.Translate(MoveDir * speed);

    }
    //用于防止移动到墙边的方法。
    //但是没被用到。
    public void SetPosition(Vector3 position)
    {
        Vector3 newPosi = PositionBasic + position;

        if (newPosi.x > devWidth)
            newPosi = new Vector3(devWidth, newPosi.y, newPosi.z);
        else if (newPosi.x < -devWidth)
            newPosi = new Vector3(-devWidth, newPosi.y, newPosi.z);
        if (newPosi.y > devHeight)
            newPosi = new Vector3(newPosi.x, devHeight, newPosi.z);
        else if (newPosi.y < -devHeight)
            newPosi = new Vector3(newPosi.x, -devHeight, newPosi.z);

        gameObject.transform.position = newPosi;
    }
    public void SetPositionEnd()
    {
        PositionBasic = gameObject.transform.position;
    }
    //射击。循环调用所有的武器。
    public void Shoot()
    {
        //for (int i = 0; i < weaponControl.Count; i++)
        //    weaponControl[i].Shoot(shootPoints[i].transform.position, Vector3.up);
        for (int i = 0; i < weaponNews.Count; i++) {
            weaponNews[i].Shoot(shootPoints[i].transform.position, Vector3.up);
        }
    }
    //开始发射激光
    public void StartRay()
    {
        for (int i = 0; i < weaponControl.Count; i++)
            weaponControl[i].StartRay();
    }
    //停止发射激光
    public void StopRay()
    {
        for (int i = 0; i < weaponControl.Count; i++)
            weaponControl[i].StopRay();
    }
    //扣血。uicontrller的--表示减一个血。
    public void DecHP()
    {
        PlayerHittedEffect();
        PlayerStatus.GetInstance().HP -= 1f;
        //Debug.Log(PlayerStatus.GetInstance().HP);
        if (PlayerStatus.GetInstance().HP <= 0.3f)
        {
            PlayerDead();
            //Debug.Log("草");
            //Application.Quit();
        }
        uIcontroller--;
    }
    public void PlayerHittedEffect()
    {

    }
    //玩家死去时的判定
    public void PlayerDead()
    {
        SceneManager.LoadScene("ScoreBroadFailed");
        ObjectPool.GetInstance().EmptyPool();
    }
    //使用道具
    // public void UsingItem()
    // {
    //     if (!isUsingItem)
    //     {
    //         isUsingItem = true;
    //         StartCoroutine(Useitem());
    //     }
    // }
    // //计算道具cd
    // IEnumerator Useitem()
    // {
    //     GameObject.Find("ItemButton").GetComponent<Image>().sprite = temp;
    //     item.Run();
    //     yield return new WaitForSeconds(item.itemEffects.time);
    //     item.End();
    //     isUsingItem = false;
    // }
    //这是一个改变武器射速的方法，由相应道具调用
    public void WeaponSpeedChange(string mode,float num) {
        if (mode == "*")
        {
            for (int i = 0; i < weaponControl.Count; i++)
                weaponControl[i].weapon.FireSpeed *= num;
        }
        else if (mode == "/") {
            for (int i = 0; i < weaponControl.Count; i++)
                weaponControl[i].weapon.FireSpeed /= num;
        }
    }
}
