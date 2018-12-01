using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public abstract class Personaje: MonoBehaviour{
	
	public string mName;
	public Text mLabel;
	public GameObject manchaCaca;
	
	//Percepción
	protected List<Personaje> mPersonajesDeInteres = new List<Personaje>();
	protected float mRadioVision = 10.0f;
	protected float mRadioOido = 14.0f;
	protected float mHalfAngleVision = 60.0f;
	protected Personaje personajeVisto;
	protected Personaje personajeOido;
	
    //Controller
    protected Controller controller;

	// Instancia de la máquina de estados
	protected FSM mFSM;
	
	// Motivaciones
	protected int mVejiga;
	protected int mAburrimiento;
	protected int ALERTA_ABURRIMIENTO;
	protected int ALERTA_VEJIGA;
	protected int MAX_LIMITE_VEJIGA; // Algunos personajes se hacen caca encima cuando lo sobrepasan
	
	// Localización del personaje
	protected Sala.Location mLocation;
	
	// NavMesh
	protected Vector3 goal;
	protected NavMeshAgent agent;
	
	

	// Métodos
	
    protected virtual void Start()
    {
        controller = GameObject.Find("WorldController").GetComponent<Controller>();
    }

	public abstract void UpdatePersonaje();
	
	public void UpdateTextoPersonaje()
	{
		Vector3 labelPos = Camera.main.WorldToScreenPoint(this.transform.position);
        mLabel.transform.position = labelPos;
	}
	
	// LLamar a base.UpdatePercepcion() desde los hijos (igual que en Start());
	public virtual void UpdatePercepcion()
	{
		// Vista
		personajeVisto = comprobarPercepcionVista();
		
		//Oído
		personajeOido = comprobarPercepcionOido();
		
		//Personaje personajePercibidoVista = comprobarPercepcionVista();
		/*if(personajePercibidoVista != null && personajePercibidoVista != personajeVisto)
		{
			personajeVisto = personajePercibidoVista;
			println("VEO A "+personajeVisto.mName);
		}else if(personajePercibidoVista == null && personajeVisto != null){
			personajeVisto = null;
			println("No veo a nadie");
		}*/
		
		//Personaje personajePercibidoOido = comprobarPercepcionOido();
		
		/*if(personajePercibidoOido != null && personajePercibidoOido != personajeOido)
		{
			personajeOido = personajePercibidoOido;
			println("OIGO A "+personajeOido.mName);
		}else if(personajePercibidoOido == null && personajeOido != null){
			personajeOido = null;
			println("No oigo a nadie");
		}
		*/
	}
	
	protected Personaje comprobarPercepcionVista()
	{
		int size = mPersonajesDeInteres.Count;
		for(int i=0; i<size; i++)
		{
			Vector3 distance = mPersonajesDeInteres[i].transform.position - this.transform.position;
			Vector3 normDistance = Vector3.Normalize(distance);
			float angleInBetween = Math.Abs(Vector3.Angle(transform.forward, normDistance));
			
			if(distance.sqrMagnitude < mRadioVision*mRadioVision 
			&& angleInBetween < mHalfAngleVision)
			{
				// Si choca con una pared, no ve
				RaycastHit info;
				if (Physics.Raycast(transform.position, normDistance, out info, mRadioVision)
					&& !info.transform.tag.Equals("Pared")) 
				{
					return mPersonajesDeInteres[i];
				}
				
			}
		}
		return null;
	}
	
	protected Personaje comprobarPercepcionOido()
	{
		int size = mPersonajesDeInteres.Count;
		for(int i=0; i<size; i++)
		{
			Vector3 distance = mPersonajesDeInteres[i].transform.position - this.transform.position;
			Vector3 normDistance = Vector3.Normalize(distance);
			
			if(distance.sqrMagnitude < mRadioOido*mRadioOido)
			{
				// Si choca con una pared, no oye
				RaycastHit info;
				if (Physics.Raycast(transform.position, normDistance, out info, mRadioOido)
					&& !info.transform.tag.Equals("Pared")) 
				{
					return mPersonajesDeInteres[i];
				}
				
			}
		}
		return null;
	}
	
	void OnDrawGizmosSelected()
	{
		// Oído
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(this.transform.position, mRadioOido);
		
		// Vista
		Gizmos.color = Color.red;
		
		Vector3 left = Quaternion.AngleAxis(mHalfAngleVision, transform.up) * transform.forward;
		Vector3 right = Quaternion.AngleAxis(-mHalfAngleVision, transform.up) * transform.forward;
		
		Vector3 forwardLeft = this.transform.position + left * mRadioVision;
		Vector3 forwardRight = this.transform.position + right * mRadioVision;
		
		Gizmos.DrawLine(this.transform.position, forwardLeft);
		Gizmos.DrawLine(this.transform.position, forwardRight);
	}
	
    public Controller GetController() { return controller; }
	public FSM GetFSM(){ return mFSM;}
	public string GetName(){return this.mName;}
	public List<Personaje> GetPersonajesDeInteres(){ return mPersonajesDeInteres;}
	public void println(string msg)
	{ 
		Debug.Log(this.mName+": "+msg);
		mLabel.text = msg;
	}
	
		
	public Sala.Location GetLocation(){ return mLocation;}
    public void ChangeLocation(Sala.Location loc){ this.mLocation = loc;}
	
	
	public void Mancha()
    {
        GameObject mancha = Instantiate(manchaCaca, new Vector3(transform.position.x, transform.position.y - GetComponent<Renderer>().bounds.size.y / 2, transform.position.z), Quaternion.identity);
        Roomba roomba = (Roomba) controller.GetPersonajesByType<Roomba>()[0];
        roomba.AddMancha(mancha);
        if (roomba.GetFSM().GetCurrentState() == IdleRoomba.GetInstance())
            roomba.GetFSM().ChangeState(BuscarMancha.GetInstance());
    }
	
	
	// Comprobación de salas a las que entra el personaje
	void OnTriggerEnter(Collider other){
		
		bool triggered = false;
		int size = Sala.tags.Length;
		for(int i=0; i<size; i++){
			if(other.gameObject.CompareTag(Sala.tags[i]))
			{
				triggered = true;
			}
		}
		
		if(triggered)
		{
			Sala.Location roomToGo = (Sala.Location)System.Enum.Parse(typeof(Sala.Location), other.gameObject.tag);
			ChangeLocation(roomToGo);
		}
    }
	
	void OnTriggerExit(Collider other){
		
		bool triggered = false;
		int size = Sala.tags.Length;
		for(int i=0; i<size; i++){
			if(other.gameObject.CompareTag(Sala.tags[i]))
			{
				triggered = true;
			}
		}
		
		if(triggered)
		{
			ChangeLocation(Sala.Location.None);
		}
	}
	

	//Movimiento
	
	public void GoTo(Sala.Location room)
	{
		agent.isStopped = false;
		goal = Sala.GetRoomPosition(room);
		agent.destination = goal;
		
	}
    public void GoTo(Vector3 goal)
    {
        agent.isStopped = false;
        agent.destination = goal;
    }
    public void Stop(){ agent.isStopped = true;}
	public bool IsAgentStopped(){ return agent.isStopped;}
    public bool PathComplete()
    {
        if (Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance + 2)
        {
            return true;
        }
        return false;
    }
	
	
	
	// max NOT included
	public int GetRandom(int min, int max){
		System.Random rnd = new System.Random();
        int n = rnd.Next(min,max);
        return n;
	}
	
	
	
	// Getters y setters de las motivaciones
	public int GetVejiga(){ return mVejiga;}
	public int GetAburrimiento(){ return mAburrimiento;}
	public void VaciarVejiga(){ mVejiga = 0;}
	
	public bool TienePis(){ return mVejiga >= ALERTA_VEJIGA;}
	public bool TieneVejigaAlLimite(){return mVejiga >= MAX_LIMITE_VEJIGA;}
	public bool EstaAburrido(){ return mAburrimiento >= ALERTA_ABURRIMIENTO;}

}
