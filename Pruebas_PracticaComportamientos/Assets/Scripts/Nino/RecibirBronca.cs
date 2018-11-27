using System.Collections;
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
		
		//if(personaje.GetLocation() != Sala.Location.Casa){
			personaje.println("Voooooooy!!");
			personaje.mLabel.text = "Voooooooy!!";
			personaje.GoTo("Casa");
		//}else{
			//personaje.Stop();
		//}
	}
	override public void Execute(Personaje personaje){
		
		if(personaje.GetLocation() == Sala.Location.Casa){
			int num = personaje.GetRandom(1,6);
			if(num<3){
				personaje.println("Que sí lo que tú digas");
				personaje.mLabel.text = "Que sí lo que tú digas";
			}
		}else{
			personaje.println("Estoy yendo");
			
		}
	}
	override public void Exit(Personaje personaje){
		personaje.println("que pesao el papa con la peta");
	}
}
