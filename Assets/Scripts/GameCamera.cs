using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameCamera : MonoBehaviour
{

    float devHeight = 19.2f;
    float devWidth = 10.8f;

    public GameObject player;

    void Start()
    {
        devHeight = devWidth * ((float)Screen.height / (float)Screen.width);

        float screenHeight = Screen.height;

        //this.GetComponent<Camera>().orthographicSize = screenHeight / 200.0f;

        float orthographicSize = this.GetComponent<Camera>().orthographicSize;

        float aspectRatio = Screen.width * 1.0f / Screen.height;

        float cameraWidth = orthographicSize * 2 * aspectRatio;

        if (cameraWidth != devWidth)
        {
            orthographicSize = devWidth / (2 * aspectRatio);
            //Debug.Log("new orthographicSize = " + orthographicSize);
            this.GetComponent<Camera>().orthographicSize = orthographicSize;
        }

    }

    public float GetdevWidth() {
        return devWidth;
    }
    public float GetdevHeight() {
        return devHeight;
    }
}