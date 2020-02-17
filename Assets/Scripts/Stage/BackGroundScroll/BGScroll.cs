using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public List<Sprite> tiny = new List<Sprite>();
    public List<Sprite> small = new List<Sprite>();

    public GameObject scroll;

    public float ScrollSpeed = 0.5f;
    public float SpawnNumber = 50;

    private List<GameObject> stars = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        scroll = GameObject.Find("Scroll");

        float number = Camera.main.GetComponent<GameCamera>().GetdevHeight() + 2f;
        for (int i = 0; i < SpawnNumber; i++) {
            stars.Add(new GameObject());
            stars[i].AddComponent<SpriteRenderer>().sprite = small[0];
            stars[i].transform.position = new Vector3(Random.Range(-5.4f,5.4f), Random.Range(-10f, 10f));
            stars[i].transform.parent = scroll.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < SpawnNumber; i++) {
            stars[i].transform.Translate(new Vector3(0,-1*i*0.04f*ScrollSpeed*Time.deltaTime));
            if(stars[i].transform.position.y<-10f)
                stars[i].transform.position = new Vector3(Random.Range(-5.4f, 5.4f), 10f);
        }
    }
}
