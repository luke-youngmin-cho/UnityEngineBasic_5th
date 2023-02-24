using System.Collections;
using System.Collections.Generic;
using ULB.RPG.DataModels;
using UnityEngine;

namespace ULB.RPG.Controllers
{
    public class ItemController : MonoBehaviour
    {
        public ItemInfo itemInfo;
        public int num;
        private InventoryDataModel _inventoryDataModel;
        private bool _hasPicked = false;
        [SerializeField] private Transform _rendererPoint;
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private float _oscillateSpeed = 2.0f;
        [SerializeField] private float _oscillateAmplitude = 0.1f;

        public void Set(ItemInfo itemInfo, int num)
        {
            this.itemInfo = itemInfo;
            this.num = num;
            _meshFilter.mesh = itemInfo.prefab.GetComponent<MeshFilter>().sharedMesh;
            _meshRenderer.materials = itemInfo.prefab.GetComponent<MeshRenderer>().sharedMaterials;
        }

        private void Awake()
        {
            if (itemInfo != null)
            {
                _meshFilter.mesh = itemInfo.prefab.GetComponent<MeshFilter>().sharedMesh;
                _meshRenderer.materials = itemInfo.prefab.GetComponent<MeshRenderer>().sharedMaterials;
            }

                //_inventoryDataModel = InventoryDataModel.instance;
            StartCoroutine(E_Init());
        }

        private void Update()
        {
            _rendererPoint.localPosition = Vector3.up * Mathf.Sin(Time.time * _oscillateSpeed) * _oscillateAmplitude;
        }

        private IEnumerator E_Init()
        {
            yield return new WaitUntil(() => InventoryDataModel.instance != null);
            _inventoryDataModel = InventoryDataModel.instance;
        }

        public void Pick(Transform subject)
        {
            if (_hasPicked)
                return;

            int slotID = _inventoryDataModel.FindIndex(x => x.id == itemInfo.id && 
                                                            x.num + num <= itemInfo.maxNum); // 기존에 동일한 아이템을 가지고 있다면 그 슬롯 반환
            slotID = slotID >= 0 ? slotID : _inventoryDataModel.FindIndex(x => x == ItemData.empty); // 빈 슬롯 찾기
            if (slotID >= 0)
            {
                _hasPicked = true;
                _inventoryDataModel.Change(slotID, new ItemData(itemInfo.id, num + _inventoryDataModel.Items[slotID].num));
                StartCoroutine(E_Follow(subject));
            }
        }

        private IEnumerator E_Follow(Transform target)
        {
            float timeMark = Time.time;

            while (Vector3.Distance(transform.position, target.position) > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, target.position, (Time.time - timeMark) * _oscillateSpeed);
                yield return null;
            }

            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
}