# Postman Collection - User API Tests

Complete test suite for the .NET 8 User Management API.

## 📦 Quick Start

### Import to Postman

1. Open Postman
2. Click **Import**
3. Select both files:
   - `starter-pack.postman_collection.json`
   - `starter-pack.postman_environment.json`
4. Select the **".NET 8 API - Local"** environment
5. Run the tests!

## 🧪 Test Cases (10 Total)

### ✅ CRUD Operations (5 tests)
1. **Get All Users (Empty)** - Initial state, should return `[]`
2. **Create User - Success** - Creates John Doe
3. **Get All Users (With Data)** - Should show created user
4. **Update User - Success** - Updates user ID 1
5. **Delete User - Success** - Deletes user ID 1

### ❌ Error Handling (5 tests)
6. **Create User - Duplicate Email** - Should return 400
7. **Create User - Invalid Email** - Should return 400
8. **Create User - Empty Fields** - Should return 400
9. **Update User - Not Found** - Should return 404
10. **Delete User - Not Found** - Should return 404

## 🎯 Test Execution Order

**Run tests in order for best results:**
1. Start with "Get All Users (Empty)"
2. Create a user
3. Test updates and deletes
4. Test error cases

## 🔧 Environment Variables

| Variable | Value | Description |
|----------|-------|-------------|
| `url` | `http://localhost:5000` | API base URL |
| `userId` | (auto-set) | Created user ID for testing |

## ✅ Expected Results

### Success Cases
```json
// GET /api/users
[]  // or array of users

// POST /api/users (success)
1  // Returns user ID

// PUT /api/users/1
(empty 200 response)

// DELETE /api/users/1
(empty 200 response)
```

### Error Cases
```json
// Duplicate email
{
  "message": "User with email 'john.doe@example.com' already exists."
}

// Not found
{
  "message": "User with id '999' doesn't exist."
}

// Validation error
{
  "message": "The Email field is not a valid e-mail address."
}
```

## 🚀 Run All Tests

Click **Run collection** button in Postman to execute all tests automatically.

### Test Scripts Included

Each test includes automatic validation:
- ✅ Status code checks
- ✅ Response format validation
- ✅ Data type checks
- ✅ Error message verification

## 📊 Test Coverage

- ✅ GET all users
- ✅ POST create user
- ✅ PUT update user
- ✅ DELETE user
- ✅ Email validation
- ✅ Phone validation
- ✅ Required field validation
- ✅ Unique email constraint
- ✅ Not found errors
- ✅ Duplicate prevention

---

**All tests should pass when the API is running!** ✅

