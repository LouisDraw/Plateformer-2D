using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Life : MonoBehaviour
{
    TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        UpdateLife();

    }

    public void UpdateLife()
    { 
        string newText = string.Empty;
        newText = string.Concat(Enumerable.Repeat("L ", GameManager.Instance.LifePoints));
        newText += string.Concat(Enumerable.Repeat("Q ", GameManager.Instance.MaxLifePoints - GameManager.Instance.LifePoints));
        text.text = newText;
    }
}
