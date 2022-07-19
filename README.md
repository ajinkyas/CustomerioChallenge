# CustomerioChallenge Web API

This README outlines the details of collaborating on this ASP.NET Core Web API application.
This app loads and summarizes activity data of customers, which is stored in a data file. It creates an in-memory store for customers at startup. The customer data is exposed via a RESTful API to view and update customer data.

## Prerequisites

You will need the following things properly installed on your computer.

* [Git](https://git-scm.com/)
* [.NET 5.0](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
* [Visual Studio](https://visualstudio.microsoft.com/vs/community/)

## Installation

* `git clone <repository-url>` this repository
* `cd CustomerioChallenge`
* `dotnet build`

## Running / Development

* `cd CustomerioChallenge.Web`
* Go to file `\CustomerioChallenge\CustomerioChallenge.Web\Startup.cs` and change the path of `messages.1.data` file if needed. You can put absolute path if relative path do not work.
* `dotnet run`
* Visit Swagger UI at [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html).
* try `dotnet help` for more details

### Running Tests

* Go back to root folder 'CustomerioChallenge' [ `cd ..` ]
* `dotnet test`

## Known issues

* `POST` and `PATCH` requests are not yet implemented

### Future work

* Better testing (probably remove static classes)
* Error handling and logging

## Assumptions
* Activities in data file might not be sorted by time
* Duplicate records might be present in data file
* Timestamp is epoch time

## Class diagram

Class diagram for CustomerioChallenge.Web project

![Class diagram for CustomerioChallenge.Web project](mermaid-diagram-2022-07-19-111735.png)