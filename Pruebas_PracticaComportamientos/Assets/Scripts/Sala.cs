using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sala{
	
	public enum Location{Casa, Bar, WC};
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
	
	private static Vector3 GetPointInRoom(string roomName){
		GameObject room;
		if(roomName.Equals("Casa")){
			room = mCollCasa;
		}else if(roomName.Equals("Bar")){
			room = mCollBar;
		}else{
			room = mCollWC;
		}
		
		//Vector3 size = room.GetComponent<Collider>().bounds.size;
		Vector3 center = room.transform.position;
		
		return center;
	}
	
	public static Vector3 GetDirToRoom(Personaje personaje, string room){
		Vector3 point = GetPointInRoom(room);
		Vector3 initialPos = personaje.transform.position;
		Vector3 dir = point-initialPos;
		dir.y = 0.0f;
		
		return Vector3.Normalize(dir);
	}
}
