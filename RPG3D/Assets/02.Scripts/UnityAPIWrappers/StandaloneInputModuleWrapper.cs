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
