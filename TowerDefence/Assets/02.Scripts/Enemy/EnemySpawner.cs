using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 레벨 데이터에 따라 적을 생성함
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    public LevelData Data;
    private List<StageData> _stageDatas = new List<StageData>(); // 현재 소환 진행중인 스테이지 데이터들
    private List<int> _spawnedStageList = new List<int>(); // 소환을 진행한 스테이지 인덱스들
    private List<int[]> _countersList = new List<int[]>(); // 소환중인 적의 남은 소환 마릿수들을 각 스테이지별로 담아놓는 리스트
    private List<float[]> _termTimersList = new List<float[]>(); // 소환중인 적들의 소환주기들을 각 스테이지별로 담아놓는 리스트
    private List<float[]> _delayTimersList = new List<float[]>(); // 소환중인 적들의 시작지연시간을 각 스테이지별로 담아놓는 리스트

    private int _currentStage; // 가장 최근 소환시작한 스테이지
    [SerializeField] private Vector3 _offset;

    /// <summary>
    /// 다음 스테이지를 시작하는 함수
    /// </summary>
    public void StartNext()
    {
        _currentStage++;
        StartSpawn(_currentStage);
    }

    /// <summary>
    /// 특정 스테이지의 소환을 시작하는 함수
    /// </summary>
    /// <param name="stage">소환하고싶은 스테이지</param>
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

        _stageDatas.Add(stageData); // 현재 소환중인 리스트에 이 스테이지 추가
        int[] tmpCounters = new int[length]; // 이 스테이지에서 소환해야하는 몬스터 각각의 마릿수
        float[] tmpTermTimers = new float[length]; // 이 스테이지에서 소환해야하는 몬스터 각각의 소환 주기
        float[] tmpDelayTimers = new float[length]; // 이 스테이지에서 소환해야하는 몬스터 각각의 시작 지연

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

    private void Start()
    {
        RegisterToObjectPool();
        ObjectPool.Instance.InstantiateAllElements();
    }

    private void Update()
    {
        Spawning();
    }

    private void Spawning()
    {
        // 자료구조를 순회하는 도중에 요소를 제거해야한다면, for문을 거꾸로 순회하면 된다.
        // 소환중인 모든 스테이지 순회 
        for (int i = _stageDatas.Count - 1; i >= 0; i--)
        {
            bool isFinished = true;

            // 해당 스테이지에서 소환해야하는 모든 적들에 대한 소환 데이터 순회
            for (int j = 0; j < _stageDatas[i].SpawnDatas.Count; j++)
            {
                // 소환할것이 남아있는지 체크
                if (_countersList[i][j] > 0)
                {
                    isFinished = false;

                    // 소환 시작 지연 체크
                    if (_delayTimersList[i][j] <= 0)
                    { 
                        // 소환 주기 체크
                        if (_termTimersList[i][j] <= 0)
                        {
                            Enemy enemy = ObjectPool.Instance.Spawn(_stageDatas[i].SpawnDatas[j].Prefab.name,
                                                                    Paths.Instance.StartPoints[_stageDatas[i].SpawnDatas[j].StartPointIndex].position
                                                                    + _stageDatas[i].SpawnDatas[j].Prefab.transform.position
                                                                    + _offset,
                                                                    Quaternion.identity).GetComponent<Enemy>();

                            enemy.OnHpMin += () => ObjectPool.Instance.Return(enemy.gameObject);
                            enemy.SetPath(Paths.Instance.StartPoints[_stageDatas[i].SpawnDatas[j].StartPointIndex],
                                          Paths.Instance.EndPoints[_stageDatas[i].SpawnDatas[j].EndPointIndex]);


                            _countersList[i][j]--;
                            _termTimersList[i][j] = _stageDatas[i].SpawnDatas[j].Term;
                        }
                        else
                        {
                            _termTimersList[i][j] -= Time.deltaTime;
                        }
                    }
                    else
                    {
                        _delayTimersList[i][j] -= Time.deltaTime;
                    }
                }
            }

            if (isFinished)
            {
                _stageDatas.RemoveAt(i);
                _countersList.RemoveAt(i);
                _termTimersList.RemoveAt(i);
                _delayTimersList.RemoveAt(i);
            }
        }
    }

    private void RegisterToObjectPool()
    {
        foreach (StageData stageData in Data.StageDatas)
        {
            foreach (SpawnData spawnData in stageData.SpawnDatas)
            {
                ObjectPool.Instance.AddElement(new ObjectPool.Element(spawnData.Prefab.name,
                                                                      spawnData.Prefab,
                                                                      spawnData.Num));
            }
        }
    }
}
