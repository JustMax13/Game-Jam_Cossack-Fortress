using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfo : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _price;


    public int Price { get => _price; private set => _price = value; }
    public GameObject Prefab { get => _prefab; private set => _prefab = value; }
}
