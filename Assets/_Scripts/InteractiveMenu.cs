using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;



public class InteractiveMenu : MonoBehaviour
{
    [SerializeField] private bool _isQuit;
    private string _startScene;
    private bool _isStaying = false;
    private Collider2D _collider;

    void Start()
    {
        GameManager.Instance.ShowInstruction(false);
        _startScene = GameManager.Instance.NextScene;
    }


    void Update()
    {
        if (_isStaying)
        {
            CustomOnTriggerStay2D(_collider);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameManager.Instance.ShowInstruction(true);
        _isStaying = true;
        _collider = collider;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        GameManager.Instance.ShowInstruction(false); 
        _isStaying = false;
    }

    private void CustomOnTriggerStay2D(Collider2D collider)
    {
        Debug.Log("Collision detection");
        if (Input.GetKey(KeyCode.E)) //le key down n'est pas detecté a chaque fois...
        {
            Debug.Log("E Pressed");
            if(_isQuit)
            {
                Debug.Log("Application quitter");
                Application.Quit();
            }
            else
            {
                Debug.Log("Play scene lancée");
                SceneManager.LoadScene(_startScene);
            }
        }
    }
}
