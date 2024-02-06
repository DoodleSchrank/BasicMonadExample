namespace Monads.Monads;

public abstract class TwoTrack<T> {
    public abstract TwoTrack<TReturn> Bind<TReturn>(Func<T, TwoTrack<TReturn>> func);
    public abstract TwoTrack<TReturn> Map<TReturn>(Func<T, TReturn> func);
    public abstract T Return(T defaultValue);


    public static TwoTrack<T> CreateSuccess(T value) => new Success<T>(value);
    public static TwoTrack<T> CreateFailure(string message) => new Failure<T>(message);

    
    
    
    
    
    
    

    public TwoTrack<TReturn> Select<TReturn>(Func<T, TReturn> func) => Map(func);

    public TwoTrack<TReturn> SelectMany<TIntermediate, TReturn>(
        Func<T, TwoTrack<TIntermediate>> function,
        Func<T, TIntermediate, TReturn> projection) =>
        Bind(outer =>
            function(outer).Bind(inner =>
                new Success<TReturn>(projection(outer, inner))));

    
    
    
    
    
    
    
    
    

    public abstract TwoTrack<TReturn> Match<TReturn>(
        Func<T, TwoTrack<TReturn>> successFunction,
        Func<Exception, TwoTrack<TReturn>> failureFunction);


    public abstract TwoTrack<T> SideEffect(Action<T> action);

    
    
    
    
    
    
    

    public TwoTrack<TReturn> Try<TReturn>(Func<T, TReturn> func) {
        try {
            return Map(func);
        } catch (Exception ex) {
            return new Failure<TReturn>(ex);
        }
    }

    public abstract T Catch(Func<Exception, T> func);
}
