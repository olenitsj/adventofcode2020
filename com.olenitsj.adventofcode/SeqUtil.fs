module SeqUtil

let toTuple input  = 
    (Seq.head input, Seq.tail input |> Seq.exactlyOne)
    