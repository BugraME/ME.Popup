using ME.Popup.Attributes.Base;
using ME.Popup.Attributes.Concrete.Form;
using ME.Popup.Attributes.Concrete.Form.File;
using ME.Popup.Attributes.Concrete.Form.Text;
using ME.Popup.Attributes.Concrete.General;
using ME.Popup.Exceptions.Concrete;
using ME.Popup.Html.Elements.Abstract;
using ME.Popup.Html.Elements.Base;
using ME.Popup.Html.Elements.Concrete.FormElements;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ME.Popup.Html.Elements.Concrete.PopupElements;

/// <summary>
/// Represents a <see cref="Form"/> element within a popup.
/// </summary>
public class Form : PopupElement {
	/// <summary>
	/// Initializes a new instance of the <see cref="Form"/> class.
	/// </summary>
	/// <param name="formAction">The action to configure the form.</param>
	/// <param name="type">The type to generate form fields from its properties.</param>
	public Form(Action<Form> formAction, Type type = null) {
		formAction(this);
		if (type != null) foreach (PropertyInfo propertyInfo in type.GetProperties()) { AddWithProperty(propertyInfo); }
	}

	/// <summary>
	/// Gets or sets the title of the form.
	/// </summary>
	public string Title { get; set; } = null;

	/// <summary>
	/// Gets or sets the action URL of the form.
	/// </summary>
	public string Action { get; set; } = "/";

	/// <summary>
	/// Gets or sets the HTTP method used to submit the form.
	/// </summary>
	public FormHttpMethod Method { get; set; } = FormHttpMethod.Post;

	/// <summary>
	/// Gets or sets the encoding type of the form.
	/// </summary>
	public FormEnctype Enctype { get; set; } = FormEnctype.FormUrlEncoded;

	private IEnumerable<FormGroup> _groups = [];
	/// <summary>
	/// Gets or sets the form groups, ordered by their defined order.
	/// </summary>
	public IEnumerable<FormGroup> Groups {
		get { return _groups.OrderBy(item => item.Order.HasValue ? item.Order : int.MaxValue); }
		set { _groups = value; }
	}


	/// <summary>
	/// Adds a form group to the list of groups.
	/// </summary>
	/// <param name="formGroup">The form group to add.</param>
	private void AddToGroup(FormGroup formGroup) => Groups = Groups.Append(formGroup);

	/// <summary>
	/// Adds a new form group with the specified element and options.
	/// </summary>
	/// <param name="element">The form element.</param>
	/// <param name="order">The order of the form group.</param>
	/// <param name="hideLabel">If set to <c>true</c>, hides the label of the form group.</param>
	public void AddGroup(IFormElement element, int? order = null, bool hideLabel = false) {
		AddToGroup(new() {
			Label = hideLabel ? null : new(),
			Element = element,
			Order = order
		});
	}

	/// <summary>
	/// Adds a new form group with the specified label, element, and order.
	/// </summary>
	/// <param name="label">The label of the form group.</param>
	/// <param name="element">The form element.</param>
	/// <param name="order">The order of the form group.</param>
	public void AddGroup(string label, IFormElement element, int? order = null) => AddToGroup(new(new(label), element) { Order = order });

	/// <summary>
	/// Adds a new form group using an action to configure it.
	/// </summary>
	/// <param name="groupAction">The action to configure the form group.</param>
	public void AddGroup(Action<FormGroup> groupAction) => AddToGroup(new(groupAction));

	/// <summary>
	/// Adds a form element based on the specified property information.
	/// </summary>
	/// <param name="propertyInfo">The property information to generate the form element.</param>
	private void AddWithProperty(PropertyInfo propertyInfo) {
		FormGroup formGroup = new() { Label = new(propertyInfo.Name), Order = 1 };
		Type propertyType = propertyInfo.PropertyType;

		if (propertyType.IsEnum) formGroup.Element = new Select(propertyType);
		else {
			InputTypes inputType = propertyType switch {
				Type type when type == typeof(int) => InputTypes.Number,
				Type type when type == typeof(DateTime) => InputTypes.DateTimeLocal,
				Type type when type == typeof(DateOnly) => InputTypes.Date,
				Type type when type == typeof(TimeSpan) || type == typeof(TimeOnly) => InputTypes.Time,
				Type type when type == typeof(bool) => InputTypes.Checkbox,
				Type type when type == typeof(IFormFile) => InputTypes.File,
				_ => InputTypes.Text
			};


			formGroup.Element = new Input(inputType);
		}


		formGroup.Element.Name = propertyInfo.Name;

		foreach (Attribute attribute in propertyInfo.GetCustomAttributes()) {
			switch (attribute) {
				//General
				case BoolAttribute boolAttribute when !boolAttribute.Status:
					continue;
				case HideLabelAttribute hideLabelAttribute:
					formGroup.Label = null;
					break;
				case IdAttribute idAttribute:
					formGroup.Element.Id = (string)idAttribute.Value;
					break;
				case NameAttribute nameAttribute:
					formGroup.Element.Name = (string)nameAttribute.Value;
					break;
				case ClassAttribute classAttribute:
					formGroup.Element.AddClass(classAttribute.Values);
					break;
				case LabelAttribute labelAttribute:
					formGroup.Label.Value = (string)labelAttribute.Value;
					break;
				case OrderAttribute orderAttribute:
					formGroup.Order = (int)orderAttribute.Value;
					break;
				//Form
				case InputTypeAttribute inputTypeAttribute:
					formGroup.Element = new Input((InputTypes)inputTypeAttribute.Value);
					break;
				case PlaceholderAttribute placeholderAttribute:
					if (formGroup.Element is Input placeholderInput) placeholderInput.Placeholder = (string)placeholderAttribute.Value;
					else if (formGroup.Element is TextArea placeholderTextarea) placeholderTextarea.Placeholder = (string)placeholderAttribute.Value;
					else throw new InvalidAttributeException(nameof(PlaceholderAttribute), formGroup.Element.GetType().Name);
					break;
				case TextAreaAttribute textAreaAttribute:
					formGroup.Element = new TextArea(textAreaAttribute.Rows, textAreaAttribute.Columns);
					break;
				case MaxLengthAttribute maxLengthAttribute:
					if (formGroup.Element is Input || formGroup.Element is TextArea) formGroup.Element.AddCustomAttribute("maxlength", maxLengthAttribute.Length.ToString());
					else throw new InvalidAttributeException(nameof(MaxLengthAttribute), formGroup.Element.GetType().Name);
					break;
				case RequiredAttribute:
					formGroup.Element.Required = true;
					break;
				case ImageReviewerAttribute:
					if (formGroup.Element is Input imgInput && imgInput.Type == InputTypes.File) {
						formGroup.PrependElement = new HtmlElement() {
							Class = ImageReviewerAttribute.Class,
							Tag = HtmlTags.Img.GetEnumDescription(),
							Shorthand = true,
						};
					}
					else throw new InvalidAttributeException(nameof(ImageReviewerAttribute), formGroup.Element.GetType().Name);
					break;
				case MultipleFilesAttribute multipleFilesAttribute:
					if (formGroup.Element is Input fileInput && fileInput.Type == InputTypes.File) {
						fileInput.AddCustomAttribute("multiple", "multiple");
					}
					else throw new InvalidAttributeException(nameof(MultipleFilesAttribute), formGroup.Element.GetType().Name);
					break;
			}
		};

		AddToGroup(formGroup);
	}
}