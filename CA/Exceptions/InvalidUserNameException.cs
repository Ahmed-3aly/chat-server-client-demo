namespace CA.Exceptions
{
	using System;
	public class InvalidUserNameException :
		ArgumentException
	{
		public InvalidUserNameException() : base
		(
			"UserName does not exist!"
		)
		{ }
	}
}
