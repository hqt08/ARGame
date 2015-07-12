using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {
	public GameObject deviceCamera;
	public GameObject gameManager;
	public Vector3 offset = new Vector3(0,10,0);

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// Handle touch or click events only if the GameObject is currently
		// being rendered
		if (GameObject.Find("metaioTracker").GetComponent<metaioTracker>().isTracked) {
			Vector3 footPositionOnScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position) - offset;
			deviceCamera.SendMessage("GetPixelColor", footPositionOnScreen);
		}
	}

	void FallOffTheWorld() {
		Debug.Log("Falling off the world");
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - Time.deltaTime * 100f, gameObject.transform.position.z);
		// Lose game if player has fallen out of view
		if (!GetComponentInChildren<Renderer>().isVisible) gameManager.SendMessage("Lose");
	}

	void AlrightInTheWorld() {
		Debug.Log("Alright in the world");
	}
}
