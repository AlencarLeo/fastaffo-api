# 📚 Database Structure & Business Rules

> This document defines the complete database structure and business logic behind the platform.  
It supports a multi-tenant architecture for companies managing jobs and teams, with staff using a mobile app and admins using a web interface.

---

## 🧱 Tables & Relationships

### `Company`
Represents a company using the platform.

| Field            | Type   | Description                     |
|------------------|--------|---------------------------------|
| id               | string | Primary key                     |
| abn              | string | Business number                 |
| name             | string | Company name                    |
| website_url      | string | Public website                  |
| contact_info_id  | string | FK to `ContactInfo`             |

- **Relationships**:
  - Has many `Admin`
  - Has many `Job`
  - Has one `RatePolicy`
  - Uses one `ContactInfo`

---

### `Admin`
Web user who manages a company.

| Field            | Type   | Description                            |
|------------------|--------|----------------------------------------|
| id               | string | Primary key                            |
| company_id       | string | FK to `Company`                        |
| name             | string | First name                             |
| lastname         | string | Last name                              |
| email            | string | Unique email                           |
| password         | string | Hashed password                        |
| role             | enum   | 'admin' or 'owner'                     |
| contact_info_id  | string | FK to `ContactInfo`                   |

- **Permissions**: All admins of a company can edit any job of that company.

---

### `Job`
A real job or event created by an admin.

| Field         | Type   | Description                    |
|---------------|--------|--------------------------------|
| id            | string | Primary key                    |
| jobNumber     | int    | Internal job reference         |
| eventName     | string | Name of the event              |
| notes         | string | Additional notes               |
| company_id    | string | FK to `Company`                |
| created_by    | string | FK to `Admin` (creator)        |
| chargedValue  | int    | Value charged to the client    |
| clientName    | string | Client name (optional)         |

---

### `Team`
A team working on a specific job.

| Field            | Type   | Description                            |
|------------------|--------|----------------------------------------|
| id               | string | Primary key                            |
| job_id           | string | FK to `Job`                            |
| name             | string | Team name                              |
| supervisor_id    | string | ID of Admin or Staff                   |
| supervisor_type  | enum   | 'admin' or 'staff'                     |

---

### `Staff`
Mobile user who can be assigned to jobs.

| Field            | Type   | Description               |
|------------------|--------|---------------------------|
| id               | string | Primary key               |
| name             | string | First name                |
| lastname         | string | Last name                 |
| email            | string | Unique email              |
| password         | string | Hashed password           |
| contact_info_id  | string | FK to `ContactInfo`       |

---

### `Staff_Job`
Links a staff member to a job (official or personal).

| Field                | Type     | Description                                      |
|----------------------|----------|--------------------------------------------------|
| staff_id             | string   | FK to `Staff`                                    |
| job_id               | string   | FK to `Job` (nullable for personal jobs)         |
| team_id              | string   | FK to `Team` (optional)                          |
| role                 | string   | Role performed in the job                        |
| start_time           | datetime | Start time                                       |
| finish_time          | datetime | Finish time                                      |
| base_rate            | int      | Agreed base rate                                 |
| travel_time_minutes  | int      | Minutes spent traveling                          |
| kilometers           | decimal  | Kilometers traveled                              |
| allowances           | decimal  | Extra allowances                                 |
| notes                | string   | Notes for internal use                           |
| total_amount         | decimal  | Final computed amount                            |
| title                | string   | Title (only for personal jobs)                  |
| location             | string   | Location (only for personal jobs)               |

- **Primary Key**: `(staff_id, start_time)`
- **If `job_id` is null**, this is a **personal job** only visible to the staff.

---

### `Staff_Team`
Many-to-many relation between staff and teams.

| Field     | Type   | Description        |
|-----------|--------|--------------------|
| staff_id  | string | FK to `Staff`      |
| team_id   | string | FK to `Team`       |

- **Primary Key**: `(staff_id, team_id)`

---

### `Request`
Represents a job or team request/invite.

| Field     | Type   | Description                                                                 |
|-----------|--------|-----------------------------------------------------------------------------|
| id        | string | Primary key                                                                 |
| type      | enum   | 'request' (staff asking) or 'invite' (admin inviting)                        |
| target    | enum   | 'job' or 'team'                                                              |
| job_id    | string | FK to `Job`                                                                  |
| team_id   | string | FK to `Team` (optional)                                                      |
| staff_id  | string | FK to `Staff` (who is being invited or requesting)                          |
| sent_by   | string | ID of Admin or Staff (must be handled by backend logic)                      |
| status    | enum   | 'pending', 'approved_by_admin', 'approved_by_staff', 'approved', 'rejected' |
| created_at| datetime | Date of the request                                                        |

- **Final approval requires both sides to accept** (`approved_by_admin` + `approved_by_staff` → `approved`).

---

### `ContactInfo`
Reusable contact and address data.

| Field            | Type   | Description         |
|------------------|--------|---------------------|
| id               | string | Primary key         |
| photo_logo_url   | string | Avatar or logo URL  |
| phone_number     | string | Contact number      |
| postal_code      | string | Postal code         |
| state            | string | State               |
| city             | string | City                |
| address_line1    | string | Street address 1    |
| address_line2    | string | Street address 2    |

Used by: `Company`, `Admin`, `Staff`

---

### `RatePolicy`
Defines how the company calculates staff payment.

| Field               | Type    | Description                          |
|---------------------|---------|--------------------------------------|
| id                  | string  | Primary key                          |
| company_id          | string  | FK to `Company`                      |
| overtime_start_minutes | int | Time in minutes before overtime      |
| overtime_multiplier | decimal | Ex: 1.5 for 150% pay                 |
| day_multiplier      | decimal | Weekend or holiday multiplier        |
| travel_time_rate    | int     | Value per minute                     |
| kilometers_rate     | int     | Value per kilometer                  |
| allowances_rate     | int     | Default fixed amount per allowance   |
| extras_rate         | int     | Other flexible extras (optional)     |

- Staff only sees the result. Rules are defined by the company.

---

## 🔐 Permission Logic (Outside Database)

| Action                         | Who can do it                     | Notes                                                         |
|--------------------------------|-----------------------------------|---------------------------------------------------------------|
| Create/Edit Job (official)     | Any Admin of the company          | Backend must validate `company_id` match                     |
| View/Edit Job (personal)       | The owning Staff only             | `job_id` in `Staff_Job` is null                              |
| Staff joins a job/team         | Needs a `Request` and approval    | Dual approval logic                                           |
| Edit `RatePolicy`              | Admin only                        | Affects all job calculations for that company                |
| Insert travel/time/km/extra    | Staff                             | But Admin may review or validate before payment              |

---

## ✅ Summary

- Official jobs are created and owned by companies, editable by their admins.
- Personal jobs are private and created by staff for their own use.
- Flexible payment system supports overtime, kilometers, travel time, and allowances.
- Staffs are mobile-focused, admins are web-focused.
- The system is permission-based and scalable.








---

## 🗃️ Database

The full database schema is defined using DBML and can be viewed in [`docs/database.md`](./docs/database.md).  
Visualize it easily using [dbdiagram.io](https://dbdiagram.io/).

---

## 🧪 Running Locally

> Requirements: .NET 8 SDK + MySQL Server

```bash
# Clone the repository
git clone https://github.com/yourusername/jobflow.git
cd jobflow/backend

# Restore dependencies and run the API
dotnet restore
dotnet run

