using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputMoblie : MonoBehaviour
{
    CharacterControl characterControl;

    Vector3 moveDir = new Vector3(0, 0, 0);

    Vector3 touchPosition = new Vector3(0,0,0);
    bool isTouching = false;

    Vector3 mousePosi;

    float devWidth;

    public GameCamera gameCamera;

    Vector3 LastFrameFinger;
    int LastFrameFingerInt;

    bool isButtonDown;

    // Start is called before the first frame update
    void Start()
    {
        touchPosition = transform.position;

        gameCamera = Camera.main.GetComponent<GameCamera>();
        devWidth = gameCamera.GetdevWidth() * 0.4f;

        characterControl = gameObject.GetComponent<CharacterControl>();
    }

#if UNITY_ANDROID
    // Update is called once per frame
    void Update()
    {
        if (isButtonDown) Shoot();

        moveDir = new Vector3(0, 0, 0);


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                if (Camera.main.ScreenToWorldPoint(touch.position).x < devWidth)
                {
                    isTouching = true;
                    touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                }
                else if (Input.touchCount > 1)
                {
                    touch = Input.GetTouch(1);
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                characterControl.SetPositionEnd();
                isTouching = false;

            }
            if (isTouching && (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary))
            {

                Vector3 forward;
                forward = Camera.main.ScreenToWorldPoint(touch.position) - touchPosition;

                Vector3 finalPosition = touchPosition + forward;

                moveDir = forward;

                characterControl.SetPosition(moveDir);

            }

        }

    }
#endif
    public void Shoot() {
        characterControl.Shoot();
    }

    public void ButtonDown()
    {
        characterControl.StartRay();
        isButtonDown = true;
    }
    public void ButtonUp()
    {
        characterControl.StopRay();
        isButtonDown = false;
    }

    public void UseItem() {
        characterControl.UsingItem();
    }
}
