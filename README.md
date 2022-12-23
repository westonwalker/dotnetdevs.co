# DotnetDevs
The reverse job board for .NET developers.

## Getting started
### Requirements
You will need the .NET 7 SDK and MySQL installed.

### Setup
1. Clone the repo
```bash
git clone https://github.com/westonwalker/dotnetdevs.co
```
2. Create a `.env` file in the root of the project. You can copy the contents in the `example.env` into your new `.env` file. DotnetDevs uses a .env file for its connection strings and other sensitive info rather than the appsettings.json.

3. Create your MySql database and update the connection string in the .env file with the database name and user creds.

4. Restore nugets
```bash
dotnet restore
```

5. Run the application
```bash
dotnet watch
```
### Optional Setup
DotnetDevs uses 3rd party services for payments and storing user images. You can still run the app locally without these but some functionality will not work.

#### Images
1. Setup a free account on [ImageKit](https://imagekit.io/).

2. Update your .env file with your imagekit api keys.

### Payments
1. Setup a free account on [Stripe](https://stripe.com/).

2. Create your products in the Stripe dashboard.

3. Update your .env file with your stripe api keys and price id.
