using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SpiritCamera : MonoBehaviour
{
    [SerializeField] Camera objCam;
    void Start()
    {
        SetIidleResolution();
    }

    public void SetIidleResolution(){
        int width = Const.IDLE_SCREEN_WIDTH;
        int height = Const.IDLE_SCREEN_HEIGHT;

        int winWidth = Screen.width;
        int winHeight = Screen.height;

        Screen.SetResolution(width,(int)((winHeight/winWidth)*width),true);
        if((float)width/height<(float)winWidth/winHeight){
            float newWidth = ((float)width/height)/((float)winWidth/winHeight);
            StartCoroutine(SetResolution((1f-newWidth)/2f,0f,newWidth,1f));
        }
        else{
            float newHeight = ((float)winWidth/winHeight)/((float)width/height);
            StartCoroutine(SetResolution(0f,(1f-newHeight)/2f,1f,newHeight));
        }
    }

    public void SetSpResolution(){
        float width = Const.SP_SCREEN_WIDTH;
        float height = Const.SP_SCREEN_HEIGHT;

        int winWidth = Screen.width;
        int winHeight = Screen.height;

        if((float)width/height<(float)winWidth/winHeight){
            float newWidth = ((float)width/height)/((float)winWidth/winHeight);
            StartCoroutine(SetResolution((1f-newWidth)/2f,0f,newWidth,1f));
        }
        else{
            float newHeight = ((float)winWidth/winHeight)/((float)width/height);
            StartCoroutine(SetResolution(0f,(1f-newHeight)/2f,1f,newHeight));
        }
    }

    IEnumerator SetResolution(float a, float b, float c, float d){
        float deltaA = (a - objCam.rect.x)/Const.SCREEN_EFF_SPEED;
        float deltaB = (b - objCam.rect.y)/Const.SCREEN_EFF_SPEED;
        float deltaC = (c - objCam.rect.width)/Const.SCREEN_EFF_SPEED;
        float deltaD = (d - objCam.rect.height)/Const.SCREEN_EFF_SPEED;
        while(true){
            if(Math.Abs(objCam.rect.x-a)<0.001f && Math.Abs(objCam.rect.y-b)<0.001f && Math.Abs(objCam.rect.width-c)<0.001f && Math.Abs(objCam.rect.height-d)<0.001f){
                objCam.rect = new Rect(a,b,c,d);
                StopCoroutine("SetResolution");
                break;
            }
            objCam.rect = new Rect(objCam.rect.x+deltaA, objCam.rect.y+deltaB, objCam.rect.width+deltaC, objCam.rect.height+deltaD);
            yield return null;
        }
        yield return null;
    }
}
