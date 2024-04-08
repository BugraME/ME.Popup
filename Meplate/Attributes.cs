using Meplate.Enums;
namespace Meplate.Attributes;
public interface IHtmlAttribute {
	(string, string) HtmlAttribute { get; }
}
[AttributeUsage(AttributeTargets.Property)] public class PropertyAttribute : Attribute { }
public class BoolAttribute : PropertyAttribute {
	public bool Status { get; }
	public BoolAttribute() => Status = true;
	public BoolAttribute(bool status) => Status = status;
}
public class ValueAttribute<T>(T value) : PropertyAttribute { public T Value { get; } = value; }

#region General
public class LabelAttribute(string value) : ValueAttribute<string>(value) { }
public class HideLabelAttribute : BoolAttribute { }
public class IdAttribute(string value) : ValueAttribute<string>(value), IHtmlAttribute {
	private readonly string _value = value;
	public (string, string) HtmlAttribute => ("id", _value);
}
public class ClassAttribute(params string[] values) : PropertyAttribute, IHtmlAttribute {
	private readonly string[] _values = values;
	public (string, string) HtmlAttribute => ("class", string.Join(' ', _values));
}

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class CustomHtmlAttribute : PropertyAttribute, IHtmlAttribute {
	private readonly (string, string) _attribute;
	public CustomHtmlAttribute(string key) {
		if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key));
		_attribute = (key, key);
	}
	public CustomHtmlAttribute(string key, string value) {
		if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key));
		if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException($"'{nameof(value)}' cannot be null or empty.", nameof(value));
		_attribute = (key, value);
	}
	public (string, string) HtmlAttribute => _attribute;
}
public class NameAttribute(string value) : ValueAttribute<string>(value) { }
public class InputTypeAttribute(InputTypes type) : ValueAttribute<InputTypes>(type) { }
public class OrderAttribute(int value) : ValueAttribute<int>(value) { }
#endregion

#region Text
public class PlaceholderAttribute(string value) : ValueAttribute<string>(value), IHtmlAttribute {
	private readonly string _value = value;
	public (string, string) HtmlAttribute => ("placeholder", _value);
}
public class TextAreaAttribute(int rows) : ValueAttribute<int>(rows), IHtmlAttribute {
	private readonly int _rows = rows;
	public (string, string) HtmlAttribute => ("rows", _rows.ToString());
}
#endregion

#region File
public class ImageReviewerAttribute : BoolAttribute { }
public class MultipleFilesAttribute : BoolAttribute, IHtmlAttribute { public (string, string) HtmlAttribute => ("multiple","multiple"); }
#endregion