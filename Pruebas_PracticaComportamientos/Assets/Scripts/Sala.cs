using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sala{
	
	public enum Location{None, Casa, Bar, WC};
	public static float timer;
	
	public static GameObject mCollCasa = GameObject.Find("Casa");
	public static GameObject mCollBar = GameObject.Find("Bar");
	public static GameObject mCollWC = GameObject.Find("WC");

    public static Vector3 GetRoomPosition(string roomName){
		GameObject room;
		if(roomName.Equals("Casa")){
			room = mCollCasa;
		}else if(roomName.Equals("Bar")){
			room = mCollBar;
		}else{
			room = mCollWC;
		}
		return room.transform.position;
	}
}
