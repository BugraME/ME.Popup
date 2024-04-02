using ME.Popup.Enums;
using ME.Popup.Extensions.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SandBox.Models;
public class TestModel2 {
	[Required] public int Id { get; set; }
	[Required, MaxLength(40), Label("Ad")] public string Name { get; set; }
	[Label("Tarih")] public DateTime Date { get; set; }
	[Order(2)] public bool IsEnabled { get; set; }
	[Order(3)] public PopupType Type { get; set; }
	[ImageReviewer, MultipleFiles] public IFormFile File { get; set; }
	[Required, MaxLength(200), Label("Adres"), TextArea(2), Order(1)] public string Address { get; set; }
	[InputType(InputType.Color)] public DateTime Time { get; set; }
}

public class TestModel {
	[InputType(InputType.Hidden), HideLabel, Required] public int Id { get; set; }
	[Label("Ad"), Required] public string FirstName { get; set; }
	[Label("Soyad"), Id("soyadId"), Class("parent", "student"), HideLabel, Placeholder("Soyad"), Name("soyad"), Required] public string LastName { get; set; }
	[Label("Doğum Tarihi"), CustomHtml("data-id", "2"), CustomHtml("data-type", "numeric"), Required] public DateOnly BirthDate { get; set; }
	[Label("Doğum Saati"), Required] public TimeSpan BirthTime { get; set; }
	[Label("Adres"), TextArea(3), MaxLength(400)] public string Address { get; set; }
	[Label("Profil Fotoğrafı "), ImageReviewer] public IFormFile ProfilePhoto { get; set; }
}