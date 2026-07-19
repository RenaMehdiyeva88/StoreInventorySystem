# StoreInventorySystem

A layered C# (.NET) console application for managing a store's inventory: product categories, products with prices and stock levels, and a full log of stock transactions (increases/decreases). Data is stored in local text files, with the storage directory created automatically on first run.

## Features

- Add and list product categories
- Add and list products (name, price, category, stock count)
- Increase stock for a product
- Decrease stock for a product
- View the full history of stock transactions
- Input validation for prices, quantities, and IDs
- Automatic creation of the local data folder on first run

## Technologies

- C#
- .NET
- Console application
- Layered architecture (DAL / BLL / UI) with repositories and services
- File-based data storage (categories.txt, products.txt, transactions.txt)

## Architecture

The solution is split into three projects (as defined in `StoreInventorySystem.slnx`):

- **StoreInventorySystem.DAL** — Data Access Layer: repositories that read/write categories, products, and stock transactions to text files.
- **StoreInventorySystem.BLL** — Business Logic Layer: `CategoryService`, `ProductService`, and `StockService`.
- **StoreInventorySystem.UI** — Presentation Layer: the console menu and user interaction.

## Requirements

- .NET SDK installed (https://dotnet.microsoft.com/download)
- Note: the app stores its data files in a fixed local folder path (`C:\PA501Files\StoreInventorySystem`) defined in the code, and creates it automatically if missing. If you're not on Windows, update this path in `StoreInventorySystem.UI/Program.cs` before running.

## Installation and Run

git clone https://github.com/RenaMehdiyeva88/StoreInventorySystem.git
cd
