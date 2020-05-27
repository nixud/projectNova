using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithGOBJ : MonoBehaviour
{
    public GameObject GameObject;
    void Update()
    {
        transform.position = GameObject.transform.position;
    }
}
