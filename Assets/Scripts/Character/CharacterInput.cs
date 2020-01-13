using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    CharacterControl characterControl;
    Vector3 moveDir = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        characterControl = gameObject.GetComponent<CharacterControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            characterControl.Shoot();

        moveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W) && moveDir.y<=0)
            moveDir += Vector3.up;
        if (Input.GetKey(KeyCode.S) && moveDir.y >= 0)
            moveDir += Vector3.down;
        if (Input.GetKey(KeyCode.A) && moveDir.x <= 0)
            moveDir += Vector3.left;
        if (Input.GetKey(KeyCode.D) && moveDir.x >= 0)
            moveDir += Vector3.right;
        moveDir.Normalize();
        characterControl.MoveDir = moveDir;
    }

}
