using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationController : FortuneWheelElement
{
    private Sequence rotationSequence;

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
  //      rotationSequence.Append(StartWheel());
        rotationSequence.Append(MiddleWheel());
    }
 //   private Tweener StartWheel()
// От старта до максимальной скорости
    //{
    //    Tweener startTween = Game.Model.WheelModel.SectorsParent.transform.DORotate(
    //        new Vector3(0, 0, -360), Game.Model.RotationModel.TimeToMax, RotateMode.FastBeyond360)
    //        .SetLoops(-1, LoopType.Incremental)
    //        .SetEase(Ease.Linear);
    //    return startTween;
    //}
    private Tweener MiddleWheel()
    // Вращение на максимальной скорости
    {
        float totalRotation = Game.Model.RotationModel.MaxSpeed * Game.Model.RotationModel.TimeAtMax; // Сколько градусов должен пройти
        Debug.Log(totalRotation);
        Tweener middleTween = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -totalRotation), Game.Model.RotationModel.TimeAtMax, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(Ease.Linear);
        return middleTween;
    }
    //    private Tweener FinalWheel()
    //// Замедление до нужного сектора
    //    {

    //    }
}
