﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public abstract class Personaje: MonoBehaviour{
	
	public string mName;
	public Text mLabel;

    //Controller
    protected Controller controller;

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
	
    protected virtual void Start()
    {
        controller = GameObject.Find("WorldController").GetComponent<Controller>();
    }

	public abstract void UpdatePersonaje();
	
    public virtual Controller GetController() { return controller; }
	public virtual string GetName(){return this.mName;}
	public virtual FSM GetFSM(){ return mFSM;}
	public virtual Sala.Location GetLocation(){ return mLocation;}
	
	public virtual void GoTo(string room){
        agent.isStopped = false;
        goal = Sala.GetRoomPosition(room);
		agent.destination = goal;
	}
    public virtual void GoTo(Vector3 goal)
    {
        agent.isStopped = false;
        agent.destination = goal;
    }
    public virtual void Stop()
    {
        agent.isStopped = true;
    }
	
	public bool IsAgentStopped(){
		return agent.isStopped;
	}

    public virtual void ChangeLocation(Sala.Location loc){ this.mLocation = loc;}
	
	public virtual void println(string msg){ Debug.Log(this.mName+": "+msg);}
	
	// Comprobación de salas a las que entra el personaje
	void OnTriggerEnter(Collider other){
		String otherName = other.gameObject.name;
		if(otherName == "Casa" || otherName == "Bar" || otherName == "WC"){
			Sala.Location roomToGo = (Sala.Location)System.Enum.Parse(typeof(Sala.Location), other.gameObject.name);
			ChangeLocation(roomToGo);
			//mLabel.text = "";
		}
    }
	
	void OnTriggerExit(Collider other){
		String otherName = other.gameObject.name;
		if(otherName == "Casa" || otherName == "Bar" || otherName == "WC"){
			Sala.Location roomToGo = Sala.Location.None;
			ChangeLocation(roomToGo);
		}
	}
	
	// max NOT included
	public virtual int GetRandom(int min, int max){
		System.Random rnd = new System.Random();
        int n = rnd.Next(min,max);
        return n;
	}

    public virtual bool PathComplete()
    {
        if (Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance + 2)
        {
            return true;
        }

        return false;
    }

}
