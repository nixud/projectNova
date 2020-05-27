using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    public void GoToNextScene()
    {
        SceneManager.LoadScene("StageSelect");
        ScoreData.Instance.levelScore = 0;
        ObjectPool.GetInstance().EmptyPool();
    }
}
