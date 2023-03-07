using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StandaloneInputModuleWrapper : StandaloneInputModule
{
    public static StandaloneInputModuleWrapper main;

    public bool TryGetGameObjectPointed<T>(int pointerID, out GameObject pointed, LayerMask ignoreMask)
        where T : BaseRaycaster
    {
        pointed = null;
        if (IsPointerOverGameObject(pointerID))
        {
            if (m_PointerData.TryGetValue(pointerID, out PointerEventData pointerEventData) &&
                pointerEventData.pointerCurrentRaycast.module.GetType() == typeof(T))
            {
                pointed = pointerEventData.pointerCurrentRaycast.gameObject;
                
                if ((1 << pointed.layer & ignoreMask) == 0)
                {
                    return true;
                }
                else
                {
                    List<RaycastResult> results = new List<RaycastResult>();
                    pointerEventData.pointerCurrentRaycast.module.Raycast(pointerEventData, results);

                    foreach (var result in results)
                    {
                        if ((1 << result.sortingLayer & ignoreMask) == 0)
                        {
                            pointed = result.gameObject;
                            return true;
                        }
                    }

                }

            }
        }
        return false;
    }

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
