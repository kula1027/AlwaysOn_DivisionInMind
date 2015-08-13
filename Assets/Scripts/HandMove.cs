using UnityEngine;
using System.Collections;

public class HandMove : MonoBehaviour {


	public Vector3 input;
	public float speed = 5;
	private PhotonView pv;
	private Gyroscope gyro;
	private Vector3 acc;


	private Vector3 curPos = Vector3.zero;
	private Quaternion curRot = Quaternion.identity;

	// Use this for initialization
	void Start () {
		pv = transform.GetComponent<PhotonView>();
		pv.observed = this;
		//pv.synchronization = ViewSynchronization.Unreliable;
		if(pv.isMine){
			GameObject.Find("MainCube").GetComponent<MainManagement>().SetMyHand(gameObject);
			if(Application.platform == RuntimePlatform.Android){
				gyro = Input.gyro;
				gyro.enabled = true;
			}
		}

		curPos = transform.position;
		curRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if(pv.isMine){
			acc = Input.acceleration;
			Vector3 acc2 = (acc  - Input.gyro.gravity);
			//acc2 *= 1.2f;
			//acc2 = new Vector3(acc2.x*acc2.x*buho(acc2.x), acc2.y*acc2.y*buho(acc2.y), acc2.z*acc2.z*buho(acc2.z));
			transform.rotation = GyroConvert(Input.gyro.attitude);
			//transform.GetComponent<Rigidbody>().velocity = (Input.acceleration  - Input.gyro.gravity)*100;
			//transform.position+= acc2*0.4f;
			//MoveHand(acc2*0.5f);
			transform.position += CutLeastValue(acc2)*3;
		}else{
			transform.rotation = Quaternion.Lerp(transform.rotation, curRot, Time.deltaTime*3.0f);
			transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime*3.0f);
		}
	}

	Vector3 CutLeastValue(Vector3 v){
		float x, y, z;
		x = v.x;
		y = v.y;
		z = v.z;
		if(-1<x && x<1){x = 0;}
		if(-1<y && y<1){y = 0;}
		if(-1<z && z<1){z = 0;}
		return new Vector3(x, y, z);
	}

	void MoveHand(Vector3 pos){
		transform.position += (transform.right * pos.x);
		transform.position += (transform.up * pos.y);
		transform.position += (transform.forward * pos.z);
	}

	int buho(float a){
		if(a<0){return -1;}
		else{return 1;}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if(stream.isWriting){
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}else{
			curPos = (Vector3)stream.ReceiveNext();
			curRot = (Quaternion)stream.ReceiveNext();
		}
	}

	void OnTriggerEnter(Collider coll){

	}

	void OnGUI(){
		GUILayout.Label("");
		GUILayout.Label("");
		GUILayout.Label("Acc : " + acc);
		GUILayout.Label("gravity : " + Input.gyro.gravity);
		GUILayout.Label("rotation : " + transform.rotation.eulerAngles);
	}

	Quaternion GyroConvert(Quaternion q){
		return Quaternion.Euler(90, 0, 0)*(new Quaternion(-q.x, -q.y, q.z, q.w));
	}

}