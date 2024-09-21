using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellManager : MonoBehaviour
{
    public static SellManager Instance { get; private set; }
    private void Start()
    {
        Instance = this;
    }

    public void Buy(BuildingInfo buildingInfo)
    {
        if (MainCurrency.Instance.Value < buildingInfo.Price)
            Debug.LogException(new System.Exception("Купівля неможлива, недостатньо коштів!"));

        FieldSpawner.Instance.Spawn(buildingInfo);
        MainCurrency.Instance.Value -= buildingInfo.Price;

        SelectedField.Instance.CurrentSelectedField = null;
    }
}
