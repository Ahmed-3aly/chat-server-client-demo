namespace CA.Exceptions
{
	using System;
	public class DuplicateUserNameException :
		NotSupportedException
	{
		public DuplicateUserNameException() : base
		(
			"UserName is already connected!"
		) { }
	}
}
