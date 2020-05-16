using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemSceneNext : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("StageSelect");
    }
}
