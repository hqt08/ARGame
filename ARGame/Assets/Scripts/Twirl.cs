using UnityEngine;
using System.Collections;

public class Twirl : MonoBehaviour {
	public float rotationAmt = 1f;

	// Use this for initialization
	void Start () {
		iTween.RotateBy (gameObject, iTween.Hash("name","twirltween"+gameObject.name,"y", rotationAmt, "time", 5f, "looptype", iTween.LoopType.loop, "easetype", iTween.EaseType.linear));
	}

	void StartTwirl() {
		iTween.RotateBy (gameObject, iTween.Hash("name","twirltween"+gameObject.name,"y", rotationAmt, "time", 5f, "looptype", iTween.LoopType.loop, "easetype", iTween.EaseType.linear));
	}

	void StopTwirl() {
		iTween.StopByName("twirltween"+gameObject.name);
	}
}
