namespace OrderSystem.BusinessExceptions
{
	public class NullOrderException : Exception
	{
		public override string Message => "The order does not exists.";
	}
}
