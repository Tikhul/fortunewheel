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

    private Tweener FinalWheel()
// Докручивание до победителя
    {
        Game.Controller.WheelController.LaunchWinnerChoice();
        Tweener finalExtra = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -Calculations.FinalFullRotations() + Calculations.RotationToWinner()), Game.Model.WheelModel.RotationSO.TimeAfterMax, RotateMode.FastBeyond360)
            .SetRelative(false)
            .SetEase(Ease.OutSine); 
        return finalExtra;
    }
}
