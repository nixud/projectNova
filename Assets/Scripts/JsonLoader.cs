using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class JsonLoader<T>
{
    public List<T> LoadData() {
        List<T> list = new List<T>();

#if UNITY_EDITOR
        FileStream fileStream = new FileStream(Application.dataPath + "/Resources/Jsons/json"+typeof(T)+".json", FileMode.Open);
        StreamReader sr = new StreamReader(fileStream);
        string line;
        string str = "";
        while ((line = sr.ReadLine()) != null)
        {
            str += line.ToString();
        }
        list = JsonConvert.DeserializeObject<List<T>>(str);

        if (list == null)
            list = new List<T>();

        fileStream.Close();
        sr.Close();
#else
        string str = Resources.Load<TextAsset>("Jsons/json" +typeof(T)).text;
        list = JsonConvert.DeserializeObject<List<T>>(str);
#endif

        return list;
    }

    public void SaveData(List<T> list) {
        File.WriteAllText(Application.dataPath + "/Resources/Jsons/json" + typeof(T) + ".json", JsonConvert.SerializeObject(list));
    }
}
