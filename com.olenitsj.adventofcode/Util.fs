module Util

let toTuple input  = 
    (Seq.head input, Seq.tail input |> Seq.exactlyOne)

let breakDrop start list = 
    let rec loop chunk chunks list = 
        match list with
        | [] -> List.rev ((List.rev chunk)::chunks)
        | x::xs when x = start && List.isEmpty chunk -> loop [xs.Head] chunks xs.Tail
        | x::xs when x = start -> loop [xs.Head] ((List.rev chunk)::chunks) xs.Tail
        | x::xs -> loop (x::chunk) chunks xs
    loop [] [] list