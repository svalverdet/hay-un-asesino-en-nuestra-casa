using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstarEnElWC : GenericState{

	private static EstarEnElWC INSTANCE;
	
	public static EstarEnElWC GetInstance(){
		if(INSTANCE == null){
			INSTANCE = new EstarEnElWC();
		}
		return INSTANCE;
	}
	
	override public void Enter(Personaje personaje){
		personaje.GoTo("WC");
	}
	
	override public void Execute(Personaje personaje){
		if(personaje.GetLocation() == Sala.Location.WC){
			personaje.println("Ahh...Feels good man");
			personaje.mLabel.text = "Ahh...Feels good man";
			Adulto a = (Adulto) personaje;
			a.EfectosDelWC();
			personaje.GetFSM().ChangeState(personaje.GetFSM().GetPreviousState());
		}
	}
	override public void Exit(Personaje personaje){
	}
}