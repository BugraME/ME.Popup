using ME.Popup.Html.Elements.Abstract;

namespace ME.Popup.Html.Elements.Base;
/// <summary>
/// Represents an HTML element.
/// </summary>
public class HtmlElement : IHtmlElement {
	/// <summary>
	/// Initializes a new instance of the <see cref="HtmlElement"/> class.
	/// </summary>
	public HtmlElement() { }

	/// <summary>
	/// Initializes a new instance of the <see cref="HtmlElement"/> class with the specified HTML tag.
	/// </summary>
	/// <param name="tag">The HTML tag.</param>
	public HtmlElement(HtmlTags tag) => Tag = tag.GetEnumDescription();

	/// <summary>
	/// Initializes a new instance of the <see cref="HtmlElement"/> class with the specified HTML tag and value.
	/// </summary>
	/// <param name="tag">The HTML tag.</param>
	/// <param name="value">The value of the element.</param>
	public HtmlElement(HtmlTags tag, string value) { Tag = tag.GetEnumDescription(); Value = value; }

	/// <summary>
	/// Gets or sets the HTML tag of the element.
	/// </summary>
	public virtual string Tag { get; set; } = HtmlTags.Div.GetEnumDescription();

	/// <summary>
	/// Gets or sets the ID attribute of the element.
	/// </summary>
	public virtual string Id { get; set; }

	private string _class = string.Empty;

	/// <summary>
	/// Gets or sets the class attribute of the element.
	/// </summary>
	public virtual string Class {
		get => _class.Trim();
		set => _class = value;
	}

	/// <summary>
	/// Gets or sets the name attribute of the element.
	/// </summary>
	public virtual string Name { get; set; }

	/// <summary>
	/// Gets or sets the value of the element.
	/// </summary>
	public virtual string Value { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether the element uses shorthand notation.
	/// </summary>
	public virtual bool Shorthand { get; set; }

	/// <summary>
	/// Gets or sets the additional attributes of the element.
	/// </summary>
	public virtual string Attributes { get; set; }

	/// <summary>
	/// Gets or sets the child nodes of the element.
	/// </summary>
	public virtual IEnumerable<IHtmlElement> Nodes { get; set; } = [];

	/// <summary>
	/// Adds one or more CSS classes to the element.
	/// </summary>
	/// <param name="className">The CSS class name(s) to add.</param>
	public virtual void AddClass(params string[] className)
		=> Class += " " + string.Join(" ", className);

	/// <summary>
	/// Adds a custom attribute to the element.
	/// </summary>
	/// <param name="key">The name of the attribute.</param>
	/// <param name="value">The value of the attribute.</param>
	public virtual void AddCustomAttribute(string key, string value)
		=> Attributes += " " + $"{key}='{value}'";

	/// <summary>
	/// Adds a custom attribute to the element.
	/// </summary>
	/// <param name="keyValue">A tuple representing the attribute name and value.</param>
	public virtual void AddCustomAttribute((string, string) keyValue)
		=> Attributes += " " + $"{keyValue.Item1}='{keyValue.Item2}'";
}