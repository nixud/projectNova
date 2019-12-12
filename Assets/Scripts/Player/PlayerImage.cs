using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImage : MonoBehaviour
{
    public List<GameObject> Points = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangePointWeapon(int i,string picName) {
        if (i <= Points.Count)
            Points[i - 1].GetComponent<SpriteRenderer>().sprite = Resources.Load("Images/Weapons/"+picName,typeof(Sprite)) as Sprite;
    }
}
