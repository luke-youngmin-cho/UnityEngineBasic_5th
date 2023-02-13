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
                        if (_rightHand.childCount > 1)
                        {
                            Destroy(_rightHand.GetChild(1).gameObject);
                            Instantiate(equipment, _rightHand).transform.SetAsLastSibling();
                            _animatorWrapper.SetBool("iSArmedWithRightHand", true);
                        }
                    }
                    break;
                case Equipment.EquipType.LeftHandWeapon:
                    {
                        if (_leftHand.childCount > 1)
                        {
                            Destroy(_leftHand.GetChild(1).gameObject);
                            Instantiate(equipment, _leftHand).transform.SetAsLastSibling();
                            _animatorWrapper.SetBool("iSArmedWithLeftHand", true);
                        }
                    }
                    break;
                case Equipment.EquipType.DoubleHandWeapon:
                    {
                        if (_rightHand.childCount > 1)
                        {
                            Destroy(_rightHand.GetChild(1).gameObject);
                        }
                        if (_leftHand.childCount > 1)
                        {
                            Destroy(_leftHand.GetChild(1).gameObject);
                        }

                        Instantiate(equipment, _rightHand).transform.SetAsLastSibling();
                        _animatorWrapper.SetBool("isArmeWithTwoHand", true);
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
    }
}