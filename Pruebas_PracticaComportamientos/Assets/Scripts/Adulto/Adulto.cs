using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Adulto : Personaje {
	
	private Perro perro;
	private Nino nino, ninoBronca;
    private bool veoPerro;

    override protected void Start()
	{
		// Variables del padre
        base.Start();
		
		agent = GetComponent<NavMeshAgent>();
		ChangeLocation(Sala.Location.Salon);
		
		mVejiga = 0;
		mAburrimiento = 0;
		ALERTA_ABURRIMIENTO = 100;
		ALERTA_VEJIGA = 150;
		
		mFSM = new FSM(this);
		mFSM.SetCurrentState(EstarEnCasa.GetInstance());
		mFSM.SetPreviousState(EstarEnCasa.GetInstance());
		mFSM.SetGlobalState(GlobalStateAdulto.GetInstance());
		mFSM.GetCurrentState().Enter(this);
		
		List<Personaje> asesinos = GetController().GetPersonajesByType<Asesino>();
		List<Personaje> ninos = GetController().GetPersonajesByType<Nino>();
        List<Personaje> perros = GetController().GetPersonajesByType<Perro>();
        mPersonajesDeInteres.AddRange(asesinos);
		mPersonajesDeInteres.AddRange(ninos);
        mPersonajesDeInteres.AddRange(perros);
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
        if (personajeOido != null 
            && mFSM.GetCurrentState() == EstarEnCasa.GetInstance())
            transform.LookAt(new Vector3(personajeOido.transform.position.x, transform.position.y, personajeOido.transform.position.z));

        if (personajeVisto != null) {
            if (personajeVisto.GetComponent<Asesino>() != null)
            {
                asesino = (Asesino)personajeVisto;
                nino = null;
                veoPerro = false;
            }
            else if (personajeVisto.GetComponent<Nino>() != null)
            {
                nino = (Nino)personajeVisto;
                veoPerro = false;
            }
            else if (personajeVisto.GetComponent<Perro>() != null)
            {
                nino = null;
                veoPerro = true;
            }

        }
        else
        {
            nino = null;
            veoPerro = false;
        }

    }
	
	// Métodos
	
	public void IncrementarVejiga(){ mVejiga+=1;}
	public void IncrementarAburrimiento(){ mAburrimiento+=2;}
	
	public void EfectosDeLaBronca(){ mAburrimiento -= 5; }
	public void EfectosDelWC(){mVejiga -= 4;}
	public void EfectosDelBar(){ mAburrimiento -= 5; mVejiga += 1;}
	
	public Nino GetNinoBronca(){ return ninoBronca; }
	public void SetNinoBronca(Nino n){ ninoBronca = n; }
    public bool GetNino() { return nino; }
    public Perro GetPerroAtencion(){return perro; }
    public bool GetVeoPerro() { return veoPerro; }
    public void SetPerroAtencion(Perro p){ perro = p;}
	
}
