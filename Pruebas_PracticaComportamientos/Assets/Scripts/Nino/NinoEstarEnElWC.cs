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
		personaje.GoTo(Sala.Location.WC);
		personaje.println("Voy a cagar mierda");
	}
	
	override public void Execute(Personaje personaje){
		
		if(personaje.PathComplete() && personaje.GetLocation() == Sala.Location.WC)
		{
			personaje.println("Hago mi pipiii");
			Nino a = (Nino) personaje;
			a.EfectosDelWC();
			if(a.GetVejiga()<0)
			{
				a.VaciarVejiga();
				if(a.GetFSM().GetPreviousState() == RecibirBronca.GetInstance()){
					a.GetFSM().ChangeState(Jugando.GetInstance());
				}else{
					a.GetFSM().ChangeState(a.GetFSM().GetPreviousState());
				}
			}
		}
	}
	
	override public void Exit(Personaje personaje){
		personaje.println("Saliendo del WC");
	}
}