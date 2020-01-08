using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourController : MonoBehaviour
{
    private float time = 0;

    public bool AlwaysShoot;
    public bool AlwaysShootUntilNextBehaviour;
    public bool AlwaysShootUntilNextBehaviourEnd;

    private bool IsNotRecycled = false;
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
        behaviours.Add(new MoveForward(gameObject, 3f, 1f));
        behaviours.Add(new AlwaysShoot(gameObject, -1f));
        behaviours.Add(new StayHere(gameObject,2f));
        behaviours.Add(new MoveForward(gameObject,2f,20f));
    }

    private void Update()
    {
        if (IsNotRecycled)
        {

            if (AlwaysShoot || AlwaysShootUntilNextBehaviour)
                gameObject.GetComponent<WeaponControlEnemy>().ShootBehaviour();

            if (behaviours.Count > 0) behaviours[0].Run(Time.deltaTime);

            time += Time.deltaTime;
            NextBehaviour();

        }
    }

    private void NextBehaviour()
    {
        if (behaviours.Count > 0)
        {
            if (behaviours[0].time <= time)
            {
                behaviours.RemoveAt(0);
                time = 0;
                if(AlwaysShootUntilNextBehaviour && AlwaysShootUntilNextBehaviourEnd)
                    AlwaysShootUntilNextBehaviourEnd = false;
                else if(AlwaysShootUntilNextBehaviour)
                    AlwaysShootUntilNextBehaviour = false;
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
}
