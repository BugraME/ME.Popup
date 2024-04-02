using HtmlAgilityPack;
using ME.Popup.Enums;
namespace ME.Popup.Models;

public class Form {
	public string HtmlId { get; set; }
	public string Action { get; set; } = "/";
	public FormHttpMethod Method { get; set; } = FormHttpMethod.Post;
	public FormEnctype Enctype { get; set; } = FormEnctype.FormUrlEncoded;
	public int ColumnCount { get; set; } = 1;
}

public class FormElement {
	public HtmlNode Label { get; set; }
	public HtmlNode Element { get; set; }
	public List<HtmlNode> PrependElements { get; set; } = [];
	public List<HtmlNode> AppendElements { get; set; } = [];
	public int? Order { get; set; }
}