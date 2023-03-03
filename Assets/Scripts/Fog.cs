using System;
using UnityEngine;

public class Fog : MonoBehaviour
{
    [SerializeField] private Material fogMaterial;
    [SerializeField] private UnityEngine.Camera mainCamera;

    [HideInInspector]
    public bool doFog;

    private void Awake()
    {
        doFog = true;
        mainCamera.depthTextureMode = DepthTextureMode.Depth;
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (doFog)
        {
            Graphics.Blit(src, dest, fogMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
