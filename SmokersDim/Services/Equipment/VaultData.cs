[Serializable]
public class VaultData
{	
	public List<CharacterItem> allVaultItems {get; set;}
	public List<CharacterItem> generalItems {get; set;}
	public List<CharacterItem> consumables { get; set; }
	
	public List<CharacterItem> kinetic { get; set; }
	public List<CharacterItem> energy { get; set; }
	public List<CharacterItem> heavy { get; set; }
}