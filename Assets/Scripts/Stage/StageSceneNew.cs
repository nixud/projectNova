using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

//地图的处理。包括随机生成地图上的关卡和加载之前的关卡。
public class StageSceneNew : MonoBehaviour
{
    private bool IsSpawned;

    public float Range = 3;

    public GameObject PlayerPlane;

    public List<GameObject> EasyStagePoint = new List<GameObject>();
    public List<GameObject> NormalStagePoint = new List<GameObject>();
    public List<GameObject> DiffStagePoint = new List<GameObject>();
    public List<GameObject> SpecialStagePoint = new List<GameObject>();

    public List<string> EasyStageNames = new List<string>();
    public List<string> NormalStageNames = new List<string>();
    public List<string> DiffStageNames = new List<string>();
    public List<string> SpecialStageNames = new List<string>();

    private List<GameObject> StagePointCheck = new List<GameObject>();
    private List<GameObject> StagePointCheckTemp = new List<GameObject>();

    private List<int> StagePointStatus = new List<int>();
    private int PlayerPosition = 0;

    public int EasyStageNum;
    public int NormalStageNum;
    public int DiffStageNum;
    public int SpecialStageNum;

    public int EasyStagePointCount;
    public int NormalStagePointCount;
    public int DiffStagePointCount;

    public List<int> StagePointCheckTable;

    public MapInfomation mapInfomation;

    public int StageSort = 0;
    public MapConfigData mapConfigData;
    [HideInInspector]
    public bool isCleared = false;
    [HideInInspector]
    public string Diffculty;

    private bool CanPass = true;

    private void Start()
    {

        mapInfomation = MapInfomation.GetInstance();

        if (mapInfomation.MapStatus[mapConfigData.MapNumber] == '0')
        {
            mapInfomation.EasyStageNames = EasyStageNames;
            mapInfomation.NormalStageNames = NormalStageNames;
            mapInfomation.HardStageNames = DiffStageNames;
            mapInfomation.SpecialStageNames = SpecialStageNames;

            RandomStage();
            mapInfomation.MapStatus[mapConfigData.MapNumber] = '1';
            mapInfomation.EasyStagePointCount = EasyStagePoint.Count;
            mapInfomation.NormalStagePointCount = NormalStagePoint.Count;
            mapInfomation.DiffStagePointCount = DiffStagePoint.Count;

            //mapInfomation.StagePointCheck = StagePointCheck;
            //mapInfomation.StagePointCheckTemp = StagePointCheckTemp;

            mapInfomation.StagePointStatus = StagePointStatus;
            mapInfomation.PlayerPosition = PlayerPosition;

            NowStageInfomation.GetInstance().PlayerPosition = 0;
        }
        else {
            EasyStagePointCount = mapInfomation.EasyStagePointCount;
            NormalStagePointCount = mapInfomation.NormalStagePointCount;
            DiffStagePointCount = mapInfomation.DiffStagePointCount;

            StagePointCheck = new List<GameObject>();
            StagePointCheckTemp = mapInfomation.StagePointCheckTemp;

            StagePointStatus = mapInfomation.StagePointStatus;
            PlayerPosition = mapInfomation.PlayerPosition;

            StagePointCheckTable = mapInfomation.StagePointCheckTable;

            EasyStageNames = mapInfomation.EasyStageNames;
            NormalStageNames = mapInfomation.NormalStageNames;
            DiffStageNames = mapInfomation.HardStageNames;
            SpecialStageNames = mapInfomation.SpecialStageNames;

            LoadStageData();

            if (NowStageInfomation.GetInstance().isCleared) {
                StagePointStatus[NowStageInfomation.GetInstance().PlayerPosition] = 1;
                mapInfomation.StagePointStatus = StagePointStatus;

                NowStageInfomation.GetInstance().isCleared = false;
            }
        }
        PlayerPlane = GameObject.Find("New Sprite");
        PlayerPlane.transform.position = StagePointCheck[NowStageInfomation.GetInstance().PlayerPosition].transform.position + new Vector3(0,0,-1);
        FreshStageButton();
    }


    private void LoadStageData() {
        int num = 0;
        for (int i=0;i<EasyStagePointCount;i++,num++) {
            GameObject go = EasyStagePoint[i];
            //EasyStagePoint.Add(go); 
            if (StagePointCheckTable[num] == 1)
            {
                StagePointCheck.Add(go);
                go.SetActive(true);
            }
            else go.SetActive(false);
        }

        for (int i = 0; i < NormalStagePointCount; i++, num++)
        {
            GameObject go = NormalStagePoint[i];
            if (StagePointCheckTable[num] == 1)
            {
                StagePointCheck.Add(go);
                go.SetActive(true);
            }
            else go.SetActive(false);
        }

        for (int i = 0; i < DiffStagePointCount; i++, num++)
        {
            GameObject go = DiffStagePoint[i];
            DiffStagePoint.Add(go);
            if (StagePointCheckTable[num] == 1)
            {
                StagePointCheck.Add(go);
                go.SetActive(true);
            }
            else go.SetActive(false);
        }
    }

    public void RandomStage() {
        CanPass = true;
        StagePointCheck.Clear();
        StagePointCheckTemp.Clear();
        StagePointStatus.Clear();

        for (int i = 0; i < EasyStagePoint.Count; i++)
            EasyStagePoint[i].SetActive(true);
        for (int i = 0; i < NormalStagePoint.Count; i++)
            NormalStagePoint[i].SetActive(true);
        for (int i = 0; i < DiffStagePoint.Count; i++)
            DiffStagePoint[i].SetActive(true);

        for (int i = 0; i<EasyStagePoint.Count - EasyStageNum;) {
            int randomNumber = (int)UnityEngine.Random.Range(0, EasyStagePoint.Count -0.01f);
            if (EasyStagePoint[randomNumber].activeSelf)
            {
                EasyStagePoint[randomNumber].SetActive(false);
                i++;
            }
        }
        for (int i = 0; i < EasyStagePoint.Count;i++) {
            if (EasyStagePoint[i].activeSelf)
            {
                StagePointCheck.Add(EasyStagePoint[i]);
                StagePointStatus.Add(0);
                mapInfomation.StagePointCheckTable.Add(1);
                
                mapInfomation.EasyStageNames.Add(EasyStagePoint[i].name);
            }
            else mapInfomation.StagePointCheckTable.Add(0);
        }

        for (int i = 0; i < NormalStagePoint.Count - NormalStageNum;)
        {
            int randomNumber = (int)UnityEngine.Random.Range(0, NormalStagePoint.Count - 0.01f);
            if (NormalStagePoint[randomNumber].activeSelf)
            {
                NormalStagePoint[randomNumber].SetActive(false);
                i++;
            }
        }
        for (int i = 0; i < NormalStagePoint.Count; i++)
        {
            if (NormalStagePoint[i].activeSelf) {
                StagePointCheck.Add(NormalStagePoint[i]); 
                StagePointStatus.Add(0);
                mapInfomation.StagePointCheckTable.Add(1);
                mapInfomation.NormalStageNames.Add(NormalStagePoint[i].name);
            }
            else mapInfomation.StagePointCheckTable.Add(0);
        }

        for (int i = 0; i < DiffStagePoint.Count - DiffStageNum;)
        {
            int randomNumber = (int)UnityEngine.Random.Range(0, DiffStagePoint.Count - 0.01f);
            if (DiffStagePoint[randomNumber].activeSelf)
            {
                DiffStagePoint[randomNumber].SetActive(false);
                i++;
            }
        }
        for (int i = 0; i < DiffStagePoint.Count; i++)
        {
            if (DiffStagePoint[i].activeSelf)
            {
                StagePointCheck.Add(DiffStagePoint[i]);
                StagePointStatus.Add(0);
                mapInfomation.StagePointCheckTable.Add(1);
                mapInfomation.HardStageNames.Add(DiffStagePoint[i].name);
            }
            else mapInfomation.StagePointCheckTable.Add(0);
        }

        StagePointCheckTemp.Add(StagePointCheck[0]);
        CheckPointPass(0);

        if (!CanPass) RandomStage();
    }

    public void FreshStageButton()
    {
        for (int i = 0; i < EasyStagePoint.Count; i++)
            if (!RangeCalculate(EasyStagePoint[i], PlayerPlane))
            {
                FreshStageButtonSetLight(EasyStagePoint[i], false);
            }
            else FreshStageButtonSetLight(EasyStagePoint[i], true);

        for (int i = 0; i < NormalStagePoint.Count; i++)
            if (!RangeCalculate(NormalStagePoint[i], PlayerPlane))
                FreshStageButtonSetLight(NormalStagePoint[i], false);
            else FreshStageButtonSetLight(NormalStagePoint[i], true);

        for (int i = 0; i < DiffStagePoint.Count; i++)
            if (!RangeCalculate(DiffStagePoint[i], PlayerPlane))
                FreshStageButtonSetLight(DiffStagePoint[i], false);
            else FreshStageButtonSetLight(DiffStagePoint[i], true);

        for (int i = 0; i < StagePointStatus.Count; i++) {
            if (StagePointStatus[i] == 1)
                StagePointCheck[i].GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);
        }
    }

    private void FreshStageButtonSetLight(GameObject gameObject,bool b) {
        if (b)
        {
            gameObject.GetComponent<TouchScriptTest>().Light.SetActive(b);
            Color color = gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 1f);
        }
        else {
            gameObject.GetComponent<TouchScriptTest>().Light.SetActive(b);
            Color color = gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.5f);
        }
    }

    private void CheckPointPass(int number) {
        bool canpass = false;
        for (int i=0;i<StagePointCheck.Count;i++) {
            if(number != i && RangeCalculatePoint(StagePointCheck[number], StagePointCheck[i])) {
                canpass = true;
                if (!StagePointCheckTemp.Contains(StagePointCheck[i]))
                {
                    StagePointCheckTemp.Add(StagePointCheck[i]);
                    CheckPointPass(i);
                }
            }
        }
        if (CanPass == true)
        {
            CanPass = canpass;
        }
    }

    private bool RangeCalculate(GameObject gameObject1,GameObject gameObject2) {
        Vector2 player = new Vector2(gameObject2.transform.position.x, PlayerPlane.transform.position.y);
        Vector2 point = new Vector2(gameObject1.transform.position.x, gameObject1.transform.position.y);
        float distance = Vector2.Distance(player, point);
        if (distance < Range)
            return true;
        return false;
    }

    private bool RangeCalculatePoint(GameObject gameObject1, GameObject gameObject2)
    {
        Vector2 player = new Vector2(gameObject2.transform.position.x, gameObject2.transform.position.y);
        Vector2 point = new Vector2(gameObject1.transform.position.x, gameObject1.transform.position.y);
        float distance = Vector2.Distance(player, point);
        if (distance < Range)
            return true;
        return false;
    }

    #region 按下关卡以及飞机移动,包含update在内
    private bool isMoving = false;
    private float MovingTime = 1;
    private float TempMovingTime = 0;

    public void StagePointPressed(GameObject button) {
        if (!isMoving)
        {
            PlayerPosition = StagePointCheck.IndexOf(button);

            isMoving = true;
            TempMovingTime = 0;
            Vector3 TargetPosition = button.transform.position;//计算移动位置
            TargetPosition.z = PlayerPlane.transform.position.z;
            PlayerPlane.transform.DOMove(TargetPosition, MovingTime);//开始移动
        }
    }

    private void Update()//计时
    {
        if (isMoving)
        {
            TempMovingTime += Time.deltaTime;
            if (TempMovingTime >= MovingTime)
            {
                isMoving = false;
                TempMovingTime = 0;
                FreshStageButton();
            }
        }
    }

    #endregion

    public void ClearThisStage() {
        StagePointStatus[PlayerPosition] = 1;
        mapInfomation.StagePointStatus = StagePointStatus;

        FreshStageButton();
    }

    public void StartStage() {
        try
        {
            if (StagePointStatus[PlayerPosition] == 1)
            {
            }
            else
            {
                NowStageInfomation.GetInstance().Diffculty = StagePointCheck[PlayerPosition].GetComponent<StageDiffculty>().Diffculty;
                NowStageInfomation.GetInstance().isCleared = StagePointCheck[PlayerPosition].GetComponent<StageDiffculty>().isCleared;

                NowStageInfomation.GetInstance().mapConfigData = Camera.main.GetComponent<MapConfigData>();

                NowStageInfomation.GetInstance().PlayerPosition = PlayerPosition;
                //            Debug.Log(NowStageInfomation.GetInstance().mapConfigData.EasyStageWaveMinAmount);

                SceneManager.LoadScene("SampleScene");
            }
        }
        catch {
            SceneManager.LoadScene("SampleSceneBoss");
        }
    }
    
    public void EnterTestLevel() {
        SceneManager.LoadScene("SampleScene");
    }
    public void BackToHome() {
        SceneManager.LoadScene("StartScene");
    }

    public void EnterBossLevel() { 
    
    }
}
