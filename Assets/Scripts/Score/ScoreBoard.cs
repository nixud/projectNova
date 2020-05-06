using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour
{
    public Text StageScore;
    // Start is called before the first frame update
    void Start()
    {
        StageScore.text = "关卡分数为：" + ScoreData.Instance.levelScore.ToString();
    }

    public void BackToStageScene() {
        SceneManager.LoadScene("StageSelect");
        ScoreData.Instance.levelScore = 0;
    }

    public void BackToStageSceneFailed()
    {
        PlayerStatus.GetInstance().HP = PlayerStatus.GetInstance().MaxHP;
        SceneManager.LoadScene("StageSelect");
        ScoreData.Instance.levelScore = 0;
    }
}
