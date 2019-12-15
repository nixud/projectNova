using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameTextLoader gameTextLoader = new GameTextLoader();
        Debug.Log(gameTextLoader.GetText(1));
    }
}
