using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTextLoader
{
    private string[][] Array;
    string[] lineArray;

    public GameTextLoader() {
        //读取csv二进制文件  
        TextAsset binAsset = Resources.Load("GameText/GameText", typeof(TextAsset)) as TextAsset;

        string tes = binAsset.text.Replace("<br />\n", "").Replace("\n", "");

        //读取每一行的内容  
        lineArray = tes.Split("\r"[0]);

        //创建二维数组  
        Array = new string[lineArray.Length][];

        //把csv中的数据储存在二位数组中  
        for (int i = 0; i < lineArray.Length; i++)
        {
            Array[i] = lineArray[i].Split(',');
        }

    }
    public string GetText(int number) {

        for (int i = 0; i < lineArray.Length; i++) {
            if (Array[i][0] == number.ToString()) return Array[i][1];
        }
        return "null";
    }
}
