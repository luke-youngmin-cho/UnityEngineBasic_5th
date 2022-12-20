using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �����Ϳ� ���� ���� ������
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    public LevelData Data;
    private List<StageData> _stageDatas = new List<StageData>(); // ���� ��ȯ �������� �������� �����͵�
    private List<int> _spawnedStageList = new List<int>(); // ��ȯ�� ������ �������� �ε�����
    private List<int[]> _countersList = new List<int[]>(); // ��ȯ���� ���� ���� ��ȯ ���������� �� ������������ ��Ƴ��� ����Ʈ
    private List<float[]> _termTimersList = new List<float[]>(); // ��ȯ���� ������ ��ȯ�ֱ���� �� ������������ ��Ƴ��� ����Ʈ
    private List<float[]> _delayTimersList = new List<float[]>(); // ��ȯ���� ������ ���������ð��� �� ������������ ��Ƴ��� ����Ʈ

    private int _currentStage; // ���� �ֱ� ��ȯ������ ��������
    [SerializeField] private Vector3 _offset;

    /// <summary>
    /// ���� ���������� �����ϴ� �Լ�
    /// </summary>
    public void StartNext()
    {
        _currentStage++;
        StartSpawn(_currentStage);
    }

    /// <summary>
    /// Ư�� ���������� ��ȯ�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="stage">��ȯ�ϰ���� ��������</param>
    public void StartSpawn(int stage)
    {
        // ��ȯ�Ϸ��� ���������� ��ȿ���� / �̹� ��ȯ������
        if (stage < 1 || 
            stage > Data.StageDatas.Count ||
            _spawnedStageList.Contains(stage))
            return;

        _spawnedStageList.Add(stage);
        StageData stageData = Data.StageDatas[stage - 1];
        int length = stageData.SpawnDatas.Count;

        _stageDatas.Add(stageData); // ���� ��ȯ���� ����Ʈ�� �� �������� �߰�
        int[] tmpCounters = new int[length]; // �� ������������ ��ȯ�ؾ��ϴ� ���� ������ ������
        float[] tmpTermTimers = new float[length]; // �� ������������ ��ȯ�ؾ��ϴ� ���� ������ ��ȯ �ֱ�
        float[] tmpDelayTimers = new float[length]; // �� ������������ ��ȯ�ؾ��ϴ� ���� ������ ���� ����

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
        // �ڷᱸ���� ��ȸ�ϴ� ���߿� ��Ҹ� �����ؾ��Ѵٸ�, for���� �Ųٷ� ��ȸ�ϸ� �ȴ�.
        // ��ȯ���� ��� �������� ��ȸ 
        for (int i = _stageDatas.Count - 1; i >= 0; i--)
        {
            bool isFinished = true;

            // �ش� ������������ ��ȯ�ؾ��ϴ� ��� ���鿡 ���� ��ȯ ������ ��ȸ
            for (int j = 0; j < _stageDatas[i].SpawnDatas.Count; j++)
            {
                // ��ȯ�Ұ��� �����ִ��� üũ
                if (_countersList[i][j] > 0)
                {
                    isFinished = false;

                    // ��ȯ ���� ���� üũ
                    if (_delayTimersList[i][j] <= 0)
                    { 
                        // ��ȯ �ֱ� üũ
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
