module Day3

open System.IO
let config  = [(1,1);(1,3);(1,5);(1,7);(2,1)]
let finalIndex steps stepsize listLength = steps * stepsize % listLength



let day3 = 
    config 
    |> Seq.map (fun (down, right) ->  
        seq { yield! File.ReadLines @"DayThreeInput.txt" }
        |> Seq.chunkBySize down
        |> Seq.indexed 
        |> Seq.tail
        |> Seq.map (fun (i,e) -> 
            Seq.skip (finalIndex i right (Seq.head e).Length) (Seq.head e) 
            |> Seq.head ) 
        |> Seq.filter (fun a -> a = '#') 
        |> Seq.length)
    |> Seq.fold (*) 1