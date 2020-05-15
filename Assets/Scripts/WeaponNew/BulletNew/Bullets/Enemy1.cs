using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : BulletNew
{
    private Vector3 InitForward;
    public float TracingCD;
    private void Start()
    {
        StartCoroutine(StopTracing());
    }

    protected override void FixedUpdate()
    {
        if (IsNotRecycled)
        {
            Nowtime += Time.deltaTime;
            NowSpeed = Speed.Evaluate(Nowtime);
            if (isTrace)
            {
                CheckAttackTarget();
                
                if (attackTarget != null)
                {
                    Vector2 Direction = attackTarget.transform.position - gameObject.transform.position;
                    Vector2 lerp = Vector2.Lerp(gameObject.transform.up, Direction, rotateSpeed * 0.01f);
                    gameObject.transform.up = (new Vector2(lerp.x, lerp.y)).normalized;
                }
            }
            transform.Translate(gameObject.transform.up * (NowSpeed * SpeedRate), Space.World);
        }
    }


        IEnumerator StopTracing()
    {
        transform.up = (dir).normalized;
        yield return new WaitForSecondsRealtime(TracingCD);
        isTrace = false;
    }
}
