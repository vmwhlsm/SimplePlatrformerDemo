using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
    
public class GemSpawner : MonoBehaviour
{
    [SerializeField] private GemManager _gem;

    private Transform[] _spawnPoints;
    private float _interval = 2;
    private System.Random _random = new System.Random();

    private void Start()
    {
        _spawnPoints = GetComponentsInChildren<Transform>();
        StartCoroutine(SpawnGem());
    }

    private IEnumerator SpawnGem()
    {
        while (true)
        {
            int randomIndex = _random.Next(0, _spawnPoints.Length - 1);
            Instantiate(_gem.gameObject, _spawnPoints[randomIndex]);

            yield return new WaitForSeconds(_interval);
        }
    }
}
