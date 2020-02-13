﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConfigData:MonoBehaviour {
    public int MapNumber;

    public int EasyStageWaveMinAmount;
    public int EasyStageWaveMaxAmount;
    public int MediumStageWaveMinAmount;
    public int MediumStageWaveMaxAmount;
    public int HardStageWaveMinAmount;
    public int HardStageWaveMaxAmount;

    public int EasyStageMinDiffculty;
    public int EasyStageMaxDiffculty;
    public int MediumStageMinDiffculty;
    public int MediumStageMaxDiffculty;
    public int HardStageMinDiffculty;
    public int HardStageMaxDiffculty;

    public float EasyStageDiffcultyRatio;
    public float MediumStageDiffcultyRatio;
    public float HardStageDiffcultyRatio;
}