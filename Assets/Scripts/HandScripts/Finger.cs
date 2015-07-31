using UnityEngine;
using System.Collections;

public class Finger: MonoBehaviour{
	private Transform point_low;
	private Transform point_mid;
	private Transform point_hi;
	private bool isThumb;

	private float rotSpeed;
	private bool isClosed;

	void Start(){
		point_low = transform.FindChild ("low");
		if (transform.FindChild ("low").FindChild ("mid") == null) {
			point_hi = transform.FindChild ("low").FindChild ("hi");
			isThumb = true;
		} else {
			point_mid = transform.FindChild ("low").FindChild ("mid");
			point_hi = transform.FindChild ("low").FindChild ("mid").FindChild("hi");
			isThumb = false;
		}


		rotSpeed = 100f;
		isClosed = false;
	}

	public void Close(){
		if(!isClosed)StartCoroutine ("CloseStart");
	}

	private IEnumerator CloseStart(){
		isClosed = true;
		while (true) {
			point_low.Rotate (new Vector3 (-Time.deltaTime * rotSpeed, 0, 0));
			if(!isThumb)point_mid.Rotate (new Vector3 (-Time.deltaTime * rotSpeed * 1.2f, 0, 0));
			point_hi.Rotate (new Vector3 (-Time.deltaTime * rotSpeed, 0, 0));
			if(isThumb && point_low.localEulerAngles.x < 310)yield break;
			if(point_low.localEulerAngles.x < 290)yield break;
			yield return null;
		}
	}
}
