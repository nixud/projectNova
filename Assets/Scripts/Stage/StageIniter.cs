using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//包括生成敌人、判断输赢在内的关卡内控制脚本。
public class StageIniter : MonoBehaviour
{
    private int WaveNumber;
    private List<Wave> thisStageWaves = new List<Wave>();

    #region 敌人生成
    private List<Wave> waves;

    private string Diffculty;

    private int StageWaveMinAmount;
    private int StageWaveMaxAmount;
    private int StageMinDiffculty;
    private int StageMaxDiffculty;

    private int StageStardandDiffculty;

    private int MapNum;

    private bool HighWave;

    private void OnEnable()
    {
        ObjectPool.GetInstance().EmptyPool();
        thisStageWaves.Clear();

        Diffculty = NowStageInfomation.GetInstance().Diffculty;

        if (!NowStageInfomation.GetInstance().isCleared) {
            if (Diffculty == "Easy") {
                StageMaxDiffculty = NowStageInfomation.GetInstance().mapConfigData.EasyStageMaxDiffculty;
                StageMinDiffculty = NowStageInfomation.GetInstance().mapConfigData.EasyStageMinDiffculty;

                StageWaveMinAmount = NowStageInfomation.GetInstance().mapConfigData.EasyStageWaveMinAmount;
                StageWaveMaxAmount = NowStageInfomation.GetInstance().mapConfigData.EasyStageWaveMaxAmount;

                StageStardandDiffculty = (int)((StageMinDiffculty + StageMaxDiffculty) * NowStageInfomation.GetInstance().mapConfigData.EasyStageDiffcultyRatio / 2);
            }
            else if (Diffculty == "Medium")
            {
                StageMaxDiffculty = NowStageInfomation.GetInstance().mapConfigData.MediumStageMaxDiffculty;
                StageMinDiffculty = NowStageInfomation.GetInstance().mapConfigData.MediumStageMinDiffculty;

                StageWaveMinAmount = NowStageInfomation.GetInstance().mapConfigData.MediumStageWaveMinAmount;
                StageWaveMaxAmount = NowStageInfomation.GetInstance().mapConfigData.MediumStageWaveMaxAmount;

                StageStardandDiffculty = (int)((StageMinDiffculty + StageMaxDiffculty) * NowStageInfomation.GetInstance().mapConfigData.MediumStageDiffcultyRatio / 2);
            }
            else if (Diffculty == "Hard")
            {
                StageMaxDiffculty = NowStageInfomation.GetInstance().mapConfigData.HardStageMaxDiffculty;
                StageMinDiffculty = NowStageInfomation.GetInstance().mapConfigData.HardStageMinDiffculty;

                StageWaveMinAmount = NowStageInfomation.GetInstance().mapConfigData.HardStageWaveMinAmount;
                StageWaveMaxAmount = NowStageInfomation.GetInstance().mapConfigData.HardStageWaveMaxAmount;

                StageStardandDiffculty = (int)((StageMinDiffculty + StageMaxDiffculty) * NowStageInfomation.GetInstance().mapConfigData.HardStageDiffcultyRatio / 2);
            }

            MapNum = NowStageInfomation.GetInstance().mapConfigData.MapNumber;


            WaveNumber = Random.Range(StageWaveMinAmount, StageWaveMaxAmount);

            JsonLoader<Wave> loader = new JsonLoader<Wave>();
            waves = loader.LoadData();

            if (WaveNumber % 2 != 0)
                WaveNumber++;

            HighWave = false;

            List<Wave> waveTemp = GetTargetWaves(StageMinDiffculty, StageMaxDiffculty);
//            Debug.Log(StageStardandDiffculty);
            for (int i = 0; i < WaveNumber; i++) {
                if (HighWave) {
                    thisStageWaves.Add(GetRandomWave(waveTemp, StageStardandDiffculty, StageMaxDiffculty));
                }
                else thisStageWaves.Add(GetRandomWave(waveTemp, StageMinDiffculty, StageStardandDiffculty));

                HighWave = !HighWave;
            }

        }
    }

    private List<Wave> GetTargetWaves(int MinDiff, int MaxDiff) {
        List<Wave> waveTemp = new List<Wave>();
        for (int i = 0; i < waves.Count; i++) {
            if (waves[i].Diffculty >= MinDiff && waves[i].Diffculty <= MaxDiff && waves[i].MapNum == MapNum) {
                waveTemp.Add(waves[i]);
            }
        }
        return waveTemp;
    }
    private Wave GetRandomWave(List<Wave> waveS, int MinDiff, int MaxDiff) {
        int i = 0;
        while (i <= 100000) {
            Wave temp = waveS[(int)Random.Range(0, waveS.Count - 0.01f)];
            if (temp.Diffculty >= MinDiff && temp.Diffculty <= MaxDiff) {
                return temp;
            }
            i++;
        }
        throw new System.Exception();
    }
    #endregion

    private float time = 0;
    private int NowKilledEnemyNumber = 0;

    private int thisWavePointer = 0;

    private bool thisWaveFinished = false;

    private void Update()
    {
        if (thisStageWaves.Count == 0)
            PlayerWin();
        else
        {
            if (thisWavePointer == thisStageWaves[0].EnemyNumber.Count)
                thisWaveFinished = true;
            try
            {
                if (!thisWaveFinished && time >= thisStageWaves[0].EnemyTime[thisWavePointer])
                {
                    time = 0;
                    UseSpawnEnemy();

                }
            }
            catch {
                Debug.Log("在难度为" + thisStageWaves[0].Diffculty + "的波次中，时间和怪物个数不匹配");
            }
            time += Time.deltaTime;
        }
    }

    private void NextWave()
    {
        NowKilledEnemyNumber = 0;
        time = 0;
        thisWavePointer = 0;
        thisWaveFinished = false;

        if (thisStageWaves.Count >= 1) {
            thisStageWaves.RemoveAt(0);
        }
        else {
            PlayerWin();
        }
    }

    private void UseSpawnEnemy() {
        if (thisWavePointer == thisStageWaves[0].EnemyNumber.Count)
            thisWaveFinished = true;
//        Debug.Log(thisStageWaves[0].EnemyPositionX[thisWavePointer]);
        SpawnEnemy(thisStageWaves[0].EnemyNumber[thisWavePointer],
                thisStageWaves[0].EnemyPositionX[thisWavePointer],
                thisStageWaves[0].EnemyPositionY[thisWavePointer],
                thisStageWaves[0].EnemyBehNumber[thisWavePointer]);
        if (thisWavePointer < thisStageWaves[0].EnemyNumber.Count)
            thisWavePointer++;
        if(thisWavePointer != thisStageWaves[0].EnemyNumber.Count && thisStageWaves[0].EnemyTime[thisWavePointer] <= time) {
            UseSpawnEnemy();
        }
    }

    private void SpawnEnemy(string enemyNumber,float positionX, float positionY, int behNumber) {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + "Enemies" + "/" + enemyNumber);
        GameObject result = Instantiate(prefab);
        var s = behNumber.ToString();
        s = s.Length == 2 ? s : "0" + s;
        result.name = s + enemyNumber;
        //Debug.Log(Camera.main.GetComponent<GameCamera>().GetdevWidth() / 2 * positionX);
        result.transform.position = new Vector2(Camera.main.GetComponent<GameCamera>().GetdevWidth()/2 * positionX,
            Camera.main.GetComponent<GameCamera>().GetdevHeight()/2 * positionY);
    }

    public void KilledOneEnemy(){
        NowKilledEnemyNumber++;
        if (NowKilledEnemyNumber == thisStageWaves[0].EnemyNumber.Count)
            NextWave();
    }

    public void PlayerWin()
    {
        NowStageInfomation.GetInstance().isCleared = true;
        SceneManager.LoadScene("ScoreBroad");
        ObjectPool.GetInstance().EmptyPool();
    }
}
