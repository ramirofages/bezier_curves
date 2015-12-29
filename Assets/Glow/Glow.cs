using UnityEngine;
using System.Collections;

public class Glow : MonoBehaviour
{

    public Material mergeTextures;
    public RenderTexture renderTexture;
  
    public Shader replacementShader;
    public Camera cam;
    void Awake()
    {
        
        renderTexture = new RenderTexture(Screen.width / 4, Screen.height / 4, 24, RenderTextureFormat.ARGB32);
        RenderTexture tempRenderTexture = new RenderTexture(Screen.width / 4, Screen.height / 4, 24, RenderTextureFormat.ARGB32);
        renderTexture.filterMode = FilterMode.Trilinear;
        tempRenderTexture.filterMode = FilterMode.Trilinear;

        GlowMapGenerator glowMapGenerator = cam.GetComponent<GlowMapGenerator>();
        glowMapGenerator.tempRenderTexture = tempRenderTexture;
        glowMapGenerator.renderTexture = renderTexture;

        cam.GetComponent<Camera>().targetTexture = renderTexture;
    }



    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        cam.RenderWithShader(replacementShader, "MyTag");
        mergeTextures.SetTexture("_GloomTexture", renderTexture);


        Graphics.Blit(source, destination, mergeTextures);

    }
}
