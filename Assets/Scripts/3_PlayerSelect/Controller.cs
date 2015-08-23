using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public bool findOwner = false;

	public float initY = 0;
	
	public bool engine = false;

	private PhotonView pv;
	private Gyroscope gyro;
	private Quaternion curRot = Quaternion.identity;

	void Start () {
		pv = transform.GetComponent<PhotonView>();
		pv.observed = this;
		transform.parent = GameObject.Find("GameMain").transform;
		if(pv.isMine){
			GameObject.Find("GameMain").GetComponent<GameMain>().SetController(gameObject);
			if(Application.platform == RuntimePlatform.Android){
				gyro = Input.gyro;
				gyro.enabled = true;
			}
		}
		curRot = transform.rotation;
	}

	public void SettingVecter(){
		initY = -transform.rotation.eulerAngles.y;
	}
	
	void Update () {
		if(pv.isMine){
			//transform.rotation = GyroConvert(Input.gyro.attitude);
			transform.rotation = GyroConvert2(Input.gyro.attitude);
			//transform.Rotate(new Vector3(0,90,0));
		}else{
			transform.rotation = curRot;
		}
	}
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if(stream.isWriting){
			//stream.SendNext(new Quaternion(ConvertTo(transform.rotation.x-initRot.x, w), ConvertTo(transform.rotation.y-initRot.y,w), ConvertTo(transform.rotation.z-initRot.z,w), 1));
			//stream.SendNext(transform.rotation);
			stream.SendNext(transform.rotation);
		}else{
			curRot = ((Quaternion)stream.ReceiveNext());
		}
	}

	Quaternion GyroConvert(Quaternion q){
		return Quaternion.Euler(90, 0, 0)*(new Quaternion(-q.x, -q.y, q.z, q.w));
	}

	Quaternion GyroConvert2(Quaternion q){
		return Quaternion.Euler(0, initY, 0)*(new Quaternion(-q.x, -q.z, -q.y, q.w));
		//return Quaternion.Euler(90, 0, 0)*(new Quaternion(-q.x, -q.y, q.z, q.w));
	}
	
	float ConvertTo(float xyz, float w){
		return xyz/w;
	}

	public void SetEngine(bool on){
		pv.RPC("RPC_SetEngine", PhotonTargets.Others, on);
		engine = on;
	}

	public bool GetEngine(){
		return engine;
	}

	[PunRPC]
	void RPC_SetEngine(bool on){
		engine = on;
	}
	
}
