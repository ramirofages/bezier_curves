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
       
    }



    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        cam.RenderWithShader(replacementShader, "MyTag");
        mergeTextures.SetTexture("_GloomTexture", renderTexture);


        Graphics.Blit(source, destination, mergeTextures);

    }
}
