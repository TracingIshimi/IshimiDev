using UnityEngine;

public class SpFunc: MonoBehaviour
{
    [SerializeField] SpiritCamera spcam;
    [SerializeField] GameObject[] deactivateItems;
    [SerializeField] GameObject[] activateItems;

    private void OnEnable(){
        Debug.Log("SpAction Call: Default");
        spcam.SetSpResolution();
        for(int i = 0; i<deactivateItems.Length; i++){
            deactivateItems[i].gameObject.SetActive(false);
        }
        for(int j = 0; j<activateItems.Length; j++){
            activateItems[j].gameObject.SetActive(true);
        }
        SpActions();
    }
    private void OnDisable(){
        spcam.SetIidleResolution();
        for(int i = 0; i<deactivateItems.Length; i++){
            deactivateItems[i].gameObject.SetActive(true);
        }
        for(int j = 0; j<activateItems.Length; j++){
            activateItems[j].gameObject.SetActive(false);
        }
        SpExit();
    }
    protected  virtual void SpActions(){
        
        // Need overriding
    }

    protected virtual void SpExit(){
        
    }
}