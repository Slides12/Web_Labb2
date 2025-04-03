# API Specification for CustomerController

## Base URL
`/api/customer`

## Endpoints

### 1. Get All Customers
**GET** `/api/customer`

#### Description
Retrieves a list of all registered customers.

#### Authorization
- Required role: `Admin`

#### Response
- **200 OK**: Returns an array of `CustomerDTO` objects.
- **401 Unauthorized**: If the user is not an admin.

---

### 2. Get Customer by Email
**GET** `/api/customer/{email}`

#### Description
Retrieves a specific customer by email.

#### Authorization
- Required role: `Admin`

#### Response
- **200 OK**: Returns a `CustomerDTO` object.
- **400 Bad Request**: If email is incorrect or customer is not found.
- **401 Unauthorized**: If the user is not an admin.

---

### 3. Create a Customer
**POST** `/api/customer`

#### Description
Creates a new customer.

#### Request Body
```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string"
}
```

#### Response
- **200 OK**: Returns the created `CustomerDTO` object.
- **400 Bad Request**: If data is missing or email is already in use.

---

### 4. Update Customer by Email
**PUT** `/api/customer/{email}`

#### Description
Updates a customer's details using their email.

#### Authorization
- Required role: `Admin`

#### Request Body
```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string"
}
```

#### Response
- **200 OK**: If the update was successful.
- **400 Bad Request**: If required fields are missing.
- **404 Not Found**: If the customer does not exist or update failed.
- **401 Unauthorized**: If the user is not an admin.

---

### 5. Delete Customer by Email
**DELETE** `/api/customer/{email}`

#### Description
Deletes a customer by their email.

#### Authorization
- Required role: `Admin`

#### Response
- **200 OK**: If the customer was successfully deleted.
- **400 Bad Request**: If email is invalid.
- **404 Not Found**: If the customer does not exist.
- **401 Unauthorized**: If the user is not an admin.

---

## Error Codes
- **400 Bad Request**: Missing or incorrect data in the request.
- **401 Unauthorized**: User authentication failed.
- **403 Forbidden**: User lacks the required role.
- **404 Not Found**: The requested customer does not exist.

## Security
- Uses role-based authorization (`Admin`) for most endpoints.

