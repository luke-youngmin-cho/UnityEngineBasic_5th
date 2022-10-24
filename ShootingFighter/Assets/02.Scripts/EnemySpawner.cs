using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnTime;
    private float _timer;
    private BoxCollider _bound;

    private float _minX;
    private float _maxX;
    private float _minZ;
    private float _maxZ;

    private void Awake()
    {
        _bound = gameObject.GetComponent<BoxCollider>();

        _minX = _bound.transform.position.x - _bound.size.x / 2.0f;
        _maxX = _bound.transform.position.x + _bound.size.x / 2.0f;
        _minZ = _bound.transform.position.z - _bound.size.z / 2.0f;
        _maxZ = _bound.transform.position.z + _bound.size.z / 2.0f;
    }

    private void Update()
    {
        if (_timer < 0)
        {
            Vector3 spawnPos = new Vector3(Random.Range(_minX, _maxX),
                                           0.0f,
                                           Random.Range(_minZ, _maxZ));
            
            GameObject go = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            go.transform.Rotate(Vector3.up * 180.0f);
            _timer = _spawnTime;
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }
}
