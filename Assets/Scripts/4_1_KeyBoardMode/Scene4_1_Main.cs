using UnityEngine;
using System.Collections;

public class Scene4_1_Main : MonoBehaviour {

	public GameObject canvas;




	void Start () {
		PhotonNetwork.isMessageQueueRunning = true;
	}
	
	public void Exit(){
		Application.Quit();
	}
	
	void Update () {

	}

	
	public void SetL1(bool on){
		GameObject.Find("GameMain").GetComponent<GameMain>().GetMyController().GetComponent<Controller>().SetL1(on);
	}
	public void SetL2(bool on){
		GameObject.Find("GameMain").GetComponent<GameMain>().GetMyController().GetComponent<Controller>().SetL2(on);
	}
	public void SetR1(bool on){
		GameObject.Find("GameMain").GetComponent<GameMain>().GetMyController().GetComponent<Controller>().SetR1(on);
	}
	public void SetR2(bool on){
		GameObject.Find("GameMain").GetComponent<GameMain>().GetMyController().GetComponent<Controller>().SetR2(on);
	}
	
	public bool GetEngine(){
		return GameObject.Find("GameMain").GetComponent<GameMain>().GetMyController().GetComponent<Controller>().GetEngine();
	}
}
