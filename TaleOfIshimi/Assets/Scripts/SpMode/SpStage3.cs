using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpStage3 : SpFunc
{
    [SerializeField] FullscreenShader shader;
    protected override void SpActions(){
        shader.SetEff(true);
    }

    
}