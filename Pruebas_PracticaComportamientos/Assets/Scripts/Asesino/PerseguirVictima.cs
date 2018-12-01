﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerseguirVictima : GenericState {

	#region SINGLETON
	private static PerseguirVictima INSTANCE;
	
	public static PerseguirVictima GetInstance(){
		if(INSTANCE == null){
			INSTANCE = new PerseguirVictima();
		}
		return INSTANCE;
	}
	#endregion
	
	override public void Enter(Personaje personaje)
	{
		Asesino a = (Asesino) personaje;
		Personaje victima = a.GetVictima();
		personaje.println("Ñejejej "+victima.GetName()+" voy a por tiiii");
		personaje.GoTo(victima.transform.position);
	}
	
	override public void Execute(Personaje personaje){
		
		Asesino a = (Asesino) personaje;
		Personaje victima = a.GetVictima();
		Vector3 distancia = victima.transform.position - a.transform.position;
		
		if(distancia.sqrMagnitude < 9)
		{
			// Si no es un adulto, me lo cargo
			if (victima.GetComponent<Adulto>() == null)
			{
				victima.GetFSM().ChangeState(Morir.GetInstance());
				a.TieneVictima(false);
				a.GetFSM().ChangeState(BuscarVictima.GetInstance());
			}
		}
	}
	override public void Exit(Personaje personaje){
	}
}
