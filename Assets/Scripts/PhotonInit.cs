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
		PhotonNetwork.JoinRandomRoom();
	}

	void OnPhotonRandomJoinFailed(){
		Debug.Log("No rooms !");
		PhotonNetwork.CreateRoom("MyRoom", true, true, 20);
	}

	void OnJoinedRoom(){
		Debug.Log("Enter Room");
		ready = true;
	}
	
	public bool GetReady(){
		return ready;
	}

}
