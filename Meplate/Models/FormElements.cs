using Meplate.Enums;

namespace Meplate.Models;
public interface IFormElement : IHtmlElement {
    public bool Required { get; set; }
}

public abstract class FormElement : HtmlElement, IFormElement {
	public string Label { get; set; } = "";
    public string Placeholder { get; set; } = null;
    public bool Required { get; set; } = false;
}

public class Label : HtmlElement {
    public Label() {}
	public Label(Action<Label> labelAction) => labelAction(this);
	public Label(string value) => Value = value;
	public sealed override string Tag { get; set; } = HtmlTags.Label.GetEnumDescription();	
}

public class Input : FormElement {
    public Input(InputTypes inputType = InputTypes.Text, string value = "", string placeholder = "", bool required = false) {
        Type = inputType;
        Value = value;
        Placeholder = placeholder;
        Required = required;
    }
    public sealed override string Tag { get; set; } = HtmlTags.Input.GetEnumDescription();
    public sealed override bool Shorthand { get; set; } = true;
    public InputTypes Type { get; set; } = InputTypes.Text;
}

public class TextArea(int rows = 2, int? cols = null) : FormElement {
	public sealed override string Tag { get; set; } = HtmlTags.Textarea.GetEnumDescription();
    public sealed override bool Shorthand { get; set; } = false;
	public int Rows { get; set; } = rows;
	public int? Cols { get; set; } = cols;
}

public class Select : FormElement {
    public Select() {}
	public Select(IEnumerable<Option> options) => Nodes = options.Cast<IHtmlElement>().ToList();
	public Select(Type type) {
		if (!type.IsEnum) throw new ArgumentException("Type must be an enum type.");
		Nodes = EnumMethods.GetEnumValues(type).Select(x => new Option {
			Value = x.Value.ToString(),
			Text = string.IsNullOrWhiteSpace(x.Description) ? x.Name : x.Description,
		}).Cast<IHtmlElement>().ToList();
	}

	public sealed override string Tag { get; set; } = HtmlTags.Select.GetEnumDescription();
    public sealed override bool Shorthand { get; set; } = false;
}
public class Select<T> : FormElement where T : Enum {
	public Select() {
		Type type = typeof(T);
        if (!type.IsEnum) throw new ArgumentException("Type must be an enum type.");
		Nodes = EnumMethods.GetEnumValues(type).Select(x => new Option {
            Value = x.Value.ToString(),
            Text =  string.IsNullOrWhiteSpace(x.Description) ? x.Name : x.Description,
		}).Cast<IHtmlElement>().ToList();
	}

	public sealed override string Tag { get; set; } = HtmlTags.Select.GetEnumDescription();
    public sealed override bool Shorthand { get; set; } = false;
    public IEnumerable<Option> Options { get; set; } = [];
}

public class Option : HtmlElement {
    public sealed override string Tag { get; set; } = HtmlTags.Option.GetEnumDescription();
    public string Text { get; set; } = "";
    public bool Selected { get; set; } = false;
    public bool Disabled { get; set; } = false;
    public int? GroupId { get; set; } = null;
}