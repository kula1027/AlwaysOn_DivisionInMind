using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public bool findOwner = false;

	public float initY = 0;
	
	public bool engine = false;

	public bool L1, L2, R1, R2;

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

	public void SetL1(bool on){
		pv.RPC("RPC_SetL1", PhotonTargets.Others, on);
		L1 = on;
	}
	public void SetL2(bool on){
		pv.RPC("RPC_SetL2", PhotonTargets.Others, on);
		L2 = on;
	}
	public void SetR1(bool on){
		pv.RPC("RPC_SetR1", PhotonTargets.Others, on);
		R1 = on;
	}
	public void SetR2(bool on){
		pv.RPC("RPC_SetR2", PhotonTargets.Others, on);
		R2 = on;
	}

	public bool GetEngine(){
		return engine;
	}

	[PunRPC]
	void RPC_SetEngine(bool on){
		engine = on;
	}

	[PunRPC]
	void RPC_SetR1(bool on){
		R1 = on;
		if(on){
			GameObject.Find("MyInput").SendMessage("SetKeyUp", MyInput.KeyCode.R1);
		}else{
			GameObject.Find("MyInput").SendMessage("SetKeyDown", MyInput.KeyCode.R1);
		}
	}
	[PunRPC]
	void RPC_SetR2(bool on){
		R2 = on;
		if(on){
			GameObject.Find("MyInput").SendMessage("SetKeyUp", MyInput.KeyCode.R2);
		}else{
			GameObject.Find("MyInput").SendMessage("SetKeyDown", MyInput.KeyCode.R2);
		}
	}
	[PunRPC]
	void RPC_SetL1(bool on){
		L1 = on;
		if(on){
			GameObject.Find("MyInput").SendMessage("SetKeyUp", MyInput.KeyCode.L1);
		}else{
			GameObject.Find("MyInput").SendMessage("SetKeyDown", MyInput.KeyCode.L1);
		}
	}
	[PunRPC]
	void RPC_SetL2(bool on){
		L2 = on;
		if(on){
			GameObject.Find("MyInput").SendMessage("SetKeyUp", MyInput.KeyCode.L2);
		}else{
			GameObject.Find("MyInput").SendMessage("SetKeyDown", MyInput.KeyCode.L2);
		}
	}
	
}
