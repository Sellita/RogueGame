namespace RogueGame.Data
{
	public interface ILivable
	{
		int Health { get; set; }
		public void GetDamage(int atack);
	}
}