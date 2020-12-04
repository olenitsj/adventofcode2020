module Day2_2
open System.IO
open TryParser

type Policy = { position1: int; position2: int; letter: char}
type Entry = {password: string; policy: Policy}

let isValid entry = Seq.indexed entry.password |> Seq.where (fun (i,e) -> e = entry.policy.letter && (i+1 = entry.policy.position1 || i+1 = entry.policy.position2)) |> Seq.tryExactlyOne

let allEntries = seq { yield! File.ReadLines @"DayTwoInput.txt" } |> Seq.choose (fun a -> 
    let spaceSplit = List.ofArray <| a.Split " "
    match spaceSplit with
        | minMax :: ch :: pass -> 
            let dashSplit = minMax.Split "-"
            Some {
            policy = {
                letter = Seq.head ch; 
                position1 = match Seq.head dashSplit |> parseInt with 
                    | Some a -> a 
                    | None -> raise (System.Exception("position1 Must be filled")); 
                position2 = match Seq.tail dashSplit |> Seq.exactlyOne|> parseInt  with 
                    | Some a -> a 
                    | None -> raise (System.Exception("position1 Must be filled"))};
            password = List.exactlyOne pass}
        | _ -> None )

let day2_2 = Seq.choose (fun a ->  isValid a) allEntries |> Seq.length