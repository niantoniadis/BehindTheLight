using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Room sampleRoom;
    Room currentRoom; // the room the player is currently inside
    public List<Room> allRooms;
    public Dictionary<string, Room> loadedRooms;
    Camera main;

    void Start()
    {
        allRooms = new List<Room>();
        loadedRooms = new Dictionary<string, Room>();
        main = Camera.main;
        currentRoom = CreateRoom(0, 0);
        HandleRooms(currentRoom);
    }

    void Update()
    {

    }

    public Room CurrentRoom
    {
        get
        {
            return currentRoom;
        }
    }

    // sets the new current room of the map
    public void UpdatePlayerRoom(Player player)
    {
        int x, y;

        // figure out direction
        if(player.Position.x < currentRoom.GetXMin())
        {
            x = currentRoom.xCoord - 1;
            y = currentRoom.yCoord;
            currentRoom = loadedRooms["left"];
        }
        if (player.Position.x > currentRoom.GetXMax())
        {
            x = currentRoom.xCoord + 1;
            y = currentRoom.yCoord;
            currentRoom = loadedRooms["right"];
        }
        if (player.Position.y < currentRoom.GetYMin())
        {
            x = currentRoom.xCoord;
            y = currentRoom.yCoord - 1;
            currentRoom = loadedRooms["bottom"];
        }
        if (player.Position.y > currentRoom.GetYMax())
        {
            x = currentRoom.xCoord;
            y = currentRoom.yCoord + 1;
            currentRoom = loadedRooms["top"];
        }
        HandleRooms(currentRoom);
        // move camera
        main.transform.position = new Vector3(currentRoom.Center.x, currentRoom.Center.y, main.transform.position.z);
    }

    //loads and unloads loaded rooms 
    void HandleRooms(Room currentRoom)
    {
        foreach(KeyValuePair<string, Room> pair in loadedRooms)
        {
            loadedRooms[pair.Key] = null;
        }
        foreach(Room r in allRooms)
        {
            if(currentRoom.xCoord + 1 == r.xCoord)
            {
                loadedRooms["right"] = r;
            }
            if(currentRoom.xCoord - 1 == r.xCoord)
            {
                loadedRooms["left"] = r;
            }
            if(currentRoom.yCoord + 1 == r.yCoord)
            {
                loadedRooms["top"] = r;
            }
            if (currentRoom.yCoord - 1 == r.yCoord)
            {
                loadedRooms["bottom"] = r;
            }
        }
        foreach (KeyValuePair<string, Room> pair in loadedRooms)
        {
            if(loadedRooms[pair.Key] == null)
            {
                switch(pair.Key)
                {
                    case "right":
                        loadedRooms[pair.Key] = CreateRoom(currentRoom.xCoord + 1, currentRoom.yCoord);
                        break;
                    case "left":
                        loadedRooms[pair.Key] = CreateRoom(currentRoom.xCoord - 1, currentRoom.yCoord);
                        break;
                    case "top":
                        loadedRooms[pair.Key] = CreateRoom(currentRoom.xCoord, currentRoom.yCoord + 1);
                        break;
                    case "bottom":
                        loadedRooms[pair.Key] = CreateRoom(currentRoom.xCoord, currentRoom.yCoord - 1);
                        break;
                }
            }
        }
    }

    public Room CreateRoom(int x, int y)
    {
        Room newRoom = Instantiate(sampleRoom.gameObject, new Vector3(x * 2 * 8.886f, y * 10f, 0), Quaternion.identity).GetComponent<Room>();
        newRoom.Load(x, y);
        allRooms.Add(newRoom);
        return newRoom;
    }

    public List<Room> GetRooms()
    {
        return allRooms;
    }
}
