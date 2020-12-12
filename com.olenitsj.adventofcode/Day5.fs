module Day5

open System.IO
open SeqUtil
open System


let calculateId (row, column) = row * 8 + column 

let takeLower input = input|> List.splitInto 2 |> List.head

let takeUpper input =  input |> List.splitInto 2 |> List.tail |> List.exactlyOne 

let seperateRowfromColumn (input:string) = 
    input 
    |> Seq.chunkBySize 7 
    |> Seq.map (fun a -> String.Concat a) 
    |> toTuple

let split chacracter list = 
    match chacracter with
    | 'F'-> takeLower list
    | 'B'-> takeUpper list
    | 'R'-> takeUpper list
    | 'L'-> takeLower list
    | _ -> []

let rec decode encodedLine list = 
    match encodedLine with
    | a::rest when rest.IsEmpty -> split a list
    | a::rest -> decode rest <| split a list
    | _ -> []

let decodeLine encodedLine = 
    let rows =  [ 0 .. 127]
    let colmuns = [ 0 .. 7]
    encodedLine 
    |> seperateRowfromColumn 
    |> (fun (a,b) ->
        let row = decode (a |> Seq.toList) rows |> List.exactlyOne
        let column = decode (b |> Seq.toList) colmuns |> List.exactlyOne
        (row, column))
 
let getAllIds input = 
    input
    |> Seq.map(fun a -> 
        decodeLine a 
        |> calculateId)

let getMissingId input = 
    let allids = getAllIds input 
    let wholelist = 
        allids 
        |> (fun t -> {Seq.min t .. Seq.max t})
    
    Seq.except allids wholelist
    |> Seq.exactlyOne
        
let day5 = 
    seq { yield! File.ReadLines @"DayVijfInput.txt" } 
    |> getMissingId 
