using ME.Popup.Attributes.Base;

namespace ME.Popup.Attributes.Concrete.Form.Text;
public class TextAreaAttribute : PropertyAttribute {
	public TextAreaAttribute(int rows = 2) => Rows = rows;
	public TextAreaAttribute(int rows, int columns) { Rows = rows; Columns = columns; }
	public int Rows { get; set; }
	public int? Columns { get; set; } = null;
}