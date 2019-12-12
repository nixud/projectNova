using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTextLoader
{
    TextAsset textAsset = new TextAsset();
    public GameTextLoader() {
        textAsset = Resources.Load("GameText/GameText",typeof(TextAsset)) as TextAsset;
    }
    public string GetText(int number) {
        string text = textAsset.ToString();
        string[] s = text.Split('\n',',');
        for (int i = 0; i < s.Length; i++) {
            if (s[i] == number.ToString()) return s[i + 1];
        }
        return "null";
    }
}
