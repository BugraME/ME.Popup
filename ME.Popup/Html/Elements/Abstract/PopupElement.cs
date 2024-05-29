namespace ME.Popup.Html.Elements.Abstract;
/// <summary>
/// Represents a base class for elements in a popup.
/// </summary>
public abstract class PopupElement : IPopupElement {
	private string _htmlClass;

	/// <summary>
	/// Gets or sets the HTML class attribute of the element.
	/// </summary>
	public string HtmlClass => _htmlClass;

	/// <summary>
	/// Gets or sets the number of columns in the element.
	/// </summary>
	public virtual int ColumnCount { get; set; }

	/// <summary>
	/// Gets or sets the HTML id attribute of the element.
	/// </summary>
	public virtual string HtmlId { get; set; }

	/// <summary>
	/// Gets or sets the CSS selector for the element.
	/// </summary>
	public virtual string Selector { get; set; } = null;

	/// <summary>
	/// Gets or sets the text for the submit button associated with the element.
	/// </summary>
	public virtual string SubmitButton { get; set; } = "Submit";

	/// <summary>
	/// Gets or sets the text for the cancel button associated with the element.
	/// </summary>
	public virtual string CancelButton { get; set; } = null;

	/// <summary>
	/// Adds one or more CSS classes to the element.
	/// </summary>
	/// <param name="className">The CSS classes to add.</param>
	public virtual void AddClass(params string[] className) => _htmlClass = string.Join(" ", className);
}
