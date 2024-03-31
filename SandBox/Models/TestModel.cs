using static ME.Popup.Enums;

namespace SandBox.Models;
public class TestModel {
	public int Id { get; set; }
	public string Name { get; set; }
	public DateTime Date { get; set; }
	public bool IsEnabled { get; set; }
	public PopupType Type { get; set; }
	public IFormFile File { get; set; }
}