namespace RogueGame.Data
{
	internal interface IAtackable
	{
		public int Damage { get; set; }
		public int Atack(ILivable livable);
	}
}