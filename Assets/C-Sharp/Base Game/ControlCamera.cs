using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
	//نگه داشتن پوزیشن های دو کاراکتر
	[SerializeField] Transform Player1 , Player2;
	//[SerializeField] private Transform Bouttom_Left;

//پیدا کردن وسط دو نقطه (در برنامه استفاده نشده)
public Vector3 HafVector(Vector3 a , Vector3 b)
{
	float x = (a.x + b.x);
	x=x/2;
	float y = (a.y + b.y);
	y=y/2;
	float z = (a.z + b.z);
	z=z/2;
	return new Vector3(z , y , z);
} 
    void Update()
    {
		//نگه داشتن پوزیشن کمرا بین دو کاراکتر
    	float x = (Player1.position.x + Player2.position.x);
    	if(x != 0)
    	{
    		x=x/2;
    	}
    	Vector3 NewPosition = Vector3.Lerp(new Vector3(transform.position.x , 0 , -1), new Vector3(x , 0 ,-1) , .1f);
    	transform.position = NewPosition;
    }
}
