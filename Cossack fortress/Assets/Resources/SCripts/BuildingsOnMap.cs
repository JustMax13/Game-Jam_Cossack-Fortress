using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsOnMap : MonoBehaviour
{
    public static BuildingsOnMap Instance {  get; private set; }
    public List<BuildingField> Buildings {  get; private set; }
    public event Action BuildingsIsEnd;

    private void Awake()
    {
        Instance = this;
        Buildings = new List<BuildingField>();
    }
    private void Update()
    {
        if (Buildings.Count <= 0)
            BuildingsIsEnd?.Invoke();
    }
}
