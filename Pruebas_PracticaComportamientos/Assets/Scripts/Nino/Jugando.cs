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
		if(personaje.GetLocation() != Sala.Location.HabitacionNino){
			personaje.GoTo(Sala.Location.HabitacionNino);
			personaje.println("Me voy a jugar");
		}
	}
	
	override public void Execute(Personaje personaje){
		if(personaje.GetLocation() == Sala.Location.HabitacionNino){
		}
	}
	
	override public void Exit(Personaje personaje){
		personaje.println("Voy a dejar de jugar");
	}
}
