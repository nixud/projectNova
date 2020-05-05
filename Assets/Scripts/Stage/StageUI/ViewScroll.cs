using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewScroll : MonoBehaviour
{
    public int minSize = 4;
    public int maxSize = 12;

    public Scrollbar scrollbar;
    public Camera _camera;
    public GameObject player;

    public void OnValueChanged() {
        _camera.orthographicSize = minSize + (maxSize - minSize) * scrollbar.value;

    }

    private void Update()
    {

        Vector3 _cameraPosition = player.transform.localPosition * (1 - scrollbar.value);
        _cameraPosition.z = _camera.transform.localPosition.z;
        _camera.transform.localPosition = _cameraPosition;
    }
}
