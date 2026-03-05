# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-070 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Finance Manager Configures Tax Settings |
| As A User Story | As a Finance Manager, I want to create, view, upda... |
| User Persona | Finance Manager. This user is responsible for the ... |
| Business Value | Ensures financial compliance with tax regulations,... |
| Functional Area | Financial Management & Configuration |
| Story Theme | System Configuration |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

View Tax Settings Page

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am logged in as a Finance Manager

### 3.1.5 When

I navigate to the 'Financial Configuration' > 'Tax Settings' page

### 3.1.6 Then

I can see a list of all existing tax settings with columns for 'Name', 'Rate (%)', 'Status (Active/Inactive)', and 'Default'.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Create a New Tax Setting

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am on the Tax Settings page

### 3.2.5 When

I click 'Add New Tax', enter a unique name 'UK VAT', a rate of '20', and click 'Save'

### 3.2.6 Then

The new 'UK VAT' tax setting appears in the list with a 20% rate and an 'Active' status, and a success notification is displayed.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Update an Existing Tax Setting

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

A tax setting 'UK VAT' with a rate of 20% exists

### 3.3.5 When

I edit the 'UK VAT' setting, change the rate to '21', and click 'Save'

### 3.3.6 Then

The tax setting in the list is updated to show a 21% rate, and a success notification is displayed.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Set a Default Tax Setting

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

At least two active tax settings exist, and 'UK VAT' is not the default

### 3.4.5 When

I select 'UK VAT' and mark it as the 'System Default'

### 3.4.6 Then

The 'UK VAT' setting is now clearly indicated as the default, and any previously default tax is no longer marked as such.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Deactivate a Tax Setting

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

An active tax setting 'Old Tax' exists and is not the default

### 3.5.5 When

I deactivate the 'Old Tax' setting

### 3.5.6 Then

The status of 'Old Tax' changes to 'Inactive' in the list, and it cannot be selected for new invoices.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Attempt to Create a Tax Setting with Invalid Data

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

I am on the 'Add New Tax' form

### 3.6.5 When

I enter a non-numeric value like 'abc' for the rate and try to save

### 3.6.6 Then

A validation error message is displayed next to the rate field, and the form is not submitted.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Attempt to Create a Tax Setting with a Duplicate Name

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

A tax setting named 'UK VAT' already exists

### 3.7.5 When

I attempt to create another tax setting with the name 'UK VAT'

### 3.7.6 Then

A validation error message is displayed stating the name must be unique, and the form is not submitted.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Attempt to Deactivate the Default Tax Setting

### 3.8.3 Scenario Type

Edge_Case

### 3.8.4 Given

The 'UK VAT' tax setting is marked as the system default

### 3.8.5 When

I attempt to deactivate it

### 3.8.6 Then

A warning message is displayed, stating 'The default tax cannot be deactivated. Please set a new default first.', and the action is prevented.

## 3.9.0 Criteria Id

### 3.9.1 Criteria Id

AC-009

### 3.9.2 Scenario

Audit Trail Logging

### 3.9.3 Scenario Type

Happy_Path

### 3.9.4 Given

I am a Finance Manager on the Tax Settings page

### 3.9.5 When

I create, update, or deactivate any tax setting

### 3.9.6 Then

A new entry is created in the audit trail logging my user ID, the action performed, the target entity (tax setting), and a before/after state snapshot of the data.

## 3.10.0 Criteria Id

### 3.10.1 Criteria Id

AC-010

### 3.10.2 Scenario

Access Control

### 3.10.3 Scenario Type

Error_Condition

### 3.10.4 Given

I am logged in as a user without the 'Finance Manager' role (e.g., Client Contact)

### 3.10.5 When

I attempt to access the Tax Settings page or its API endpoint directly

### 3.10.6 Then

I am shown an 'Access Denied' page or receive a 403 Forbidden API response.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A data table to list tax settings.
- Columns: Name, Rate (%), Status, Default indicator.
- Action buttons/icons per row: 'Edit', 'Deactivate'/'Activate'.
- A primary button: 'Add New Tax'.
- A modal or form for adding/editing a tax setting with fields for 'Name' (text input) and 'Rate' (numeric input).
- A mechanism to set a default (e.g., a radio button column or an option within the 'Edit' form).
- Confirmation dialogs for deactivation.
- Toast notifications for success/error messages.

## 4.2.0 User Interactions

- The list of tax settings should be sortable by name and rate.
- Input fields must have clear labels and validation messages for invalid input.
- The UI should provide a clear visual distinction for the default tax setting.

## 4.3.0 Display Requirements

- A warning banner should be displayed on the page if no default tax is set.
- Rates should be displayed consistently (e.g., to two decimal places).

## 4.4.0 Accessibility Needs

- All form fields must have associated labels.
- All interactive elements (buttons, links) must be keyboard-navigable and have clear focus states.
- The page must adhere to WCAG 2.1 Level AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A tax setting name must be unique within the system.

### 5.1.3 Enforcement Point

Backend API upon creation or update of a tax setting.

### 5.1.4 Violation Handling

The API returns a 409 Conflict error with a descriptive message, which the frontend displays to the user.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Only one tax setting can be marked as the system default at any time.

### 5.2.3 Enforcement Point

Backend API when a user sets a new default.

### 5.2.4 Violation Handling

The system will automatically unset the previous default when a new one is set within the same atomic transaction.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

The system default tax setting cannot be deactivated.

### 5.3.3 Enforcement Point

Backend API when a deactivation request is received.

### 5.3.4 Violation Handling

The API returns a 400 Bad Request error with a message explaining the rule.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-074

#### 6.1.1.2 Dependency Reason

The 'Finance Manager' role and its permissions must be defined to properly implement and test access control for this feature.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-086

#### 6.1.2.2 Dependency Reason

The audit trail system must be in place to log the creation, update, and deactivation of tax settings as required by AC-009.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-056

#### 6.1.3.2 Dependency Reason

This story defines the tax settings, which are consumed by the invoice creation process. This story must be completed before or in parallel with invoice generation to ensure correct calculations.

## 6.2.0.0 Technical Dependencies

- Backend API endpoints for CRUD operations on tax settings.
- Authentication and authorization middleware to enforce role-based access.
- Database schema for storing tax settings.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- API response time for loading and saving tax settings must be under 250ms (p95).
- The UI should load in under 2 seconds.

## 7.2.0.0 Security

- Access to the tax settings API and UI must be strictly limited to users with the 'Finance Manager' or 'System Administrator' roles, enforced at the API gateway and service level.
- All changes to tax settings must be logged in the immutable audit trail as per REQ-FUN-005.

## 7.3.0.0 Usability

- The interface for managing tax settings should be intuitive, requiring minimal training for a Finance Manager.
- Error messages must be clear and actionable.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must be fully functional on the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The core CRUD logic is low complexity.
- The business rules around a single default and preventing deactivation add moderate complexity.
- The primary complexity lies in the critical integration with the invoice generation service, which must reliably fetch and apply the correct tax rate.
- Ensuring atomic database transactions for updating the default tax setting is crucial.

## 8.3.0.0 Technical Risks

- Potential for incorrect integration with the invoicing module, leading to inaccurate financial calculations. This risk must be mitigated with thorough integration testing.
- Failure to properly log all changes in the audit trail could lead to compliance issues.

## 8.4.0.0 Integration Points

- Project Service / Payment Service: The service responsible for generating invoices will need to query the tax settings defined by this feature.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify all CRUD operations for tax settings.
- Test all validation and business rules (duplicate names, deactivating default).
- Crucial E2E Test: Create a tax setting, set it as default, create a project, generate an invoice, and verify the tax amount on the invoice is correct.
- Test that changing the default tax rate only affects newly created invoices, not historical ones.
- Verify role-based access control by attempting to access the feature with an unauthorized user role.

## 9.3.0.0 Test Data Needs

- User accounts with 'Finance Manager' and non-'Finance Manager' roles.
- Pre-existing tax settings for update/delete scenarios.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented with >80% coverage and passing
- Integration testing with the invoice generation module completed successfully
- User interface reviewed and approved for UX and accessibility
- Performance requirements verified
- Security requirements validated, including role-based access control
- User documentation for managing tax settings is created and published
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a prerequisite for accurate invoice generation. It should be prioritized and completed before or in the same sprint as the invoice creation story (US-056) to enable end-to-end testing of the financial workflow.

## 11.4.0.0 Release Impact

- This is a foundational feature for financial operations. The platform cannot go live with invoicing capabilities without this functionality.

