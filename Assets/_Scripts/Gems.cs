using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gems : MonoBehaviour
{

    [SerializeField] Animator animator;
    TextMeshProUGUI text;
    private Score score;

    void Awake()
    {
        score = GameObject.FindWithTag("Score").GetComponent<Score>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.name);
        if (collider.gameObject.tag == "Player")
        {
            GameManager.Instance.GetComponent<AudioController>().PlayPoint();
            Destroy(GetComponent<BoxCollider2D>());
            GameManager.Instance.Coins += 1;
            score.UpdateScore();
            StartCoroutine(KillGem());
        }
    }


    IEnumerator KillGem()
    {
        animator.SetBool("Death", true);
        yield return new WaitForSeconds(.3f);
        Destroy(gameObject);
    }
}
