using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageIniter : MonoBehaviour
{
    private int WaveNumber;
    private List<Wave> waves;
    private List<Wave> thisStageWaves;
    private string Diffculty;

    private int StageWaveMinAmount;
    private int StageWaveMaxAmount;
    private int StageMinDiffculty;
    private int StageMaxDiffculty;

    private int StageStardandDiffculty;

    private void Awake()
    {
        ObjectPool.GetInstance().EmptyPool();
        thisStageWaves.Clear();
        if (!NowStageInfomation.GetInstance().isCleared) {
            if(Diffculty == "Easy") {
                StageMaxDiffculty = NowStageInfomation.GetInstance().mapConfigData.EasyStageMaxDiffculty;
                StageMinDiffculty = NowStageInfomation.GetInstance().mapConfigData.EasyStageMinDiffculty;

                StageWaveMinAmount = NowStageInfomation.GetInstance().mapConfigData.EasyStageWaveMinAmount;
                StageWaveMaxAmount = NowStageInfomation.GetInstance().mapConfigData.EasyStageWaveMaxAmount;

                StageStardandDiffculty = (StageWaveMinAmount + StageWaveMaxAmount) * NowStageInfomation.GetInstance().mapConfigData.EasyStageDiffcultyRatio / 2;
            }
            else if (Diffculty == "Medium")
            {
                StageMaxDiffculty = NowStageInfomation.GetInstance().mapConfigData.MediumStageMaxDiffculty;
                StageMinDiffculty = NowStageInfomation.GetInstance().mapConfigData.MediumStageMinDiffculty;

                StageWaveMinAmount = NowStageInfomation.GetInstance().mapConfigData.MediumStageWaveMinAmount;
                StageWaveMaxAmount = NowStageInfomation.GetInstance().mapConfigData.MediumStageWaveMaxAmount;

                StageStardandDiffculty = (StageWaveMinAmount + StageWaveMaxAmount) * NowStageInfomation.GetInstance().mapConfigData.MediumStageDiffcultyRatio / 2;
            }
            else if (Diffculty == "Hard")
            {
                StageMaxDiffculty = NowStageInfomation.GetInstance().mapConfigData.HardStageMaxDiffculty;
                StageMinDiffculty = NowStageInfomation.GetInstance().mapConfigData.HardStageMinDiffculty;

                StageWaveMinAmount = NowStageInfomation.GetInstance().mapConfigData.HardStageWaveMinAmount;
                StageWaveMaxAmount = NowStageInfomation.GetInstance().mapConfigData.HardStageWaveMaxAmount;

                StageStardandDiffculty = (StageWaveMinAmount + StageWaveMaxAmount) * NowStageInfomation.GetInstance().mapConfigData.HardStageDiffcultyRatio / 2;
            }

            WaveNumber = Random.Range(StageWaveMinAmount,StageWaveMaxAmount);

            JsonLoader<Wave> loader = new JsonLoader<Wave>();
            waves = loader.LoadData();

            List<Wave> waveTemp = new List<Wave>();
            for(int i = 0; i < WaveNumber; i++) {

            }
        }
    }

    private List<Wave> GetTargetWaves(int MinDiff,int MaxDiff) {
        List<Wave> waveTemp = new List<Wave>();
        for (int i = 0; i < waves.Count; i++) {
            if (waves[i].Diffculty >= MinDiff && waves[i].Diffculty <= MaxDiff) {
                waveTemp.Add(waves[i]);
            }
        }
    }
}
