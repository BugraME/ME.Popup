using ME.Popup.Html.Elements.Abstract;
using ME.Popup.Html.Elements.Base;

namespace ME.Popup.Html.Elements.Concrete.FormElements;
/// <summary>
/// Represents an HTML <select> element.
/// </summary>
public class Select : FormElement {
	/// <summary>
	/// Initializes a new instance of the Select class.
	/// </summary>
	public Select() { }

	/// <summary>
	/// Initializes a new instance of the Select class with the provided options.
	/// </summary>
	/// <param name="options">The options to populate the select element.</param>
	public Select(IEnumerable<Option> options) => Nodes = options.Cast<IHtmlElement>().ToList();

	/// <summary>
	/// Initializes a new instance of the Select class with options generated from the provided enum type.
	/// </summary>
	/// <param name="type">The enum type used to generate options.</param>
	/// <exception cref="ArgumentException">Thrown when the provided type is not an enum.</exception>
	public Select(Type type) {
		if (!type.IsEnum) throw new ArgumentException("Type must be an enum type.");
		Nodes = EnumMethods.GetEnumValues(type).Select(x => new Option {
			Value = x.Value.ToString(),
			Text = string.IsNullOrWhiteSpace(x.Description) ? x.Name : x.Description,
		}).Cast<IHtmlElement>().ToList();
	}

	/// <summary>
	/// Gets or sets the HTML tag for the select element.
	/// </summary>
	public sealed override string Tag { get; set; } = HtmlTags.Select.GetEnumDescription();

	/// <summary>
	/// Gets or sets whether the select element uses shorthand syntax.
	/// </summary>
	public sealed override bool Shorthand { get; set; } = false;
}

/// <summary>
/// Represents an HTML <select> element with options generated from a specified enum type.
/// </summary>
/// <typeparam name="T">The enum type used to generate options.</typeparam>
public class Select<T> : FormElement where T : Enum {
	/// <summary>
	/// Initializes a new instance of the Select class with options generated from the specified enum type.
	/// </summary>
	/// <exception cref="ArgumentException">Thrown when T is not an enum.</exception>
	public Select() {
		Type type = typeof(T);
		if (!type.IsEnum) throw new ArgumentException("Type must be an enum type.");
		Nodes = EnumMethods.GetEnumValues(type).Select(x => new Option {
			Value = x.Value.ToString(),
			Text = string.IsNullOrWhiteSpace(x.Description) ? x.Name : x.Description,
		}).Cast<IHtmlElement>().ToList();
	}

	/// <summary>
	/// Gets or sets the HTML tag for the select element.
	/// </summary>
	public sealed override string Tag { get; set; } = HtmlTags.Select.GetEnumDescription();

	/// <summary>
	/// Gets or sets whether the select element uses shorthand syntax.
	/// </summary>
	public sealed override bool Shorthand { get; set; } = false;

	/// <summary>
	/// Gets or sets the options of the select element.
	/// </summary>
	public IEnumerable<Option> Options { get; set; } = [];
}