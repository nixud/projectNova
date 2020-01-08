using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneControl : MonoBehaviour
{
    public void EnterGame()
    {
        SceneManager.LoadScene("SelectInfoSence");
    }
}
