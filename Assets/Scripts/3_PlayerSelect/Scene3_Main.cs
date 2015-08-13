using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scene3_Main : MonoBehaviour {
	
	public void OnButtonID(InputField input){
		if(GameObject.Find("PhotonInit").GetComponent<PhotonInit>().GetReady()){
			GameObject.Find("GameMain").GetComponent<GameMain>().SetID(input.text);
			if(Application.platform == RuntimePlatform.Android){
				CreateController();
			}
		}
	}

	private void CreateController(){
		PhotonNetwork.Instantiate("Controller", new Vector3(0, 0, 0), Quaternion.identity, 0);
	}
}
