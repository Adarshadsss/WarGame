using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public Button StatusButton;
    public GameObject StatusPanel;


    public void ShowStatusPanel()
    {
        if (StatusPanel.activeInHierarchy)
        {
            StatusPanel.SetActive(false);
        }
        else
        {
            StatusPanel.SetActive(true);
        }
    }

    
}
