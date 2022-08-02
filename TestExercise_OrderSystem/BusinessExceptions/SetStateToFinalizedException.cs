namespace OrderSystem.BusinessExceptions
{
	public class SetStateToFinalizedException : Exception
	{
		public override string Message => "Order state cannot change to finalized.";
	}
}