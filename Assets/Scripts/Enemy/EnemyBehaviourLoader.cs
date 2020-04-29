using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyBehaviourLoader
{
    private static List<EnemyBehaviourContainer> _enemyBehaviourContainers;

    public static List<EnemyBehaviourContainer> LoadBehaviours()
    {
        JsonLoader<EnemyBehaviourContainer> loader = new JsonLoader<EnemyBehaviourContainer>();
        return  _enemyBehaviourContainers = loader.LoadData();
    }
    
    public static EnemyBehaviourContainer LoadBehaviour(string number)
    {
        EnemyBehaviourContainer temp = new EnemyBehaviourContainer();
        if (_enemyBehaviourContainers == null)
        {
            JsonLoader<EnemyBehaviourContainer> loader = new JsonLoader<EnemyBehaviourContainer>();
            _enemyBehaviourContainers = loader.LoadData();
        }

        foreach (var container in _enemyBehaviourContainers)
        {
            if (container.number == "default")
                temp = container;
            if (container.number == number)
                return container;
        }

        return temp;
    }

    public static void SaveBehaviour(List<EnemyBehaviourContainer> enemyBehaviourContainers)
    {
        JsonLoader<EnemyBehaviourContainer> loader = new JsonLoader<EnemyBehaviourContainer>();
        loader.SaveData(enemyBehaviourContainers);
    }
}
