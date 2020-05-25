using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager
{
    private readonly static string loadSceneName = "LoadScene";

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(loadSceneName);
        
    }
    
    
}
