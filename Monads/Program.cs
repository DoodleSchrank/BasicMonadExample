using Monads.Monads;

static TwoTrack<int> AssureIsBigger(int check, int threshold) {
    if (check > threshold) return new Success<int>(check);
    return new Failure<int>("Number was smaller!");
}

var success =
    from v1 in new Success<string>("test")
    let x = v1 + "AnotherOne"
    let y = x.Length
    from v2 in AssureIsBigger(y, 5)
    from v3 in new Success<string>(v2.ToString() + " in success")
    select v3;
success.SideEffect(action: Console.WriteLine);


var success2 =
    new Success<string>("test")
        .Bind(v1 => AssureIsBigger((v1 + "AnotherOne").Length, 5))
        .Map(v2 => v2 + " in success2")
        .SideEffect(action: Console.WriteLine);


var fail =
        (from v1 in new Success<string>("test")
        let y = v1.Length
        from v2 in AssureIsBigger(y, 5)
        select v2)
    .SideEffect(action: Console.WriteLine)
    .Match(x => new Success<string>(x.ToString()),
        x => new Success<string>(x.Message))
    .SideEffect(action: Console.WriteLine);



