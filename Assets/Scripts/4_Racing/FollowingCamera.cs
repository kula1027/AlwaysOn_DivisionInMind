using UnityEngine;
using System.Collections;

public class FollowingCamera : MonoBehaviour {

	public GameObject targetShip;
	private bool findTarget = false;

	private float rotateY = 0;
	
	void Start () {
		GameObject gameMain = GameObject.Find("GameMain");
		int count = gameMain.transform.childCount;
		for(int i = 0 ; i < count ; i++ ){
			if(gameMain.transform.GetChild(i).GetComponent<Ship>()){
				if(gameMain.transform.GetChild(i).GetComponent<Ship>().pv.isMine){
					targetShip = gameMain.transform.GetChild(i).gameObject;
					findTarget = true;
				}
			}
		}
	}

	void Update () {

	}

	void LateUpdate(){
		if(findTarget){
			transform.position = targetShip.transform.position;
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0,targetShip.transform.rotation.eulerAngles.y,0)), 0.5f);
		}
	}
}
