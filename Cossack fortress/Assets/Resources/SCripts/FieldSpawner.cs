using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSpawner : MonoBehaviour
{
    public static FieldSpawner Instance { get; private set; }
    private void Start()
    {
        Instance = this;
    }

    public void Spawn(BuildingInfo buildingInfo)
    {
        var selectField = SelectedField.Instance.CurrentSelectedField.gameObject;
        var spawnObject = Instantiate(buildingInfo.Prefab, selectField.transform.parent);

        spawnObject.transform.position = selectField.transform.position;

        Destroy(selectField.gameObject);
    }
}
