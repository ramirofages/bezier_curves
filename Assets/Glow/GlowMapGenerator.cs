using UnityEngine;
using System.Collections;

public class GlowMapGenerator : MonoBehaviour
{

    public Material mat;
    public RenderTexture renderTexture;
    public RenderTexture tempRenderTexture;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        

        // reducimos resolucion
        Graphics.Blit(source, tempRenderTexture);

        // horizontal
        mat.SetVector("offsets", new Vector4(1.0f / renderTexture.width, 0, 0, 0));
        Graphics.Blit(tempRenderTexture, renderTexture, mat);

        //vertical
        mat.SetVector("offsets", new Vector4(0, 1.0f  / renderTexture.height, 0, 0)); 
        Graphics.Blit(renderTexture, tempRenderTexture, mat);

       
        
        //retornamos
        Graphics.Blit(tempRenderTexture, destination);
        
    }
}
