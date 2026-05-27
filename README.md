# Microsoft Full-Stack Developer Training Project Archive

This repository is the main portfolio directory where projects and technical assignments developed by **Mert Ege Cetinkaya** as part of the Microsoft Full-Stack Developer training program are gathered under one roof.

---

### Project 05: SafeVault Security API (Defense-in-Depth)

A highly secure REST API demonstrating Defense-in-Depth strategies. This project focuses on implementing robust authentication, authorization, and data sanitization techniques to protect against common web vulnerabilities like XSS and SQL Injection, complete with automated integration tests.

* **Technologies Used:** C#, .NET 8, JWT Bearer Authentication, Role-Based Access Control (RBAC), xUnit, WebApplicationFactory.
* **Key Features:**
  * **Authentication & RBAC:** Implemented JWT generation and strict role-based endpoint protection (e.g., locking sensitive data behind `RequireRole("Admin")`).
  * **Input Validation & SQLi Prevention:** Utilized strongly-typed models and Data Annotations (`[RegularExpression]`, `[StringLength]`) to instantly reject malicious inputs and prevent SQL Injection attacks.
  * **XSS Sanitization:** Integrated `HtmlEncoder.Default.Encode()` to neutralize dangerous HTML/JS payloads (Cross-Site Scripting) before they reach the data store.
  * **Automated Security Testing:** Built a side-by-side xUnit integration testing architecture to automatically verify 401 Unauthorized, 403 Forbidden, and 400 Bad Request responses.

---

#### 🛡️ Engineering Architecture: Defense-in-Depth Security Pipeline

Below is a flowchart visualizing how an HTTP request is validated through the API's multiple security layers. It clearly illustrates the decision points for JWT authentication, RBAC authorization, and input sanitization, along with the corresponding HTTP status code responses (401, 403, 400, or 201).

<img width="800" alt="SafeVault Security Pipeline" src="https://github.com/user-attachments/assets/2242a263-165c-45a1-b306-49e7513ad2ea" />

---

### Project 04: InventoryHub - Full-Stack Management Dashboard

A comprehensive, 3-tier architecture application integrating a Blazor WebAssembly frontend with a .NET 8 Minimal API backend, utilizing Entity Framework Core and an SQLite database for persistent storage.

<img width="600" alt="InventoryHub Demo" src="https://github.com/user-attachments/assets/0725707a-d54e-4b71-bebd-467c4bf46a5b" />

* **Technologies Used:** C#, .NET 8, Blazor WebAssembly, Minimal API, Entity Framework Core, SQLite, Bootstrap 5.
* **Key Features:**
  * **Full CRUD Operations:** Seamlessly create, read, update, and delete inventory items in real-time.
  * **State-Driven UI & Caching:** Optimistic UI updates utilizing Blazor's local state management to prevent unnecessary full-database fetches.
  * **Live Search & Filtering:** Client-side real-time filtering mechanism without database round-trips.
  * **Financial & Analytical Widgets:** Dynamic calculation of total inventory value and critical stock tracking.
* **Source Code:** [📂 Go to Project Files](./04-inventory-hub)

**How I Used Copilot for System Design & Integration**
Throughout the development of InventoryHub, I utilized GitHub Copilot as an intelligent pair-programmer to accelerate architectural decisions and integrations:
1. **Domain Models & EF Core:** Generated the initial `Product` class and `AppDbContext` bridge for SQLite integration.
2. **RESTful Endpoints:** Scaffolded the Minimal API `GET`, `POST`, `PUT`, and `DELETE` routes, ensuring correct JSON serialization via `Results` methods.
3. **Performance Optimization:** Guided Copilot to refine the `HttpClient` post-request logic on the frontend to solely fetch the newly added JSON object instead of reloading the entire dataset, maximizing bandwidth efficiency.
4. **Debugging Razor Syntax:** Resolved `CS1525` lambda expression errors by prompting Copilot to extract inline UI logic into clean C# code-behind methods.

---

### Project 03: User Management API (ASP.NET Core)

This project is a RESTful API built with ASP.NET Core Minimal APIs. It features robust architecture including Custom Middleware for global exception handling and performance logging, strictly typed endpoints (`TypedResults`), and built-in input validation.

**How I Used Copilot for Debugging**

During the development of the POST `/users` endpoint, I encountered a `500 Internal Server Error`. Thanks to my Custom Global Exception Handling Middleware, the sensitive stack trace was hidden from the client, returning a secure generic JSON error. 

However, my Performance Logging Middleware successfully logged the real error in the terminal: `Unhandled exception: Simulated database failure for Copilot assignment!`.

I used GitHub Copilot Chat to debug the issue by pasting the error log. Copilot successfully identified that the issue was caused by a deliberate simulated exception I had placed in the code and instructed me to remove the `throw new Exception(...)` line.

**Debugging Proofs:**
1. **Client-Side (Swagger):** Secure 500 error response hiding the stack trace.
   <br><img width="400" alt="swagger" src="https://github.com/user-attachments/assets/44beade4-2da2-4941-a758-39a4996b5760" />
2. **Server-Side (Terminal):** Custom middleware successfully logging the real exception and execution time.
   <br><img width="500" alt="Terminal Log" src="https://github.com/user-attachments/assets/580f9755-a8fd-4615-be2d-5f655a017438" />
3. **Resolution (Copilot Chat):** AI identifying the root cause and providing the solution.
   <br><img width="400" alt="Copilot Chat" src="https://github.com/user-attachments/assets/917765fe-e026-45a7-b1db-e15ebc83124a" />
* **Source Code:** [📂 Go to Project Files](./03-user-management-api)

---

### 02. EventEase - Event Management Application (Blazor Web App)
A modern and dynamic single-page application (SPA) developed using the Blazor architecture.

<img width="600" alt="EventEase Demo" src="https://github.com/user-attachments/assets/3c8c4580-cec8-47b1-9e95-4e109e0ada38" />

* **Technologies Used:** C#, .NET 8, Blazor, HTML/CSS
* **Key Features:**
  * Real-time attendance tracking with advanced State Management.
  * Two-Way Data Binding and instant Input Validation.
  * Component-based architecture, sharply aligned modern card design, and seamless Routing without page reloads.
* **Source Code:** [📂 Go to Project Files](./02-blazor-app)

---

### 01. Personal Portfolio Website
A personal website developed for the front-end fundamentals module of the training program.

<img width="600" alt="Portfolio Demo" src="https://github.com/user-attachments/assets/3b6712b8-7866-4074-820b-8e12cd0b9272" />

* **Technologies Used:** HTML5, CSS3, Vanilla JavaScript
* **Key Features:**
  * Mobile-responsive Grid/Flexbox architecture.
  * Accessibility-compliant interface and dynamic theme (Dark/Light mode) infrastructure.
* **Source Code:** [📂 Go to Project Files](./01-portfolio-website)

---

## Live Preview

You can use the link below to access HTML/CSS-based static projects directly via your browser:
**[mertege1.github.io/microsoft-fullstack-course/](https://mertege1.github.io/microsoft-fullstack-course/)**

*(Note: For interactive projects that require a background .NET server and an active SignalR connection/database, such as Blazor and Minimal APIs, you can access the source code directly from the respective folder links and review the application workflow from the preview GIFs above.)*
