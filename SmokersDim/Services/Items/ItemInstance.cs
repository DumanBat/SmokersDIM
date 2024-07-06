using System.Collections.Generic;

public class ItemInstance
{
	public CommonItemData item {get; set;}
	public InstanceData instance { get; set; }
	public PerksData perks { get; set; }
	public StatsData stats { get; set; }
	public DisplayProperties displayProperties {get; set;}
}

public class CommonItemData 
{
	public CharacterItem data {get; set;}
}

public class InstanceData
{
	public InstanceDetails data { get; set; }
}

public class InstanceDetails
{
	public int damageType { get; set; }
	public long damageTypeHash { get; set; }
	public PrimaryStat primaryStat { get; set; }
	public int itemLevel { get; set; }
	public int quality { get; set; }
	public bool isEquipped { get; set; }
	public bool canEquip { get; set; }
	public int equipRequiredLevel { get; set; }
	public List<long> unlockHashesRequiredToEquip { get; set; }
	public int cannotEquipReason { get; set; }
}

public class PrimaryStat
{
	public long statHash { get; set; }
	public int value { get; set; }
}

public class PerksData
{
	public Perks data { get; set; }
}

public class Perks
{
	public List<Perk> perks { get; set; }
}

public class Perk
{
	public long perkHash { get; set; }
	public string iconPath { get; set; }
	public bool isActive { get; set; }
	public bool visible { get; set; }
}

public class StatsData
{
	public Stats data { get; set; }
}

public class Stats
{
	public Dictionary<long, StatValue> stats { get; set; }
}

public class StatValue
{
	public long statHash { get; set; }
	public int value { get; set; }
}