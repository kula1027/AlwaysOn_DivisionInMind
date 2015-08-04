using UnityEngine;
using System.Collections;

public class Finger: MonoBehaviour{
	private Transform point_low;
	private Transform point_mid;
	private Transform point_hi;

	private float start_low;
	private float start_mid;
	private float start_hi;

	private bool isThumb;

	private float rotSpeed;
	private bool isMoving;

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

		start_low = point_low.localEulerAngles.x;
		if(!isThumb)start_mid = point_mid.localEulerAngles.x;
		start_hi = point_hi.localEulerAngles.x;

		rotSpeed = 100f;
		isMoving = false;
	}

	public void Close(){
		if(!isMoving)StartCoroutine ("CloseStart");
	}
	public void Open(){
		if(!isMoving)StartCoroutine ("OpenStart");
	}

	private IEnumerator CloseStart(){
		isMoving = true;
		while (true) {
			point_low.Rotate (new Vector3 (-Time.deltaTime * rotSpeed, 0, 0));
			if(!isThumb)point_mid.Rotate (new Vector3 (-Time.deltaTime * rotSpeed * 1.2f, 0, 0));
			point_hi.Rotate (new Vector3 (-Time.deltaTime * rotSpeed, 0, 0));
			if(isThumb && point_low.localEulerAngles.x < 310){
				isMoving = false;
				yield break;
			}
			if(point_low.localEulerAngles.x < 290){
				isMoving = false;
				yield break;
			}
			yield return null;
		}
	}
	private IEnumerator OpenStart(){
		isMoving = true;
		while (true) {
			point_low.Rotate (new Vector3 (Time.deltaTime * rotSpeed, 0, 0));
			if(!isThumb)point_mid.Rotate (new Vector3 (Time.deltaTime * rotSpeed * 1.2f, 0, 0));
			point_hi.Rotate (new Vector3 (Time.deltaTime * rotSpeed, 0, 0));
			if(isThumb && point_low.localEulerAngles.x > start_low){
				isMoving = false;
				yield break;
			}
			if(point_low.localEulerAngles.x > start_low){
				isMoving = false;
				yield break;
			}
			yield return null;
		}
	}
}
