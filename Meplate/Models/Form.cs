using Meplate.Enums;

namespace Meplate.Models;
public class Form : PopupElement {
	public Form() { }
	public Form(Action<Form> formAction) => formAction(this);
	public string Title { get; set; } = null;
	public string Action { get; set; } = "/";
	public FormHttpMethod Method { get; set; } = FormHttpMethod.Post;
	public FormEnctype Enctype { get; set; } = FormEnctype.FormUrlEncoded;
	public List<FormGroup> Groups { get; set; } = [];

	public void AddGroups(params Action<FormGroup>[] groupAction) {
		foreach (Action<FormGroup> action in groupAction) Groups.Add(new(action));
	}	
}

public class FormGroup {
	private Label label;
	public FormGroup() { }
	public FormGroup(Action<FormGroup> groupAction) => groupAction(this);
	public object Label {
		get => label; 
		set => label = new(value.ToString()); 
	}
	public IFormElement Element { get; set; }
}