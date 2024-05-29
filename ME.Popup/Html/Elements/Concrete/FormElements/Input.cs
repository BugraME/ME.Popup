using ME.Popup.Html.Elements.Base;

namespace ME.Popup.Html.Elements.Concrete.FormElements;
/// <summary>
/// Represents an input element in a form.
/// </summary>
public class Input : FormElement {
	/// <summary>
	/// Initializes a new instance of the Input class with specified parameters.
	/// </summary>
	/// <param name="inputType">The type of input (default is Text).</param>
	/// <param name="value">The value of the input (default is empty).</param>
	/// <param name="placeholder">The placeholder text (default is empty).</param>
	/// <param name="required">Specifies whether the input is required (default is false).</param>
	public Input(InputTypes inputType = InputTypes.Text, string value = "", string placeholder = "", bool required = false) {
		Type = inputType;
		Value = value;
		Placeholder = placeholder;
		Required = required;
	}

	/// <summary>
	/// Gets or sets the HTML tag associated with the input element.
	/// </summary>
	public sealed override string Tag { get; set; } = HtmlTags.Input.GetEnumDescription();

	/// <summary>
	/// Gets or sets a value indicating whether the input element supports shorthand notation.
	/// </summary>
	public sealed override bool Shorthand { get; set; } = true;

	/// <summary>
	/// Gets or sets the type of the input element.
	/// </summary>
	public InputTypes Type { get; set; } = InputTypes.Text;
}
