module TestsDay5

open System
open Xunit
open Day5

[<Theory>]
[<InlineData(70, 7, 567)>]
[<InlineData(14, 7, 119)>]
[<InlineData(102, 4, 820)>]
let ``Seat ID calculation`` (row , column, seatID ) =
    Assert.Equal(seatID, calculateId (row, column))

[<Theory>]
[<InlineData("BFFFBBFRRR", 70, 7)>]
[<InlineData("FFFBBBFRRR", 14, 7)>]
[<InlineData("BBFFBBFRLL", 102, 4)>]
let ``Decode seat location`` (encodedLine:string, row , column) = 
    Assert.Equal((row, column), decodeLine encodedLine)

[<Fact>]
let ``Take lower half of list`` () = 
    Assert.Equal<int list>([0 .. 63], (takeLower <| [0 .. 127]))


[<Fact>]
let ``Take upper half of list`` () = 
    Assert.Equal<int list>([64 .. 127], (takeUpper <| [ 0 .. 127]))
    
[<Theory>]
[<InlineData("BFFFBBFRRR", "BFFFBBF", "RRR")>]
[<InlineData("FFFBBBFRRR", "FFFBBBF", "RRR")>]
[<InlineData("BBFFBBFRLL", "BBFFBBF", "RLL")>]
let ``Seperate Rows From Columns`` (encodedLine, row, column) = 
    Assert.Equal((row, column), seperateRowfromColumn encodedLine)

[<Fact>]
let ``Decode row`` () =
    Assert.Equal<int list>([70], decode ['B'; 'F'; 'F';'F';'B'; 'B'; 'F'] [0 .. 127])
    Assert.Equal<int list>([14], decode ['F'; 'F'; 'F';'B';'B'; 'B'; 'F'] [0 .. 127])
    Assert.Equal<int list>([102], decode ['B'; 'B'; 'F';'F';'B'; 'B'; 'F'] [0 .. 127])

[<Fact>]
let ``Decode column`` () =
    Assert.Equal<int list>([7], decode ['R'; 'R'; 'R'] [0 .. 7])
    Assert.Equal<int list>([4], decode ['R'; 'L'; 'L'] [0 .. 7])

[<Fact>]
let ``All id's`` () =
    let input = seq {"BFFFBBFRRR";"FFFBBBFRRR";"BBFFBBFRLL"}
    let expectedOutput = seq {567; 119; 820 }
    Assert.Equal<int seq>(expectedOutput, getAllIds input)  
  