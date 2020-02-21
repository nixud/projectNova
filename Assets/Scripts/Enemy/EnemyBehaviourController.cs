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
        if (gameObject.name == "test")
        {
            behaviours.Add(new MoveForward(gameObject, 3f, new Vector3(0, -1, 0), 1f));
            behaviours.Add(new ShootPlayerOnce(gameObject,-1f));
            behaviours.Add(new AlwaysShoot(gameObject, -1f));
            behaviours.Add(new StayHere(gameObject, 4f));
            behaviours.Add(new MoveForward(gameObject, 2f, new Vector3(0, -1, 0), 20f));
        }
        else if(gameObject.name == "Enemy000004")
        {
            behaviours.Add(new MoveForward(gameObject, 3f, new Vector3(0, -1, 0), 1f));
            behaviours.Add(new ShootPlayerOnce(gameObject, -1f));
            behaviours.Add(new AlwaysShoot(gameObject, -1f));
            behaviours.Add(new StayHere(gameObject, 16f));
            behaviours.Add(new MoveForward(gameObject, 2f, new Vector3(0, -1, 0), 20f));
        }
        else if (gameObject.name == "Enemy000003")
        {
            behaviours.Add(new Kamikaze(gameObject, 3f, 10f));
        }
        else {
            behaviours.Add(new MoveForward(gameObject, 4f, new Vector3(0, -1, 0), 1f));
            behaviours.Add(new ShootPlayerOnce(gameObject, -1f));
            behaviours.Add(new AlwaysShoot(gameObject, -1f));
            behaviours.Add(new StayHere(gameObject, 8f));
            behaviours.Add(new MoveForward(gameObject, 4f, new Vector3(0, -1, 0), 20f));
        }
    }

    private void Update()
    {
        if (IsNotRecycled)
        {

            if (AlwaysShoot || AlwaysShootUntilNextBehaviour)
                gameObject.GetComponent<EnemyControl>().Shoot(Vector3.down);

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

    private void LeftAndRightBehaviour() {
        behaviours.Add(new MoveForward(gameObject, 3f, new Vector3(0.3f, -1, 0), 1f));
        behaviours.Add(new MoveForward(gameObject, 3f, new Vector3(-0.3f, -1, 0), 1f));
        behaviours.Add(new MoveForward(gameObject, 3f, new Vector3(0.3f, -1, 0), 1f));
        behaviours.Add(new MoveForward(gameObject, 3f, new Vector3(-0.3f, -1, 0), 1f));
    }
}
