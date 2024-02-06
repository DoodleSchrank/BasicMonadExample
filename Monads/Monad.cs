namespace Monads;

public abstract class Monad<T>(T value) {
    // list instantiation, .ToList(), new Enumerable<>() etc.

    // .ToList() .ToArray() .First() etc.
    public abstract Monad<TReturn> Bind<TReturn>(Func<T, Monad<TReturn>> func);
 
    // select, where etc.
    public abstract Monad<TReturn> Map<TReturn>(Func<T, TReturn> func);
 
    public T Return() => value;
}