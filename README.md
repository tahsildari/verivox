# Verivox Assignment - API Testing Project

This project is a lightweight C# test suite designed to automatically test the API endpoints of the Verivox Assignment project. It supports cross-platform execution on MacOS, Linux, and Windows.

## Table of Contents

- [Overview](#overview)
- [Technologies](#technologies)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Running the Tests](#running-the-tests)
- [API Endpoints](#api-endpoints)

## Overview

This project is dedicated to testing the API endpoints of a separate Verivox Assignment project. It is built using xUnit and RestSharp, with Fluent Assertions for better test readability and maintainability.

The tests cover:
- Validation of status codes (e.g., 404 for invalid zip codes)
- Verification of API responses against expected data
- Regular expression checks for German zip code formats

## Technologies

- C# (.NET 8)
- xUnit (for testing)
- RestSharp (for making HTTP requests)
- Fluent Assertions (for expressive assertions)
- Git (for version control)

## Prerequisites

Before you begin, ensure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio Code](https://code.visualstudio.com/Download) or Visual Studio

## Getting Started

1. **Clone the repository:**

   ```bash
   git clone https://github.com/tahsildari/verivox.git

2. **Configure API Base URL:**

   Make sure you have correctly set up the appsettings.json to have the ApiUrl.
   ```bash
   {
    "Settings": {
      "ApiUrl": "https://service.verivox.de/geo/latestv2/cities"
    }
   }

## Running the Tests

Using the dotnet CLI, you can easily run the tests.
```code
dotnet test
```
The result will look like:
![image](https://github.com/user-attachments/assets/86058e65-1a42-4aea-bbc0-0dd3b5f73f34)

Some test cases are Skipped. If you wish to run them, don't forget to remove the skip parameter first.
![image](https://github.com/user-attachments/assets/9be8aca3-9dcf-4543-95a3-0c1bd3fc13b9)

## API Endpoints

Two API Endpoints are used in this project.

1. [Cities endpoint](https://service.verivox.de/geo/latestv2/cities/)
   
![image](https://github.com/user-attachments/assets/fbe85bc5-98a9-40ea-8cd3-74f8ebe34349)

2. [Streets endpoint](https://service.verivox.de/geo/latestv2/cities/10409/Berlin/streets)

![image](https://github.com/user-attachments/assets/b461a684-93eb-4e6e-aa31-e781ff3246fd)
