namespace CA.Exceptions
{
	using System;
	public class AlreadyEnteredException :
		NotSupportedException
	{
		public AlreadyEnteredException() : base
		(
			"UserName already entered the room!"
		) { }
	}
}
