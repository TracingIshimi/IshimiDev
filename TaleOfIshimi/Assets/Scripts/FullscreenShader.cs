using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FullscreenShader : MonoBehaviour
{
    public Material fullscreenMaterial; // Shader Graph를 적용할 Material
    private bool isEffectActive = false; // 효과 활성화 여부
    private RenderTexture tempRenderTexture;

    public void SetEff(bool eff){
        isEffectActive = eff;
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (isEffectActive && fullscreenMaterial != null)
        {
            // 임시 렌더 텍스처 생성
            if (tempRenderTexture == null || tempRenderTexture.width != source.width || tempRenderTexture.height != source.height)
            {
                if (tempRenderTexture != null)
                {
                    tempRenderTexture.Release();
                }
                tempRenderTexture = new RenderTexture(source.width, source.height, 0);
            }

            // Shader Graph가 적용된 머티리얼을 사용하여 화면 렌더링
            Graphics.Blit(source, tempRenderTexture, fullscreenMaterial);
            Graphics.Blit(tempRenderTexture, destination);
        }
        else
        {
            // 원본 화면 유지
            Graphics.Blit(source, destination);
        }
    }
}
