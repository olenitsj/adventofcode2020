module Day4

open System.IO
open System
open PassportValidation

let input = seq { yield! File.ReadLines @"DayFourInput.txt" } |> Seq.toList 

let breakDrop start list = 
    let rec loop chunk chunks list = 
        match list with
        | [] -> List.rev ((List.rev chunk)::chunks)
        | x::xs when x = start && List.isEmpty chunk -> loop [xs.Head] chunks xs.Tail
        | x::xs when x = start -> loop [xs.Head] ((List.rev chunk)::chunks) xs.Tail
        | x::xs -> loop (x::chunk) chunks xs
    loop [] [] list

 
let toTuple (input : string []) = 
    (Seq.head input, Seq.tail input |> Seq.exactlyOne)
    

let passports = 
    input 
    |> breakDrop "" 
    |> List.map (fun s -> 
        let flatList = String.concat " " s
        flatList.Replace('\n', ' ') 
        |> List.ofSeq 
        |> breakDrop ' ' 
        |> List.map (fun a -> (String.Concat a).Split ":" |> toTuple)
        |> dict)

let day4 = 
    passports
    |> List.where (fun passport -> validatePassport passport = Result.Ok passport)
    |> List.length