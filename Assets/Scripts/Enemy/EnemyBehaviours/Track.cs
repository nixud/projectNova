using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Track : EnemyBehaviours
{
    private Vector3 direction;
    private float speed;
    private Transform player;

    private float lerpSpeed = 0.03f;
    
    public Track(GameObject gameObject, float speed)
    {
        this.gameObject = gameObject;
        this.speed = speed;
        player = GameObject.Find("Player").transform;
        time = float.MaxValue;
    }
    
    public override Vector3 Calculate(float Dt)
    {
        return Vector3.down * (speed * Dt);
    }

    public override void Start()
    {
        gameObject.transform.up = direction;
    }

    public override void Run(float Dt)
    {
        var tmp = player.position - gameObject.transform.position;
        //
        // var angle = Vector2.Angle(new Vector2(tmp.x, tmp.y), new Vector2(-gameObject.transform.up.x, -gameObject.transform.up.y));
        var lerp = Vector3.Lerp(gameObject.transform.up, -tmp, lerpSpeed);
        gameObject.transform.up = (new Vector3(lerp.x, lerp.y, 0)).normalized;
        
        gameObject.transform.Translate(Calculate(Dt));
    }

    public override void End()
    {
        gameObject.transform.up = direction;
    }
}
