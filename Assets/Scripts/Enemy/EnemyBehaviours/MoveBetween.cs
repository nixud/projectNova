using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetween : EnemyBehaviours
{
    private float speed;
    private Vector3 point1, point2;
    private bool dir;
    
    /// <summary>
    /// 两点间移动， 默认先向第一点移动
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="NowSpeed"></param>
    /// <param name="t"></param>
    /// <param name="point1">第一点</param>
    /// <param name="point2">第二点</param>
    public MoveBetween(GameObject gameObject, float NowSpeed, float t, Vector3 point1, Vector3 point2)
    {
        this.gameObject = gameObject;
        time = t;
        this.point1 = point1;
        this.point2 = point2;
        dir = true;
        speed = NowSpeed;
    }
    
    
    public override Vector3 Calculate(float Dt)
    {
        if (dir)
        {
            if (Vector3.Distance(gameObject.transform.position, point1) < 0.5f)
                dir = false;
            return Vector3.MoveTowards(gameObject.transform.position, point1, speed * Dt);
        }
        else
        {
            if (Vector3.Distance(gameObject.transform.position, point2) < 0.5f)
                dir = true;
            return Vector3.MoveTowards(gameObject.transform.position, point2, Dt * speed);
        }
    }

    public override void Start()
    {
        
    }

    public override void Run(float Dt)
    {
        gameObject.transform.position = Calculate(Dt);
    }

    public override void End()
    {
        
    }
}
