using UnityEngine;
using System.Collections;

/// TESTING CODE...



public class FingerMovement : MonoBehaviour {
	GameObject f_point;
	GameObject f_point_low;
	GameObject f_point_mid;
	GameObject f_point_hi;

	public const float rotateSpeed = 25f;

	void Start () {
		f_point = transform.FindChild ("finger_point").gameObject;
		f_point_low = f_point.transform.FindChild("low").gameObject;
		f_point_mid = f_point_low.transform.FindChild("mid").gameObject;
		f_point_hi = f_point_mid.transform.FindChild("hi").gameObject;

		StartCoroutine("Close");
	}

	IEnumerator Close(){
		while (true) {
			f_point_low.transform.Rotate (new Vector3 (-Time.deltaTime * rotateSpeed, 0, 0));
			f_point_mid.transform.Rotate (new Vector3 (-Time.deltaTime * rotateSpeed * 1.2f, 0, 0));
			f_point_hi.transform.Rotate (new Vector3 (-Time.deltaTime * rotateSpeed, 0, 0));
			if(f_point_low.transform.localEulerAngles.x < 290)yield break;
			yield return null;
		}
	}


}
