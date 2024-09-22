using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _outputTime;
    [SerializeField] private int _wavePauseTime;
    [SerializeField] private float _awaitAtStart;
    [SerializeField] private float _awaitBeforeWave;
    [SerializeField] private int _lastWaveNumber;

    private float _currentAwaitBeforeWave;
    private float _remainedWaveTime;
    private int _waveNumber;
    private bool _allWaveIsEnd;

    public Dictionary<int, WaveBaff> WaveBaffDictionary { get; private set; }
    public bool AllWaveIsEnd { get => _allWaveIsEnd; }

    public event Action<WaveBaff> NewWaveIsStart;

    public static WaveManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _allWaveIsEnd = false;
        _waveNumber = 0;
        _remainedWaveTime = _wavePauseTime;
        _outputTime.text = TimeToString();

        WaveBaffDictionary = new Dictionary<int, WaveBaff>
        {
            {
                1, new WaveBaff(1, 1, 1, 2)
            },
            {
                2, new WaveBaff(1, 1, 1, 3)
            },
            {
                3, new WaveBaff(1.3f, 1.3f, 1.3f, 3)
            },
            {
                4, new WaveBaff(1.8f, 1.8f, 1.8f, 3)
            },
            {
                5, new WaveBaff(1.3f, 1.3f, 1.3f, 5)
            },
            {
                6, new WaveBaff(1.8f, 1.8f, 1.8f, 5)
            },
            {
                7, new WaveBaff(1.1f, 1.1f, 1.1f, 7)
            },
            {
                8, new WaveBaff(1.6f, 1.6f, 1.6f, 7)
            },
            {
                9, new WaveBaff(1.2f, 1.2f, 1.2f, 9)
            },
            {
                10, new WaveBaff(2f, 2f, 2f, 13)
            },

        };
    }

    private void Update()
    {
        if (NeedToWait())
            return;

        if (_allWaveIsEnd)
            return;

        UpdateTime();
        SetOutputTime();
        CheckWaveTime();
    }

    private bool NeedToWait()
    {
        if (_awaitAtStart > 0)
        {
            _awaitAtStart -= Time.deltaTime;
            return true;
        }
        else if(_currentAwaitBeforeWave > 0)
        {
            _currentAwaitBeforeWave -= Time.deltaTime;
            return true;
        }
        else
            return false;
        
    }
    private string TimeToString()
    {
        int aroundTime = Mathf.RoundToInt(_remainedWaveTime);

        if(aroundTime < 10)
            return "0" + aroundTime;
        else 
            return aroundTime.ToString();
    }
    private void UpdateTime()
        => _remainedWaveTime -= Time.deltaTime;
    private void SetOutputTime()
        => _outputTime.text = TimeToString();
    private void CheckWaveTime()
    {
        if(_remainedWaveTime <= 0)
        {
            _waveNumber++;

            if (CheckLastWave())
                _allWaveIsEnd = true;
            else
            {
                _remainedWaveTime = _wavePauseTime;
                _currentAwaitBeforeWave = _awaitBeforeWave;
                SetOutputTime();
            }

            NewWaveIsStart?.Invoke(WaveBaffDictionary.GetValueOrDefault(_waveNumber));
        }
    }
    private bool CheckLastWave()
    {
        if (_waveNumber >= _lastWaveNumber)
            return true;
        else
            return false;
    }
}
