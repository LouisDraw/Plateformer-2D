using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;



public class InteractiveMenu : MonoBehaviour
{
    [SerializeField] string _startScene;

    void Start()
    {
        GameManager.Instance.ShowInstruction(false);
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameManager.Instance.ShowInstruction(true);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        GameManager.Instance.ShowInstruction(false);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        Debug.Log("Collision detection");
        if (Input.GetKey(KeyCode.E)) //le key down n'est pas detecté a chaque fois...
        {
            Debug.Log("E Pressed");
            if(_startScene == string.Empty)
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
