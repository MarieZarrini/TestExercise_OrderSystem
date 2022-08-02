namespace OrderSystem.BusinessExceptions
{
	public class InvalidOrderStateException : Exception
	{
		public override string Message => "Your order state is invalid.";
	}
}
