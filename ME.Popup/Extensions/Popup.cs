using HtmlAgilityPack;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using static ME.Popup.Enums;
namespace ME.Popup.Extensions;
public class Popup {
	//Model
	public string Id { get; set; } = "Popup-" + Guid.NewGuid().ToString("N");

	//Constructor
	private readonly HtmlDocument Document;
	private readonly HtmlNode Script;

	private Popup() {
		Document = new();
		Script = Document.CreateElement("script");
	}

	public static Popup CreatePopup() {
		Popup popup = new();
		HtmlNode div = popup.Document.CreateElement("div");
		div.AddClass("me-popup");
		popup.Document.DocumentNode.AppendChild(div);
		return popup;
	}

	public Popup Form<T>(int gridColumnCount = 1) {
		HtmlNode form = Document.CreateElement("form");
		Document.DocumentNode.SelectSingleNode("//div").AppendChild(form);

        if (gridColumnCount >= 7) form.AddClass("me-col-7");
        else if (gridColumnCount > 1) form.AddClass($"me-col-{gridColumnCount}");

        foreach (PropertyInfo propertyInfo in typeof(T).GetProperties()) {
			Type propertyType = propertyInfo.PropertyType;
			bool isNullable = propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>);

			HtmlNode parentDiv = Document.CreateElement("div");
			parentDiv.AddClass("me-form-group");

			HtmlNode label = Document.CreateElement("label");
			label.InnerHtml = propertyInfo.Name + ":" + (isNullable ? "*" : string.Empty);
            parentDiv.AppendChild(label);


			if (propertyType == typeof(int)) {
				HtmlNode input = Document.CreateElement("input");
				input.Attributes.Add("type", "number");
				input.Attributes.Add("name", propertyInfo.Name);
                parentDiv.AppendChild(input);
			}
			else if (propertyType == typeof(DateTime)) {
				HtmlNode input = Document.CreateElement("input");
				input.Attributes.Add("type", "datetime-local");
				input.Attributes.Add("name", propertyInfo.Name);
                parentDiv.AppendChild(input);
			}
			else if (propertyType == typeof(bool)) {
				HtmlNode input = Document.CreateElement("input");
				input.Attributes.Add("type", "checkbox");
				input.Attributes.Add("name", propertyInfo.Name);
                parentDiv.AppendChild(input);
			}
			else if (propertyType == typeof(IFormFile)) {
				HtmlNode input = Document.CreateElement("input");
				input.Attributes.Add("type", "file");
				input.Attributes.Add("name", propertyInfo.Name);
                parentDiv.AppendChild(input);
			}
			else if (propertyType.IsEnum) {
				//enum select
				HtmlNode select = Document.CreateElement("select");
				select.Attributes.Add("name", propertyInfo.Name);
				foreach (var item in GetEnumValues(propertyType)) {
					HtmlNode option = Document.CreateElement("option");
					option.Attributes.Add("value", item.Value == 0 ? item.Name : item.Value.ToString());
					option.InnerHtml = !string.IsNullOrWhiteSpace(item.Description) ? item.Description : item.Name;
					select.AppendChild(option);
				}
                parentDiv.AppendChild(select);
			}
			else {
				HtmlNode input = Document.CreateElement("input");
				input.Attributes.Add("type", "text");
				input.Attributes.Add("name", propertyInfo.Name);
                parentDiv.AppendChild(input);
			}

            form.AppendChild(parentDiv);
		}
		return this;
	}

	public IHtmlContent Render(string triggerSelector) {
		Script.InnerHtml = $@"$(document).on('click', '{triggerSelector}', () => $('#{Id}').fadeIn());";
		Document.DocumentNode.AppendChild(Script);
		HtmlNode popup = Document.DocumentNode.SelectSingleNode("//div");
		popup.Attributes.Add("id", Id);

		HtmlNode closeIcon = Document.CreateElement("i");
		closeIcon.InnerHtml = "x";
		closeIcon.AddClass("me-close-icon");
		popup.ChildNodes.First().PrependChild(closeIcon);
		return new HtmlString(Document.DocumentNode.OuterHtml);
	}
}