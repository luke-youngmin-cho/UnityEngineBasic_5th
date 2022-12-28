using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLaserBeamer : TowerAttacker
{
    [SerializeField] private LineRenderer _beam;
    [SerializeField] private ParticleSystem _beamHitEffect;
    [SerializeField] private Transform _firePoint;
 
    private int _damageStep;
    public int DamageStep
    {
        get
        {
            return _damageStep;
        }
        set
        {
            _damageStep = value;
            _beam.startWidth = 0.03f * (1 + value * 0.3f);
            _beam.endWidth = 0.03f * (1 + value * 0.3f);
            _beamHitEffect.transform.localScale = Vector3.one * (1 + value * 0.2f);
        }
    }
    [SerializeField] private float _damageChargeTime = 0.5f;
    [SerializeField] private float _damageGain = 1.5f;
    [SerializeField] private float _damagePeriod = 0.1f;
    private float _chargeTimer;
    private float _damageTimer;
    private Transform _targetMem;
    protected Transform TargetMem
    {
        get
        {
            return _targetMem;
        }
        set
        {
            if (_targetMem == value)
                return;

            _targetMem = value;

            if (_targetMem == null)
            {
                _beam.enabled = false;
                _beamHitEffect.Stop();
                DamageStep = -1;
                _chargeTimer = 0;
            }
            else
            {
                _beam.enabled = true;
                _beamHitEffect.Play();
            }
        }
    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Attack();
    }

    protected override void Attack()
    {
        TargetMem = Target;

        if (_targetMem == null)
            return;

        _beam.SetPosition(0, _firePoint.position);
        _beam.SetPosition(1, _targetMem.position);

        if (Physics.Raycast(_firePoint.position,
                            (_targetMem.position - _firePoint.position).normalized,
                            out RaycastHit hit,
                            Vector3.Distance(_targetMem.position, _firePoint.position),
                            TargetLayer))
        {
            _beamHitEffect.transform.position = hit.point;
            _beamHitEffect.transform.LookAt(_firePoint);
        }


        if (_chargeTimer <= 0)
        {
            if (_damageStep < 2)
            {
                DamageStep++;
                _chargeTimer = _damageChargeTime;
            }
        }
        else
        {
            _chargeTimer -= Time.fixedDeltaTime;
        }

        if (_damageTimer <= 0)
        {
            if (Target.TryGetComponent(out Enemy enemy))
            {
                enemy.Hp -= Damage * (1 + DamageStep) * _damageGain;
                _damageTimer = _damagePeriod;
            }
        }
        else
        {
            _damageTimer -= Time.fixedDeltaTime;
        }
    }
}
