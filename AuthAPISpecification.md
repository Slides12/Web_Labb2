# API Specification för AuthController

## Base URL
`/api/auth`

## Endpoints

### 1. Get All Users
**GET** `/api/auth`

#### Description
Retrieves a list of all registered users.

#### Authorization
- Required role: `Admin`

#### Response
- **200 OK**: Returns an array of `CustomerDTO` objects.
- **401 Unauthorized**: If the user is not an admin.

---

### 2. Login
**POST** `/api/auth/login`

#### Description
Authenticates a user and returns a JWT token if credentials are valid.

#### Request Body
```json
{
  "username": "string",
  "password": "string"
}
```

#### Response
- **200 OK**: Returns a token and user role.
- **401 Unauthorized**: If credentials are invalid.

---

### 3. Register
**POST** `/api/auth/register`

#### Description
Registers a new user.

#### Request Body
```json
{
  "username": "string",
  "password": "string",
  "email": "string",
  "role": "string"
}
```

#### Response
- **200 OK**: Returns the created user.
- **400 Bad Request**: If the username already exists.

---

### 4. Get User by Username
**GET** `/api/auth/get-user/{username}`

#### Description
Retrieves a user by their username.

#### Authorization
- Required role: `Admin`

#### Response
- **200 OK**: Returns a `ProductDTO` object.
- **404 Not Found**: If no user is found.

---

### 5. Update User Information
**PUT** `/api/auth/update/{username}`

#### Description
Updates a user's information.

#### Authorization
- Required role: `Admin`

#### Request Body
```json
{
  "username": "string",
  "email": "string",
  "role": "string"
}
```

#### Response
- **200 OK**: If the update was successful.
- **400 Bad Request**: If required fields are missing.
- **404 Not Found**: If the user does not exist.

---

### 6. Delete User
**DELETE** `/api/auth/{username}`

#### Description
Deletes a user by their username.

#### Authorization
- Required role: `Admin`

#### Response
- **204 No Content**: If the user was successfully deleted.
- **400 Bad Request**: If username is invalid.
- **404 Not Found**: If the user does not exist.

---

## Error Codes
- **400 Bad Request**: Missing or incorrect data in the request.
- **401 Unauthorized**: User authentication failed.
- **403 Forbidden**: User lacks the required role.
- **404 Not Found**: The requested resource does not exist.

## Security
- Uses JWT authentication.
- Admin privileges required for certain endpoints.

