using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public abstract class Personaje: MonoBehaviour{
	
	public string mName;
	public Text mLabel;
	
	// Instancia de la máquina de estados
	protected FSM mFSM;
	
	// Última vez que se actualizó el estado
	protected float mLastTimeUpdated;
	
	// Tiempo que puede pasar entre actualizaciones del estado
	protected float mIntervalToUpdate;
	
	// Localización del personaje
	protected Sala.Location mLocation;
	
	// NavMesh
	protected Vector3 goal;
	protected NavMeshAgent agent;
	
	
	// Métodos
	
	public abstract void UpdatePersonaje();
	
	public virtual string GetName(){return this.mName;}
	public virtual FSM GetFSM(){ return mFSM;}
	public virtual Sala.Location GetLocation(){ return mLocation;}
	
	public virtual void GoTo(string room){
		goal = Sala.GetRoomPosition(room);
		agent.destination = goal;
	}
	
	public virtual void ChangeLocation(Sala.Location loc){ this.mLocation = loc;}
	
	public virtual void println(string msg){ Debug.Log(this.mName+": "+msg);}
	
	// Comprobación de salas a las que entra el personaje
	void OnTriggerEnter(Collider other){
		String otherName = other.gameObject.name;
		if(otherName == "Casa" || otherName == "Bar" || otherName == "WC"){
			Sala.Location roomToGo = (Sala.Location)System.Enum.Parse(typeof(Sala.Location), other.gameObject.name);
			ChangeLocation(roomToGo);
			mLabel.text = "";
		}
    }
	
	// max NOT included
	public virtual int GetRandom(int min, int max){
		System.Random rnd = new System.Random(); int n = rnd.Next(min,max); return n;
	}
	
}
