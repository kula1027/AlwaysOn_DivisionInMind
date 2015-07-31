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
		TestClose ();
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

	private void TestClose(){
		if (Input.GetButtonDown ("Fire1"))
			hand_R.CloseAll ();
	}
}
