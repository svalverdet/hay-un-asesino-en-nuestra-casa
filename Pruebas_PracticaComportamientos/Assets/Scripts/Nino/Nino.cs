using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Nino : Personaje {

	public enum Location{Casa, Bar, WC};
	
	// Instancia de la máquina de estados
	private FSM mFSM;
	private Location mLocation;
	private int mVejiga;
	
	/*
	public Nino(int id, string name): base(id, name){
		this.mVejiga = 2;
		this.mLocation = Location.Casa;
		
		this.mFSM = new FSM(this);
		this.mFSM.SetCurrentState(Jugando.GetInstance());
		this.mFSM.SetGlobalState(NinoGlobalState.GetInstance());
	}*/
	
	override public void UpdatePersonaje(){
		this.mFSM.Update();
		this.mVejiga+=2;
		
	}
	
	public FSM GetFSM(){ return this.mFSM;}
	public Location GetLocation(){ return this.mLocation;}
	public int GetVejiga(){ return this.mVejiga;}
	
	public void ChangeLocation(Location loc){ this.mLocation = loc;}
	public bool TienePis(){ return this.mVejiga >= 6;}	
	public void EfectosDelWC(){ this.mVejiga = 0;}
	
	
}
