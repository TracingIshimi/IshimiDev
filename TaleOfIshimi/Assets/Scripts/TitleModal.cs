using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleModal : MonoBehaviour
{
    [SerializeField] GameObject modalPanel;
    [SerializeField] Button noBtn;
    // Start is called before the first frame update
    void Start()
    {
        noBtn.onClick.AddListener(HideModal);
    }

    void HideModal() {
        modalPanel.SetActive(false);
    }
}
