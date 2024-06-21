using System.Text.Json.Serialization;

 [Serializable]
public class EquipmentDatas
 {
 	public List<EquipmentData> equipmentDatas { get; set; }
 }
 
 [Serializable]
 public class EquipmentData
{
	public string characterId { get; set; }
	public List<Item> items { get; set; }
}

 [Serializable]
public class Item
{
	public string itemHash { get; set; }
	public string itemInstanceId { get; set; }
	public int quantity { get; set; }
	public int bindStatus { get; set; }
	public int location { get; set; }
	public string bucketHash { get; set; }
	public int transferStatus { get; set; }
	public bool lockable { get; set; }
	public int state { get; set; }
	public int dismantlePermission { get; set; }
	public bool isWrapper { get; set; }
	public List<int> tooltipNotificationIndexes { get; set; }
	public int versionNumber { get; set; }
	public string overrideStyleItemHash { get; set; }
}
