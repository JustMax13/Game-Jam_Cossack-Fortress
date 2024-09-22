using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _selectVisual;
    private UI_Value _UI_Value;

    private void Start()
    {
        _UI_Value = DefaultFieldValue.Instance.UI_Value;
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
