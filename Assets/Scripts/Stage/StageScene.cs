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

    public int EasyStageNum;
    public int NormalStageNum;
    public int DiffStageNum;
    public int SpecialStageNum;

    public void Start()
    {
        if (IsSpawned == false)
        {
            RandomStage();
            PlayerPlane = GameObject.Find("New Sprite");
            PlayerPlane.transform.position = EasyStagePoint[0].transform.position;
            FreshStageButton();
            IsSpawned = true;
        }
    }

    public void RandomStage() {
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
        for (int i = 0; i < NormalStagePoint.Count - NormalStageNum;)
        {
            int randomNumber = (int)Random.Range(0, NormalStagePoint.Count - 0.01f);
            if (NormalStagePoint[randomNumber].activeSelf)
            {
                NormalStagePoint[randomNumber].SetActive(false);
                i++;
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
    }

    public void FreshStageButton()
    {
        for (int i = 0; i < EasyStagePoint.Count; i++)
            if (!RangeCalculate(EasyStagePoint[i],PlayerPlane))
                EasyStagePoint[i].GetComponent<Button>().interactable = false;
        for (int i = 0; i < NormalStagePoint.Count; i++)
            if (!RangeCalculate(NormalStagePoint[i], PlayerPlane))
                NormalStagePoint[i].GetComponent<Button>().interactable = false;
        for (int i = 0; i < DiffStagePoint.Count; i++)
            if (!RangeCalculate(DiffStagePoint[i], PlayerPlane))
                DiffStagePoint[i].GetComponent<Button>().interactable = false;
    }

    private bool RangeCalculate(GameObject gameObject,GameObject gameObject2) {
        Vector2 player = new Vector2(gameObject2.transform.position.x, PlayerPlane.transform.position.y);
        Vector2 point = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        float distance = Vector2.Distance(player, point);
        if (distance < Range)
            return true;
        return false;
    }

    public void EnterTestLevel() {
        SceneManager.LoadScene("SampleScene");
    }
    public void BackToHome() {
        SceneManager.LoadScene("StartScene");
    }
}
