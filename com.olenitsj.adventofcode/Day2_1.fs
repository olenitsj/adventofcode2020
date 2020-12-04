module Day2_1
open System.IO
open TryParser

type Policy = { minOccurance: int; maxOccurance: int; letter: char}
type Entry = {password: string; policy: Policy}

let isValid entry = 
    let occurances = Seq.filter (fun a -> a = entry.policy.letter) entry.password |> Seq.length  
    entry.policy.maxOccurance >= occurances && entry.policy.minOccurance <= occurances 
   

let allEntries = seq { yield! File.ReadLines @"DayTwoInput.txt" } |> Seq.choose (fun a -> 
    let spaceSplit = List.ofArray <| a.Split " "
    match spaceSplit with
        | minMax :: ch :: pass -> 
            let dashSplit = minMax.Split "-"
            Some {
            policy = {
                letter = Seq.head ch; 
                minOccurance = match Seq.head dashSplit |> parseInt with | Some a -> a | None -> raise (System.Exception("minOccureance Must be filled")); 
                maxOccurance = match Seq.tail dashSplit |> Seq.exactlyOne|> parseInt  with | Some a -> a | None -> System.Int32.MaxValue};
            password = List.exactlyOne pass}
        | _ -> None )

let day2_1 = Seq.where (fun a ->  isValid a) allEntries |> Seq.length