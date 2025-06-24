
---

# 📚 Database Structure & Business Rules

> Multi-tenant platform for companies to manage jobs, teams, and mobile staff.
> Admins operate via web; staff via mobile app.
>  
> 🔗 View interactive schema on [dbdiagram.io](https://dbdiagram.io/d/6856419ff039ec6d36397813)

---

## 🧱 Tables & Relationships

---

### 👤 `Admin`

A user with permissions to manage a company.

| Field             | Type   | Description                  |
| ----------------- | ------ | ---------------------------- |
| `id`              | string | Primary key                  |
| `name`            | string | First name                   |
| `lastname`        | string | Last name                    |
| `email`           | string | Unique email                 |
| `password`        | string | Hashed password              |
| `role`            | enum   | 'owner' or 'admin'           |
| `company_id`      | string | FK to `Company`              |
| `contact_info_id` | string | Optional FK to `ContactInfo` |

---

### 🏢 `Company`

Represents a registered company.

| Field             | Type   | Description         |
| ----------------- | ------ | ------------------- |
| `id`              | string | Primary key         |
| `abn`             | string | Unique business ID  |
| `name`            | string | Company name        |
| `website_url`     | string | Public website      |
| `contact_info_id` | string | FK to `ContactInfo` |

**Relations**:

* Has many `Admin`
* Has many `Job`
* Has one `RatePolicy`
* May use one `ContactInfo`

---

### 📞 `ContactInfo`

Reusable contact and address data.

| Field            | Type   | Description          |
| ---------------- | ------ | -------------------- |
| `id`             | string | Primary key          |
| `photo_logo_url` | string | Avatar/logo URL      |
| `phone_number`   | string | Phone number         |
| `postal_code`    | string | Postal code          |
| `state`          | string | State                |
| `city`           | string | City                 |
| `address_line1`  | string | Address line 1       |
| `address_line2`  | string | Address line 2 (opt) |

---

### 🧮 `ExtraRateAmountEntry`

Optional extras used in staff payments.

| Field         | Type   | Description              |
| ------------- | ------ | ------------------------ |
| `id`          | string | Primary key              |
| `company_id`  | string | Optional FK to `Company` |
| `label`       | string | Name/label of the extra  |
| `description` | string | Optional explanation     |

---

### 🗂️ `Job`

Represents a work opportunity or event.

| Field            | Type   | Description                |
| ---------------- | ------ | -------------------------- |
| `id`             | string | Primary key                |
| `job_ref`        | string | Unique job reference       |
| `event_name`     | string | Event name                 |
| `notes`          | string | Additional notes           |
| `charged_amount` | int    | Value charged to client    |
| `client_name`    | string | Name of the client         |
| `status`         | enum   | 'planning', 'active', etc. |
| `location`       | string | Optional job location      |
| `company_id`     | string | FK to `Company`            |
| `created_by`     | string | FK to `Admin` (creator)    |

---

### 🧾 `RatePolicy`

Company-specific payment rules.

| Field                    | Type    | Description                      |
| ------------------------ | ------- | -------------------------------- |
| `id`                     | string  | Primary key                      |
| `company_id`             | string  | FK to `Company`                  |
| `overtime_start_minutes` | int     | When overtime kicks in (minutes) |
| `overtime_multiplier`    | decimal | 1.5 = 150% pay                   |
| `day_multiplier`         | decimal | Weekend/holiday pay rate         |
| `travel_time_rate`       | int     | Per minute rate                  |
| `kilometers_rate`        | int     | Per km                           |

---

### 📬 `Request`

Join or invite logic for jobs/teams.

| Field        | Type     | Description                                    |
| ------------ | -------- | ---------------------------------------------- |
| `id`         | string   | Primary key                                    |
| `type`       | enum     | 'request' (by staff) or 'invite' (by admin)    |
| `target`     | enum     | 'job' or 'team'                                |
| `job_id`     | string   | FK to `Job`                                    |
| `team_id`    | string   | Optional FK to `Team`                          |
| `staff_id`   | string   | FK to `Staff`                                  |
| `sent_by`    | string   | ID of who sent (must resolve in backend logic) |
| `status`     | enum     | 'pending', 'approved', 'rejected', etc.        |
| `created_at` | datetime | Creation timestamp                             |

---

### 👷‍♂️ `Staff`

Mobile users assigned to jobs.

| Field             | Type   | Description                  |
| ----------------- | ------ | ---------------------------- |
| `id`              | string | Primary key                  |
| `name`            | string | First name                   |
| `lastname`        | string | Last name                    |
| `email`           | string | Unique email                 |
| `password`        | string | Hashed password              |
| `contact_info_id` | string | Optional FK to `ContactInfo` |

---

### 📅 `StaffJob`

Links a staff member to a job or their own personal job.

| Field                 | Type     | Description                           |
| --------------------- | -------- | ------------------------------------- |
| `id`                  | string   | Primary key                           |
| `staff_id`            | string   | FK to `Staff`                         |
| `job_id`              | string   | FK to `Job` (nullable = personal job) |
| `team_id`             | string   | Optional FK to `Team`                 |
| `role`                | string   | Role played                           |
| `start_time`          | datetime | Job start                             |
| `finish_time`         | datetime | Job end                               |
| `base_rate`           | int      | Base pay                              |
| `travel_time_minutes` | int      | Travel time                           |
| `kilometers`          | decimal  | Distance travelled                    |
| `notes`               | string   | Internal notes                        |
| `total_amount`        | decimal  | Final amount                          |
| `title`               | string   | Personal job title (optional)         |
| `location`            | string   | Personal job location                 |
| `is_personal_job`     | bool     | Marks job as personal                 |

---

### 🏷️ `StaffJobAllowance`

Represents extra allowances tied to a staff job.

| Field                        | Type   | Description                             |
| ---------------------------- | ------ | --------------------------------------- |
| `id`                         | string | Primary key                             |
| `staff_job_id`               | string | FK to `StaffJob`                        |
| `extra_rate_amount_entry_id` | string | FK to `ExtraRateAmountEntry` (optional) |
| `label`                      | string | Label (fallback if FK is null)          |
| `amount`                     | int    | Amount allowed                          |

---

### 🔗 `StaffTeam`

Many-to-many relation between `Staff` and `Team`.

| Field      | Type   | Description   |
| ---------- | ------ | ------------- |
| `staff_id` | string | FK to `Staff` |
| `team_id`  | string | FK to `Team`  |

---

### 👥 `Team`

Group of staff working on a job.

| Field             | Type   | Description                         |
| ----------------- | ------ | ----------------------------------- |
| `id`              | string | Primary key                         |
| `job_id`          | string | FK to `Job`                         |
| `name`            | string | Team name                           |
| `supervisor_id`   | string | FK to Admin or Staff (custom logic) |
| `supervisor_type` | enum   | 'admin' or 'staff'                  |

---
