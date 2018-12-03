using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Asesino : Personaje {
	
	private Personaje mVictima;
	private bool mTieneVictima = false;

    override protected void Start()
	{
		// Variables del padre
        base.Start();
		
		agent = GetComponent<NavMeshAgent>();
		ChangeLocation(Sala.Location.Bar);
		
		mVejiga = 0;
		ALERTA_VEJIGA = 250;
		
		mEstadoInicial = BuscarVictima.GetInstance();
		mFSM = new FSM(this);
		mFSM.SetCurrentState(mEstadoInicial);
		mFSM.SetPreviousState(mEstadoInicial);
		mFSM.SetGlobalState(GlobalStateAsesino.GetInstance());
		mFSM.GetCurrentState().Enter(this);
		
		List<Personaje> ancianos = GetController().GetPersonajesByType<Anciano>();
		List<Personaje> ninos = GetController().GetPersonajesByType<Nino>();
		List<Personaje> adultos = GetController().GetPersonajesByType<Adulto>();
		mPersonajesDeInteres.AddRange(ancianos);
		mPersonajesDeInteres.AddRange(ninos);
		mPersonajesDeInteres.AddRange(adultos);
		
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
		
		if(!mTieneVictima && personajeVisto != null){
			mTieneVictima = true;
			mVictima = personajeVisto;
		}
	}
	
	// Métodos
	
	public void IncrementarVejiga(){ mVejiga+=1;}
	
	public void EfectosDelWC(){mVejiga -= 5;}
	
	public Personaje GetVictima(){return mVictima;}
	public void SetVictima(Personaje p){ mVictima = p;}
	public bool TieneVictima(){ return mTieneVictima;}
	public void TieneVictima(bool v){ mTieneVictima = v;}
	
}
