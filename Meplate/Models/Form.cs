using Meplate.Attributes;
using Meplate.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Reflection.Emit;

namespace Meplate.Models;
public class Form : PopupElement {
	public Form(Action<Form> formAction, Type type = null) {
		formAction(this);
		if (type != null) foreach (PropertyInfo propertyInfo in type.GetProperties()) { AddWithProperty(propertyInfo); }
	}

	public string Title { get; set; } = null;
	public string Action { get; set; } = "/";
	public FormHttpMethod Method { get; set; } = FormHttpMethod.Post;
	public FormEnctype Enctype { get; set; } = FormEnctype.FormUrlEncoded;
	public List<FormGroup> Groups { get; set; } = [];

	public void AddGroup(string label, IFormElement element) => Groups.Add(new() { Label = new(label), Element = element });
	public void AddGroup(Action<Label> label, IFormElement element) => Groups.Add(new() { Label = new(label), Element = element });
	public void AddGroup(Label label, IFormElement element) => Groups.Add(new() { Label = label, Element = element });
	public void AddGroup(Action<FormGroup> groupAction) => Groups.Add(new(groupAction));
	public void AddGroup(FormGroup group) => Groups.Add(group);
	private void AddWithProperty(PropertyInfo propertyInfo) {
		FormGroup formGroup = new() { Element = new Input(), Order = 1 };

		RequiredAttribute requiredAttribute = propertyInfo.GetCustomAttribute<RequiredAttribute>();
		HideLabelAttribute hideLabelAttribute = propertyInfo.GetCustomAttribute<HideLabelAttribute>();
		if (hideLabelAttribute == null || !hideLabelAttribute.Status) {
			LabelAttribute labelAttribute = propertyInfo.GetCustomAttribute<LabelAttribute>();
			formGroup.Label = new((labelAttribute != null ? labelAttribute.Value : propertyInfo.Name) + ":" + (requiredAttribute != null ? "*" : string.Empty));
		}

		NameAttribute nameAttribute = propertyInfo.GetCustomAttribute<NameAttribute>();
		if (nameAttribute != null) formGroup.Element.AddCustomAttribute("name", nameAttribute.Value);
		else formGroup.Element.AddCustomAttribute("name", propertyInfo.Name);

		InputTypeAttribute inputTypeAttribute = propertyInfo.GetCustomAttribute<InputTypeAttribute>();
		TextAreaAttribute textAreaAttribute = propertyInfo.GetCustomAttribute<TextAreaAttribute>();
		Type propertyType = propertyInfo.PropertyType;


		if (propertyType.IsEnum) {
			formGroup.Element = new Select(propertyType);
			foreach (EnumModel enumModel in EnumMethods.GetEnumValues(propertyType)) {
				Option option = new() {
					Value = enumModel.Value == 0 ? enumModel.Name : enumModel.Value.ToString(),
					Text = !string.IsNullOrWhiteSpace(enumModel.Description) ? enumModel.Description : enumModel.Name
				};
				formGroup.Element.Nodes.Add(option);
			}
		}
		else if (textAreaAttribute != null) {
			formGroup.Element = new TextArea();
			formGroup.Element.AddCustomAttribute(textAreaAttribute.HtmlAttribute);
		}
		else {
			InputTypes inputType = InputTypes.Text;
			if (inputTypeAttribute != null) inputType = InputTypes.Hidden;
			else if (propertyType == typeof(int)) inputType = InputTypes.Number;
			else if (propertyType == typeof(DateTime)) inputType = InputTypes.DateTimeLocal;
			else if (propertyType == typeof(DateOnly)) inputType = InputTypes.Date;
			else if (propertyType == typeof(TimeSpan) || propertyType == typeof(TimeOnly)) inputType = InputTypes.Time;
			else if (propertyType == typeof(bool)) inputType = InputTypes.Checkbox;
			else if (propertyType == typeof(IFormFile)) inputType = InputTypes.File;
			formGroup.Element = new Input(inputType);
		}
		foreach (IHtmlAttribute attribute in propertyInfo.GetCustomAttributes().Where(x => x is IHtmlAttribute).Cast<IHtmlAttribute>()) {
			formGroup.Element.AddCustomAttribute(attribute.HtmlAttribute);
		}

		MaxLengthAttribute maxLengthAttribute = propertyInfo.GetCustomAttribute<MaxLengthAttribute>();
		if (maxLengthAttribute != null) formGroup.Element.AddCustomAttribute("maxlength", maxLengthAttribute.Length.ToString());
		if (requiredAttribute != null) formGroup.Element.AddCustomAttribute("required", "required");

		Groups.Add(formGroup);
	}

}

public class FormGroup {
	public FormGroup() { }
	public FormGroup(Action<FormGroup> groupAction) => groupAction(this);
	public Label Label { get; set; }
	public IFormElement Element { get; set; }
	public int? Order { get; set; } = null;
}