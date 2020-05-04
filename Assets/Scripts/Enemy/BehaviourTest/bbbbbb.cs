using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using TreeEditor;
using UnityEditor.Experimental.UIElements;
using UnityEngine;

public class bbbbbb : MonoBehaviour
{
    public GameObject gobj;
    
    private string name = "Cube";

    private Transform tar;
    public float Rospeed;
    public float MovSpeed;
    public float lerpSpeed = 0.0001f;
    private Transform my;
    private bool turn;

    private GameObject g;

    private Vector3 forward = new Vector3(0, -1, 0);
    // Start is called before the first frame update
    void Start()
    {
        tar = GameObject.Find(name).transform;
        g = GameObject.Find(name);
        turn = true;
        // var tmp = tar.position - transform.position;
        // transform.up = new Vector3(-tmp.x, -tmp.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!g.GetComponent<aaaaaa>().move)
            return;

        // if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(tar.position.x, tar.position.y)) < 2f)
        // {
        //     turn = false;
        // }

        if (turn)
        {
            var tmp = tar.position - transform.position;
            //
            var angle = Vector2.Angle(new Vector2(tmp.x, tmp.y), new Vector2(-transform.up.x, -transform.up.y));
            var lerp = Vector3.Lerp(transform.up, -tmp, lerpSpeed);
            transform.up = (new Vector3(lerp.x, lerp.y, 0)).normalized;
            // transform.up = new Vector3(-tmp.x, -tmp.y).normalized;
        }

        //
        Debug.DrawLine(transform.position, transform.position - transform.up);
        var temp = transform.position;
        transform.Translate(new Vector3(0, -1, 0) * MovSpeed * Time.deltaTime);
        Debug.DrawLine(temp, transform.position);
        Debug.DrawLine(transform.position, transform.position - transform.up);

        
    }
}
