using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;

public class BuildingField : MonoBehaviour
{
    [Header("Загальне")]
    [SerializeField] private GameObject _defaultFieldPrefab;
    [SerializeField] private SpriteRenderer _thisSpriteRender;
    [SerializeField] private Slider _hpBar;
    [SerializeField] private Canvas _hpBarCanvas;


    [Header("В стані спокою")]
    [SerializeField] private Sprite _calmSprite;
    [SerializeField] private float _maxHitPoint;
    [SerializeField] private float _earningsPerSecond;

    [Header("В стані захисту")]
    [SerializeField] private Sprite _defenderSprite;
    [SerializeField] private float _maxDefenderHitPoint;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _timeToReload;

    private bool _destroyStarted;
    private float _hitPoint;
    private float _defenderHitPoint;
    private float _currentWaitUntilAttack;
    private State _curentState;
    private Enemy _targetEnemy;

    private void Start()
    {
        _hpBarCanvas.worldCamera = Camera.main;
        _destroyStarted = false;
        _hitPoint = _maxHitPoint;
        _defenderHitPoint = _maxDefenderHitPoint;
        BuildingsOnMap.Instance.Buildings.Add(this);

        UpdateSlider();
    }
    private void OnDestroy()
    {
        BuildingsOnMap.Instance.Buildings.Remove(this);
    }
    private void Update()
    {
        switch (_curentState)
        {
            case State.Calm:
                {
                    InCalmStateActionPerUpdate();
                    break;
                };

            case State.Defender:
                {
                    InDefenderStateActionPerUpdate();
                    break;
                };
        }
    }
    private void OnMouseDown()
    {
        // тут же поставить счетчик и отсчитывать удержание, именно перед
        ChangeState();
    }

    private void InCalmStateActionPerUpdate()
    {
        MainCurrency.Instance.Value += _earningsPerSecond * Time.deltaTime;
    }
    private void InDefenderStateActionPerUpdate() 
    {
        _targetEnemy = FindClosestEnemy();

        if (_targetEnemy != null)
            Attack();
    }

    private Enemy FindClosestEnemy()
    {
        Enemy closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (var target in EnemyOnMap.Instance.Enemies)
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

    private void Attack()
    {
        if (_currentWaitUntilAttack <= 0)
        {
            _targetEnemy.GetDamage(_damage);
            RestartTimer();
        }
        else
            _currentWaitUntilAttack -= Time.deltaTime;
    }
    private void RestartTimer()
        => _currentWaitUntilAttack = _timeToReload;

    private void CheckHitPoints()
    {
        if (_hitPoint <= 0 || _defenderHitPoint <= 0)
            DestroyThisBuilding();
    }
    private void DestroyThisBuilding()
    {
        if (_destroyStarted) return;
        var defaultField = Instantiate(_defaultFieldPrefab, gameObject.transform.parent);
        defaultField.transform.position = gameObject.transform.position;

        _destroyStarted = true;
        Destroy(gameObject);
    }
    private void ChangeState()
    {
        switch (_curentState) 
        {
            case State.Calm:
                {
                    _curentState = State.Defender;
                    _thisSpriteRender.sprite = _defenderSprite;
                    break;
                }
            case State.Defender: 
                {
                    _curentState = State.Calm;
                    _thisSpriteRender.sprite = _calmSprite;
                    break;
                }
        }

        UpdateSlider();
    }

    public void GetDamage(float damage)
    {
        switch (_curentState)
        {
            case State.Calm:
                {
                    _hitPoint -= damage;
                    break;
                }
            case State.Defender:
                {
                    _defenderHitPoint -= damage;
                    break;
                }
        }

        CheckHitPoints();
        UpdateSlider();
    }

    private void UpdateSlider()
    {
        switch (_curentState)
        {
            case State.Calm:
                {
                    _hpBar.value = _hitPoint / _maxHitPoint;
                    break;
                }
            case State.Defender:
                {
                    _hpBar.value = _defenderHitPoint / _maxDefenderHitPoint;
                    break;
                }
        }
    }

    enum State
    {
        Calm,
        Defender
    }
}
