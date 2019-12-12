using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class EnemyJsonLoader
{
    
    public List<Enemy> LoadData() {
        List<Enemy> weaponlist = new List<Enemy>();

#if UNITY_EDITOR
        FileStream fileStream = new FileStream(Application.dataPath + "/Resources/Jsons/json1.json", FileMode.Open);
        StreamReader sr = new StreamReader(fileStream);
        string line;
        string str = "";
        while ((line = sr.ReadLine()) != null)
        {
            str += line.ToString();
        }
        weaponlist = JsonConvert.DeserializeObject<List<Enemy>>(str);

        if (weaponlist == null)
            weaponlist = new List<Enemy>();

        fileStream.Close();
        sr.Close();
#else
        string str = Resources.Load<TextAsset>("Jsons/" + "json1").text;
        weaponlist = JsonConvert.DeserializeObject<List<Enemy>>(str);
#endif

        weaponlist = EnemySort(weaponlist);

        return weaponlist;
    }

    public Enemy LoadData(string EnemyNum)
    {
        List<Enemy> weaponlist = new List<Enemy>();

#if UNITY_EDITOR
        FileStream fileStream = new FileStream(Application.dataPath + "/Resources/Jsons/json1.json", FileMode.Open);
        StreamReader sr = new StreamReader(fileStream);
        string line;
        string str = "";
        while ((line = sr.ReadLine()) != null)
        {
            str += line.ToString();
        }
        weaponlist = JsonConvert.DeserializeObject<List<Enemy>>(str);

        if (weaponlist == null)
            weaponlist = new List<Enemy>();

        fileStream.Close();
        sr.Close();
#else
        string str = Resources.Load<TextAsset>("Jsons/" + "json1").text;
        weaponlist = JsonConvert.DeserializeObject<List<Enemy>>(str);
#endif

        Enemy returnEnemy = new Enemy();
        for (int i=0;i<weaponlist.Count;i++) {
            if (weaponlist[i].Number == EnemyNum)
                returnEnemy = weaponlist[i];
        }

        return returnEnemy;
    }

    public void SaveData(Enemy weapon) {
        List<Enemy> weaponlist = new List<Enemy>();

        weaponlist = LoadData();

        int index=0,i;
        for (i=0;i<weaponlist.Count;i++) {
            if (weaponlist[i].Number == weapon.Number)
                break;
        }
        index = i;
        if (index != weaponlist.Count)
        {
            weaponlist.RemoveAt(index);
        }
        weaponlist.Insert(index,weapon);

        File.WriteAllText(Application.dataPath + "/Resources/Jsons/json1.json", JsonConvert.SerializeObject(weaponlist));
    }
    public void DeleteData(int index, List<Enemy> weaponlist) {

        weaponlist.RemoveAt(index);

        //weaponlist = EnemySort(weaponlist);

        File.WriteAllText(Application.dataPath + "/Resources/Jsons/json1.json", JsonConvert.SerializeObject(weaponlist));
    }
    public List<Enemy> EnemySort(List<Enemy> weapons) {
        if(weapons.Count>=1)
            weapons.Sort();
        return weapons;
    } 
}
