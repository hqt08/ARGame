using UnityEngine;
using System.Collections;

public class GoalController : MonoBehaviour {
	public GameObject gameManager;
	public Vector3 position;

	// Use this for initialization
	void Start () {
		transform.position = position; 
	}

	void OnTriggerEnter(Collider obj) {
		if (obj.gameObject.tag == "Player") {
			Debug.Log("Won!");
			gameManager.SendMessage("Win");
		}
	}
}
