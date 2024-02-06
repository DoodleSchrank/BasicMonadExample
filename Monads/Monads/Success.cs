namespace Monads.Monads;

public sealed class Success<T> : TwoTrack<T> {
    public Success(T value) => _value = value;
    private readonly T _value;

    public override TwoTrack<TReturn> Bind<TReturn>(Func<T, TwoTrack<TReturn>> func) {
        return func(_value);
    }

    public override TwoTrack<TReturn> Map<TReturn>(Func<T, TReturn> func) {
        return new Success<TReturn>(func(_value));
    }

    public override T Return(T defaultValue) => _value;

    public override TwoTrack<TReturn> Match<TReturn>(
        Func<T, TwoTrack<TReturn>>         successFunction,
        Func<Exception, TwoTrack<TReturn>> failureFunction) {
        return Bind(successFunction);
    }

    public override TwoTrack<T> SideEffect(Action<T> action) {
        action.Invoke(_value);
        return this;
    }

    public override T Catch(Func<Exception, T> func) {
        return _value;
    }
}
