using HtmlAgilityPack;
using ME.Popup.Enums;

namespace ME.Popup.Extensions.Attributes;
public interface IHtmlAttribute { HtmlAttribute HtmlAttribute { get; } }
[AttributeUsage(AttributeTargets.Property)] public class PropertyAttribute : Attribute { protected HtmlDocument Document = new(); }
public class BoolAttribute : PropertyAttribute {
	public bool Status { get; }
	public BoolAttribute() => Status = true;
	public BoolAttribute(bool status) => Status = status;
}
public class ValueAttribute<T>(T value) : PropertyAttribute {
	public T Value { get; } = value;
}

#region General
public class LabelAttribute(string value) : ValueAttribute<string>(value) { }
public class InputTypeAttribute(InputType type) : ValueAttribute<InputType>(type), IHtmlAttribute {
	private readonly InputType _type = type;
	public HtmlAttribute HtmlAttribute => Document.CreateAttribute("type", _type.GetEnumDescription());
}
public class OrderAttribute(int value) : ValueAttribute<int>(value) { }
#endregion

#region Text
public class TextAreaAttribute(int rows) : ValueAttribute<int>(rows), IHtmlAttribute {
	private readonly int _rows = rows;
	public HtmlAttribute HtmlAttribute => Document.CreateAttribute("rows", _rows.ToString());
}

#endregion

#region File
public class ImageReviewerAttribute : BoolAttribute { }
public class MultipleFilesAttribute : BoolAttribute, IHtmlAttribute { public HtmlAttribute HtmlAttribute => Document.CreateAttribute("multiple"); }
#endregion