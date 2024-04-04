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
    public Label(string value) => Value = value;
	public override string Tag { get; set; } = HtmlTags.Label.GetEnumDescription();	
}

public class Input : FormElement {
    public Input(InputTypes inputType = InputTypes.Text, string value = "", string placeholder = "", bool required = false) {
        Type = inputType;
        Value = value;
        Placeholder = placeholder;
        Required = required;
    }
    public override string Tag { get; set; } = HtmlTags.Input.GetEnumDescription();
    public override bool Shorthand { get; set; } = true;
    public InputTypes Type { get; set; } = InputTypes.Text;
}

public class TextArea : FormElement {
    public override string Tag { get; set; } = HtmlTags.Textarea.GetEnumDescription();
    public override bool Shorthand { get; set; } = false;
    public int? Cols { get; set; } = null;
    public int Rows { get; set; } = 2;
}

public class Select : FormElement {
    public override string Tag { get; set; } = HtmlTags.Select.GetEnumDescription();
    public override bool Shorthand { get; set; } = false;
    public IEnumerable<Option> Options { get; set; } = [];
}

public class Option : HtmlElement {
    public override string Tag { get; set; } = HtmlTags.Option.GetEnumDescription();
    public string Text { get; set; } = "";
    public bool Selected { get; set; } = false;
    public bool Disabled { get; set; } = false;
    public int? GroupId { get; set; } = null;
}