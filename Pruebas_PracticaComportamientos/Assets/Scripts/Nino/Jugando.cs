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
		personaje.println("Me pongo a jugar");
	}
	override public void Execute(Personaje personaje){
		personaje.println("Yupii");
		//Nino a = (Nino) personaje;
	}
	override public void Exit(Personaje personaje){
		personaje.println("Voy a dejar de jugar");
	}
}
