using UnityEngine;
using System.Collections;

public class PhotonInit : MonoBehaviour {

	public string version = "v1.0";
	public int sendRateOnSerialize = 10;
	private bool ready = false;

	void Awake(){
		DontDestroyOnLoad(this);
	}

	void Start(){
		PhotonNetwork.ConnectUsingSettings(version);
		PhotonNetwork.sendRateOnSerialize = sendRateOnSerialize;
	}

	void OnGUI(){
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

	void OnJoinedLobby(){
		Debug.Log("Enter Lobby");
		ready = true;
		if(Application.platform != RuntimePlatform.Android){
			PhotonNetwork.JoinRandomRoom();
		}
	}

	void OnPhotonRandomJoinFailed(){
		Debug.Log("No rooms !");
		PhotonNetwork.CreateRoom("MyRoom", true, true, 20);
	}

	public void OnJoinRandomRoom(){
		PhotonNetwork.player.name = GameObject.Find("GameMain").GetComponent<GameMain>().GetID();
		PhotonNetwork.JoinRandomRoom();

	}

	void OnJoinedRoom(){
		Debug.Log("Enter Room");
		if(Application.platform == RuntimePlatform.Android){
			GameObject.Find("Canvas").GetComponent<Scene3_Main>().CreateController();
		}
	}
	
	public bool GetReady(){
		return ready;
	}

}
