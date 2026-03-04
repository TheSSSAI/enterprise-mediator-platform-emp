# 1 Entities

## 1.1 User

### 1.1.1 Name

User

### 1.1.2 Description

Represents system users (Administrators, Vendor Contacts, Client Contacts) with authentication and profile information.

### 1.1.3 Attributes

#### 1.1.3.1 userId

##### 1.1.3.1.1 Name

userId

##### 1.1.3.1.2 Type

🔹 Guid

##### 1.1.3.1.3 Is Required

✅ Yes

##### 1.1.3.1.4 Is Primary Key

✅ Yes

##### 1.1.3.1.5 Is Unique

✅ Yes

##### 1.1.3.1.6 Index Type

UniqueIndex

##### 1.1.3.1.7 Size

0

##### 1.1.3.1.8 Constraints

*No items available*

##### 1.1.3.1.9 Default Value



##### 1.1.3.1.10 Is Foreign Key

❌ No

##### 1.1.3.1.11 Precision

0

##### 1.1.3.1.12 Scale

0

#### 1.1.3.2.0 email

##### 1.1.3.2.1 Name

email

##### 1.1.3.2.2 Type

🔹 VARCHAR

##### 1.1.3.2.3 Is Required

✅ Yes

##### 1.1.3.2.4 Is Primary Key

❌ No

##### 1.1.3.2.5 Is Unique

✅ Yes

##### 1.1.3.2.6 Index Type

UniqueIndex

##### 1.1.3.2.7 Size

255

##### 1.1.3.2.8 Constraints

- EMAIL_FORMAT

##### 1.1.3.2.9 Default Value



##### 1.1.3.2.10 Is Foreign Key

❌ No

##### 1.1.3.2.11 Precision

0

##### 1.1.3.2.12 Scale

0

#### 1.1.3.3.0 passwordHash

##### 1.1.3.3.1 Name

passwordHash

##### 1.1.3.3.2 Type

🔹 VARCHAR

##### 1.1.3.3.3 Is Required

✅ Yes

##### 1.1.3.3.4 Is Primary Key

❌ No

##### 1.1.3.3.5 Is Unique

❌ No

##### 1.1.3.3.6 Index Type

None

##### 1.1.3.3.7 Size

255

##### 1.1.3.3.8 Constraints

*No items available*

##### 1.1.3.3.9 Default Value



##### 1.1.3.3.10 Is Foreign Key

❌ No

##### 1.1.3.3.11 Precision

0

##### 1.1.3.3.12 Scale

0

#### 1.1.3.4.0 firstName

##### 1.1.3.4.1 Name

firstName

##### 1.1.3.4.2 Type

🔹 VARCHAR

##### 1.1.3.4.3 Is Required

✅ Yes

##### 1.1.3.4.4 Is Primary Key

❌ No

##### 1.1.3.4.5 Is Unique

❌ No

##### 1.1.3.4.6 Index Type

Index

##### 1.1.3.4.7 Size

100

##### 1.1.3.4.8 Constraints

*No items available*

##### 1.1.3.4.9 Default Value



##### 1.1.3.4.10 Is Foreign Key

❌ No

##### 1.1.3.4.11 Precision

0

##### 1.1.3.4.12 Scale

0

#### 1.1.3.5.0 lastName

##### 1.1.3.5.1 Name

lastName

##### 1.1.3.5.2 Type

🔹 VARCHAR

##### 1.1.3.5.3 Is Required

✅ Yes

##### 1.1.3.5.4 Is Primary Key

❌ No

##### 1.1.3.5.5 Is Unique

❌ No

##### 1.1.3.5.6 Index Type

Index

##### 1.1.3.5.7 Size

100

##### 1.1.3.5.8 Constraints

*No items available*

##### 1.1.3.5.9 Default Value



##### 1.1.3.5.10 Is Foreign Key

❌ No

##### 1.1.3.5.11 Precision

0

##### 1.1.3.5.12 Scale

0

#### 1.1.3.6.0 roleId

##### 1.1.3.6.1 Name

roleId

##### 1.1.3.6.2 Type

🔹 Guid

##### 1.1.3.6.3 Is Required

✅ Yes

##### 1.1.3.6.4 Is Primary Key

❌ No

##### 1.1.3.6.5 Is Unique

❌ No

##### 1.1.3.6.6 Index Type

Index

##### 1.1.3.6.7 Size

0

##### 1.1.3.6.8 Constraints

*No items available*

##### 1.1.3.6.9 Default Value



##### 1.1.3.6.10 Is Foreign Key

✅ Yes

##### 1.1.3.6.11 Precision

0

##### 1.1.3.6.12 Scale

0

#### 1.1.3.7.0 clientId

##### 1.1.3.7.1 Name

clientId

##### 1.1.3.7.2 Type

🔹 Guid

##### 1.1.3.7.3 Is Required

❌ No

##### 1.1.3.7.4 Is Primary Key

❌ No

##### 1.1.3.7.5 Is Unique

❌ No

##### 1.1.3.7.6 Index Type

Index

##### 1.1.3.7.7 Size

0

##### 1.1.3.7.8 Constraints

*No items available*

##### 1.1.3.7.9 Default Value



##### 1.1.3.7.10 Is Foreign Key

✅ Yes

##### 1.1.3.7.11 Precision

0

##### 1.1.3.7.12 Scale

0

#### 1.1.3.8.0 vendorId

##### 1.1.3.8.1 Name

vendorId

##### 1.1.3.8.2 Type

🔹 Guid

##### 1.1.3.8.3 Is Required

❌ No

##### 1.1.3.8.4 Is Primary Key

❌ No

##### 1.1.3.8.5 Is Unique

❌ No

##### 1.1.3.8.6 Index Type

Index

##### 1.1.3.8.7 Size

0

##### 1.1.3.8.8 Constraints

*No items available*

##### 1.1.3.8.9 Default Value



##### 1.1.3.8.10 Is Foreign Key

✅ Yes

##### 1.1.3.8.11 Precision

0

##### 1.1.3.8.12 Scale

0

#### 1.1.3.9.0 isActive

##### 1.1.3.9.1 Name

isActive

##### 1.1.3.9.2 Type

🔹 BOOLEAN

##### 1.1.3.9.3 Is Required

✅ Yes

##### 1.1.3.9.4 Is Primary Key

❌ No

##### 1.1.3.9.5 Is Unique

❌ No

##### 1.1.3.9.6 Index Type

Index

##### 1.1.3.9.7 Size

0

##### 1.1.3.9.8 Constraints

*No items available*

##### 1.1.3.9.9 Default Value

true

##### 1.1.3.9.10 Is Foreign Key

❌ No

##### 1.1.3.9.11 Precision

0

##### 1.1.3.9.12 Scale

0

#### 1.1.3.10.0 isDeleted

##### 1.1.3.10.1 Name

isDeleted

##### 1.1.3.10.2 Type

🔹 BOOLEAN

##### 1.1.3.10.3 Is Required

✅ Yes

##### 1.1.3.10.4 Is Primary Key

❌ No

##### 1.1.3.10.5 Is Unique

❌ No

##### 1.1.3.10.6 Index Type

Index

##### 1.1.3.10.7 Size

0

##### 1.1.3.10.8 Constraints

*No items available*

##### 1.1.3.10.9 Default Value

false

##### 1.1.3.10.10 Is Foreign Key

❌ No

##### 1.1.3.10.11 Precision

0

##### 1.1.3.10.12 Scale

0

#### 1.1.3.11.0 createdAt

##### 1.1.3.11.1 Name

createdAt

##### 1.1.3.11.2 Type

🔹 DateTime

##### 1.1.3.11.3 Is Required

✅ Yes

##### 1.1.3.11.4 Is Primary Key

❌ No

##### 1.1.3.11.5 Is Unique

❌ No

##### 1.1.3.11.6 Index Type

Index

##### 1.1.3.11.7 Size

0

##### 1.1.3.11.8 Constraints

*No items available*

##### 1.1.3.11.9 Default Value

CURRENT_TIMESTAMP

##### 1.1.3.11.10 Is Foreign Key

❌ No

##### 1.1.3.11.11 Precision

0

##### 1.1.3.11.12 Scale

0

#### 1.1.3.12.0 updatedAt

##### 1.1.3.12.1 Name

updatedAt

##### 1.1.3.12.2 Type

🔹 DateTime

##### 1.1.3.12.3 Is Required

✅ Yes

##### 1.1.3.12.4 Is Primary Key

❌ No

##### 1.1.3.12.5 Is Unique

❌ No

##### 1.1.3.12.6 Index Type

None

##### 1.1.3.12.7 Size

0

##### 1.1.3.12.8 Constraints

*No items available*

##### 1.1.3.12.9 Default Value

CURRENT_TIMESTAMP

##### 1.1.3.12.10 Is Foreign Key

❌ No

##### 1.1.3.12.11 Precision

0

##### 1.1.3.12.12 Scale

0

### 1.1.4.0.0 Primary Keys

- userId

### 1.1.5.0.0 Unique Constraints

- {'name': 'UC_User_Email', 'columns': ['email']}

### 1.1.6.0.0 Indexes

#### 1.1.6.1.0 IX_User_FullName

##### 1.1.6.1.1 Name

IX_User_FullName

##### 1.1.6.1.2 Columns

- firstName
- lastName

##### 1.1.6.1.3 Type

🔹 BTree

#### 1.1.6.2.0 IX_User_Active_NotDeleted

##### 1.1.6.2.1 Name

IX_User_Active_NotDeleted

##### 1.1.6.2.2 Columns

- isActive
- isDeleted

##### 1.1.6.2.3 Type

🔹 BTree

## 1.2.0.0.0 Role

### 1.2.1.0.0 Name

Role

### 1.2.2.0.0 Description

Defines user roles and permissions within the system (e.g., SystemAdmin, VendorContact).

### 1.2.3.0.0 Attributes

#### 1.2.3.1.0 roleId

##### 1.2.3.1.1 Name

roleId

##### 1.2.3.1.2 Type

🔹 Guid

##### 1.2.3.1.3 Is Required

✅ Yes

##### 1.2.3.1.4 Is Primary Key

✅ Yes

##### 1.2.3.1.5 Is Unique

✅ Yes

##### 1.2.3.1.6 Index Type

UniqueIndex

##### 1.2.3.1.7 Size

0

##### 1.2.3.1.8 Constraints

*No items available*

##### 1.2.3.1.9 Default Value



##### 1.2.3.1.10 Is Foreign Key

❌ No

##### 1.2.3.1.11 Precision

0

##### 1.2.3.1.12 Scale

0

#### 1.2.3.2.0 name

##### 1.2.3.2.1 Name

name

##### 1.2.3.2.2 Type

🔹 VARCHAR

##### 1.2.3.2.3 Is Required

✅ Yes

##### 1.2.3.2.4 Is Primary Key

❌ No

##### 1.2.3.2.5 Is Unique

✅ Yes

##### 1.2.3.2.6 Index Type

UniqueIndex

##### 1.2.3.2.7 Size

50

##### 1.2.3.2.8 Constraints

*No items available*

##### 1.2.3.2.9 Default Value



##### 1.2.3.2.10 Is Foreign Key

❌ No

##### 1.2.3.2.11 Precision

0

##### 1.2.3.2.12 Scale

0

### 1.2.4.0.0 Primary Keys

- roleId

### 1.2.5.0.0 Unique Constraints

- {'name': 'UC_Role_Name', 'columns': ['name']}

### 1.2.6.0.0 Indexes

*No items available*

## 1.3.0.0.0 Client

### 1.3.1.0.0 Name

Client

### 1.3.2.0.0 Description

Represents a client company. Central entity for client management and project association.

### 1.3.3.0.0 Attributes

#### 1.3.3.1.0 clientId

##### 1.3.3.1.1 Name

clientId

##### 1.3.3.1.2 Type

🔹 Guid

##### 1.3.3.1.3 Is Required

✅ Yes

##### 1.3.3.1.4 Is Primary Key

✅ Yes

##### 1.3.3.1.5 Is Unique

✅ Yes

##### 1.3.3.1.6 Index Type

UniqueIndex

##### 1.3.3.1.7 Size

0

##### 1.3.3.1.8 Constraints

*No items available*

##### 1.3.3.1.9 Default Value



##### 1.3.3.1.10 Is Foreign Key

❌ No

##### 1.3.3.1.11 Precision

0

##### 1.3.3.1.12 Scale

0

#### 1.3.3.2.0 companyName

##### 1.3.3.2.1 Name

companyName

##### 1.3.3.2.2 Type

🔹 VARCHAR

##### 1.3.3.2.3 Is Required

✅ Yes

##### 1.3.3.2.4 Is Primary Key

❌ No

##### 1.3.3.2.5 Is Unique

❌ No

##### 1.3.3.2.6 Index Type

Index

##### 1.3.3.2.7 Size

255

##### 1.3.3.2.8 Constraints

*No items available*

##### 1.3.3.2.9 Default Value



##### 1.3.3.2.10 Is Foreign Key

❌ No

##### 1.3.3.2.11 Precision

0

##### 1.3.3.2.12 Scale

0

#### 1.3.3.3.0 status

##### 1.3.3.3.1 Name

status

##### 1.3.3.3.2 Type

🔹 VARCHAR

##### 1.3.3.3.3 Is Required

✅ Yes

##### 1.3.3.3.4 Is Primary Key

❌ No

##### 1.3.3.3.5 Is Unique

❌ No

##### 1.3.3.3.6 Index Type

Index

##### 1.3.3.3.7 Size

50

##### 1.3.3.3.8 Constraints

- ENUM('Active', 'Inactive')

##### 1.3.3.3.9 Default Value

Active

##### 1.3.3.3.10 Is Foreign Key

❌ No

##### 1.3.3.3.11 Precision

0

##### 1.3.3.3.12 Scale

0

#### 1.3.3.4.0 isDeleted

##### 1.3.3.4.1 Name

isDeleted

##### 1.3.3.4.2 Type

🔹 BOOLEAN

##### 1.3.3.4.3 Is Required

✅ Yes

##### 1.3.3.4.4 Is Primary Key

❌ No

##### 1.3.3.4.5 Is Unique

❌ No

##### 1.3.3.4.6 Index Type

Index

##### 1.3.3.4.7 Size

0

##### 1.3.3.4.8 Constraints

*No items available*

##### 1.3.3.4.9 Default Value

false

##### 1.3.3.4.10 Is Foreign Key

❌ No

##### 1.3.3.4.11 Precision

0

##### 1.3.3.4.12 Scale

0

#### 1.3.3.5.0 createdAt

##### 1.3.3.5.1 Name

createdAt

##### 1.3.3.5.2 Type

🔹 DateTime

##### 1.3.3.5.3 Is Required

✅ Yes

##### 1.3.3.5.4 Is Primary Key

❌ No

##### 1.3.3.5.5 Is Unique

❌ No

##### 1.3.3.5.6 Index Type

Index

##### 1.3.3.5.7 Size

0

##### 1.3.3.5.8 Constraints

*No items available*

##### 1.3.3.5.9 Default Value

CURRENT_TIMESTAMP

##### 1.3.3.5.10 Is Foreign Key

❌ No

##### 1.3.3.5.11 Precision

0

##### 1.3.3.5.12 Scale

0

#### 1.3.3.6.0 updatedAt

##### 1.3.3.6.1 Name

updatedAt

##### 1.3.3.6.2 Type

🔹 DateTime

##### 1.3.3.6.3 Is Required

✅ Yes

##### 1.3.3.6.4 Is Primary Key

❌ No

##### 1.3.3.6.5 Is Unique

❌ No

##### 1.3.3.6.6 Index Type

None

##### 1.3.3.6.7 Size

0

##### 1.3.3.6.8 Constraints

*No items available*

##### 1.3.3.6.9 Default Value

CURRENT_TIMESTAMP

##### 1.3.3.6.10 Is Foreign Key

❌ No

##### 1.3.3.6.11 Precision

0

##### 1.3.3.6.12 Scale

0

### 1.3.4.0.0 Primary Keys

- clientId

### 1.3.5.0.0 Unique Constraints

*No items available*

### 1.3.6.0.0 Indexes

#### 1.3.6.1.0 IX_Client_CompanyName

##### 1.3.6.1.1 Name

IX_Client_CompanyName

##### 1.3.6.1.2 Columns

- companyName

##### 1.3.6.1.3 Type

🔹 BTree

#### 1.3.6.2.0 IX_Client_Status_NotDeleted

##### 1.3.6.2.1 Name

IX_Client_Status_NotDeleted

##### 1.3.6.2.2 Columns

- status
- isDeleted

##### 1.3.6.2.3 Type

🔹 BTree

#### 1.3.6.3.0 IX_Client_Status_Deleted_Name

##### 1.3.6.3.1 Name

IX_Client_Status_Deleted_Name

##### 1.3.6.3.2 Columns

- status
- isDeleted
- companyName

##### 1.3.6.3.3 Type

🔹 BTree

## 1.4.0.0.0 Vendor

### 1.4.1.0.0 Name

Vendor

### 1.4.2.0.0 Description

Represents a vendor company. Stores profile information, skills, and contact details.

### 1.4.3.0.0 Attributes

#### 1.4.3.1.0 vendorId

##### 1.4.3.1.1 Name

vendorId

##### 1.4.3.1.2 Type

🔹 Guid

##### 1.4.3.1.3 Is Required

✅ Yes

##### 1.4.3.1.4 Is Primary Key

✅ Yes

##### 1.4.3.1.5 Is Unique

✅ Yes

##### 1.4.3.1.6 Index Type

UniqueIndex

##### 1.4.3.1.7 Size

0

##### 1.4.3.1.8 Constraints

*No items available*

##### 1.4.3.1.9 Default Value



##### 1.4.3.1.10 Is Foreign Key

❌ No

##### 1.4.3.1.11 Precision

0

##### 1.4.3.1.12 Scale

0

#### 1.4.3.2.0 companyName

##### 1.4.3.2.1 Name

companyName

##### 1.4.3.2.2 Type

🔹 VARCHAR

##### 1.4.3.2.3 Is Required

✅ Yes

##### 1.4.3.2.4 Is Primary Key

❌ No

##### 1.4.3.2.5 Is Unique

❌ No

##### 1.4.3.2.6 Index Type

Index

##### 1.4.3.2.7 Size

255

##### 1.4.3.2.8 Constraints

*No items available*

##### 1.4.3.2.9 Default Value



##### 1.4.3.2.10 Is Foreign Key

❌ No

##### 1.4.3.2.11 Precision

0

##### 1.4.3.2.12 Scale

0

#### 1.4.3.3.0 contactInfo

##### 1.4.3.3.1 Name

contactInfo

##### 1.4.3.3.2 Type

🔹 JSON

##### 1.4.3.3.3 Is Required

❌ No

##### 1.4.3.3.4 Is Primary Key

❌ No

##### 1.4.3.3.5 Is Unique

❌ No

##### 1.4.3.3.6 Index Type

None

##### 1.4.3.3.7 Size

0

##### 1.4.3.3.8 Constraints

*No items available*

##### 1.4.3.3.9 Default Value



##### 1.4.3.3.10 Is Foreign Key

❌ No

##### 1.4.3.3.11 Precision

0

##### 1.4.3.3.12 Scale

0

#### 1.4.3.4.0 paymentDetails

##### 1.4.3.4.1 Name

paymentDetails

##### 1.4.3.4.2 Type

🔹 JSON

##### 1.4.3.4.3 Is Required

❌ No

##### 1.4.3.4.4 Is Primary Key

❌ No

##### 1.4.3.4.5 Is Unique

❌ No

##### 1.4.3.4.6 Index Type

None

##### 1.4.3.4.7 Size

0

##### 1.4.3.4.8 Constraints

*No items available*

##### 1.4.3.4.9 Default Value



##### 1.4.3.4.10 Is Foreign Key

❌ No

##### 1.4.3.4.11 Precision

0

##### 1.4.3.4.12 Scale

0

#### 1.4.3.5.0 isDeleted

##### 1.4.3.5.1 Name

isDeleted

##### 1.4.3.5.2 Type

🔹 BOOLEAN

##### 1.4.3.5.3 Is Required

✅ Yes

##### 1.4.3.5.4 Is Primary Key

❌ No

##### 1.4.3.5.5 Is Unique

❌ No

##### 1.4.3.5.6 Index Type

Index

##### 1.4.3.5.7 Size

0

##### 1.4.3.5.8 Constraints

*No items available*

##### 1.4.3.5.9 Default Value

false

##### 1.4.3.5.10 Is Foreign Key

❌ No

##### 1.4.3.5.11 Precision

0

##### 1.4.3.5.12 Scale

0

#### 1.4.3.6.0 createdAt

##### 1.4.3.6.1 Name

createdAt

##### 1.4.3.6.2 Type

🔹 DateTime

##### 1.4.3.6.3 Is Required

✅ Yes

##### 1.4.3.6.4 Is Primary Key

❌ No

##### 1.4.3.6.5 Is Unique

❌ No

##### 1.4.3.6.6 Index Type

Index

##### 1.4.3.6.7 Size

0

##### 1.4.3.6.8 Constraints

*No items available*

##### 1.4.3.6.9 Default Value

CURRENT_TIMESTAMP

##### 1.4.3.6.10 Is Foreign Key

❌ No

##### 1.4.3.6.11 Precision

0

##### 1.4.3.6.12 Scale

0

#### 1.4.3.7.0 updatedAt

##### 1.4.3.7.1 Name

updatedAt

##### 1.4.3.7.2 Type

🔹 DateTime

##### 1.4.3.7.3 Is Required

✅ Yes

##### 1.4.3.7.4 Is Primary Key

❌ No

##### 1.4.3.7.5 Is Unique

❌ No

##### 1.4.3.7.6 Index Type

None

##### 1.4.3.7.7 Size

0

##### 1.4.3.7.8 Constraints

*No items available*

##### 1.4.3.7.9 Default Value

CURRENT_TIMESTAMP

##### 1.4.3.7.10 Is Foreign Key

❌ No

##### 1.4.3.7.11 Precision

0

##### 1.4.3.7.12 Scale

0

### 1.4.4.0.0 Primary Keys

- vendorId

### 1.4.5.0.0 Unique Constraints

*No items available*

### 1.4.6.0.0 Indexes

- {'name': 'IX_Vendor_CompanyName', 'columns': ['companyName'], 'type': 'BTree'}

## 1.5.0.0.0 Skill

### 1.5.1.0.0 Name

Skill

### 1.5.2.0.0 Description

Master list of skills and areas of expertise that can be associated with vendors and projects.

### 1.5.3.0.0 Attributes

#### 1.5.3.1.0 skillId

##### 1.5.3.1.1 Name

skillId

##### 1.5.3.1.2 Type

🔹 Guid

##### 1.5.3.1.3 Is Required

✅ Yes

##### 1.5.3.1.4 Is Primary Key

✅ Yes

##### 1.5.3.1.5 Is Unique

✅ Yes

##### 1.5.3.1.6 Index Type

UniqueIndex

##### 1.5.3.1.7 Size

0

##### 1.5.3.1.8 Constraints

*No items available*

##### 1.5.3.1.9 Default Value



##### 1.5.3.1.10 Is Foreign Key

❌ No

##### 1.5.3.1.11 Precision

0

##### 1.5.3.1.12 Scale

0

#### 1.5.3.2.0 name

##### 1.5.3.2.1 Name

name

##### 1.5.3.2.2 Type

🔹 VARCHAR

##### 1.5.3.2.3 Is Required

✅ Yes

##### 1.5.3.2.4 Is Primary Key

❌ No

##### 1.5.3.2.5 Is Unique

✅ Yes

##### 1.5.3.2.6 Index Type

UniqueIndex

##### 1.5.3.2.7 Size

100

##### 1.5.3.2.8 Constraints

*No items available*

##### 1.5.3.2.9 Default Value



##### 1.5.3.2.10 Is Foreign Key

❌ No

##### 1.5.3.2.11 Precision

0

##### 1.5.3.2.12 Scale

0

#### 1.5.3.3.0 vectorEmbedding

##### 1.5.3.3.1 Name

vectorEmbedding

##### 1.5.3.3.2 Type

🔹 JSON

##### 1.5.3.3.3 Is Required

❌ No

##### 1.5.3.3.4 Is Primary Key

❌ No

##### 1.5.3.3.5 Is Unique

❌ No

##### 1.5.3.3.6 Index Type

Index

##### 1.5.3.3.7 Size

0

##### 1.5.3.3.8 Constraints

*No items available*

##### 1.5.3.3.9 Default Value



##### 1.5.3.3.10 Is Foreign Key

❌ No

##### 1.5.3.3.11 Precision

0

##### 1.5.3.3.12 Scale

0

### 1.5.4.0.0 Primary Keys

- skillId

### 1.5.5.0.0 Unique Constraints

- {'name': 'UC_Skill_Name', 'columns': ['name']}

### 1.5.6.0.0 Indexes

- {'name': 'IX_Skill_VectorEmbedding_HNSW', 'columns': ['vectorEmbedding'], 'type': 'HNSW'}

## 1.6.0.0.0 VendorSkill

### 1.6.1.0.0 Name

VendorSkill

### 1.6.2.0.0 Description

Join table to create a many-to-many relationship between Vendors and Skills.

### 1.6.3.0.0 Attributes

#### 1.6.3.1.0 vendorId

##### 1.6.3.1.1 Name

vendorId

##### 1.6.3.1.2 Type

🔹 Guid

##### 1.6.3.1.3 Is Required

✅ Yes

##### 1.6.3.1.4 Is Primary Key

✅ Yes

##### 1.6.3.1.5 Is Unique

❌ No

##### 1.6.3.1.6 Index Type

Index

##### 1.6.3.1.7 Size

0

##### 1.6.3.1.8 Constraints

*No items available*

##### 1.6.3.1.9 Default Value



##### 1.6.3.1.10 Is Foreign Key

✅ Yes

##### 1.6.3.1.11 Precision

0

##### 1.6.3.1.12 Scale

0

#### 1.6.3.2.0 skillId

##### 1.6.3.2.1 Name

skillId

##### 1.6.3.2.2 Type

🔹 Guid

##### 1.6.3.2.3 Is Required

✅ Yes

##### 1.6.3.2.4 Is Primary Key

✅ Yes

##### 1.6.3.2.5 Is Unique

❌ No

##### 1.6.3.2.6 Index Type

Index

##### 1.6.3.2.7 Size

0

##### 1.6.3.2.8 Constraints

*No items available*

##### 1.6.3.2.9 Default Value



##### 1.6.3.2.10 Is Foreign Key

✅ Yes

##### 1.6.3.2.11 Precision

0

##### 1.6.3.2.12 Scale

0

### 1.6.4.0.0 Primary Keys

- vendorId
- skillId

### 1.6.5.0.0 Unique Constraints

*No items available*

### 1.6.6.0.0 Indexes

- {'name': 'IX_VendorSkill_SkillId', 'columns': ['skillId'], 'type': 'BTree'}

## 1.7.0.0.0 Project

### 1.7.1.0.0 Name

Project

### 1.7.2.0.0 Description

Represents a single project, derived from an SOW, and is the central point for proposals, financials, and status tracking.

### 1.7.3.0.0 Attributes

#### 1.7.3.1.0 projectId

##### 1.7.3.1.1 Name

projectId

##### 1.7.3.1.2 Type

🔹 Guid

##### 1.7.3.1.3 Is Required

✅ Yes

##### 1.7.3.1.4 Is Primary Key

✅ Yes

##### 1.7.3.1.5 Is Unique

✅ Yes

##### 1.7.3.1.6 Index Type

UniqueIndex

##### 1.7.3.1.7 Size

0

##### 1.7.3.1.8 Constraints

*No items available*

##### 1.7.3.1.9 Default Value



##### 1.7.3.1.10 Is Foreign Key

❌ No

##### 1.7.3.1.11 Precision

0

##### 1.7.3.1.12 Scale

0

#### 1.7.3.2.0 clientId

##### 1.7.3.2.1 Name

clientId

##### 1.7.3.2.2 Type

🔹 Guid

##### 1.7.3.2.3 Is Required

✅ Yes

##### 1.7.3.2.4 Is Primary Key

❌ No

##### 1.7.3.2.5 Is Unique

❌ No

##### 1.7.3.2.6 Index Type

Index

##### 1.7.3.2.7 Size

0

##### 1.7.3.2.8 Constraints

*No items available*

##### 1.7.3.2.9 Default Value



##### 1.7.3.2.10 Is Foreign Key

✅ Yes

##### 1.7.3.2.11 Precision

0

##### 1.7.3.2.12 Scale

0

#### 1.7.3.3.0 name

##### 1.7.3.3.1 Name

name

##### 1.7.3.3.2 Type

🔹 VARCHAR

##### 1.7.3.3.3 Is Required

✅ Yes

##### 1.7.3.3.4 Is Primary Key

❌ No

##### 1.7.3.3.5 Is Unique

❌ No

##### 1.7.3.3.6 Index Type

Index

##### 1.7.3.3.7 Size

255

##### 1.7.3.3.8 Constraints

*No items available*

##### 1.7.3.3.9 Default Value



##### 1.7.3.3.10 Is Foreign Key

❌ No

##### 1.7.3.3.11 Precision

0

##### 1.7.3.3.12 Scale

0

#### 1.7.3.4.0 status

##### 1.7.3.4.1 Name

status

##### 1.7.3.4.2 Type

🔹 VARCHAR

##### 1.7.3.4.3 Is Required

✅ Yes

##### 1.7.3.4.4 Is Primary Key

❌ No

##### 1.7.3.4.5 Is Unique

❌ No

##### 1.7.3.4.6 Index Type

Index

##### 1.7.3.4.7 Size

50

##### 1.7.3.4.8 Constraints

- ENUM('SOW_REVIEW', 'MATCHING', 'PROPOSAL', 'ACTIVE', 'COMPLETED', 'CANCELLED')

##### 1.7.3.4.9 Default Value

SOW_REVIEW

##### 1.7.3.4.10 Is Foreign Key

❌ No

##### 1.7.3.4.11 Precision

0

##### 1.7.3.4.12 Scale

0

#### 1.7.3.5.0 marginOverride

##### 1.7.3.5.1 Name

marginOverride

##### 1.7.3.5.2 Type

🔹 DECIMAL

##### 1.7.3.5.3 Is Required

❌ No

##### 1.7.3.5.4 Is Primary Key

❌ No

##### 1.7.3.5.5 Is Unique

❌ No

##### 1.7.3.5.6 Index Type

None

##### 1.7.3.5.7 Size

0

##### 1.7.3.5.8 Constraints

*No items available*

##### 1.7.3.5.9 Default Value



##### 1.7.3.5.10 Is Foreign Key

❌ No

##### 1.7.3.5.11 Precision

5

##### 1.7.3.5.12 Scale

2

#### 1.7.3.6.0 totalInvoicedAmount

##### 1.7.3.6.1 Name

totalInvoicedAmount

##### 1.7.3.6.2 Type

🔹 DECIMAL

##### 1.7.3.6.3 Is Required

✅ Yes

##### 1.7.3.6.4 Is Primary Key

❌ No

##### 1.7.3.6.5 Is Unique

❌ No

##### 1.7.3.6.6 Index Type

None

##### 1.7.3.6.7 Size

0

##### 1.7.3.6.8 Constraints

*No items available*

##### 1.7.3.6.9 Default Value

0.00

##### 1.7.3.6.10 Is Foreign Key

❌ No

##### 1.7.3.6.11 Precision

14

##### 1.7.3.6.12 Scale

2

#### 1.7.3.7.0 totalPaidOutAmount

##### 1.7.3.7.1 Name

totalPaidOutAmount

##### 1.7.3.7.2 Type

🔹 DECIMAL

##### 1.7.3.7.3 Is Required

✅ Yes

##### 1.7.3.7.4 Is Primary Key

❌ No

##### 1.7.3.7.5 Is Unique

❌ No

##### 1.7.3.7.6 Index Type

None

##### 1.7.3.7.7 Size

0

##### 1.7.3.7.8 Constraints

*No items available*

##### 1.7.3.7.9 Default Value

0.00

##### 1.7.3.7.10 Is Foreign Key

❌ No

##### 1.7.3.7.11 Precision

14

##### 1.7.3.7.12 Scale

2

#### 1.7.3.8.0 calculatedProfit

##### 1.7.3.8.1 Name

calculatedProfit

##### 1.7.3.8.2 Type

🔹 DECIMAL

##### 1.7.3.8.3 Is Required

✅ Yes

##### 1.7.3.8.4 Is Primary Key

❌ No

##### 1.7.3.8.5 Is Unique

❌ No

##### 1.7.3.8.6 Index Type

None

##### 1.7.3.8.7 Size

0

##### 1.7.3.8.8 Constraints

*No items available*

##### 1.7.3.8.9 Default Value

0.00

##### 1.7.3.8.10 Is Foreign Key

❌ No

##### 1.7.3.8.11 Precision

14

##### 1.7.3.8.12 Scale

2

#### 1.7.3.9.0 isDeleted

##### 1.7.3.9.1 Name

isDeleted

##### 1.7.3.9.2 Type

🔹 BOOLEAN

##### 1.7.3.9.3 Is Required

✅ Yes

##### 1.7.3.9.4 Is Primary Key

❌ No

##### 1.7.3.9.5 Is Unique

❌ No

##### 1.7.3.9.6 Index Type

Index

##### 1.7.3.9.7 Size

0

##### 1.7.3.9.8 Constraints

*No items available*

##### 1.7.3.9.9 Default Value

false

##### 1.7.3.9.10 Is Foreign Key

❌ No

##### 1.7.3.9.11 Precision

0

##### 1.7.3.9.12 Scale

0

#### 1.7.3.10.0 createdAt

##### 1.7.3.10.1 Name

createdAt

##### 1.7.3.10.2 Type

🔹 DateTime

##### 1.7.3.10.3 Is Required

✅ Yes

##### 1.7.3.10.4 Is Primary Key

❌ No

##### 1.7.3.10.5 Is Unique

❌ No

##### 1.7.3.10.6 Index Type

Index

##### 1.7.3.10.7 Size

0

##### 1.7.3.10.8 Constraints

*No items available*

##### 1.7.3.10.9 Default Value

CURRENT_TIMESTAMP

##### 1.7.3.10.10 Is Foreign Key

❌ No

##### 1.7.3.10.11 Precision

0

##### 1.7.3.10.12 Scale

0

#### 1.7.3.11.0 updatedAt

##### 1.7.3.11.1 Name

updatedAt

##### 1.7.3.11.2 Type

🔹 DateTime

##### 1.7.3.11.3 Is Required

✅ Yes

##### 1.7.3.11.4 Is Primary Key

❌ No

##### 1.7.3.11.5 Is Unique

❌ No

##### 1.7.3.11.6 Index Type

None

##### 1.7.3.11.7 Size

0

##### 1.7.3.11.8 Constraints

*No items available*

##### 1.7.3.11.9 Default Value

CURRENT_TIMESTAMP

##### 1.7.3.11.10 Is Foreign Key

❌ No

##### 1.7.3.11.11 Precision

0

##### 1.7.3.11.12 Scale

0

### 1.7.4.0.0 Primary Keys

- projectId

### 1.7.5.0.0 Unique Constraints

*No items available*

### 1.7.6.0.0 Indexes

#### 1.7.6.1.0 IX_Project_ClientId

##### 1.7.6.1.1 Name

IX_Project_ClientId

##### 1.7.6.1.2 Columns

- clientId

##### 1.7.6.1.3 Type

🔹 BTree

#### 1.7.6.2.0 IX_Project_Status_NotDeleted

##### 1.7.6.2.1 Name

IX_Project_Status_NotDeleted

##### 1.7.6.2.2 Columns

- status
- isDeleted

##### 1.7.6.2.3 Type

🔹 BTree

## 1.8.0.0.0 SowDocument

### 1.8.1.0.0 Name

SowDocument

### 1.8.2.0.0 Description

Stores metadata about an uploaded Statement of Work document, including its processing status.

### 1.8.3.0.0 Attributes

#### 1.8.3.1.0 sowDocumentId

##### 1.8.3.1.1 Name

sowDocumentId

##### 1.8.3.1.2 Type

🔹 Guid

##### 1.8.3.1.3 Is Required

✅ Yes

##### 1.8.3.1.4 Is Primary Key

✅ Yes

##### 1.8.3.1.5 Is Unique

✅ Yes

##### 1.8.3.1.6 Index Type

UniqueIndex

##### 1.8.3.1.7 Size

0

##### 1.8.3.1.8 Constraints

*No items available*

##### 1.8.3.1.9 Default Value



##### 1.8.3.1.10 Is Foreign Key

❌ No

##### 1.8.3.1.11 Precision

0

##### 1.8.3.1.12 Scale

0

#### 1.8.3.2.0 projectId

##### 1.8.3.2.1 Name

projectId

##### 1.8.3.2.2 Type

🔹 Guid

##### 1.8.3.2.3 Is Required

✅ Yes

##### 1.8.3.2.4 Is Primary Key

❌ No

##### 1.8.3.2.5 Is Unique

✅ Yes

##### 1.8.3.2.6 Index Type

UniqueIndex

##### 1.8.3.2.7 Size

0

##### 1.8.3.2.8 Constraints

*No items available*

##### 1.8.3.2.9 Default Value



##### 1.8.3.2.10 Is Foreign Key

✅ Yes

##### 1.8.3.2.11 Precision

0

##### 1.8.3.2.12 Scale

0

#### 1.8.3.3.0 originalFilename

##### 1.8.3.3.1 Name

originalFilename

##### 1.8.3.3.2 Type

🔹 VARCHAR

##### 1.8.3.3.3 Is Required

✅ Yes

##### 1.8.3.3.4 Is Primary Key

❌ No

##### 1.8.3.3.5 Is Unique

❌ No

##### 1.8.3.3.6 Index Type

None

##### 1.8.3.3.7 Size

255

##### 1.8.3.3.8 Constraints

*No items available*

##### 1.8.3.3.9 Default Value



##### 1.8.3.3.10 Is Foreign Key

❌ No

##### 1.8.3.3.11 Precision

0

##### 1.8.3.3.12 Scale

0

#### 1.8.3.4.0 storagePath

##### 1.8.3.4.1 Name

storagePath

##### 1.8.3.4.2 Type

🔹 VARCHAR

##### 1.8.3.4.3 Is Required

✅ Yes

##### 1.8.3.4.4 Is Primary Key

❌ No

##### 1.8.3.4.5 Is Unique

❌ No

##### 1.8.3.4.6 Index Type

None

##### 1.8.3.4.7 Size

1,024

##### 1.8.3.4.8 Constraints

*No items available*

##### 1.8.3.4.9 Default Value



##### 1.8.3.4.10 Is Foreign Key

❌ No

##### 1.8.3.4.11 Precision

0

##### 1.8.3.4.12 Scale

0

#### 1.8.3.5.0 status

##### 1.8.3.5.1 Name

status

##### 1.8.3.5.2 Type

🔹 VARCHAR

##### 1.8.3.5.3 Is Required

✅ Yes

##### 1.8.3.5.4 Is Primary Key

❌ No

##### 1.8.3.5.5 Is Unique

❌ No

##### 1.8.3.5.6 Index Type

Index

##### 1.8.3.5.7 Size

50

##### 1.8.3.5.8 Constraints

- ENUM('PROCESSING', 'REVIEW_PENDING', 'FAILED', 'COMPLETED')

##### 1.8.3.5.9 Default Value

PROCESSING

##### 1.8.3.5.10 Is Foreign Key

❌ No

##### 1.8.3.5.11 Precision

0

##### 1.8.3.5.12 Scale

0

#### 1.8.3.6.0 processingErrorDetails

##### 1.8.3.6.1 Name

processingErrorDetails

##### 1.8.3.6.2 Type

🔹 TEXT

##### 1.8.3.6.3 Is Required

❌ No

##### 1.8.3.6.4 Is Primary Key

❌ No

##### 1.8.3.6.5 Is Unique

❌ No

##### 1.8.3.6.6 Index Type

None

##### 1.8.3.6.7 Size

0

##### 1.8.3.6.8 Constraints

*No items available*

##### 1.8.3.6.9 Default Value



##### 1.8.3.6.10 Is Foreign Key

❌ No

##### 1.8.3.6.11 Precision

0

##### 1.8.3.6.12 Scale

0

#### 1.8.3.7.0 uploadedById

##### 1.8.3.7.1 Name

uploadedById

##### 1.8.3.7.2 Type

🔹 Guid

##### 1.8.3.7.3 Is Required

✅ Yes

##### 1.8.3.7.4 Is Primary Key

❌ No

##### 1.8.3.7.5 Is Unique

❌ No

##### 1.8.3.7.6 Index Type

Index

##### 1.8.3.7.7 Size

0

##### 1.8.3.7.8 Constraints

*No items available*

##### 1.8.3.7.9 Default Value



##### 1.8.3.7.10 Is Foreign Key

✅ Yes

##### 1.8.3.7.11 Precision

0

##### 1.8.3.7.12 Scale

0

#### 1.8.3.8.0 createdAt

##### 1.8.3.8.1 Name

createdAt

##### 1.8.3.8.2 Type

🔹 DateTime

##### 1.8.3.8.3 Is Required

✅ Yes

##### 1.8.3.8.4 Is Primary Key

❌ No

##### 1.8.3.8.5 Is Unique

❌ No

##### 1.8.3.8.6 Index Type

Index

##### 1.8.3.8.7 Size

0

##### 1.8.3.8.8 Constraints

*No items available*

##### 1.8.3.8.9 Default Value

CURRENT_TIMESTAMP

##### 1.8.3.8.10 Is Foreign Key

❌ No

##### 1.8.3.8.11 Precision

0

##### 1.8.3.8.12 Scale

0

### 1.8.4.0.0 Primary Keys

- sowDocumentId

### 1.8.5.0.0 Unique Constraints

- {'name': 'UC_SowDocument_ProjectId', 'columns': ['projectId']}

### 1.8.6.0.0 Indexes

- {'name': 'IX_SowDocument_Status', 'columns': ['status'], 'type': 'BTree'}

## 1.9.0.0.0 ProjectBrief

### 1.9.1.0.0 Name

ProjectBrief

### 1.9.2.0.0 Description

Stores the AI-extracted and human-in-the-loop verified data from an SOW, forming the basis for vendor matching.

### 1.9.3.0.0 Attributes

#### 1.9.3.1.0 projectBriefId

##### 1.9.3.1.1 Name

projectBriefId

##### 1.9.3.1.2 Type

🔹 Guid

##### 1.9.3.1.3 Is Required

✅ Yes

##### 1.9.3.1.4 Is Primary Key

✅ Yes

##### 1.9.3.1.5 Is Unique

✅ Yes

##### 1.9.3.1.6 Index Type

UniqueIndex

##### 1.9.3.1.7 Size

0

##### 1.9.3.1.8 Constraints

*No items available*

##### 1.9.3.1.9 Default Value



##### 1.9.3.1.10 Is Foreign Key

❌ No

##### 1.9.3.1.11 Precision

0

##### 1.9.3.1.12 Scale

0

#### 1.9.3.2.0 projectId

##### 1.9.3.2.1 Name

projectId

##### 1.9.3.2.2 Type

🔹 Guid

##### 1.9.3.2.3 Is Required

✅ Yes

##### 1.9.3.2.4 Is Primary Key

❌ No

##### 1.9.3.2.5 Is Unique

✅ Yes

##### 1.9.3.2.6 Index Type

UniqueIndex

##### 1.9.3.2.7 Size

0

##### 1.9.3.2.8 Constraints

*No items available*

##### 1.9.3.2.9 Default Value



##### 1.9.3.2.10 Is Foreign Key

✅ Yes

##### 1.9.3.2.11 Precision

0

##### 1.9.3.2.12 Scale

0

#### 1.9.3.3.0 scopeSummary

##### 1.9.3.3.1 Name

scopeSummary

##### 1.9.3.3.2 Type

🔹 TEXT

##### 1.9.3.3.3 Is Required

❌ No

##### 1.9.3.3.4 Is Primary Key

❌ No

##### 1.9.3.3.5 Is Unique

❌ No

##### 1.9.3.3.6 Index Type

None

##### 1.9.3.3.7 Size

0

##### 1.9.3.3.8 Constraints

*No items available*

##### 1.9.3.3.9 Default Value



##### 1.9.3.3.10 Is Foreign Key

❌ No

##### 1.9.3.3.11 Precision

0

##### 1.9.3.3.12 Scale

0

#### 1.9.3.4.0 status

##### 1.9.3.4.1 Name

status

##### 1.9.3.4.2 Type

🔹 VARCHAR

##### 1.9.3.4.3 Is Required

✅ Yes

##### 1.9.3.4.4 Is Primary Key

❌ No

##### 1.9.3.4.5 Is Unique

❌ No

##### 1.9.3.4.6 Index Type

Index

##### 1.9.3.4.7 Size

50

##### 1.9.3.4.8 Constraints

- ENUM('PENDING_REVIEW', 'APPROVED')

##### 1.9.3.4.9 Default Value

PENDING_REVIEW

##### 1.9.3.4.10 Is Foreign Key

❌ No

##### 1.9.3.4.11 Precision

0

##### 1.9.3.4.12 Scale

0

#### 1.9.3.5.0 reviewedById

##### 1.9.3.5.1 Name

reviewedById

##### 1.9.3.5.2 Type

🔹 Guid

##### 1.9.3.5.3 Is Required

❌ No

##### 1.9.3.5.4 Is Primary Key

❌ No

##### 1.9.3.5.5 Is Unique

❌ No

##### 1.9.3.5.6 Index Type

Index

##### 1.9.3.5.7 Size

0

##### 1.9.3.5.8 Constraints

*No items available*

##### 1.9.3.5.9 Default Value



##### 1.9.3.5.10 Is Foreign Key

✅ Yes

##### 1.9.3.5.11 Precision

0

##### 1.9.3.5.12 Scale

0

#### 1.9.3.6.0 reviewedAt

##### 1.9.3.6.1 Name

reviewedAt

##### 1.9.3.6.2 Type

🔹 DateTime

##### 1.9.3.6.3 Is Required

❌ No

##### 1.9.3.6.4 Is Primary Key

❌ No

##### 1.9.3.6.5 Is Unique

❌ No

##### 1.9.3.6.6 Index Type

None

##### 1.9.3.6.7 Size

0

##### 1.9.3.6.8 Constraints

*No items available*

##### 1.9.3.6.9 Default Value



##### 1.9.3.6.10 Is Foreign Key

❌ No

##### 1.9.3.6.11 Precision

0

##### 1.9.3.6.12 Scale

0

### 1.9.4.0.0 Primary Keys

- projectBriefId

### 1.9.5.0.0 Unique Constraints

- {'name': 'UC_ProjectBrief_ProjectId', 'columns': ['projectId']}

### 1.9.6.0.0 Indexes

- {'name': 'IX_ProjectBrief_Status', 'columns': ['status'], 'type': 'BTree'}

## 1.10.0.0.0 ProjectBriefSkill

### 1.10.1.0.0 Name

ProjectBriefSkill

### 1.10.2.0.0 Description

Join table to create a many-to-many relationship between Project Briefs and required Skills.

### 1.10.3.0.0 Attributes

#### 1.10.3.1.0 projectBriefId

##### 1.10.3.1.1 Name

projectBriefId

##### 1.10.3.1.2 Type

🔹 Guid

##### 1.10.3.1.3 Is Required

✅ Yes

##### 1.10.3.1.4 Is Primary Key

✅ Yes

##### 1.10.3.1.5 Is Unique

❌ No

##### 1.10.3.1.6 Index Type

Index

##### 1.10.3.1.7 Size

0

##### 1.10.3.1.8 Constraints

*No items available*

##### 1.10.3.1.9 Default Value



##### 1.10.3.1.10 Is Foreign Key

✅ Yes

##### 1.10.3.1.11 Precision

0

##### 1.10.3.1.12 Scale

0

#### 1.10.3.2.0 skillId

##### 1.10.3.2.1 Name

skillId

##### 1.10.3.2.2 Type

🔹 Guid

##### 1.10.3.2.3 Is Required

✅ Yes

##### 1.10.3.2.4 Is Primary Key

✅ Yes

##### 1.10.3.2.5 Is Unique

❌ No

##### 1.10.3.2.6 Index Type

Index

##### 1.10.3.2.7 Size

0

##### 1.10.3.2.8 Constraints

*No items available*

##### 1.10.3.2.9 Default Value



##### 1.10.3.2.10 Is Foreign Key

✅ Yes

##### 1.10.3.2.11 Precision

0

##### 1.10.3.2.12 Scale

0

### 1.10.4.0.0 Primary Keys

- projectBriefId
- skillId

### 1.10.5.0.0 Unique Constraints

*No items available*

### 1.10.6.0.0 Indexes

- {'name': 'IX_ProjectBriefSkill_SkillId', 'columns': ['skillId'], 'type': 'BTree'}

## 1.11.0.0.0 Proposal

### 1.11.1.0.0 Name

Proposal

### 1.11.2.0.0 Description

Represents a vendor's proposal for a project, including cost, timeline, and status.

### 1.11.3.0.0 Attributes

#### 1.11.3.1.0 proposalId

##### 1.11.3.1.1 Name

proposalId

##### 1.11.3.1.2 Type

🔹 Guid

##### 1.11.3.1.3 Is Required

✅ Yes

##### 1.11.3.1.4 Is Primary Key

✅ Yes

##### 1.11.3.1.5 Is Unique

✅ Yes

##### 1.11.3.1.6 Index Type

UniqueIndex

##### 1.11.3.1.7 Size

0

##### 1.11.3.1.8 Constraints

*No items available*

##### 1.11.3.1.9 Default Value



##### 1.11.3.1.10 Is Foreign Key

❌ No

##### 1.11.3.1.11 Precision

0

##### 1.11.3.1.12 Scale

0

#### 1.11.3.2.0 projectId

##### 1.11.3.2.1 Name

projectId

##### 1.11.3.2.2 Type

🔹 Guid

##### 1.11.3.2.3 Is Required

✅ Yes

##### 1.11.3.2.4 Is Primary Key

❌ No

##### 1.11.3.2.5 Is Unique

❌ No

##### 1.11.3.2.6 Index Type

Index

##### 1.11.3.2.7 Size

0

##### 1.11.3.2.8 Constraints

*No items available*

##### 1.11.3.2.9 Default Value



##### 1.11.3.2.10 Is Foreign Key

✅ Yes

##### 1.11.3.2.11 Precision

0

##### 1.11.3.2.12 Scale

0

#### 1.11.3.3.0 vendorId

##### 1.11.3.3.1 Name

vendorId

##### 1.11.3.3.2 Type

🔹 Guid

##### 1.11.3.3.3 Is Required

✅ Yes

##### 1.11.3.3.4 Is Primary Key

❌ No

##### 1.11.3.3.5 Is Unique

❌ No

##### 1.11.3.3.6 Index Type

Index

##### 1.11.3.3.7 Size

0

##### 1.11.3.3.8 Constraints

*No items available*

##### 1.11.3.3.9 Default Value



##### 1.11.3.3.10 Is Foreign Key

✅ Yes

##### 1.11.3.3.11 Precision

0

##### 1.11.3.3.12 Scale

0

#### 1.11.3.4.0 cost

##### 1.11.3.4.1 Name

cost

##### 1.11.3.4.2 Type

🔹 DECIMAL

##### 1.11.3.4.3 Is Required

✅ Yes

##### 1.11.3.4.4 Is Primary Key

❌ No

##### 1.11.3.4.5 Is Unique

❌ No

##### 1.11.3.4.6 Index Type

None

##### 1.11.3.4.7 Size

0

##### 1.11.3.4.8 Constraints

*No items available*

##### 1.11.3.4.9 Default Value



##### 1.11.3.4.10 Is Foreign Key

❌ No

##### 1.11.3.4.11 Precision

12

##### 1.11.3.4.12 Scale

2

#### 1.11.3.5.0 timelineDescription

##### 1.11.3.5.1 Name

timelineDescription

##### 1.11.3.5.2 Type

🔹 TEXT

##### 1.11.3.5.3 Is Required

✅ Yes

##### 1.11.3.5.4 Is Primary Key

❌ No

##### 1.11.3.5.5 Is Unique

❌ No

##### 1.11.3.5.6 Index Type

None

##### 1.11.3.5.7 Size

0

##### 1.11.3.5.8 Constraints

*No items available*

##### 1.11.3.5.9 Default Value



##### 1.11.3.5.10 Is Foreign Key

❌ No

##### 1.11.3.5.11 Precision

0

##### 1.11.3.5.12 Scale

0

#### 1.11.3.6.0 status

##### 1.11.3.6.1 Name

status

##### 1.11.3.6.2 Type

🔹 VARCHAR

##### 1.11.3.6.3 Is Required

✅ Yes

##### 1.11.3.6.4 Is Primary Key

❌ No

##### 1.11.3.6.5 Is Unique

❌ No

##### 1.11.3.6.6 Index Type

Index

##### 1.11.3.6.7 Size

50

##### 1.11.3.6.8 Constraints

- ENUM('SUBMITTED', 'SHORTLISTED', 'REJECTED', 'AWARDED')

##### 1.11.3.6.9 Default Value

SUBMITTED

##### 1.11.3.6.10 Is Foreign Key

❌ No

##### 1.11.3.6.11 Precision

0

##### 1.11.3.6.12 Scale

0

#### 1.11.3.7.0 submittedAt

##### 1.11.3.7.1 Name

submittedAt

##### 1.11.3.7.2 Type

🔹 DateTime

##### 1.11.3.7.3 Is Required

✅ Yes

##### 1.11.3.7.4 Is Primary Key

❌ No

##### 1.11.3.7.5 Is Unique

❌ No

##### 1.11.3.7.6 Index Type

Index

##### 1.11.3.7.7 Size

0

##### 1.11.3.7.8 Constraints

*No items available*

##### 1.11.3.7.9 Default Value

CURRENT_TIMESTAMP

##### 1.11.3.7.10 Is Foreign Key

❌ No

##### 1.11.3.7.11 Precision

0

##### 1.11.3.7.12 Scale

0

### 1.11.4.0.0 Primary Keys

- proposalId

### 1.11.5.0.0 Unique Constraints

- {'name': 'UC_Proposal_Project_Vendor', 'columns': ['projectId', 'vendorId']}

### 1.11.6.0.0 Indexes

#### 1.11.6.1.0 IX_Proposal_ProjectId_Status

##### 1.11.6.1.1 Name

IX_Proposal_ProjectId_Status

##### 1.11.6.1.2 Columns

- projectId
- status

##### 1.11.6.1.3 Type

🔹 BTree

#### 1.11.6.2.0 IX_Proposal_Vendor_SubmittedAt

##### 1.11.6.2.1 Name

IX_Proposal_Vendor_SubmittedAt

##### 1.11.6.2.2 Columns

- vendorId
- submittedAt DESC

##### 1.11.6.2.3 Type

🔹 BTree

## 1.12.0.0.0 ProposalQuestion

### 1.12.1.0.0 Name

ProposalQuestion

### 1.12.2.0.0 Description

Stores questions submitted by vendors during the proposal phase and the corresponding answers from administrators.

### 1.12.3.0.0 Attributes

#### 1.12.3.1.0 proposalQuestionId

##### 1.12.3.1.1 Name

proposalQuestionId

##### 1.12.3.1.2 Type

🔹 Guid

##### 1.12.3.1.3 Is Required

✅ Yes

##### 1.12.3.1.4 Is Primary Key

✅ Yes

##### 1.12.3.1.5 Is Unique

✅ Yes

##### 1.12.3.1.6 Index Type

UniqueIndex

##### 1.12.3.1.7 Size

0

##### 1.12.3.1.8 Constraints

*No items available*

##### 1.12.3.1.9 Default Value



##### 1.12.3.1.10 Is Foreign Key

❌ No

##### 1.12.3.1.11 Precision

0

##### 1.12.3.1.12 Scale

0

#### 1.12.3.2.0 projectId

##### 1.12.3.2.1 Name

projectId

##### 1.12.3.2.2 Type

🔹 Guid

##### 1.12.3.2.3 Is Required

✅ Yes

##### 1.12.3.2.4 Is Primary Key

❌ No

##### 1.12.3.2.5 Is Unique

❌ No

##### 1.12.3.2.6 Index Type

Index

##### 1.12.3.2.7 Size

0

##### 1.12.3.2.8 Constraints

*No items available*

##### 1.12.3.2.9 Default Value



##### 1.12.3.2.10 Is Foreign Key

✅ Yes

##### 1.12.3.2.11 Precision

0

##### 1.12.3.2.12 Scale

0

#### 1.12.3.3.0 submittedByVendorId

##### 1.12.3.3.1 Name

submittedByVendorId

##### 1.12.3.3.2 Type

🔹 Guid

##### 1.12.3.3.3 Is Required

✅ Yes

##### 1.12.3.3.4 Is Primary Key

❌ No

##### 1.12.3.3.5 Is Unique

❌ No

##### 1.12.3.3.6 Index Type

Index

##### 1.12.3.3.7 Size

0

##### 1.12.3.3.8 Constraints

*No items available*

##### 1.12.3.3.9 Default Value



##### 1.12.3.3.10 Is Foreign Key

✅ Yes

##### 1.12.3.3.11 Precision

0

##### 1.12.3.3.12 Scale

0

#### 1.12.3.4.0 questionText

##### 1.12.3.4.1 Name

questionText

##### 1.12.3.4.2 Type

🔹 TEXT

##### 1.12.3.4.3 Is Required

✅ Yes

##### 1.12.3.4.4 Is Primary Key

❌ No

##### 1.12.3.4.5 Is Unique

❌ No

##### 1.12.3.4.6 Index Type

None

##### 1.12.3.4.7 Size

0

##### 1.12.3.4.8 Constraints

*No items available*

##### 1.12.3.4.9 Default Value



##### 1.12.3.4.10 Is Foreign Key

❌ No

##### 1.12.3.4.11 Precision

0

##### 1.12.3.4.12 Scale

0

#### 1.12.3.5.0 answerText

##### 1.12.3.5.1 Name

answerText

##### 1.12.3.5.2 Type

🔹 TEXT

##### 1.12.3.5.3 Is Required

❌ No

##### 1.12.3.5.4 Is Primary Key

❌ No

##### 1.12.3.5.5 Is Unique

❌ No

##### 1.12.3.5.6 Index Type

None

##### 1.12.3.5.7 Size

0

##### 1.12.3.5.8 Constraints

*No items available*

##### 1.12.3.5.9 Default Value



##### 1.12.3.5.10 Is Foreign Key

❌ No

##### 1.12.3.5.11 Precision

0

##### 1.12.3.5.12 Scale

0

#### 1.12.3.6.0 answeredById

##### 1.12.3.6.1 Name

answeredById

##### 1.12.3.6.2 Type

🔹 Guid

##### 1.12.3.6.3 Is Required

❌ No

##### 1.12.3.6.4 Is Primary Key

❌ No

##### 1.12.3.6.5 Is Unique

❌ No

##### 1.12.3.6.6 Index Type

Index

##### 1.12.3.6.7 Size

0

##### 1.12.3.6.8 Constraints

*No items available*

##### 1.12.3.6.9 Default Value



##### 1.12.3.6.10 Is Foreign Key

✅ Yes

##### 1.12.3.6.11 Precision

0

##### 1.12.3.6.12 Scale

0

#### 1.12.3.7.0 createdAt

##### 1.12.3.7.1 Name

createdAt

##### 1.12.3.7.2 Type

🔹 DateTime

##### 1.12.3.7.3 Is Required

✅ Yes

##### 1.12.3.7.4 Is Primary Key

❌ No

##### 1.12.3.7.5 Is Unique

❌ No

##### 1.12.3.7.6 Index Type

Index

##### 1.12.3.7.7 Size

0

##### 1.12.3.7.8 Constraints

*No items available*

##### 1.12.3.7.9 Default Value

CURRENT_TIMESTAMP

##### 1.12.3.7.10 Is Foreign Key

❌ No

##### 1.12.3.7.11 Precision

0

##### 1.12.3.7.12 Scale

0

#### 1.12.3.8.0 answeredAt

##### 1.12.3.8.1 Name

answeredAt

##### 1.12.3.8.2 Type

🔹 DateTime

##### 1.12.3.8.3 Is Required

❌ No

##### 1.12.3.8.4 Is Primary Key

❌ No

##### 1.12.3.8.5 Is Unique

❌ No

##### 1.12.3.8.6 Index Type

None

##### 1.12.3.8.7 Size

0

##### 1.12.3.8.8 Constraints

*No items available*

##### 1.12.3.8.9 Default Value



##### 1.12.3.8.10 Is Foreign Key

❌ No

##### 1.12.3.8.11 Precision

0

##### 1.12.3.8.12 Scale

0

### 1.12.4.0.0 Primary Keys

- proposalQuestionId

### 1.12.5.0.0 Unique Constraints

*No items available*

### 1.12.6.0.0 Indexes

- {'name': 'IX_ProposalQuestion_ProjectId', 'columns': ['projectId'], 'type': 'BTree'}

## 1.13.0.0.0 Invoice

### 1.13.1.0.0 Name

Invoice

### 1.13.2.0.0 Description

Represents a financial invoice sent to a client for a project.

### 1.13.3.0.0 Attributes

#### 1.13.3.1.0 invoiceId

##### 1.13.3.1.1 Name

invoiceId

##### 1.13.3.1.2 Type

🔹 Guid

##### 1.13.3.1.3 Is Required

✅ Yes

##### 1.13.3.1.4 Is Primary Key

✅ Yes

##### 1.13.3.1.5 Is Unique

✅ Yes

##### 1.13.3.1.6 Index Type

UniqueIndex

##### 1.13.3.1.7 Size

0

##### 1.13.3.1.8 Constraints

*No items available*

##### 1.13.3.1.9 Default Value



##### 1.13.3.1.10 Is Foreign Key

❌ No

##### 1.13.3.1.11 Precision

0

##### 1.13.3.1.12 Scale

0

#### 1.13.3.2.0 projectId

##### 1.13.3.2.1 Name

projectId

##### 1.13.3.2.2 Type

🔹 Guid

##### 1.13.3.2.3 Is Required

✅ Yes

##### 1.13.3.2.4 Is Primary Key

❌ No

##### 1.13.3.2.5 Is Unique

❌ No

##### 1.13.3.2.6 Index Type

Index

##### 1.13.3.2.7 Size

0

##### 1.13.3.2.8 Constraints

*No items available*

##### 1.13.3.2.9 Default Value



##### 1.13.3.2.10 Is Foreign Key

✅ Yes

##### 1.13.3.2.11 Precision

0

##### 1.13.3.2.12 Scale

0

#### 1.13.3.3.0 amount

##### 1.13.3.3.1 Name

amount

##### 1.13.3.3.2 Type

🔹 DECIMAL

##### 1.13.3.3.3 Is Required

✅ Yes

##### 1.13.3.3.4 Is Primary Key

❌ No

##### 1.13.3.3.5 Is Unique

❌ No

##### 1.13.3.3.6 Index Type

None

##### 1.13.3.3.7 Size

0

##### 1.13.3.3.8 Constraints

*No items available*

##### 1.13.3.3.9 Default Value



##### 1.13.3.3.10 Is Foreign Key

❌ No

##### 1.13.3.3.11 Precision

12

##### 1.13.3.3.12 Scale

2

#### 1.13.3.4.0 status

##### 1.13.3.4.1 Name

status

##### 1.13.3.4.2 Type

🔹 VARCHAR

##### 1.13.3.4.3 Is Required

✅ Yes

##### 1.13.3.4.4 Is Primary Key

❌ No

##### 1.13.3.4.5 Is Unique

❌ No

##### 1.13.3.4.6 Index Type

Index

##### 1.13.3.4.7 Size

50

##### 1.13.3.4.8 Constraints

- ENUM('DRAFT', 'SENT', 'PAID', 'OVERDUE')

##### 1.13.3.4.9 Default Value

DRAFT

##### 1.13.3.4.10 Is Foreign Key

❌ No

##### 1.13.3.4.11 Precision

0

##### 1.13.3.4.12 Scale

0

#### 1.13.3.5.0 issuedDate

##### 1.13.3.5.1 Name

issuedDate

##### 1.13.3.5.2 Type

🔹 DateTime

##### 1.13.3.5.3 Is Required

✅ Yes

##### 1.13.3.5.4 Is Primary Key

❌ No

##### 1.13.3.5.5 Is Unique

❌ No

##### 1.13.3.5.6 Index Type

Index

##### 1.13.3.5.7 Size

0

##### 1.13.3.5.8 Constraints

*No items available*

##### 1.13.3.5.9 Default Value



##### 1.13.3.5.10 Is Foreign Key

❌ No

##### 1.13.3.5.11 Precision

0

##### 1.13.3.5.12 Scale

0

#### 1.13.3.6.0 dueDate

##### 1.13.3.6.1 Name

dueDate

##### 1.13.3.6.2 Type

🔹 DateTime

##### 1.13.3.6.3 Is Required

✅ Yes

##### 1.13.3.6.4 Is Primary Key

❌ No

##### 1.13.3.6.5 Is Unique

❌ No

##### 1.13.3.6.6 Index Type

Index

##### 1.13.3.6.7 Size

0

##### 1.13.3.6.8 Constraints

*No items available*

##### 1.13.3.6.9 Default Value



##### 1.13.3.6.10 Is Foreign Key

❌ No

##### 1.13.3.6.11 Precision

0

##### 1.13.3.6.12 Scale

0

### 1.13.4.0.0 Primary Keys

- invoiceId

### 1.13.5.0.0 Unique Constraints

*No items available*

### 1.13.6.0.0 Indexes

#### 1.13.6.1.0 IX_Invoice_ProjectId

##### 1.13.6.1.1 Name

IX_Invoice_ProjectId

##### 1.13.6.1.2 Columns

- projectId

##### 1.13.6.1.3 Type

🔹 BTree

#### 1.13.6.2.0 IX_Invoice_Status

##### 1.13.6.2.1 Name

IX_Invoice_Status

##### 1.13.6.2.2 Columns

- status

##### 1.13.6.2.3 Type

🔹 BTree

#### 1.13.6.3.0 IX_Invoice_IssuedDate

##### 1.13.6.3.1 Name

IX_Invoice_IssuedDate

##### 1.13.6.3.2 Columns

- issuedDate

##### 1.13.6.3.3 Type

🔹 BTree

### 1.13.7.0.0 Partitioning

| Property | Value |
|----------|-------|
| Type | RANGE |
| Column | issuedDate |
| Strategy | Yearly |

## 1.14.0.0.0 Payout

### 1.14.1.0.0 Name

Payout

### 1.14.2.0.0 Description

Represents a financial payout made to a vendor for their work on a project.

### 1.14.3.0.0 Attributes

#### 1.14.3.1.0 payoutId

##### 1.14.3.1.1 Name

payoutId

##### 1.14.3.1.2 Type

🔹 Guid

##### 1.14.3.1.3 Is Required

✅ Yes

##### 1.14.3.1.4 Is Primary Key

✅ Yes

##### 1.14.3.1.5 Is Unique

✅ Yes

##### 1.14.3.1.6 Index Type

UniqueIndex

##### 1.14.3.1.7 Size

0

##### 1.14.3.1.8 Constraints

*No items available*

##### 1.14.3.1.9 Default Value



##### 1.14.3.1.10 Is Foreign Key

❌ No

##### 1.14.3.1.11 Precision

0

##### 1.14.3.1.12 Scale

0

#### 1.14.3.2.0 projectId

##### 1.14.3.2.1 Name

projectId

##### 1.14.3.2.2 Type

🔹 Guid

##### 1.14.3.2.3 Is Required

✅ Yes

##### 1.14.3.2.4 Is Primary Key

❌ No

##### 1.14.3.2.5 Is Unique

❌ No

##### 1.14.3.2.6 Index Type

Index

##### 1.14.3.2.7 Size

0

##### 1.14.3.2.8 Constraints

*No items available*

##### 1.14.3.2.9 Default Value



##### 1.14.3.2.10 Is Foreign Key

✅ Yes

##### 1.14.3.2.11 Precision

0

##### 1.14.3.2.12 Scale

0

#### 1.14.3.3.0 vendorId

##### 1.14.3.3.1 Name

vendorId

##### 1.14.3.3.2 Type

🔹 Guid

##### 1.14.3.3.3 Is Required

✅ Yes

##### 1.14.3.3.4 Is Primary Key

❌ No

##### 1.14.3.3.5 Is Unique

❌ No

##### 1.14.3.3.6 Index Type

Index

##### 1.14.3.3.7 Size

0

##### 1.14.3.3.8 Constraints

*No items available*

##### 1.14.3.3.9 Default Value



##### 1.14.3.3.10 Is Foreign Key

✅ Yes

##### 1.14.3.3.11 Precision

0

##### 1.14.3.3.12 Scale

0

#### 1.14.3.4.0 amount

##### 1.14.3.4.1 Name

amount

##### 1.14.3.4.2 Type

🔹 DECIMAL

##### 1.14.3.4.3 Is Required

✅ Yes

##### 1.14.3.4.4 Is Primary Key

❌ No

##### 1.14.3.4.5 Is Unique

❌ No

##### 1.14.3.4.6 Index Type

None

##### 1.14.3.4.7 Size

0

##### 1.14.3.4.8 Constraints

*No items available*

##### 1.14.3.4.9 Default Value



##### 1.14.3.4.10 Is Foreign Key

❌ No

##### 1.14.3.4.11 Precision

12

##### 1.14.3.4.12 Scale

2

#### 1.14.3.5.0 status

##### 1.14.3.5.1 Name

status

##### 1.14.3.5.2 Type

🔹 VARCHAR

##### 1.14.3.5.3 Is Required

✅ Yes

##### 1.14.3.5.4 Is Primary Key

❌ No

##### 1.14.3.5.5 Is Unique

❌ No

##### 1.14.3.5.6 Index Type

Index

##### 1.14.3.5.7 Size

50

##### 1.14.3.5.8 Constraints

- ENUM('PENDING', 'PAID', 'FAILED')

##### 1.14.3.5.9 Default Value

PENDING

##### 1.14.3.5.10 Is Foreign Key

❌ No

##### 1.14.3.5.11 Precision

0

##### 1.14.3.5.12 Scale

0

#### 1.14.3.6.0 paidDate

##### 1.14.3.6.1 Name

paidDate

##### 1.14.3.6.2 Type

🔹 DateTime

##### 1.14.3.6.3 Is Required

❌ No

##### 1.14.3.6.4 Is Primary Key

❌ No

##### 1.14.3.6.5 Is Unique

❌ No

##### 1.14.3.6.6 Index Type

Index

##### 1.14.3.6.7 Size

0

##### 1.14.3.6.8 Constraints

*No items available*

##### 1.14.3.6.9 Default Value



##### 1.14.3.6.10 Is Foreign Key

❌ No

##### 1.14.3.6.11 Precision

0

##### 1.14.3.6.12 Scale

0

### 1.14.4.0.0 Primary Keys

- payoutId

### 1.14.5.0.0 Unique Constraints

*No items available*

### 1.14.6.0.0 Indexes

#### 1.14.6.1.0 IX_Payout_ProjectId

##### 1.14.6.1.1 Name

IX_Payout_ProjectId

##### 1.14.6.1.2 Columns

- projectId

##### 1.14.6.1.3 Type

🔹 BTree

#### 1.14.6.2.0 IX_Payout_VendorId

##### 1.14.6.2.1 Name

IX_Payout_VendorId

##### 1.14.6.2.2 Columns

- vendorId

##### 1.14.6.2.3 Type

🔹 BTree

#### 1.14.6.3.0 IX_Payout_Status

##### 1.14.6.3.1 Name

IX_Payout_Status

##### 1.14.6.3.2 Columns

- status

##### 1.14.6.3.3 Type

🔹 BTree

## 1.15.0.0.0 ProjectPayoutRule

### 1.15.1.0.0 Name

ProjectPayoutRule

### 1.15.2.0.0 Description

Defines the specific payout rules and structures for a project (e.g., 50/50 on milestones).

### 1.15.3.0.0 Attributes

#### 1.15.3.1.0 projectPayoutRuleId

##### 1.15.3.1.1 Name

projectPayoutRuleId

##### 1.15.3.1.2 Type

🔹 Guid

##### 1.15.3.1.3 Is Required

✅ Yes

##### 1.15.3.1.4 Is Primary Key

✅ Yes

##### 1.15.3.1.5 Is Unique

✅ Yes

##### 1.15.3.1.6 Index Type

UniqueIndex

##### 1.15.3.1.7 Size

0

##### 1.15.3.1.8 Constraints

*No items available*

##### 1.15.3.1.9 Default Value



##### 1.15.3.1.10 Is Foreign Key

❌ No

##### 1.15.3.1.11 Precision

0

##### 1.15.3.1.12 Scale

0

#### 1.15.3.2.0 projectId

##### 1.15.3.2.1 Name

projectId

##### 1.15.3.2.2 Type

🔹 Guid

##### 1.15.3.2.3 Is Required

✅ Yes

##### 1.15.3.2.4 Is Primary Key

❌ No

##### 1.15.3.2.5 Is Unique

❌ No

##### 1.15.3.2.6 Index Type

Index

##### 1.15.3.2.7 Size

0

##### 1.15.3.2.8 Constraints

*No items available*

##### 1.15.3.2.9 Default Value



##### 1.15.3.2.10 Is Foreign Key

✅ Yes

##### 1.15.3.2.11 Precision

0

##### 1.15.3.2.12 Scale

0

#### 1.15.3.3.0 description

##### 1.15.3.3.1 Name

description

##### 1.15.3.3.2 Type

🔹 VARCHAR

##### 1.15.3.3.3 Is Required

✅ Yes

##### 1.15.3.3.4 Is Primary Key

❌ No

##### 1.15.3.3.5 Is Unique

❌ No

##### 1.15.3.3.6 Index Type

None

##### 1.15.3.3.7 Size

255

##### 1.15.3.3.8 Constraints

*No items available*

##### 1.15.3.3.9 Default Value



##### 1.15.3.3.10 Is Foreign Key

❌ No

##### 1.15.3.3.11 Precision

0

##### 1.15.3.3.12 Scale

0

#### 1.15.3.4.0 payoutPercentage

##### 1.15.3.4.1 Name

payoutPercentage

##### 1.15.3.4.2 Type

🔹 DECIMAL

##### 1.15.3.4.3 Is Required

❌ No

##### 1.15.3.4.4 Is Primary Key

❌ No

##### 1.15.3.4.5 Is Unique

❌ No

##### 1.15.3.4.6 Index Type

None

##### 1.15.3.4.7 Size

0

##### 1.15.3.4.8 Constraints

*No items available*

##### 1.15.3.4.9 Default Value



##### 1.15.3.4.10 Is Foreign Key

❌ No

##### 1.15.3.4.11 Precision

5

##### 1.15.3.4.12 Scale

2

### 1.15.4.0.0 Primary Keys

- projectPayoutRuleId

### 1.15.5.0.0 Unique Constraints

*No items available*

### 1.15.6.0.0 Indexes

- {'name': 'IX_ProjectPayoutRule_ProjectId', 'columns': ['projectId'], 'type': 'BTree'}

## 1.16.0.0.0 AuditLog

### 1.16.1.0.0 Name

AuditLog

### 1.16.2.0.0 Description

Logs significant changes made to key entities for security and traceability.

### 1.16.3.0.0 Attributes

#### 1.16.3.1.0 auditLogId

##### 1.16.3.1.1 Name

auditLogId

##### 1.16.3.1.2 Type

🔹 Guid

##### 1.16.3.1.3 Is Required

✅ Yes

##### 1.16.3.1.4 Is Primary Key

✅ Yes

##### 1.16.3.1.5 Is Unique

✅ Yes

##### 1.16.3.1.6 Index Type

UniqueIndex

##### 1.16.3.1.7 Size

0

##### 1.16.3.1.8 Constraints

*No items available*

##### 1.16.3.1.9 Default Value



##### 1.16.3.1.10 Is Foreign Key

❌ No

##### 1.16.3.1.11 Precision

0

##### 1.16.3.1.12 Scale

0

#### 1.16.3.2.0 actorUserId

##### 1.16.3.2.1 Name

actorUserId

##### 1.16.3.2.2 Type

🔹 Guid

##### 1.16.3.2.3 Is Required

✅ Yes

##### 1.16.3.2.4 Is Primary Key

❌ No

##### 1.16.3.2.5 Is Unique

❌ No

##### 1.16.3.2.6 Index Type

Index

##### 1.16.3.2.7 Size

0

##### 1.16.3.2.8 Constraints

*No items available*

##### 1.16.3.2.9 Default Value



##### 1.16.3.2.10 Is Foreign Key

✅ Yes

##### 1.16.3.2.11 Precision

0

##### 1.16.3.2.12 Scale

0

#### 1.16.3.3.0 actionType

##### 1.16.3.3.1 Name

actionType

##### 1.16.3.3.2 Type

🔹 VARCHAR

##### 1.16.3.3.3 Is Required

✅ Yes

##### 1.16.3.3.4 Is Primary Key

❌ No

##### 1.16.3.3.5 Is Unique

❌ No

##### 1.16.3.3.6 Index Type

Index

##### 1.16.3.3.7 Size

50

##### 1.16.3.3.8 Constraints

- ENUM('CREATE', 'UPDATE', 'DELETE', 'OVERRIDE')

##### 1.16.3.3.9 Default Value



##### 1.16.3.3.10 Is Foreign Key

❌ No

##### 1.16.3.3.11 Precision

0

##### 1.16.3.3.12 Scale

0

#### 1.16.3.4.0 entityName

##### 1.16.3.4.1 Name

entityName

##### 1.16.3.4.2 Type

🔹 VARCHAR

##### 1.16.3.4.3 Is Required

✅ Yes

##### 1.16.3.4.4 Is Primary Key

❌ No

##### 1.16.3.4.5 Is Unique

❌ No

##### 1.16.3.4.6 Index Type

Index

##### 1.16.3.4.7 Size

100

##### 1.16.3.4.8 Constraints

*No items available*

##### 1.16.3.4.9 Default Value



##### 1.16.3.4.10 Is Foreign Key

❌ No

##### 1.16.3.4.11 Precision

0

##### 1.16.3.4.12 Scale

0

#### 1.16.3.5.0 entityId

##### 1.16.3.5.1 Name

entityId

##### 1.16.3.5.2 Type

🔹 VARCHAR

##### 1.16.3.5.3 Is Required

✅ Yes

##### 1.16.3.5.4 Is Primary Key

❌ No

##### 1.16.3.5.5 Is Unique

❌ No

##### 1.16.3.5.6 Index Type

Index

##### 1.16.3.5.7 Size

36

##### 1.16.3.5.8 Constraints

*No items available*

##### 1.16.3.5.9 Default Value



##### 1.16.3.5.10 Is Foreign Key

❌ No

##### 1.16.3.5.11 Precision

0

##### 1.16.3.5.12 Scale

0

#### 1.16.3.6.0 changes

##### 1.16.3.6.1 Name

changes

##### 1.16.3.6.2 Type

🔹 JSON

##### 1.16.3.6.3 Is Required

✅ Yes

##### 1.16.3.6.4 Is Primary Key

❌ No

##### 1.16.3.6.5 Is Unique

❌ No

##### 1.16.3.6.6 Index Type

None

##### 1.16.3.6.7 Size

0

##### 1.16.3.6.8 Constraints

*No items available*

##### 1.16.3.6.9 Default Value



##### 1.16.3.6.10 Is Foreign Key

❌ No

##### 1.16.3.6.11 Precision

0

##### 1.16.3.6.12 Scale

0

#### 1.16.3.7.0 createdAt

##### 1.16.3.7.1 Name

createdAt

##### 1.16.3.7.2 Type

🔹 DateTime

##### 1.16.3.7.3 Is Required

✅ Yes

##### 1.16.3.7.4 Is Primary Key

❌ No

##### 1.16.3.7.5 Is Unique

❌ No

##### 1.16.3.7.6 Index Type

Index

##### 1.16.3.7.7 Size

0

##### 1.16.3.7.8 Constraints

*No items available*

##### 1.16.3.7.9 Default Value

CURRENT_TIMESTAMP

##### 1.16.3.7.10 Is Foreign Key

❌ No

##### 1.16.3.7.11 Precision

0

##### 1.16.3.7.12 Scale

0

### 1.16.4.0.0 Primary Keys

- auditLogId

### 1.16.5.0.0 Unique Constraints

*No items available*

### 1.16.6.0.0 Indexes

#### 1.16.6.1.0 IX_AuditLog_Entity

##### 1.16.6.1.1 Name

IX_AuditLog_Entity

##### 1.16.6.1.2 Columns

- entityName
- entityId

##### 1.16.6.1.3 Type

🔹 BTree

#### 1.16.6.2.0 IX_AuditLog_Actor

##### 1.16.6.2.1 Name

IX_AuditLog_Actor

##### 1.16.6.2.2 Columns

- actorUserId

##### 1.16.6.2.3 Type

🔹 BTree

#### 1.16.6.3.0 IX_AuditLog_CreatedAt_EntityName

##### 1.16.6.3.1 Name

IX_AuditLog_CreatedAt_EntityName

##### 1.16.6.3.2 Columns

- createdAt DESC
- entityName

##### 1.16.6.3.3 Type

🔹 BTree

### 1.16.7.0.0 Partitioning

| Property | Value |
|----------|-------|
| Type | RANGE |
| Column | createdAt |
| Strategy | Monthly |

## 1.17.0.0.0 SystemSetting

### 1.17.1.0.0 Name

SystemSetting

### 1.17.2.0.0 Description

Stores global system configuration values, such as default margin fees.

### 1.17.3.0.0 Attributes

#### 1.17.3.1.0 settingKey

##### 1.17.3.1.1 Name

settingKey

##### 1.17.3.1.2 Type

🔹 VARCHAR

##### 1.17.3.1.3 Is Required

✅ Yes

##### 1.17.3.1.4 Is Primary Key

✅ Yes

##### 1.17.3.1.5 Is Unique

✅ Yes

##### 1.17.3.1.6 Index Type

UniqueIndex

##### 1.17.3.1.7 Size

100

##### 1.17.3.1.8 Constraints

*No items available*

##### 1.17.3.1.9 Default Value



##### 1.17.3.1.10 Is Foreign Key

❌ No

##### 1.17.3.1.11 Precision

0

##### 1.17.3.1.12 Scale

0

#### 1.17.3.2.0 settingValue

##### 1.17.3.2.1 Name

settingValue

##### 1.17.3.2.2 Type

🔹 JSON

##### 1.17.3.2.3 Is Required

✅ Yes

##### 1.17.3.2.4 Is Primary Key

❌ No

##### 1.17.3.2.5 Is Unique

❌ No

##### 1.17.3.2.6 Index Type

None

##### 1.17.3.2.7 Size

0

##### 1.17.3.2.8 Constraints

*No items available*

##### 1.17.3.2.9 Default Value



##### 1.17.3.2.10 Is Foreign Key

❌ No

##### 1.17.3.2.11 Precision

0

##### 1.17.3.2.12 Scale

0

#### 1.17.3.3.0 description

##### 1.17.3.3.1 Name

description

##### 1.17.3.3.2 Type

🔹 TEXT

##### 1.17.3.3.3 Is Required

❌ No

##### 1.17.3.3.4 Is Primary Key

❌ No

##### 1.17.3.3.5 Is Unique

❌ No

##### 1.17.3.3.6 Index Type

None

##### 1.17.3.3.7 Size

0

##### 1.17.3.3.8 Constraints

*No items available*

##### 1.17.3.3.9 Default Value



##### 1.17.3.3.10 Is Foreign Key

❌ No

##### 1.17.3.3.11 Precision

0

##### 1.17.3.3.12 Scale

0

### 1.17.4.0.0 Primary Keys

- settingKey

### 1.17.5.0.0 Unique Constraints

*No items available*

### 1.17.6.0.0 Indexes

*No items available*

## 1.18.0.0.0 Notification

### 1.18.1.0.0 Name

Notification

### 1.18.2.0.0 Description

Stores notification events for users, supporting in-app and email channels.

### 1.18.3.0.0 Attributes

#### 1.18.3.1.0 notificationId

##### 1.18.3.1.1 Name

notificationId

##### 1.18.3.1.2 Type

🔹 Guid

##### 1.18.3.1.3 Is Required

✅ Yes

##### 1.18.3.1.4 Is Primary Key

✅ Yes

##### 1.18.3.1.5 Is Unique

✅ Yes

##### 1.18.3.1.6 Index Type

UniqueIndex

##### 1.18.3.1.7 Size

0

##### 1.18.3.1.8 Constraints

*No items available*

##### 1.18.3.1.9 Default Value



##### 1.18.3.1.10 Is Foreign Key

❌ No

##### 1.18.3.1.11 Precision

0

##### 1.18.3.1.12 Scale

0

#### 1.18.3.2.0 recipientUserId

##### 1.18.3.2.1 Name

recipientUserId

##### 1.18.3.2.2 Type

🔹 Guid

##### 1.18.3.2.3 Is Required

✅ Yes

##### 1.18.3.2.4 Is Primary Key

❌ No

##### 1.18.3.2.5 Is Unique

❌ No

##### 1.18.3.2.6 Index Type

Index

##### 1.18.3.2.7 Size

0

##### 1.18.3.2.8 Constraints

*No items available*

##### 1.18.3.2.9 Default Value



##### 1.18.3.2.10 Is Foreign Key

✅ Yes

##### 1.18.3.2.11 Precision

0

##### 1.18.3.2.12 Scale

0

#### 1.18.3.3.0 eventType

##### 1.18.3.3.1 Name

eventType

##### 1.18.3.3.2 Type

🔹 VARCHAR

##### 1.18.3.3.3 Is Required

✅ Yes

##### 1.18.3.3.4 Is Primary Key

❌ No

##### 1.18.3.3.5 Is Unique

❌ No

##### 1.18.3.3.6 Index Type

Index

##### 1.18.3.3.7 Size

100

##### 1.18.3.3.8 Constraints

*No items available*

##### 1.18.3.3.9 Default Value



##### 1.18.3.3.10 Is Foreign Key

❌ No

##### 1.18.3.3.11 Precision

0

##### 1.18.3.3.12 Scale

0

#### 1.18.3.4.0 content

##### 1.18.3.4.1 Name

content

##### 1.18.3.4.2 Type

🔹 TEXT

##### 1.18.3.4.3 Is Required

✅ Yes

##### 1.18.3.4.4 Is Primary Key

❌ No

##### 1.18.3.4.5 Is Unique

❌ No

##### 1.18.3.4.6 Index Type

None

##### 1.18.3.4.7 Size

0

##### 1.18.3.4.8 Constraints

*No items available*

##### 1.18.3.4.9 Default Value



##### 1.18.3.4.10 Is Foreign Key

❌ No

##### 1.18.3.4.11 Precision

0

##### 1.18.3.4.12 Scale

0

#### 1.18.3.5.0 relatedEntityName

##### 1.18.3.5.1 Name

relatedEntityName

##### 1.18.3.5.2 Type

🔹 VARCHAR

##### 1.18.3.5.3 Is Required

❌ No

##### 1.18.3.5.4 Is Primary Key

❌ No

##### 1.18.3.5.5 Is Unique

❌ No

##### 1.18.3.5.6 Index Type

Index

##### 1.18.3.5.7 Size

100

##### 1.18.3.5.8 Constraints

*No items available*

##### 1.18.3.5.9 Default Value



##### 1.18.3.5.10 Is Foreign Key

❌ No

##### 1.18.3.5.11 Precision

0

##### 1.18.3.5.12 Scale

0

#### 1.18.3.6.0 relatedEntityId

##### 1.18.3.6.1 Name

relatedEntityId

##### 1.18.3.6.2 Type

🔹 Guid

##### 1.18.3.6.3 Is Required

❌ No

##### 1.18.3.6.4 Is Primary Key

❌ No

##### 1.18.3.6.5 Is Unique

❌ No

##### 1.18.3.6.6 Index Type

Index

##### 1.18.3.6.7 Size

0

##### 1.18.3.6.8 Constraints

*No items available*

##### 1.18.3.6.9 Default Value



##### 1.18.3.6.10 Is Foreign Key

❌ No

##### 1.18.3.6.11 Precision

0

##### 1.18.3.6.12 Scale

0

#### 1.18.3.7.0 isRead

##### 1.18.3.7.1 Name

isRead

##### 1.18.3.7.2 Type

🔹 BOOLEAN

##### 1.18.3.7.3 Is Required

✅ Yes

##### 1.18.3.7.4 Is Primary Key

❌ No

##### 1.18.3.7.5 Is Unique

❌ No

##### 1.18.3.7.6 Index Type

Index

##### 1.18.3.7.7 Size

0

##### 1.18.3.7.8 Constraints

*No items available*

##### 1.18.3.7.9 Default Value

false

##### 1.18.3.7.10 Is Foreign Key

❌ No

##### 1.18.3.7.11 Precision

0

##### 1.18.3.7.12 Scale

0

#### 1.18.3.8.0 readAt

##### 1.18.3.8.1 Name

readAt

##### 1.18.3.8.2 Type

🔹 DateTime

##### 1.18.3.8.3 Is Required

❌ No

##### 1.18.3.8.4 Is Primary Key

❌ No

##### 1.18.3.8.5 Is Unique

❌ No

##### 1.18.3.8.6 Index Type

None

##### 1.18.3.8.7 Size

0

##### 1.18.3.8.8 Constraints

*No items available*

##### 1.18.3.8.9 Default Value



##### 1.18.3.8.10 Is Foreign Key

❌ No

##### 1.18.3.8.11 Precision

0

##### 1.18.3.8.12 Scale

0

#### 1.18.3.9.0 createdAt

##### 1.18.3.9.1 Name

createdAt

##### 1.18.3.9.2 Type

🔹 DateTime

##### 1.18.3.9.3 Is Required

✅ Yes

##### 1.18.3.9.4 Is Primary Key

❌ No

##### 1.18.3.9.5 Is Unique

❌ No

##### 1.18.3.9.6 Index Type

Index

##### 1.18.3.9.7 Size

0

##### 1.18.3.9.8 Constraints

*No items available*

##### 1.18.3.9.9 Default Value

CURRENT_TIMESTAMP

##### 1.18.3.9.10 Is Foreign Key

❌ No

##### 1.18.3.9.11 Precision

0

##### 1.18.3.9.12 Scale

0

### 1.18.4.0.0 Primary Keys

- notificationId

### 1.18.5.0.0 Unique Constraints

*No items available*

### 1.18.6.0.0 Indexes

#### 1.18.6.1.0 IX_Notification_Recipient_Unread

##### 1.18.6.1.1 Name

IX_Notification_Recipient_Unread

##### 1.18.6.1.2 Columns

- recipientUserId
- isRead

##### 1.18.6.1.3 Type

🔹 BTree

#### 1.18.6.2.0 IX_Notification_Recipient_CreatedAt

##### 1.18.6.2.1 Name

IX_Notification_Recipient_CreatedAt

##### 1.18.6.2.2 Columns

- recipientUserId
- createdAt DESC

##### 1.18.6.2.3 Type

🔹 BTree

## 1.19.0.0.0 WebhookEndpoint

### 1.19.1.0.0 Name

WebhookEndpoint

### 1.19.2.0.0 Description

Stores configured webhook endpoints for system integrations like Slack.

### 1.19.3.0.0 Attributes

#### 1.19.3.1.0 webhookEndpointId

##### 1.19.3.1.1 Name

webhookEndpointId

##### 1.19.3.1.2 Type

🔹 Guid

##### 1.19.3.1.3 Is Required

✅ Yes

##### 1.19.3.1.4 Is Primary Key

✅ Yes

##### 1.19.3.1.5 Is Unique

✅ Yes

##### 1.19.3.1.6 Index Type

UniqueIndex

##### 1.19.3.1.7 Size

0

##### 1.19.3.1.8 Constraints

*No items available*

##### 1.19.3.1.9 Default Value



##### 1.19.3.1.10 Is Foreign Key

❌ No

##### 1.19.3.1.11 Precision

0

##### 1.19.3.1.12 Scale

0

#### 1.19.3.2.0 url

##### 1.19.3.2.1 Name

url

##### 1.19.3.2.2 Type

🔹 VARCHAR

##### 1.19.3.2.3 Is Required

✅ Yes

##### 1.19.3.2.4 Is Primary Key

❌ No

##### 1.19.3.2.5 Is Unique

✅ Yes

##### 1.19.3.2.6 Index Type

UniqueIndex

##### 1.19.3.2.7 Size

2,048

##### 1.19.3.2.8 Constraints

*No items available*

##### 1.19.3.2.9 Default Value



##### 1.19.3.2.10 Is Foreign Key

❌ No

##### 1.19.3.2.11 Precision

0

##### 1.19.3.2.12 Scale

0

#### 1.19.3.3.0 description

##### 1.19.3.3.1 Name

description

##### 1.19.3.3.2 Type

🔹 TEXT

##### 1.19.3.3.3 Is Required

❌ No

##### 1.19.3.3.4 Is Primary Key

❌ No

##### 1.19.3.3.5 Is Unique

❌ No

##### 1.19.3.3.6 Index Type

None

##### 1.19.3.3.7 Size

0

##### 1.19.3.3.8 Constraints

*No items available*

##### 1.19.3.3.9 Default Value



##### 1.19.3.3.10 Is Foreign Key

❌ No

##### 1.19.3.3.11 Precision

0

##### 1.19.3.3.12 Scale

0

#### 1.19.3.4.0 secretToken

##### 1.19.3.4.1 Name

secretToken

##### 1.19.3.4.2 Type

🔹 VARCHAR

##### 1.19.3.4.3 Is Required

❌ No

##### 1.19.3.4.4 Is Primary Key

❌ No

##### 1.19.3.4.5 Is Unique

❌ No

##### 1.19.3.4.6 Index Type

None

##### 1.19.3.4.7 Size

255

##### 1.19.3.4.8 Constraints

*No items available*

##### 1.19.3.4.9 Default Value



##### 1.19.3.4.10 Is Foreign Key

❌ No

##### 1.19.3.4.11 Precision

0

##### 1.19.3.4.12 Scale

0

#### 1.19.3.5.0 isActive

##### 1.19.3.5.1 Name

isActive

##### 1.19.3.5.2 Type

🔹 BOOLEAN

##### 1.19.3.5.3 Is Required

✅ Yes

##### 1.19.3.5.4 Is Primary Key

❌ No

##### 1.19.3.5.5 Is Unique

❌ No

##### 1.19.3.5.6 Index Type

Index

##### 1.19.3.5.7 Size

0

##### 1.19.3.5.8 Constraints

*No items available*

##### 1.19.3.5.9 Default Value

true

##### 1.19.3.5.10 Is Foreign Key

❌ No

##### 1.19.3.5.11 Precision

0

##### 1.19.3.5.12 Scale

0

#### 1.19.3.6.0 createdAt

##### 1.19.3.6.1 Name

createdAt

##### 1.19.3.6.2 Type

🔹 DateTime

##### 1.19.3.6.3 Is Required

✅ Yes

##### 1.19.3.6.4 Is Primary Key

❌ No

##### 1.19.3.6.5 Is Unique

❌ No

##### 1.19.3.6.6 Index Type

Index

##### 1.19.3.6.7 Size

0

##### 1.19.3.6.8 Constraints

*No items available*

##### 1.19.3.6.9 Default Value

CURRENT_TIMESTAMP

##### 1.19.3.6.10 Is Foreign Key

❌ No

##### 1.19.3.6.11 Precision

0

##### 1.19.3.6.12 Scale

0

#### 1.19.3.7.0 updatedAt

##### 1.19.3.7.1 Name

updatedAt

##### 1.19.3.7.2 Type

🔹 DateTime

##### 1.19.3.7.3 Is Required

✅ Yes

##### 1.19.3.7.4 Is Primary Key

❌ No

##### 1.19.3.7.5 Is Unique

❌ No

##### 1.19.3.7.6 Index Type

None

##### 1.19.3.7.7 Size

0

##### 1.19.3.7.8 Constraints

*No items available*

##### 1.19.3.7.9 Default Value

CURRENT_TIMESTAMP

##### 1.19.3.7.10 Is Foreign Key

❌ No

##### 1.19.3.7.11 Precision

0

##### 1.19.3.7.12 Scale

0

### 1.19.4.0.0 Primary Keys

- webhookEndpointId

### 1.19.5.0.0 Unique Constraints

- {'name': 'UC_WebhookEndpoint_Url', 'columns': ['url']}

### 1.19.6.0.0 Indexes

- {'name': 'IX_WebhookEndpoint_IsActive', 'columns': ['isActive'], 'type': 'BTree'}

## 1.20.0.0.0 DashboardMetrics

### 1.20.1.0.0 Name

DashboardMetrics

### 1.20.2.0.0 Description

Stores pre-aggregated, denormalized data for the main dashboard to ensure fast load times. This table is populated by a background job or triggers.

### 1.20.3.0.0 Attributes

#### 1.20.3.1.0 metricKey

##### 1.20.3.1.1 Name

metricKey

##### 1.20.3.1.2 Type

🔹 VARCHAR

##### 1.20.3.1.3 Is Required

✅ Yes

##### 1.20.3.1.4 Is Primary Key

✅ Yes

##### 1.20.3.1.5 Is Unique

✅ Yes

##### 1.20.3.1.6 Size

50

##### 1.20.3.1.7 Constraints

*No items available*

##### 1.20.3.1.8 Default Value

'main'

##### 1.20.3.1.9 Is Foreign Key

❌ No

##### 1.20.3.1.10 Precision

0

##### 1.20.3.1.11 Scale

0

#### 1.20.3.2.0 projectsByStatusCount

##### 1.20.3.2.1 Name

projectsByStatusCount

##### 1.20.3.2.2 Type

🔹 JSON

##### 1.20.3.2.3 Is Required

❌ No

##### 1.20.3.2.4 Is Primary Key

❌ No

##### 1.20.3.2.5 Is Unique

❌ No

##### 1.20.3.2.6 Index Type

None

##### 1.20.3.2.7 Size

0

##### 1.20.3.2.8 Constraints

*No items available*

##### 1.20.3.2.9 Default Value



##### 1.20.3.2.10 Is Foreign Key

❌ No

##### 1.20.3.2.11 Precision

0

##### 1.20.3.2.12 Scale

0

#### 1.20.3.3.0 pendingActionsCount

##### 1.20.3.3.1 Name

pendingActionsCount

##### 1.20.3.3.2 Type

🔹 JSON

##### 1.20.3.3.3 Is Required

❌ No

##### 1.20.3.3.4 Is Primary Key

❌ No

##### 1.20.3.3.5 Is Unique

❌ No

##### 1.20.3.3.6 Index Type

None

##### 1.20.3.3.7 Size

0

##### 1.20.3.3.8 Constraints

*No items available*

##### 1.20.3.3.9 Default Value



##### 1.20.3.3.10 Is Foreign Key

❌ No

##### 1.20.3.3.11 Precision

0

##### 1.20.3.3.12 Scale

0

#### 1.20.3.4.0 financialMetrics

##### 1.20.3.4.1 Name

financialMetrics

##### 1.20.3.4.2 Type

🔹 JSON

##### 1.20.3.4.3 Is Required

❌ No

##### 1.20.3.4.4 Is Primary Key

❌ No

##### 1.20.3.4.5 Is Unique

❌ No

##### 1.20.3.4.6 Index Type

None

##### 1.20.3.4.7 Size

0

##### 1.20.3.4.8 Constraints

*No items available*

##### 1.20.3.4.9 Default Value



##### 1.20.3.4.10 Is Foreign Key

❌ No

##### 1.20.3.4.11 Precision

0

##### 1.20.3.4.12 Scale

0

#### 1.20.3.5.0 upcomingMilestones

##### 1.20.3.5.1 Name

upcomingMilestones

##### 1.20.3.5.2 Type

🔹 JSON

##### 1.20.3.5.3 Is Required

❌ No

##### 1.20.3.5.4 Is Primary Key

❌ No

##### 1.20.3.5.5 Is Unique

❌ No

##### 1.20.3.5.6 Index Type

None

##### 1.20.3.5.7 Size

0

##### 1.20.3.5.8 Constraints

*No items available*

##### 1.20.3.5.9 Default Value



##### 1.20.3.5.10 Is Foreign Key

❌ No

##### 1.20.3.5.11 Precision

0

##### 1.20.3.5.12 Scale

0

#### 1.20.3.6.0 lastUpdatedAt

##### 1.20.3.6.1 Name

lastUpdatedAt

##### 1.20.3.6.2 Type

🔹 DateTime

##### 1.20.3.6.3 Is Required

✅ Yes

##### 1.20.3.6.4 Is Primary Key

❌ No

##### 1.20.3.6.5 Is Unique

❌ No

##### 1.20.3.6.6 Index Type

None

##### 1.20.3.6.7 Size

0

##### 1.20.3.6.8 Constraints

*No items available*

##### 1.20.3.6.9 Default Value

CURRENT_TIMESTAMP

##### 1.20.3.6.10 Is Foreign Key

❌ No

##### 1.20.3.6.11 Precision

0

##### 1.20.3.6.12 Scale

0

### 1.20.4.0.0 Primary Keys

- metricKey

### 1.20.5.0.0 Unique Constraints

*No items available*

### 1.20.6.0.0 Indexes

*No items available*

# 2.0.0.0.0 Relations

## 2.1.0.0.0 RoleUsers

### 2.1.1.0.0 Name

RoleUsers

### 2.1.2.0.0 Id

REL_ROLE_USER_001

### 2.1.3.0.0 Source Entity

Role

### 2.1.4.0.0 Target Entity

User

### 2.1.5.0.0 Type

🔹 OneToMany

### 2.1.6.0.0 Source Multiplicity

1

### 2.1.7.0.0 Target Multiplicity

0..*

### 2.1.8.0.0 Cascade Delete

❌ No

### 2.1.9.0.0 Is Identifying

❌ No

### 2.1.10.0.0 On Delete

Restrict

### 2.1.11.0.0 On Update

Cascade

## 2.2.0.0.0 ClientUsers

### 2.2.1.0.0 Name

ClientUsers

### 2.2.2.0.0 Id

REL_CLIENT_USER_001

### 2.2.3.0.0 Source Entity

Client

### 2.2.4.0.0 Target Entity

User

### 2.2.5.0.0 Type

🔹 OneToMany

### 2.2.6.0.0 Source Multiplicity

1

### 2.2.7.0.0 Target Multiplicity

0..*

### 2.2.8.0.0 Cascade Delete

❌ No

### 2.2.9.0.0 Is Identifying

❌ No

### 2.2.10.0.0 On Delete

SetNull

### 2.2.11.0.0 On Update

Cascade

## 2.3.0.0.0 VendorUsers

### 2.3.1.0.0 Name

VendorUsers

### 2.3.2.0.0 Id

REL_VENDOR_USER_001

### 2.3.3.0.0 Source Entity

Vendor

### 2.3.4.0.0 Target Entity

User

### 2.3.5.0.0 Type

🔹 OneToMany

### 2.3.6.0.0 Source Multiplicity

1

### 2.3.7.0.0 Target Multiplicity

0..*

### 2.3.8.0.0 Cascade Delete

❌ No

### 2.3.9.0.0 Is Identifying

❌ No

### 2.3.10.0.0 On Delete

SetNull

### 2.3.11.0.0 On Update

Cascade

## 2.4.0.0.0 ClientProjects

### 2.4.1.0.0 Name

ClientProjects

### 2.4.2.0.0 Id

REL_CLIENT_PROJECT_001

### 2.4.3.0.0 Source Entity

Client

### 2.4.4.0.0 Target Entity

Project

### 2.4.5.0.0 Type

🔹 OneToMany

### 2.4.6.0.0 Source Multiplicity

1

### 2.4.7.0.0 Target Multiplicity

0..*

### 2.4.8.0.0 Cascade Delete

❌ No

### 2.4.9.0.0 Is Identifying

❌ No

### 2.4.10.0.0 On Delete

Restrict

### 2.4.11.0.0 On Update

Cascade

## 2.5.0.0.0 ProjectSowDocument

### 2.5.1.0.0 Name

ProjectSowDocument

### 2.5.2.0.0 Id

REL_PROJECT_SOWDOCUMENT_001

### 2.5.3.0.0 Source Entity

Project

### 2.5.4.0.0 Target Entity

SowDocument

### 2.5.5.0.0 Type

🔹 Composition

### 2.5.6.0.0 Source Multiplicity

1

### 2.5.7.0.0 Target Multiplicity

1

### 2.5.8.0.0 Cascade Delete

✅ Yes

### 2.5.9.0.0 Is Identifying

❌ No

### 2.5.10.0.0 On Delete

Cascade

### 2.5.11.0.0 On Update

Cascade

## 2.6.0.0.0 UserUploadedSowDocuments

### 2.6.1.0.0 Name

UserUploadedSowDocuments

### 2.6.2.0.0 Id

REL_USER_SOWDOCUMENT_001

### 2.6.3.0.0 Source Entity

User

### 2.6.4.0.0 Target Entity

SowDocument

### 2.6.5.0.0 Type

🔹 OneToMany

### 2.6.6.0.0 Source Multiplicity

1

### 2.6.7.0.0 Target Multiplicity

0..*

### 2.6.8.0.0 Cascade Delete

❌ No

### 2.6.9.0.0 Is Identifying

❌ No

### 2.6.10.0.0 On Delete

Restrict

### 2.6.11.0.0 On Update

Cascade

## 2.7.0.0.0 ProjectProjectBrief

### 2.7.1.0.0 Name

ProjectProjectBrief

### 2.7.2.0.0 Id

REL_PROJECT_PROJECTBRIEF_001

### 2.7.3.0.0 Source Entity

Project

### 2.7.4.0.0 Target Entity

ProjectBrief

### 2.7.5.0.0 Type

🔹 Composition

### 2.7.6.0.0 Source Multiplicity

1

### 2.7.7.0.0 Target Multiplicity

1

### 2.7.8.0.0 Cascade Delete

✅ Yes

### 2.7.9.0.0 Is Identifying

❌ No

### 2.7.10.0.0 On Delete

Cascade

### 2.7.11.0.0 On Update

Cascade

## 2.8.0.0.0 UserReviewedProjectBriefs

### 2.8.1.0.0 Name

UserReviewedProjectBriefs

### 2.8.2.0.0 Id

REL_USER_PROJECTBRIEF_001

### 2.8.3.0.0 Source Entity

User

### 2.8.4.0.0 Target Entity

ProjectBrief

### 2.8.5.0.0 Type

🔹 OneToMany

### 2.8.6.0.0 Source Multiplicity

1

### 2.8.7.0.0 Target Multiplicity

0..*

### 2.8.8.0.0 Cascade Delete

❌ No

### 2.8.9.0.0 Is Identifying

❌ No

### 2.8.10.0.0 On Delete

SetNull

### 2.8.11.0.0 On Update

Cascade

## 2.9.0.0.0 ProjectProposals

### 2.9.1.0.0 Name

ProjectProposals

### 2.9.2.0.0 Id

REL_PROJECT_PROPOSAL_001

### 2.9.3.0.0 Source Entity

Project

### 2.9.4.0.0 Target Entity

Proposal

### 2.9.5.0.0 Type

🔹 Composition

### 2.9.6.0.0 Source Multiplicity

1

### 2.9.7.0.0 Target Multiplicity

0..*

### 2.9.8.0.0 Cascade Delete

✅ Yes

### 2.9.9.0.0 Is Identifying

❌ No

### 2.9.10.0.0 On Delete

Cascade

### 2.9.11.0.0 On Update

Cascade

## 2.10.0.0.0 VendorProposals

### 2.10.1.0.0 Name

VendorProposals

### 2.10.2.0.0 Id

REL_VENDOR_PROPOSAL_001

### 2.10.3.0.0 Source Entity

Vendor

### 2.10.4.0.0 Target Entity

Proposal

### 2.10.5.0.0 Type

🔹 OneToMany

### 2.10.6.0.0 Source Multiplicity

1

### 2.10.7.0.0 Target Multiplicity

0..*

### 2.10.8.0.0 Cascade Delete

❌ No

### 2.10.9.0.0 Is Identifying

❌ No

### 2.10.10.0.0 On Delete

Restrict

### 2.10.11.0.0 On Update

Cascade

## 2.11.0.0.0 ProjectProposalQuestions

### 2.11.1.0.0 Name

ProjectProposalQuestions

### 2.11.2.0.0 Id

REL_PROJECT_PROPOSALQUESTION_001

### 2.11.3.0.0 Source Entity

Project

### 2.11.4.0.0 Target Entity

ProposalQuestion

### 2.11.5.0.0 Type

🔹 Composition

### 2.11.6.0.0 Source Multiplicity

1

### 2.11.7.0.0 Target Multiplicity

0..*

### 2.11.8.0.0 Cascade Delete

✅ Yes

### 2.11.9.0.0 Is Identifying

❌ No

### 2.11.10.0.0 On Delete

Cascade

### 2.11.11.0.0 On Update

Cascade

## 2.12.0.0.0 VendorProposalQuestions

### 2.12.1.0.0 Name

VendorProposalQuestions

### 2.12.2.0.0 Id

REL_VENDOR_PROPOSALQUESTION_001

### 2.12.3.0.0 Source Entity

Vendor

### 2.12.4.0.0 Target Entity

ProposalQuestion

### 2.12.5.0.0 Type

🔹 OneToMany

### 2.12.6.0.0 Source Multiplicity

1

### 2.12.7.0.0 Target Multiplicity

0..*

### 2.12.8.0.0 Cascade Delete

❌ No

### 2.12.9.0.0 Is Identifying

❌ No

### 2.12.10.0.0 On Delete

Restrict

### 2.12.11.0.0 On Update

Cascade

## 2.13.0.0.0 UserAnsweredProposalQuestions

### 2.13.1.0.0 Name

UserAnsweredProposalQuestions

### 2.13.2.0.0 Id

REL_USER_PROPOSALQUESTION_001

### 2.13.3.0.0 Source Entity

User

### 2.13.4.0.0 Target Entity

ProposalQuestion

### 2.13.5.0.0 Type

🔹 OneToMany

### 2.13.6.0.0 Source Multiplicity

1

### 2.13.7.0.0 Target Multiplicity

0..*

### 2.13.8.0.0 Cascade Delete

❌ No

### 2.13.9.0.0 Is Identifying

❌ No

### 2.13.10.0.0 On Delete

SetNull

### 2.13.11.0.0 On Update

Cascade

## 2.14.0.0.0 ProjectInvoices

### 2.14.1.0.0 Name

ProjectInvoices

### 2.14.2.0.0 Id

REL_PROJECT_INVOICE_001

### 2.14.3.0.0 Source Entity

Project

### 2.14.4.0.0 Target Entity

Invoice

### 2.14.5.0.0 Type

🔹 Composition

### 2.14.6.0.0 Source Multiplicity

1

### 2.14.7.0.0 Target Multiplicity

0..*

### 2.14.8.0.0 Cascade Delete

✅ Yes

### 2.14.9.0.0 Is Identifying

❌ No

### 2.14.10.0.0 On Delete

Cascade

### 2.14.11.0.0 On Update

Cascade

## 2.15.0.0.0 ProjectPayouts

### 2.15.1.0.0 Name

ProjectPayouts

### 2.15.2.0.0 Id

REL_PROJECT_PAYOUT_001

### 2.15.3.0.0 Source Entity

Project

### 2.15.4.0.0 Target Entity

Payout

### 2.15.5.0.0 Type

🔹 Composition

### 2.15.6.0.0 Source Multiplicity

1

### 2.15.7.0.0 Target Multiplicity

0..*

### 2.15.8.0.0 Cascade Delete

✅ Yes

### 2.15.9.0.0 Is Identifying

❌ No

### 2.15.10.0.0 On Delete

Cascade

### 2.15.11.0.0 On Update

Cascade

## 2.16.0.0.0 VendorPayouts

### 2.16.1.0.0 Name

VendorPayouts

### 2.16.2.0.0 Id

REL_VENDOR_PAYOUT_001

### 2.16.3.0.0 Source Entity

Vendor

### 2.16.4.0.0 Target Entity

Payout

### 2.16.5.0.0 Type

🔹 OneToMany

### 2.16.6.0.0 Source Multiplicity

1

### 2.16.7.0.0 Target Multiplicity

0..*

### 2.16.8.0.0 Cascade Delete

❌ No

### 2.16.9.0.0 Is Identifying

❌ No

### 2.16.10.0.0 On Delete

Restrict

### 2.16.11.0.0 On Update

Cascade

## 2.17.0.0.0 ProjectProjectPayoutRules

### 2.17.1.0.0 Name

ProjectProjectPayoutRules

### 2.17.2.0.0 Id

REL_PROJECT_PROJECTPAYOUTRULE_001

### 2.17.3.0.0 Source Entity

Project

### 2.17.4.0.0 Target Entity

ProjectPayoutRule

### 2.17.5.0.0 Type

🔹 Composition

### 2.17.6.0.0 Source Multiplicity

1

### 2.17.7.0.0 Target Multiplicity

0..*

### 2.17.8.0.0 Cascade Delete

✅ Yes

### 2.17.9.0.0 Is Identifying

❌ No

### 2.17.10.0.0 On Delete

Cascade

### 2.17.11.0.0 On Update

Cascade

## 2.18.0.0.0 UserAuditLogs

### 2.18.1.0.0 Name

UserAuditLogs

### 2.18.2.0.0 Id

REL_USER_AUDITLOG_001

### 2.18.3.0.0 Source Entity

User

### 2.18.4.0.0 Target Entity

AuditLog

### 2.18.5.0.0 Type

🔹 OneToMany

### 2.18.6.0.0 Source Multiplicity

1

### 2.18.7.0.0 Target Multiplicity

0..*

### 2.18.8.0.0 Cascade Delete

❌ No

### 2.18.9.0.0 Is Identifying

❌ No

### 2.18.10.0.0 On Delete

Restrict

### 2.18.11.0.0 On Update

Cascade

## 2.19.0.0.0 UserNotifications

### 2.19.1.0.0 Name

UserNotifications

### 2.19.2.0.0 Id

REL_USER_NOTIFICATION_001

### 2.19.3.0.0 Source Entity

User

### 2.19.4.0.0 Target Entity

Notification

### 2.19.5.0.0 Type

🔹 Composition

### 2.19.6.0.0 Source Multiplicity

1

### 2.19.7.0.0 Target Multiplicity

0..*

### 2.19.8.0.0 Cascade Delete

✅ Yes

### 2.19.9.0.0 Is Identifying

❌ No

### 2.19.10.0.0 On Delete

Cascade

### 2.19.11.0.0 On Update

Cascade

## 2.20.0.0.0 VendorSkills

### 2.20.1.0.0 Name

VendorSkills

### 2.20.2.0.0 Id

REL_VENDOR_SKILL_001

### 2.20.3.0.0 Source Entity

Vendor

### 2.20.4.0.0 Target Entity

Skill

### 2.20.5.0.0 Type

🔹 ManyToMany

### 2.20.6.0.0 Source Multiplicity

0..*

### 2.20.7.0.0 Target Multiplicity

0..*

### 2.20.8.0.0 Cascade Delete

❌ No

### 2.20.9.0.0 Is Identifying

✅ Yes

### 2.20.10.0.0 Join Table

#### 2.20.10.1.0 Name

VendorSkill

#### 2.20.10.2.0 Columns

##### 2.20.10.2.1 vendorId

###### 2.20.10.2.1.1 Name

vendorId

###### 2.20.10.2.1.2 Type

🔹 Guid

###### 2.20.10.2.1.3 References

Vendor.vendorId

##### 2.20.10.2.2.0 skillId

###### 2.20.10.2.2.1 Name

skillId

###### 2.20.10.2.2.2 Type

🔹 Guid

###### 2.20.10.2.2.3 References

Skill.skillId

### 2.20.11.0.0.0 On Delete

Cascade

### 2.20.12.0.0.0 On Update

Cascade

## 2.21.0.0.0.0 ProjectBriefSkills

### 2.21.1.0.0.0 Name

ProjectBriefSkills

### 2.21.2.0.0.0 Id

REL_PROJECTBRIEF_SKILL_001

### 2.21.3.0.0.0 Source Entity

ProjectBrief

### 2.21.4.0.0.0 Target Entity

Skill

### 2.21.5.0.0.0 Type

🔹 ManyToMany

### 2.21.6.0.0.0 Source Multiplicity

0..*

### 2.21.7.0.0.0 Target Multiplicity

0..*

### 2.21.8.0.0.0 Cascade Delete

❌ No

### 2.21.9.0.0.0 Is Identifying

✅ Yes

### 2.21.10.0.0.0 Join Table

#### 2.21.10.1.0.0 Name

ProjectBriefSkill

#### 2.21.10.2.0.0 Columns

##### 2.21.10.2.1.0 projectBriefId

###### 2.21.10.2.1.1 Name

projectBriefId

###### 2.21.10.2.1.2 Type

🔹 Guid

###### 2.21.10.2.1.3 References

ProjectBrief.projectBriefId

##### 2.21.10.2.2.0 skillId

###### 2.21.10.2.2.1 Name

skillId

###### 2.21.10.2.2.2 Type

🔹 Guid

###### 2.21.10.2.2.3 References

Skill.skillId

### 2.21.11.0.0.0 On Delete

Cascade

### 2.21.12.0.0.0 On Update

Cascade

