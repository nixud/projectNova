using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingmanController : MonoBehaviour
{
    public GameObject target;       // 跟随的物体
    public float gapDistance;       // 与跟随物体的最短距离
    public int gapCount;            // 与跟随的物体所间隔位置数
    private Vector2 lastPos;
    private Vector2 movePos;
    private Queue<Vector2> targetPosQueue;
    private WingmanAction wingmanAction;
    private bool isAttack = false;

    void Start()
    {
        wingmanAction = GetComponent<WingmanAction>();
        lastPos = (Vector2)target.transform.position;
        movePos = (Vector2)transform.position;
        targetPosQueue = new Queue<Vector2>();
        StartCoroutine(CheckMovePos());
    }

    /// <summary>
    /// 调用WingmanAction中具体实现的行为方法
    /// </summary>
    void Update()
    {
        wingmanAction.Move(movePos);
        wingmanAction.Attack();
    }

    public void SetTarget(GameObject obj)
    {
        target = obj;
    }
    /// <summary>
    /// 检测是否需要移动到跟随目标的上"gap"次位置
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckMovePos()
    {
        while (true)
        {
            if (lastPos != (Vector2)target.transform.position && (lastPos - (Vector2)target.transform.position).magnitude > gapDistance)
            {
                if (targetPosQueue.Count > gapCount)
                    movePos = targetPosQueue.Dequeue();
                targetPosQueue.Enqueue(lastPos);
                lastPos = (Vector2)target.transform.position;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}