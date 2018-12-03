using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCollider : MonoBehaviour {

	
	public Sala.Location locationType;
	
	public void Start(){
		// register with the manager
		Sala.AddCollider(this);
	}
	
	
	
}
