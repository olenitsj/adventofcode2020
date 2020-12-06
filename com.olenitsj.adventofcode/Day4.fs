module Day4

open System.IO
open System

let requiredAttributes = ["byr";"iyr";"eyr";"hgt";"hcl";"ecl";"pid"] |> List.sort 
let optionalAttributes = ["cid"] |> List.sort 
let allAttributes = ["byr";"iyr";"eyr";"hgt";"hcl";"ecl";"pid";"cid"] |> List.sort 

type Passport = 
    { BirthYear: string 
      IssueYear: string 
      ExpirationYear: string 
      Height: string 
      HairColor: string 
      EyeColor: string 
      PassportID: string 
      CountryID: string}

// let (|Valid|InValid|) passport = 
    //match passport with
    // Missing attributes -> Invalid
    // BirthYear invalid ->  

let input = seq { yield! File.ReadLines @"DayFourInput.txt" } |> Seq.toList 

let breakDrop start list = 
  let rec loop chunk chunks list = 
    match list with
    | [] -> List.rev ((List.rev chunk)::chunks)
    | x::xs when x = start && List.isEmpty chunk -> loop [xs.Head] chunks xs.Tail
    | x::xs when x = start -> loop [xs.Head] ((List.rev chunk)::chunks) xs.Tail
    | x::xs -> loop (x::chunk) chunks xs
  loop [] [] list

let passports = 
    input 
    |> breakDrop "" 
    |> List.map (fun s -> 
            let flatList = String.concat " " s
            flatList.Replace('\n', ' ') 
            |> List.ofSeq 
            |> breakDrop ' ' 
            |> List.map (String.Concat))

let allkeys (passport:string list) : string list = passport |> List.map (fun b -> b.Split ":" |> Seq.head)

let day4 = 
    passports
    |> List.map (fun a -> allkeys a |> List.sort ) 
    |> List.where (fun b -> b = requiredAttributes || b = allAttributes)
    |> List.length