using System.Drawing;

namespace MemorySecurIT.Classes
{
	public enum EtatCarte
	{
		Cachee,
		Revelee,
		Trouvee
	}

	public class Carte
	{
		public int Id { get; set; }
		public Image Image { get; set; }
		public EtatCarte Etat { get; set; }

		public Carte(int id, Image image)
		{
			Id = id;
			Image = image;
			Etat = EtatCarte.Cachee;
		}
	}
}
