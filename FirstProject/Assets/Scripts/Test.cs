using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int Num;

    /// <summary>
    /// Script Instance 가 처음 Load 될 때 한번 호출
    /// (Script Instance : GameObject 에 붙인 Script)
    /// Load 되는 시기는 해당 Script Instance 를 가지고있는 GameObject 가 처음 생성될때
    /// Script Instance 의 활성/비활성 유무와 관계없이 호출
    /// </summary>
    private void Awake()
    {
        Debug.Log("[Test] : Awake");
    }

    /// <summary>
    /// Script Instance 가 활성화 될 때 마다 호출
    /// 해당 Script Instance 를 가지고있는  GameObject 가 활성/ 비활성화 될때도 영향을 받음
    /// </summary>
    private void OnEnable()
    {
        Debug.Log("[Test] : OnEnable");
    }

    /// <summary>
    /// Script를 GameObject 에 붙일때 처음 호출됨.
    /// Script Instance 의 멤버 변수들을 초기화 해줌.
    /// Inspector 창에서 수동으로 호출할 수 있음.
    /// </summary>
    private void Reset()
    {
        Debug.Log("[Test] : Reset");
    }

    /// <summary>
    /// Update 함수가 호출되기 전에 한번 실행되는 함수
    /// </summary>
    private void Start()
    {
        Debug.Log("[Test] : Start");
    }

    /// <summary>
    /// 게임 로직 매 프레임 마다 처음 호출되는 함수
    /// </summary>
    private void Update()
    {
        Debug.Log("[Test] : Update");
    }

    /// <summary>
    /// 게임 로직 매 프레임 마다 마지막에 호출되는 함수
    /// </summary>
    private void LateUpdate()
    {
        Debug.Log("[Test] : LateUpdate");
    }

    /// <summary>
    /// 물리 연산을 위한 프레임 마다 호출되는 함수
    /// </summary>
    private void FixedUpdate()
    {
        Debug.Log("[Test] : FixedUpdate");
    }

    /// <summary>
    /// Editor에서 Gizmo(에디터상의 그래픽적인 부가 요소들) 들을 그릴 때 호출되는 함수
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, Vector3.one);
    }

    /// <summary>
    /// UI 이벤트를 처리하기위한 함수
    /// </summary>
    private void OnGUI()
    {
        Event e = Event.current;
        Debug.Log(e.mousePosition);
    }

    /// <summary>
    /// 어플리케이션이 일시정지될때 호출
    /// </summary>
    private void OnApplicationPause(bool pause)
    {
        Debug.Log($"[Test] : paused = {pause}");
    }

    /// <summary>
    /// 어플리케이션이 종료될때 호출
    /// </summary>
    private void OnApplicationQuit()
    {
        Debug.Log("[Test] : application quit ");
    }

    /// <summary>
    /// Script Instance 가 비활성화될 때 마다 호출
    /// </summary>
    private void OnDisable()
    {
        Debug.Log("[Test] : Disabled");
    }

    /// <summary>
    /// 파괴될 때 호출
    /// </summary>
    private void OnDestroy()
    {
        Debug.Log("[Test] : Destroyed");
    }
}
