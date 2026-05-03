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
		public string CheminImage { get; set; }
		public EtatCarte Etat { get; set; }

		// initialise une carte avec son id et image
		public Carte(int id, string cheminImage)
		{
			Id = id;
			CheminImage = cheminImage;
			Image = null;
			Etat = EtatCarte.Cachee;
		}
	}
}
