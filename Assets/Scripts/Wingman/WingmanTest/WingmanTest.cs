using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingmanTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 1000);
        Debug.Log(cols.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
