using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnMap : MonoBehaviour
{
    public static EnemyOnMap Instance { get; private set; }
    public List<Enemy> Enemies { get; private set; }
    public event Action EnemiesIsEnd;

    private void Awake()
    {
        Instance = this;
        Enemies = new List<Enemy>();
    }

    private void Update()
    {
        if (WaveManager.Instance.AllWaveIsEnd)
            if (Enemies.Count == 0)
                EnemiesIsEnd?.Invoke();
    }
}
