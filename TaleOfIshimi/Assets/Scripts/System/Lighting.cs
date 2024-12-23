using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lighting : MonoBehaviour
{
    [SerializeField] Light2D lightobj;

    public float lightMax = 0.7f;
    public float lightMin = 0.3f;

    private float interval;
    public float timegap;
    private bool lightup;

    private void Start(){
        lightup=true;
        lightobj.falloffIntensity=lightMin;
        interval = lightMax-lightMin;
    }

    private void Update(){
        //Debug.Log(lightup);
        if(lightup){
            lightobj.falloffIntensity+=Time.deltaTime*interval/timegap;
            if(lightobj.falloffIntensity>=lightMax){
                lightup=false;
            }
        }
        else{
            lightobj.falloffIntensity-=Time.deltaTime*interval/timegap;
            if(lightobj.falloffIntensity<=lightMin){
                lightup=true;
            }
        }
    }

}
