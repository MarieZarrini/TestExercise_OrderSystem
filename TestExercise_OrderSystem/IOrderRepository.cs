namespace OrderSystem
{
	public interface IOrderRepository
	{
		Order GetById(int id);
	}
}