using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
public class MoveCamera : MonoBehaviour
{
    private bool isMouseDrag;

    Vector3 screenPosition;
    Vector3 offset;

    public float MaxY = 3.5f;
    public float MaxX = 8f;

    void Start()
    {
        isMouseDrag = false;
    }
    void OnMouseDown()
    {
        screenPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));
        isMouseDrag = true;
    }
    void OnMouseDrag()
    {
        if (isMouseDrag)
        {
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;
            if (currentPosition.y > MaxY) {
                currentPosition = new Vector3(currentPosition.x, MaxY, currentPosition.z);
            }
            if (currentPosition.y < -MaxY)
            {
                currentPosition = new Vector3(currentPosition.x, -MaxY, currentPosition.z);
            }
            if (currentPosition.x > MaxX)
            {
                currentPosition = new Vector3(MaxX, currentPosition.y, currentPosition.z);
            }
            if (currentPosition.x < -MaxX)
            {
                currentPosition = new Vector3(-MaxX, currentPosition.y, currentPosition.z);
            }
            gameObject.transform.position = currentPosition;
        }
    }
    void OnMouseUp()
    {
        isMouseDrag = false;
    }
}