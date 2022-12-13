using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public LevelData Data;
    public List<StageData> _stageDatas = new List<StageData>();
    private List<int> _spawnedStageList = new List<int>();
    private List<int[]> _countersList = new List<int[]>();
    private List<float[]> _termTimersList = new List<float[]>();
    private List<float[]> _delayTimersList = new List<float[]>();

    private int _currentStage;

    public void StartSpawn(int stage)
    {
        // 소환하려는 스테이지가 유효한지 / 이미 소환중인지
        if (stage < 1 || 
            stage > Data.StageDatas.Count ||
            _spawnedStageList.Contains(stage))
            return;

        _spawnedStageList.Add(stage);
        StageData stageData = Data.StageDatas[stage - 1];
        int length = stageData.SpawnDatas.Count;

        _stageDatas.Add(stageData);
        int[] tmpCounters = new int[length];
        float[] tmpTermTimers = new float[length];
        float[] tmpDelayTimers = new float[length];

        for (int i = 0; i < length; i++)
        {
            tmpCounters[i] = stageData.SpawnDatas[i].Num;
            tmpTermTimers[i] = stageData.SpawnDatas[i].Term;
            tmpDelayTimers[i] = stageData.SpawnDatas[i].StartDelay;
        }

        _countersList.Add(tmpCounters);
        _termTimersList.Add(tmpTermTimers);
        _delayTimersList.Add(tmpDelayTimers);
    }
}
