using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuscarVictima : GenericState {

	#region SINGLETON
	private static BuscarVictima INSTANCE;
	
	public static BuscarVictima GetInstance(){
		if(INSTANCE == null){
			INSTANCE = new BuscarVictima();
		}
		return INSTANCE;
	}
	#endregion
	
	override public void Enter(Personaje personaje)
	{
		personaje.println("jijiji");
        personaje.GoTo(Sala.GetRandomRoomPositionExcept(new Sala.Location[] { personaje.GetLocation(), Sala.Location.Bar }));
    }
	
	override public void Execute(Personaje personaje){
		
		if (personaje.PathComplete())
        {
            personaje.GoTo(Sala.GetRandomRoomPositionExcept(new Sala.Location[] { personaje.GetLocation(), Sala.Location.Bar }));
        }
	}
	override public void Exit(Personaje personaje){
	}
}
