using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationController : FortuneWheelElement
{
    private Sequence rotationSequence; // Общая последовательность

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
        float acceleration = Game.Model.RotationModel.MaxSpeed / Game.Model.RotationModel.TimeToMax; // Расчет ускорения
        float totalRotation = (acceleration * Game.Model.RotationModel.TimeToMax * Game.Model.RotationModel.TimeToMax) / 2; // Расчет градусов поворота

        Tweener startTween = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -totalRotation), Game.Model.RotationModel.TimeToMax, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(Ease.InSine);
        return startTween;
    }
    private Tweener MiddleWheel()
 // Вращение на максимальной скорости
    {
        float totalRotation = Game.Model.RotationModel.MaxSpeed * Game.Model.RotationModel.TimeAtMax; // Сколько градусов должен пройти

        Tweener middleTween = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -totalRotation), Game.Model.RotationModel.TimeAtMax, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(Ease.Linear);
        return middleTween;
    }

    private Tweener FinalWheel()
    // Замедление до нужного сектора
    {
        float deceleration = Game.Model.RotationModel.MaxSpeed / Game.Model.RotationModel.TimeAfterMax; // Расчет замедления
        float totalRotation = (deceleration * Game.Model.RotationModel.TimeAfterMax * Game.Model.RotationModel.TimeAfterMax) / 2; // Расчет градусов поворота

        Tweener finalTween = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -totalRotation), Game.Model.RotationModel.TimeAfterMax, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(Ease.OutSine);
        return finalTween;
    }
}
