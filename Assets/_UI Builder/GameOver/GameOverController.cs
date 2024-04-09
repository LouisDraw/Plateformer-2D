using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private AudioController audioController;
    private bool _canExecute;

 
    void Awake()
    {
        Camera.main.gameObject.GetComponent<PostProcessVolume>().profile = GameManager.Instance.GetPPPMain;
        m_UIDocument.enabled = false;
        audioController = GameManager.Instance.GetComponent<AudioController>();
    }

    private void RageQuitClicked(ClickEvent _event)
    {
        Debug.Log("RageQuitClicked");
        StartCoroutine(ChangeScene(null));
    }

    private void RestartClicked(ClickEvent _event)
    {
        Debug.Log("Restart button clicked");
        StartCoroutine(ChangeScene(SceneManager.GetActiveScene().name));
    }

    private void MainMenuClicked(ClickEvent _event)
    {
        
        Debug.Log("Main Menu clicked");
        StartCoroutine(ChangeScene(GameManager.Instance.MainMenuScene));
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

    private IEnumerator ChangeScene(string sceneName)
    {
        audioController = GameManager.Instance.GetComponent<AudioController>();
        yield return new WaitForSeconds(audioController.PlayConfirm());
        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }
        Application.Quit();
    }
}