using ME.Popup.Html.Elements.Abstract;
using ME.Popup.Html.Elements.Concrete.FormElements;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
namespace ME.Popup.Html;
public static class Extensions {

	/// <summary>
	/// Generates a new instance of the <c>Popup</c> class with the ModelType property set 
	/// to the type of the model associated with the provided <see cref="IHtmlHelper{TModel}" />.
	/// </summary>
	/// <typeparam name="TModel">The type of the model associated with the HtmlHelper.</typeparam>
	/// <param name="htmlHelper">The HtmlHelper instance used to access the model metadata.</param>
	/// <returns>A new instance of the <c>Popup</c> class with the ModelType property initialized.</returns>
	public static Models.Popup ME<TModel>(this IHtmlHelper<TModel> htmlHelper)
		=> new() { ModelType = htmlHelper.ViewData.ModelMetadata.ModelType };

	/// <summary>
	/// Generates an HTML string representation of the specified <see cref="IHtmlElement"/> and its child elements.
	/// </summary>
	/// <param name="htmlElement">The root <see cref="IHtmlElement"/> to be converted to an HTML string.</param>
	/// <returns>A string containing the HTML representation of the provided element and its children.</returns>
	/// <remarks>
	/// The method constructs the HTML starting with the opening tag, including attributes (id, class, name, and any additional attributes),
	/// and conditionally includes form-specific attributes if the element implements <see cref="IFormElement"/>. 
	/// If the element has a shorthand notation and a value, it self-closes; otherwise, it closes with a full end tag.
	/// Child elements are recursively processed and included in the HTML string.
	/// </remarks>
	public static string CreateElement(IHtmlElement htmlElement) {
		StringBuilder stringBuilder = new();

		if (htmlElement != null) {
			// Opening Tag
			stringBuilder.AppendFormat("<{0} ", htmlElement.Tag);

			// Attributes
			if (!string.IsNullOrWhiteSpace(htmlElement.Id)) stringBuilder.AppendFormat("id='{0}' ", htmlElement.Id);
			if (!string.IsNullOrWhiteSpace(htmlElement.Class)) stringBuilder.AppendFormat("class='{0}' ", htmlElement.Class);
			if (!string.IsNullOrWhiteSpace(htmlElement.Name)) stringBuilder.AppendFormat("name='{0}' ", htmlElement.Name);
			if (!string.IsNullOrWhiteSpace(htmlElement.Attributes)) stringBuilder.Append(htmlElement.Attributes);

			// Conditions
			if (htmlElement is IFormElement formElement) stringBuilder.CheckFormElements(formElement);

			// Shorthand Check + Value
			if (htmlElement.Shorthand && !string.IsNullOrWhiteSpace(htmlElement.Value)) stringBuilder.AppendFormat("value='{0}' />", htmlElement.Value);
			else if (htmlElement.Shorthand) stringBuilder.Append(" />");
			else stringBuilder.AppendFormat(">{0}", htmlElement.Value);

			// Nodes
			foreach (IHtmlElement node in htmlElement.Nodes) {
				stringBuilder.Append(CreateElement(node));
			}

			// Closing Tag
			if (!htmlElement.Shorthand) stringBuilder.AppendFormat("</{0}>", htmlElement.Tag);
		}
		return stringBuilder.ToString();
	}

	/// <summary>
	/// Appends HTML attributes to a <see cref="StringBuilder"/> based on the type and properties of the given <see cref="IFormElement"/>.
	/// Handles <see cref="Input"/>, <see cref="TextArea"/>, and <see cref="Option"/> elements, setting attributes such as 'type', 'placeholder', 'required',
	/// 'rows', 'cols', and 'value' accordingly.
	/// </summary>
	/// <param name="stringBuilder">The <see cref="StringBuilder"/> to append the HTML attributes to.</param>
	/// <param name="formElement">The <see cref="IFormElement"/> representing the form element whose attributes are to be processed.</param>
	private static void CheckFormElements(this StringBuilder stringBuilder, IFormElement formElement) {
		if (formElement is Input || formElement.Tag == HtmlTags.Input.GetEnumDescription()) {
			Input input = (Input)formElement;
			stringBuilder.AppendFormat("type='{0}' ", input.Type.GetEnumDescription());
			if (!string.IsNullOrWhiteSpace(input.Placeholder)) stringBuilder.AppendFormat("placeholder='{0}' ", input.Placeholder);
			if (input.Required) stringBuilder.Append("required ");
		}
		else if (formElement is TextArea || formElement.Tag == HtmlTags.Textarea.GetEnumDescription()) {
			TextArea textArea = (TextArea)formElement;
			stringBuilder.AppendFormat("rows='{0}' ", textArea.Rows);
			if (textArea.Cols.HasValue) stringBuilder.AppendFormat("cols='{0}' ", textArea.Cols.Value);
			if (!string.IsNullOrWhiteSpace(textArea.Placeholder)) stringBuilder.AppendFormat("placeholder='{0}' ", textArea.Placeholder);
			if (textArea.Required) stringBuilder.Append("required ");
		}
		else if (formElement is Option || formElement.Tag == HtmlTags.Option.GetEnumDescription()) {
			Option option = (Option)formElement;
			stringBuilder.AppendFormat("value='{0}' ", option.Value);
			formElement.Value = option.Text;
		}
	}
}