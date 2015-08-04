using UnityEngine;
using System.Collections;

public class MainManagement : MonoBehaviour {

	private GameObject myHand;

	public void SetMyHand(GameObject hand){
		myHand = hand;
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}
}
