using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	
	private PhotonView pv;
	private Vector3 curPos = Vector3.zero;
	private Quaternion curRot = Quaternion.identity;
	private GameMain gameMain;
	
	void Start () {
		pv = transform.GetComponent<PhotonView>();
		gameMain = GameObject.Find("GameMain").GetComponent<GameMain>();
		pv.observed = this;
		if(pv.isMine){
			transform.FindChild("Camera").gameObject.SetActive(true);
		}
		transform.parent = GameObject.Find("GameMain").transform;
		curRot = transform.rotation;
	}
	
	void Update () {
		if(pv.isMine){
			curRot = gameMain.GetMyController().transform.rotation;
			transform.rotation = Quaternion.Lerp(transform.rotation, curRot, Time.deltaTime*5f);
		}else{
			transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime*5f);
			transform.rotation = Quaternion.Lerp(transform.rotation, curRot, Time.deltaTime*5f);
		}
	}
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if(stream.isWriting){
			stream.SendNext(transform.position);
			stream.SendNext(gameMain.GetMyController().transform.rotation);
		}else{
			curPos = (Vector3)stream.ReceiveNext();
			curRot = (Quaternion)stream.ReceiveNext();
		}
	}
}
