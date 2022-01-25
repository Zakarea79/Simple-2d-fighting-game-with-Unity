using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
	[SerializeField] Transform Player1 , Player2;
	[SerializeField] private Transform Bouttom_Left;
        Vector3 NewPosition;

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
    	float x = (Player1.position.x + Player2.position.x);
    	if(x != 0)
    	{
    		x=x/2;
    	}
    	NewPosition = Vector3.Lerp(new Vector3(transform.position.x , 0 , -1), new Vector3(x , 0 ,-1) , .1f);
    	transform.position = NewPosition;
    }
    //smooth camera movement
    public void LateUpdate()
    {
        transform.position = NewPosition;
    }
}
