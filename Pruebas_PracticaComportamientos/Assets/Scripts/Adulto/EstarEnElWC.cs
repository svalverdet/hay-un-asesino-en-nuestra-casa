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
		if(personaje.GetSmartObjectInUse() != null){
			personaje.println("Voy a hacer uso del urinario");
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
				personaje.println("Ahh...Feels good man");
				personaje.GetSmartObjectInUse().inUse = false;
				personaje.SetSmartObjectInUse(null);
				personaje.GetFSM().ChangeState(NinoIdle.GetInstance());
			}
		}
	}
	override public void Exit(Personaje personaje){
		if(personaje.GetSmartObjectInUse()!=null){
			personaje.GetSmartObjectInUse().inUse = false;
			personaje.SetSmartObjectInUse(null);
		}
	}
}