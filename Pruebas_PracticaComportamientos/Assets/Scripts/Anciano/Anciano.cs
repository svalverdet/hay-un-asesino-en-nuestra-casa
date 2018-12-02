﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Anciano : Personaje {

	private bool sit;

    override protected void Start()
    {
		// Variables del padre
        base.Start();
		
        agent = GetComponent<NavMeshAgent>();
        ChangeLocation(Sala.Location.Entrada);

		mVejiga = 0;
		ALERTA_VEJIGA = 50;
		
        mFSM = new FSM(this);
        mFSM.SetCurrentState(Deambular.GetInstance());
        mFSM.SetPreviousState(Deambular.GetInstance());
        mFSM.SetGlobalState(AncianoGlobal.GetInstance());
        mFSM.GetCurrentState().Enter(this);
        
		// Variables propias
		sit = false;
    }

    override public void UpdatePersonaje()
    {
		mFSM.Update();
		IncrementarVejiga();
        
    }


    // Métodos
	
	public void EfectosDelWC(){ this.mVejiga = 0;}
	public void IncrementarVejiga(){ mVejiga+=1;}
	
	public bool IsSit(){ return sit;}
	public void SetSit(bool s){ sit = s;}
}
