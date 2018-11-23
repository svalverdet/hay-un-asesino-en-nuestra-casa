using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstarEnElBar : GenericState{

	#region SINGLETON
	private static EstarEnElBar INSTANCE;
	
	public static EstarEnElBar GetInstance(){
		if(INSTANCE == null){
			INSTANCE = new EstarEnElBar();
		}
		return INSTANCE;
	}
	#endregion
	
	override public void Enter(Personaje personaje){
		personaje.GoTo("Bar");
	}
	
	override public void Execute(Personaje personaje){
		
		// Sólo entra si el personaje se encuentra ya en la localización
		if(personaje.GetLocation() == Sala.Location.Bar){
			Adulto a = (Adulto) personaje;
			a.EfectosDelBar();
			personaje.GetFSM().ChangeState(EstarEnCasa.GetInstance());
		}
		
	}
	override public void Exit(Personaje personaje){
	}
}
