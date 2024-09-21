using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private UI_Value _UI_Value;
    [SerializeField] private SpriteRenderer _selectVisual;

    private void Start()
    {
        _selectVisual.enabled = false;
    }
    private void OnMouseDown()
    {
        if (TouchManager.Instance.IsPointerOnUI())
            return;

        SetSelect();
        _UI_Value.CloseInverfaces();
        _UI_Value.OpenInverfaces();
    }

    public void SetSelect()
    {
        _selectVisual.enabled = true;
        SelectedField.Instance.CurrentSelectedField = this;
    }
    public void DropSelect()
    {
        _selectVisual.enabled = false;
    }
}
