namespace OrderSystem.BusinessExceptions
{
	public class SetStateToShippedException : Exception
	{
		public override string Message => "Order state cannot change to Shipped.";
	}
}
