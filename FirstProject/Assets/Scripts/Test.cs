using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int Num;

    /// <summary>
    /// Script Instance �� ó�� Load �� �� �ѹ� ȣ��
    /// (Script Instance : GameObject �� ���� Script)
    /// Load �Ǵ� �ñ�� �ش� Script Instance �� �������ִ� GameObject �� ó�� �����ɶ�
    /// Script Instance �� Ȱ��/��Ȱ�� ������ ������� ȣ��
    /// </summary>
    private void Awake()
    {
        Debug.Log("[Test] : Awake");
    }

    /// <summary>
    /// Script Instance �� Ȱ��ȭ �� �� ���� ȣ��
    /// �ش� Script Instance �� �������ִ�  GameObject �� Ȱ��/ ��Ȱ��ȭ �ɶ��� ������ ����
    /// </summary>
    private void OnEnable()
    {
        Debug.Log("[Test] : OnEnable");
    }

    /// <summary>
    /// Script�� GameObject �� ���϶� ó�� ȣ���.
    /// Script Instance �� ��� �������� �ʱ�ȭ ����.
    /// Inspector â���� �������� ȣ���� �� ����.
    /// </summary>
    private void Reset()
    {
        Debug.Log("[Test] : Reset");
    }

    /// <summary>
    /// Update �Լ��� ȣ��Ǳ� ���� �ѹ� ����Ǵ� �Լ�
    /// </summary>
    private void Start()
    {
        Debug.Log("[Test] : Start");
    }

    /// <summary>
    /// ���� ���� �� ������ ���� ó�� ȣ��Ǵ� �Լ�
    /// </summary>
    private void Update()
    {
        Debug.Log("[Test] : Update");
    }

    /// <summary>
    /// ���� ���� �� ������ ���� �������� ȣ��Ǵ� �Լ�
    /// </summary>
    private void LateUpdate()
    {
        Debug.Log("[Test] : LateUpdate");
    }

    /// <summary>
    /// ���� ������ ���� ������ ���� ȣ��Ǵ� �Լ�
    /// </summary>
    private void FixedUpdate()
    {
        Debug.Log("[Test] : FixedUpdate");
    }

    /// <summary>
    /// Editor���� Gizmo(�����ͻ��� �׷������� �ΰ� ��ҵ�) ���� �׸� �� ȣ��Ǵ� �Լ�
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, Vector3.one);
    }

    /// <summary>
    /// UI �̺�Ʈ�� ó���ϱ����� �Լ�
    /// </summary>
    private void OnGUI()
    {
        Event e = Event.current;
        Debug.Log(e.mousePosition);
    }

    /// <summary>
    /// ���ø����̼��� �Ͻ������ɶ� ȣ��
    /// </summary>
    private void OnApplicationPause(bool pause)
    {
        Debug.Log($"[Test] : paused = {pause}");
    }

    /// <summary>
    /// ���ø����̼��� ����ɶ� ȣ��
    /// </summary>
    private void OnApplicationQuit()
    {
        Debug.Log("[Test] : application quit ");
    }

    /// <summary>
    /// Script Instance �� ��Ȱ��ȭ�� �� ���� ȣ��
    /// </summary>
    private void OnDisable()
    {
        Debug.Log("[Test] : Disabled");
    }

    /// <summary>
    /// �ı��� �� ȣ��
    /// </summary>
    private void OnDestroy()
    {
        Debug.Log("[Test] : Destroyed");
    }
}
