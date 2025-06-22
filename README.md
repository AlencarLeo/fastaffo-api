# 🛠️ JobFlow

**JobFlow** is a platform designed to help companies manage jobs, teams, and staff assignments — with separate experiences for **admins (web)** and **staff (mobile)**.

---

## 📚 Overview

The platform allows businesses to create and manage jobs, invite or approve workers, assign roles, and define flexible rate policies (e.g. overtime, travel time, kilometers, allowances).  
Staff can also log personal jobs and track their work directly from their mobile app.

---

## 🧱 Tech Stack

- ⚙️ Backend: [.NET 8](https://dotnet.microsoft.com/)
- 🛢️ Database: MySQL + [DBML](https://dbdiagram.io/)
- 💻 Admin Interface (web): *coming soon*
- 📱 Staff App (mobile): *coming soon*
- ☁️ Hosting: Railway / Azure / Vercel (planned)

---

## 🚧 Project Status

📌 Database schema finalized  
🔧 Backend development in progress  
📱 Mobile app to be started soon

---

## 📁 Project Structure

# FasStaffo API

A back-end platform for managing job bookings, team organisation, and workforce coordination between companies and staff members (labourers). Built with .NET and Entity Framework.

---

## ✅ Phase 1 – Core Job Booking System (Completed)

The first milestone of the project implements the full cycle of job creation and staff assignment:

### Features:
- **Companies** and their admins (owners or regular) can:
  - Create jobs and define teams.
  - Invite staff or receive requests to join jobs/teams.
- **Staff (labour)** users can:
  - Accept or request jobs/teams.
  - Create **personal jobs** for tracking their own work (e.g. freelance work).
  - Add working time, travel time, kilometres, allowances.
  - See their upcoming and past jobs.
- **Requests system** supporting:
  - Type: `invite` or `request`
  - Target: `job` or `team`
  - Approval workflow: staff + admin
- **Company rate policies**:
  - Overtime, day multipliers, travel and kilometre compensation.
- **Allowances**:
  - Custom types created by the company (e.g. meal, overnight)
  - Staff can attach them to jobs with specific values.

The system ensures data consistency, separation of concerns, and tracking of relationships between staff, teams, jobs, and companies.

---

## 🛣 Roadmap

### 🔄 Phase 2 – Dynamic Role Management
- Admins can define roles (e.g. Runner, Site Manager).
- Staff can be assigned predefined roles per job/team via dropdowns.

### 📊 Phase 3 – Labour and Company Statistics
- Labour:
  - Total hours worked
  - Jobs completed
  - Total earnings
- Companies:
  - Labour usage metrics
  - Cost summaries
  - Job history

### 📇 Phase 4 – Company Client Book
- Each company can save a list of clients.
- Quick select client when creating a job.
- Track client history and job frequency.

### 📤 Phase 5 – Labour Reports
- Weekly or job-based reports.
- Sent to company admins with:
  - Hours worked
  - Costs
  - Staff involved

### 🔔 Phase 6 – Push Notifications (Labour App)
- Job invitations
- Job start reminders
- Shift changes or updates

### 🌟 Phase 7 – Evaluation System
- Clients can evaluate:
  - Individual staff
  - Whole crew
  - Overall job quality
- Via secure link or post-job portal

### 📩 Phase 8 – Invoicing System
- Labour can generate invoices based on jobs worked.
- Send invoice to company admins.
- Optional company confirmation workflow.

### 🧭 Phase 9 – Labour Marketplace
- Labour can:
  - View available jobs from other companies
  - Apply directly
  - Maintain full profile: documents, tools, skills
- Companies can see matching profiles.

### ⏰ Phase 10 – Job Schedule Conflict Detection
- Prevent staff from being scheduled in overlapping jobs.
- Warnings for company/admin and labour.

### 💬 Phase 11 – In-App Chat
- Real-time messaging between:
  - Admin ↔ Staff
  - Team-level discussion channels

### 🛰 Phase 12 – Job Tracking
- Labour can check-in/out of jobs.
- GPS location capture (optional).
- Track time automatically.

### 🔍 Phase 13 – Job Discovery + Smart Assignment
- Companies find labour like Uber finds drivers.
- Labours receive job suggestions based on skills, tools, and location.
- Improved tabs, filters, and batch application for labourers.

---

## 🛠️ Tech Stack

- **Backend:** .NET 8, Entity Framework Core
- **Database:** SQL Server (Code-first approach with strong relational modeling)
- **Authentication:** (To be defined)
- **Frontend (planned):**
  - Web App for Admins (React/Next.js)
  - Mobile App for Labourers (React Native)

---

## 📂 Structure (Coming soon)
> Folder structure and API endpoints documentation will be added here.

---

## 📌 Contributing
Contributions are welcome in future phases. For now, this project is in its early development and internal usage stage.

---

## 📃 License
[MIT License](LICENSE)

---

