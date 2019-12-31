using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public GameObject Character;
    public Vector3 MoveDir;
    public float speed;

    public Vector3 PositionBasic = new Vector3(0,0,0);

    public GameCamera gameCamera;
    public GameObject ShootButton;
    public GameObject ShootPosition;

    public Ship ship;
    private float playerHP = 3;

    public UIcontroller uIcontroller;

    WeaponControl weaponControl;

    float devWidth;
    float devHeight;

    public bool IsAutoFire;
    public string WeaponName;
    // Start is called before the first frame update
    void Start()
    {
        uIcontroller = GameObject.Find("Canvas").GetComponent<UIcontroller>();
        uIcontroller.Init(4);
        uIcontroller.AddArmor();

        UserConfig.Instance.SetAutoFire(IsAutoFire);

        gameCamera = Camera.main.GetComponent<GameCamera>();
        ShootButton = GameObject.Find("ShootButton");
        
        devWidth = gameCamera.GetdevWidth() * 0.5f;
        devHeight = gameCamera.GetdevHeight() * 0.5f;
        if (Character == null) Character = gameObject;

        PositionBasic = Character.transform.position;

        weaponControl = gameObject.GetComponent<WeaponControl>();
        weaponControl.LoadWeapon(WeaponName);

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
            Shoot(ShootPosition.transform.position);
        }

        Vector3 newPosi = Character.transform.position + MoveDir*speed;
        if (Mathf.Abs(newPosi.x) >= devWidth)
            MoveDir = new Vector3(0, MoveDir.y, MoveDir.z);
        if (Mathf.Abs(newPosi.y) >= devHeight)
            MoveDir = new Vector3(MoveDir.x, 0, MoveDir.z);
        gameObject.transform.Translate(MoveDir * speed);
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
    public void Shoot(Vector3 position) {
        weaponControl.Shoot(position, Vector3.up);
    }
    public void StartRay() {
        weaponControl.StartRay();
    }
    public void StopRay()
    {
        weaponControl.StopRay();
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
        Debug.Log("IsDead");
    }
}
