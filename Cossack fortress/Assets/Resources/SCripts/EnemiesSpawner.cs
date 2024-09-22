using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private Enemy _defaultEnemy;
    [SerializeField] private Transform _enemyParent;
    [SerializeField] private Transform[] _spawnPoints;

    private void Start()
    {
        WaveManager.Instance.NewWaveIsStart += Spawn;
    }

    private void Spawn(WaveBaff baff)
    {
        for (int i = 0; i < baff.NumberOfEnemies; i++)
        {
            var enemy = Instantiate(_defaultEnemy.gameObject, _enemyParent);
            int spawnIndex = Random.Range(0, _spawnPoints.Length - 1);
            enemy.transform.position = _spawnPoints[spawnIndex].position;
        }
    }
}
