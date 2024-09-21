using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Value : MonoBehaviour
{
    [SerializeField] private GameObject[] _interfacesToOpen;
    [SerializeField] private GameObject[] _interfacesToClose;

    public GameObject[] InterfacesToOpen { get => _interfacesToOpen; private set => _interfacesToOpen = value; }
    public GameObject[] InterfacesToClose { get => _interfacesToClose; private set => _interfacesToClose = value; }

    public void OpenInverfaces()
    {
        foreach (var interfaceToOpen in _interfacesToOpen)
            interfaceToOpen.SetActive(true);
    }
    public void CloseInverfaces()
    {
        foreach (var interfaceToClose in _interfacesToClose)
            interfaceToClose.SetActive(false);
    }
}
