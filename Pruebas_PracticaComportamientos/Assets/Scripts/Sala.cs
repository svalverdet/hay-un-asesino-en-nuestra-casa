using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sala{
	
	
	public static float timer;
	
	// Locations and colliders
	public enum Location{None, Salon, Cocina, Entrada, WC, WC2, HabitacionNino, Bar};
	
	public static List<GameObject> mColliders = new List<GameObject>();
	public static string[] tags = {"Salon", "Cocina", "Entrada", "WC", "WC2", "HabitacionNino", "Bar"};
	
	public static GameObject mCollSalon = GameObject.FindWithTag("Salon");
	public static GameObject mCollCocina = GameObject.FindWithTag("Cocina");
	public static GameObject mCollEntrada = GameObject.FindWithTag("Entrada");
	public static GameObject mCollWC = GameObject.FindWithTag("WC");
	public static GameObject mCollWC2 = GameObject.FindWithTag("WC2");
	public static GameObject mCollHabitacionNino = GameObject.FindWithTag("HabitacionNino");
	public static GameObject mCollBar = GameObject.FindWithTag("Bar");
	
	public static List<GameObject> mSeats = new List<GameObject>();
	
	public static GameObject mCollSeat1 = GameObject.Find("Seat1");
    public static GameObject mCollSeat2 = GameObject.Find("Seat2");
	
	public static void SetList(){
		mColliders.Add(mCollSalon);
		mColliders.Add(mCollCocina);
		mColliders.Add(mCollEntrada);
		mColliders.Add(mCollWC);
		mColliders.Add(mCollWC2);
		mColliders.Add(mCollHabitacionNino);
		mColliders.Add(mCollBar);
		
		mSeats.Add(mCollSeat1);
		mSeats.Add(mCollSeat2);
	}

	public static Vector3 GetRoomPosition(Location roomName){
		GameObject room;
		
		switch(roomName){
			case Location.Salon:
			room = mCollSalon;
			break;
			case Location.Cocina:
			room = mCollCocina;
			break;
			case Location.Entrada:
			room = mCollEntrada;
			break;
			case Location.WC:
			room = mCollWC;
			break;
			case Location.WC2:
			room = mCollWC2;
			break;
			case Location.HabitacionNino:
			room = mCollHabitacionNino;
			break;
			case Location.Bar:
			room = mCollBar;
			break;
			default:
			room = mCollEntrada;
			Debug.Log("ERROR ERROR ERROR");
			break;
		}
		
		return room.transform.position;
	}
	
	public static Vector3 GetRandomRoomPositionExcept(Location[] rooms){
		
		// Preparar la lista de las posibles localizaciones a las que ir
		List<Location> avaiableLocations = new List<Location>();
		foreach (Location loc in Location.GetValues(typeof(Location)))
		{
		   avaiableLocations.Add(loc);
		}
		avaiableLocations.Remove(Location.None);
        foreach (Location loc in rooms)
        {
            avaiableLocations.Remove(loc);
        }
		
		// Tomar una de ellas al azar
		int index = Mathf.FloorToInt(Random.value * avaiableLocations.Count);
        Location nextLocation = avaiableLocations[index];
		
		// Buscar su posicion
		return GetRoomPosition(nextLocation);
		
	}
	
	//Devuelve el asiento más cercano
    public static Vector3 GetSeatLocation(Personaje personaje){
        float distance = Mathf.Infinity;
        float distanceToCharacter = Mathf.Infinity;
        Vector3 actualPos = new Vector3(0.0f, 0.0f, 0.0f);
        foreach (GameObject c in mSeats)
        {
            distanceToCharacter = (personaje.transform.position - c.transform.position).magnitude;
            if (distance > distanceToCharacter)
            {
                distance = distanceToCharacter;
                actualPos = c.transform.position;
            }
        }
        return actualPos;
    }
}
