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
        // 마우스 위치에 GameObject 있는지?
        if (IsPointerOverGameObject(pointerID))
        {
            // 마우스 데이터에서 특정 ID 에 대한 데이터 가져오기
            if (m_PointerData.TryGetValue(pointerID, out PointerEventData pointerEventData))
            {
                // 캐스팅 된 GameObject 가 내가 찾는 모듈타입으로 캐스팅된 GameObject 라면 true 반환
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
