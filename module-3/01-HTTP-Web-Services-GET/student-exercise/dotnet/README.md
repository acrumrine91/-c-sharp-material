# Web Services GET Exercise (C#)

In this exercise, you'll work on a command-line application that displays online auction info. A portion of the command-line application is provided. You'll write the remaining functionality.

You'll add web API calls using RestSharp to retrieve a list of auctions, details for a single auction, and filter the list of auctions by title and current bid.

## Step One: Explore the API

The auction API has some auto-generated documentation available at https://te-mockauction-server.azurewebsites.net/swagger/index.html

Look through this documentation - particularly the documentation on the two Auction GET methods (`/students/{studentId}/auctions` and `/students/{studentId}/auctions`).

Wherever you see a `{}`, that indicates a placeholder for something. Here, `studentId` refers to the number on your laptop (typically on a sticker on or underneath the laptop). So if my laptop's ID was `12345`, I could get all auctions by doing a GET to `https://te-mockauction-server.azurewebsites.net/students/12345/auctions`

Before moving on to the next step, explore the web API using Postman. You can access the following endpoints:

- GET: https://te-mockauction-server.azurewebsites.net/students/{studentId}/auctions
- GET: https://te-mockauction-server.azurewebsites.net/students/{studentId}/auctions/{id} (use a number between 1 and 7 in place of `{id}`)

## Step Two: Review the starting code

### Data Model

There's a class provided in `/src/Data/Auction.cs` that represents the data model for an auction object. If you've looked at the JSON results from the API, the properties for the class should look familiar.

### Provided Code

In `UserInterface.cs`, you'll find two methods that print information to the console:

- `PrintAuctions()`: Prints a list of auctions
- `PrintAuction()`: Prints a single auction

Take a moment to review `PrintAuctions()` and `PrintAuction()`. Note how the methods access and display the properties of the `auction` object.

### Your Code

In `APIService.cs`, you'll find four methods where you'll add code to call the API methods:

- `GetAllAuctions()`
- `GetDetailsForAuction()`
- `GetAuctionsSearchTitle()`
- `GetAuctionsSearchPrice()`

In the `MenuSelection()` method, you'll see how each menu option calls these methods and passes their return values to one of the `Print` methods described in the previous section.

### Unit tests

In `AuctionApp.Tests`, you'll find the unit tests for the methods you'll write today. After you complete each step, more tests pass.

> Note: The unit tests use a third-party library called FluentAssertions. You can install it through NuGet, like RestSharp. This third-party library makes it easier to test object comparison, a task that's not always easy to do, even for experienced programmers.

## Step Four: Write the console application

### 1. Declare the API URL and RestClient

In `APIService.cs`, before adding any API code, you need to declare two variables. Place these variables between `public static class APIService` and `public static List<Auction> GetAllAuctions()` so that all methods in the class can access it.

Add these variables:

- A string to store the API URL that all methods can access. You can get the API URL from the output in Step One. This is the same URL you used to test in Postman.
- A `RestClient` to make the API calls in each method.

### 2. List all auctions

In the `GetAllAuctions()` method, remove `throw new NotImplementedException();` and add code here to:

- Create a new `RestRequest` and pass it the API URL.
- Make a `GET` request and save the response in an `IRestResponse` variable using the type parameter so RestSharp can automatically deserialize it. Hint: it'll be a collection of `Auction` items.
- `return` the deserialized object.

Once you've done this, run the unit tests. After `GetAllAuctions_ExpectList` passes, you can run the application. If you select option 1 on the menu, you'll see the ID, title, and current bid for each auction.

### 3. List details for specific auction

In the `GetDetailsForAuction()` method, remove `throw new NotImplementedException();` and add code here to:

- Create a new `RestRequest` and pass it the API URL with a slash `/` and the `auctionId` variable appended to it. Hint: look at the second URL in Step Two.
- Make a `GET` request and save the response in an `IRestResponse` variable using the type parameter so RestSharp can automatically deserialize it. This method only retrieves one `Auction` item.
- `return` the deserialized object.

Once you've done this, run the unit tests. After `GetDetailsForAuction_ExpectSpecificItems` passes, run the application. If you select option 2 on the menu, and enter an ID of one of the auctions, you'll see the full details for that auction.

### 4. Find auctions with a specified term in the title

This API URL uses a query string. If you don't remember what a query string is, refer back to the student book.

Instead of adding a slash `/`, you'll use a question mark `?` and `title_like=` before appending the `searchTitle` variable to the URL. The `title_like` parameter allows you to search for auctions that have a title containing the string you pass to it.

In the `GetAuctionsSearchTitle()` method, remove `throw new NotImplementedException();` and add code here to:

- Create a new `RestRequest` and pass it the API URL with the question mark `?` and query string to appended to it.
- Make a `GET` request and save the response in an `IRestResponse` variable using the type parameter so RestSharp can automatically deserialize it. This one is a collection again.
- `return` the deserialized object.

Once you've done this, run the unit tests. After `GetAuctionsSearchTitle_ExpectTwo` and `GetAuctionsSearchTitle_ExpectNone` pass, you can run the application. If you select option 3 on the menu, and enter a string, like `watch`, you'll see the ID, title, and current bid for each auction that matches.

### 5. Find auctions below a specified price

This API URL also uses a query string, but the parameter key is `currentBid_lte`. This parameter looks at the `currentBid` field and returns auctions that are **L**ess **T**han or **E**qual to the value you supply.

In the `GetAuctionsSearchPrice()` method, remove `throw new NotImplementedException();` and add code here to:

- Create a new `RestRequest` and pass it the API URL with the question mark `?` and query string to appended to it.
- Make a `GET` request and save the response in an `IRestResponse` variable using the type parameter so RestSharp can automatically deserialize it. This one is a collection again.
- `return` the deserialized object.

Once you've done this, run the unit tests. After `GetAuctionsSearchPrice_ExpectThree` and `GetAuctionsSearchPrice_ExpectOne` pass, you can run the application. If you select option 4 on the menu, and enter a number, like `150`, you'll see the ID, title, and current bid for each auction that matches.
