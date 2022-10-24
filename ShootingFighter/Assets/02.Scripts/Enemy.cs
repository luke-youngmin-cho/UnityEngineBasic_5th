using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    private float _hp;
    // ������Ƽ (Property)
    // C# ���� �ʵ��� ĸ��ȭ�� ���� ���� 
    // Getter, Setter �����ڸ� �����ؼ� ���� �аų� �� �� �ѹ� ������ �� �ִ�.
    // ĸ��ȭ : ĸ���˾�ó�� �߸��� �� ������ ���ų� ���ٽ� Ư���� ������ �����ϵ��� ����� �۾�
    public float Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            if (value < 0)
                value = 0;

            _hp = value;
            _hpBar.value = _hp / _hpMax;

            if (_hp <= 0)
                Destroy(this.gameObject);
        }
    }
    [SerializeField] private float _hpMax = 100.0f;
    [SerializeField] private Slider _hpBar;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _damage = 20.0f;
    [SerializeField] private LayerMask _targetLayer;
    

    //==========================================================
    //******************** Public Methods **********************
    //==========================================================

    public void Hurt(float damage)
    {
        Hp -= damage;
    }

    //==========================================================
    //******************** Private Methods *********************
    //==========================================================

    private void Awake()
    {
        Hp = _hpMax;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & _targetLayer) > 0)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                player.Hurt(_damage);
                Destroy(gameObject);
            }
        }
    }

    ////==========================================================
    ////******************** Get Methods **********************
    ////==========================================================
    //public float GetHp()
    //{
    //    return _hp;
    //}
    //
    ////==========================================================
    ////******************** Set Methods **********************
    ////==========================================================
    //
    //public void SetHp(float value)
    //{
    //    if (value < 0)
    //        value = 0;
    //    Hp = value;
    //}
}
