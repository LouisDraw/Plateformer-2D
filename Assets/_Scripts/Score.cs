using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        
    }

    public void UpdateLife()
    { 
        string newText = string.Empty;
        print(GameManager.Instance.MaxLifePoints + " " + GameManager.Instance.LifePoints);
        newText = string.Concat(Enumerable.Repeat("L ", GameManager.Instance.LifePoints));
        print(newText + (GameManager.Instance.MaxLifePoints - GameManager.Instance.LifePoints));
        int lostLifePoint = GameManager.Instance.MaxLifePoints - GameManager.Instance.LifePoints;
        print(lostLifePoint);
        if (lostLifePoint <= 0)
        {
            newText += string.Concat(Enumerable.Repeat("Q ", GameManager.Instance.MaxLifePoints - GameManager.Instance.LifePoints));
        }
        text.text = newText;
    }
}
