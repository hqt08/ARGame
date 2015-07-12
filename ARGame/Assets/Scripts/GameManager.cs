using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject WinScreen;
	public GameObject LoseScreen;
	public Text timer;
	public Text info;
	public AudioSource audioSource;

	private float time = 0f;
	public float totaltime = 1000f;
	public bool done = false;

	void Update() {
		if (!done) {
			// Update Timing;
			if (Time.timeSinceLevelLoad > 3.3f && time < totaltime) {
				time += Time.deltaTime;
				if (time >= totaltime) time = totaltime; 
				timer.text = "Time Left: " + (totaltime - time).ToString("0.00");
			}

			if (time >= totaltime) {
				Lose ();
			}
		} else {
			if (Input.GetKey("space")) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	void Win() {
		RestartSound();
		done = true;
		WinScreen.SetActive(true);
		info.text = "Press Space to Restart...";
	}

	void Lose() {
		RestartSound();
		audioSource.pitch = 0.5f;
		done = true;
		LoseScreen.SetActive(true);
		info.text = "Press Space to Restart...";
	}

	void RestartSound() {
		audioSource.Stop();
		audioSource.Play();
	}
}
