/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcharBronca : GenericState {

	private static EcharBronca INSTANCE;
	
	public static EcharBronca GetInstance(){
		if(INSTANCE == null){
			INSTANCE = new EcharBronca();
		}
		return INSTANCE;
	}
	
	override public void Enter(Personaje personaje){
		Adulto a = (Adulto) personaje;
		personaje.println("NIÑOOOOOOOOO!!");
		Nino n = (Nino)Sala.GetPersonaje("Nino");
		a.println(n.GetName()+ "ven aquí YA");
		n.GetFSM().ChangeState(RecibirBronca.GetInstance());
	}
	override public void Execute(Personaje personaje){
		personaje.println("dsanjkdsahjk$3·45jj disia 343 ");
		
	}
	override public void Exit(Personaje personaje){
		int num = personaje.GetRandom(1,6);
		if(num<3)
			personaje.println("Qué poco nos respetan los niños...");
		else if(num>=3)
			personaje.println("En mis tiempos con una buena hostia se le quitaba la tontería.");
	}
}
*/