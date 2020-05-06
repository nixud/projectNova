using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingmanController : MonoBehaviour
{
    public GameObject followTarget; // 跟随的物体
    public float gapDistance;       // 与跟随物体的最短距离
    public int gapCount;            // 与跟随的物体所间隔位置数
    [Header("攻击目标锁定范围，0为无锁定行为")]
    public float attackArrange;     // 攻击目标锁定范围
    private Vector2 lastPos;
    private Vector2 movePos;
    private Vector3 attackTargetPos;
    private Queue<Vector2> targetPosQueue;
    private WingmanAction wingmanAction;
    private bool isAttack = false;

    void Start()
    {
        wingmanAction = GetComponent<WingmanAction>();
        lastPos = (Vector2)followTarget.transform.position;
        movePos = (Vector2)transform.position;
        targetPosQueue = new Queue<Vector2>();
        StartCoroutine(CheckMovePos());
        if (attackArrange > 0)
        {
            StartCoroutine(CheckAttackTarget());
        }
    }

    /// <summary>
    /// 调用WingmanAction中具体实现的行为方法
    /// </summary>
    void Update()
    {
        wingmanAction.Move(movePos);
        wingmanAction.Attack(attackTargetPos);
    }

    public void SetTarget(GameObject obj)
    {
        followTarget = obj;
    }
    /// <summary>
    /// 检测是否需要移动到跟随目标的上"gap"次位置
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckMovePos()
    {
        while (true)
        {
            if (lastPos != (Vector2)followTarget.transform.position && (lastPos - (Vector2)followTarget.transform.position).magnitude > gapDistance)
            {
                if (targetPosQueue.Count > gapCount)
                    movePos = targetPosQueue.Dequeue();
                targetPosQueue.Enqueue(lastPos);
                lastPos = (Vector2)followTarget.transform.position;
            }
            yield return new WaitForEndOfFrame();
        }
    }
    /// <summary>
    /// 检测最近的Boss与敌人进行攻击
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckAttackTarget()
    {
        while (true)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, attackArrange);
            
            if (cols.Length > 0)
            {
                List<float> distanceList = new List<float>();
                Dictionary<GameObject, float> colsDic = new Dictionary<GameObject, float>();
                for (int i = 0;i < cols.Length; i++)
                {
                    if (cols[i].transform.CompareTag("Boss") || cols[i].transform.CompareTag("Enemy"))
                    {
                        float distance = (cols[i].transform.position - transform.position).magnitude;
                        if (colsDic.ContainsKey(cols[i].gameObject))
                        {
                            colsDic.Add(cols[i].gameObject, distance);
                        }
                        else
                        {
                            colsDic[cols[i].gameObject] = distance;
                        }
                        if (!distanceList.Contains(distance))
                        {
                            distanceList.Add(distance);
                        }
                    }
                }
                if (distanceList.Count > 0)
                {
                    distanceList.Sort();
                    GameObject o;
                    foreach (KeyValuePair<GameObject, float> c in colsDic)
                    {
                        if (c.Value == distanceList[0])
                            attackTargetPos = c.Key.transform.position;
                        else
                            o = gameObject;
                    }
                }
                else
                {
                    attackTargetPos = Vector3.zero;
                }

            }
            else
            {
                attackTargetPos = Vector3.zero;
            }
            yield return null;
        }
    }
}