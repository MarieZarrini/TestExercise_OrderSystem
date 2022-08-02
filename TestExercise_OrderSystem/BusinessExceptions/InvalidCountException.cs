namespace OrderSystem.BusinessExceptions
{
	public class InvalidCountException : Exception
	{
		public override string Message => "Count cannot be less than or equal to 0 or more than 3";
	}
}
