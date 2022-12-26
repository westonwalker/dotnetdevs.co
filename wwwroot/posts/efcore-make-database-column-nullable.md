---
title: How to make a database column nullable in Entity Framework Core
layout: page
description: If your using the code first approach in Entity Framework Core, you'll probably need to make some properties nullable in your database. Here's how you do it.
---
# How to make a database column nullable in Entity Framework Core
If your using the code first approach in Entity Framework Core, you'll probably need to make some properties nullable in your database. Here's how you do it.

In your model, add ? to your class property you want to make nullable in the database.

```csharp
public DateTime? UpdatedDate { get; set; }
```

Now, create and run a new migration.

```csharp
dotnet ef migrations add MakeUpdatedDateNullable
dotnet ef database update
```
The `UpdatedDate` column should now be nullable in your database!