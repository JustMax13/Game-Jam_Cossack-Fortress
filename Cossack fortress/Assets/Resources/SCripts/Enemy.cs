using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxHitPoint;
    [SerializeField] private float _defaultDamage;
    [SerializeField] private float _defaultSpeed;

    public float HitPoint { get; set; }
    public float Damage { get; set; }
    public float Speed { get; set; }

    private void Start()
    {
        HitPoint = _maxHitPoint;
        Damage = _defaultDamage;
        Speed = _defaultSpeed;
    }
}
