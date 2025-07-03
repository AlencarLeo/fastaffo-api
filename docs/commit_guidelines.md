# 💾 Commit Message Guidelines

This project follows the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) specification to ensure a consistent, readable, and automated-friendly commit history.

---

## 🧱 Structure

```bash
<type>(optional scope): short description
```

- **type**: category of the change.
- **scope**: optional module or area affected.
- **description**: concise summary in the imperative tense.

---

## 🍿 Allowed Types

| Type       | Description                                                     |
| ---------- | --------------------------------------------------------------- |
| `feat`     | A new feature                                                   |
| `fix`      | A bug fix                                                       |
| `chore`    | Maintenance tasks (e.g., configs, dependencies, etc.)           |
| `refactor` | Code change that doesn’t fix a bug or add a feature             |
| `style`    | Code style changes (whitespace, formatting, missing semicolons) |
| `docs`     | Documentation-only changes                                      |
| `test`     | Adding or updating tests                                        |
| `perf`     | Performance improvements                                        |
| `ci`       | CI/CD configuration changes                                     |
| `build`    | Changes that affect the build system or external dependencies   |
| `revert`   | Reverts a previous commit                                       |

---

## ✅ Examples

```bash
feat: add JWT authentication
fix: resolve crash on email validation
docs: update installation section in README
style: fix indentation in route files
refactor(auth): extract login logic to a service
chore: add .editorconfig to project
```

---

## 🛠️ Pro Tip

Keep the subject line under **72 characters**. For more detailed commits, use a body after a blank line:

```bash
feat: implement email service

- Adds support for sending emails using SMTP
- Includes unit tests for EmailService
```

---

## ⚙️ Optional Automation

To enforce this convention automatically, you can use tools such as:

- [`commitlint`](https://commitlint.js.org/)
- [`husky`](https://typicode.github.io/husky/)
- [`standard-version`](https://github.com/conventional-changelog/standard-version)
- [`semantic-release`](https://semantic-release.gitbook.io/)

---

## 🔗 References

- [Conventional Commits](https://www.conventionalcommits.org/)
- [Semantic Release](https://semantic-release.gitbook.io/semantic-release/)

