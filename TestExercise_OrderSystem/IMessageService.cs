namespace OrderSystem
{
	public interface IMessageService
	{
		void Send(string message, string phoneNumber);
	}
}