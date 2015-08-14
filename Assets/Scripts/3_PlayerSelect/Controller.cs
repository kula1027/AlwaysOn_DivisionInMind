using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	private PhotonView pv;
	private Gyroscope gyro;
	private Quaternion curRot = Quaternion.identity;
	
	void Start () {
		pv = transform.GetComponent<PhotonView>();
		pv.observed = this;
		transform.parent = GameObject.Find("GameMain").transform;
		if(pv.isMine){
			if(Application.platform == RuntimePlatform.Android){
				gyro = Input.gyro;
				gyro.enabled = true;
			}
		}
		curRot = transform.rotation;
	}
	
	void Update () {
		if(pv.isMine){
			transform.rotation = GyroConvert(Input.gyro.attitude);
		}else{
			transform.rotation = curRot;
		}
	}
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if(stream.isWriting){
			stream.SendNext(transform.rotation);
		}else{
			curRot = (Quaternion)stream.ReceiveNext();
		}
	}

	Quaternion GyroConvert(Quaternion q){
		return Quaternion.Euler(90, 0, 0)*(new Quaternion(-q.x, -q.y, q.z, q.w));
	}
}
