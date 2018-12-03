using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstarEnElWCAsesino : GenericState{

	private static EstarEnElWCAsesino INSTANCE;
	
	public static EstarEnElWCAsesino GetInstance(){
		if(INSTANCE == null){
			INSTANCE = new EstarEnElWCAsesino();
		}
		return INSTANCE;
	}
	
	override public void Enter(Personaje personaje){
	
		if(personaje.GetSmartObjectInUse() != null){
			personaje.println("PASO QUE ME HAGO PIS");
			personaje.GoTo(personaje.GetSmartObjectInUse().transform.position);
		}else{
			personaje.GetFSM().ChangeState(BuscarVictima.GetInstance());
		}
	}
	
	override public void Execute(Personaje personaje){
		
		if(personaje.PathComplete())
		{
			personaje.println("Hago mi pipiii");
			personaje.GetSmartObjectInUse().Use(personaje);
			
			// Si ha terminado de consumir el objeto, cambia de estado
			if(personaje.GetSmartObjectInUse().checkUseFinished(personaje)){
				personaje.GetSmartObjectInUse().inUse = false;
				personaje.SetSmartObjectInUse(null);
				Debug.Log("YA HE TERMINADO");
				personaje.GetFSM().ChangeState(BuscarVictima.GetInstance());
			}
		}
	}
	override public void Exit(Personaje personaje){
		if(personaje.GetSmartObjectInUse()!=null){
			personaje.println("Jo, cómo me estaba meando");
			personaje.GetSmartObjectInUse().inUse = false;
			personaje.SetSmartObjectInUse(null);
		}
	}
}