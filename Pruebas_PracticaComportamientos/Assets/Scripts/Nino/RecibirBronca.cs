/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecibirBronca : GenericState{

	private static RecibirBronca INSTANCE;
	
	public static RecibirBronca GetInstance(){
		if(INSTANCE == null){
			INSTANCE = new RecibirBronca();
		}
		return INSTANCE;
	}
	
	override public void Enter(Personaje personaje){
		personaje.println("Vooooy!");
	}
	override public void Execute(Personaje personaje){
		personaje.println("que sí lo que tú digas");
		Nino a = (Nino) personaje;
		a.GetFSM().ChangeState(a.GetFSM().GetPreviousState());
		Adulto n = (Adulto)Sala.GetPersonaje("Adulto");
		n.GetFSM().ChangeState(n.GetFSM().GetPreviousState());
	}
	override public void Exit(Personaje personaje){
		
	}
}
*/