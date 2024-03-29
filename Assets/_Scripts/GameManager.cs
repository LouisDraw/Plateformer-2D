using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance => _instance;

    [SerializeField] private int _maxLifePoints;

    private int _coinScore = 0;
    private int _lifepoint;
    
    


    public int MaxLifePoints
    {
        get => _maxLifePoints;
    }
    public int Coins
    {
        get => _coinScore;
        set => _coinScore = value;
    }

    
    public int LifePoints
    {
        get => _coinScore;
        set => _coinScore = value;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
        _lifepoint = MaxLifePoints;
    }
}
