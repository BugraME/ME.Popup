namespace ME.Popup.Html.Elements.Abstract;
/// <summary>
/// Represents an HTML element.
/// </summary>
public interface IHtmlElement {
	/// <summary>
	/// Gets or sets the tag of the HTML element.
	/// </summary>
	string Tag { get; set; }

	/// <summary>
	/// Gets or sets the ID attribute of the HTML element.
	/// </summary>
	string Id { get; set; }

	/// <summary>
	/// Gets or sets the class attribute of the HTML element.
	/// </summary>
	string Class { get; set; }

	/// <summary>
	/// Gets or sets the name attribute of the HTML element.
	/// </summary>
	string Name { get; set; }

	/// <summary>
	/// Gets or sets the value attribute of the HTML element.
	/// </summary>
	string Value { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether the HTML element uses shorthand notation.
	/// </summary>
	bool Shorthand { get; set; }

	/// <summary>
	/// Gets or sets the additional attributes of the HTML element.
	/// </summary>
	string Attributes { get; set; }

	/// <summary>
	/// Gets or sets the child nodes of the HTML element.
	/// </summary>
	IEnumerable<IHtmlElement> Nodes { get; set; }

	/// <summary>
	/// Adds one or more CSS class names to the HTML element.
	/// </summary>
	/// <param name="className">The class names to add.</param>
	void AddClass(params string[] className);

	/// <summary>
	/// Adds a custom attribute to the HTML element.
	/// </summary>
	/// <param name="key">The key of the custom attribute.</param>
	/// <param name="value">The value of the custom attribute.</param>
	void AddCustomAttribute(string key, string value);

	/// <summary>
	/// Adds a custom attribute to the HTML element.
	/// </summary>
	/// <param name="keyValue">A tuple representing the key-value pair of the custom attribute.</param>
	void AddCustomAttribute((string, string) keyValue);
}