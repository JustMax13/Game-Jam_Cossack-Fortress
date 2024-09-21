using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBuyButton : MonoBehaviour
{
    [SerializeField] private BuildingInfo _buildingInfo;
    [SerializeField] private Button _button;

    private void Update()
    {
        if(MainCurrency.Instance.Value < _buildingInfo.Price)
            _button.interactable = false;
        else
            _button.interactable = true;
    }
}
