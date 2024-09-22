using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultFieldValue : MonoBehaviour
{
    [SerializeField] private UI_Value _UI_Value;
    public static DefaultFieldValue Instance { get; private set; }
    public UI_Value UI_Value { get => _UI_Value; private set => _UI_Value = value; }

    private void Awake()
    {
        Instance = this;
    }
}
