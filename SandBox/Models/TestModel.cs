using ME.Popup;
using ME.Popup.Attributes.Concrete.Form;
using ME.Popup.Attributes.Concrete.Form.File;
using ME.Popup.Attributes.Concrete.Form.Text;
using ME.Popup.Attributes.Concrete.General;
using System.ComponentModel.DataAnnotations;

namespace SandBox.Models;
public class TestModel2 {
	[Required] public int Id { get; set; }
	[Required, MaxLength(40), Label("Ad")] public string Name { get; set; }
	[Label("Tarih")] public DateTime Date { get; set; }
	[Order(2)] public bool IsEnabled { get; set; }
	[ImageReviewer, MultipleFiles] public IFormFile File { get; set; }
	[Required, MaxLength(200), Label("Adres"), TextArea, Order(1)] public string Address { get; set; }
	[InputType(InputTypes.DateTimeLocal)] public DateTime Time { get; set; }
}
public class TestModel {
	[Required] public int Id { get; set; }
	[Required] public string FirstName { get; set; }
	[Required] public string LastName { get; set; }
	[Required] public DateOnly BirthDate { get; set; }
	[Required] public TimeSpan BirthTime { get; set; }
	[TextArea(3), MaxLength(400)] public string Address { get; set; }
	[ImageReviewer, MultipleFiles] public IFormFile ProfilePhoto { get; set; }
	public bool IsLegal { get; set; }
}