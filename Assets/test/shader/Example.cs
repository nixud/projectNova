using UnityEngine;


[ExecuteInEditMode]
public class Example : MonoBehaviour
{
    public Material material;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // Copy the source Render Texture to the destination,
        // applying the material along the way.
        Graphics.Blit(source, destination, material);
    }
}