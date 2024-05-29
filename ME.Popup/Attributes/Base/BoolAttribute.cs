namespace ME.Popup.Attributes.Base;
public abstract class BoolAttribute(bool status) : PropertyAttribute {
    public bool Status { get; } = status;
}