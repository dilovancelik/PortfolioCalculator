# Portfolio Calculator API

This is a project build to showcase a way to calculate portfolio risk and return using MathNet.Numerics, and utilising the hardware acceleration build into the library. 

The portfolio calculations are done using linear algebra.

## Installation

The project is build using .NET core 3.0, if you have it installed, you can run the api with the following commands 
```bash
dotnet restore
dotnet build
dotnet run
```

## Usage

There is a file called test_data.json, you can use that as your body in any http post request to test the code. 

## License
[MIT](https://choosealicense.com/licenses/mit/)