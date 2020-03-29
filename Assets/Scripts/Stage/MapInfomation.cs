using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用于关卡与地图间传值。
public class MapInfomation
{
    #region 单例
    private static MapInfomation instance;
    private MapInfomation()
    {
    }
    public static MapInfomation GetInstance()
    {
        if (instance == null)
        {
            instance = new MapInfomation();
        }
        return instance;
    }
    #endregion

    public char[] MapStatus = "00000000000000000000".ToCharArray();

    public List<GameObject> StagePointCheck;
    public List<GameObject> StagePointCheckTemp;

    public int EasyStagePointNum;
    public int NormalStagePointNum;
    public int DiffStagePointNum;
    public int SpecialStagePointNum;

    public int EasyStagePointCount;
    public int NormalStagePointCount;
    public int DiffStagePointCount;
    public int SpecialStagePointCount;

    public List<int> StagePointStatus;
    public int PlayerPosition = 0;

    public List<int> StagePointCheckTable = new List<int>();

}
