using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject startScreen;
	public GameObject WinScreen;
	public GameObject LoseScreen;
	public GameObject player;
	public GameObject mTracker;
	public Text timer;
	public Text info;
	public AudioSource audioSource;
	public AudioClip audioClipWin, audioClipLose, audioClipUsual;

	private float time = 0f;
	public float totaltime = 10f;
	public bool done = false;

	void Start() {
		Time.timeScale = 0f;
	}

	void Update() {
		if (!done) {
			// Update Timing;
			if (time < totaltime) {
				time += Time.deltaTime;
				if (time >= totaltime) time = totaltime; 
				timer.text = "Time Left: " + (totaltime - time).ToString("0.0");
			}

			if (time >= totaltime) {
				Lose ();
			}
		}
	}

	/// <summary>
	/// Win Game
	/// </summary>
	void Win() {
		if (!done) {
			RestartSound(audioClipWin, 0.8f);
			done = true;
			WinScreen.SetActive(true);
			WinScreen.SendMessage("StartAnim");
			info.text = "Tap button to Restart...";
			player.GetComponent<GestureHandler>().enabled = false;
		}
	}

	/// <summary>
	/// Lose Game
	/// </summary>
	void Lose() {
		if (!done) {
			RestartSound(audioClipLose, 0.8f);
			done = true;
			LoseScreen.SetActive(true);
			LoseScreen.SendMessage("StartAnim");
			info.text = "Tap button to Restart...";
			player.GetComponent<GestureHandler>().enabled = false;
		}
	}

	/// <summary>
	/// Restart Game
	/// </summary>
	void Restart() {
		done = false;
		time = 0;
		WinScreen.SetActive(false);
		LoseScreen.SetActive(false);
		RestartSound(audioClipUsual, 0.4f);
		// Reset Goal
		player.SendMessage("ResetGoal");
		// Reset Tracker
		player.transform.position = mTracker.transform.position;
		player.transform.rotation = mTracker.transform.rotation;
		info.text = "Use darker objects (tinted green) to travel and find the ball goal!";
		player.GetComponent<GestureHandler>().enabled = true;
	}

	/// <summary>
	/// Function to change sound clips
	/// </summary>
	/// <param name="audioClip">Audio clip.</param>
	/// <param name="volume">Volume.</param>
	void RestartSound(AudioClip audioClip, float volume) {
		audioSource.Stop();
		audioSource.clip = audioClip;
		audioSource.loop = false;
		audioSource.volume = volume;
		audioSource.Play();
	}

	/// <summary>
	/// Start Button Function
	/// </summary>
	public void StartButton() {
		startScreen.SetActive(false);
		Time.timeScale = 1f;
		mTracker.SetActive(true);
	}

	/// <summary>
	/// Restart Button Function
	/// </summary>
	public void RestartButton() {
		// Clear assets and restart level
		Restart ();
	}
}
