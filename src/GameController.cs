using Godot;
using System;
using System.Threading.Tasks;

public class GameController : Node2D {
	public static GameController instance;

	// static GameController() {
	// 	void HandleTaskException(object e) {
	// 		Task t = e as Task;
	//
	// 		GD.Print($"Unhandled exception in task!\n{t.Exception.InnerException.Message}\n{t.Exception.InnerException.StackTrace}");
	// 	}
	// 	TaskScheduler.UnobservedTaskException += (o, e) => HandleTaskException(o);
	// }

	public static int currentPlayerID = 2;
	public static string CurrentPlayerTag {
		get {
			return "P" + currentPlayerID;
		}
	}

	public void SetTaskbar(bool toggle) {
		taskbar.SetVisible(toggle);
	}

	[Export]
	public NodePath _taskbar;
	public Control taskbar;

	public static string[] playerCompanyNames = { "Sunshine Airways", "Falcon Lines", "Phönix Travel", "Honey Airlines" };
	public static string[] playerNames = { "Tina Cortez", "Siggi Sorglos", "Igor Tuppolevsky", "Mario Zucchero" };

	public static Action onUnhandledInput;
	public static bool canPlayerInteract = true;

	public static Random r = new Random();

	public static bool fastForward = false;

	public override void _Ready() {
		instance = this;
		taskbar = GetNode<Control>(_taskbar);

		RoomManager.ChangeRoom("RoomMainMenu", isAirport: false);
		GetTree().Connect("screen_resized", this, "OnScreenSizeChanged");
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventKey k)
		{
			//			fastForward = false;
			//			if (k.Scancode == (int)KeyList.Space) {
			//				//onUnhandledInput?.Invoke();
			//				fastForward = k.Pressed;
			//			}
			switch (k.Scancode)
			{
				// Speed up time x20
				case (int)KeyList.Space:
					//onUnhandledInput?.Invoke();
					fastForward = k.Pressed;
					break;
				// Move to Petrol Air
				case (int)KeyList.A:
					RoomManager.ChangeRoom("RoomArabAir", false);
					break;
				// Move to Bank
				case (int)KeyList.B:
					RoomManager.ChangeRoom("RoomBank", false);
					break;
				// Move to Rick's Cafe
				case (int)KeyList.C:
					RoomManager.ChangeRoom("RoomCafe", false);
					break;
				// Move to Duty-Free Shop
				case (int)KeyList.D:
					RoomManager.ChangeRoom("RoomDutyFree", false);
					break;
				// Move to plane dealer
				//case (int)KeyList.E:
				//	RoomManager.ChangeRoom("RoomPlaneDealer", false);
				//	break;
				// Move to Office and open Globe
				//case (int)KeyList.G:
				//	RoomManager.ChangeRoom("RoomGlobe", false);
				//	break;
				// Move to cargo office
				case (int)KeyList.H:
					RoomManager.ChangeRoom("RoomCargo", false);
					break;
				// Move to advertising agency
				case (int)KeyList.I:
					RoomManager.ChangeRoom("RoomMarketing", false);
					break;
				// Move to newspaper stand
				case (int)KeyList.K:
					RoomManager.ChangeRoom("RoomKiosk", false);
					break;
				// Move to Last Minute counter
				case (int)KeyList.L:
					RoomManager.ChangeRoom("RoomLastMinute", false);
					break;
				// Move to museum
				case (int)KeyList.M:
					RoomManager.ChangeRoom("RoomMuseum", false);
					break;
				// Move to NASA Shop
				//case (int)KeyList.N:
				//	RoomManager.ChangeRoom("RoomNASA", false);
				//	break;
				// Move to your Office
				case (int)KeyList.O:
					RoomManager.ChangeRoom("RoomOffice", false);
					break;
				// Move to Personnel Office
				case (int)KeyList.P:
					RoomManager.ChangeRoom("RoomPersonnel", false);
					break;
				// Move to Route Management Board
				//case (int)KeyList.R:
				//	RoomManager.ChangeRoom("RoomRoute", false);
				//	break;
				// Move to Telescope (Check score)
				case (int)KeyList.S:
					RoomManager.ChangeRoom("RoomScore", false);
					break;
				// Move to Air Travel Counter
				case (int)KeyList.T:
					RoomManager.ChangeRoom("RoomAirTravel", false);
					break;
				// Move to airport management (Mr. Uhrig)
				case (int)KeyList.U:
					RoomManager.ChangeRoom("RoomManager", false);
					break;
				// Move to Workshop
				//case (int)KeyList.W:
				//	RoomManager.ChangeRoom("RoomWorkshop", false);
				//	break;
				// Move to Hi-Tech Design Shop
				//case (int)KeyList.X:
				//	RoomManager.ChangeRoom("RoomDesign", false);
				//	break;
				// Move to Security Office
				//case (int)KeyList.Y:
				//	RoomManager.ChangeRoom("RoomSecurity", false);
				//	break;
			}
		}
		if (@event is InputEventMouseButton m)
		{
			OnMouseClick(m);
		}
	}

	public static void OnMouseClick(InputEventMouseButton m) {
		if (m.IsPressed() && m.ButtonIndex == (int)ButtonList.Left) {
			onUnhandledInput?.Invoke();
		}
	}

	override public void _Process(float delta) {
		TimeScale = fastForward ? 20:1;

	}

	public static int TimeScale { get; set; }

	public void OnScreenSizeChanged() {
		//GetViewport().SetSizeOverride(true, new Vector2(OS.GetWindowSize().x, GetViewportRect().Size.y));
		// Vector2 screenSize = OS.GetWindowSize();
		// Viewport viewport = GetViewport();

		// float scaleX = Mathf.Floor(screenSize.x / viewport.Size.x);
		// float scaleY = Mathf.Floor(screenSize.y / viewport.Size.y);

		// float scale = Mathf.Max(1, Mathf.Min(scaleX, scaleY));

		// Vector2 diffHalf = ((screenSize - (viewport.Size * scale)) / 2).Floor();

		// viewport.SetAttachToScreenRect(new Rect2(diffHalf, viewport.Size * scale));
	}
}
