using ME.Popup.Attributes.Abstract;

namespace ME.Popup.Attributes.Base;
public abstract class ValueAttribute(object value) : PropertyAttribute, IValueAttribute {
    public object Value { get; } = value;
}