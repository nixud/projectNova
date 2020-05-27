using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelectControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NormalMode()
    {
        SceneManager.LoadScene("NEWShipSelect");
    }

    public void MoveBack()
    {
        SceneManager.LoadScene("NEWStartScene");
    }
}
