using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RotationScriptableObject", menuName = "ScriptableObjects/RotationSO", order = 1)]

public class RotationSO : ScriptableObject
{
    [SerializeField] private float _timeToMax; // ����� �������
    [SerializeField] private float _timeAtMax; // ����� �� ���� ���������
    [SerializeField] private float _timeAfterMax; // ����� ����������
    [SerializeField] private float _maxSpeed; // ������������ ��������

    public float TimeToMax
    {
        get => _timeToMax;
        set => _timeToMax = value;
    }
    public float TimeAtMax
    {
        get => _timeAtMax;
        set => _timeAtMax = value;
    }
    public float TimeAfterMax
    {
        get => _timeAfterMax;
        set => _timeAfterMax = value;
    }
    public float MaxSpeed
    {
        get => _maxSpeed;
        set => _maxSpeed = value;

    }
}
