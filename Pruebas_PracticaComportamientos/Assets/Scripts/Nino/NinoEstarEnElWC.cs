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
		
		if(personaje.GetSmartObjectInUse() != null){
			personaje.println("Voy a cagar mierda");
			personaje.GoTo(personaje.GetSmartObjectInUse().transform.position);
		}else{
			personaje.GetFSM().ChangeState(NinoIdle.GetInstance());
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
				personaje.GetFSM().ChangeState(NinoIdle.GetInstance());
			}
		}
		
	}
	
	override public void Exit(Personaje personaje){
		personaje.println("Saliendo del WC");
		if(personaje.GetSmartObjectInUse()!=null){
			personaje.GetSmartObjectInUse().inUse = false;
			personaje.SetSmartObjectInUse(null);
		}
	}
}