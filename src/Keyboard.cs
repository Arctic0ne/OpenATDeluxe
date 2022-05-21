using Godot;
using System;

public class Keyboard : Node2D {
    public Random r = new Random();

    public static Node2D airportInstance;
    public static Node2D myRoom;
    public static Vector2 myRoomPos;
    public static Vector2 entranceOffset;
    public static bool isStandardDoor;

    public override void _Ready() {
        GD.Print("Keyboard _Ready() called");
    }

    public static void moveToRoom(NodePath path) {
        airportInstance = RoomManager.GetRoomInstance("RoomAirport");
        entranceOffset = isStandardDoor ? new Vector2(0, 11.5f) : entranceOffset;
        GD.Print("moveToRoom path=" + path);
        GD.Print("airportInstance=" + airportInstance.Filename);
        myRoom = (Node2D)airportInstance.GetNode("Interactables/" + path);
        GD.Print("myRoom exists=" + myRoom != null);
        myRoomPos = myRoom.Position;
        //PlayerCharacter.instance.SetPath(myRoomPos);

        if (GameController.canPlayerInteract == false)
            return;

        if (RoomManager.currentRoom == "RoomAirport")
        {
            PlayerCharacter.instance.SetPath(new Vector2(myRoomPos.x + entranceOffset.x, myRoomPos.y + entranceOffset.y));
            Action<BaseCharacter> changeRoom = null;
            changeRoom = (c) => {
                PlayerCharacter.instance.OnGoalReached -= changeRoom;

                RoomManager.roomPosition = new Vector2(myRoomPos.x + entranceOffset.x, myRoomPos.y + entranceOffset.y);
                RoomManager.ChangeRoom("Room" + path.ToString(), false);
            };

            PlayerCharacter.instance.OnGoalReached += changeRoom;
        }
        else
        {
            RoomManager.ChangeRoom("RoomAirport", true);
        }
    }
}