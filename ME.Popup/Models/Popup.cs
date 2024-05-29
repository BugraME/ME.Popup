using ME.Popup.Html.Elements.Abstract;
using ME.Popup.Html.Elements.Base;
using ME.Popup.Html.Elements.Concrete.FormElements;
using ME.Popup.Html.Elements.Concrete.PopupElements;
using Microsoft.AspNetCore.Html;
using System.Text;
using System.Text.Encodings.Web;
using Extensions = ME.Popup.Html.Extensions;

namespace ME.Popup.Models;
public class Popup : IHtmlContent {
	public Popup() { }
	public string Id { get; set; } = $"Popup-{Guid.NewGuid():N}";
	public IPopupElement Element { get; set; }
	public Type ModelType { get; set; } = null;

	public Popup Form(Action<Form> formAction, bool useModel = false) {
		if (useModel) Element = new Form(formAction, ModelType);
		else Element = new Form(formAction);
		return this;
	}
	public Popup Form<T>(Action<Form> formAction) {
		Element = new Form(formAction, typeof(T));
		return this;
	}

	public void WriteTo(TextWriter writer, HtmlEncoder encoder) {
		StringBuilder builder = new();

		if (Element.Selector != null) {
			builder.AppendLine("<script>");
			builder.AppendLine("window.addEventListener('load', function() {");
			builder.Append('\t').AppendLine($"fadeElementIn('{Element.Selector}','#{Id}')");
			builder.AppendLine("});");
			builder.AppendLine("</script>");
		}

		HtmlElement popupDiv = new(HtmlTags.Div) { Id = Id, Class = "me-popup" };
		if (Element is Form form) {
			FormElement formElement = new();
			formElement.Nodes = formElement.Nodes.Prepend(new HtmlElement(HtmlTags.I, "x") { Class = "me-close-icon" });
			if (!string.IsNullOrWhiteSpace(form.HtmlId)) formElement.AddCustomAttribute("id", form.HtmlId);
			formElement.AddCustomAttribute("action", form.Action);
			formElement.AddCustomAttribute("method", form.Method.GetEnumDescription());
			formElement.AddCustomAttribute("enctype", form.Enctype.GetEnumDescription());
			if (!string.IsNullOrWhiteSpace(form.Title)) formElement.Nodes = formElement.Nodes.Append(new HtmlElement(HtmlTags.H2, form.Title));
			if (form.ColumnCount >= 7) formElement.AddClass("me-col-7");
			else if (form.ColumnCount > 1) formElement.AddClass($"me-col-{form.ColumnCount}");
			foreach (FormGroup group in form.Groups) {
				IEnumerable<IHtmlElement> groupNodes = [group.PrependElement,];
				if (group.Label != null) {
					group.Label.Value += group.Element.Required ? "* :" : " :";
					groupNodes = groupNodes.Append(group.Label);
				}
				groupNodes = groupNodes.Append(group.Element);
				IHtmlElement formGroup = new HtmlElement { Class = "me-form-group", Nodes = groupNodes };
				formElement.Nodes = formElement.Nodes.Append(formGroup);
			}


			HtmlElement buttonGroup = new(HtmlTags.Div) { Class = "me-btn-group" };
			if (form.CancelButton != null && !string.IsNullOrWhiteSpace(form.CancelButton)) {
				buttonGroup.Nodes = buttonGroup.Nodes.Append(new HtmlElement(HtmlTags.Button, form.CancelButton) { Class = "me-btn me-cancel", Attributes = "type='button'" });
			}
			buttonGroup.Nodes = buttonGroup.Nodes.Append(new HtmlElement(HtmlTags.Button, form.SubmitButton) { Class = "me-btn me-submit" });
			formElement.Nodes = formElement.Nodes.Append(buttonGroup);
			popupDiv.Nodes = popupDiv.Nodes.Append(formElement);
		}
		builder.AppendLine(Extensions.CreateElement(popupDiv));
		builder.AppendLine("<input name='ProfilePhoto' multiple='multiple' type='file' size='30'>");

		writer.Write(builder.ToString());
	}
}