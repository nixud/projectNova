using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewScroll : MonoBehaviour
{
    public int minSize = 4;
    public int maxSize = 12;

    public Scrollbar scrollbar;
    public Camera camerA;

    public void OnValueChanged() {
        camerA.orthographicSize = minSize + (maxSize - minSize) * scrollbar.value;
    }
}
