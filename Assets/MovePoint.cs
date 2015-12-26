using UnityEngine;
using System.Collections;

public class MovePoint : MonoBehaviour {

	bool trackMouse;


	Vector3 mouseOffset;

	void Update()
	{
		if(trackMouse)
		{
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mouseWorldPos.z=0;
			transform.position= mouseWorldPos - mouseOffset;
		}
	}
	void OnMouseDown()
	{
		trackMouse=true;
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mouseWorldPos.z=0;
		mouseOffset = mouseWorldPos - transform.position;
	}
	void OnMouseUp()
	{
		trackMouse=false;
	}
}
