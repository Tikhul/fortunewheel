using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationController : FortuneWheelElement
{
    //private Sequence rotationSequence; // Общая последовательность
    [SerializeField] private RotationCalculations _calculations;

    public RotationCalculations Calculations
    {
        get => _calculations;
        set => _calculations = value;
    }

    public void CollectSequence(Sequence rotationSequence)
// Заполнение очереди анимаций
    {
        Game.Controller.SectorController.LaunchSectorTurnOff();
        rotationSequence.Append(StartWheel());
        rotationSequence.Append(MiddleWheel());
        rotationSequence.Append(FinalWheel());
        StartCoroutine(AfterSequence(rotationSequence));
    }

    IEnumerator AfterSequence(Sequence rotationSequence)
    {
        yield return rotationSequence.WaitForCompletion();
        Game.Controller.SectorController.LaunchSectorHighlight();
    }
    private Tweener StartWheel()
 //    От старта до максимальной скорости
    {
        Tweener startTween = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -Calculations.RotationToMax()), Game.Model.WheelModel.RotationSO.TimeToMax, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(Ease.InSine);

        return startTween;
    }
    private Tweener MiddleWheel()
 // Вращение на максимальной скорости
    {
        Tweener middleTween = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -Calculations.RotationAtMax()), Game.Model.WheelModel.RotationSO.TimeAtMax, RotateMode.FastBeyond360)
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
        Debug.Log(Calculations.FinalFullTime());
        Tweener finalFull = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -Calculations.FinalFullRotations()), Calculations.FinalFullTime(), RotateMode.FastBeyond360)
            .SetRelative(true);
        return finalFull;
    }

    private Tweener FinalExtraRotations()
// Докручивание до победителя
    {
        Debug.Log(Calculations.FinalExtraTime());
        Debug.Log(Calculations.RotationToWinner());
        Tweener finalExtra = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, Calculations.RotationToWinner()), Calculations.FinalExtraTime(), RotateMode.FastBeyond360)
            .SetRelative(false);
        return finalExtra;
    }
}
