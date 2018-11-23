using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	public List<Personaje> personajes;
	
	void Start () {
		Sala.timer = 0.0f;
	}
	
	void Update () {
		Sala.timer += Time.deltaTime;
		//float seconds = Sala.timer % 60;
		
		int size = personajes.Count;
		for(int i=0; i<size; i++){
			personajes[i].UpdatePersonaje();
		}
	}
}
