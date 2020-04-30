using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Track : EnemyBehaviours
{
    private Vector3 temp;
    private float speed;
    private Transform player;

    public Track(GameObject gameObject, float speed)
    {
        this.gameObject = gameObject;
        this.speed = speed;
        player = GameObject.Find("Player").transform;
        temp = (player.position - gameObject.transform.position).normalized;
    }
    
    public override Vector3 Calculate(float Dt)
    {
        if (Vector3.Distance(gameObject.transform.position, player.position) > 2f)
            temp = (player.position - gameObject.transform.position).normalized;
        return temp;
    }

    public override void Run(float Dt)
    {
        Debug.Log(Calculate(Dt));
        gameObject.transform.position += Calculate(Dt) * (speed * Dt);
        Debug.Log(gameObject.transform.position);
    }
}
