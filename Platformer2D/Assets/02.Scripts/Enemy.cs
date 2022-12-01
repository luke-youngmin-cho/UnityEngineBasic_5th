using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    private int _hp;
    public int HP
    {
        get
        {
            return _hp;
        }
        set
        {
            if (value <= 0)
            {
                value = 0;
                OnHpMin?.Invoke();
            }
            else if (value < _hp)
            {
                OnHpDecrease?.Invoke();
            }

            _hp = value;
            _hpSlider.value = (float)value / _hpMax;
        }
    }
    [SerializeField] private int _hpMax;
    public event Action OnHpMin;
    public event Action OnHpDecrease;


    [SerializeField] private Slider _hpSlider;

    public int ATK;

    [SerializeField] private LayerMask _targetLayer;

    public void Hurt(GameObject hitter, int damage, bool isCritical)
    {
        HP -= damage;
        DamagePopUp.Create(1 << hitter.layer, transform.position + Vector3.up * 0.25f, damage);
    }


    private void Awake()
    {
        HP = _hpMax;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == _targetLayer)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player.Invincible == false)
            {
                player.Hurt(gameObject, ATK, false);
                player.Knockback();
            }
        }
    }
}
