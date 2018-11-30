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
		personaje.GoTo(Sala.Location.Bar);
	}
	
	override public void Execute(Personaje personaje){
		
		// Sólo entra si el personaje se encuentra ya en la localización
		if(personaje.GetLocation() == Sala.Location.Bar){
			personaje.println("Odio mi vida");
			Adulto a = (Adulto) personaje;
			a.EfectosDelBar();
			if(a.GetAburrimiento()<0){
				personaje.GetFSM().ChangeState(EstarEnCasa.GetInstance());
			}
		}
		
	}
	override public void Exit(Personaje personaje){
	}
}
