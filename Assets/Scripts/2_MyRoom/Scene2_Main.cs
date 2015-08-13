using UnityEngine;
using System.Collections;

public class Scene2_Main : MonoBehaviour {

	public int targetShipNumber = 0;
	public GameObject cam;

	void Start () {
	
	}

	void Update () {
		switch(targetShipNumber){
		case 0:
			cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Quaternion.Euler(new Vector3(5.8f, -56.43f, 0)), 0.1f);
			break;
		case 1:
			cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Quaternion.Euler(new Vector3(5.8f, 0, 0)), 0.1f);
			break;
		case 2:
			cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Quaternion.Euler(new Vector3(5.8f, 44.44f, 0)), 0.1f);
			break;
		}
	}

	public void OnButtonShift(bool right){
		if(right){
			if(targetShipNumber<2){
				targetShipNumber ++;
			}
		}else{
			if(0<targetShipNumber){
				targetShipNumber--;
			}
		}
	}

	public void OnButtonStart(){
		Application.LoadLevel("3_PlayerSelect");
	}
}
