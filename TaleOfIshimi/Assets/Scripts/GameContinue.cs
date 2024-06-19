using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameContinue : MonoBehaviour
{
    [SerializeField] GameObject modalPanel;
    [SerializeField] Button continueBtn;
    // Start is called before the first frame update
    void Start()
    {
        modalPanel.SetActive(false);
        continueBtn.onClick.AddListener(ShowModal);
    }

    void ShowModal() {
        modalPanel.SetActive(true);
    }
}
