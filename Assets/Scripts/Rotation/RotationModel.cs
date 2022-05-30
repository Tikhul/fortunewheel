using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotationModel : FortuneWheelElement
{
    [SerializeField] private float _timeToMax; // Время разгона
    [SerializeField] private float _timeAtMax; // Время на макс раскрутке
    [SerializeField] private float _timeAfterMax; // Время замедления
    [SerializeField] private float _maxSpeed; // Максимальная скорость

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
