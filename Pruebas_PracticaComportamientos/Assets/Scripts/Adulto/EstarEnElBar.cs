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
		
		if(personaje.GetSmartObjectInUse() != null){
			int num = personaje.GetRandom(1,4);
			string msg;
			if(num == 1){
				msg = "Voy a comprar tabaco";
			}else if(num == 2){
				msg = "Cariño me voy un rato con los colegas al club de literatura";
			}else{
				msg = "Me voy a dar un voltio";
			}
			personaje.println(msg);
			personaje.GoTo(personaje.GetSmartObjectInUse().transform.position);
		}else{
			personaje.GetFSM().ChangeState(EstarEnCasa.GetInstance());
		}
	}
	
	override public void Execute(Personaje personaje){
		
		if (personaje.PathComplete())
        {
            personaje.println("Odio mi vida");
			personaje.GetSmartObjectInUse().Use(personaje);
			
			// Si ha terminado de consumir el objeto, cambia de estado
			if(personaje.GetSmartObjectInUse().checkUseFinished(personaje)){
				personaje.GetSmartObjectInUse().inUse = false;
				personaje.SetSmartObjectInUse(null);
				personaje.GetFSM().ChangeState(EstarEnCasa.GetInstance());
			}
        }
		
	}
	override public void Exit(Personaje personaje){
		if(personaje.GetSmartObjectInUse() != null){
			personaje.GetSmartObjectInUse().inUse = false;
			personaje.SetSmartObjectInUse(null);
		}
	}
}
