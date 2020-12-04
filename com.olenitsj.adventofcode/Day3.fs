module Day3

open System.IO
let config  = [(1,1);(1,3);(1,5);(1,7);(2,1)]
let finalIndex steps listLength = steps * 3 % listLength


let day3 = 
    seq { yield! File.ReadLines @"DayThreeInput.txt" } 
    |> Seq.indexed 
    |> Seq.map (fun (i,e) -> Seq.skip (finalIndex i e.Length) e |> Seq.head ) 
    |> Seq.filter (fun a -> a = '#') 
    |> Seq.length