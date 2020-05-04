using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//这个脚本用于测试波次。
//包括生成敌人、判断输赢在内的关卡内控制脚本。
public class StageIniterTest : MonoBehaviour
{
    private int WaveNumber;
    private List<Wave> thisStageWaves = new List<Wave>();

    public int TargetDiffculty = 50;

    #region 敌人生成
    private List<Wave> waves;

    private string Diffculty;

    private int StageStardandDiffculty;

    private int MapNum;

    private bool HighWave;

    private void OnEnable()
    {
        ObjectPool.GetInstance().EmptyPool();
        thisStageWaves.Clear();

        Diffculty = NowStageInfomation.GetInstance().Diffculty;

        JsonLoader<Wave> loader = new JsonLoader<Wave>();
        waves = loader.LoadData();

        for (int i = 0; i < waves.Count; i++) {
            if (waves[i].Diffculty == TargetDiffculty) {
                thisStageWaves.Add(waves[i]);
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
            Debug.Log("测试已完成");
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
            Debug.Log("测试已完成");
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
        result.name = behNumber.ToString() + enemyNumber;
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
