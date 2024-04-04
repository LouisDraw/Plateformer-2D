using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class GameOverController : MonoBehaviour
{
    [SerializeField] private UIDocument m_UIDocument;
    private VisualElement m_root;

    private Button rageQuitButton;
    private Button restartButton;
    private Button mainMenuButton;

 
    void Awake()
    {
        Camera.main.gameObject.GetComponent<PostProcessVolume>().profile = GameManager.Instance.GetPPPMain;
        m_UIDocument.enabled = false;
    }

    private void RageQuitClicked(ClickEvent _event)
    {
        Debug.Log("RageQuitClicked");
        Application.Quit();
    }

    private void RestartClicked(ClickEvent _event)
    {
        Debug.Log("Restart button clicked");
        SceneManager.LoadScene(GameManager.Instance.NextScene.name);
    }

    private void MainMenuClicked(ClickEvent _event)
    {
        Debug.Log("Main Menu clicked");
        SceneManager.LoadScene(GameManager.Instance.MainMenuScene.name);
    }

    public void GameOver()
    {
        m_UIDocument.enabled = true;

        m_root = m_UIDocument.rootVisualElement;
        
        rageQuitButton = m_root.Q<Button>("RageQuit");
        restartButton = m_root.Q<Button>("Retry");
        mainMenuButton = m_root.Q<Button>("MainMenu");

        rageQuitButton.RegisterCallback<ClickEvent>(RageQuitClicked);
        restartButton.RegisterCallback<ClickEvent>(RestartClicked);
        mainMenuButton.RegisterCallback<ClickEvent>(MainMenuClicked);


        Camera.main.gameObject.GetComponent<PostProcessVolume>().profile = GameManager.Instance.GetPPPUI;
    }

}