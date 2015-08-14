using UnityEngine;
using System.Collections;

public class Scene4_Main : MonoBehaviour {


	void Start () {
		PhotonNetwork.isMessageQueueRunning = true;
		if(Application.platform != RuntimePlatform.Android){
			CreateShip();
		}

	}

	void Update () {
	
	}

	
	public void CreateShip(){
		PhotonNetwork.Instantiate("4_Racing/Ship", new Vector3(PhotonNetwork.player.ID*3, 0, 0), Quaternion.identity, 0);
	}

}
