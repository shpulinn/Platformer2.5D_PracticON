using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private GameObject healthIconPrefab;

    private List<GameObject> _healthIcons = new List<GameObject>();

    public void Setup(int maxHealth)
    {
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject healthIcon = Instantiate(healthIconPrefab, transform);
            _healthIcons.Add(healthIcon);
        }
    }

    public void RefreshHealth(int currentHealth)
    {
        for (int i = 0; i < _healthIcons.Count; i++)
        {
            if (i < currentHealth)
            {
                //_healthIcons[i].SetActive(true);
                _healthIcons[i].GetComponent<Image>().color = new Color(255, 255, 255, 1f);
            }
            else
            {
                // _healthIcons[i].SetActive(false);
                _healthIcons[i].GetComponent<Image>().color = new Color(255, 255, 255, 0.1f);
            }
        }
    }
}
