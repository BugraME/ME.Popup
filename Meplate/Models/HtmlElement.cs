using Meplate.Enums;

namespace Meplate.Models;
public interface IHtmlElement {
	public string Tag { get; set; }
	public string Id { get; set; }
	public string Class { get; set; }
	public string Name { get; set; }
	public string Value { get; set; }
	public bool Shorthand { get; set; }
	public string Attributes { get; set; }
	public List<IHtmlElement> Nodes { get; set; }
	public void AddClass(params string[] className);
	public void AddCustomAttributes(params (string, string)[] keyValues);
	public void AddCustomAttribute(string key, string value);
	public void AddCustomAttribute((string, string) keyValue);
}

public class HtmlElement : IHtmlElement {
	public HtmlElement() { }
	public HtmlElement(HtmlTags tag) => Tag = tag.GetEnumDescription();
	public HtmlElement(HtmlTags tag, string value) { Tag = tag.GetEnumDescription(); Value = value; }
	public virtual string Tag { get; set; } = HtmlTags.Div.GetEnumDescription();
	public virtual string Id { get; set; }
	public virtual string Class { get; set; }
	public virtual string Name { get; set; }
	public virtual string Value { get; set; }
	public virtual bool Shorthand { get; set; }
	public virtual string Attributes { get; set; }
	public virtual List<IHtmlElement> Nodes { get; set; } = [];
	public virtual void AddClass(params string[] className)
		=> Class += " " + string.Join(" ", className);
	public virtual void AddCustomAttributes(params (string, string)[] keyValues) => Attributes += " " + string.Join(" ", keyValues.Select(x => $"{x.Item1}='{x.Item2}'"));
	public virtual void AddCustomAttribute(string key, string value) => Attributes += " " + $"{key}='{value}'";
	public virtual void AddCustomAttribute((string, string) keyValue) => Attributes += " " + $"{keyValue.Item1}='{keyValue.Item2}'";
}