using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public LevelData Data;

    public void FailLevel()
    {
        
    }


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Pathfinder.SetUp();
    }


}
