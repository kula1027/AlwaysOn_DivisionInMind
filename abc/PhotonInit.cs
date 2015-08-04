using UnityEngine;
using System.Collections;

public class PhotonInit : MonoBehaviour {

	public string version = "v1.0";
	public int sendRateOnSerialize = 10;

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
		if(Application.platform == RuntimePlatform.Android){
			StartCoroutine(this.CreateTank());
		}
	}

	IEnumerator CreateTank(){
		float pos = Random.Range(-3f, 3f);
		PhotonNetwork.Instantiate("Hand", new Vector3(pos, 0, pos), Quaternion.identity, 0);
		yield return null;
	}

}
