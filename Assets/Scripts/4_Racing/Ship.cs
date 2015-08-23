using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	
	public PhotonView pv;
	private Vector3 curPos = Vector3.zero;
	private Quaternion curRot = Quaternion.identity;
	private GameMain gameMain;

	public float y = 0;
	public GameObject ship;

	public _ShipRenderer[] _renderer;
	
	void Start () {
		if(Application.platform == RuntimePlatform.Android){
			Destroy(gameObject);
		}
		pv = transform.GetComponent<PhotonView>();
		gameMain = GameObject.Find("GameMain").GetComponent<GameMain>();
		pv.observed = this;
		if(pv.isMine){
			//transform.FindChild("Camera").gameObject.SetActive(true);
		}
		transform.parent = GameObject.Find("GameMain").transform;
		curRot = transform.rotation;
	}
	
	void Update () {
		if(pv.isMine){
			Vector3 curRot = gameMain.GetMyController().transform.rotation.eulerAngles;
			y += Norm (curRot.y) * Time.deltaTime;
			if(360 < y) y -= 360;
			if(y < -360) y += 360;
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(curRot.x, y,curRot.z)), Time.deltaTime*5f);
			if(gameMain.GetMyController().GetComponent<Controller>().GetEngine()){
				transform.GetComponent<Rigidbody>().AddForce(ship.transform.forward * Time.deltaTime * 1000);
				Debug.Log(Vector3.Distance(Vector3.zero, transform.GetComponent<Rigidbody>().velocity));
				if(50<Vector3.Distance(Vector3.zero, transform.GetComponent<Rigidbody>().velocity)){
					transform.GetComponent<Rigidbody>().velocity *= 0.95f;
				}
			}
			for(int i = 0 ; i < _renderer.Length ; i ++){
				_renderer[i].SetEngine(gameMain.GetMyController().GetComponent<Controller>().GetEngine());
			}
		}else{
			transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime*5f);
			transform.rotation = Quaternion.Lerp(transform.rotation, curRot, Time.deltaTime*5f);
		}
	}

	private float Norm(float value){
		if(180 < value){
			return value-360;
		}
		return value;
	}

	private Vector3 NormVec(Vector3 vec){
		return new Vector3(Norm(vec.x), Norm (vec.y), Norm (vec.z));
	}

	private Vector3 LarfVec(Vector3 frm, Vector3 to){
		return new Vector3(LarfFloat(frm.x, to.x), LarfFloat(frm.y, to.y), LarfFloat(frm.z, to.z));
	}

	private float LarfFloat(float frm, float to){
		if(frm<to){
			if(frm+180 < to){
				return frm-to+360;
			}else{
				return to-frm;
			}
		}else{
			if(to+180 < frm){
				return to-frm+360;
			}else{
				return frm-to;
			}
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
