**This project is a school assignment developed for educational purposes and is not intended for production use.**

# Howest Deckbuilder

## Overview
Howest Deckbuilder is a .NET project designed to facilitate the creation and management of card decks, possibly for a card game like Magic: The Gathering. The repository includes various components such as a Web API, GraphQL API, and a minimal API, indicating a robust backend setup.

## Features
- **Web API**: Provides endpoints for managing card decks.
- **GraphQL API**: Offers a flexible query interface for card data.
- **Minimal API**: Lightweight API for quick and efficient interactions.
- **Shared Components**: Common logic and models shared across different parts of the application.

## Installation

1. **Clone the repository:**
    ```sh
    git clone https://github.com/SamRFi/howest-deckbuilder.git
    ```
2. **Navigate to the project directory:**
    ```sh
    cd howest-deckbuilder
    ```
3. **Restore NuGet packages:**
    ```sh
    dotnet restore
    ```
4. **Build the solution:**
    ```sh
    dotnet build
    ```

## Usage

1. **Run the Web API:**
    ```sh
    dotnet run --project Howest.Magic.WebAPI
    ```
2. **Run the GraphQL API:**
    ```sh
    dotnet run --project Howest.MagicCards.GraphQL
    ```
3. **Run the Minimal API:**
    ```sh
    dotnet run --project Howest.MagicCards.MinimalAPI
    ```

## Project Structure

- **Howest.Magic.DAL**: Data access layer, handling database interactions.
- **Howest.Magic.WebAPI**: RESTful API for deck management.
- **Howest.MagicCards.GraphQL**: GraphQL interface for advanced queries.
- **Howest.MagicCards.MinimalAPI**: Simplified API for basic operations.
- **Howest.MagicCards.Shared**: Shared logic and models.
- **Howest.MagicCards.Web**: Web interface for user interaction.
