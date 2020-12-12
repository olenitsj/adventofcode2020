module PassportValidation
open Result
open TryParser
open System.Collections.Generic
// based on Railway oriented programming https://fsharpforfunandprofit.com/posts/recipe-part2/

let eyeColors = ["amb"; "blu"; "brn"; "gry"; "grn"; "hzl"; "oth"]
let hairColorAllowedchars = ['0';'1';'2';'3';'4';'5';'6';'7';'8';'9';'a';'b';'c';'d';'e';'f';]
let pidAllowedchars = ['0';'1';'2';'3';'4';'5';'6';'7';'8';'9']
let requiredAttributes = ["byr"; "iyr"; "eyr"; "hgt" ; "hcl"; "ecl"; "pid"]

let isCompletePassport (passport: IDictionary<string, string>) =
    if Seq.except passport.Keys requiredAttributes |> Seq.length = 0
    then Result.Ok passport
    else Result.Error "Missing required passport attributes"
    
let isValidyear (passport: IDictionary<string, string>) attribute lowerBound upperBound errorMessage = 
    match passport.Item attribute with 
           | Int i when i >= lowerBound && i <= upperBound -> Result.Ok passport 
           | _ -> Result.Error errorMessage

let isValidBirthYear (passport: IDictionary<string, string>) =
    isValidyear passport "byr" 1920 2002 "Invalid Birth Year"

let isValidIssueYear (passport: IDictionary<string, string>) =
    isValidyear passport "iyr" 2010 2020 "Invalid Issue Year"

let isValidExpirationYear (passport: IDictionary<string, string>) = 
    isValidyear passport "eyr" 2010 2030 "Invalid Expiration Year"

let (|CM|) (height:string) = 
    if height.EndsWith "cm" 
    then parseInt (height.Split "cm" |> Seq.head)
    else None

let (|IN|) (height:string) =
    if height.EndsWith "in" 
    then parseInt (height.Split "in" |> Seq.head)
    else None

let isValidHeight (passport: IDictionary<string, string>) =
    match passport.Item "hgt" with 
    | CM height when 
        height.IsSome && height.Value >= 150 
        && height.Value <= 193 
        -> Result.Ok passport
    | IN height when 
        height.IsSome && height.Value >= 59  
        && height.Value <= 76
        -> Result.Ok passport
    | _ -> Result.Error "Invalid height"

let isValidHairColor (passport: IDictionary<string, string>) = 
    let color = passport.Item "hcl"
    if  Seq.head color = '#' 
        && Seq.tail color |> Seq.length = 6 
        && Seq.except hairColorAllowedchars <| Seq.tail color |> Seq.length = 0 
    then Result.Ok passport 
    else Result.Error "Invalid hair color"

let isValidEyeColor(passport: IDictionary<string, string>) = 
    let color = passport.Item "ecl"
    if List.contains color eyeColors 
    then Result.Ok passport 
    else Result.Error "Invalid Eye color"
    
let isValidPassportID (passport: IDictionary<string, string>) = 
    let id = passport.Item "pid"
    if  Seq.length id = 9 
        && Seq.except pidAllowedchars id |> Seq.length = 0
    then Result.Ok passport 
    else Result.Error "Invalid passport id"

let validatePassport : IDictionary<string, string> -> Result<IDictionary<string, string>, string> = 
    isCompletePassport
    >> bind isValidBirthYear 
    >> bind isValidIssueYear 
    >> bind isValidExpirationYear 
    >> bind isValidHeight 
    >> bind isValidHairColor
    >> bind isValidEyeColor
    >> bind isValidPassportID