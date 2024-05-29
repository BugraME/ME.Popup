using ME.Popup.Attributes.Base;

namespace ME.Popup.Attributes.Concrete.General;
public class HideLabelAttribute(bool status = true) : BoolAttribute(status) { }