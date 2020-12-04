// Learn more about F# at http://fsharp.org
open Day1
open Day2_1
open Day2_2

[<EntryPoint>]
let main argv =
   printfn "Day 1: %i" <| day1 2020 3
   printfn "Day 2_1: %A" <| day2_1
   printfn "Day 2_2: %A" <| day2_2
   0 // return an integer exit code
