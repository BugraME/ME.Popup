using ME.Popup.Attributes.Base;

namespace ME.Popup.Attributes.Concrete.General;
public class ClassAttribute(params string[] values) : PropertyAttribute {
    public string[] Values { get; } = values;
}