using ME.Popup.Enums;
using ME.Popup.Extensions.Attributes;

namespace SandBox.Models;
public class HalilModel {
	[InputType(InputType.Hidden), HideLabel, Name("id"), Class("me-3", "ms-3"), CustomHtml("aycicek")] public int Id { get; set; }
	[Label("Ad")] public string FirstName { get; set; }
	public string LastName { get; set; }
	public DateTime BirthDate { get; set; }
	[TextArea(5)] public string Address { get; set; }
	[ImageReviewer] public IFormFile ProfilePhoto { get; set; }
	public PopupType Gender { get;}

}