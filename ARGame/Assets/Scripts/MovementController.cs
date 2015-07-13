using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {
	public GameObject deviceCamera;
	public GameObject gameManager;
	public GameObject goalPrefab;
	public Vector3 offset = new Vector3(0,10,0);
	public int screenOffset = 50;

	private GameObject goal; // ball goal gameobject reference
	private bool goalSetup; // has the goal already been setup?
	private RaycastHit mHit; // raycast buffer

	
	void Update () {
		// Handle color detection events only if the GameObject is currently
		// is being tracked and game not yet over
		if (GameObject.Find("metaioTracker").GetComponent<metaioTracker>().isTracked && !gameManager.GetComponent<GameManager>().done) {
			Vector3 footPositionOnScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position) - offset;
			deviceCamera.SendMessage("GetPixelColor", footPositionOnScreen);
			if (!goalSetup) {
				SetupGoal(gameObject.transform.position);
				goalSetup = true;
			}
		}
	}

	/// <summary>
	/// Simulate player falling the off the world.
	/// </summary>
	void FallOffTheWorld() {
		Vector3 downwards = - gameObject.transform.up.normalized;
		gameObject.transform.position += downwards * 20f;
		// Lose game if player has fallen out of view
		if (!GetComponentInChildren<Renderer>().isVisible) gameManager.SendMessage("Lose");
	}

	/// <summary>
	/// Sets up the goal based on the furthest corner point.
	/// </summary>
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
		goal = (GameObject) Instantiate(goalPrefab, goalPos, Quaternion.identity);
		goal.AddComponent<GoalController>();
		goal.GetComponent<GoalController>().gameManager = gameManager;
		goal.GetComponent<GoalController>().position = goalPos;
		goal.transform.parent = GameObject.Find("metaioTracker").transform;
	}

	/// <summary>
	/// For recalculating the goal position
	/// </summary>
	public void ResetGoal() {
		GameObject.Destroy(goal);
		goalSetup = false;
	}
}
