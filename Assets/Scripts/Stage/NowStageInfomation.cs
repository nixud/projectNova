﻿public class NowStageInfomation
{
    #region 单例
    private static NowStageInfomation instance;
    private NowStageInfomation()
    {
    }
    public static NowStageInfomation GetInstance()
    {
        if (instance == null)
        {
            instance = new NowStageInfomation();
        }
        return instance;
    }

    #endregion

    public int StageSort = 0;
    public MapConfigData mapConfigData;
    public bool isCleared = false;
    public string Diffculty;
}
