using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelModel : FortuneWheelElement
{
    private Dictionary<int, List<double>> _sectorsInfo = new Dictionary<int, List<double>>();
    private int _probabilitySum;
    private int _actualWinnerId;
    [SerializeField] private GameObject _sectorsParent;
    [SerializeField] private List<SectorSO> _sectors;
    [SerializeField] private Image _sectorPrefab;
    [SerializeField] private int _receivedWinnerId;
    [SerializeField] private bool _randomWinner = true;

    public GameObject SectorsParent
    {
        get => _sectorsParent;
        set => _sectorsParent = value;
    }
    public Dictionary<int, List<double>> SectorsInfo
    {
        get => _sectorsInfo;
        set => _sectorsInfo = value;
    }
    public int ProbabilitySum
    {
        get => _probabilitySum;
        set => _probabilitySum = value;
    }
    
    public List<SectorSO> Sectors
    {
        get
        {
            if (_sectors.Count < 3 || _sectors.Count > 15)
            {
                Debug.Log("Нужно от 3 до 15 секторов");
                return null;
            }
            else
            {
                return _sectors;
            }
        }
        set => _sectors = value;
    }

    public int ReceivedWinnerId
    {
        get => _receivedWinnerId;
        set => _receivedWinnerId = value;
    }

    public int ActualWinnerId
    {
        get => _actualWinnerId;
        set => _actualWinnerId = value;
    }
    public bool RandomWinner
    {
        get => _randomWinner;
        set => _randomWinner = value;
    }
    public Image SectorPrefab
    {
        get => _sectorPrefab;
        set => _sectorPrefab = value;
    }
}
