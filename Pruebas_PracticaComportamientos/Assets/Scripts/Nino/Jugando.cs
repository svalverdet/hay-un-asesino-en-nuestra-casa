using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugando : GenericState{

	private static Jugando INSTANCE;
	
	public static Jugando GetInstance(){
		if(INSTANCE == null){
			INSTANCE = new Jugando();
		}
		return INSTANCE;
	}
	
	override public void Enter(Personaje personaje){
		personaje.GoTo("Casa");
		personaje.println("Me voy a jugar");
		personaje.mLabel.text = "Me voy a jugar";
	}
	
	override public void Execute(Personaje personaje){
		if(personaje.GetLocation() == Sala.Location.Casa){
			personaje.println("Yupii");
			personaje.mLabel.text = "Yupii mira papi soy bueno a que si papi";
		}
	}
	
	override public void Exit(Personaje personaje){
		personaje.println("Voy a dejar de jugar");
		personaje.mLabel.text = "Voy a dejar de jugar";
	}
}
