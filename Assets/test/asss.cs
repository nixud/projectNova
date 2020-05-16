using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asss : MonoBehaviour
{
    public GameObject effect;
    public SpriteMask sm1, sm2;

    public float f1, f2, f3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        change(effect, f1);
        change(sm1.gameObject, f2);
        change(sm2.gameObject, f3);
    }

    void change(GameObject g, float r)
    {
        g.transform.localScale = g.transform.localScale + new Vector3(r, r, 0);
    }
}
