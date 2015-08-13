using UnityEngine;
using System.Collections;

public class Scene2_ShipMoving : MonoBehaviour {

	private float timer = 0;
	private Vector3 firstPos;

	void Start(){
		firstPos = transform.position;
	}

	void Update () {
		timer += Time.deltaTime;
		if(10000000< timer){
			timer = 0;
		}
		transform.position = new Vector3(0, Mathf.Sin(timer)*1, 0) + firstPos;
		//transform.Rotate(new Vector3(0, Time.deltaTime * 5, 0));
	}
}
