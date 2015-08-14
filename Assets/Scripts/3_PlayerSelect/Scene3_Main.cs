using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scene3_Main : MonoBehaviour {
	
	public void OnButtonID(InputField input){
		if(GameObject.Find("GameMain").GetComponent<PhotonInit>().GetReady()){
			GameObject.Find("GameMain").GetComponent<GameMain>().SetID(input.text);
			GameObject.Find("GameMain").GetComponent<PhotonInit>().OnJoinRandomRoom();

		}
	}

	public void OnButtonReady(){
		if(Application.platform != RuntimePlatform.Android){
			if(GameObject.Find("GameMain").GetComponent<GameMain>().findTarget){
				GameObject.Find("GameMain").GetComponent<GameMain>().ready = true;
				transform.GetComponent<PhotonView>().RPC("GoNextScene", PhotonTargets.All, null);
				//GoNextScene();
			}
		}
	}
	
	public void CreateController(){
		PhotonNetwork.Instantiate("3_PlayerSelect/Controller", new Vector3(0, 0, 0), Quaternion.identity, 0);
	}

	[PunRPC]
	void GoNextScene(){
		PhotonNetwork.isMessageQueueRunning = false;
		Application.LoadLevel("4_Racing");
	}
}
