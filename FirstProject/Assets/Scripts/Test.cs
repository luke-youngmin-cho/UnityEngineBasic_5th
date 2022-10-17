using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int Num;

    private void Awake()
    {
        Debug.Log("[Test] : Awake");
    }

    private void OnEnable()
    {
        Debug.Log("[Test] : OnEnable");
    }

    private void Reset()
    {
        Debug.Log("[Test] : Reset");
    }

    private void Start()
    {
        Debug.Log("[Test] : Start");
    }

    private void Update()
    {
        Debug.Log("[Test] : Update");
    }

    private void LateUpdate()
    {
        Debug.Log("[Test] : LateUpdate");
    }

    private void FixedUpdate()
    {
        Debug.Log("[Test] : FixedUpdate");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, Vector3.one);
    }

    private void OnGUI()
    {
        Event e = Event.current;
        Debug.Log(e.mousePosition);
    }

    private void OnApplicationPause(bool pause)
    {
        Debug.Log($"[Test] : paused = {pause}");
    }

    private void OnApplicationQuit()
    {
        Debug.Log("[Test] : application quit ");
    }

    private void OnDisable()
    {
        Debug.Log("[Test] : Disabled");
    }

    private void OnDestroy()
    {
        Debug.Log("[Test] : Destroyed");
    }
}
