using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager stageManager;

    public int stageNum = 1;

    private void Awake(){
        if(stageManager == null){
            stageManager = this;
            DontDestroyOnLoad(stageManager);
        }
        else if(stageManager != this){
            Destroy(this);
            return;
        }
    }

    public void SetStageNum(int num){
        stageNum = num;
    }
    public int GetStageNum(){
        return stageNum;
    }
}
