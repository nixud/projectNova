using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//单张地图（目前就一张）的设定。
public class MapConfigData:MonoBehaviour {
    public int MapNumber;

    #region 波次数量

    /// <summary>
    /// 简单关卡的波次最小数量
    /// </summary>
    public int EasyStageWaveMinAmount;
    /// <summary>
    /// 简单关卡的波次最大数量
    /// </summary>
    public int EasyStageWaveMaxAmount;
    /// <summary>
    /// 中等关卡的波次最小数量
    /// </summary>
    public int MediumStageWaveMinAmount;
    /// <summary>
    /// 中等关卡的波次最大数量
    /// </summary>
    public int MediumStageWaveMaxAmount;
    /// <summary>
    /// 困难关卡的波次最小数量
    /// </summary>
    public int HardStageWaveMinAmount;
    /// <summary>
    /// 困难关卡的波次最大数量
    /// </summary>
    public int HardStageWaveMaxAmount;

    #endregion

    #region 关卡难度

    /// <summary>
    /// 简单关卡的最小难度
    /// </summary>
    public int EasyStageMinDiffculty;
    /// <summary>
    /// 简单关卡的最大难度
    /// </summary>
    public int EasyStageMaxDiffculty;
    /// <summary>
    /// 中等关卡的最小难度
    /// </summary>
    public int MediumStageMinDiffculty;
    /// <summary>
    /// 中等关卡的最大难度
    /// </summary>
    public int MediumStageMaxDiffculty;
    /// <summary>
    /// 困难关卡的最小难度
    /// </summary>
    public int HardStageMinDiffculty;
    /// <summary>
    /// 苦难关卡的最大难度
    /// </summary>
    public int HardStageMaxDiffculty;

    public float EasyStageDiffcultyRatio;
    public float MediumStageDiffcultyRatio;
    public float HardStageDiffcultyRatio;


    public int EasyStageDiffcultyIncrease;//简单关卡难度增长量
    public int MediumStageDiffcultyIncrease;//中等关卡难度增长量
    public int HardStageDiffcultyIncrease;//困难关卡难度增长量

    public int EasyStageDiffcultyIncreaseMaxTimes;//简单关卡难度增长最大次数
    public int MediumStageDiffcultyIncreaseMaxTimes;//中等关卡难度增长最大次数
    public int HardStageDiffcultyIncreaseMaxTimes;//困难关卡难度增长最大次数

    #endregion
}