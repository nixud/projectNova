using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyBehaviourContainer
{
    public static void SetBehaviour(EnemyBehaviourContainer enemyBehaviourContainer, GameObject gameObject, List<EnemyBehaviours> behaviourses)
    {
        int index;
        if (enemyBehaviourContainer.behaviourGroup.Count > 1)
        {
            if (gameObject.transform.position.x < 0)
                index = 0;
            else
                index = 1;
        }
        else
            index = 0;

        //Debug.Log(enemyBehaviourContainer.behaviourGroup.Count);
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
                    behaviourses.Add(new Move(gameObject, behaviourContainer.Speed, behaviourContainer.Vector1, behaviourContainer.Time));
                    break;
                case BehaviourEnum.MoveForward:
                    behaviourses.Add(new MoveForward(gameObject, behaviourContainer.Speed, behaviourContainer.Vector1, behaviourContainer.Time));
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
                case BehaviourEnum.MoveBetween:
                    behaviourses.Add(new MoveBetween(gameObject, behaviourContainer.Speed, behaviourContainer.Time, behaviourContainer.Vector1, behaviourContainer.vector2));
                    break;
                case BehaviourEnum.Track:
                    behaviourses.Add(new Track(gameObject, behaviourContainer.Speed));
                    break;
                case BehaviourEnum.MoveToPoint:
                    behaviourses.Add(new MoveToPoint(gameObject, behaviourContainer.Speed, behaviourContainer.Vector1));
                    break;
                case BehaviourEnum.MoveForwardToPoint:
                    behaviourses.Add(new MoveForwardToPoint(gameObject, behaviourContainer.Speed, behaviourContainer.Vector1));
                    break;
                case BehaviourEnum.MoveForwardChangeVelocity:
                    behaviourses.Add(new MoveForwardChangeVelocity(gameObject, behaviourContainer.Speed, behaviourContainer.f_field1, behaviourContainer.Vector1, behaviourContainer.Time));
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

    // 预留字段
    public float f_field1;
    public float f_field2;
    public float f_field3;
    
    public Vector3 Vector1;
    public Vector3 vector2;
    
    // 向量预留字段
    public Vector3 v_field1;

    public BaseBehaviourContainer(BehaviourEnum type, float time, float speed = 0, Vector3 vector1 = default, Vector3 vector2 = default)
    {
        this.Type = type;
        this.Time = time;
        this.Speed = speed;
        this.Vector1 = vector1;
        this.vector2 = vector2;
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
    StayHere,
    MoveBetween,
    Track,
    MoveToPoint,
    MoveForwardToPoint,
    MoveForwardChangeVelocity
}