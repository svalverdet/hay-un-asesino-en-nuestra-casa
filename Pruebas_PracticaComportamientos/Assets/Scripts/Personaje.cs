using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public abstract class Personaje: MonoBehaviour{
	
	public string mName;
	protected Text mLabel;
	public GameObject manchaCaca;
    public GameObject manchaSangre;
    protected Asesino asesino;

	//Percepción
	protected List<Personaje> mPersonajesDeInteres = new List<Personaje>();
	protected float mRadioVision = 10.0f;
	protected float mRadioOido = 5.0f;
	protected float mHalfAngleVision = 60.0f;
	protected Personaje personajeVisto;
	protected Personaje personajeOido;
	
    //Controller
    protected Controller controller;

	// Instancia de la máquina de estados
	protected FSM mFSM;
	protected GenericState mEstadoInicial;
	
	// Motivaciones
    protected int mHealth = 25;
	
	public float mVejiga;
	public AnimationCurve mVejigaUrgency;
	public float mAburrimiento;
	public AnimationCurve mAburrimientoUrgency;
	protected float ALERTA_ABURRIMIENTO;
	protected float ALERTA_VEJIGA;
	protected float MAX_LIMITE_VEJIGA; // Algunos personajes se hacen caca encima cuando lo sobrepasan
	public GenericSmartObject mSmartObjectInUse;
	
	// Localización del personaje
	protected Sala.Location mLocation;
	
	// NavMesh
	protected Vector3 goal;
	protected NavMeshAgent agent;

    // Última vez que se actualizó el estado
    float mLastTimeUpdated = 0.0f;

    // Tiempo que puede pasar entre actualizaciones del estado
    float mIntervalToUpdate = 2.0f;

    // Métodos

    protected virtual void Start()
    {
        controller = GameObject.Find("WorldController").GetComponent<Controller>();
        mLabel = Instantiate(GameObject.Find("TextPrefab").GetComponent<Text>(), transform.position, transform.rotation) as Text;
        GameObject canvas = GameObject.Find("Canvas");
        mLabel.transform.SetParent(canvas.transform, false);
    }

    public abstract void UpdatePersonaje();
	
	public void UpdateTextoPersonaje()
	{
		Vector3 labelPos = Camera.main.WorldToScreenPoint(this.transform.position);
        mLabel.transform.position = labelPos;
        if(Sala.timer - mLastTimeUpdated > mIntervalToUpdate)
        {
            mLastTimeUpdated = Sala.timer;
            mLabel.text = "";
        }
    }
	
	// LLamar a base.UpdatePercepcion() desde los hijos (igual que en Start());
	public virtual void UpdatePercepcion()
	{
		// Vista
		personajeVisto = comprobarPercepcionVista();
		
		//Oído
		personajeOido = comprobarPercepcionOido();
		
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
	public GenericState GetEstadoInicial(){ return mEstadoInicial;}
	public void println(string msg)
	{
        if (mLabel.text.Equals("")) {
            Debug.Log(this.mName + ": " + msg);
            mLabel.text = msg;
        }		
	}
	
		
	public Sala.Location GetLocation(){ return mLocation;}
    public void ChangeLocation(Sala.Location loc){ this.mLocation = loc;}
	
	
	public void Mancha(int tipo)
    {
        GameObject mancha;
        if (tipo == 0)
            mancha = Instantiate(manchaCaca, new Vector3(transform.position.x, transform.position.y - GetComponent<Renderer>().bounds.size.y / 2, transform.position.z), Quaternion.identity);
        else
            mancha = Instantiate(manchaSangre, new Vector3(transform.position.x, transform.position.y - GetComponent<Renderer>().bounds.size.y / 2, transform.position.z), Quaternion.identity);

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
	public float GetVejiga(){ return mVejiga;}
	public float GetAburrimiento(){ return mAburrimiento;}
    public float GetHealth() { return mHealth; }
    public Asesino GetAsesino() { return asesino; }
    public void SetAsesino(Asesino a) { asesino = a; }
    public void Damaged() { mHealth--; }
    public void RemoveLabel() { GameObject.Destroy(mLabel); }

	public void VaciarVejiga(){ mVejiga = 0;}
	
	public bool TienePis(){ return mVejiga >= ALERTA_VEJIGA;}
	public bool TieneVejigaAlLimite(){return mVejiga >= MAX_LIMITE_VEJIGA;}
	public bool EstaAburrido(){ return mAburrimiento >= ALERTA_ABURRIMIENTO;}

	public void AddVejiga(float v){ mVejiga += v;}
	public void AddAburrimiento(float a){ mAburrimiento += a;}
	
	public GenericSmartObject GetSmartObjectInUse(){ return mSmartObjectInUse;}
	public void SetSmartObjectInUse(GenericSmartObject gso){ mSmartObjectInUse = gso;}
	public float GetUrgency(float vejiga, float aburrimiento)
	{ 
		float vejigaUrgency = mVejigaUrgency.Evaluate(Mathf.Clamp(vejiga/100, 0.0f, 1.0f));
		float aburrimientoUrgency = mAburrimientoUrgency.Evaluate(Mathf.Clamp(aburrimiento/100, 0.0f, 1.0f));
		return (vejigaUrgency + aburrimientoUrgency)/2;
	}
	
    public void AddInteraccionAsesino()
    {
        List<Personaje> asesinos = GetController().GetPersonajesByType<Asesino>();
        mPersonajesDeInteres.AddRange(asesinos);
    }
}
