# API Specification for ProductController

## Base URL
`/api/product`

## Endpoints

### 1. Get All Products
**GET** `/api/product`

#### Description
Retrieves a list of all products.

#### Response
- **200 OK**: Returns an array of `ProductDTO` objects.

---

### 2. Get Product by ID
**GET** `/api/product/id/{id}`

#### Description
Retrieves a specific product by its ID.

#### Response
- **200 OK**: Returns a `ProductDTO` object.
- **404 Not Found**: If the product does not exist.

---

### 3. Get Product by Name
**GET** `/api/product/name/{name}`

#### Description
Retrieves a specific product by its name.

#### Response
- **200 OK**: Returns a `ProductDTO` object.
- **404 Not Found**: If the product does not exist.

---

### 4. Create a Product
**POST** `/api/product`

#### Description
Creates a new product.

#### Request Body
```json
{
  "productId": "string",
  "name": "string",
  "description": "string",
  "price": "number"
}
```

#### Response
- **201 Created**: Returns the created `ProductDTO` object.
- **400 Bad Request**: If the provided data is invalid.

---

### 5. Upload Product Image
**POST** `/api/product/upload-image`

#### Description
Uploads an image for a product.

#### Authorization
- Required role: `Admin`

#### Request
- Accepts an image file (`multipart/form-data`).

#### Response
- **200 OK**: Returns the uploaded image URL.
- **400 Bad Request**: If no file is uploaded.

---

### 6. Update Product Information
**PUT** `/api/product/update/{id}`

#### Description
Updates product details.

#### Authorization
- Required role: `Admin`

#### Request Body
```json
{
  "productId": "string",
  "name": "string",
  "description": "string",
  "price": "number"
}
```

#### Response
- **200 OK**: If the update was successful.
- **400 Bad Request**: If required fields are missing.
- **404 Not Found**: If the product does not exist.

---

### 7. Update Product Status
**PUT** `/api/product/status/{id}`

#### Description
Updates the status of a product.

#### Authorization
- Required role: `Admin`

#### Response
- **200 OK**: If the status update was successful.
- **400 Bad Request**: If product ID is missing.
- **404 Not Found**: If the product does not exist.

---

### 8. Delete Product
**DELETE** `/api/product/{id}`

#### Description
Deletes a product by its ID.

#### Authorization
- Required role: `Admin`

#### Response
- **204 No Content**: If the product was successfully deleted.
- **400 Bad Request**: If product ID is missing.
- **404 Not Found**: If the product does not exist.

---

## Error Codes
- **400 Bad Request**: Missing or incorrect data in the request.
- **401 Unauthorized**: User authentication failed.
- **403 Forbidden**: User lacks the required role.
- **404 Not Found**: The requested product does not exist.

## Security
- Uses role-based authorization (`Admin`) for certain endpoints.

