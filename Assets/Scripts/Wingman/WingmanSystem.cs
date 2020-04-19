using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 僚机系统，挂载于player上对生成僚机进行管理
/// </summary>
public class WingmanSystem : MonoBehaviour
{
    public List<string> wingmansNumbers;
    private List<GameObject> wingmans = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < wingmansNumbers.Count; i++)
        {
            // 由wingmansNumbers从json中读取对应的僚机信息
            WingmanJsonLoader loader = new WingmanJsonLoader();
            Wingman curWingman = loader.LoadData(wingmansNumbers[i]);
            // 生成僚机预设，并使其初始位置与玩家重叠
            GameObject prefab = Resources.Load<GameObject>("Prefabs/Wingmans/" + curWingman.Prefab);
            GameObject curPrefab = Instantiate(prefab);
            curPrefab.transform.position = transform.position;
            // 设置当前僚机的跟随目标
            WingmanController wingmanController = curPrefab.GetComponent<WingmanController>();
            if (i == 0)
            {
                wingmanController.SetTarget(gameObject);
            }
            else
            {
                wingmanController.SetTarget(wingmans[i - 1]);
            }
            //Debug.Log(curPrefab);
            wingmans.Add(curPrefab);
        }
    }
}
