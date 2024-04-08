using Meplate.Enums;
using Microsoft.AspNetCore.Html;

namespace Meplate.Models;
public class Popup {
	public string Id { get; set; } = "Popup-" + Guid.NewGuid().ToString("N");
	public IPopupElement Element { get; set; }
	public Popup Form(Action<Form> formAction) {
		Element = new Form(formAction);
		return this;
	}
	public Popup Form<T>(Action<Form> formAction) {		
		Element = new Form(formAction, typeof(T));
		return this;
	}

	public IHtmlContent Render(string selector) {
		string script = $@"<script>$(document).on('click', '{selector}', () => $('#{Id}').fadeIn());</script>";

		HtmlElement popupDiv = new() { Id = Id, Class = "me-popup" };
		if (Element is Form form) {
			HtmlElement formElement = new() { Tag = "form" };
			formElement.Nodes.Add(new HtmlElement(HtmlTags.I, "x") { Class = "me-close-icon" });
			if (!string.IsNullOrEmpty(form.HtmlId)) formElement.AddCustomAttribute("id", form.HtmlId);
			formElement.AddCustomAttributes(("action", form.Action), ("method", form.Method.GetEnumDescription()), ("enctype", form.Enctype.GetEnumDescription()));
			if (!string.IsNullOrEmpty(form.Title)) formElement.Nodes.Add(new HtmlElement(HtmlTags.H2, form.Title));
			if (form.ColumnCount >= 7) formElement.AddClass("me-col-7");
			else if (form.ColumnCount > 1) formElement.AddClass($"me-col-{form.ColumnCount}");
			foreach (FormGroup group in form.Groups) {
				Label groupLabel = group.Label;
				groupLabel.Value += group.Element.Required ? ":*" : ":";
				IHtmlElement formGroup = new HtmlElement { Class = "me-form-group", Nodes = [groupLabel, group.Element] };
				formElement.Nodes.Add(formGroup);
			}
			popupDiv.Nodes.Add(formElement);
		}
		string result = HtmlGenerator.CreateElement(popupDiv);
		return new HtmlString(script + "\n" + result);
	}
}


public interface IPopupElement {
	public string HtmlId { get; set; }
	public string HtmlClass { get; }
	public int ColumnCount { get; set; }
}
public class PopupElement : IPopupElement {
	private string _htmlClass;
	public string HtmlClass => _htmlClass;
	public virtual int ColumnCount { get; set; }
	public virtual string HtmlId { get; set; }
	public virtual void AddClass(params string[] className) => _htmlClass = string.Join(" ", className);
}