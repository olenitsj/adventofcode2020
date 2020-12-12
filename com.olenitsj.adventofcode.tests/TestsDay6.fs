module TestsDay6

open System
open Xunit
open Day6

[<Fact>]
let ``Split in groups`` () =
    let input = ["abc";"";"a";"b";"c";"";"ab";"ac";"";"a";"a";"a";"a";"";"b"]
    let expectedOutput = [["abc"];["a";"b";"c"];["ab";"ac"];["a";"a";"a";"a"];["b"]]
    Assert.Equal<string list>(expectedOutput, splitInGroups input)

[<Fact>]
let ``Combine list`` () =
     let input = [["abc"];["a";"b";"c"];["ab";"ac"];["a";"a";"a";"a"];["b"]]
     let expectedOutput = [['a';'b';'c'];['a';'b';'c'];['a';'b';'c'];['a'];['b']]
     Assert.Equal<char list>(expectedOutput, getUniqueAnswers input)

[<Fact>]
let ``Sum of all answers`` () =
    let input = [['a';'b';'c'];['a';'b';'c'];['a';'b';'c'];['a'];['b']]
    let expectedOutput = 11
    Assert.Equal(expectedOutput, sumOfAllGroups input)