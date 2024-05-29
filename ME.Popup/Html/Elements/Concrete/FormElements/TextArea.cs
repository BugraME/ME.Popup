using ME.Popup.Html.Elements.Base;

namespace ME.Popup.Html.Elements.Concrete.FormElements;
/// <summary>
/// Represents a TextArea form element that can be used in web forms.
/// </summary>
public class TextArea(int rows = 2, int? cols = null) : FormElement {
	/// <summary>
	/// Gets or sets the HTML tag associated with the TextArea element.
	/// </summary>
	public sealed override string Tag { get; set; } = HtmlTags.Textarea.GetEnumDescription();

	/// <summary>
	/// Gets or sets whether the TextArea supports shorthand notation.
	/// </summary>
	public sealed override bool Shorthand { get; set; } = false;

	/// <summary>
	/// Gets or sets the number of rows for the TextArea.
	/// </summary>
	public int Rows { get; set; } = rows;

	/// <summary>
	/// Gets or sets the number of columns for the TextArea.
	/// </summary>
	public int? Cols { get; set; } = cols;
}