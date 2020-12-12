module Day6

open System.IO
open Util

let splitInGroups input = breakDrop "" input 

let getUniqueAnswers input = 
    input |> List.map (fun a -> a |> List.map (fun b -> b |> Seq.toList) |> List.concat |> List.distinct)

let sumOfAllGroups groups = 
    groups 
    |> List.map (List.length) 
    |> List.sum

let day6 = 
    seq { yield! File.ReadLines @"DaySixInput.txt" } 
    |> List.ofSeq 
    |> splitInGroups 
    |> getUniqueAnswers 
    |> sumOfAllGroups

     
