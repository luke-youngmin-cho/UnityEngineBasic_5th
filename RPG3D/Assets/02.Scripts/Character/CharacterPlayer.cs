using UnityEngine;
using ULB.RPG.FSM;

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
                case Equipment.EquipType.RightHandWeapon:
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
                case Equipment.EquipType.LeftHandWeapon:
                    {
                        if (_leftHand.childCount > 0)
                        {
                            Destroy(_leftHand.GetChild(0).gameObject);
                        }
                        Instantiate(equipment, _leftHand);
                    }
                    break;
                case Equipment.EquipType.DoubleHandWeapon:
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
                case Equipment.EquipType.Top:
                    break;
                case Equipment.EquipType.Bottom:
                    break;
                case Equipment.EquipType.Head:
                    break;
                case Equipment.EquipType.Ring:
                    break;
                case Equipment.EquipType.Necklace:
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
                case Equipment.EquipType.RightHandWeapon:
                    {
                        Destroy(equipment);
                        Instantiate(_bareHand, _leftHand).type = Equipment.EquipType.LeftHandWeapon;
                        Instantiate(_bareHand, _rightHand).type = Equipment.EquipType.RightHandWeapon;
                        SetAnimatorParameterForWeapon(_bareHand);
                    }
                    break;
                case Equipment.EquipType.LeftHandWeapon:
                    break;
                case Equipment.EquipType.DoubleHandWeapon:
                    {
                        // todo -> 
                        // 인벤토리공간확인 
                        // 장착해제
                        // 맨손장착
                        // 인벤토리에 해제한 장비 추가
                        Destroy(equipment);
                        Instantiate(_bareHand, _leftHand).type = Equipment.EquipType.LeftHandWeapon;
                        Instantiate(_bareHand, _rightHand).type = Equipment.EquipType.RightHandWeapon;
                        SetAnimatorParameterForWeapon(_bareHand);
                    }
                    break;
                case Equipment.EquipType.Top:
                    break;
                case Equipment.EquipType.Bottom:
                    break;
                case Equipment.EquipType.Head:
                    break;
                case Equipment.EquipType.Ring:
                    break;
                case Equipment.EquipType.Necklace:
                    break;
                default:
                    break;
            }
            return true;
        }

        public bool TryUnequip(Equipment.EquipType equipmentType)
        {
            switch (equipmentType)
            {
                case Equipment.EquipType.RightHandWeapon:
                    {
                        if (_rightHand.childCount <= 0)
                            return false;

                        _rightHand.GetChild(0).GetComponent<Equipment>().Unequip(this);
                        Destroy(_rightHand.GetChild(0).gameObject);
                        Instantiate(_bareHand, _leftHand).type = Equipment.EquipType.LeftHandWeapon;
                        Instantiate(_bareHand, _rightHand).type = Equipment.EquipType.RightHandWeapon;
                        SetAnimatorParameterForWeapon(_bareHand);
                    }
                    break;
                case Equipment.EquipType.LeftHandWeapon:
                    break;
                case Equipment.EquipType.DoubleHandWeapon:
                    {
                        if (_rightHand.childCount <= 0)
                            return false;

                        // todo -> 
                        // 인벤토리공간확인 
                        // 장착해제
                        // 맨손장착
                        // 인벤토리에 해제한 장비 추가
                        _rightHand.GetChild(0).GetComponent<Equipment>().Unequip(this);
                        Destroy(_rightHand.GetChild(0).gameObject);
                        Instantiate(_bareHand, _leftHand).type = Equipment.EquipType.LeftHandWeapon;
                        Instantiate(_bareHand, _rightHand).type = Equipment.EquipType.RightHandWeapon;
                        SetAnimatorParameterForWeapon(_bareHand);
                    }
                    break;
                case Equipment.EquipType.Top:
                    break;
                case Equipment.EquipType.Bottom:
                    break;
                case Equipment.EquipType.Head:
                    break;
                case Equipment.EquipType.Ring:
                    break;
                case Equipment.EquipType.Necklace:
                    break;
                default:
                    break;
            }

            
            return true;
        }
        public bool TryGetEquipment(Equipment.EquipType equipType, out Equipment equipment)
        {
            Transform equipPoint = null;
            equipment = null;

            switch (equipType)
            {
                case Equipment.EquipType.RightHandWeapon:
                    equipPoint = _rightHand;
                    break;
                case Equipment.EquipType.LeftHandWeapon:
                    equipPoint = _leftHand;
                    break;
                case Equipment.EquipType.DoubleHandWeapon:
                    equipPoint = _rightHand;
                    break;
                case Equipment.EquipType.Top:
                    break;
                case Equipment.EquipType.Bottom:
                    break;
                case Equipment.EquipType.Head:
                    break;
                case Equipment.EquipType.Ring:
                    break;
                case Equipment.EquipType.Necklace:
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