using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum StateType
    {
        Idle,
        WaitUntilLevelSelected,
        StartLevel,
        WaitUntilLevelFinished,
        SucceedLevel,
        FailLevel,
        WaitForUser,
    }

    public static GameManager Instance;
    public StateType Current;
    public LevelData Data;
    [SerializeField] private GameObject _successUI;
    [SerializeField] private GameObject _failureUI;

    public void GoToLobby()
    {
        Data = null;
        ChangeState(StateType.WaitUntilLevelSelected);
        SceneManager.LoadScene("SelectLevel");
    }

    public void SelectLevel(LevelData levelData)
    {
        ChangeState(StateType.WaitUntilLevelSelected);
        Data = levelData;
    }

    public void SucceedLevel()
    {
        if (Current == StateType.WaitUntilLevelFinished)
            Current = StateType.SucceedLevel;
    }

    public void FailLevel()
    {
        if (Current == StateType.WaitUntilLevelFinished)
            Current = StateType.FailLevel;
    }


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        MoveNext();
    }

    private void Update()
    {
        switch (Current)
        {
            case StateType.Idle:
                break;
            case StateType.WaitUntilLevelSelected:
                {
                    if (Data != null)
                    {
                        SceneManager.LoadScene($"Level{Data.Level}");
                        MoveNext();
                    }
                }
                break;
            case StateType.StartLevel:
                {                    
                    MoveNext();
                }
                break;
            case StateType.WaitUntilLevelFinished:
                {

                }
                break;
            case StateType.SucceedLevel:
                {
                    Instantiate(_successUI);
                    Current = StateType.WaitForUser;
                }
                break;
            case StateType.FailLevel:
                {
                    Instantiate(_failureUI);
                    Current = StateType.WaitForUser;
                }
                break;
            case StateType.WaitForUser:
                break;
            default:
                break;
        }
    }

    private void MoveNext() => Current++;
    private void ChangeState(StateType newState) => Current = newState;
}
