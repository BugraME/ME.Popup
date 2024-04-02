using HtmlAgilityPack;
using ME.Popup.Enums;
using ME.Popup.Extensions.Attributes;
using ME.Popup.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
namespace ME.Popup.Extensions;
public class Popup {
	//Model
	public string Id { get; set; } = "Popup-" + Guid.NewGuid().ToString("N");

	//Constructor
	private readonly HtmlDocument Document = new();
	private readonly HtmlNode Script;
	private readonly List<FormElement> FormElements = [];

	private Popup() {
		Script = CreateElement("script");
	}

	public static Popup CreatePopup() {
		Popup popup = new();
		HtmlNode div = popup.CreateElement("div");
		div.AddClass("me-popup");
		popup.Document.DocumentNode.AppendChild(div);
		return popup;
	}

	public Popup Form<T>(Form form) {
		HtmlNode formNode = CreateElement("form");
		if (!string.IsNullOrWhiteSpace(form.HtmlId)) formNode.Attributes.Add("id", form.HtmlId);
		formNode.Attributes.Add("action", form.Action);
		formNode.Attributes.Add("method", form.Method.ToString().ToLower());
		formNode.Attributes.Add("enctype", form.Enctype.GetEnumDescription());
		if (form.ColumnCount >= 7) formNode.AddClass("me-col-7");
		else if (form.ColumnCount > 1) formNode.AddClass($"me-col-{form.ColumnCount}");
		Document.DocumentNode.SelectSingleNode("//div").AppendChild(formNode);

		foreach (PropertyInfo propertyInfo in typeof(T).GetProperties()) {
			FormElement formElement = new();

			HtmlNode parentDiv = CreateElement("div");
			parentDiv.AddClass("me-form-group");

			HtmlNode label = CreateElement("label");
			LabelAttribute labelAttribute = propertyInfo.GetCustomAttribute<LabelAttribute>();
			label.InnerHtml = (labelAttribute != null ? labelAttribute.Value : propertyInfo.Name) + ":" + (propertyInfo.GetCustomAttribute<RequiredAttribute>() != null ? "*" : string.Empty);

			HtmlNode element = InitializeElement(propertyInfo);
			ImageReviewerAttribute imageReviewerAttribute = propertyInfo.GetCustomAttribute<ImageReviewerAttribute>();
			if (imageReviewerAttribute != null && imageReviewerAttribute.Status) {
				element.Attributes.Add("accept", "image/*");
				HtmlNode imgNode = CreateElement("img");
				imgNode.AddClass("me-image-reviewer");
				formElement.PrependElements.Add(imgNode);
			}

			MultipleFilesAttribute multipleFilesAttribute = propertyInfo.GetCustomAttribute<MultipleFilesAttribute>();
			if (multipleFilesAttribute != null && multipleFilesAttribute.Status) element.Attributes.Add("multiple", "multiple");

			formElement.Label = label;
			formElement.Element = element;
			formElement.Order = propertyInfo.GetCustomAttribute<OrderAttribute>()?.Value;
			FormElements.Add(formElement);
		}
		return this;
	}

	public IHtmlContent Render(string triggerSelector) {
		Script.InnerHtml = $@"$(document).on('click', '{triggerSelector}', () => $('#{Id}').fadeIn());";
		Document.DocumentNode.AppendChild(Script);
		HtmlNode popup = Document.DocumentNode.SelectSingleNode("//div");
		popup.Attributes.Add("id", Id);

		HtmlNode closeIcon = CreateElement("i");
		closeIcon.InnerHtml = "x";
		closeIcon.AddClass("me-close-icon");
		popup.ChildNodes.First().PrependChild(closeIcon);

		if (FormElements.Count > 0) {
			HtmlNode form = Document.DocumentNode.SelectSingleNode("//form");
			foreach (FormElement formElement in FormElements.OrderByDescending(o => o.Order.HasValue).ThenBy(o => o.Order)) {
				HtmlNode formGroup = CreateElement("div");
				formGroup.AddClass("me-form-group");
				if (formElement.PrependElements.Count > 0) {
					foreach (HtmlNode prependElement in formElement.PrependElements) { formGroup.AppendChild(prependElement); }
				}
				formGroup.AppendChild(formElement.Label);
				formGroup.AppendChild(formElement.Element);
				if (formElement.AppendElements.Count > 0) {
					foreach (HtmlNode appendElement in formElement.AppendElements) { formGroup.AppendChild(appendElement); }
				}
				form.AppendChild(formGroup);
			}

			HtmlNode div = CreateElement("div");
			div.AddClass("me-form-group");
			HtmlNode submitButton = CreateElement("button");
			submitButton.InnerHtml = "Submit";
			submitButton.Attributes.Add("type", "submit");
			div.AppendChild(submitButton);
			form.AppendChild(div);
		}

		return new HtmlString(Document.DocumentNode.OuterHtml);
	}

	private HtmlNode InitializeElement(PropertyInfo propertyInfo) {
		HtmlNode element = CreateElement("input");
		element.Attributes.Add("name", propertyInfo.Name);

		InputTypeAttribute inputTypeAttribute = propertyInfo.GetCustomAttribute<InputTypeAttribute>();
		TextAreaAttribute textAreaAttribute = propertyInfo.GetCustomAttribute<TextAreaAttribute>();
		Type propertyType = propertyInfo.PropertyType;

		if (inputTypeAttribute != null) element.Attributes.Add("type", inputTypeAttribute.Value.GetEnumDescription());
		else {
			if (propertyType.IsEnum) {
				element = CreateElement("select");
				foreach (EnumModel enumModel in EnumMethods.GetEnumValues(propertyType)) {
					HtmlNode option = CreateElement("option");
					option.Attributes.Add("value", enumModel.Value == 0 ? enumModel.Name : enumModel.Value.ToString());
					option.InnerHtml = !string.IsNullOrWhiteSpace(enumModel.Description) ? enumModel.Description : enumModel.Name;
					element.AppendChild(option);
				}
			}
			else if (textAreaAttribute != null) {
				element = CreateElement("textarea");
				element.Attributes.Add(textAreaAttribute.HtmlAttribute);
			}
			else {
				InputType inputType = InputType.Text;
				if (propertyType == typeof(int)) inputType = InputType.Number;
				else if (propertyType == typeof(DateTime)) inputType = InputType.DateTimeLocal;
				else if (propertyType == typeof(DateOnly)) inputType = InputType.Date;
				else if (propertyType == typeof(TimeSpan) || propertyType == typeof(TimeOnly)) inputType = InputType.Time;
				else if (propertyType == typeof(bool)) inputType = InputType.Checkbox;
				else if (propertyType == typeof(IFormFile)) inputType = InputType.File;
				element.Attributes.Add("type", inputType.GetEnumDescription());
			}
		}

		foreach (IHtmlAttribute attribute in propertyInfo.GetCustomAttributes().Where(x => x is IHtmlAttribute).Cast<IHtmlAttribute>()) {
			element.Attributes.Add(attribute.HtmlAttribute);
		}

		MaxLengthAttribute maxLengthAttribute = propertyInfo.GetCustomAttribute<MaxLengthAttribute>();
		if (maxLengthAttribute != null) element.Attributes.Add("maxlength", maxLengthAttribute.Length.ToString());

		if (propertyInfo.GetCustomAttribute<RequiredAttribute>() != null) element.Attributes.Add("required", "required");

		return element;
	}



	private HtmlNode CreateElement(string tagName) => Document.CreateElement(tagName);
}