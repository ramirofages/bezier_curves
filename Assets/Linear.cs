using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Linear : MonoBehaviour 
{

	public Transform p0;
	public Transform p1;
	public Transform p2;
	public Transform pA;
	public Transform pB;
	public Transform target;
	public bool showNormals=false;
	public bool showCurve=false;

	public Text tValue;
	public Text precisionValue;

	public float precision=0.001f;

	[Range(0f,1f)]
	public float t;



	void Update () 
	{


		pA.position = Vector3.Lerp(p0.position,p1.position,t);
		pB.position = Vector3.Lerp(p1.position,p2.position,t);
		target.position = Vector3.Lerp(pA.position,pB.position,t);


		GLDebug.DrawLine(pA.position,pB.position,Color.grey,0,true);
		GLDebug.DrawLine(p0.position,p1.position,Color.grey,0,true);
		GLDebug.DrawLine(p1.position,p2.position,Color.grey,0,true);
		if(showCurve)
			DrawBezier();
	}


	void DrawBezier()
	{
		float length=0f;
		Vector3 lastPos = p0.position;

		while(length < 1)
		{
			Vector3 Apos = Vector3.Lerp(p0.position,p1.position,length);
			Vector3 Bpos = Vector3.Lerp(p1.position,p2.position,length);
			Vector3 tempPos = Vector3.Lerp(Apos,Bpos,length);

			GLDebug.DrawLine(lastPos,tempPos,Color.red,0,true);

			if(showNormals)
				DrawNormal(Apos,Bpos,tempPos);


			lastPos= tempPos;
			length+= precision;
		}
		GLDebug.DrawLine(lastPos,p2.position,Color.red,0,true);
	}

	void DrawNormal(Vector3 a, Vector3 b, Vector3 origin)
	{
		Vector3 tan= (b-a).normalized;
		Vector3 binormal = Vector3.Cross(tan,Vector3.up);
		Vector3 normal = Vector3.Cross(binormal,tan);
		GLDebug.DrawRay(origin,normal,Color.cyan,0,true);
	}





	public void SetShowCurve(bool value)
	{
		showCurve=value;
	}

	public void SetShowNormals(bool norm)
	{
		showNormals=norm;
	}

	public void SetT(float t)
	{
		this.t = t;
		tValue.text="T = "+t;
	}
	public void SetPrecision(float pres)
	{
		precision = pres;
		precisionValue.text = "Precision = "+pres;
	}
}
