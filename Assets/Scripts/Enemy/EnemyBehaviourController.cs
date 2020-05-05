using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourController : MonoBehaviour
{
    //用于敌人行动逻辑控制的类
    private float time = 0;

    public bool AlwaysShoot;
    public bool AlwaysShootUntilNextBehaviour;
    public bool AlwaysShootUntilNextBehaviourEnd;

    [HideInInspector]public bool couldChangeBeh = false;

    private bool IsNotRecycled = false;
    private bool CanMove = false;

    private void OnEnable()
    {
        IsNotRecycled = true;
    }
    private void OnDisable()
    {
        IsNotRecycled = false;
    }

    private List<EnemyBehaviours> behaviours = new List<EnemyBehaviours>();

    private void Start()
    {
        //暂时的敌人逻辑获取
        // if (gameObject.name == "test")
        // {
        //     behaviours.Add(new MoveForward(gameObject, 3f, new Vector3(0, -1, 0), 1f));
        //     behaviours.Add(new ShootPlayerOnce(gameObject,-1f));
        //     behaviours.Add(new AlwaysShoot(gameObject, -1f));
        //     behaviours.Add(new StayHere(gameObject, 4f));
        //     behaviours.Add(new MoveForward(gameObject, 2f, new Vector3(0, -1, 0), 20f));
        // }
        // else if(gameObject.name == "Enemy000003")
        // {
        //     behaviours.Add(new MoveForward(gameObject, 3f, new Vector3(0, -1, 0), 1f));
        //     behaviours.Add(new ShootOnce(gameObject, -1f));
        //     behaviours.Add(new AlwaysShoot(gameObject, -1f));
        //     behaviours.Add(new StayHere(gameObject, 16f));
        //     behaviours.Add(new MoveForward(gameObject, 2f, new Vector3(0, -1, 0), 20f));
        // }
        // else if (gameObject.name == "Enemy000005")
        // {
        //     behaviours.Add(new Kamikaze(gameObject, 6f, 10f));
        // }
        // else {
        // behaviours.Add(new MoveForward(gameObject, 4f, new Vector3(-1, -1, 0), 1f));
        // behaviours.Add(new AlwaysShootUNB(gameObject, -1));
        // behaviours.Add(new MoveBetween(gameObject, 2, 10, new Vector3(-4, 6.5f, 0), new Vector3(-1, 6.5f, 0)));
        // behaviours.Add(new MoveForward(gameObject, 4f, new Vector3(-1, 0, 0), 20f));
        // }
        
        
        int count = (Convert.ToInt32(gameObject.name[0]) - Convert.ToInt32('0')) * 10 + (Convert.ToInt32(gameObject.name[1]) - Convert.ToInt32('0'));
        gameObject.name = gameObject.name.Remove(0, 2);
        EnemyBehaviourContainer.SetBehaviour(EnemyBehaviourLoader.LoadBehaviour(count.ToString()), gameObject, behaviours, gameObject.name);
        
        Debug.Log(gameObject.name + " : " + count.ToString());
        
        behaviours[0]?.Start();
    }

    private void Update()
    {
        if (IsNotRecycled)
        {

            if (AlwaysShoot || AlwaysShootUntilNextBehaviour)
                gameObject.GetComponent<EnemyControl>().Shoot(Vector3.down);

            if (behaviours.Count > 0) behaviours[0].Run(Time.deltaTime);

            time += Time.deltaTime;
            CheckShouldRecycle();
            NextBehaviour();

        }
    }

    // 检查小怪出屏直接回收
    private void CheckShouldRecycle()
    {
        if (Mathf.Abs(transform.position.x) > Camera.main.GetComponent<GameCamera>().GetdevWidth() / 2 * 1.26 || 
            Mathf.Abs(transform.position.y) > Camera.main.GetComponent<GameCamera>().GetdevHeight() / 2 * 1.26)
            behaviours.Clear();
    }

    private void NextBehaviour()
    {

        if (behaviours.Count > 0)
        {
            if (behaviours[0].time <= time || couldChangeBeh)
            {
                behaviours[0]?.End();
                behaviours.RemoveAt(0);
                if (behaviours.Count > 0)
                    behaviours[0]?.Start();
                time = 0;
                if(AlwaysShootUntilNextBehaviour && AlwaysShootUntilNextBehaviourEnd)
                    AlwaysShootUntilNextBehaviourEnd = false;
                else if(AlwaysShootUntilNextBehaviour)
                    AlwaysShootUntilNextBehaviour = false;

                couldChangeBeh = false;
            }
        }
        else
        {
            gameObject.GetComponent<EnemyControl>().RecycleNow();
            time = 0;
            IsNotRecycled = false;
            AlwaysShoot = false;
            AlwaysShootUntilNextBehaviour = false;
            AlwaysShootUntilNextBehaviourEnd = false;
        }
    }

    private void LeftAndRightBehaviour() {
        behaviours.Add(new MoveForward(gameObject, 3f, new Vector3(0.3f, -1, 0), 1f));
        behaviours.Add(new MoveForward(gameObject, 3f, new Vector3(-0.3f, -1, 0), 1f));
        behaviours.Add(new MoveForward(gameObject, 3f, new Vector3(0.3f, -1, 0), 1f));
        behaviours.Add(new MoveForward(gameObject, 3f, new Vector3(-0.3f, -1, 0), 1f));
    }
}
