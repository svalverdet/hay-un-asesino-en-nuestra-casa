using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Nino : Personaje {

	private Adulto mAdultoBronca;

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

        // Variables propias
        // ...
        List<Personaje> asesinos = GetController().GetPersonajesByType<Asesino>();
        mPersonajesDeInteres.AddRange(asesinos);

    }

    override public void UpdatePersonaje()
	{
		mFSM.Update();
		IncrementarVejiga();
	}

    override public void UpdatePercepcion()
    {
        base.UpdatePercepcion();

        if (personajeOido != null
            && mFSM.GetCurrentState() == Jugando.GetInstance())
            transform.LookAt(new Vector3(personajeOido.transform.position.x, transform.position.y, personajeOido.transform.position.z));


        if (personajeVisto != null && personajeVisto.GetComponent<Asesino>() != null)
        {
            asesino = (Asesino) personajeVisto;
        }
    }

    // Métodos

    public void EfectosDelWC(){ this.mVejiga-=5; }
	
	public void IncrementarVejiga(){ mVejiga+=1; }
	
	public Adulto GetAdultoBronca(){ return this.mAdultoBronca; }

    public void SetAdultoBronca(Adulto a){ this.mAdultoBronca = a; }
	
}