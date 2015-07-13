using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {
	public GameObject deviceCamera;
	public GameObject gameManager;
	public GameObject goalPrefab;
	public Vector3 offset = new Vector3(0,10,0);
	public int screenOffset = 50;

	private bool goalSetup; // has the goal already been setup?
	private RaycastHit mHit; // raycast buffer

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// Handle color detection events only if the GameObject is currently
		// being rendered and is being tracked
		if (GameObject.Find("metaioTracker").GetComponent<metaioTracker>().isTracked) {
			Vector3 footPositionOnScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position) - offset;
			deviceCamera.SendMessage("GetPixelColor", footPositionOnScreen);
			if (!goalSetup) {
				SetupGoal(gameObject.transform.position);
				goalSetup = true;
			}
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

	void SetupGoal(Vector3 playerPos) {
		// Array of corner edges in field of view
		Ray[] cornerPos = new Ray[4];
		cornerPos[0] = Camera.main.ScreenPointToRay(new Vector3(screenOffset,screenOffset));
		cornerPos[1] = Camera.main.ScreenPointToRay(new Vector3(Screen.width-screenOffset,screenOffset));
		cornerPos[2] = Camera.main.ScreenPointToRay(new Vector3(screenOffset,Screen.height-screenOffset));
		cornerPos[3] = Camera.main.ScreenPointToRay(new Vector3(Screen.width-screenOffset,Screen.height-screenOffset));

		// Choose the location of the furthest edge to place goal
		Vector3 goalPos = playerPos;
		GameObject TrackerPlane = GameObject.Find("TrackerPlane");
		if (TrackerPlane == null) return;
		foreach (Ray ray in cornerPos) {
			if (Physics.Raycast(ray, out mHit, Camera.main.farClipPlane, 1<<TrackerPlane.layer)) {
				Vector3 pos = mHit.point;
				if ((pos-playerPos).sqrMagnitude > (goalPos-playerPos).sqrMagnitude) {
					goalPos = pos;
				}
			}
		}
		GameObject goal = (GameObject) Instantiate(goalPrefab, goalPos, Quaternion.identity);
		goal.AddComponent<GoalController>();
		goal.GetComponent<GoalController>().gameManager = gameManager;
		goal.GetComponent<GoalController>().position = goalPos;
		goal.transform.parent = GameObject.Find("metaioTracker").transform;
	}
}
