using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackerWithProjectile : TowerAttacker
{    
    [SerializeField] protected Transform[] FirePoints;
    [SerializeField] private float _reloadTime;
    private float _reloadTimer;

    [SerializeField] private float _firePointBlinkTime;
    private float _blinktimer;
    private bool _isBlinking;

    private void Update()
    {
        Reload();
    }

    private void Reload()
    {
        if (_reloadTimer < 0)
        {
            if (Target != null)
            {
                Attack();
                _reloadTimer = _reloadTime;
            }
        }
        else
        {
            _reloadTimer -= Time.deltaTime;
        }
    }

    protected override void Attack()
    {
        BlinkFirePoints();
    }

    private void BlinkFirePoints()
    {
        if (_isBlinking)
            _blinktimer = _firePointBlinkTime;
        else
            StartCoroutine(E_BlinkFirePoints());
    }

    private IEnumerator E_BlinkFirePoints()
    {
        _isBlinking = true;
        _blinktimer = _firePointBlinkTime;
        ActiveFirePoints();
        while (_blinktimer > 0)
        {
            _blinktimer -= Time.deltaTime;
            yield return null;
        }
        DeactiveFirePoints();
        _isBlinking = false;
    }

    private void ActiveFirePoints()
    {
        foreach (Transform firePoint in FirePoints)
            firePoint.gameObject.SetActive(true);
    }
    
    private void DeactiveFirePoints()
    {
        foreach (Transform firePoint in FirePoints)
            firePoint.gameObject.SetActive(false);
    }
}
