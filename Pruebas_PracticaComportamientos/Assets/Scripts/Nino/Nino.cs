using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Nino : Personaje {

	private Adulto mAdultoBronca;
    private Asesino asesino;

    override protected  void Start()
	{
		// Variables del padre
		base.Start();
		
		agent = GetComponent<NavMeshAgent>();
		ChangeLocation(Sala.Location.HabitacionNino);
		
		mVejiga = 0;
		ALERTA_VEJIGA = 100;
		MAX_LIMITE_VEJIGA = 250;

        mFSM = new FSM(this);
		mFSM.SetCurrentState(Jugando.GetInstance());
		mFSM.SetPreviousState(Jugando.GetInstance());
		mFSM.SetGlobalState(NinoGlobalState.GetInstance());
		mFSM.GetCurrentState().Enter(this);

        List<Personaje> asesinos = GetController().GetPersonajesByType<Asesino>();
        mPersonajesDeInteres.AddRange(asesinos);
        // Variables propias
        // ...

    }

    override public void UpdatePersonaje()
	{
		mFSM.Update();
		IncrementarVejiga();
	}

    override public void UpdatePercepcion()
    {
        base.UpdatePercepcion();

        if (personajeVisto != null && personajeVisto.GetComponent<Asesino>() != null)
        {
            asesino = (Asesino) personajeVisto;
        }
    }

    // Métodos

    public void EfectosDelWC(){ this.mVejiga-=5; }
	
	public void IncrementarVejiga(){ mVejiga+=1; }
	
	public Adulto GetAdultoBronca(){ return this.mAdultoBronca; }
    
    public Asesino GetAsesino() { return asesino; }
    public void SetAsesino(Asesino a) { asesino = a; }

    public void SetAdultoBronca(Adulto a){ this.mAdultoBronca = a; }
	
	
	
}