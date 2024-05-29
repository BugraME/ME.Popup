namespace ME.Popup.Html.Elements.Abstract;
/// <summary>
/// Represents an interface for defining properties of popup elements.
/// </summary>
public interface IPopupElement {
	/// <summary>
	/// Gets or sets the HTML ID of the popup element.
	/// </summary>
	string HtmlId { get; set; }

	/// <summary>
	/// Gets the HTML class of the popup element.
	/// </summary>
	string HtmlClass { get; }

	/// <summary>
	/// Gets or sets the CSS selector of the popup element.
	/// </summary>
	string Selector { get; set; }

	/// <summary>
	/// Gets or sets the name of the submit button associated with the popup element.
	/// </summary>
	string SubmitButton { get; set; }

	/// <summary>
	/// Gets or sets the name of the cancel button associated with the popup element.
	/// </summary>
	string CancelButton { get; set; }

	/// <summary>
	/// Gets or sets the number of columns in the layout of the popup element.
	/// </summary>
	int ColumnCount { get; set; }
}