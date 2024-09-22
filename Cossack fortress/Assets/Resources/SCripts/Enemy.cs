using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Slider _hpBar;
    [SerializeField] private Canvas _hpBarCanvas;
    [SerializeField] private float _defaultHitPoint;
    [SerializeField] private float _defaultDamage;
    [SerializeField] private float _defaultShotTimeSpeed;
    [SerializeField] private float _defaultSpeed;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _timeToReload;

    private float _currentWaitUntilAttack;
    private bool _canAttack;
    private BuildingField _targetBuilding;
    private BuildingField _lastTargetBuilding;
    private float _curentMaxHitPoint;

    public float HitPoint { get; set; }
    public float Damage { get; set; }
    public float Speed { get; set; }
    public float ShotTimeSpeed { get; set; }
    public bool CanAttack 
    { 
        get => _canAttack;
        set 
        {
            _canAttack = value;
            _currentWaitUntilAttack = 0;
        }
    }

    public void Initialization()
    {
        CanAttack = false;
        HitPoint = _defaultHitPoint;
        _curentMaxHitPoint = HitPoint;
        Damage = _defaultDamage;
        Speed = _defaultSpeed;
        ShotTimeSpeed = _defaultShotTimeSpeed;

        _hpBarCanvas.worldCamera = Camera.main;
        UpdateSlider();

        EnemyOnMap.Instance.Enemies.Add(this);
    }
    private void Update()
    {
        if (HitPoint > _curentMaxHitPoint)
            _curentMaxHitPoint = HitPoint;

        _targetBuilding = FindClosestBuilding();

        if (_targetBuilding == null)
            return;
        if (_lastTargetBuilding != _targetBuilding)
            CanAttack = false;

        if(CanAttack)
            Attack();
        else
            Move();

        _lastTargetBuilding = _targetBuilding;
    }

    private void UpdateSlider()
        => _hpBar.value = HitPoint / _curentMaxHitPoint;
    private void Move()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = _targetBuilding.transform.position;

        float distance = Vector3.Distance(currentPosition, targetPosition);
        if (distance > _attackDistance)
        {
            Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, Speed * Time.deltaTime);
            transform.position = newPosition;

            CanAttack = false;
        }
        else
            CanAttack = true;
    }

    private void Attack()
    {
        if(_currentWaitUntilAttack <= 0)
        {
            _targetBuilding.GetDamage(Damage);
            RestartTimer();
        }
        else
            _currentWaitUntilAttack -= Time.deltaTime;

    }
    private void RestartTimer()
        => _currentWaitUntilAttack = _timeToReload;
    private BuildingField FindClosestBuilding()
    {
        BuildingField closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (var target in BuildingsOnMap.Instance.Buildings)
        {
            float distance = Vector3.Distance(currentPosition, target.transform.position);
            if (distance < minDistance)
            {
                closest = target;
                minDistance = distance;
            }
        }

        return closest;
    }
    public void GetDamage(float damage)
    {
        HitPoint -= damage;
        UpdateSlider();

        CheckHitPoint();
    }

    private void CheckHitPoint()
    {
        if (HitPoint <= 0)
            DoDestroy();
    }
    private void DoDestroy()
    {
        EnemyOnMap.Instance.Enemies.Remove(this);
        Destroy(gameObject);
    }
}
