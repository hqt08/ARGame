using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwapImage : MonoBehaviour {
	public Sprite[] sprites;
	public float waitingTime = 0.5f;
	int i = 0;
	int n = 1;

	// Use this for initialization
	void Start () {
		n = sprites.Length;
		StartCoroutine(Swappy());
	}

	IEnumerator Swappy() {
		while (true) {
			GetComponent<Image>().sprite = sprites[i % n];
			i++;
			yield return new WaitForSeconds(waitingTime);
		}
	}
}
