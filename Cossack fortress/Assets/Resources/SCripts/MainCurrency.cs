using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainCurrency : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _output;
    [SerializeField] private float _startAmount;
    public static MainCurrency Instance { get; private set; }
    private float _value;
    public float Value 
    {
        get => _value;
        set
        {
            if(value < 0)
                Debug.LogException(new System.Exception("Купівля неможлива, недостатньо коштів!"));

            _value = value;
        }
    }

    private void Start()
    {
        Instance = this;
        _value = _startAmount;
    }

    private void Update()
    {
        _output.text = ((int)Value).ToString();
    }
}
