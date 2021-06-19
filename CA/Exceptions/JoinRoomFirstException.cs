namespace CA.Exceptions
{
	using System;
	public class JoinRoomFirstException :
		NotSupportedException
	{
		public JoinRoomFirstException() : base
		(
			"You need to join the room first!"
		)
		{ }
	}
}
