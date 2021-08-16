# Introduction 

This solution allows you to calculate the performance of the stock symbols price.\
The data is stored in a local database(`(localdb)\\mssqllocaldb`).

## Getting Started with Stock Symbols Performance API

In the project directory `..\StockSymbolsPerformanceAPI\StockSymbolsPerformanceAPI`, you can open console and run:

### `dotnet run`

Runs the app in the development mode.\
Open [https://localhost:5025/help/index.html](https://localhost:5025/help/index.html) to view Swagger API page in the browser.

You will also see any logs in the console.

### `Visual Studio`

You can also open project in Visual Studio and press the shortcut f5 button or press the start button of the project.\
To do this, open the file `StockSymbolsPerformanceAPI.sln` in the directory `..\StockSymbolsPerformanceAPI\` using the Visual Studio.

## Getting Started with React App

### `yarn`

Downloads and installs all dependencies.

### `yarn start`

Runs the app in the development mode.\
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

The page will reload if you make edits.\
You will also see any lint errors in the console.

You must enter the name of the symbol in the input field and choose how to calculate the performance by pressing one of the two buttons\
`Performance By Day` or `Performance By Hour` and press `Get stock symbol data`.

You can also press `Update SPY symbol data` to check if there is any new data for the `SPY` symbol.\
If new data is not found or you are launching the application for the first time, the data will be saved to the local database(`(localdb)\\mssqllocaldb`).
