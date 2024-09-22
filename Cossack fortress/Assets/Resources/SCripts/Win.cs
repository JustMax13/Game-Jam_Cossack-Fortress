using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] private Canvas _winCanvas;

    private void Start()
    {
        EnemyOnMap.Instance.EnemiesIsEnd += IsWin;
    }

    private void IsWin()
    {
        _winCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
