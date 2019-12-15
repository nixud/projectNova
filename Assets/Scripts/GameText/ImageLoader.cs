using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageLoader
{
    private string type;
    private int picNumber = 0;
    string[] s;
    TextAsset textAsset = new TextAsset();
    public ImageLoader(string type) {
        textAsset = Resources.Load("PicConfig/"+type) as TextAsset;
        this.type = type;
        s = textAsset.ToString().Replace("\r", "").Split('\n');
        picNumber = int.Parse(s[0]);
    }

    public Sprite GetPic(int number) {
        Sprite sprite = Resources.Load(s[1] + s[number + 1],typeof(Sprite)) as Sprite;
        return sprite;
    }

    public int GetNum() {
        return picNumber;
    }
}
