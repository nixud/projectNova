using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipSelectControl : MonoBehaviour
{
    public void MoveToNext()
    {
        SceneManager.LoadScene("itemSelectScene");
    }

    public void MoveBack()
    {
        SceneManager.LoadScene("NEWModeSelect");
    }
}
