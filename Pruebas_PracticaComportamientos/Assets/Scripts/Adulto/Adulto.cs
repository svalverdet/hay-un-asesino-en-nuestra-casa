﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Adulto : Personaje {
	
	private Perro perro;
	private Nino nino;

    override protected void Start()
	{
		// Variables del padre
        base.Start();
		
		agent = GetComponent<NavMeshAgent>();
		ChangeLocation(Sala.Location.Salon);
		
		mVejiga = 2;
		mAburrimiento = 5;
		ALERTA_ABURRIMIENTO = 15;
		ALERTA_VEJIGA = 18;
		
		mFSM = new FSM(this);
		mFSM.SetCurrentState(EstarEnCasa.GetInstance());
		mFSM.SetPreviousState(EstarEnCasa.GetInstance());
		mFSM.SetGlobalState(GlobalStateAdulto.GetInstance());
		mFSM.GetCurrentState().Enter(this);
		
		// Variables propias
		// ...
	}
	
	override public void UpdatePersonaje()
	{
		mFSM.Update();
		IncrementarVejiga();
	}
	
	
	// Métodos
	
	public void IncrementarVejiga(){ mVejiga+=1;}
	public void IncrementarAburrimiento(){ mAburrimiento+=2;}
	
	public void EfectosDeLaBronca(){ mAburrimiento -= 5; }
	public void EfectosDelWC(){mVejiga -= 4;}
	public void EfectosDelBar(){ mAburrimiento -= 5; mVejiga += 1;}
	
	public Nino GetNinoBronca(){ return nino;}
	public void SetNinoBronca(Nino n){ nino = n;}
	public Perro GetPerroAtencion(){return perro;}
	public void SetPerroAtencion(Perro p){ perro = p;}
	
}
