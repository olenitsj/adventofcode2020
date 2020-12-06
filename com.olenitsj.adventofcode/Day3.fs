module Day3

open System.IO

let config  = [(1,1);(1,3);(1,5);(1,7);(2,1)]
let finalIndex steps stepsize listLength = steps * stepsize % listLength
let input = seq { yield! File.ReadLines @"DayThreeInput.txt" }

let day3 = 
    config 
    |> Seq.map (fun (stepsDown, stepsRight) ->  
        input
        |> Seq.chunkBySize stepsDown
        |> Seq.indexed 
        |> Seq.tail
        |> Seq.map (fun (i,e) -> 
            Seq.skip (finalIndex i stepsRight (Seq.head e).Length) (Seq.head e) 
            |> Seq.head ) 
        |> Seq.filter (fun a -> a = '#') 
        |> Seq.length)
    |> Seq.fold (*) 1