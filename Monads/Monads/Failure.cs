namespace Monads.Monads;

public sealed class Failure<T> : TwoTrack<T> {
    public Failure(string    message) => _exception = new Exception(message);
    public Failure(Exception exception) => _exception = exception;
    private readonly Exception _exception;


    public override TwoTrack<TReturn> Bind<TReturn>(Func<T, TwoTrack<TReturn>> func) {
        return new Failure<TReturn>(_exception);
    }

    public override TwoTrack<TReturn> Map<TReturn>(Func<T, TReturn> func) {
        return new Failure<TReturn>(_exception);
    }

    public override T Return(T defaultValue) => defaultValue;

    public override TwoTrack<TReturn> Match<TReturn>(
        Func<T, TwoTrack<TReturn>>         successFunction,
        Func<Exception, TwoTrack<TReturn>> failureFunction) {
        return failureFunction(_exception);
    }

    public override TwoTrack<T> SideEffect(Action<T> action) {
        return this;
    }

    public override T Catch(Func<Exception, T> func) {
        return func(_exception);
    }
}
