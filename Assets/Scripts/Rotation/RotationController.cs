using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationController : FortuneWheelElement
{
    private Sequence rotationSequence; // Общая последовательность
    [SerializeField] private RotationCalculations _calculations;

    public RotationCalculations Calculations
    {
        get => _calculations;
        set => _calculations = value;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rotationSequence = DOTween.Sequence();
            CollectSequence();
        }
    }

    private void CollectSequence()
// Заполнение очереди анимаций
    {
        rotationSequence.Append(StartWheel());
        rotationSequence.Append(MiddleWheel());
        rotationSequence.Append(FinalWheel());
    }
    private Tweener StartWheel()
 //    От старта до максимальной скорости
    {
        Tweener startTween = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -Calculations.RotationToMax()), Game.Model.RotationModel.TimeToMax, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(Ease.InSine);
        return startTween;
    }
    private Tweener MiddleWheel()
 // Вращение на максимальной скорости
    {
        Tweener middleTween = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -Calculations.RotationAtMax()), Game.Model.RotationModel.TimeAtMax, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(Ease.Linear);
        return middleTween;
    }

    private Sequence FinalWheel()
    // Замедление до нужного сектора
    {
        Game.Controller.WheelController.LaunchWinnerChoice(); // Выбор победителя
        Sequence finalSequence = DOTween.Sequence();
        finalSequence.Append(FinalFullRotations());
        finalSequence.Append(FinalExtraRotations());
        finalSequence.SetEase(Ease.OutSine);

        return finalSequence;
    }

    private Tweener FinalFullRotations()
// Основные (полные) повороты до остановки
    {
        Tweener finalFull = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -Calculations.FinalFullRotations()), Calculations.FinalFullTime(), RotateMode.FastBeyond360)
            .SetRelative(true);
        return finalFull;
    }

    private Tweener FinalExtraRotations()
// Докручивание до победителя
    {
        Tweener finalExtra = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -Calculations.RotationToWinner()), Calculations.FinalExtraTime(), RotateMode.FastBeyond360)
            .SetRelative(false);
        return finalExtra;
    }
    
}
