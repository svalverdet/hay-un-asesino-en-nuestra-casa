using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSmartObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	
	
	
	
	// Advertisement
	// The promised changes to the NPC's motivation values (eg. reduce Hunger by 10)
	// Hunger = -10
	
	// Requirements
	// returns bool (receives Personaje)
	// if none return true
	// The attributes the NPC must have to use the object
	
	// Actions
	// The actual changes to the NPC's motivations and attributes when used. (Includes animations and sound effects to play).
	// Set Hunger -= 10
	
	/*
	
	public float advertisedHungerAdjustment; //promised change in hunger
	public string requiredItem;
	public string actionRemoveItem;
	public string actionAddItem;
	public float actionHungerAdjustment;
	public string actionAnimation;
	
	public void Start(){
		// register with the manager
	}
	
	// This could be placed in a separate component so you can add different components for different objects.
	public void Use(Consumer consumer){
		consumer.attributes.Remove(actionRemoveItem);
		consumer.attributes.Add(actionAddItem);
		consumer.hunger += actionHungerAdjustment;
		consumer.animation.Play(actionAnimation);
	}
	
	
	// THE PROCESS
	Loop through all smart objects in the scene (a list provided by the manager) and reject objects whose requirements are false. Compute the effect
	that the object's advertisement will have on the NPC's happiness value (Attenuated by distance). Choose the object that has the best effect on
	happiness.
	
	Using 40 coroutines is probably the most efficient approach.
	
	If one NPC is using an object, set it so the requirement check returns false.
	
	
	
	
	ejemplo de la silla
	
	silla oferta -5 de aburrimiento
	no tiene requisitos, solo que nadie más la esté usando
	
	el viejo está en la casa y pregunta al controller qué objeto puede usar
	el controller recibe el personaje del viejo, y recorre la lista de objetos
	objetos: pistola, silla y silla2
	la pistola ofrece seguridad, silla está cerca y ofrece diversion, silla2 esta lejos y ofrece diversion
	Se aplica sobre la funcion
	Se ordenan por máximo valor de felicidad aportada, se devuelve el objeto que mas puntua
	
	El viejo recibe el objeto de vuelta y se dirige hacia él. Cuando llega a él, lo usa.
	
	
	
	
	
	
	
	
	*/
}
