using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SectorView : FortuneWheelElement
{
    [SerializeField] private TMP_Text _idText;

    public TMP_Text IdText
    {
        get => _idText;
        set => _idText = value;
    }

    public void SetUpSector(Image newSector, float zRotation, int index)
    {
        SetTransform(newSector, zRotation, index);
        SetText(index);
    }

    private void SetTransform(Image newSector, float zRotation, int index)
    {
        newSector.transform.SetParent(Game.Model.WheelModel.SectorsParent.transform);
        newSector.sprite = Game.Model.WheelModel.Sectors[index].SourceImage;
        newSector.transform.localScale = new Vector3(1, 1, 1);
        newSector.transform.localPosition = newSector.transform.position;
        newSector.transform.localRotation = Quaternion.Euler(0, 0, zRotation);
        newSector.fillAmount = (float)Math.Round((double)1 / Game.Model.WheelModel.Sectors.Count, 3);
    }
    private void SetText(int index)
    {
        IdText.text = Game.Model.WheelModel.Sectors[index].Id.ToString();
    }
}
