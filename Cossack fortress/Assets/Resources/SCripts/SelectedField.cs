using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedField : MonoBehaviour
{
    public static SelectedField Instance { get; private set; }
    private Field _currentSelectedField;
    public Field CurrentSelectedField 
    {
        get => _currentSelectedField;
        set
        {
            _currentSelectedField?.DropSelect();

            _currentSelectedField = value;
        }
    }

    private void Start()
    {
        Instance = this;
        CurrentSelectedField = null;
    }

    public void DropSelect()
    {
        _currentSelectedField?.DropSelect();
        _currentSelectedField = null;
    }
}
