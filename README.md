# asp_net_mvc

## Requirements
.NET 8.0

## Task
You have been hired by * (choose your own name) Books to develop an online store for them.

The store must meet the following requirements:

- [x] Customers must be able to register and create a profile
- [x] Customers must be able to view items, details, and search inventory
- [ ] Customers must be able to multiple items to their cart
- [ ] Customers must be able to perform a checkout (mock with no payment for Sprint 1)
- [ ] Each user's cart must be persisted between devices.
- [ ] A customer must be logged in to add an item to their cart.
- [ ] Any unsuccessful login attempts must be logged.

## MVC
The architecture will be divided into three primary components:

- **Model**: Represents the data structure and business logic. It directly manages the data, logic, and rules of the application. In this context, models will represent entities such as users, items, and carts, and interact with the database using T-SQL for data operations.
- **View**: The user interface of the application. Views in ASP.NET will be dynamically generated to present the models' data. Using Bootstrap will ensure the UI is responsive and visually appealing across different devices.
- **Controller**: Acts as an intermediary between models and views, handling user input and responses. Controllers will process requests, perform operations using the models, and select views to render to the user.

The client-server interaction will be facilitated through HTTP requests and responses. The client (browser) sends requests to the server, which processes them through controllers, interacts with the database as needed, and returns the appropriate views as responses.

## Technical Design

  For the Infinity Books online store, implementing a RESTful API within an ASP.NET MVC application offers a structured approach to handle various functionalities such as user registration, login, item browsing, and cart management. Here's how each part can be designed and implemented:

### 1. **User Registration and Login**

- **UserController**:
  - **Registration Endpoint** (`POST /users/register`): This endpoint will accept user registration details (e.g., name, email, password) and create a new user record in the database. It involves validating the input data, hashing the password for security, and then saving the user information.
  - **Login Endpoint** (`POST /users/login`): This endpoint will handle user login requests by verifying email and password against the database. Upon successful authentication, it will generate a session token or a cookie to maintain the user's logged-in state.

### 2. **Item Browsing**

- **ItemController**:
  - **List Items Endpoint** (`GET /items`): This will retrieve and return a list of all items or those that match certain search criteria or categories from the database.
  - **Item Details Endpoint** (`GET /items/{id}`): This endpoint will provide detailed information about a specific item identified by its `id`.

### 3. **Cart Management**

- **CartController**:
  - **Add to Cart Endpoint** (`POST /cart/add`): Allows a logged-in user to add an item to their cart. It will require user identification (from the session or token) and item details (e.g., item ID, quantity).
  - **View Cart Endpoint** (`GET /cart`): This endpoint will display the contents of the user's cart, requiring user identification to fetch the appropriate cart data from the database.
  - **Checkout Endpoint** (`POST /cart/checkout`): Initiates the checkout process for items in the cart. This could involve verifying the items' availability, calculating the total price, and then marking the items as sold or reserved. The payment processing would be mocked at this stage.

### Database Interactions and T-SQL

- Use T-SQL for all database interactions to ensure efficient data manipulation and retrieval. This includes writing stored procedures for creating users, adding items to carts, and fetching item details, which adds a layer of abstraction and security to the database operations.

### Security and Session Management

- Implement authentication and authorization using ASP.NET's built-in features or external libraries such as Identity Framework for managing users, roles, and permissions.
- Use HTTPS for all API endpoints to ensure data is encrypted in transit.
- Store hashed passwords in the database for security.
- Use tokens (e.g., JWT) or cookies for maintaining user sessions and managing access to certain endpoints.

### Project Setup in Visual Studio 2022

1. **Create a New ASP.NET MVC Project**: Start by setting up a new project in Visual Studio 2022, selecting the ASP.NET Web Application (.NET Framework) template, and then choosing the MVC project type.
2. **Configure Dependencies**: Install necessary NuGet packages for entity framework, identity framework (for authentication and authorization), and any other libraries needed for your project.
3. **Define Models, Views, and Controllers**: Based on the MVC pattern, structure your application by defining models for your data, views for your UI, and controllers to handle the business logic.
4. **Database Setup**: Using SQL Server, design your database according to the ERD previously defined, and use Entity Framework for ORM (Object-Relational Mapping) to interact with the database.

## Database
Create the database called BooksDB
### Create the tables
```sql
-- Create Category Table
CREATE TABLE Category (
 CategoryID INT PRIMARY KEY IDENTITY(1,1),
 Name NVARCHAR(255) NOT NULL,
 Description NVARCHAR(500)
);

-- Create User Table
CREATE TABLE [User] (
 UserID INT PRIMARY KEY IDENTITY(1,1),
 Email NVARCHAR(255) NOT NULL,
 Password NVARCHAR(255) NOT NULL,
 Name NVARCHAR(255) NOT NULL,
 -- Ensures email uniqueness across users
 CONSTRAINT UQ_User_Email UNIQUE (Email)
);

-- Create Item Table
CREATE TABLE Item (
 ItemID INT PRIMARY KEY IDENTITY(1,1),
 Name NVARCHAR(255) NOT NULL,
 Description NVARCHAR(1000) NOT NULL,
 Price DECIMAL(10, 2) NOT NULL,
 CategoryID INT,
 -- Foreign Key Relationship with Category Table
 FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID)
);

-- Create Cart Table
CREATE TABLE Cart (
 CartID INT PRIMARY KEY IDENTITY(1,1),
 UserID INT NOT NULL,
 DateCreated DATETIME NOT NULL DEFAULT GETDATE(),
 -- Foreign Key Relationship with User Table
 FOREIGN KEY (UserID) REFERENCES [User](UserID)
);

-- Create CartItem Table
CREATE TABLE CartItem (
 CartItemID INT PRIMARY KEY IDENTITY(1,1),
 CartID INT NOT NULL,
 ItemID INT NOT NULL,
 Quantity INT NOT NULL,
 -- Foreign Key Relationships
 FOREIGN KEY (CartID) REFERENCES Cart(CartID),
 FOREIGN KEY (ItemID) REFERENCES Item(ItemID)
);```

### Insert the data
```sql
-- Insert Categories
INSERT INTO Category (Name, Description) VALUES
('Fiction', 'Fictional works of literature'),
('Non-Fiction', 'Informative and factual texts'),
('Science Fiction', 'Novels featuring futuristic technology and space exploration'),
('Fantasy', 'Books that contain magical or supernatural elements');

-- Insert Users
INSERT INTO [User] (Email, Password, Name) VALUES
('jane.doe@example.com', 'password123', 'Jane Doe'),
('john.smith@example.com', 'password123', 'John Smith');

-- Insert Items
INSERT INTO Item (Name, Description, Price, CategoryID) VALUES
('The Great Gatsby', 'A classic novel set in the Roaring Twenties.', 19.99, 1),
('Sapiens', 'A brief history of humankind.', 22.50, 2),
('Dune', 'A science fiction saga set in a distant future.', 15.99, 3),
('Harry Potter and the Sorcerer''s Stone', 'The first book in the Harry Potter series.', 9.99, 4);

-- Insert Cart for a User
INSERT INTO Cart (UserID, DateCreated) VALUES
(1, GETDATE()); -- Assuming UserID 1 is Jane Doe

-- Insert Cart Items for the Cart
INSERT INTO CartItem (CartID, ItemID, Quantity) VALUES
(1, 1, 1), -- One copy of "The Great Gatsby"
(1, 4, 2); -- Two copies of "Harry Potter and the Sorcerer's Stone"
```

## ERD

```
[User]
 |*UserID (PK)
 |*Email
 |*Password
 |*Name
      |
      | 1
      |                                 [Category]
      |                                     |*CategoryID (PK)
      |-------------------------------------|*Name
      |                                     |*Description
      | 1
      |                                 [Item]
      |*ItemID (PK) <-----------------------|*Name
      |                                     |*Description
      |                                     |*Price
[Cart]                                     |*CategoryID (FK)
 |*CartID (PK)                             |
 |*UserID (FK)-----------------------------|
 |*DateCreated                             |
      |                                     |
      | 1                                   |
      |                                     | 1
      |                                [CartItem]
      |*CartItemID (PK) <-------------------|*CartID (FK)
                                            |*ItemID (FK)
                                            |*Quantity
```

### Explanation:

- **Entities** are shown in square brackets `[Entity Name]`.
- **Attributes** of each entity are listed underneath with a bullet (`*`), indicating their nature:
  - **(PK)** signifies a Primary Key.
  - **(FK)** denotes a Foreign Key, establishing a relationship with another entity.
- **Relationships** between entities are represented by lines, showing how entities are connected:
  - A solid line (`---`) represents a relationship, with labels near the entities indicating the nature of the relationship (`1` for one side of a relationship, connecting to another entity which could be `1` or `*` for many).
  - For instance, a `User` can have multiple `Carts` (`1 to *` relationship), while each `Cart` is associated with a single `User`.

This diagram details the structure of the Infinity Books system's database, outlining the key entities, their fields, and how they interrelate, providing a blueprint for database schema design and implementation.
