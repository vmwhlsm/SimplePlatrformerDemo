using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
    
public class GemSpawner : MonoBehaviour
{
    [SerializeField] private Gem _gem;

    private Transform[] _spawnPoints;
    private float _interval = 2;
    private float _timer = 0;
    private System.Random _random = new System.Random();

    private void Start()
    {
        _spawnPoints = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        if (_timer >= _interval)
        {
            _timer = 0;
            SpawnGem();
        }

        _timer += Time.deltaTime;
    }

    private void SpawnGem()
    {
        int randomIndex = _random.Next(0, _spawnPoints.Length - 1);
        Instantiate(_gem.gameObject, _spawnPoints[randomIndex]);
    }
}
