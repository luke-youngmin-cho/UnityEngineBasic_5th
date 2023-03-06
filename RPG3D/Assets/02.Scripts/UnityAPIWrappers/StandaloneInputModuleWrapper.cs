using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StandaloneInputModuleWrapper : StandaloneInputModule
{
    public static StandaloneInputModuleWrapper main;

    public bool IsPointerOverGameObject<T>(int pointerID)
        where T : BaseRaycaster
    {
        // ���콺 ��ġ�� GameObject �ִ���?
        if (IsPointerOverGameObject(pointerID))
        {
            // ���콺 �����Ϳ��� Ư�� ID �� ���� ������ ��������
            if (m_PointerData.TryGetValue(pointerID, out PointerEventData pointerEventData))
            {
                // ĳ���� �� GameObject �� ���� ã�� ���Ÿ������ ĳ���õ� GameObject ��� true ��ȯ
                return pointerEventData.pointerCurrentRaycast.module.GetType() == typeof(T);
            }
        }
        return false;
    }

    protected override void Awake()
    {
        base.Awake();
        main = this;
    }
}
