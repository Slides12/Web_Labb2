# API Specification for OrderController

## Base URL
`/api/order`

## Endpoints

### 1. Get All Orders
**GET** `/api/order`

#### Description
Retrieves a list of all orders.

#### Response
- **200 OK**: Returns an array of `OrderDTO` objects.
- **500 Internal Server Error**: If an unexpected issue occurs.

---

### 2. Get Order by ID
**GET** `/api/order/{id}`

#### Description
Retrieves a specific order by its ID.

#### Response
- **200 OK**: Returns an `OrderDTO` object.
- **400 Bad Request**: If the order is not found.

---

### 3. Get Orders by Customer ID
**GET** `/api/order/orders/{customerId}`

#### Description
Retrieves all orders associated with a given customer ID.

#### Response
- **200 OK**: Returns a list of `OrderDTO` objects.
- **400 Bad Request**: If no orders are found for the specified customer ID.

---

### 4. Create an Order
**POST** `/api/order`

#### Description
Creates a new order.

#### Request Body
```json
{
  "customerID": "integer",
  "orderDetails": [
    {
      "productName": "string",
      "quantity": "integer"
    }
  ]
}
```

#### Response
- **201 Created**: Returns the created `OrderDTO` object.
- **400 Bad Request**: If required fields are missing or the request is invalid.

---

### 5. Update an Order
**PUT** `/api/order/{id}`

#### Description
Updates an existing order by its ID.

#### Request Body
```json
{
  "orderID": "integer",
  "customerID": "integer",
  "orderDetails": [
    {
      "productName": "string",
      "quantity": "integer"
    }
  ]
}
```

#### Response
- **200 OK**: If the update was successful.
- **400 Bad Request**: If required fields are missing.
- **404 Not Found**: If the order does not exist or update failed.

---

### 6. Delete an Order
**DELETE** `/api/order/{id}`

#### Description
Deletes an order by its ID.

#### Response
- **200 OK**: If the order was successfully deleted.
- **400 Bad Request**: If the ID is invalid.
- **404 Not Found**: If the order does not exist.

---

## Error Codes
- **400 Bad Request**: Missing or incorrect data in the request.
- **404 Not Found**: The requested order does not exist.
- **500 Internal Server Error**: If an unexpected issue occurs.

