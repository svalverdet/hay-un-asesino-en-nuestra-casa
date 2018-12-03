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
		
		personaje.println("Voooooooy!!");
		Adulto a = ((Nino)personaje).GetAdultoBronca();
		if(a!=null)
		{
			Vector3 loc = a.transform.position;
			personaje.GoTo(loc);
		}
		else
		{
			personaje.GetFSM().RevertToPreviousState();
		}
	}
	
	override public void Execute(Personaje personaje){
		
		if(((Nino)personaje).GetAdultoBronca().GetNino() == personaje)
		{
			int num = personaje.GetRandom(1,6);
			if(num<3){
				personaje.println("Que sí lo que tú digas");
			}
		}
	}
	override public void Exit(Personaje personaje){
		Nino n = (Nino)personaje;
		personaje.println("que pesao el papa con la peta");
		n.SetAdultoBronca(null);
		
	}
}
