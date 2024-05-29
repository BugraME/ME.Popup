namespace ME.Popup.Exceptions.Abstract;
public abstract class BaseException : Exception {
    public BaseException(string message) : base(message) { }

    public BaseException(string message, Exception inner) : base(message, inner) { }
}