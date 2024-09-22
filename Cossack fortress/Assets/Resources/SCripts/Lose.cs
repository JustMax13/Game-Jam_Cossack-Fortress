using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    [SerializeField] private Canvas _loseCanvas;

    private void Start()
    {
        BuildingsOnMap.Instance.BuildingsIsEnd += IsLose;
    }

    private void IsLose()
    {
        _loseCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    private void OnDestroy()
        => Time.timeScale = 1f;
}
