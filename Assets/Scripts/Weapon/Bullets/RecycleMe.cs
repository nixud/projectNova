using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleMe : MonoBehaviour
{
    public float DistoryTime = 3f;
    void OnEnable()
    {
        StartCoroutine(Recycle());
    }
    IEnumerator Recycle() {
        yield return new WaitForSeconds(DistoryTime);
        ObjectPool.GetInstance().RecycleObj(gameObject);
    }
}
