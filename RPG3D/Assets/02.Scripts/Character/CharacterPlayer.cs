using UnityEngine;
using ULB.RPG.FSM;
using ULB.RPG.DataModels;
using static UnityEditor.Progress;

namespace ULB.RPG
{
    public class CharacterPlayer : CharacterBase
    {
        [SerializeField] private AnimatorWrapper _animatorWrapper;
        [SerializeField] private Transform _rightHand;
        [SerializeField] private Transform _leftHand;
        [SerializeField] private BareHand _bareHand;


        public bool TryEquip(Equipment equipment)
        {
            switch (equipment.type)
            {
                case EquipType.RightHandWeapon:
                    {
                        // todo ->
                        // 이 장비를 장착하기위해 해제해야하는 장비 계산 
                        // 인벤토리에 해제한 장비가 들어갈 (남은 슬롯 - 1)이 있는지
                        // 장비 해제
                        // 새 장비 장착
                        // 해제한 장비 인벤토리에 집어넣음
                        if (_rightHand.childCount > 0)
                        {
                            Destroy(_rightHand.GetChild(0).gameObject);
                        }
                        Instantiate(equipment, _rightHand);
                        SetAnimatorParameterForWeapon((Weapon)equipment);
                    }
                    break;
                case EquipType.LeftHandWeapon:
                    {
                        if (_leftHand.childCount > 0)
                        {
                            Destroy(_leftHand.GetChild(0).gameObject);
                        }
                        Instantiate(equipment, _leftHand);
                    }
                    break;
                case EquipType.DoubleHandWeapon:
                    {
                        if (_rightHand.childCount > 0)
                        {
                            Destroy(_rightHand.GetChild(0).gameObject);
                        }
                        if (_leftHand.childCount > 0)
                        {
                            Destroy(_leftHand.GetChild(0).gameObject);
                        }

                        Instantiate(equipment, _rightHand);
                        SetAnimatorParameterForWeapon((Weapon)equipment);
                    }
                    break;
                case EquipType.Top:
                    break;
                case EquipType.Bottom:
                    break;
                case EquipType.Head:
                    break;
                case EquipType.Ring:
                    break;
                case EquipType.Necklace:
                    break;
                default:
                    break;
            }

            equipment.Equip(this);
            return true;
        }

        public bool TryUnequip(Equipment equipment)
        {
            switch (equipment.type)
            {
                case EquipType.RightHandWeapon:
                    {
                        Destroy(equipment);
                        Instantiate(_bareHand, _leftHand).type = EquipType.LeftHandWeapon;
                        Instantiate(_bareHand, _rightHand).type = EquipType.RightHandWeapon;
                        SetAnimatorParameterForWeapon(_bareHand);
                    }
                    break;
                case EquipType.LeftHandWeapon:
                    break;
                case EquipType.DoubleHandWeapon:
                    {
                        Destroy(equipment);
                        Instantiate(_bareHand, _leftHand).type = EquipType.LeftHandWeapon;
                        Instantiate(_bareHand, _rightHand).type = EquipType.RightHandWeapon;
                        SetAnimatorParameterForWeapon(_bareHand);
                    }
                    break;
                case EquipType.Top:
                    break;
                case EquipType.Bottom:
                    break;
                case EquipType.Head:
                    break;
                case EquipType.Ring:
                    break;
                case EquipType.Necklace:
                    break;
                default:
                    break;
            }
            return true;
        }

        public bool TryUnequip(EquipType equipmentType)
        {
            switch (equipmentType)
            {
                case EquipType.RightHandWeapon:
                    {
                        if (_rightHand.childCount > 0)
                        {
                            _rightHand.GetChild(0).GetComponent<Equipment>().Unequip(this);
                            Destroy(_rightHand.GetChild(0).gameObject);
                        }
                        
                        Instantiate(_bareHand, _leftHand).type = EquipType.LeftHandWeapon;
                        Instantiate(_bareHand, _rightHand).type = EquipType.RightHandWeapon;
                        SetAnimatorParameterForWeapon(_bareHand);
                    }
                    break;
                case EquipType.LeftHandWeapon:
                    break;
                case EquipType.DoubleHandWeapon:
                    {
                        if (_rightHand.childCount > 0)
                        {
                            _rightHand.GetChild(0).GetComponent<Equipment>().Unequip(this);
                            Destroy(_rightHand.GetChild(0).gameObject);
                        }
                        if (_leftHand.childCount > 0)
                        {
                            _leftHand.GetChild(0).GetComponent<Equipment>().Unequip(this);
                            Destroy(_leftHand.GetChild(0).gameObject);
                        }

                        Instantiate(_bareHand, _leftHand).type = EquipType.LeftHandWeapon;
                        Instantiate(_bareHand, _rightHand).type = EquipType.RightHandWeapon;
                        SetAnimatorParameterForWeapon(_bareHand);
                    }
                    break;
                case EquipType.Top:
                    break;
                case EquipType.Bottom:
                    break;
                case EquipType.Head:
                    break;
                case EquipType.Ring:
                    break;
                case EquipType.Necklace:
                    break;
                default:
                    break;
            }

            
            return true;
        }
        public bool TryGetEquipment(EquipType equipType, out Equipment equipment)
        {
            Transform equipPoint = null;
            equipment = null;

            switch (equipType)
            {
                case EquipType.RightHandWeapon:
                    equipPoint = _rightHand;
                    break;
                case EquipType.LeftHandWeapon:
                    equipPoint = _leftHand;
                    break;
                case EquipType.DoubleHandWeapon:
                    equipPoint = _rightHand;
                    break;
                case EquipType.Top:
                    break;
                case EquipType.Bottom:
                    break;
                case EquipType.Head:
                    break;
                case EquipType.Ring:
                    break;
                case EquipType.Necklace:
                    break;
                default:
                    break;
            }

            if (equipPoint.childCount > 0)
            {
                return equipPoint.GetChild(0).TryGetComponent(out equipPoint);
            }
            return false;
        }

        protected override CharacterStateMachine CreateMachine()
        {
            return new PlayerStateMachine(gameObject);
        }

        protected override void Awake()
        {
            base.Awake();
            onHpDecreased += (value) => machine.ChangeState(CharacterStateMachine.StateType.Hurt);
            onHpMin += () => machine.ChangeState(CharacterStateMachine.StateType.Die);

            ItemsEquippedDataModel.instance.OnItemChanged += (equipType, itemID) =>
            {
                if (itemID >= 0)
                    TryEquip((Equipment)ItemInfoAssets.instance[itemID].prefab);
                else
                    TryUnequip((EquipType)equipType);
            };

            for (int equipType = 0; equipType < ItemsEquippedDataModel.instance.Items.Count; equipType++)
            {
                if (ItemsEquippedDataModel.instance.Items[equipType] >= 0)
                    TryEquip((Equipment)ItemInfoAssets.instance[ItemsEquippedDataModel.instance.Items[equipType]].prefab);
                else
                    TryUnequip((EquipType)equipType);
            }
        }

        public override void Hit()
        {
            base.Hit();
        }

        private void SetAnimatorParameterForWeapon(Weapon weapon)
        {
            _animatorWrapper.SetInt("weaponType", (int)weapon.weaponType);
        }

        #region AnimationEvents
        public void StartCastRightHandWeapon()
        {
            if (_rightHand.GetChild(0).TryGetComponent(out Weapon weapon))
            {
                weapon.doCast = true;
            }
        }

        public void HitRightHandWeapon(int targetNum)
        {
            if (_rightHand.GetChild(0).TryGetComponent(out Weapon weapon))
            {
                foreach (IDamageable target in weapon.targetsCasted.Values)
                {
                    target.Damage(stats[StatType.STR].valueModified);
                    targetNum--;
                    if (targetNum <= 0)
                        return;
                }
            }
        }
        public void StartCastLeftHandWeapon()
        {
            if (_leftHand.GetChild(0).TryGetComponent(out Weapon weapon))
            {
                weapon.doCast = true;
            }
        }

        public void HitLeftHandWeapon(int targetNum)
        {
            if (_leftHand.GetChild(0).TryGetComponent(out Weapon weapon))
            {
                foreach (IDamageable target in weapon.targetsCasted.Values)
                {
                    target.Damage(stats[StatType.STR].valueModified);
                    targetNum--;
                    if (targetNum <= 0)
                        return;
                }
            }
        }
        #endregion
    }
}