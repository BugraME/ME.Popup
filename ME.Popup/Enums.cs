using System.ComponentModel;
using System.Reflection;
namespace ME.Popup.Enums;
public record struct EnumModel(int Value, string Name, string Description);
public static class EnumMethods {
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
		Value = Convert.ToInt32(x),
		Name = x.ToString(),
		Description = GetEnumDescription(x)
	});
}
public enum InputType {
	[Description("text")] Text = 0,
	[Description("email")] Email = 1,
	[Description("password")] Password = 2,
	[Description("tel")] Tel = 3,
	[Description("url")] Url = 4,
	[Description("search")] Search = 5,
	[Description("textarea")] Textarea = 6,
	[Description("hidden")] Hidden = 7,
	[Description("number")] Number = 8,
	[Description("range")] Range = 9,
	[Description("date")] Date = 10,
	[Description("time")] Time = 11,
	[Description("datetime-local")] DateTimeLocal = 12,
	[Description("month")] Month = 13,
	[Description("week")] Week = 14,
	[Description("datetime")] DateTime = 15,
	[Description("color")] Color = 16,
	[Description("file")] File = 17,
	[Description("checkbox")] Checkbox = 18,
	[Description("radio")] Radio = 19,
	[Description("select")] Select = 20,
	[Description("button")] Button = 21,
	[Description("submit")] Submit = 22,
	[Description("reset")] Reset = 23,
	[Description("image")] Image = 24,
}

public enum ElementType {
	[Description("input")] Input = 0,
	[Description("textarea")] Textarea = 1,
	[Description("select")] Select = 2,
}

public enum FormHttpMethod {
	[Description("get")] Get = 1,
	[Description("post")] Post = 2,
}
public enum FormEnctype {
	[Description("text/plain")] TextPlain = 0,
	[Description("multipart/form-data")] MultipartFormData = 1,
	[Description("application/x-www-form-urlencoded")] FormUrlEncoded = 2,
}
public enum PopupType {
	Form = 0,
	Question = 1,
}