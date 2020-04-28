using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class behTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        JsonLoader<EnemyBehaviourContainer> loader = new JsonLoader<EnemyBehaviourContainer>();
        EnemyBehaviourContainer e = new EnemyBehaviourContainer();

        e.number = "000001";
        e.behaviourGroup.Add(new List<BaseBehaviourContainer>());
        e.behaviourGroup[0].Add(new BaseBehaviourContainer(BehaviourEnum.AlwaysShoot, 5f));
        e.behaviourGroup.Add(new List<BaseBehaviourContainer>());
        e.behaviourGroup[1].Add(new BaseBehaviourContainer(BehaviourEnum.ShootOnce, 3f));
        e.behaviourGroup[1].Add(new BaseBehaviourContainer(BehaviourEnum.Move, 3f, 3f, Vector3.forward));
        
        List<EnemyBehaviourContainer> l = new List<EnemyBehaviourContainer>();
        l.Add(e);
        EnemyBehaviourContainer e1 = new EnemyBehaviourContainer();
        e1.number = "000002";
        e1.behaviourGroup.Add(new List<BaseBehaviourContainer>());
        e1.behaviourGroup[0].Add(new BaseBehaviourContainer(BehaviourEnum.AlwaysShoot, 3f));
        e1.behaviourGroup[0].Add(new BaseBehaviourContainer(BehaviourEnum.AlwaysShootUNB, 10f));
        e1.behaviourGroup.Add(new List<BaseBehaviourContainer>());
        e1.behaviourGroup[1].Add(new BaseBehaviourContainer(BehaviourEnum.ShootOnce, 3f));
        e1.behaviourGroup[1].Add(new BaseBehaviourContainer(BehaviourEnum.Move, 3f, 3f, Vector3.forward));
        l.Add(e1);
        loader.SaveData(l);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
