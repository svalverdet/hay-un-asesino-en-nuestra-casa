using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstarEnCasa : GenericState {

	#region SINGLETON
	private static EstarEnCasa INSTANCE;
	
	public static EstarEnCasa GetInstance(){
		if(INSTANCE == null){
			INSTANCE = new EstarEnCasa();
		}
		return INSTANCE;
	}
	#endregion
	
	override public void Enter(Personaje personaje){
		personaje.GoTo(Sala.Location.Salon);
	}
	
	override public void Execute(Personaje personaje){
		
		// Sólo entra si el personaje se encuentra ya en la localización
		if(personaje.GetLocation() == Sala.Location.Salon){
			
			// Hacer cosas de casa
			Adulto a = (Adulto) personaje;
			a.IncrementarAburrimiento();
			float rnd = Random.value * 100;
			if(a.EstaAburrido()){
				if(rnd<100){
					personaje.GetFSM().ChangeState(EcharBronca.GetInstance());
				}else{
					personaje.GetFSM().ChangeState(EstarEnElBar.GetInstance());
				}
				
			}
		}
	}
	override public void Exit(Personaje personaje){
	}
}
