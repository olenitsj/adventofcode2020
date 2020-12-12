module Day4

open System.IO
open System
open PassportValidation
open Util

let input = seq { yield! File.ReadLines @"DayFourInput.txt" } |> Seq.toList 

 
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