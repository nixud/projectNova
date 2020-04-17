using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingmanPlayerTest : MonoBehaviour
{
    public float speed;
    private Rigidbody2D RB;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RB.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
    }
}
