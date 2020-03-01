namespace RogueGame.Data
{
	public interface ILivable
	{
		int Health { get; set; }
		public int GetDamage(int atack);
	}
}