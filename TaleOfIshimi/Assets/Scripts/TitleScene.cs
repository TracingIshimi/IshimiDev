using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField] GameObject modalPanel;

    public void SceneChange() {
        SceneManager.LoadScene("1_IdleRoom");
    }

    public void ModalPanelOn() {
        modalPanel.SetActive(true);
    }

    public void ModalPanelOff() {
        modalPanel.SetActive(false);
    }
}
