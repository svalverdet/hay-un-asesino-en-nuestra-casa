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
		personaje.GoTo("Casa");
	}
	
	override public void Execute(Personaje personaje){
		
		// Sólo entra si el personaje se encuentra ya en la localización
		if(personaje.GetLocation() == Sala.Location.Casa){
			
			// Hacer cosas de casa
			Adulto a = (Adulto) personaje;
			a.IncrementarSed();
			if(a.TieneSed()){
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
				personaje.mLabel.text = msg;
				personaje.GetFSM().ChangeState(EstarEnElBar.GetInstance());
			}
		}
		
		/*int num = personaje.GetRandom(1,5);
		Debug.Log(num);
		if(num == 2){
			personaje.GetFSM().ChangeState(EcharBronca.GetInstance());
		}
		*/
	}
	override public void Exit(Personaje personaje){
	}
}
