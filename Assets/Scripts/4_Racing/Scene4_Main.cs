using UnityEngine;
using System.Collections;

public class Scene4_Main : MonoBehaviour {

	public GameObject canvas;
	public Transform Map;
	public Transform camera;

	void Start () {
		PhotonNetwork.isMessageQueueRunning = true;
		Instantiate(Map, new Vector3(50, 0, 50), Quaternion.identity);
		if(Application.platform == RuntimePlatform.Android){
			canvas.SetActive(true);
		}else{
			CreateShip();
			Instantiate(camera, new Vector3(0, 0, 0), Quaternion.identity);
		}
	}

	public void Exit(){
		Application.Quit();
	}

	void Update () {
		if(Application.platform == RuntimePlatform.Android){
			if(0 < Input.touchCount){
				//터치를 한 경우
				Vector3 touchPos = Input.GetTouch(0).position;
				//첫번째 손가락 터치 저장
				if(0.2f * Screen.width < touchPos.x && touchPos.x < 0.8f * Screen.width){
					if(0.2f * Screen.height < touchPos.y && touchPos.y < 0.8f * Screen.height){
						//버튼의 범위안에 첫번째 손가락이 있을 때
						if(!GetEngine()){
							SetEngine(true);
							//엔진 on
						}
					}else{
						//버튼의 범위 안에 없을 때
						if(GetEngine()){
							SetEngine(false);
							//엔진 off
						}
					}
				}else{
					//버튼의 범위 안에 없을 때
					if(GetEngine()){
						SetEngine(false);
						//엔진 off
					}
				}
			}else{
				//손가락 터치가 없을 때
				if(GetEngine()){
					SetEngine(false);
					//엔진 off
				}
			}
		}
	}

	public void SetEngine(bool on){
		GameObject.Find("GameMain").GetComponent<GameMain>().GetMyController().GetComponent<Controller>().SetEngine(on);
	}

	public bool GetEngine(){
		return GameObject.Find("GameMain").GetComponent<GameMain>().GetMyController().GetComponent<Controller>().GetEngine();
	}

	public void CreateShip(){
		PhotonNetwork.Instantiate("4_Racing/Ship", new Vector3(PhotonNetwork.player.ID*3, 0, 0), Quaternion.identity, 0);
	}

}
