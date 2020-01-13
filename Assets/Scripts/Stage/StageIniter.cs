using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageIniter : MonoBehaviour
{
    private void Start()
    {
        ObjectPool.GetInstance().EmptyPool();
    }
}
