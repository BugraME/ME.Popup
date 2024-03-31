using System.ComponentModel;
using System.Reflection;
namespace ME.Popup;
public record struct EnumModel(int Value, string Name, string Description);
public static class Enums {
	public static string GetEnumDescription(this Enum value) {
		FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
		if (fieldInfo == null) return "Error";
		var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
		return attributes.Length == 0 ? value.ToString() : ((DescriptionAttribute)attributes[0]).Description;
	}
	public static IEnumerable<EnumModel> GetEnumValues<T>() where T : Enum {
		T[] array = (T[])Enum.GetValues(typeof(T));
		return array.Cast<Enum>().GetEnumModel();
	}
	public static IEnumerable<EnumModel> GetEnumValues(Type type) {
		Array enumValues = Enum.GetValues(type);
		return enumValues.Cast<Enum>().GetEnumModel();
	}
	private static IEnumerable<EnumModel> GetEnumModel(this IEnumerable<Enum> array) => array.Select(x => new EnumModel {
		Value = int.TryParse(x.ToString(), out int value) ? value : 0,
		Name = x.ToString(),
		Description = GetEnumDescription(x)
	});

	public enum PopupType {
		Form = 0,
		Question = 1,
	}
}