using UnityEngine;
using System.Collections;

public class CameraRenderEffect : MonoBehaviour {

	public Color thresholdColor = new Color(0.3f,0.3f,0.3f,1f);
	public Color shadingColor = new Color(0f,1f,0f,1f);
	private Material mat;

	// Called by the camera to apply the image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination){

		if (!mat) {
			// Use the color detection shader
			var shader = Shader.Find ("Custom/ColorDetect");
			mat = new Material (shader);
			mat.hideFlags = HideFlags.HideAndDontSave;

			// Set up threshold and shading colors
			mat.SetColor("_ThresholdColor", thresholdColor);
			mat.SetColor("_ShadingColor", shadingColor);
		}

		//mat is the material containing your shader
		Graphics.Blit(source,destination,mat);
	}

}
