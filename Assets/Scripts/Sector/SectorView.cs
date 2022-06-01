using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SectorView : FortuneWheelElement
{
    private int sectorId;
    [SerializeField] private TMP_Text _idText;

    public TMP_Text IdText
    {
        get => _idText;
        set => _idText = value;
    }

    public void SetUpSector(Image newSector, float zRotation, int index)
    {
        sectorId = Game.Model.WheelModel.Sectors[index].Id;
        
        SetTransform(newSector, zRotation);
        SetColor(newSector, index);
        SetText(index);
    }

    private void SetTransform(Image newSector, float zRotation)
    {
        newSector.transform.SetParent(Game.Model.WheelModel.SectorsParent.transform);
        newSector.transform.localScale = new Vector3(1, 1, 1);
        newSector.transform.localPosition = newSector.transform.position;
        newSector.transform.localRotation = Quaternion.Euler(0, 0, zRotation);
    }
    private void SetColor(Image newSector, int index)
    {
        newSector.sprite = Game.Model.WheelModel.Sectors[index].SourceImage;
        Color tmp = newSector.color;
        tmp.a = 0.3f;
        newSector.color = tmp;
        newSector.fillAmount = (float)Math.Round((double)1 / Game.Model.WheelModel.Sectors.Count, 3);
    }
    private void SetText(int index)
    {
        IdText.text = sectorId.ToString();
    }

    public void HighlightSector(int receivedId)
    {
        if (receivedId.Equals(sectorId))
        {
            Color tmp = GetComponent<Image>().color;
            tmp.a = 1.0f;
            GetComponent<Image>().color = tmp;
        }
    }
}
