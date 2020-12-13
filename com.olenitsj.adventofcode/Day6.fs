module Day6

open System.IO
open Util

let splitInGroups input = breakDrop "" input 

let getUniqueAnswers input = 
    input 
    |> List.map (fun group -> 
        group 
        |> List.map (fun b -> b |> Seq.toList) 
        |> List.concat 
        |> List.distinct)

let getCommonAnswers input = 
    input 
    |> List.map (fun group -> 
        group
        |> List.map (fun b -> b |> Seq.toList)
        |> (fun c -> 
            List.concat c 
            |> List.countBy id 
            |> List.where (fun (x,y) -> y = group.Length)
            |> List.map (fun (x,y) -> x )))

let sumOfAllGroups groups = 
    groups 
    |> List.map (List.length) 
    |> List.sum

let readGroups = 
    seq { yield! File.ReadLines @"DaySixInput.txt" } 
    |> List.ofSeq 
    |> splitInGroups 

let day6p1 = 
    readGroups 
    |> getUniqueAnswers
    |> sumOfAllGroups
     
let day6p2 = 
    readGroups
    |> getCommonAnswers 
    |> sumOfAllGroups