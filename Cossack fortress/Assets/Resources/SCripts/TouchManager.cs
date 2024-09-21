using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    public static TouchManager Instance { get; private set; }
    private void Start()
    {
        Instance = this;
    }

    public bool IsPointerOnUI()
        => EventSystem.current.IsPointerOverGameObject();
}
