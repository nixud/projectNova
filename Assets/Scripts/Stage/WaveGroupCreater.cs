using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGroupCreater
{
    //此段代码已作废

    private List<Wave> waves;
    public WaveGroupCreater() {
        JsonLoader<Wave> loader = new JsonLoader<Wave>();
        waves = loader.LoadData();
    }
    public List<Wave> CreateStrongWave(int map,int diffAVG,int diffMAX) {
        List<Wave> mapWave = new List<Wave>();
        List<Wave> FinalWave = new List<Wave>();
        int totalDiff = 0;
        for (int i=0;i<waves.Count;i++) {
            if (waves[i].MapNum == map) mapWave.Add(waves[i]);
        }
        while (totalDiff<diffAVG || totalDiff>diffMAX) {
            totalDiff = 0;
            FinalWave = new List<Wave>();
            int nums = Random.Range(1,5);
            
            for (int i = 0; i < nums; i++) {
                Wave waveTemp = mapWave[Random.Range(0, mapWave.Count)];
                FinalWave.Add(waveTemp);
                totalDiff += waveTemp.Diffculty;
            }
        }
        return FinalWave;
    }
}
