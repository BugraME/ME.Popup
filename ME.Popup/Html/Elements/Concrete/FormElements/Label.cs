using ME.Popup.Html.Elements.Base;

namespace ME.Popup.Html.Elements.Concrete.FormElements;
/// <summary>
/// Represents a label HTML element.
/// </summary>
public class Label : HtmlElement {
	/// <summary>
	/// Initializes a new instance of the <see cref="Label"/> class.
	/// </summary>
	public Label() { }

	/// <summary>
	/// Initializes a new instance of the <see cref="Label"/> class with the specified actions.
	/// </summary>
	/// <param name="labelAction">The action to be performed on the label.</param>
	public Label(Action<Label> labelAction) => labelAction(this);

	/// <summary>
	/// Initializes a new instance of the <see cref="Label"/> class with the specified value.
	/// </summary>
	/// <param name="value">The value of the label.</param>
	public Label(string value) => Value = value;

	/// <summary>
	/// Gets or sets the HTML tag of the label element.
	/// </summary>
	public sealed override string Tag { get; set; } = HtmlTags.Label.GetEnumDescription();
}