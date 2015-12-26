using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{

   
    Vector3 lookPos;
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            Matrix4x4 matriz = Matrix4x4.identity;
            matriz.SetColumn(3, Vector4.one);
            //transform.position = matriz.MultiplyPoint(transform.position);
            
            float x = transform.position.x;
            float y = transform.position.y;
            float z = transform.position.z;
            

            float angulo = 45 * Mathf.Deg2Rad;
            float newX = x * 1 + y * 0                 + z * 0;
            float newY = x * 0 + y * Mathf.Cos(angulo) + z * (- Mathf.Sin(angulo));
            float newZ = x * 0 + y * Mathf.Sin(angulo) + z * Mathf.Cos(angulo);
            
            Vector3 result = new Vector3(newX, newY, newZ);
            transform.position = result;
            print(result.ToString());
            Debug.DrawLine(Vector3.zero, result, Color.red, 3f);
        }
    }

}
