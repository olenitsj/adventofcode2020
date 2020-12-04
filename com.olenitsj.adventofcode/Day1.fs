module Day1
open System.IO
open TryParser

let cartesianProduct ng = List.foldBack(fun n g->[for n' in n do for g' in g do yield n'::g']) ng [[]]

let day1 sum n = 
    let billsList = seq { yield! File.ReadLines @"DayOneInput.txt" |> Seq.choose parseInt }    
    let billsLists = [ for _ in 1 .. n -> billsList ]
    let result = cartesianProduct billsLists |> List.where (fun a -> List.sum a = sum) |> List.head
    List.fold (*) 1 result