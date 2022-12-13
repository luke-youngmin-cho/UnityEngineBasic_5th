using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paths : MonoBehaviour
{
    public static Paths Instance;
    public List<Transform> StartPoints = new List<Transform>();
    public List<Transform> EndPoints = new List<Transform>();

    [Serializable]
    public class Path
    {
        [SerializeField] private List<Transform> _path;
        public List<Transform> GetPath() => _path;
    }
    [SerializeField] private List<Path> _paths;
    public List<Path> GetPaths() => _paths;

    private void Awake()
    {
        Instance = this;
    }
}
