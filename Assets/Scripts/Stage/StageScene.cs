using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageScene : MonoBehaviour
{
    private bool IsSpawned;

    public float Range = 3;

    public GameObject PlayerPlane;

    public Sprite EasyStageIcon;
    public Sprite NormalStageIcon;
    public Sprite DiffStageIcon;
    public Sprite SpecialStageIcon;

    public List<GameObject> EasyStagePoint = new List<GameObject>();
    public List<GameObject> NormalStagePoint = new List<GameObject>();
    public List<GameObject> DiffStagePoint = new List<GameObject>();
    public List<GameObject> SpecialStagePoint = new List<GameObject>();

    private List<GameObject> StagePointCheck = new List<GameObject>();
    private List<GameObject> StagePointCheckTemp = new List<GameObject>();

    private List<int> StagePointStatus = new List<int>();
    private int PlayerPosition = 0;

    public int EasyStageNum;
    public int NormalStageNum;
    public int DiffStageNum;
    public int SpecialStageNum;

    public int StageSort = 0;
    public MapConfigData mapConfigData;
    [HideInInspector]
    public bool isCleared = false;
    [HideInInspector]
    public string Diffculty;

    private bool CanPass = true;

    public void Start()
    {
        if (IsSpawned == false)
        {
            RandomStage();
            PlayerPlane = GameObject.Find("New Sprite");
            PlayerPlane.transform.position = StagePointCheck[0].transform.position;
            FreshStageButton();
            IsSpawned = true;
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
            int randomNumber = (int)Random.Range(0, EasyStagePoint.Count -0.01f);
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
            }
        }

        for (int i = 0; i < NormalStagePoint.Count - NormalStageNum;)
        {
            int randomNumber = (int)Random.Range(0, NormalStagePoint.Count - 0.01f);
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
            }
        }

        for (int i = 0; i < DiffStagePoint.Count - DiffStageNum;)
        {
            int randomNumber = (int)Random.Range(0, DiffStagePoint.Count - 0.01f);
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
            }
        }

        StagePointCheckTemp.Add(StagePointCheck[0]);
        CheckPointPass(0);

        if (!CanPass) RandomStage();
    }

    public void FreshStageButton()
    {
        for (int i = 0; i < EasyStagePoint.Count; i++)
            if (!RangeCalculate(EasyStagePoint[i],PlayerPlane))
                EasyStagePoint[i].GetComponent<Button>().interactable = false;
            else EasyStagePoint[i].GetComponent<Button>().interactable = true;

        for (int i = 0; i < NormalStagePoint.Count; i++)
            if (!RangeCalculate(NormalStagePoint[i], PlayerPlane))
                NormalStagePoint[i].GetComponent<Button>().interactable = false;
            else NormalStagePoint[i].GetComponent<Button>().interactable = true;

        for (int i = 0; i < DiffStagePoint.Count; i++)
            if (!RangeCalculate(DiffStagePoint[i], PlayerPlane))
                DiffStagePoint[i].GetComponent<Button>().interactable = false;
            else DiffStagePoint[i].GetComponent<Button>().interactable = true;

        for (int i = 0; i < StagePointStatus.Count; i++) {
            if (StagePointStatus[i] == 1)
                StagePointCheck[i].GetComponent<Image>().color = new Color(1,1,1,0.5f);
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

    public void StagePointPressed(GameObject button) {
        PlayerPosition = StagePointCheck.IndexOf(button);
        PlayerPlane.transform.position = button.transform.position;
        FreshStageButton();
    }

    public void ClearThisStage() {
        StagePointStatus[PlayerPosition] = 1;
        FreshStageButton();
    }

    public void StartStage() {
        NowStageInfomation.GetInstance().Diffculty = StagePointCheck[PlayerPosition].GetComponent<StageDiffculty>().Diffculty;
        NowStageInfomation.GetInstance().isCleared = StagePointCheck[PlayerPosition].GetComponent<StageDiffculty>().isCleared;

        NowStageInfomation.GetInstance().mapConfigData = this.GetComponent<MapConfigData>();

        Debug.Log(NowStageInfomation.GetInstance().mapConfigData.EasyStageWaveMinAmount);

        SceneManager.LoadScene("SampleScene");
    }
    
    public void EnterTestLevel() {
        SceneManager.LoadScene("SampleScene");
    }
    public void BackToHome() {
        SceneManager.LoadScene("StartScene");
    }
}
