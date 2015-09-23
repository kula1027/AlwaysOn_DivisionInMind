using UnityEngine;
using System.Collections;

public class MyInput : MonoBehaviour {
	
	public enum KeyCode{R1, R2, L1, L2};

	private static bool r1, r2, l1, l2;
	private static bool r1_Down, r2_Down, l1_Down, l2_Down;
	private static bool r1_Up, r2_Up, l1_Up, l2_Up;

	void Start () {
	
	}

	void Update () {
	
	}

	void LateUpdate(){
		if(r1_Down == true){	r1_Down = false;		}
		if(r2_Down == true){	r2_Down = false;		}
		if(l1_Down == true){	l1_Down = false;		}
		if(l2_Down == true){	l2_Down = false;		}

		if(r1 == true){				r1 = false;		}
		if(r2 == true){				r2 = false;		}
		if(l1 == true){				l1 = false;		}
		if(l2 == true){				l2 = false;		}

		if(r1_Up == true){	r1_Up = false;		}
		if(r2_Up == true){	r2_Up = false;		}
		if(l1_Up == true){	l1_Up = false;		}
		if(l2_Up == true){	l2_Up = false;		}
	}

	public static bool GetKey(KeyCode key){
		switch(key){
		case KeyCode.R1:
			return r1;
			break;
		case KeyCode.R2:
			return r2;
			break;
		case KeyCode.L1:
			return l1;
			break;
		case KeyCode.L2:
			return l2;
			break;
		}

		return false;
	}

	private void SetKeyUp(KeyCode key){
		switch(key){
		case KeyCode.R1:
			r1 = false;
			r1_Up = true;
			break;
		case KeyCode.R2:
			r2 = false;
			r2_Up = true;
			break;
		case KeyCode.L1:
			l1 = false;
			l1_Up = true;
			break;
		case KeyCode.L2:
			l2 = false;
			l2_Up = true;
			break;
		}
	}

	private void SetKeyDown(KeyCode key){
		switch(key){
		case KeyCode.R1:
			r1 = true;
			r1_Down = true;
			break;
		case KeyCode.R2:
			r2 = true;
			r2_Down = true;
			break;
		case KeyCode.L1:
			l1 = true;
			l1_Down = true;
			break;
		case KeyCode.L2:
			l2 = true;
			l2_Down = true;
			break;
		}
	}

	public static bool GetKeyDown(KeyCode key){
		switch(key){
		case KeyCode.R1:
			return r1_Down;
			break;
		case KeyCode.R2:
			return r2_Down;
			break;
		case KeyCode.L1:
			return l1_Down;
			break;
		case KeyCode.L2:
			return l2_Down;
			break;
		}
		return false;
	}

	public static bool GetKeyUp(KeyCode key){
		switch(key){
		case KeyCode.R1:
			return r1_Up;
			break;
		case KeyCode.R2:
			return r2_Up;
			break;
		case KeyCode.L1:
			return l1_Up;
			break;
		case KeyCode.L2:
			return l2_Up;
			break;
		}
		return false;
	}
}
