using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class bbbbbb : MonoBehaviour
{
    private string name = "Cube";

    private Transform tar;
    public float Rospeed;
    public float MovSpeed;
    private Transform my;

    private Vector3 forward = new Vector3(0, -1, 0);
    // Start is called before the first frame update
    void Start()
    {
        tar = GameObject.Find(name).transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, tar.position, MovSpeed * Time.deltaTime);
    }
}
