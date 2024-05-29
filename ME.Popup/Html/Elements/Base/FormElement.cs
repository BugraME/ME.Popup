using ME.Popup.Html.Elements.Abstract;

namespace ME.Popup.Html.Elements.Base;
/// <summary>
/// Represents a form element in HTML, inheriting from HtmlElement and implementing IFormElement interface.
/// </summary>
public class FormElement : HtmlElement, IFormElement {
	/// <summary>
	/// Gets or sets the HTML tag of the form element.
	/// </summary>
	public override string Tag => HtmlTags.Form.ToString();

	/// <summary>
	/// Gets or sets the label associated with the form element.
	/// </summary>
	public string Label { get; set; } = "";

	/// <summary>
	/// Gets or sets the placeholder text for the form element.
	/// </summary>
	public string Placeholder { get; set; } = null;

	/// <summary>
	/// Gets or sets a value indicating whether the form element is required.
	/// </summary>
	public bool Required { get; set; } = false;
}