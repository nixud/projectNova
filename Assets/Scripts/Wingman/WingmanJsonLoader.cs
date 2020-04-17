using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class WingmanJsonLoader
{
    /// <summary>
    /// 读取jsonWingman中所有wingman信息并写入list返回
    /// </summary>
    /// <returns>json中的wingman信息list</returns>
    public List<Wingman> LoadData()
    {
        JsonLoader<Wingman> loader = new JsonLoader<Wingman>();
        return loader.LoadData();
    }
    /// <summary>
    /// 按wingman的编号查找json中相对应的信息
    /// </summary>
    /// <param name="wingmanNum">所需查找wingman的编号</param>
    /// <returns>所需查找的wingman在json中的信息，若查找失败则是空wingman</returns>
    public Wingman LoadData(string wingmanNum)
    {
        JsonLoader<Wingman> loader = new JsonLoader<Wingman>();
        List<Wingman> wingmanList = new List<Wingman>();

        wingmanList = loader.LoadData();

        Wingman returnWingman = new Wingman();
        for (int i = 0; i < wingmanList.Count; i++)
        {
            if (wingmanList[i].Number == wingmanNum)
                returnWingman = wingmanList[i];
        }

        return returnWingman;
    }
    /// <summary>
    /// 将新wingman写入json，若编号已存在则直接覆盖
    /// </summary>
    /// <param name="wingman">所需写入的wingman</param>
    public void SaveData(Wingman wingman)
    {
        JsonLoader<Wingman> loader = new JsonLoader<Wingman>();
        List<Wingman> wingmanList = new List<Wingman>();
        wingmanList = LoadData();       //取出json中的数据进行比对

        int i = 0;
        for (i = 0; i < wingmanList.Count; i++)     //探查是否有同编号的僚机信息
        {
            if (wingmanList[i].Number == wingman.Number)
                break;
        }
        if (i != wingmanList.Count)     //若有，则删除原本的僚机信息
        {
            wingmanList.RemoveAt(i);
        }
        wingmanList.Insert(i, wingman);     //将新僚机信息插入
        if (wingmanList.Count > 1)          //将僚机信息按编号数字大小排序
        {
                wingmanList.Sort();
        }

        loader.SaveData(wingmanList);       //将僚机列表覆盖写入json
    }
    /// <summary>
    /// 删除json文件中index位置上的wingman信息
    /// </summary>
    /// <param name="index">所选位置</param>
    /// <param name="wingmanList">传入的要存储修改的list</param>
    public void DeleteData(int index, List<Wingman> wingmanList)
    {
        wingmanList.RemoveAt(index);
        JsonLoader<Wingman> loader = new JsonLoader<Wingman>();
        loader.SaveData(wingmanList);
    }
}
