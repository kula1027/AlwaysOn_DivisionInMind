using UnityEngine;
using System.Collections;

public class CarMove : MonoBehaviour {

	public bool engine = true;

	public float speed = 10;
	
	void Start () {
	
	}

	void Update () {

	}

	public void RotateRight(){

	}

	public void RotateLeft(){

	}

	public void RotateUp(){

	}

	public void RotateDown(){
		X x = new Y();
	}




}


public abstract class X{
	int x;
	abstract public void testFunc();
}

public class Y : X{
	int y;

	public override void testFunc(){

	}
}
