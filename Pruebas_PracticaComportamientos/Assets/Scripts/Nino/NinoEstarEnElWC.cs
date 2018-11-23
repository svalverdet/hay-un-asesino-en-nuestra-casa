using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinoEstarEnElWC : GenericState{

	private static NinoEstarEnElWC INSTANCE;
	
	public static NinoEstarEnElWC GetInstance(){
		if(INSTANCE == null){
			INSTANCE = new NinoEstarEnElWC();
		}
		return INSTANCE;
	}
	
	override public void Enter(Personaje personaje){
		personaje.println("Entrando en WC");
	}
	override public void Execute(Personaje personaje){
		personaje.println("Hago mi pipiii");
		Nino a = (Nino) personaje;
		a.EfectosDelWC();
		a.GetFSM().ChangeState(a.GetFSM().GetPreviousState());
	}
	override public void Exit(Personaje personaje){
		personaje.println("Saliendo del WC");
	}
}