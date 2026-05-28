using UnityEngine;
using System.Collections;

/**
 *	Rapidly sets a light on/off.
 *	
 *	(c) 2015, Jean Moreno
**/

[RequireComponent(typeof(Light))]
public class WFX_LightFlicker : MonoBehaviour
{


	void Start()
	{
		GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
	}

}
