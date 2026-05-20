# Microsoft Full-Stack Developer Training Project Archive

This repository is the main portfolio directory where projects and technical assignments developed by **Mert Ege Cetinkaya** as part of the Microsoft Full-Stack Developer training program are gathered under one roof.

---

## 📂 Projects

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

*(Note: For interactive projects that require a background .NET server and an active SignalR connection, such as Blazor, you can access the source code directly from the respective folder links and review the application workflow from the preview GIFs above.)*