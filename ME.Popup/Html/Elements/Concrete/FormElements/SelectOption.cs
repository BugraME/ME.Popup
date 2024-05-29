using ME.Popup.Html.Elements.Base;

namespace ME.Popup.Html.Elements.Concrete.FormElements;
/// <summary>
/// Represents an <c>&lt;option&gt;</c> element within an HTML form.
/// </summary>
public class Option : HtmlElement {
	/// <summary>
	/// Gets or sets the tag name for the <c>&lt;option&gt;</c> element.
	/// </summary>
	public sealed override string Tag { get; set; } = HtmlTags.Option.GetEnumDescription();

	/// <summary>
	/// Gets or sets the text content of the <c>&lt;option&gt;</c> element.
	/// </summary>
	public string Text { get; set; } = "";

	/// <summary>
	/// Gets or sets a value indicating whether this <c>&lt;option&gt;</c> element is selected.
	/// </summary>
	public bool Selected { get; set; } = false;

	/// <summary>
	/// Gets or sets a value indicating whether this <c>&lt;option&gt;</c> element is disabled.
	/// </summary>
	public bool Disabled { get; set; } = false;

	/// <summary>
	/// Gets or sets the group ID of this <c>&lt;option&gt;</c> element.
	/// </summary>
	public int? GroupId { get; set; } = null;
}