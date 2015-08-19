using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	
	private PhotonView pv;
	private Vector3 curPos = Vector3.zero;
	private Quaternion curRot = Quaternion.identity;
	private GameMain gameMain;

	private Vector3 firstRot;
	
	void Start () {
		pv = transform.GetComponent<PhotonView>();
		gameMain = GameObject.Find("GameMain").GetComponent<GameMain>();
		firstRot = gameMain.GetMyController().transform.rotation.eulerAngles;
		pv.observed = this;
		if(pv.isMine){
			transform.FindChild("Camera").gameObject.SetActive(true);
		}
		transform.parent = GameObject.Find("GameMain").transform;
		curRot = transform.rotation;
	}
	
	void Update () {
		if(pv.isMine){
			curRot = Quaternion.Euler(firstRot - gameMain.GetMyController().transform.rotation.eulerAngles);
			transform.Rotate(new Vector3(0,10,0)*Time.deltaTime);
			//transform.rotation = Quaternion.Lerp(transform.rotation, curRot, Time.deltaTime*5f);
			//transform.position += transform.forward;
		}else{
			transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime*5f);
			transform.rotation = Quaternion.Lerp(transform.rotation, curRot, Time.deltaTime*5f);
		}
	}
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if(stream.isWriting){
			stream.SendNext(transform.position);
			stream.SendNext(curRot);
		}else{
			curPos = (Vector3)stream.ReceiveNext();
			curRot = (Quaternion)stream.ReceiveNext();
		}
	}
}
