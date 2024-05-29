namespace ME.Popup.Attributes.Concrete.General;
public class CustomHtmlAttribute {
	public string Key { get; }
	public string Value { get; }
	public CustomHtmlAttribute(string key) { Key = key; Value = key; }
	public CustomHtmlAttribute(string key, string value) { Key = key; Value = value; }
}