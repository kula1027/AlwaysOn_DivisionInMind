using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {
	Finger f_thumb;
	Finger f_point;
	Finger f_middle;
	Finger f_ring;
	Finger f_little;

	void Awake () {
		Init_Finger ();
	}

	private void Init_Finger(){
		f_thumb = transform.FindChild ("finger_thumb").GetComponent<Finger> ();
		f_point = transform.FindChild ("finger_point").GetComponent<Finger> ();
		f_middle = transform.FindChild ("finger_middle").GetComponent<Finger> ();
		f_ring = transform.FindChild ("finger_ring").GetComponent<Finger> ();
		f_little = transform.FindChild ("finger_little").GetComponent<Finger> ();
	}

	public void CloseAll(){
		f_thumb.Close ();
		f_point.Close ();
		f_middle.Close ();
		f_ring.Close ();
		f_little.Close ();
	}
	public void OpenAll(){
		f_thumb.Open ();
		f_point.Open ();
		f_middle.Open ();
		f_ring.Open ();
		f_little.Open ();
	}

	public void CloseAt(int index){//0번째 -> 엄지 4번째 -> 새끼
		switch (index) {
		case 0:
			f_thumb.Close ();
			break;
		case 1:
			f_point.Close ();
			break;
		case 2:
			f_middle.Close ();
			break;
		case 3:
			f_ring.Close ();
			break;
		case 4:
			f_little.Close ();
			break;
		}
	}
	public void OpenAt(int index){//0번째 -> 엄지 4번째 -> 새끼
		switch (index) {
		case 0:
			f_thumb.Open ();
			break;
		case 1:
			f_point.Open ();
			break;
		case 2:
			f_middle.Open ();
			break;
		case 3:
			f_ring.Open ();
			break;
		case 4:
			f_little.Open ();
			break;
		}
	}
}
