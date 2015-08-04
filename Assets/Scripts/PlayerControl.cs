using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	private CharacterController cCtrl;
	private Transform cam;
	private Hand hand_R;

	public float moveSpeed;
	public float rotSpeed;

	bool isMovable;

	void Start () {
		cCtrl = GetComponent<CharacterController> ();
		cam = transform.FindChild ("Main Camera");
		hand_R = transform.FindChild ("Right Hand").GetComponent<Hand> ();

		isMovable = true;
	}



	void Update () {
		if(isMovable)Move ();
		HeadTurn ();
		GetInput ();
	}

	private void Move(){
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		
		Vector3 moveVector = transform.forward * v + transform.right * h;
		cCtrl.Move(moveVector * moveSpeed);
	}

	private void HeadTurn(){
		float mX = Input.GetAxis ("Mouse X");
		float mY = Input.GetAxis("Mouse Y");
		
		transform.Rotate(new Vector3(0, mX * rotSpeed, 0));
		cam.Rotate(new Vector3(-mY * rotSpeed, 0, 0));
	}

	private void GetInput(){
		if (Input.GetKeyDown (KeyCode.Keypad7))
			hand_R.CloseAll ();
		if (Input.GetKeyDown (KeyCode.Keypad8))
			hand_R.OpenAll ();

		if (Input.GetKeyDown (KeyCode.Keypad1))
			hand_R.OpenAt (0);
		if (Input.GetKeyDown (KeyCode.Keypad2))
			hand_R.OpenAt (1);
		if (Input.GetKeyDown (KeyCode.Keypad3))
			hand_R.OpenAt (2);
		if (Input.GetKeyDown (KeyCode.Keypad4))
			hand_R.OpenAt (3);
		if (Input.GetKeyDown (KeyCode.Keypad5))
			hand_R.OpenAt (4);

		if (Input.GetKeyDown (KeyCode.Alpha1))
			hand_R.CloseAt (0);
		if (Input.GetKeyDown (KeyCode.Alpha2))
			hand_R.CloseAt (1);
		if (Input.GetKeyDown (KeyCode.Alpha3))
			hand_R.CloseAt (2);
		if (Input.GetKeyDown (KeyCode.Alpha4))
			hand_R.CloseAt (3);
		if (Input.GetKeyDown (KeyCode.Alpha5))
			hand_R.CloseAt (4);
	}
}
