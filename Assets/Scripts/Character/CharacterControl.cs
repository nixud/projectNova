using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterControl : MonoBehaviour
{
    public GameObject Character;
    public Vector3 MoveDir;
    public float speed;

    public Vector3 PositionBasic = new Vector3(0,0,0);

    public GameCamera gameCamera;
    public GameObject ShootButton;

    public Ship ship;
    private float playerHP = 3;

    public UIcontroller uIcontroller;

    List<WeaponControl> weaponControl = new List<WeaponControl>();
    public List<GameObject> shootPoints = new List<GameObject>();

    ScoreData scoreData;

    float devWidth;
    float devHeight;

    public bool IsAutoFire;
    public List<string> WeaponName = new List<string>();

    float itemTime = 0;
    bool isUsingItem = false;
    // Start is called before the first frame update
    void Start()
    {
        if (shootPoints.Count != WeaponName.Count)
            throw new Exception();

        scoreData = ScoreData.Instance;

        uIcontroller = GameObject.Find("Canvas").GetComponent<UIcontroller>();
        uIcontroller.Init((int)playerHP);
        uIcontroller.AddArmor();

        UserConfig.Instance.SetAutoFire(IsAutoFire);

        gameCamera = Camera.main.GetComponent<GameCamera>();
        ShootButton = GameObject.Find("ShootButton");
        
        devWidth = gameCamera.GetdevWidth() * 0.5f;
        devHeight = gameCamera.GetdevHeight() * 0.5f;
        if (Character == null) Character = gameObject;

        PositionBasic = Character.transform.position;

        for (int i = 0; i < WeaponName.Count; i++)
        {
            weaponControl.Add(gameObject.AddComponent<WeaponControl>());
            weaponControl[i].LoadWeapon(WeaponName[i]);
        }

        if (UserConfig.Instance.GetAutoFire() == true)
        {
            ShootButton.SetActive(false);
            StartRay();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (UserConfig.Instance.GetAutoFire() == true) {
            Shoot();
        }

        Vector3 newPosi = Character.transform.position + MoveDir*speed;
        if (Mathf.Abs(newPosi.x) >= devWidth)
            MoveDir = new Vector3(0, MoveDir.y, MoveDir.z);
        if (Mathf.Abs(newPosi.y) >= devHeight)
            MoveDir = new Vector3(MoveDir.x, 0, MoveDir.z);
        gameObject.transform.Translate(MoveDir * speed);

        if (scoreData.levelScore >= 10)
        {
            SceneManager.LoadScene("ScoreBroad");
            ObjectPool.GetInstance().EmptyPool();
        }
    }

    public void SetPosition(Vector3 position) {
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
    public void SetPositionEnd() {
        PositionBasic = gameObject.transform.position;
    }
    public void Shoot() {
        for(int i=0;i<weaponControl.Count;i++)
            weaponControl[i].Shoot(shootPoints[i].transform.position, Vector3.up);
    }
    public void StartRay() {
        for (int i = 0; i < weaponControl.Count; i++)
            weaponControl[i].StartRay();
    }
    public void StopRay()
    {
        for (int i = 0; i < weaponControl.Count; i++)
            weaponControl[i].StopRay();
    }
    public void DecHP()
    {
        PlayerHittedEffect();
        playerHP -= 0.5f;
        if (playerHP <= 0.01f)
        {
            PlayerDead();
        }
        uIcontroller--;
    }
    public void PlayerHittedEffect() {

    }
    public void PlayerDead() {
        SceneManager.LoadScene("ScoreBroadFailed");
        ObjectPool.GetInstance().EmptyPool();
    }
}
