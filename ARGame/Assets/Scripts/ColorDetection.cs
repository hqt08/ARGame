using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorDetection : MonoBehaviour {

	public Camera camera;
	public Color shadingColor = Color.green;
	public GameObject player;
	
	private RaycastHit mHit; // buffer for raycast hit
	private Vector3 mpos; // screen position (to test pixel)
	private Texture2D tex; // texture2D buffer

	// Use this for initialization
	void Start () {
		tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
	}

	/// <summary>
	/// Gets the color of the selected pixel.
	/// </summary>
	void GetPixelColor(Vector3 screenposition) {
		mpos = screenposition;
		StartCoroutine(setupSceneBasedOnColorDetection());
	}

	IEnumerator setupSceneBasedOnColorDetection() {
		// Wait for end of frame, make sure all rendering done
		yield return new WaitForEndOfFrame();
		// Use texture to read the selected pixel into it.
		tex.ReadPixels(new Rect((int)mpos.x, (int)mpos.y, 1, 1), 0, 0);
		tex.Apply();

		// Get pixel color from selected pixel
		Color pixelColor = tex.GetPixel(0, 0);

		// Check if the player is indeed on a dark surface, else fall off the world
		if (pixelColor != shadingColor) {
			player.SendMessage("FallOffTheWorld");
		} 
	}

}
	