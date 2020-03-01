namespace RogueGame.Data
{
	internal interface IAtackable
	{
		public int Damage { get; set; }
		public void Atack(ILivable livable);
	}
}