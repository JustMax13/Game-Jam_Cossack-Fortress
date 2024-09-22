using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class BuildingField : MonoBehaviour
{
    [Header("��������")]
    [SerializeField] private GameObject _defaultFieldPrefab;
    [SerializeField] private SpriteRenderer _thisSpriteRender;

    [Header("� ���� ������")]
    [SerializeField] private Sprite _calmSprite;
    [SerializeField] private float _maxHitPoint;
    [SerializeField] private float _earningsPerSecond;

    [Header("� ���� �������")]
    [SerializeField] private Sprite _defenderSprite;
    [SerializeField] private float _defenderMaxHitPoint;
    [SerializeField] private float _damage;
    // ���� �� ���� ������� ������� ����

    private float _hitPoint;
    private float _defenderHitPoint;
    private State _curentState;

    private void Update()
    {
        CheckHitPoints();

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
        // ��� �� ��������� ������� � ����������� ���������, ������ �����
        ChangeState();
    }

    private void InCalmStateActionPerUpdate()
    {
        MainCurrency.Instance.Value += _earningsPerSecond * Time.deltaTime;
    }
    private void InDefenderStateActionPerUpdate() 
    {
        // ͳ�� �� ������
    }

    private void CheckHitPoints()
    {
        if (_hitPoint <= 0 || _defenderHitPoint <= 0)
            DestroyThisBuilding();
    }
    private void DestroyThisBuilding()
    {
        throw new System.Exception("����������� ���");
        // ����� ���� ���������� �� ���������� ������� ������� ���� ( ��� ��� ��������� ���� ���� ������ )
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
    }


    enum State
    {
        Calm,
        Defender
    }
}
