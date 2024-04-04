using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;


    [Header("Player")]
    [SerializeField] private int _maxLifePoints;


    [Header("Post Process Management")]
    [SerializeField] private PostProcessProfile _ppProfile_Main;
    [SerializeField] private PostProcessProfile _ppProfile_UI;


    [Header("Scene Management")]
    [SerializeField] private Scene _previousScene;
    [SerializeField] private Scene _mainMenuScene;
    [SerializeField] private Scene _nextScene;


    private int _coinScore = 0;
    private int _lifepoint;
    private bool _death = false;



    // ppProfil Accessibilty
    public PostProcessProfile GetPPPMain
    {
        get => _ppProfile_Main;
    }

    public PostProcessProfile GetPPPUI
    {
        get => _ppProfile_UI;
    }


    // Scenes Accessibilty
    public Scene NextScene
    {
        get => _nextScene;
    }
    public Scene PreviousScene
    {
        get => _previousScene;
    }
    public Scene MainMenuScene
    {
        get => _mainMenuScene;
    }


    // Player Attributes Accessibility
    public bool Death
    {
        get => _death;
        set => _death = value;
    }
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
        get => _lifepoint;
        set => _lifepoint = value;
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
        _lifepoint = _maxLifePoints;
    }
}