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
		
		if(personaje.GetSmartObjectInUse() != null){
			personaje.println("Voy a jugar");
			personaje.GoTo(personaje.GetSmartObjectInUse().transform.position);
		}else{
			personaje.GetFSM().ChangeState(NinoIdle.GetInstance());
		}
	}
	
	override public void Execute(Personaje personaje){
        if (personaje.PathComplete())
        {
            personaje.println("Estoy jugando");
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
		personaje.println("Voy a dejar de jugar");
		if(personaje.GetSmartObjectInUse() != null){
			personaje.GetSmartObjectInUse().inUse = false;
			personaje.SetSmartObjectInUse(null);
		}
	}
}
