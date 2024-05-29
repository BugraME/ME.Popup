using ME.Popup.Exceptions.Abstract;

namespace ME.Popup.Exceptions.Concrete;
public class InvalidAttributeException(string attributeName, string element)
    : BaseException($"Attribute {attributeName.Replace("Attribute", "")} is not valid for element {element}.") {
}