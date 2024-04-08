using Meplate.Enums;
using Meplate.Models;
namespace Meplate;
public static class HtmlGenerator {
	/// <summary>
	/// Creates an HTML element based on the provided IHtmlElement object.
	/// </summary>
	/// <param name="htmlElement">The IHtmlElement object representing the HTML element to create.</param>
	/// <returns>A string representing the HTML element.</returns>
	public static string CreateElement(IHtmlElement htmlElement) {
		//Opening Tag
		string stringResult = $"<{htmlElement.Tag} ";

		//Attributes
		if (!string.IsNullOrWhiteSpace(htmlElement.Id)) stringResult += $"id='{htmlElement.Id}' ";
		if (!string.IsNullOrWhiteSpace(htmlElement.Class)) stringResult += $"class='{htmlElement.Class}' ";
		if (!string.IsNullOrWhiteSpace(htmlElement.Name)) stringResult += $"name='{htmlElement.Name}' ";
		if (!string.IsNullOrWhiteSpace(htmlElement.Attributes)) stringResult += $"{htmlElement.Attributes} ";

		//Conditions
		if (htmlElement is Input  || htmlElement.Tag == HtmlTags.Input.GetEnumDescription()) {
			Input input = (Input)htmlElement;
			stringResult += $"type='{input.Type.GetEnumDescription()}' ";
			if (!string.IsNullOrWhiteSpace(input.Placeholder)) stringResult += $"placeholder='{input.Placeholder}' ";
			if (input.Required) stringResult += "required ";
		}
		else if (htmlElement is TextArea || htmlElement.Tag == HtmlTags.Textarea.GetEnumDescription()) {
			TextArea textArea = (TextArea)htmlElement;

			stringResult += $"rows='{textArea.Rows}' ";
			if (textArea.Cols.HasValue) stringResult += $"cols='{textArea.Cols.Value}' ";
			if (!string.IsNullOrWhiteSpace(textArea.Placeholder)) stringResult += $"placeholder='{textArea.Placeholder}' ";
			if (textArea.Required) stringResult += "required ";
		}	
		else if (htmlElement is Option || htmlElement.Tag == HtmlTags.Option.GetEnumDescription()) {
			Option option = (Option)htmlElement;
			stringResult += $"value='{option.Value}'>{option.Text}</{htmlElement.Tag}>";
			return stringResult;
		}

		//Shorthand Check + Value
		if (htmlElement.Shorthand) stringResult += $"value='{htmlElement.Value}' />";
		else stringResult += $">{htmlElement.Value}";

		//Nodes
		foreach (IHtmlElement node in htmlElement.Nodes) {
			stringResult += CreateElement(node);
		}

		//Closing Tag
		if (!htmlElement.Shorthand) stringResult += $"</{htmlElement.Tag}>";
		return stringResult;
	}
}