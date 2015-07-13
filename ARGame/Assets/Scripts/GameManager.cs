using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject WinScreen;
	public GameObject LoseScreen;
	public Text timer;
	public Text info;
	public AudioSource audioSource;
	public AudioClip audioClipWin,audioClipLose;

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
		if (!done) {
			RestartSound(audioClipWin);
			done = true;
			WinScreen.SetActive(true);
			info.text = "Press Space to Restart...";
		}
	}

	void Lose() {
		if (!done) {
			RestartSound(audioClipLose);
			done = true;
			LoseScreen.SetActive(true);
			info.text = "Press Space to Restart...";
		}
	}

	void RestartSound(AudioClip audioClip) {
		audioSource.Stop();
		audioSource.clip = audioClip;
		audioSource.loop = false;
		audioSource.volume = 0.8f;
		audioSource.Play();
	}
}
