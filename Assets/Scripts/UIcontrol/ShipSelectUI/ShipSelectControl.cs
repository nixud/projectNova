using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipSelectControl : MonoBehaviour
{
    #region 临时出包飞船信息填充
    
    
    private int _index = 0;
    private GameObject[] _prefabs = new GameObject[2];

    private GameObject playerObj;
    private void Start()
    {
        _prefabs[0] = Resources.Load<GameObject>(PlayerSelect.Ship1Path);
        _prefabs[1] = Resources.Load<GameObject>(PlayerSelect.Ship2Path);
        playerObj =Instantiate(_prefabs[0]);
        playerObj.transform.position = new Vector3(0, 2.5f, 0);
        gameObject.GetComponent<DescriptionFill>().FillDescription(ShipInfo.Ship1);
        _index = 0;
        PlayerSelect.Instance.PlayerShip = "Player0";
    }

    private void Update()
    {
        
    }


    private bool _buttonCanPressed = true;
    public void SelectNext()
    {

        if (!_buttonCanPressed)
            return;
        
        Destroy(playerObj);
        if (_index == 0)
        {
            _index = 1;
            playerObj = Instantiate(_prefabs[1]);
            playerObj.transform.position = new Vector3(0, 2.5f, 0);
            gameObject.GetComponent<DescriptionFill>().FillDescription(ShipInfo.Ship2);
            PlayerSelect.Instance.PlayerShip = "Player1";
        }
        else
        {
            _index = 0;
            playerObj = Instantiate(_prefabs[0]);
            playerObj.transform.position = new Vector3(0, 2.5f, 0);
            gameObject.GetComponent<DescriptionFill>().FillDescription(ShipInfo.Ship1);
            PlayerSelect.Instance.PlayerShip = "Player0";
        }
        StartCoroutine(CountSecond(0.5f));
    }

    IEnumerator CountSecond(float t)
    {
        _buttonCanPressed = false;
        yield return new WaitForSeconds(t);
        _buttonCanPressed = true;
    }
    
    #endregion

    
    public void MoveToNext()
    {
        SceneManager.LoadScene("itemSelectScene");
    }

    public void MoveBack()
    {
        SceneManager.LoadScene("NEWModeSelect");
    }
}
