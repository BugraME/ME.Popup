using ME.Popup.Html.Elements.Abstract;

namespace ME.Popup.Html.Elements.Concrete.FormElements;
/// <summary>
/// Represents a form group.
/// </summary>
public class FormGroup {
	/// <summary>
	/// Initializes a new instance of the <see cref="FormGroup"/> class.
	/// </summary>
	public FormGroup() { }

	/// <summary>
	/// Initializes a new instance of the <see cref="FormGroup"/> class with a label and form element.
	/// </summary>
	/// <param name="label">The label for the form group.</param>
	/// <param name="element">The form element.</param>
	public FormGroup(Label label, IFormElement element) { Label = label; Element = element; }

	/// <summary>
	/// Initializes a new instance of the <see cref="FormGroup"/> class with a group action.
	/// </summary>
	/// <param name="groupAction">The action to be performed on the form group.</param>
	public FormGroup(Action<FormGroup> groupAction) => groupAction(this);

	/// <summary>
	/// Gets or sets the element to be prepended to the form group.
	/// </summary>
	public IHtmlElement PrependElement { get; set; } = null;

	/// <summary>
	/// Gets or sets the label for the form group.
	/// </summary>
	public Label Label { get; set; } = null;

	/// <summary>
	/// Gets or sets the form element.
	/// </summary>
	public IFormElement Element { get; set; }

	/// <summary>
	/// Gets or sets the order of the form group.
	/// </summary>
	public int? Order { get; set; } = null;
}