# BookStore Documentation

1. Domain Layer

The Domain layer contains the core business models. The following models are used:

    Author:
        Id: Unique identifier for the author.
        FirstName: Author's first name.
        LastName: Author's last name.
        FullName: Author's full name.
        Email: Author's email address.
        PhoneNumber: Author's phone number.
        AllBooks: A collection of books associated with the author.

    Book:
        BookName: The name of the book.
        BookDescription: A description of the book.
        BookImage: An image associated with the book.
        Price: Price of the book.
        Rating: Rating of the book.
        AuthorId: Foreign key to the Author.
        Author: Navigation property to the Author model.
        PublisherId: Foreign key to the Publisher.
        Publisher: Navigation property to the Publisher model.
        BookInShoppingCarts: A collection of shopping carts that contain the book.
        BooksInOrder: A collection of orders that contain the book.

    BookInOrder:
        BookId: Foreign key to the Book.
        Book: Navigation property to the Book model.
        OrderId: Foreign key to the Order.
        Order: Navigation property to the Order model.
        Quantity: Quantity of the book in the order.

    BookInShoppingCart:
        BookId: Foreign key to the Book.
        ShoppingCartId: Foreign key to the ShoppingCart.
        Book: Navigation property to the Book model.
        ShoppingCart: Navigation property to the ShoppingCart model.
        Quantity: Quantity of the book in the shopping cart.

    Order:
        userId: The ID of the user who owns the order.
        Owner: Navigation property to the user (Owner of the order).
        BooksInOrder: A collection of books in the order.

    Publisher:
        Id: Unique identifier for the publisher.
        PublisherName: The name of the publisher.
        Books: A collection of books published by the publisher.

    ShoppingCart:
        OwnerId: The ID of the user who owns the shopping cart.
        Owner: Navigation property to the user (Owner of the shopping cart).
        BookInShoppingCarts: A collection of books in the shopping cart.



2. Repository Layer

The Repository Layer serves to abstract data access operations, isolating the database interaction logic from the rest of the application. This layer provides a uniform interface for data operations and enhances maintainability, testability, and scalability of the application.

    Interfaces:

        IOrderRepository: This interface defines operations for the Order entity. It includes basic data access methods for managing orders in the database.

            Methods:
            List<Order> GetAllOrders();
            This method retrieves a list of all orders from the database.
            Order GetDetailsForOrder(BaseEntity id);
            This method retrieves detailed information for a specific order based on the provided identifier.

        IRepository<T>: A generic interface for CRUD operations on any entity that inherits from BaseEntity. This interface serves as the base for defining operations for all entities within the system. By using this interface, we can implement operations for different types of entities without rewriting code for each entity.

            Methods:
            IEnumerable<T> GetAll();
            Retrieves all entities of type T from the database.
            T Get(Guid? id);
            Retrieves a specific entity by its identifier.
            T Insert(T entity);
            Inserts a new entity into the database.
            List<T> InsertMany(List<T> entities);
            Inserts multiple entities into the database.
            T Update(T entity);
            Updates an existing entity in the database.
            T Delete(T entity);
            Deletes an entity from the database.

        IUserRepository: This interface defines operations for managing IntegratedSystemsUser entities, which represent users in the system.

            Methods:
            IEnumerable<IntegratedSystemsUser> GetAll();
            Retrieves all users from the database.
            IntegratedSystemsUser Get(string? id);
            Retrieves a specific user by their identifier.
            void Insert(IntegratedSystemsUser entity);
            Inserts a new user into the database.
            void Update(IntegratedSystemsUser entity);
            Updates an existing user's information in the database.
            void Delete(IntegratedSystemsUser entity);
            Deletes a user from the database.



3. Service Layer

The Service layer contains business logic and operations related to the models. Some specific services in this layer are:

    IService<T>: A generic service interface used for common operations across various entities. The idea is to reduce redundancy and provide a standardized way of interacting with the database for all entities in the application
        GetAllAsync(): Fetches all entities from the database.
        GetByIdAsync(): Retrieves an entity by its id.
        AddAsync(): Adds a new entity to the database.
        UpdateAsync(): Updates an existing entity.
        DeleteAsync(): Deletes an entity by its id.
    IOrderService: A service interface for order-related operations.It handles more complex operations like calculating totals, applying discounts, processing payment, and ensuring order integrity.
        CreateOrderAsync(): Creates a new order from a shopping cart, linking it to a user and the books they purchased.
        GetUserOrdersAsync(): Retrieves all orders placed by a specific user.
        GetOrderDetailsAsync(): Provides detailed information about a specific order.
        CompleteOrderAsync(): Finalizes the order, potentially changing its status to "completed" and processing payment.
    IPdfService: The IPdfService interface is responsible for operations related to PDF generation, such as generating invoices or receipts for users after completing an order, or even generating a catalog of books.
        GenerateInvoiceAsync(): Generates a PDF invoice for a completed order, which may include details like book names, prices, and the total amount.
        GenerateBookCatalogAsync(): Generates a PDF catalog of available books, which could be used as a downloadable resource or a promotional material.
    IShoppingCartService: A service interface for shopping cart-related operations.
        GetCartAsync(): Retrieves the shopping cart for a specific user.
        AddBookToCartAsync(): Adds a specified quantity of a book to the user's shopping cart.
        RemoveBookFromCartAsync(): Removes a book from the user's cart.
        ClearCartAsync(): Clears all items from the user's cart.


4. Web Layer 

4.1. AuthorController

The AuthorController handles the HTTP requests related to authors. It performs CRUD operations (Create, Read, Update, Delete) for managing authors.

Functions of AuthorController:

    Index(): Displays all authors.
    Details(int id): Shows detailed information about a specific author.
    Create(): Provides a form for creating a new author.
    Edit(int id): Provides a form for editing an existing author's details.
    Delete(int id): Deletes an author after confirmation.
    ConfirmDelete(int id): Confirms the deletion of an author.

4.2. BookController

The BookController manages HTTP requests for books and offers CRUD operations for adding, updating, or removing books.
Functions of BookController:

    Index(): Displays all books.
    Details(int id): Displays detailed information about a specific book.
    Create(): Provides a form for creating a new book.
    Edit(int id): Provides a form for editing an existing book.
    Delete(int id): Deletes a book after confirmation.
    ConfirmDelete(int id): Confirms the deletion of a book.
    Search(string query): Searches for books based on a query.

4.3. PublisherController

The PublisherController handles requests related to publishers and allows users to perform CRUD operations for managing publishers.

Functions of PublisherController:

    Index(): Displays a list of all publishers.
    Details(int id): Shows details of a specific publisher.
    Create(): Provides a form to create a new publisher.
    Edit(int id): Provides a form to edit an existing publisher.
    Delete(int id): Deletes a publisher after confirmation.
    ConfirmDelete(int id): Confirms the deletion of a publisher.

4.4. OrderController

The OrderController manages requests related to orders, offering operations for viewing, creating, and managing orders placed by users.

Functions of OrderController:

    Index(): Displays a list of all orders.
    Details(int id): Displays details of a specific order.
    Create(): Provides a form for placing a new order.
    ConfirmOrder(int id): Confirms an order after placing it.
    Delete(int id): Deletes an order after confirmation.

4.5. ShoppingCartController

The ShoppingCartController is responsible for managing the shopping cart, allowing users to add, view, and remove items from the cart.

Functions of ShoppingCartController:

    Index(): Displays the shopping cart with all items.
    AddToCart(int bookId): Adds a book to the shopping cart.
    RemoveFromCart(int bookId): Removes a book from the shopping cart.
    ClearCart(): Clears all items from the shopping cart.
    Checkout(): Begins the checkout process, converting the cart into an order.




*Flow of Data and User Interaction

    Creating and Editing Data:
        When users create or edit an author or book, the form on the view is populated with either default or existing data. When the form is submitted (via POST), the data is sent to the controller, which then calls the appropriate service method to save or update the information in the database.

    Deleting Data:
        Deleting an author or book requires two steps: one to confirm the deletion (via GET) and the second to actually delete it (via POST). After confirmation, the controller interacts with the service layer to perform the deletion in the database.

    Displaying Data:
        Views for listing all authors or books call the controller to fetch the data and display it. The controller fetches this data from the Repository layer and passes it to the view for rendering.
        The Details action fetches specific author or book data and displays detailed information on the respective page.

    Search and Filtering:
        The Search action allows users to search books by various criteria. The controller interacts with the service layer to query the database for matching books and then renders them in the view.
