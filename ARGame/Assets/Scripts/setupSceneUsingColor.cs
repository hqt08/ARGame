using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class setupSceneUsingColor : MonoBehaviour {

	public Camera camera;
	public Color thresholdColor = new Color(0.3f,0.3f,0.3f);
	public GameObject player;

	Texture2D overlayTexture;
	private RaycastHit mHit;
	public Text text;

	Vector3 mpos;

	// Use this for initialization
	void Start () {
	
	}

	void Update() {
//		#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_EDITOR
//		if (Input.GetMouseButtonDown(1)) { GetPixelColor(Input.mousePosition);}
//		#elif UNITY_ANDROID || UNITY_IPHONE
//		if (Input.touchCount > 0) { GetPixelColor(Input.GetTouch(0).position);}
//		#endif

	}

	void GetPixelColor(Vector3 screenposition) {
		mpos = screenposition;
		//var ray = Camera.main.ScreenPointToRay (mpos);
		StartCoroutine(setupSceneBasedOnColorDetection());
	}

	IEnumerator setupSceneBasedOnColorDetection() {
		// Make a new texture of the right size and
		// read the camera image into it.
		var tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		yield return new WaitForEndOfFrame();
		tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		tex.Apply();
		
		Color pixelColor = tex.GetPixel( (int) mpos.x, (int) mpos.y );
		//print(bla.ToHexStringRGBA());
		text.text = pixelColor.ToHexStringRGBA();

		// Check if the player is indeed on a dark surface, else fall off the world
		if (pixelColor != Color.green) { //(new Vector3(pixelColor.r, pixelColor.g, pixelColor.b) - new Vector3(0,1,0)).magnitude < 0.1
			player.SendMessage("FallOffTheWorld");
		} else {
			player.SendMessage("AlrightInTheWorld");
		}
	}

}
	