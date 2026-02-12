# 🛠 NUnit API Testing in C#

Welcome! 👋  
This project is a revisited and improved version of my first NUnit API tests tutorial. Here, we automate API tests in **C#** using **NUnit**, **RestSharp**, and **Allure Reports**. The goal is to create **robust, reusable, and maintainable tests** that can run locally or in CI/CD pipelines.

---

## 📌 Features

- ✅ Automated API tests using **NUnit** and **C#**
- ✅ REST API requests handled via **RestSharp**
- ✅ Configurable environment variables via `.runsettings`
- ✅ Sensitive data protection for API keys & user data
- ✅ Allure report integration for test results
- ✅ Clean, reusable, and maintainable code structure
- ✅ Example API: [DummyJSON](https://dummyjson.com/)

---

## 🧪 Tests Included

| Endpoint | Test | Expected Result |
| --- | --- | --- |
| `/users` | Get all users | Returns `200 OK` and a list of users |
| `/users/add` | Create a new user | Returns `201 Created` with user data |

Also includes an example for NASA API testing using `.runsettings` for secure API keys.

---

## ⚙️ Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Rider](https://www.jetbrains.com/rider/)
- [Allure](https://allurereport.org)

---

### 1️⃣ Clone the repository

```bash
git clone https://github.com/asowve/ApiTestsWithNUnit.git
cd ApiTestsWithNUnit
```
### 2️⃣ Install dependencies
```bash
dotnet restore
```
### 3️⃣ Configure environment variables
- Create a .runsettings file (already included as an example)
- Add your API keys and variables
- Do NOT commit sensitive info ⚠️

Example .runsettings:

```xml
    <RunSettings>
    <TestRunParameters>
    <Parameter name="BaseUrl" value="https://dummyjson.com" />
    <Parameter name="UserName" value="John" />
    <Parameter name="LastName" value="Doe" />
    <Parameter name="Email" value="john.do@example.com" />
    <Parameter name="ApiToken" value="YOUR_API_KEY" />
    </TestRunParameters>
    </RunSettings>
```
### 4️⃣ Run the tests
```bash
dotnet test --settings .runsettings
```
Tests will execute and results will be displayed in the console.

### 5️⃣ Generate Allure report

After running the tests:
```bash
allure serve ./bin/Debug/net8.0/allure-results
```
This will open a beautiful interactive report in your browser. 🎉

## 💡 Tips & Notes
- Use Setup() in tests to avoid duplication
- Abstract API requests to make tests maintainable
- Keep sensitive data out of version control
- Allure reports give a quick visual overview of test results
- Tests are reusable and can be extended for other endpoints





