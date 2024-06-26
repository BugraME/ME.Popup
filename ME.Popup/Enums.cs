﻿using System.ComponentModel;
using System.Reflection;

namespace ME.Popup;
public record struct EnumModel(int Value, string Name, string Description);
public static class EnumMethods {
    public static string GetEnumDescription(this Enum value) {
        FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
        if (fieldInfo == null) return "Error";
        object[] attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
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
public enum InputTypes {
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
public enum HtmlTags {
    [Description("a")] A = 0,
    [Description("abbr")] Abbr = 1,
    [Description("address")] Address = 2,
    [Description("article")] Article = 3,
    [Description("aside")] Aside = 4,
    [Description("audio")] Audio = 5,
    [Description("b")] B = 6,
    [Description("blockquote")] Blockquote = 7,
    [Description("body")] Body = 8,
    [Description("br")] Br = 9,
    [Description("button")] Button = 10,
    [Description("canvas")] Canvas = 11,
    [Description("caption")] Caption = 12,
    [Description("cite")] Cite = 13,
    [Description("code")] Code = 14,
    [Description("col")] Col = 15,
    [Description("colgroup")] Colgroup = 16,
    [Description("data")] Data = 17,
    [Description("datalist")] Datalist = 18,
    [Description("dd")] Dd = 19,
    [Description("del")] Del = 20,
    [Description("details")] Details = 21,
    [Description("dfn")] Dfn = 22,
    [Description("div")] Div = 23,
    [Description("dl")] Dl = 24,
    [Description("dt")] Dt = 25,
    [Description("em")] Em = 26,
    [Description("embed")] Embed = 27,
    [Description("fieldset")] Fieldset = 28,
    [Description("figcaption")] Figcaption = 29,
    [Description("figure")] Figure = 30,
    [Description("footer")] Footer = 31,
    [Description("form")] Form = 32,
    [Description("h1")] H1 = 33,
    [Description("h2")] H2 = 34,
    [Description("h3")] H3 = 35,
    [Description("h4")] H4 = 36,
    [Description("h5")] H5 = 37,
    [Description("h6")] H6 = 38,
    [Description("head")] Head = 39,
    [Description("header")] Header = 40,
    [Description("hr")] Hr = 41,
    [Description("html")] Html = 42,
    [Description("i")] I = 43,
    [Description("iframe")] Iframe = 44,
    [Description("img")] Img = 45,
    [Description("input")] Input = 46,
    [Description("ins")] Ins = 47,
    [Description("kbd")] Kbd = 48,
    [Description("label")] Label = 49,
    [Description("legend")] Legend = 50,
    [Description("li")] Li = 51,
    [Description("link")] Link = 52,
    [Description("main")] Main = 53,
    [Description("map")] Map = 54,
    [Description("mark")] Mark = 55,
    [Description("meta")] Meta = 56,
    [Description("meter")] Meter = 57,
    [Description("nav")] Nav = 58,
    [Description("object")] Object = 59,
    [Description("ol")] Ol = 60,
    [Description("optgroup")] Optgroup = 61,
    [Description("option")] Option = 62,
    [Description("output")] Output = 63,
    [Description("p")] P = 64,
    [Description("picture")] Picture = 65,
    [Description("pre")] Pre = 66,
    [Description("progress")] Progress = 67,
    [Description("q")] Q = 68,
    [Description("rp")] Rp = 69,
    [Description("rt")] Rt = 70,
    [Description("ruby")] Ruby = 71,
    [Description("s")] S = 72,
    [Description("samp")] Samp = 73,
    [Description("script")] Script = 74,
    [Description("section")] Section = 75,
    [Description("select")] Select = 76,
    [Description("small")] Small = 77,
    [Description("source")] Source = 78,
    [Description("span")] Span = 79,
    [Description("strong")] Strong = 80,
    [Description("style")] Style = 81,
    [Description("sub")] Sub = 82,
    [Description("summary")] Summary = 83,
    [Description("sup")] Sup = 84,
    [Description("table")] Table = 85,
    [Description("tbody")] Tbody = 86,
    [Description("td")] Td = 87,
    [Description("template")] Template = 88,
    [Description("textarea")] Textarea = 89,
    [Description("tfoot")] Tfoot = 90,
    [Description("th")] Th = 91,
    [Description("thead")] Thead = 92,
    [Description("time")] Time = 93,
    [Description("title")] Title = 94,
    [Description("tr")] Tr = 95,
    [Description("track")] Track = 96,
    [Description("u")] U = 97,
    [Description("ul")] Ul = 98,
    [Description("var")] Var = 99,
    [Description("video")] Video = 100,
    [Description("wbr")] Wbr = 101
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