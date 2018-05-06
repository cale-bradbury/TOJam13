/** \addtogroup PostFX 
*  @{
*/

using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Camp Cult/Color/Neet")]
public class Neet : ImageEffectBase {
    public float count = 24f;          //how many repititions to have
    public Vector4 center = new Vector4(.5f, .5f, .5f, .5f);//center of the effect
    public float mul = .99f;        //how far it shifts the image, probibly keep between .9 and 1.0
    public float falloffStart = .02f;   //where the falloff of the effect starts as fracion of screen size
    public float falloffEnd = .3f;      //where the falloff of the effect ends as fracion of screen size
    public float alpha = 1f;       //makes the coloration less visible, really high numbers are very glitch looking, but looks best at 1. imo
    public float phase = 0f;       //rotate the color

    // Called by camera to apply image effect
    void OnRenderImage (RenderTexture source, RenderTexture destination) {
		material.SetVector("center", center);
        material.SetFloat("count", count);
        material.SetFloat("mul", mul);
        material.SetFloat("falloffStart", falloffStart);
        material.SetFloat("falloffEnd", falloffEnd);
        material.SetFloat("alpha", alpha);
        material.SetFloat("phase", phase);

        Graphics.Blit (source, destination, material);
	}
}


/** @}*/