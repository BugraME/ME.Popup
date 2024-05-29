namespace ME.Popup.Html.Elements.Abstract;
/// <summary>
/// Represents an interface for form elements in C#.
/// </summary>
public interface IFormElement : IHtmlElement {
	/// <summary>
	/// Gets or sets a value indicating whether the form element is required.
	/// </summary>
	bool Required { get; set; }
}