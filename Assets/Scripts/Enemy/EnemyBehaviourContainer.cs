using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyBehaviourContainer
{
    public static void SetBehaviour(EnemyBehaviourContainer enemyBehaviourContainer, GameObject gameObject, List<EnemyBehaviours> behaviourses, int index = 0)
    {
        index = index > enemyBehaviourContainer.behaviourGroup.Count ? 0 : index - 1;
        foreach (var behaviourContainer in enemyBehaviourContainer.behaviourGroup[index])
        {
            switch (behaviourContainer.Type)
            {
                case BehaviourEnum.AlwaysShoot:
                    behaviourses.Add(new AlwaysShoot(gameObject, behaviourContainer.Time));
                    break;
                case BehaviourEnum.AlwaysShootUNB:
                    behaviourses.Add(new AlwaysShootUNB(gameObject, behaviourContainer.Time));
                    break;
                case BehaviourEnum.Kamikaze:
                    behaviourses.Add(new Kamikaze(gameObject, behaviourContainer.Speed, behaviourContainer.Time));
                    break;
                case BehaviourEnum.Move:
                    behaviourses.Add(new Move(gameObject, behaviourContainer.Speed, behaviourContainer.Dir, behaviourContainer.Time));
                    break;
                case BehaviourEnum.MoveForward:
                    behaviourses.Add(new MoveForward(gameObject, behaviourContainer.Speed, behaviourContainer.Dir, behaviourContainer.Time));
                    break;
                case BehaviourEnum.ShootOnce:
                    behaviourses.Add(new ShootOnce(gameObject, behaviourContainer.Time));
                    break;
                case BehaviourEnum.ShootPlayerOnce:
                    behaviourses.Add(new ShootPlayerOnce(gameObject, behaviourContainer.Time));
                    break;
                case BehaviourEnum.StayHere:
                    behaviourses.Add(new StayHere(gameObject, behaviourContainer.Time));
                    break;
                default:
                    break;
            }
        }
    }


    public string number;
    public List<List<BaseBehaviourContainer>> behaviourGroup = new List<List<BaseBehaviourContainer>>();
}

public class BaseBehaviourContainer
{
    public BehaviourEnum Type;
    public float Time;
    public float Speed;
    public Vector3 Dir;
        
    public BaseBehaviourContainer(BehaviourEnum type, float time, float speed = 0, Vector3 dir = default)
    {
        this.Type = type;
        this.Time = time;
        this.Speed = speed;
        this.Dir = dir;
    }

    public BaseBehaviourContainer()
    {
    }
}

public enum BehaviourEnum
{
    AlwaysShoot,
    AlwaysShootUNB,
    Kamikaze,
    Move,
    MoveForward,
    ShootOnce,
    ShootPlayerOnce,
    StayHere
}