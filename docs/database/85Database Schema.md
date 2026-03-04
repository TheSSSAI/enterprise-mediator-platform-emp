# 1 Title

Enterprise Mediator Platform - Transactional Database

# 2 Name

emp_transactional_db

# 3 Db Type

- relational
- vector

# 4 Db Technology

PostgreSQL 16

# 5 Entities

## 5.1 User

### 5.1.1 Name

User

### 5.1.2 Description

Represents system users (Administrators, Finance Managers, Vendor Contacts, Client Contacts) with authentication and profile information. Compliant with REQ-DAT-001 and REQ-NFR-003.

### 5.1.3 Attributes

#### 5.1.3.1 userId

##### 5.1.3.1.1 Name

userId

##### 5.1.3.1.2 Type

🔹 Guid

##### 5.1.3.1.3 Is Required

✅ Yes

##### 5.1.3.1.4 Is Primary Key

✅ Yes

##### 5.1.3.1.5 Size

0

##### 5.1.3.1.6 Is Unique

✅ Yes

##### 5.1.3.1.7 Constraints

*No items available*

##### 5.1.3.1.8 Precision

0

##### 5.1.3.1.9 Scale

0

##### 5.1.3.1.10 Is Foreign Key

❌ No

#### 5.1.3.2.0 email

##### 5.1.3.2.1 Name

email

##### 5.1.3.2.2 Type

🔹 VARCHAR

##### 5.1.3.2.3 Is Required

✅ Yes

##### 5.1.3.2.4 Is Primary Key

❌ No

##### 5.1.3.2.5 Size

255

##### 5.1.3.2.6 Is Unique

✅ Yes

##### 5.1.3.2.7 Constraints

- CHECK (email ~* '^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$')

##### 5.1.3.2.8 Precision

0

##### 5.1.3.2.9 Scale

0

##### 5.1.3.2.10 Is Foreign Key

❌ No

#### 5.1.3.3.0 passwordHash

##### 5.1.3.3.1 Name

passwordHash

##### 5.1.3.3.2 Type

🔹 VARCHAR

##### 5.1.3.3.3 Is Required

✅ Yes

##### 5.1.3.3.4 Is Primary Key

❌ No

##### 5.1.3.3.5 Size

255

##### 5.1.3.3.6 Is Unique

❌ No

##### 5.1.3.3.7 Constraints

*No items available*

##### 5.1.3.3.8 Precision

0

##### 5.1.3.3.9 Scale

0

##### 5.1.3.3.10 Is Foreign Key

❌ No

#### 5.1.3.4.0 firstName

##### 5.1.3.4.1 Name

firstName

##### 5.1.3.4.2 Type

🔹 VARCHAR

##### 5.1.3.4.3 Is Required

✅ Yes

##### 5.1.3.4.4 Is Primary Key

❌ No

##### 5.1.3.4.5 Size

100

##### 5.1.3.4.6 Is Unique

❌ No

##### 5.1.3.4.7 Constraints

*No items available*

##### 5.1.3.4.8 Precision

0

##### 5.1.3.4.9 Scale

0

##### 5.1.3.4.10 Is Foreign Key

❌ No

#### 5.1.3.5.0 lastName

##### 5.1.3.5.1 Name

lastName

##### 5.1.3.5.2 Type

🔹 VARCHAR

##### 5.1.3.5.3 Is Required

✅ Yes

##### 5.1.3.5.4 Is Primary Key

❌ No

##### 5.1.3.5.5 Size

100

##### 5.1.3.5.6 Is Unique

❌ No

##### 5.1.3.5.7 Constraints

*No items available*

##### 5.1.3.5.8 Precision

0

##### 5.1.3.5.9 Scale

0

##### 5.1.3.5.10 Is Foreign Key

❌ No

#### 5.1.3.6.0 roleId

##### 5.1.3.6.1 Name

roleId

##### 5.1.3.6.2 Type

🔹 Guid

##### 5.1.3.6.3 Is Required

✅ Yes

##### 5.1.3.6.4 Is Primary Key

❌ No

##### 5.1.3.6.5 Size

0

##### 5.1.3.6.6 Is Unique

❌ No

##### 5.1.3.6.7 Constraints

*No items available*

##### 5.1.3.6.8 Precision

0

##### 5.1.3.6.9 Scale

0

##### 5.1.3.6.10 Is Foreign Key

✅ Yes

#### 5.1.3.7.0 clientId

##### 5.1.3.7.1 Name

clientId

##### 5.1.3.7.2 Type

🔹 Guid

##### 5.1.3.7.3 Is Required

❌ No

##### 5.1.3.7.4 Is Primary Key

❌ No

##### 5.1.3.7.5 Size

0

##### 5.1.3.7.6 Is Unique

❌ No

##### 5.1.3.7.7 Constraints

*No items available*

##### 5.1.3.7.8 Precision

0

##### 5.1.3.7.9 Scale

0

##### 5.1.3.7.10 Is Foreign Key

✅ Yes

#### 5.1.3.8.0 vendorId

##### 5.1.3.8.1 Name

vendorId

##### 5.1.3.8.2 Type

🔹 Guid

##### 5.1.3.8.3 Is Required

❌ No

##### 5.1.3.8.4 Is Primary Key

❌ No

##### 5.1.3.8.5 Size

0

##### 5.1.3.8.6 Is Unique

❌ No

##### 5.1.3.8.7 Constraints

*No items available*

##### 5.1.3.8.8 Precision

0

##### 5.1.3.8.9 Scale

0

##### 5.1.3.8.10 Is Foreign Key

✅ Yes

#### 5.1.3.9.0 mfaEnabled

##### 5.1.3.9.1 Name

mfaEnabled

##### 5.1.3.9.2 Type

🔹 BOOLEAN

##### 5.1.3.9.3 Is Required

✅ Yes

##### 5.1.3.9.4 Is Primary Key

❌ No

##### 5.1.3.9.5 Size

0

##### 5.1.3.9.6 Is Unique

❌ No

##### 5.1.3.9.7 Constraints

- DEFAULT false

##### 5.1.3.9.8 Precision

0

##### 5.1.3.9.9 Scale

0

##### 5.1.3.9.10 Is Foreign Key

❌ No

#### 5.1.3.10.0 mfaSecret

##### 5.1.3.10.1 Name

mfaSecret

##### 5.1.3.10.2 Type

🔹 VARCHAR

##### 5.1.3.10.3 Is Required

❌ No

##### 5.1.3.10.4 Is Primary Key

❌ No

##### 5.1.3.10.5 Size

255

##### 5.1.3.10.6 Is Unique

❌ No

##### 5.1.3.10.7 Constraints

*No items available*

##### 5.1.3.10.8 Precision

0

##### 5.1.3.10.9 Scale

0

##### 5.1.3.10.10 Is Foreign Key

❌ No

#### 5.1.3.11.0 isActive

##### 5.1.3.11.1 Name

isActive

##### 5.1.3.11.2 Type

🔹 BOOLEAN

##### 5.1.3.11.3 Is Required

✅ Yes

##### 5.1.3.11.4 Is Primary Key

❌ No

##### 5.1.3.11.5 Size

0

##### 5.1.3.11.6 Is Unique

❌ No

##### 5.1.3.11.7 Constraints

- DEFAULT true

##### 5.1.3.11.8 Precision

0

##### 5.1.3.11.9 Scale

0

##### 5.1.3.11.10 Is Foreign Key

❌ No

#### 5.1.3.12.0 isDeleted

##### 5.1.3.12.1 Name

isDeleted

##### 5.1.3.12.2 Type

🔹 BOOLEAN

##### 5.1.3.12.3 Is Required

✅ Yes

##### 5.1.3.12.4 Is Primary Key

❌ No

##### 5.1.3.12.5 Size

0

##### 5.1.3.12.6 Is Unique

❌ No

##### 5.1.3.12.7 Constraints

- DEFAULT false

##### 5.1.3.12.8 Precision

0

##### 5.1.3.12.9 Scale

0

##### 5.1.3.12.10 Is Foreign Key

❌ No

#### 5.1.3.13.0 createdAt

##### 5.1.3.13.1 Name

createdAt

##### 5.1.3.13.2 Type

🔹 DateTimeOffset

##### 5.1.3.13.3 Is Required

✅ Yes

##### 5.1.3.13.4 Is Primary Key

❌ No

##### 5.1.3.13.5 Size

0

##### 5.1.3.13.6 Is Unique

❌ No

##### 5.1.3.13.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.1.3.13.8 Precision

0

##### 5.1.3.13.9 Scale

0

##### 5.1.3.13.10 Is Foreign Key

❌ No

#### 5.1.3.14.0 updatedAt

##### 5.1.3.14.1 Name

updatedAt

##### 5.1.3.14.2 Type

🔹 DateTimeOffset

##### 5.1.3.14.3 Is Required

✅ Yes

##### 5.1.3.14.4 Is Primary Key

❌ No

##### 5.1.3.14.5 Size

0

##### 5.1.3.14.6 Is Unique

❌ No

##### 5.1.3.14.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.1.3.14.8 Precision

0

##### 5.1.3.14.9 Scale

0

##### 5.1.3.14.10 Is Foreign Key

❌ No

### 5.1.4.0.0 Primary Keys

- userId

### 5.1.5.0.0 Unique Constraints

- {'name': 'UC_User_Email', 'columns': ['email']}

### 5.1.6.0.0 Indexes

#### 5.1.6.1.0 IX_User_FullName

##### 5.1.6.1.1 Name

IX_User_FullName

##### 5.1.6.1.2 Columns

- firstName
- lastName

##### 5.1.6.1.3 Type

🔹 BTree

#### 5.1.6.2.0 IX_User_RoleId

##### 5.1.6.2.1 Name

IX_User_RoleId

##### 5.1.6.2.2 Columns

- roleId

##### 5.1.6.2.3 Type

🔹 BTree

#### 5.1.6.3.0 IX_User_Active_NotDeleted

##### 5.1.6.3.1 Name

IX_User_Active_NotDeleted

##### 5.1.6.3.2 Columns

- isActive
- isDeleted

##### 5.1.6.3.3 Type

🔹 BTree

## 5.2.0.0.0 Role

### 5.2.1.0.0 Name

Role

### 5.2.2.0.0 Description

Defines user roles and permissions as per REQ-SEC-001 (SystemAdmin, FinanceManager, ClientContact, VendorContact).

### 5.2.3.0.0 Attributes

#### 5.2.3.1.0 roleId

##### 5.2.3.1.1 Name

roleId

##### 5.2.3.1.2 Type

🔹 Guid

##### 5.2.3.1.3 Is Required

✅ Yes

##### 5.2.3.1.4 Is Primary Key

✅ Yes

##### 5.2.3.1.5 Size

0

##### 5.2.3.1.6 Is Unique

✅ Yes

##### 5.2.3.1.7 Constraints

*No items available*

##### 5.2.3.1.8 Precision

0

##### 5.2.3.1.9 Scale

0

##### 5.2.3.1.10 Is Foreign Key

❌ No

#### 5.2.3.2.0 name

##### 5.2.3.2.1 Name

name

##### 5.2.3.2.2 Type

🔹 VARCHAR

##### 5.2.3.2.3 Is Required

✅ Yes

##### 5.2.3.2.4 Is Primary Key

❌ No

##### 5.2.3.2.5 Size

50

##### 5.2.3.2.6 Is Unique

✅ Yes

##### 5.2.3.2.7 Constraints

*No items available*

##### 5.2.3.2.8 Precision

0

##### 5.2.3.2.9 Scale

0

##### 5.2.3.2.10 Is Foreign Key

❌ No

### 5.2.4.0.0 Primary Keys

- roleId

### 5.2.5.0.0 Unique Constraints

- {'name': 'UC_Role_Name', 'columns': ['name']}

### 5.2.6.0.0 Indexes

*No items available*

## 5.3.0.0.0 Client

### 5.3.1.0.0 Name

Client

### 5.3.2.0.0 Description

Represents a client company. Central entity for client management and project association. REQ-DAT-001.

### 5.3.3.0.0 Attributes

#### 5.3.3.1.0 clientId

##### 5.3.3.1.1 Name

clientId

##### 5.3.3.1.2 Type

🔹 Guid

##### 5.3.3.1.3 Is Required

✅ Yes

##### 5.3.3.1.4 Is Primary Key

✅ Yes

##### 5.3.3.1.5 Size

0

##### 5.3.3.1.6 Is Unique

✅ Yes

##### 5.3.3.1.7 Constraints

*No items available*

##### 5.3.3.1.8 Precision

0

##### 5.3.3.1.9 Scale

0

##### 5.3.3.1.10 Is Foreign Key

❌ No

#### 5.3.3.2.0 companyName

##### 5.3.3.2.1 Name

companyName

##### 5.3.3.2.2 Type

🔹 VARCHAR

##### 5.3.3.2.3 Is Required

✅ Yes

##### 5.3.3.2.4 Is Primary Key

❌ No

##### 5.3.3.2.5 Size

255

##### 5.3.3.2.6 Is Unique

❌ No

##### 5.3.3.2.7 Constraints

*No items available*

##### 5.3.3.2.8 Precision

0

##### 5.3.3.2.9 Scale

0

##### 5.3.3.2.10 Is Foreign Key

❌ No

#### 5.3.3.3.0 address

##### 5.3.3.3.1 Name

address

##### 5.3.3.3.2 Type

🔹 JSONB

##### 5.3.3.3.3 Is Required

❌ No

##### 5.3.3.3.4 Is Primary Key

❌ No

##### 5.3.3.3.5 Size

0

##### 5.3.3.3.6 Is Unique

❌ No

##### 5.3.3.3.7 Constraints

*No items available*

##### 5.3.3.3.8 Precision

0

##### 5.3.3.3.9 Scale

0

##### 5.3.3.3.10 Is Foreign Key

❌ No

#### 5.3.3.4.0 billingDetails

##### 5.3.3.4.1 Name

billingDetails

##### 5.3.3.4.2 Type

🔹 JSONB

##### 5.3.3.4.3 Is Required

❌ No

##### 5.3.3.4.4 Is Primary Key

❌ No

##### 5.3.3.4.5 Size

0

##### 5.3.3.4.6 Is Unique

❌ No

##### 5.3.3.4.7 Constraints

*No items available*

##### 5.3.3.4.8 Precision

0

##### 5.3.3.4.9 Scale

0

##### 5.3.3.4.10 Is Foreign Key

❌ No

#### 5.3.3.5.0 status

##### 5.3.3.5.1 Name

status

##### 5.3.3.5.2 Type

🔹 VARCHAR

##### 5.3.3.5.3 Is Required

✅ Yes

##### 5.3.3.5.4 Is Primary Key

❌ No

##### 5.3.3.5.5 Size

50

##### 5.3.3.5.6 Is Unique

❌ No

##### 5.3.3.5.7 Constraints

- CHECK (status IN ('Active', 'Inactive'))
- DEFAULT 'Active'

##### 5.3.3.5.8 Precision

0

##### 5.3.3.5.9 Scale

0

##### 5.3.3.5.10 Is Foreign Key

❌ No

#### 5.3.3.6.0 isDeleted

##### 5.3.3.6.1 Name

isDeleted

##### 5.3.3.6.2 Type

🔹 BOOLEAN

##### 5.3.3.6.3 Is Required

✅ Yes

##### 5.3.3.6.4 Is Primary Key

❌ No

##### 5.3.3.6.5 Size

0

##### 5.3.3.6.6 Is Unique

❌ No

##### 5.3.3.6.7 Constraints

- DEFAULT false

##### 5.3.3.6.8 Precision

0

##### 5.3.3.6.9 Scale

0

##### 5.3.3.6.10 Is Foreign Key

❌ No

#### 5.3.3.7.0 createdAt

##### 5.3.3.7.1 Name

createdAt

##### 5.3.3.7.2 Type

🔹 DateTimeOffset

##### 5.3.3.7.3 Is Required

✅ Yes

##### 5.3.3.7.4 Is Primary Key

❌ No

##### 5.3.3.7.5 Size

0

##### 5.3.3.7.6 Is Unique

❌ No

##### 5.3.3.7.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.3.3.7.8 Precision

0

##### 5.3.3.7.9 Scale

0

##### 5.3.3.7.10 Is Foreign Key

❌ No

#### 5.3.3.8.0 updatedAt

##### 5.3.3.8.1 Name

updatedAt

##### 5.3.3.8.2 Type

🔹 DateTimeOffset

##### 5.3.3.8.3 Is Required

✅ Yes

##### 5.3.3.8.4 Is Primary Key

❌ No

##### 5.3.3.8.5 Size

0

##### 5.3.3.8.6 Is Unique

❌ No

##### 5.3.3.8.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.3.3.8.8 Precision

0

##### 5.3.3.8.9 Scale

0

##### 5.3.3.8.10 Is Foreign Key

❌ No

### 5.3.4.0.0 Primary Keys

- clientId

### 5.3.5.0.0 Unique Constraints

*No items available*

### 5.3.6.0.0 Indexes

#### 5.3.6.1.0 IX_Client_CompanyName

##### 5.3.6.1.1 Name

IX_Client_CompanyName

##### 5.3.6.1.2 Columns

- companyName

##### 5.3.6.1.3 Type

🔹 BTree

#### 5.3.6.2.0 IX_Client_Status_NotDeleted_Name

##### 5.3.6.2.1 Name

IX_Client_Status_NotDeleted_Name

##### 5.3.6.2.2 Columns

- status
- isDeleted
- companyName

##### 5.3.6.2.3 Type

🔹 BTree

## 5.4.0.0.0 Vendor

### 5.4.1.0.0 Name

Vendor

### 5.4.2.0.0 Description

Represents a vendor company. Stores profile information, skills, and contact details. REQ-DAT-001.

### 5.4.3.0.0 Attributes

#### 5.4.3.1.0 vendorId

##### 5.4.3.1.1 Name

vendorId

##### 5.4.3.1.2 Type

🔹 Guid

##### 5.4.3.1.3 Is Required

✅ Yes

##### 5.4.3.1.4 Is Primary Key

✅ Yes

##### 5.4.3.1.5 Size

0

##### 5.4.3.1.6 Is Unique

✅ Yes

##### 5.4.3.1.7 Constraints

*No items available*

##### 5.4.3.1.8 Precision

0

##### 5.4.3.1.9 Scale

0

##### 5.4.3.1.10 Is Foreign Key

❌ No

#### 5.4.3.2.0 companyName

##### 5.4.3.2.1 Name

companyName

##### 5.4.3.2.2 Type

🔹 VARCHAR

##### 5.4.3.2.3 Is Required

✅ Yes

##### 5.4.3.2.4 Is Primary Key

❌ No

##### 5.4.3.2.5 Size

255

##### 5.4.3.2.6 Is Unique

❌ No

##### 5.4.3.2.7 Constraints

*No items available*

##### 5.4.3.2.8 Precision

0

##### 5.4.3.2.9 Scale

0

##### 5.4.3.2.10 Is Foreign Key

❌ No

#### 5.4.3.3.0 contactInfo

##### 5.4.3.3.1 Name

contactInfo

##### 5.4.3.3.2 Type

🔹 JSONB

##### 5.4.3.3.3 Is Required

❌ No

##### 5.4.3.3.4 Is Primary Key

❌ No

##### 5.4.3.3.5 Size

0

##### 5.4.3.3.6 Is Unique

❌ No

##### 5.4.3.3.7 Constraints

*No items available*

##### 5.4.3.3.8 Precision

0

##### 5.4.3.3.9 Scale

0

##### 5.4.3.3.10 Is Foreign Key

❌ No

#### 5.4.3.4.0 address

##### 5.4.3.4.1 Name

address

##### 5.4.3.4.2 Type

🔹 JSONB

##### 5.4.3.4.3 Is Required

❌ No

##### 5.4.3.4.4 Is Primary Key

❌ No

##### 5.4.3.4.5 Size

0

##### 5.4.3.4.6 Is Unique

❌ No

##### 5.4.3.4.7 Constraints

*No items available*

##### 5.4.3.4.8 Precision

0

##### 5.4.3.4.9 Scale

0

##### 5.4.3.4.10 Is Foreign Key

❌ No

#### 5.4.3.5.0 paymentDetails

##### 5.4.3.5.1 Name

paymentDetails

##### 5.4.3.5.2 Type

🔹 JSONB

##### 5.4.3.5.3 Is Required

❌ No

##### 5.4.3.5.4 Is Primary Key

❌ No

##### 5.4.3.5.5 Size

0

##### 5.4.3.5.6 Is Unique

❌ No

##### 5.4.3.5.7 Constraints

*No items available*

##### 5.4.3.5.8 Precision

0

##### 5.4.3.5.9 Scale

0

##### 5.4.3.5.10 Is Foreign Key

❌ No

#### 5.4.3.6.0 paymentGatewayAccountId

##### 5.4.3.6.1 Name

paymentGatewayAccountId

##### 5.4.3.6.2 Type

🔹 VARCHAR

##### 5.4.3.6.3 Is Required

❌ No

##### 5.4.3.6.4 Is Primary Key

❌ No

##### 5.4.3.6.5 Size

255

##### 5.4.3.6.6 Is Unique

✅ Yes

##### 5.4.3.6.7 Constraints

*No items available*

##### 5.4.3.6.8 Precision

0

##### 5.4.3.6.9 Scale

0

##### 5.4.3.6.10 Is Foreign Key

❌ No

#### 5.4.3.7.0 vettingStatus

##### 5.4.3.7.1 Name

vettingStatus

##### 5.4.3.7.2 Type

🔹 VARCHAR

##### 5.4.3.7.3 Is Required

✅ Yes

##### 5.4.3.7.4 Is Primary Key

❌ No

##### 5.4.3.7.5 Size

50

##### 5.4.3.7.6 Is Unique

❌ No

##### 5.4.3.7.7 Constraints

- CHECK (vettingStatus IN ('PendingVetting', 'Active', 'Deactivated'))
- DEFAULT 'PendingVetting'

##### 5.4.3.7.8 Precision

0

##### 5.4.3.7.9 Scale

0

##### 5.4.3.7.10 Is Foreign Key

❌ No

#### 5.4.3.8.0 performanceMetrics

##### 5.4.3.8.1 Name

performanceMetrics

##### 5.4.3.8.2 Type

🔹 JSONB

##### 5.4.3.8.3 Is Required

❌ No

##### 5.4.3.8.4 Is Primary Key

❌ No

##### 5.4.3.8.5 Size

0

##### 5.4.3.8.6 Is Unique

❌ No

##### 5.4.3.8.7 Constraints

*No items available*

##### 5.4.3.8.8 Precision

0

##### 5.4.3.8.9 Scale

0

##### 5.4.3.8.10 Is Foreign Key

❌ No

#### 5.4.3.9.0 isDeleted

##### 5.4.3.9.1 Name

isDeleted

##### 5.4.3.9.2 Type

🔹 BOOLEAN

##### 5.4.3.9.3 Is Required

✅ Yes

##### 5.4.3.9.4 Is Primary Key

❌ No

##### 5.4.3.9.5 Size

0

##### 5.4.3.9.6 Is Unique

❌ No

##### 5.4.3.9.7 Constraints

- DEFAULT false

##### 5.4.3.9.8 Precision

0

##### 5.4.3.9.9 Scale

0

##### 5.4.3.9.10 Is Foreign Key

❌ No

#### 5.4.3.10.0 createdAt

##### 5.4.3.10.1 Name

createdAt

##### 5.4.3.10.2 Type

🔹 DateTimeOffset

##### 5.4.3.10.3 Is Required

✅ Yes

##### 5.4.3.10.4 Is Primary Key

❌ No

##### 5.4.3.10.5 Size

0

##### 5.4.3.10.6 Is Unique

❌ No

##### 5.4.3.10.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.4.3.10.8 Precision

0

##### 5.4.3.10.9 Scale

0

##### 5.4.3.10.10 Is Foreign Key

❌ No

#### 5.4.3.11.0 updatedAt

##### 5.4.3.11.1 Name

updatedAt

##### 5.4.3.11.2 Type

🔹 DateTimeOffset

##### 5.4.3.11.3 Is Required

✅ Yes

##### 5.4.3.11.4 Is Primary Key

❌ No

##### 5.4.3.11.5 Size

0

##### 5.4.3.11.6 Is Unique

❌ No

##### 5.4.3.11.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.4.3.11.8 Precision

0

##### 5.4.3.11.9 Scale

0

##### 5.4.3.11.10 Is Foreign Key

❌ No

### 5.4.4.0.0 Primary Keys

- vendorId

### 5.4.5.0.0 Unique Constraints

- {'name': 'UC_Vendor_PaymentGatewayAccountId', 'columns': ['paymentGatewayAccountId']}

### 5.4.6.0.0 Indexes

#### 5.4.6.1.0 IX_Vendor_CompanyName

##### 5.4.6.1.1 Name

IX_Vendor_CompanyName

##### 5.4.6.1.2 Columns

- companyName

##### 5.4.6.1.3 Type

🔹 BTree

#### 5.4.6.2.0 IX_Vendor_VettingStatus

##### 5.4.6.2.1 Name

IX_Vendor_VettingStatus

##### 5.4.6.2.2 Columns

- vettingStatus

##### 5.4.6.2.3 Type

🔹 BTree

## 5.5.0.0.0 Skill

### 5.5.1.0.0 Name

Skill

### 5.5.2.0.0 Description

Master list of skills and expertise for vendors and projects. Includes vector embeddings for semantic search per REQ-FUN-002.

### 5.5.3.0.0 Attributes

#### 5.5.3.1.0 skillId

##### 5.5.3.1.1 Name

skillId

##### 5.5.3.1.2 Type

🔹 Guid

##### 5.5.3.1.3 Is Required

✅ Yes

##### 5.5.3.1.4 Is Primary Key

✅ Yes

##### 5.5.3.1.5 Size

0

##### 5.5.3.1.6 Is Unique

✅ Yes

##### 5.5.3.1.7 Constraints

*No items available*

##### 5.5.3.1.8 Precision

0

##### 5.5.3.1.9 Scale

0

##### 5.5.3.1.10 Is Foreign Key

❌ No

#### 5.5.3.2.0 name

##### 5.5.3.2.1 Name

name

##### 5.5.3.2.2 Type

🔹 VARCHAR

##### 5.5.3.2.3 Is Required

✅ Yes

##### 5.5.3.2.4 Is Primary Key

❌ No

##### 5.5.3.2.5 Size

100

##### 5.5.3.2.6 Is Unique

✅ Yes

##### 5.5.3.2.7 Constraints

*No items available*

##### 5.5.3.2.8 Precision

0

##### 5.5.3.2.9 Scale

0

##### 5.5.3.2.10 Is Foreign Key

❌ No

#### 5.5.3.3.0 vectorEmbedding

##### 5.5.3.3.1 Name

vectorEmbedding

##### 5.5.3.3.2 Type

🔹 vector(1536)

##### 5.5.3.3.3 Is Required

❌ No

##### 5.5.3.3.4 Is Primary Key

❌ No

##### 5.5.3.3.5 Size

0

##### 5.5.3.3.6 Is Unique

❌ No

##### 5.5.3.3.7 Constraints

*No items available*

##### 5.5.3.3.8 Precision

0

##### 5.5.3.3.9 Scale

0

##### 5.5.3.3.10 Is Foreign Key

❌ No

### 5.5.4.0.0 Primary Keys

- skillId

### 5.5.5.0.0 Unique Constraints

- {'name': 'UC_Skill_Name', 'columns': ['name']}

### 5.5.6.0.0 Indexes

- {'name': 'IX_Skill_VectorEmbedding_HNSW', 'columns': ['vectorEmbedding'], 'type': 'HNSW'}

## 5.6.0.0.0 VendorSkill

### 5.6.1.0.0 Name

VendorSkill

### 5.6.2.0.0 Description

Join table for the many-to-many relationship between Vendors and Skills.

### 5.6.3.0.0 Attributes

#### 5.6.3.1.0 vendorId

##### 5.6.3.1.1 Name

vendorId

##### 5.6.3.1.2 Type

🔹 Guid

##### 5.6.3.1.3 Is Required

✅ Yes

##### 5.6.3.1.4 Is Primary Key

✅ Yes

##### 5.6.3.1.5 Size

0

##### 5.6.3.1.6 Is Unique

❌ No

##### 5.6.3.1.7 Constraints

*No items available*

##### 5.6.3.1.8 Precision

0

##### 5.6.3.1.9 Scale

0

##### 5.6.3.1.10 Is Foreign Key

✅ Yes

#### 5.6.3.2.0 skillId

##### 5.6.3.2.1 Name

skillId

##### 5.6.3.2.2 Type

🔹 Guid

##### 5.6.3.2.3 Is Required

✅ Yes

##### 5.6.3.2.4 Is Primary Key

✅ Yes

##### 5.6.3.2.5 Size

0

##### 5.6.3.2.6 Is Unique

❌ No

##### 5.6.3.2.7 Constraints

*No items available*

##### 5.6.3.2.8 Precision

0

##### 5.6.3.2.9 Scale

0

##### 5.6.3.2.10 Is Foreign Key

✅ Yes

### 5.6.4.0.0 Primary Keys

- vendorId
- skillId

### 5.6.5.0.0 Unique Constraints

*No items available*

### 5.6.6.0.0 Indexes

- {'name': 'IX_VendorSkill_SkillId', 'columns': ['skillId'], 'type': 'BTree'}

## 5.7.0.0.0 Project

### 5.7.1.0.0 Name

Project

### 5.7.2.0.0 Description

A project derived from an SOW, central for proposals, financials, and status tracking. REQ-DAT-001.

### 5.7.3.0.0 Attributes

#### 5.7.3.1.0 projectId

##### 5.7.3.1.1 Name

projectId

##### 5.7.3.1.2 Type

🔹 Guid

##### 5.7.3.1.3 Is Required

✅ Yes

##### 5.7.3.1.4 Is Primary Key

✅ Yes

##### 5.7.3.1.5 Size

0

##### 5.7.3.1.6 Is Unique

✅ Yes

##### 5.7.3.1.7 Constraints

*No items available*

##### 5.7.3.1.8 Precision

0

##### 5.7.3.1.9 Scale

0

##### 5.7.3.1.10 Is Foreign Key

❌ No

#### 5.7.3.2.0 clientId

##### 5.7.3.2.1 Name

clientId

##### 5.7.3.2.2 Type

🔹 Guid

##### 5.7.3.2.3 Is Required

✅ Yes

##### 5.7.3.2.4 Is Primary Key

❌ No

##### 5.7.3.2.5 Size

0

##### 5.7.3.2.6 Is Unique

❌ No

##### 5.7.3.2.7 Constraints

*No items available*

##### 5.7.3.2.8 Precision

0

##### 5.7.3.2.9 Scale

0

##### 5.7.3.2.10 Is Foreign Key

✅ Yes

#### 5.7.3.3.0 awardedVendorId

##### 5.7.3.3.1 Name

awardedVendorId

##### 5.7.3.3.2 Type

🔹 Guid

##### 5.7.3.3.3 Is Required

❌ No

##### 5.7.3.3.4 Is Primary Key

❌ No

##### 5.7.3.3.5 Size

0

##### 5.7.3.3.6 Is Unique

❌ No

##### 5.7.3.3.7 Constraints

*No items available*

##### 5.7.3.3.8 Precision

0

##### 5.7.3.3.9 Scale

0

##### 5.7.3.3.10 Is Foreign Key

✅ Yes

#### 5.7.3.4.0 awardedProposalId

##### 5.7.3.4.1 Name

awardedProposalId

##### 5.7.3.4.2 Type

🔹 Guid

##### 5.7.3.4.3 Is Required

❌ No

##### 5.7.3.4.4 Is Primary Key

❌ No

##### 5.7.3.4.5 Size

0

##### 5.7.3.4.6 Is Unique

✅ Yes

##### 5.7.3.4.7 Constraints

*No items available*

##### 5.7.3.4.8 Precision

0

##### 5.7.3.4.9 Scale

0

##### 5.7.3.4.10 Is Foreign Key

✅ Yes

#### 5.7.3.5.0 name

##### 5.7.3.5.1 Name

name

##### 5.7.3.5.2 Type

🔹 VARCHAR

##### 5.7.3.5.3 Is Required

✅ Yes

##### 5.7.3.5.4 Is Primary Key

❌ No

##### 5.7.3.5.5 Size

255

##### 5.7.3.5.6 Is Unique

❌ No

##### 5.7.3.5.7 Constraints

*No items available*

##### 5.7.3.5.8 Precision

0

##### 5.7.3.5.9 Scale

0

##### 5.7.3.5.10 Is Foreign Key

❌ No

#### 5.7.3.6.0 status

##### 5.7.3.6.1 Name

status

##### 5.7.3.6.2 Type

🔹 VARCHAR

##### 5.7.3.6.3 Is Required

✅ Yes

##### 5.7.3.6.4 Is Primary Key

❌ No

##### 5.7.3.6.5 Size

50

##### 5.7.3.6.6 Is Unique

❌ No

##### 5.7.3.6.7 Constraints

- CHECK (status IN ('SOW_REVIEW', 'MATCHING', 'PROPOSED', 'AWARDED', 'ACTIVE', 'COMPLETED', 'CANCELLED', 'ON_HOLD', 'DISPUTED'))
- DEFAULT 'SOW_REVIEW'

##### 5.7.3.6.8 Precision

0

##### 5.7.3.6.9 Scale

0

##### 5.7.3.6.10 Is Foreign Key

❌ No

#### 5.7.3.7.0 startDate

##### 5.7.3.7.1 Name

startDate

##### 5.7.3.7.2 Type

🔹 DateTimeOffset

##### 5.7.3.7.3 Is Required

❌ No

##### 5.7.3.7.4 Is Primary Key

❌ No

##### 5.7.3.7.5 Size

0

##### 5.7.3.7.6 Is Unique

❌ No

##### 5.7.3.7.7 Constraints

*No items available*

##### 5.7.3.7.8 Precision

0

##### 5.7.3.7.9 Scale

0

##### 5.7.3.7.10 Is Foreign Key

❌ No

#### 5.7.3.8.0 endDate

##### 5.7.3.8.1 Name

endDate

##### 5.7.3.8.2 Type

🔹 DateTimeOffset

##### 5.7.3.8.3 Is Required

❌ No

##### 5.7.3.8.4 Is Primary Key

❌ No

##### 5.7.3.8.5 Size

0

##### 5.7.3.8.6 Is Unique

❌ No

##### 5.7.3.8.7 Constraints

*No items available*

##### 5.7.3.8.8 Precision

0

##### 5.7.3.8.9 Scale

0

##### 5.7.3.8.10 Is Foreign Key

❌ No

#### 5.7.3.9.0 marginOverride

##### 5.7.3.9.1 Name

marginOverride

##### 5.7.3.9.2 Type

🔹 DECIMAL

##### 5.7.3.9.3 Is Required

❌ No

##### 5.7.3.9.4 Is Primary Key

❌ No

##### 5.7.3.9.5 Size

0

##### 5.7.3.9.6 Is Unique

❌ No

##### 5.7.3.9.7 Constraints

*No items available*

##### 5.7.3.9.8 Precision

5

##### 5.7.3.9.9 Scale

2

##### 5.7.3.9.10 Is Foreign Key

❌ No

#### 5.7.3.10.0 totalInvoicedAmount

##### 5.7.3.10.1 Name

totalInvoicedAmount

##### 5.7.3.10.2 Type

🔹 DECIMAL

##### 5.7.3.10.3 Is Required

✅ Yes

##### 5.7.3.10.4 Is Primary Key

❌ No

##### 5.7.3.10.5 Size

0

##### 5.7.3.10.6 Is Unique

❌ No

##### 5.7.3.10.7 Constraints

- DEFAULT 0.00

##### 5.7.3.10.8 Precision

14

##### 5.7.3.10.9 Scale

2

##### 5.7.3.10.10 Is Foreign Key

❌ No

#### 5.7.3.11.0 totalPaidOutAmount

##### 5.7.3.11.1 Name

totalPaidOutAmount

##### 5.7.3.11.2 Type

🔹 DECIMAL

##### 5.7.3.11.3 Is Required

✅ Yes

##### 5.7.3.11.4 Is Primary Key

❌ No

##### 5.7.3.11.5 Size

0

##### 5.7.3.11.6 Is Unique

❌ No

##### 5.7.3.11.7 Constraints

- DEFAULT 0.00

##### 5.7.3.11.8 Precision

14

##### 5.7.3.11.9 Scale

2

##### 5.7.3.11.10 Is Foreign Key

❌ No

#### 5.7.3.12.0 calculatedProfit

##### 5.7.3.12.1 Name

calculatedProfit

##### 5.7.3.12.2 Type

🔹 DECIMAL

##### 5.7.3.12.3 Is Required

✅ Yes

##### 5.7.3.12.4 Is Primary Key

❌ No

##### 5.7.3.12.5 Size

0

##### 5.7.3.12.6 Is Unique

❌ No

##### 5.7.3.12.7 Constraints

- DEFAULT 0.00

##### 5.7.3.12.8 Precision

14

##### 5.7.3.12.9 Scale

2

##### 5.7.3.12.10 Is Foreign Key

❌ No

#### 5.7.3.13.0 isDeleted

##### 5.7.3.13.1 Name

isDeleted

##### 5.7.3.13.2 Type

🔹 BOOLEAN

##### 5.7.3.13.3 Is Required

✅ Yes

##### 5.7.3.13.4 Is Primary Key

❌ No

##### 5.7.3.13.5 Size

0

##### 5.7.3.13.6 Is Unique

❌ No

##### 5.7.3.13.7 Constraints

- DEFAULT false

##### 5.7.3.13.8 Precision

0

##### 5.7.3.13.9 Scale

0

##### 5.7.3.13.10 Is Foreign Key

❌ No

#### 5.7.3.14.0 createdAt

##### 5.7.3.14.1 Name

createdAt

##### 5.7.3.14.2 Type

🔹 DateTimeOffset

##### 5.7.3.14.3 Is Required

✅ Yes

##### 5.7.3.14.4 Is Primary Key

❌ No

##### 5.7.3.14.5 Size

0

##### 5.7.3.14.6 Is Unique

❌ No

##### 5.7.3.14.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.7.3.14.8 Precision

0

##### 5.7.3.14.9 Scale

0

##### 5.7.3.14.10 Is Foreign Key

❌ No

#### 5.7.3.15.0 updatedAt

##### 5.7.3.15.1 Name

updatedAt

##### 5.7.3.15.2 Type

🔹 DateTimeOffset

##### 5.7.3.15.3 Is Required

✅ Yes

##### 5.7.3.15.4 Is Primary Key

❌ No

##### 5.7.3.15.5 Size

0

##### 5.7.3.15.6 Is Unique

❌ No

##### 5.7.3.15.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.7.3.15.8 Precision

0

##### 5.7.3.15.9 Scale

0

##### 5.7.3.15.10 Is Foreign Key

❌ No

### 5.7.4.0.0 Primary Keys

- projectId

### 5.7.5.0.0 Unique Constraints

*No items available*

### 5.7.6.0.0 Indexes

#### 5.7.6.1.0 IX_Project_ClientId

##### 5.7.6.1.1 Name

IX_Project_ClientId

##### 5.7.6.1.2 Columns

- clientId

##### 5.7.6.1.3 Type

🔹 BTree

#### 5.7.6.2.0 IX_Project_AwardedVendorId

##### 5.7.6.2.1 Name

IX_Project_AwardedVendorId

##### 5.7.6.2.2 Columns

- awardedVendorId

##### 5.7.6.2.3 Type

🔹 BTree

#### 5.7.6.3.0 IX_Project_Status_NotDeleted

##### 5.7.6.3.1 Name

IX_Project_Status_NotDeleted

##### 5.7.6.3.2 Columns

- status
- isDeleted

##### 5.7.6.3.3 Type

🔹 BTree

## 5.8.0.0.0 SowDocument

### 5.8.1.0.0 Name

SowDocument

### 5.8.2.0.0 Description

Metadata about an uploaded Statement of Work document, including its processing status. REQ-DAT-001.

### 5.8.3.0.0 Attributes

#### 5.8.3.1.0 sowDocumentId

##### 5.8.3.1.1 Name

sowDocumentId

##### 5.8.3.1.2 Type

🔹 Guid

##### 5.8.3.1.3 Is Required

✅ Yes

##### 5.8.3.1.4 Is Primary Key

✅ Yes

##### 5.8.3.1.5 Size

0

##### 5.8.3.1.6 Is Unique

✅ Yes

##### 5.8.3.1.7 Constraints

*No items available*

##### 5.8.3.1.8 Precision

0

##### 5.8.3.1.9 Scale

0

##### 5.8.3.1.10 Is Foreign Key

❌ No

#### 5.8.3.2.0 projectId

##### 5.8.3.2.1 Name

projectId

##### 5.8.3.2.2 Type

🔹 Guid

##### 5.8.3.2.3 Is Required

✅ Yes

##### 5.8.3.2.4 Is Primary Key

❌ No

##### 5.8.3.2.5 Size

0

##### 5.8.3.2.6 Is Unique

✅ Yes

##### 5.8.3.2.7 Constraints

*No items available*

##### 5.8.3.2.8 Precision

0

##### 5.8.3.2.9 Scale

0

##### 5.8.3.2.10 Is Foreign Key

✅ Yes

#### 5.8.3.3.0 originalFilename

##### 5.8.3.3.1 Name

originalFilename

##### 5.8.3.3.2 Type

🔹 VARCHAR

##### 5.8.3.3.3 Is Required

✅ Yes

##### 5.8.3.3.4 Is Primary Key

❌ No

##### 5.8.3.3.5 Size

255

##### 5.8.3.3.6 Is Unique

❌ No

##### 5.8.3.3.7 Constraints

*No items available*

##### 5.8.3.3.8 Precision

0

##### 5.8.3.3.9 Scale

0

##### 5.8.3.3.10 Is Foreign Key

❌ No

#### 5.8.3.4.0 storagePath

##### 5.8.3.4.1 Name

storagePath

##### 5.8.3.4.2 Type

🔹 VARCHAR

##### 5.8.3.4.3 Is Required

✅ Yes

##### 5.8.3.4.4 Is Primary Key

❌ No

##### 5.8.3.4.5 Size

1,024

##### 5.8.3.4.6 Is Unique

❌ No

##### 5.8.3.4.7 Constraints

*No items available*

##### 5.8.3.4.8 Precision

0

##### 5.8.3.4.9 Scale

0

##### 5.8.3.4.10 Is Foreign Key

❌ No

#### 5.8.3.5.0 sanitizedStoragePath

##### 5.8.3.5.1 Name

sanitizedStoragePath

##### 5.8.3.5.2 Type

🔹 VARCHAR

##### 5.8.3.5.3 Is Required

❌ No

##### 5.8.3.5.4 Is Primary Key

❌ No

##### 5.8.3.5.5 Size

1,024

##### 5.8.3.5.6 Is Unique

❌ No

##### 5.8.3.5.7 Constraints

*No items available*

##### 5.8.3.5.8 Precision

0

##### 5.8.3.5.9 Scale

0

##### 5.8.3.5.10 Is Foreign Key

❌ No

#### 5.8.3.6.0 status

##### 5.8.3.6.1 Name

status

##### 5.8.3.6.2 Type

🔹 VARCHAR

##### 5.8.3.6.3 Is Required

✅ Yes

##### 5.8.3.6.4 Is Primary Key

❌ No

##### 5.8.3.6.5 Size

50

##### 5.8.3.6.6 Is Unique

❌ No

##### 5.8.3.6.7 Constraints

- CHECK (status IN ('PROCESSING', 'REVIEW_PENDING', 'FAILED', 'COMPLETED'))
- DEFAULT 'PROCESSING'

##### 5.8.3.6.8 Precision

0

##### 5.8.3.6.9 Scale

0

##### 5.8.3.6.10 Is Foreign Key

❌ No

#### 5.8.3.7.0 processingErrorDetails

##### 5.8.3.7.1 Name

processingErrorDetails

##### 5.8.3.7.2 Type

🔹 TEXT

##### 5.8.3.7.3 Is Required

❌ No

##### 5.8.3.7.4 Is Primary Key

❌ No

##### 5.8.3.7.5 Size

0

##### 5.8.3.7.6 Is Unique

❌ No

##### 5.8.3.7.7 Constraints

*No items available*

##### 5.8.3.7.8 Precision

0

##### 5.8.3.7.9 Scale

0

##### 5.8.3.7.10 Is Foreign Key

❌ No

#### 5.8.3.8.0 uploadedById

##### 5.8.3.8.1 Name

uploadedById

##### 5.8.3.8.2 Type

🔹 Guid

##### 5.8.3.8.3 Is Required

✅ Yes

##### 5.8.3.8.4 Is Primary Key

❌ No

##### 5.8.3.8.5 Size

0

##### 5.8.3.8.6 Is Unique

❌ No

##### 5.8.3.8.7 Constraints

*No items available*

##### 5.8.3.8.8 Precision

0

##### 5.8.3.8.9 Scale

0

##### 5.8.3.8.10 Is Foreign Key

✅ Yes

#### 5.8.3.9.0 createdAt

##### 5.8.3.9.1 Name

createdAt

##### 5.8.3.9.2 Type

🔹 DateTimeOffset

##### 5.8.3.9.3 Is Required

✅ Yes

##### 5.8.3.9.4 Is Primary Key

❌ No

##### 5.8.3.9.5 Size

0

##### 5.8.3.9.6 Is Unique

❌ No

##### 5.8.3.9.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.8.3.9.8 Precision

0

##### 5.8.3.9.9 Scale

0

##### 5.8.3.9.10 Is Foreign Key

❌ No

### 5.8.4.0.0 Primary Keys

- sowDocumentId

### 5.8.5.0.0 Unique Constraints

- {'name': 'UC_SowDocument_ProjectId', 'columns': ['projectId']}

### 5.8.6.0.0 Indexes

- {'name': 'IX_SowDocument_Status', 'columns': ['status'], 'type': 'BTree'}

## 5.9.0.0.0 ProjectBrief

### 5.9.1.0.0 Name

ProjectBrief

### 5.9.2.0.0 Description

AI-extracted and human-verified data from an SOW, forming the basis for vendor matching. REQ-FUN-002.

### 5.9.3.0.0 Attributes

#### 5.9.3.1.0 projectBriefId

##### 5.9.3.1.1 Name

projectBriefId

##### 5.9.3.1.2 Type

🔹 Guid

##### 5.9.3.1.3 Is Required

✅ Yes

##### 5.9.3.1.4 Is Primary Key

✅ Yes

##### 5.9.3.1.5 Size

0

##### 5.9.3.1.6 Is Unique

✅ Yes

##### 5.9.3.1.7 Constraints

*No items available*

##### 5.9.3.1.8 Precision

0

##### 5.9.3.1.9 Scale

0

##### 5.9.3.1.10 Is Foreign Key

❌ No

#### 5.9.3.2.0 projectId

##### 5.9.3.2.1 Name

projectId

##### 5.9.3.2.2 Type

🔹 Guid

##### 5.9.3.2.3 Is Required

✅ Yes

##### 5.9.3.2.4 Is Primary Key

❌ No

##### 5.9.3.2.5 Size

0

##### 5.9.3.2.6 Is Unique

✅ Yes

##### 5.9.3.2.7 Constraints

*No items available*

##### 5.9.3.2.8 Precision

0

##### 5.9.3.2.9 Scale

0

##### 5.9.3.2.10 Is Foreign Key

✅ Yes

#### 5.9.3.3.0 scopeSummary

##### 5.9.3.3.1 Name

scopeSummary

##### 5.9.3.3.2 Type

🔹 TEXT

##### 5.9.3.3.3 Is Required

❌ No

##### 5.9.3.3.4 Is Primary Key

❌ No

##### 5.9.3.3.5 Size

0

##### 5.9.3.3.6 Is Unique

❌ No

##### 5.9.3.3.7 Constraints

*No items available*

##### 5.9.3.3.8 Precision

0

##### 5.9.3.3.9 Scale

0

##### 5.9.3.3.10 Is Foreign Key

❌ No

#### 5.9.3.4.0 extractedData

##### 5.9.3.4.1 Name

extractedData

##### 5.9.3.4.2 Type

🔹 JSONB

##### 5.9.3.4.3 Is Required

❌ No

##### 5.9.3.4.4 Is Primary Key

❌ No

##### 5.9.3.4.5 Size

0

##### 5.9.3.4.6 Is Unique

❌ No

##### 5.9.3.4.7 Constraints

*No items available*

##### 5.9.3.4.8 Precision

0

##### 5.9.3.4.9 Scale

0

##### 5.9.3.4.10 Is Foreign Key

❌ No

#### 5.9.3.5.0 status

##### 5.9.3.5.1 Name

status

##### 5.9.3.5.2 Type

🔹 VARCHAR

##### 5.9.3.5.3 Is Required

✅ Yes

##### 5.9.3.5.4 Is Primary Key

❌ No

##### 5.9.3.5.5 Size

50

##### 5.9.3.5.6 Is Unique

❌ No

##### 5.9.3.5.7 Constraints

- CHECK (status IN ('PENDING_REVIEW', 'APPROVED'))
- DEFAULT 'PENDING_REVIEW'

##### 5.9.3.5.8 Precision

0

##### 5.9.3.5.9 Scale

0

##### 5.9.3.5.10 Is Foreign Key

❌ No

#### 5.9.3.6.0 reviewedById

##### 5.9.3.6.1 Name

reviewedById

##### 5.9.3.6.2 Type

🔹 Guid

##### 5.9.3.6.3 Is Required

❌ No

##### 5.9.3.6.4 Is Primary Key

❌ No

##### 5.9.3.6.5 Size

0

##### 5.9.3.6.6 Is Unique

❌ No

##### 5.9.3.6.7 Constraints

*No items available*

##### 5.9.3.6.8 Precision

0

##### 5.9.3.6.9 Scale

0

##### 5.9.3.6.10 Is Foreign Key

✅ Yes

#### 5.9.3.7.0 reviewedAt

##### 5.9.3.7.1 Name

reviewedAt

##### 5.9.3.7.2 Type

🔹 DateTimeOffset

##### 5.9.3.7.3 Is Required

❌ No

##### 5.9.3.7.4 Is Primary Key

❌ No

##### 5.9.3.7.5 Size

0

##### 5.9.3.7.6 Is Unique

❌ No

##### 5.9.3.7.7 Constraints

*No items available*

##### 5.9.3.7.8 Precision

0

##### 5.9.3.7.9 Scale

0

##### 5.9.3.7.10 Is Foreign Key

❌ No

### 5.9.4.0.0 Primary Keys

- projectBriefId

### 5.9.5.0.0 Unique Constraints

- {'name': 'UC_ProjectBrief_ProjectId', 'columns': ['projectId']}

### 5.9.6.0.0 Indexes

- {'name': 'IX_ProjectBrief_Status', 'columns': ['status'], 'type': 'BTree'}

## 5.10.0.0.0 ProjectBriefSkill

### 5.10.1.0.0 Name

ProjectBriefSkill

### 5.10.2.0.0 Description

Join table for the many-to-many relationship between Project Briefs and required Skills.

### 5.10.3.0.0 Attributes

#### 5.10.3.1.0 projectBriefId

##### 5.10.3.1.1 Name

projectBriefId

##### 5.10.3.1.2 Type

🔹 Guid

##### 5.10.3.1.3 Is Required

✅ Yes

##### 5.10.3.1.4 Is Primary Key

✅ Yes

##### 5.10.3.1.5 Size

0

##### 5.10.3.1.6 Is Unique

❌ No

##### 5.10.3.1.7 Constraints

*No items available*

##### 5.10.3.1.8 Precision

0

##### 5.10.3.1.9 Scale

0

##### 5.10.3.1.10 Is Foreign Key

✅ Yes

#### 5.10.3.2.0 skillId

##### 5.10.3.2.1 Name

skillId

##### 5.10.3.2.2 Type

🔹 Guid

##### 5.10.3.2.3 Is Required

✅ Yes

##### 5.10.3.2.4 Is Primary Key

✅ Yes

##### 5.10.3.2.5 Size

0

##### 5.10.3.2.6 Is Unique

❌ No

##### 5.10.3.2.7 Constraints

*No items available*

##### 5.10.3.2.8 Precision

0

##### 5.10.3.2.9 Scale

0

##### 5.10.3.2.10 Is Foreign Key

✅ Yes

### 5.10.4.0.0 Primary Keys

- projectBriefId
- skillId

### 5.10.5.0.0 Unique Constraints

*No items available*

### 5.10.6.0.0 Indexes

- {'name': 'IX_ProjectBriefSkill_SkillId', 'columns': ['skillId'], 'type': 'BTree'}

## 5.11.0.0.0 Proposal

### 5.11.1.0.0 Name

Proposal

### 5.11.2.0.0 Description

A vendor's proposal for a project, including cost, timeline, and status. REQ-DAT-001.

### 5.11.3.0.0 Attributes

#### 5.11.3.1.0 proposalId

##### 5.11.3.1.1 Name

proposalId

##### 5.11.3.1.2 Type

🔹 Guid

##### 5.11.3.1.3 Is Required

✅ Yes

##### 5.11.3.1.4 Is Primary Key

✅ Yes

##### 5.11.3.1.5 Size

0

##### 5.11.3.1.6 Is Unique

✅ Yes

##### 5.11.3.1.7 Constraints

*No items available*

##### 5.11.3.1.8 Precision

0

##### 5.11.3.1.9 Scale

0

##### 5.11.3.1.10 Is Foreign Key

❌ No

#### 5.11.3.2.0 projectId

##### 5.11.3.2.1 Name

projectId

##### 5.11.3.2.2 Type

🔹 Guid

##### 5.11.3.2.3 Is Required

✅ Yes

##### 5.11.3.2.4 Is Primary Key

❌ No

##### 5.11.3.2.5 Size

0

##### 5.11.3.2.6 Is Unique

❌ No

##### 5.11.3.2.7 Constraints

*No items available*

##### 5.11.3.2.8 Precision

0

##### 5.11.3.2.9 Scale

0

##### 5.11.3.2.10 Is Foreign Key

✅ Yes

#### 5.11.3.3.0 vendorId

##### 5.11.3.3.1 Name

vendorId

##### 5.11.3.3.2 Type

🔹 Guid

##### 5.11.3.3.3 Is Required

✅ Yes

##### 5.11.3.3.4 Is Primary Key

❌ No

##### 5.11.3.3.5 Size

0

##### 5.11.3.3.6 Is Unique

❌ No

##### 5.11.3.3.7 Constraints

*No items available*

##### 5.11.3.3.8 Precision

0

##### 5.11.3.3.9 Scale

0

##### 5.11.3.3.10 Is Foreign Key

✅ Yes

#### 5.11.3.4.0 cost

##### 5.11.3.4.1 Name

cost

##### 5.11.3.4.2 Type

🔹 DECIMAL

##### 5.11.3.4.3 Is Required

✅ Yes

##### 5.11.3.4.4 Is Primary Key

❌ No

##### 5.11.3.4.5 Size

0

##### 5.11.3.4.6 Is Unique

❌ No

##### 5.11.3.4.7 Constraints

*No items available*

##### 5.11.3.4.8 Precision

12

##### 5.11.3.4.9 Scale

2

##### 5.11.3.4.10 Is Foreign Key

❌ No

#### 5.11.3.5.0 currency

##### 5.11.3.5.1 Name

currency

##### 5.11.3.5.2 Type

🔹 VARCHAR

##### 5.11.3.5.3 Is Required

✅ Yes

##### 5.11.3.5.4 Is Primary Key

❌ No

##### 5.11.3.5.5 Size

3

##### 5.11.3.5.6 Is Unique

❌ No

##### 5.11.3.5.7 Constraints

*No items available*

##### 5.11.3.5.8 Precision

0

##### 5.11.3.5.9 Scale

0

##### 5.11.3.5.10 Is Foreign Key

❌ No

#### 5.11.3.6.0 timelineDescription

##### 5.11.3.6.1 Name

timelineDescription

##### 5.11.3.6.2 Type

🔹 TEXT

##### 5.11.3.6.3 Is Required

✅ Yes

##### 5.11.3.6.4 Is Primary Key

❌ No

##### 5.11.3.6.5 Size

0

##### 5.11.3.6.6 Is Unique

❌ No

##### 5.11.3.6.7 Constraints

*No items available*

##### 5.11.3.6.8 Precision

0

##### 5.11.3.6.9 Scale

0

##### 5.11.3.6.10 Is Foreign Key

❌ No

#### 5.11.3.7.0 status

##### 5.11.3.7.1 Name

status

##### 5.11.3.7.2 Type

🔹 VARCHAR

##### 5.11.3.7.3 Is Required

✅ Yes

##### 5.11.3.7.4 Is Primary Key

❌ No

##### 5.11.3.7.5 Size

50

##### 5.11.3.7.6 Is Unique

❌ No

##### 5.11.3.7.7 Constraints

- CHECK (status IN ('SUBMITTED', 'IN_REVIEW', 'SHORTLISTED', 'REJECTED', 'ACCEPTED'))
- DEFAULT 'SUBMITTED'

##### 5.11.3.7.8 Precision

0

##### 5.11.3.7.9 Scale

0

##### 5.11.3.7.10 Is Foreign Key

❌ No

#### 5.11.3.8.0 submittedAt

##### 5.11.3.8.1 Name

submittedAt

##### 5.11.3.8.2 Type

🔹 DateTimeOffset

##### 5.11.3.8.3 Is Required

✅ Yes

##### 5.11.3.8.4 Is Primary Key

❌ No

##### 5.11.3.8.5 Size

0

##### 5.11.3.8.6 Is Unique

❌ No

##### 5.11.3.8.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.11.3.8.8 Precision

0

##### 5.11.3.8.9 Scale

0

##### 5.11.3.8.10 Is Foreign Key

❌ No

### 5.11.4.0.0 Primary Keys

- proposalId

### 5.11.5.0.0 Unique Constraints

- {'name': 'UC_Proposal_Project_Vendor', 'columns': ['projectId', 'vendorId']}

### 5.11.6.0.0 Indexes

#### 5.11.6.1.0 IX_Proposal_ProjectId_Status

##### 5.11.6.1.1 Name

IX_Proposal_ProjectId_Status

##### 5.11.6.1.2 Columns

- projectId
- status

##### 5.11.6.1.3 Type

🔹 BTree

#### 5.11.6.2.0 IX_Proposal_Vendor_SubmittedAt

##### 5.11.6.2.1 Name

IX_Proposal_Vendor_SubmittedAt

##### 5.11.6.2.2 Columns

- vendorId
- submittedAt

##### 5.11.6.2.3 Type

🔹 BTree

## 5.12.0.0.0 ProposalQuestion

### 5.12.1.0.0 Name

ProposalQuestion

### 5.12.2.0.0 Description

Questions submitted by vendors during the proposal phase and answers from administrators. REQ-FUN-003.

### 5.12.3.0.0 Attributes

#### 5.12.3.1.0 proposalQuestionId

##### 5.12.3.1.1 Name

proposalQuestionId

##### 5.12.3.1.2 Type

🔹 Guid

##### 5.12.3.1.3 Is Required

✅ Yes

##### 5.12.3.1.4 Is Primary Key

✅ Yes

##### 5.12.3.1.5 Size

0

##### 5.12.3.1.6 Is Unique

✅ Yes

##### 5.12.3.1.7 Constraints

*No items available*

##### 5.12.3.1.8 Precision

0

##### 5.12.3.1.9 Scale

0

##### 5.12.3.1.10 Is Foreign Key

❌ No

#### 5.12.3.2.0 projectId

##### 5.12.3.2.1 Name

projectId

##### 5.12.3.2.2 Type

🔹 Guid

##### 5.12.3.2.3 Is Required

✅ Yes

##### 5.12.3.2.4 Is Primary Key

❌ No

##### 5.12.3.2.5 Size

0

##### 5.12.3.2.6 Is Unique

❌ No

##### 5.12.3.2.7 Constraints

*No items available*

##### 5.12.3.2.8 Precision

0

##### 5.12.3.2.9 Scale

0

##### 5.12.3.2.10 Is Foreign Key

✅ Yes

#### 5.12.3.3.0 submittedByVendorId

##### 5.12.3.3.1 Name

submittedByVendorId

##### 5.12.3.3.2 Type

🔹 Guid

##### 5.12.3.3.3 Is Required

✅ Yes

##### 5.12.3.3.4 Is Primary Key

❌ No

##### 5.12.3.3.5 Size

0

##### 5.12.3.3.6 Is Unique

❌ No

##### 5.12.3.3.7 Constraints

*No items available*

##### 5.12.3.3.8 Precision

0

##### 5.12.3.3.9 Scale

0

##### 5.12.3.3.10 Is Foreign Key

✅ Yes

#### 5.12.3.4.0 questionText

##### 5.12.3.4.1 Name

questionText

##### 5.12.3.4.2 Type

🔹 TEXT

##### 5.12.3.4.3 Is Required

✅ Yes

##### 5.12.3.4.4 Is Primary Key

❌ No

##### 5.12.3.4.5 Size

0

##### 5.12.3.4.6 Is Unique

❌ No

##### 5.12.3.4.7 Constraints

*No items available*

##### 5.12.3.4.8 Precision

0

##### 5.12.3.4.9 Scale

0

##### 5.12.3.4.10 Is Foreign Key

❌ No

#### 5.12.3.5.0 answerText

##### 5.12.3.5.1 Name

answerText

##### 5.12.3.5.2 Type

🔹 TEXT

##### 5.12.3.5.3 Is Required

❌ No

##### 5.12.3.5.4 Is Primary Key

❌ No

##### 5.12.3.5.5 Size

0

##### 5.12.3.5.6 Is Unique

❌ No

##### 5.12.3.5.7 Constraints

*No items available*

##### 5.12.3.5.8 Precision

0

##### 5.12.3.5.9 Scale

0

##### 5.12.3.5.10 Is Foreign Key

❌ No

#### 5.12.3.6.0 answeredById

##### 5.12.3.6.1 Name

answeredById

##### 5.12.3.6.2 Type

🔹 Guid

##### 5.12.3.6.3 Is Required

❌ No

##### 5.12.3.6.4 Is Primary Key

❌ No

##### 5.12.3.6.5 Size

0

##### 5.12.3.6.6 Is Unique

❌ No

##### 5.12.3.6.7 Constraints

*No items available*

##### 5.12.3.6.8 Precision

0

##### 5.12.3.6.9 Scale

0

##### 5.12.3.6.10 Is Foreign Key

✅ Yes

#### 5.12.3.7.0 createdAt

##### 5.12.3.7.1 Name

createdAt

##### 5.12.3.7.2 Type

🔹 DateTimeOffset

##### 5.12.3.7.3 Is Required

✅ Yes

##### 5.12.3.7.4 Is Primary Key

❌ No

##### 5.12.3.7.5 Size

0

##### 5.12.3.7.6 Is Unique

❌ No

##### 5.12.3.7.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.12.3.7.8 Precision

0

##### 5.12.3.7.9 Scale

0

##### 5.12.3.7.10 Is Foreign Key

❌ No

#### 5.12.3.8.0 answeredAt

##### 5.12.3.8.1 Name

answeredAt

##### 5.12.3.8.2 Type

🔹 DateTimeOffset

##### 5.12.3.8.3 Is Required

❌ No

##### 5.12.3.8.4 Is Primary Key

❌ No

##### 5.12.3.8.5 Size

0

##### 5.12.3.8.6 Is Unique

❌ No

##### 5.12.3.8.7 Constraints

*No items available*

##### 5.12.3.8.8 Precision

0

##### 5.12.3.8.9 Scale

0

##### 5.12.3.8.10 Is Foreign Key

❌ No

### 5.12.4.0.0 Primary Keys

- proposalQuestionId

### 5.12.5.0.0 Unique Constraints

*No items available*

### 5.12.6.0.0 Indexes

- {'name': 'IX_ProposalQuestion_ProjectId', 'columns': ['projectId'], 'type': 'BTree'}

## 5.13.0.0.0 Invoice

### 5.13.1.0.0 Name

Invoice

### 5.13.2.0.0 Description

A financial invoice sent to a client for a project. REQ-FUN-004.

### 5.13.3.0.0 Attributes

#### 5.13.3.1.0 invoiceId

##### 5.13.3.1.1 Name

invoiceId

##### 5.13.3.1.2 Type

🔹 Guid

##### 5.13.3.1.3 Is Required

✅ Yes

##### 5.13.3.1.4 Is Primary Key

✅ Yes

##### 5.13.3.1.5 Size

0

##### 5.13.3.1.6 Is Unique

✅ Yes

##### 5.13.3.1.7 Constraints

*No items available*

##### 5.13.3.1.8 Precision

0

##### 5.13.3.1.9 Scale

0

##### 5.13.3.1.10 Is Foreign Key

❌ No

#### 5.13.3.2.0 projectId

##### 5.13.3.2.1 Name

projectId

##### 5.13.3.2.2 Type

🔹 Guid

##### 5.13.3.2.3 Is Required

✅ Yes

##### 5.13.3.2.4 Is Primary Key

❌ No

##### 5.13.3.2.5 Size

0

##### 5.13.3.2.6 Is Unique

❌ No

##### 5.13.3.2.7 Constraints

*No items available*

##### 5.13.3.2.8 Precision

0

##### 5.13.3.2.9 Scale

0

##### 5.13.3.2.10 Is Foreign Key

✅ Yes

#### 5.13.3.3.0 amount

##### 5.13.3.3.1 Name

amount

##### 5.13.3.3.2 Type

🔹 DECIMAL

##### 5.13.3.3.3 Is Required

✅ Yes

##### 5.13.3.3.4 Is Primary Key

❌ No

##### 5.13.3.3.5 Size

0

##### 5.13.3.3.6 Is Unique

❌ No

##### 5.13.3.3.7 Constraints

*No items available*

##### 5.13.3.3.8 Precision

12

##### 5.13.3.3.9 Scale

2

##### 5.13.3.3.10 Is Foreign Key

❌ No

#### 5.13.3.4.0 currency

##### 5.13.3.4.1 Name

currency

##### 5.13.3.4.2 Type

🔹 VARCHAR

##### 5.13.3.4.3 Is Required

✅ Yes

##### 5.13.3.4.4 Is Primary Key

❌ No

##### 5.13.3.4.5 Size

3

##### 5.13.3.4.6 Is Unique

❌ No

##### 5.13.3.4.7 Constraints

*No items available*

##### 5.13.3.4.8 Precision

0

##### 5.13.3.4.9 Scale

0

##### 5.13.3.4.10 Is Foreign Key

❌ No

#### 5.13.3.5.0 status

##### 5.13.3.5.1 Name

status

##### 5.13.3.5.2 Type

🔹 VARCHAR

##### 5.13.3.5.3 Is Required

✅ Yes

##### 5.13.3.5.4 Is Primary Key

❌ No

##### 5.13.3.5.5 Size

50

##### 5.13.3.5.6 Is Unique

❌ No

##### 5.13.3.5.7 Constraints

- CHECK (status IN ('DRAFT', 'SENT', 'PAID', 'OVERDUE', 'VOID'))
- DEFAULT 'DRAFT'

##### 5.13.3.5.8 Precision

0

##### 5.13.3.5.9 Scale

0

##### 5.13.3.5.10 Is Foreign Key

❌ No

#### 5.13.3.6.0 issuedDate

##### 5.13.3.6.1 Name

issuedDate

##### 5.13.3.6.2 Type

🔹 DateTimeOffset

##### 5.13.3.6.3 Is Required

✅ Yes

##### 5.13.3.6.4 Is Primary Key

❌ No

##### 5.13.3.6.5 Size

0

##### 5.13.3.6.6 Is Unique

❌ No

##### 5.13.3.6.7 Constraints

*No items available*

##### 5.13.3.6.8 Precision

0

##### 5.13.3.6.9 Scale

0

##### 5.13.3.6.10 Is Foreign Key

❌ No

#### 5.13.3.7.0 dueDate

##### 5.13.3.7.1 Name

dueDate

##### 5.13.3.7.2 Type

🔹 DateTimeOffset

##### 5.13.3.7.3 Is Required

✅ Yes

##### 5.13.3.7.4 Is Primary Key

❌ No

##### 5.13.3.7.5 Size

0

##### 5.13.3.7.6 Is Unique

❌ No

##### 5.13.3.7.7 Constraints

*No items available*

##### 5.13.3.7.8 Precision

0

##### 5.13.3.7.9 Scale

0

##### 5.13.3.7.10 Is Foreign Key

❌ No

### 5.13.4.0.0 Primary Keys

- invoiceId

### 5.13.5.0.0 Unique Constraints

*No items available*

### 5.13.6.0.0 Indexes

#### 5.13.6.1.0 IX_Invoice_ProjectId

##### 5.13.6.1.1 Name

IX_Invoice_ProjectId

##### 5.13.6.1.2 Columns

- projectId

##### 5.13.6.1.3 Type

🔹 BTree

#### 5.13.6.2.0 IX_Invoice_Status_DueDate

##### 5.13.6.2.1 Name

IX_Invoice_Status_DueDate

##### 5.13.6.2.2 Columns

- status
- dueDate

##### 5.13.6.2.3 Type

🔹 BTree

## 5.14.0.0.0 Payout

### 5.14.1.0.0 Name

Payout

### 5.14.2.0.0 Description

A financial payout made to a vendor for their work on a project. REQ-FUN-004.

### 5.14.3.0.0 Attributes

#### 5.14.3.1.0 payoutId

##### 5.14.3.1.1 Name

payoutId

##### 5.14.3.1.2 Type

🔹 Guid

##### 5.14.3.1.3 Is Required

✅ Yes

##### 5.14.3.1.4 Is Primary Key

✅ Yes

##### 5.14.3.1.5 Size

0

##### 5.14.3.1.6 Is Unique

✅ Yes

##### 5.14.3.1.7 Constraints

*No items available*

##### 5.14.3.1.8 Precision

0

##### 5.14.3.1.9 Scale

0

##### 5.14.3.1.10 Is Foreign Key

❌ No

#### 5.14.3.2.0 projectId

##### 5.14.3.2.1 Name

projectId

##### 5.14.3.2.2 Type

🔹 Guid

##### 5.14.3.2.3 Is Required

✅ Yes

##### 5.14.3.2.4 Is Primary Key

❌ No

##### 5.14.3.2.5 Size

0

##### 5.14.3.2.6 Is Unique

❌ No

##### 5.14.3.2.7 Constraints

*No items available*

##### 5.14.3.2.8 Precision

0

##### 5.14.3.2.9 Scale

0

##### 5.14.3.2.10 Is Foreign Key

✅ Yes

#### 5.14.3.3.0 vendorId

##### 5.14.3.3.1 Name

vendorId

##### 5.14.3.3.2 Type

🔹 Guid

##### 5.14.3.3.3 Is Required

✅ Yes

##### 5.14.3.3.4 Is Primary Key

❌ No

##### 5.14.3.3.5 Size

0

##### 5.14.3.3.6 Is Unique

❌ No

##### 5.14.3.3.7 Constraints

*No items available*

##### 5.14.3.3.8 Precision

0

##### 5.14.3.3.9 Scale

0

##### 5.14.3.3.10 Is Foreign Key

✅ Yes

#### 5.14.3.4.0 amount

##### 5.14.3.4.1 Name

amount

##### 5.14.3.4.2 Type

🔹 DECIMAL

##### 5.14.3.4.3 Is Required

✅ Yes

##### 5.14.3.4.4 Is Primary Key

❌ No

##### 5.14.3.4.5 Size

0

##### 5.14.3.4.6 Is Unique

❌ No

##### 5.14.3.4.7 Constraints

*No items available*

##### 5.14.3.4.8 Precision

12

##### 5.14.3.4.9 Scale

2

##### 5.14.3.4.10 Is Foreign Key

❌ No

#### 5.14.3.5.0 currency

##### 5.14.3.5.1 Name

currency

##### 5.14.3.5.2 Type

🔹 VARCHAR

##### 5.14.3.5.3 Is Required

✅ Yes

##### 5.14.3.5.4 Is Primary Key

❌ No

##### 5.14.3.5.5 Size

3

##### 5.14.3.5.6 Is Unique

❌ No

##### 5.14.3.5.7 Constraints

*No items available*

##### 5.14.3.5.8 Precision

0

##### 5.14.3.5.9 Scale

0

##### 5.14.3.5.10 Is Foreign Key

❌ No

#### 5.14.3.6.0 status

##### 5.14.3.6.1 Name

status

##### 5.14.3.6.2 Type

🔹 VARCHAR

##### 5.14.3.6.3 Is Required

✅ Yes

##### 5.14.3.6.4 Is Primary Key

❌ No

##### 5.14.3.6.5 Size

50

##### 5.14.3.6.6 Is Unique

❌ No

##### 5.14.3.6.7 Constraints

- CHECK (status IN ('PENDING', 'PAID', 'FAILED'))
- DEFAULT 'PENDING'

##### 5.14.3.6.8 Precision

0

##### 5.14.3.6.9 Scale

0

##### 5.14.3.6.10 Is Foreign Key

❌ No

#### 5.14.3.7.0 paidDate

##### 5.14.3.7.1 Name

paidDate

##### 5.14.3.7.2 Type

🔹 DateTimeOffset

##### 5.14.3.7.3 Is Required

❌ No

##### 5.14.3.7.4 Is Primary Key

❌ No

##### 5.14.3.7.5 Size

0

##### 5.14.3.7.6 Is Unique

❌ No

##### 5.14.3.7.7 Constraints

*No items available*

##### 5.14.3.7.8 Precision

0

##### 5.14.3.7.9 Scale

0

##### 5.14.3.7.10 Is Foreign Key

❌ No

### 5.14.4.0.0 Primary Keys

- payoutId

### 5.14.5.0.0 Unique Constraints

*No items available*

### 5.14.6.0.0 Indexes

#### 5.14.6.1.0 IX_Payout_ProjectId

##### 5.14.6.1.1 Name

IX_Payout_ProjectId

##### 5.14.6.1.2 Columns

- projectId

##### 5.14.6.1.3 Type

🔹 BTree

#### 5.14.6.2.0 IX_Payout_VendorId

##### 5.14.6.2.1 Name

IX_Payout_VendorId

##### 5.14.6.2.2 Columns

- vendorId

##### 5.14.6.2.3 Type

🔹 BTree

#### 5.14.6.3.0 IX_Payout_Status

##### 5.14.6.3.1 Name

IX_Payout_Status

##### 5.14.6.3.2 Columns

- status

##### 5.14.6.3.3 Type

🔹 BTree

## 5.15.0.0.0 Transaction

### 5.15.1.0.0 Name

Transaction

### 5.15.2.0.0 Description

Unified ledger for all financial movements (payments, payouts, fees, refunds) to ensure atomicity and provide a clear financial history. REQ-FUN-004.

### 5.15.3.0.0 Attributes

#### 5.15.3.1.0 transactionId

##### 5.15.3.1.1 Name

transactionId

##### 5.15.3.1.2 Type

🔹 Guid

##### 5.15.3.1.3 Is Required

✅ Yes

##### 5.15.3.1.4 Is Primary Key

✅ Yes

##### 5.15.3.1.5 Size

0

##### 5.15.3.1.6 Is Unique

✅ Yes

##### 5.15.3.1.7 Constraints

*No items available*

##### 5.15.3.1.8 Precision

0

##### 5.15.3.1.9 Scale

0

##### 5.15.3.1.10 Is Foreign Key

❌ No

#### 5.15.3.2.0 projectId

##### 5.15.3.2.1 Name

projectId

##### 5.15.3.2.2 Type

🔹 Guid

##### 5.15.3.2.3 Is Required

✅ Yes

##### 5.15.3.2.4 Is Primary Key

❌ No

##### 5.15.3.2.5 Size

0

##### 5.15.3.2.6 Is Unique

❌ No

##### 5.15.3.2.7 Constraints

*No items available*

##### 5.15.3.2.8 Precision

0

##### 5.15.3.2.9 Scale

0

##### 5.15.3.2.10 Is Foreign Key

✅ Yes

#### 5.15.3.3.0 invoiceId

##### 5.15.3.3.1 Name

invoiceId

##### 5.15.3.3.2 Type

🔹 Guid

##### 5.15.3.3.3 Is Required

❌ No

##### 5.15.3.3.4 Is Primary Key

❌ No

##### 5.15.3.3.5 Size

0

##### 5.15.3.3.6 Is Unique

❌ No

##### 5.15.3.3.7 Constraints

*No items available*

##### 5.15.3.3.8 Precision

0

##### 5.15.3.3.9 Scale

0

##### 5.15.3.3.10 Is Foreign Key

✅ Yes

#### 5.15.3.4.0 payoutId

##### 5.15.3.4.1 Name

payoutId

##### 5.15.3.4.2 Type

🔹 Guid

##### 5.15.3.4.3 Is Required

❌ No

##### 5.15.3.4.4 Is Primary Key

❌ No

##### 5.15.3.4.5 Size

0

##### 5.15.3.4.6 Is Unique

❌ No

##### 5.15.3.4.7 Constraints

*No items available*

##### 5.15.3.4.8 Precision

0

##### 5.15.3.4.9 Scale

0

##### 5.15.3.4.10 Is Foreign Key

✅ Yes

#### 5.15.3.5.0 paymentGatewayTransactionId

##### 5.15.3.5.1 Name

paymentGatewayTransactionId

##### 5.15.3.5.2 Type

🔹 VARCHAR

##### 5.15.3.5.3 Is Required

✅ Yes

##### 5.15.3.5.4 Is Primary Key

❌ No

##### 5.15.3.5.5 Size

255

##### 5.15.3.5.6 Is Unique

✅ Yes

##### 5.15.3.5.7 Constraints

*No items available*

##### 5.15.3.5.8 Precision

0

##### 5.15.3.5.9 Scale

0

##### 5.15.3.5.10 Is Foreign Key

❌ No

#### 5.15.3.6.0 type

##### 5.15.3.6.1 Name

type

##### 5.15.3.6.2 Type

🔹 VARCHAR

##### 5.15.3.6.3 Is Required

✅ Yes

##### 5.15.3.6.4 Is Primary Key

❌ No

##### 5.15.3.6.5 Size

50

##### 5.15.3.6.6 Is Unique

❌ No

##### 5.15.3.6.7 Constraints

- CHECK (type IN ('PAYMENT', 'PAYOUT', 'REFUND', 'PLATFORM_FEE'))

##### 5.15.3.6.8 Precision

0

##### 5.15.3.6.9 Scale

0

##### 5.15.3.6.10 Is Foreign Key

❌ No

#### 5.15.3.7.0 amount

##### 5.15.3.7.1 Name

amount

##### 5.15.3.7.2 Type

🔹 DECIMAL

##### 5.15.3.7.3 Is Required

✅ Yes

##### 5.15.3.7.4 Is Primary Key

❌ No

##### 5.15.3.7.5 Size

0

##### 5.15.3.7.6 Is Unique

❌ No

##### 5.15.3.7.7 Constraints

*No items available*

##### 5.15.3.7.8 Precision

14

##### 5.15.3.7.9 Scale

2

##### 5.15.3.7.10 Is Foreign Key

❌ No

#### 5.15.3.8.0 currency

##### 5.15.3.8.1 Name

currency

##### 5.15.3.8.2 Type

🔹 VARCHAR

##### 5.15.3.8.3 Is Required

✅ Yes

##### 5.15.3.8.4 Is Primary Key

❌ No

##### 5.15.3.8.5 Size

3

##### 5.15.3.8.6 Is Unique

❌ No

##### 5.15.3.8.7 Constraints

*No items available*

##### 5.15.3.8.8 Precision

0

##### 5.15.3.8.9 Scale

0

##### 5.15.3.8.10 Is Foreign Key

❌ No

#### 5.15.3.9.0 status

##### 5.15.3.9.1 Name

status

##### 5.15.3.9.2 Type

🔹 VARCHAR

##### 5.15.3.9.3 Is Required

✅ Yes

##### 5.15.3.9.4 Is Primary Key

❌ No

##### 5.15.3.9.5 Size

50

##### 5.15.3.9.6 Is Unique

❌ No

##### 5.15.3.9.7 Constraints

- CHECK (status IN ('PENDING', 'COMPLETED', 'FAILED'))

##### 5.15.3.9.8 Precision

0

##### 5.15.3.9.9 Scale

0

##### 5.15.3.9.10 Is Foreign Key

❌ No

#### 5.15.3.10.0 processedAt

##### 5.15.3.10.1 Name

processedAt

##### 5.15.3.10.2 Type

🔹 DateTimeOffset

##### 5.15.3.10.3 Is Required

✅ Yes

##### 5.15.3.10.4 Is Primary Key

❌ No

##### 5.15.3.10.5 Size

0

##### 5.15.3.10.6 Is Unique

❌ No

##### 5.15.3.10.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.15.3.10.8 Precision

0

##### 5.15.3.10.9 Scale

0

##### 5.15.3.10.10 Is Foreign Key

❌ No

### 5.15.4.0.0 Primary Keys

- transactionId

### 5.15.5.0.0 Unique Constraints

- {'name': 'UC_Transaction_GatewayId', 'columns': ['paymentGatewayTransactionId']}

### 5.15.6.0.0 Indexes

#### 5.15.6.1.0 IX_Transaction_ProjectId

##### 5.15.6.1.1 Name

IX_Transaction_ProjectId

##### 5.15.6.1.2 Columns

- projectId

##### 5.15.6.1.3 Type

🔹 BTree

#### 5.15.6.2.0 IX_Transaction_Type_Status_Date

##### 5.15.6.2.1 Name

IX_Transaction_Type_Status_Date

##### 5.15.6.2.2 Columns

- type
- status
- processedAt

##### 5.15.6.2.3 Type

🔹 BTree

## 5.16.0.0.0 SystemSetting

### 5.16.1.0.0 Name

SystemSetting

### 5.16.2.0.0 Description

Global system configuration values, such as default margin fees. REQ-FUN-004.

### 5.16.3.0.0 Attributes

#### 5.16.3.1.0 settingKey

##### 5.16.3.1.1 Name

settingKey

##### 5.16.3.1.2 Type

🔹 VARCHAR

##### 5.16.3.1.3 Is Required

✅ Yes

##### 5.16.3.1.4 Is Primary Key

✅ Yes

##### 5.16.3.1.5 Size

100

##### 5.16.3.1.6 Is Unique

✅ Yes

##### 5.16.3.1.7 Constraints

*No items available*

##### 5.16.3.1.8 Precision

0

##### 5.16.3.1.9 Scale

0

##### 5.16.3.1.10 Is Foreign Key

❌ No

#### 5.16.3.2.0 settingValue

##### 5.16.3.2.1 Name

settingValue

##### 5.16.3.2.2 Type

🔹 JSONB

##### 5.16.3.2.3 Is Required

✅ Yes

##### 5.16.3.2.4 Is Primary Key

❌ No

##### 5.16.3.2.5 Size

0

##### 5.16.3.2.6 Is Unique

❌ No

##### 5.16.3.2.7 Constraints

*No items available*

##### 5.16.3.2.8 Precision

0

##### 5.16.3.2.9 Scale

0

##### 5.16.3.2.10 Is Foreign Key

❌ No

#### 5.16.3.3.0 description

##### 5.16.3.3.1 Name

description

##### 5.16.3.3.2 Type

🔹 TEXT

##### 5.16.3.3.3 Is Required

❌ No

##### 5.16.3.3.4 Is Primary Key

❌ No

##### 5.16.3.3.5 Size

0

##### 5.16.3.3.6 Is Unique

❌ No

##### 5.16.3.3.7 Constraints

*No items available*

##### 5.16.3.3.8 Precision

0

##### 5.16.3.3.9 Scale

0

##### 5.16.3.3.10 Is Foreign Key

❌ No

### 5.16.4.0.0 Primary Keys

- settingKey

### 5.16.5.0.0 Unique Constraints

*No items available*

### 5.16.6.0.0 Indexes

*No items available*

## 5.17.0.0.0 Notification

### 5.17.1.0.0 Name

Notification

### 5.17.2.0.0 Description

Notification events for users, supporting in-app, email, and webhook channels. REQ-FUN-005.

### 5.17.3.0.0 Attributes

#### 5.17.3.1.0 notificationId

##### 5.17.3.1.1 Name

notificationId

##### 5.17.3.1.2 Type

🔹 Guid

##### 5.17.3.1.3 Is Required

✅ Yes

##### 5.17.3.1.4 Is Primary Key

✅ Yes

##### 5.17.3.1.5 Size

0

##### 5.17.3.1.6 Is Unique

✅ Yes

##### 5.17.3.1.7 Constraints

*No items available*

##### 5.17.3.1.8 Precision

0

##### 5.17.3.1.9 Scale

0

##### 5.17.3.1.10 Is Foreign Key

❌ No

#### 5.17.3.2.0 recipientUserId

##### 5.17.3.2.1 Name

recipientUserId

##### 5.17.3.2.2 Type

🔹 Guid

##### 5.17.3.2.3 Is Required

✅ Yes

##### 5.17.3.2.4 Is Primary Key

❌ No

##### 5.17.3.2.5 Size

0

##### 5.17.3.2.6 Is Unique

❌ No

##### 5.17.3.2.7 Constraints

*No items available*

##### 5.17.3.2.8 Precision

0

##### 5.17.3.2.9 Scale

0

##### 5.17.3.2.10 Is Foreign Key

✅ Yes

#### 5.17.3.3.0 eventType

##### 5.17.3.3.1 Name

eventType

##### 5.17.3.3.2 Type

🔹 VARCHAR

##### 5.17.3.3.3 Is Required

✅ Yes

##### 5.17.3.3.4 Is Primary Key

❌ No

##### 5.17.3.3.5 Size

100

##### 5.17.3.3.6 Is Unique

❌ No

##### 5.17.3.3.7 Constraints

*No items available*

##### 5.17.3.3.8 Precision

0

##### 5.17.3.3.9 Scale

0

##### 5.17.3.3.10 Is Foreign Key

❌ No

#### 5.17.3.4.0 content

##### 5.17.3.4.1 Name

content

##### 5.17.3.4.2 Type

🔹 TEXT

##### 5.17.3.4.3 Is Required

✅ Yes

##### 5.17.3.4.4 Is Primary Key

❌ No

##### 5.17.3.4.5 Size

0

##### 5.17.3.4.6 Is Unique

❌ No

##### 5.17.3.4.7 Constraints

*No items available*

##### 5.17.3.4.8 Precision

0

##### 5.17.3.4.9 Scale

0

##### 5.17.3.4.10 Is Foreign Key

❌ No

#### 5.17.3.5.0 relatedEntityName

##### 5.17.3.5.1 Name

relatedEntityName

##### 5.17.3.5.2 Type

🔹 VARCHAR

##### 5.17.3.5.3 Is Required

❌ No

##### 5.17.3.5.4 Is Primary Key

❌ No

##### 5.17.3.5.5 Size

100

##### 5.17.3.5.6 Is Unique

❌ No

##### 5.17.3.5.7 Constraints

*No items available*

##### 5.17.3.5.8 Precision

0

##### 5.17.3.5.9 Scale

0

##### 5.17.3.5.10 Is Foreign Key

❌ No

#### 5.17.3.6.0 relatedEntityId

##### 5.17.3.6.1 Name

relatedEntityId

##### 5.17.3.6.2 Type

🔹 Guid

##### 5.17.3.6.3 Is Required

❌ No

##### 5.17.3.6.4 Is Primary Key

❌ No

##### 5.17.3.6.5 Size

0

##### 5.17.3.6.6 Is Unique

❌ No

##### 5.17.3.6.7 Constraints

*No items available*

##### 5.17.3.6.8 Precision

0

##### 5.17.3.6.9 Scale

0

##### 5.17.3.6.10 Is Foreign Key

❌ No

#### 5.17.3.7.0 isRead

##### 5.17.3.7.1 Name

isRead

##### 5.17.3.7.2 Type

🔹 BOOLEAN

##### 5.17.3.7.3 Is Required

✅ Yes

##### 5.17.3.7.4 Is Primary Key

❌ No

##### 5.17.3.7.5 Size

0

##### 5.17.3.7.6 Is Unique

❌ No

##### 5.17.3.7.7 Constraints

- DEFAULT false

##### 5.17.3.7.8 Precision

0

##### 5.17.3.7.9 Scale

0

##### 5.17.3.7.10 Is Foreign Key

❌ No

#### 5.17.3.8.0 readAt

##### 5.17.3.8.1 Name

readAt

##### 5.17.3.8.2 Type

🔹 DateTimeOffset

##### 5.17.3.8.3 Is Required

❌ No

##### 5.17.3.8.4 Is Primary Key

❌ No

##### 5.17.3.8.5 Size

0

##### 5.17.3.8.6 Is Unique

❌ No

##### 5.17.3.8.7 Constraints

*No items available*

##### 5.17.3.8.8 Precision

0

##### 5.17.3.8.9 Scale

0

##### 5.17.3.8.10 Is Foreign Key

❌ No

#### 5.17.3.9.0 createdAt

##### 5.17.3.9.1 Name

createdAt

##### 5.17.3.9.2 Type

🔹 DateTimeOffset

##### 5.17.3.9.3 Is Required

✅ Yes

##### 5.17.3.9.4 Is Primary Key

❌ No

##### 5.17.3.9.5 Size

0

##### 5.17.3.9.6 Is Unique

❌ No

##### 5.17.3.9.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.17.3.9.8 Precision

0

##### 5.17.3.9.9 Scale

0

##### 5.17.3.9.10 Is Foreign Key

❌ No

### 5.17.4.0.0 Primary Keys

- notificationId

### 5.17.5.0.0 Unique Constraints

*No items available*

### 5.17.6.0.0 Indexes

#### 5.17.6.1.0 IX_Notification_Recipient_Unread

##### 5.17.6.1.1 Name

IX_Notification_Recipient_Unread

##### 5.17.6.1.2 Columns

- recipientUserId
- isRead

##### 5.17.6.1.3 Type

🔹 BTree

#### 5.17.6.2.0 IX_Notification_Recipient_CreatedAt

##### 5.17.6.2.1 Name

IX_Notification_Recipient_CreatedAt

##### 5.17.6.2.2 Columns

- recipientUserId
- createdAt

##### 5.17.6.2.3 Type

🔹 BTree

## 5.18.0.0.0 WebhookEndpoint

### 5.18.1.0.0 Name

WebhookEndpoint

### 5.18.2.0.0 Description

Configured webhook endpoints for integrations like Slack. REQ-FUN-005.

### 5.18.3.0.0 Attributes

#### 5.18.3.1.0 webhookEndpointId

##### 5.18.3.1.1 Name

webhookEndpointId

##### 5.18.3.1.2 Type

🔹 Guid

##### 5.18.3.1.3 Is Required

✅ Yes

##### 5.18.3.1.4 Is Primary Key

✅ Yes

##### 5.18.3.1.5 Size

0

##### 5.18.3.1.6 Is Unique

✅ Yes

##### 5.18.3.1.7 Constraints

*No items available*

##### 5.18.3.1.8 Precision

0

##### 5.18.3.1.9 Scale

0

##### 5.18.3.1.10 Is Foreign Key

❌ No

#### 5.18.3.2.0 url

##### 5.18.3.2.1 Name

url

##### 5.18.3.2.2 Type

🔹 VARCHAR

##### 5.18.3.2.3 Is Required

✅ Yes

##### 5.18.3.2.4 Is Primary Key

❌ No

##### 5.18.3.2.5 Size

2,048

##### 5.18.3.2.6 Is Unique

✅ Yes

##### 5.18.3.2.7 Constraints

*No items available*

##### 5.18.3.2.8 Precision

0

##### 5.18.3.2.9 Scale

0

##### 5.18.3.2.10 Is Foreign Key

❌ No

#### 5.18.3.3.0 description

##### 5.18.3.3.1 Name

description

##### 5.18.3.3.2 Type

🔹 TEXT

##### 5.18.3.3.3 Is Required

❌ No

##### 5.18.3.3.4 Is Primary Key

❌ No

##### 5.18.3.3.5 Size

0

##### 5.18.3.3.6 Is Unique

❌ No

##### 5.18.3.3.7 Constraints

*No items available*

##### 5.18.3.3.8 Precision

0

##### 5.18.3.3.9 Scale

0

##### 5.18.3.3.10 Is Foreign Key

❌ No

#### 5.18.3.4.0 secretToken

##### 5.18.3.4.1 Name

secretToken

##### 5.18.3.4.2 Type

🔹 VARCHAR

##### 5.18.3.4.3 Is Required

❌ No

##### 5.18.3.4.4 Is Primary Key

❌ No

##### 5.18.3.4.5 Size

255

##### 5.18.3.4.6 Is Unique

❌ No

##### 5.18.3.4.7 Constraints

*No items available*

##### 5.18.3.4.8 Precision

0

##### 5.18.3.4.9 Scale

0

##### 5.18.3.4.10 Is Foreign Key

❌ No

#### 5.18.3.5.0 isActive

##### 5.18.3.5.1 Name

isActive

##### 5.18.3.5.2 Type

🔹 BOOLEAN

##### 5.18.3.5.3 Is Required

✅ Yes

##### 5.18.3.5.4 Is Primary Key

❌ No

##### 5.18.3.5.5 Size

0

##### 5.18.3.5.6 Is Unique

❌ No

##### 5.18.3.5.7 Constraints

- DEFAULT true

##### 5.18.3.5.8 Precision

0

##### 5.18.3.5.9 Scale

0

##### 5.18.3.5.10 Is Foreign Key

❌ No

#### 5.18.3.6.0 createdAt

##### 5.18.3.6.1 Name

createdAt

##### 5.18.3.6.2 Type

🔹 DateTimeOffset

##### 5.18.3.6.3 Is Required

✅ Yes

##### 5.18.3.6.4 Is Primary Key

❌ No

##### 5.18.3.6.5 Size

0

##### 5.18.3.6.6 Is Unique

❌ No

##### 5.18.3.6.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.18.3.6.8 Precision

0

##### 5.18.3.6.9 Scale

0

##### 5.18.3.6.10 Is Foreign Key

❌ No

#### 5.18.3.7.0 updatedAt

##### 5.18.3.7.1 Name

updatedAt

##### 5.18.3.7.2 Type

🔹 DateTimeOffset

##### 5.18.3.7.3 Is Required

✅ Yes

##### 5.18.3.7.4 Is Primary Key

❌ No

##### 5.18.3.7.5 Size

0

##### 5.18.3.7.6 Is Unique

❌ No

##### 5.18.3.7.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.18.3.7.8 Precision

0

##### 5.18.3.7.9 Scale

0

##### 5.18.3.7.10 Is Foreign Key

❌ No

### 5.18.4.0.0 Primary Keys

- webhookEndpointId

### 5.18.5.0.0 Unique Constraints

- {'name': 'UC_WebhookEndpoint_Url', 'columns': ['url']}

### 5.18.6.0.0 Indexes

- {'name': 'IX_WebhookEndpoint_IsActive', 'columns': ['isActive'], 'type': 'BTree'}

## 5.19.0.0.0 WebhookEventLog

### 5.19.1.0.0 Name

WebhookEventLog

### 5.19.2.0.0 Description

Stores processed webhook event IDs from gateways like Stripe to ensure idempotency. REQ-INT-002.

### 5.19.3.0.0 Attributes

#### 5.19.3.1.0 eventId

##### 5.19.3.1.1 Name

eventId

##### 5.19.3.1.2 Type

🔹 VARCHAR

##### 5.19.3.1.3 Is Required

✅ Yes

##### 5.19.3.1.4 Is Primary Key

✅ Yes

##### 5.19.3.1.5 Size

255

##### 5.19.3.1.6 Is Unique

✅ Yes

##### 5.19.3.1.7 Constraints

*No items available*

##### 5.19.3.1.8 Precision

0

##### 5.19.3.1.9 Scale

0

##### 5.19.3.1.10 Is Foreign Key

❌ No

#### 5.19.3.2.0 source

##### 5.19.3.2.1 Name

source

##### 5.19.3.2.2 Type

🔹 VARCHAR

##### 5.19.3.2.3 Is Required

✅ Yes

##### 5.19.3.2.4 Is Primary Key

✅ Yes

##### 5.19.3.2.5 Size

50

##### 5.19.3.2.6 Is Unique

❌ No

##### 5.19.3.2.7 Constraints

*No items available*

##### 5.19.3.2.8 Precision

0

##### 5.19.3.2.9 Scale

0

##### 5.19.3.2.10 Is Foreign Key

❌ No

#### 5.19.3.3.0 isProcessed

##### 5.19.3.3.1 Name

isProcessed

##### 5.19.3.3.2 Type

🔹 BOOLEAN

##### 5.19.3.3.3 Is Required

✅ Yes

##### 5.19.3.3.4 Is Primary Key

❌ No

##### 5.19.3.3.5 Size

0

##### 5.19.3.3.6 Is Unique

❌ No

##### 5.19.3.3.7 Constraints

- DEFAULT true

##### 5.19.3.3.8 Precision

0

##### 5.19.3.3.9 Scale

0

##### 5.19.3.3.10 Is Foreign Key

❌ No

#### 5.19.3.4.0 receivedAt

##### 5.19.3.4.1 Name

receivedAt

##### 5.19.3.4.2 Type

🔹 DateTimeOffset

##### 5.19.3.4.3 Is Required

✅ Yes

##### 5.19.3.4.4 Is Primary Key

❌ No

##### 5.19.3.4.5 Size

0

##### 5.19.3.4.6 Is Unique

❌ No

##### 5.19.3.4.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.19.3.4.8 Precision

0

##### 5.19.3.4.9 Scale

0

##### 5.19.3.4.10 Is Foreign Key

❌ No

### 5.19.4.0.0 Primary Keys

- eventId
- source

### 5.19.5.0.0 Unique Constraints

*No items available*

### 5.19.6.0.0 Indexes

*No items available*

## 5.20.0.0.0 DashboardMetrics

### 5.20.1.0.0 Name

DashboardMetrics

### 5.20.2.0.0 Description

Pre-aggregated, denormalized data for dashboards to ensure fast loads. Populated by a background job. REQ-FUNC-024.

### 5.20.3.0.0 Attributes

#### 5.20.3.1.0 metricKey

##### 5.20.3.1.1 Name

metricKey

##### 5.20.3.1.2 Type

🔹 VARCHAR

##### 5.20.3.1.3 Is Required

✅ Yes

##### 5.20.3.1.4 Is Primary Key

✅ Yes

##### 5.20.3.1.5 Size

50

##### 5.20.3.1.6 Is Unique

✅ Yes

##### 5.20.3.1.7 Constraints

- DEFAULT 'main'

##### 5.20.3.1.8 Precision

0

##### 5.20.3.1.9 Scale

0

##### 5.20.3.1.10 Is Foreign Key

❌ No

#### 5.20.3.2.0 metricsData

##### 5.20.3.2.1 Name

metricsData

##### 5.20.3.2.2 Type

🔹 JSONB

##### 5.20.3.2.3 Is Required

✅ Yes

##### 5.20.3.2.4 Is Primary Key

❌ No

##### 5.20.3.2.5 Size

0

##### 5.20.3.2.6 Is Unique

❌ No

##### 5.20.3.2.7 Constraints

*No items available*

##### 5.20.3.2.8 Precision

0

##### 5.20.3.2.9 Scale

0

##### 5.20.3.2.10 Is Foreign Key

❌ No

#### 5.20.3.3.0 lastUpdatedAt

##### 5.20.3.3.1 Name

lastUpdatedAt

##### 5.20.3.3.2 Type

🔹 DateTimeOffset

##### 5.20.3.3.3 Is Required

✅ Yes

##### 5.20.3.3.4 Is Primary Key

❌ No

##### 5.20.3.3.5 Size

0

##### 5.20.3.3.6 Is Unique

❌ No

##### 5.20.3.3.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.20.3.3.8 Precision

0

##### 5.20.3.3.9 Scale

0

##### 5.20.3.3.10 Is Foreign Key

❌ No

### 5.20.4.0.0 Primary Keys

- metricKey

### 5.20.5.0.0 Unique Constraints

*No items available*

### 5.20.6.0.0 Indexes

*No items available*

