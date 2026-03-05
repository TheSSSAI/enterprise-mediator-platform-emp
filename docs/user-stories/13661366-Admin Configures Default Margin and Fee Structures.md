# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-068 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Configures Default Margin and Fee Structures |
| As A User Story | As a System Administrator, I want to access a conf... |
| User Persona | System Administrator. The Finance Manager role als... |
| Business Value | Establishes a standardized, automated method for c... |
| Functional Area | System Configuration & Financial Management |
| Story Theme | Financial Workflow Automation |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Admin navigates to and views the margin configuration UI

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a System Administrator logged into the platform

### 3.1.5 When

I navigate to the 'System Settings' > 'Financial Configuration' section

### 3.1.6 Then

I should see a dedicated area for 'Default Margin Structure' with options to select 'Percentage' or 'Fixed Fee' and input the corresponding value.

### 3.1.7 Validation Notes

Verify the navigation path exists and the UI components are rendered correctly. The current saved setting should be pre-populated in the form.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Admin successfully sets a percentage-based default margin

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am on the 'Financial Configuration' page

### 3.2.5 When

I select the 'Percentage' margin type, enter '20' into the percentage field, and click 'Save Changes'

### 3.2.6 Then

The system should display a success notification, and the new default margin of 20% should be persisted.

### 3.2.7 Validation Notes

Check the database to confirm the configuration is saved. Reloading the page should show '20' in the percentage field.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Admin successfully sets a fixed-fee default margin

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I am on the 'Financial Configuration' page

### 3.3.5 When

I select the 'Fixed Fee' margin type, enter '5000' in the amount field, select 'USD' as the currency, and click 'Save Changes'

### 3.3.6 Then

The system should display a success notification, and the new default margin of 5000 USD should be persisted.

### 3.3.7 Validation Notes

Check the database to confirm the configuration (type, amount, currency) is saved. Reloading the page should show the saved values.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Admin attempts to save an invalid percentage value

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am on the 'Financial Configuration' page with 'Percentage' type selected

### 3.4.5 When

I enter '-10' into the percentage field and click 'Save Changes'

### 3.4.6 Then

The system must prevent the save and display an inline validation error message, such as 'Value must be a positive number'.

### 3.4.7 Validation Notes

Test with negative numbers, zero, and non-numeric characters. The form should not submit.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Admin attempts to save an invalid fixed-fee value

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am on the 'Financial Configuration' page with 'Fixed Fee' type selected

### 3.5.5 When

I enter 'abc' into the amount field and click 'Save Changes'

### 3.5.6 Then

The system must prevent the save and display an inline validation error message, such as 'Amount must be a valid number'.

### 3.5.7 Validation Notes

Test with negative numbers, zero, and non-numeric characters. The form should not submit.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Unauthorized user attempts to access the configuration page

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

I am logged in as a user with the 'Vendor Contact' role

### 3.6.5 When

I attempt to navigate directly to the '/settings/financials' URL

### 3.6.6 Then

I should be redirected or shown a '403 Access Denied' page.

### 3.6.7 Validation Notes

Verify that both the frontend route and the backend API endpoint are protected by role-based access control.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Changing the margin structure is recorded in the audit trail

### 3.7.3 Scenario Type

Happy_Path

### 3.7.4 Given

The default margin is currently set to 15% and I am a System Administrator

### 3.7.5 When

I change the margin to a fixed fee of 2500 EUR and save the changes

### 3.7.6 Then

A new entry must be created in the audit trail log detailing the timestamp, my user ID, the action ('Configuration Update'), the target entity ('Default Margin'), and a snapshot of the change (e.g., before: {type: 'percentage', value: 15}, after: {type: 'fixed', value: 2500, currency: 'EUR'}).

### 3.7.7 Validation Notes

Query the audit log table/service to confirm the entry is created with the correct details.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated page or section under 'System Settings' for 'Financial Configuration'.
- Radio buttons or a dropdown to select margin type ('Percentage', 'Fixed Fee').
- A number input field for the percentage value (conditionally displayed).
- A number input field for the fixed fee amount (conditionally displayed).
- A dropdown selector for currency (ISO 4217 codes), conditionally displayed with the fixed fee field.
- A 'Save Changes' button.
- Inline validation message containers.
- A toast or banner notification for success/failure messages.

## 4.2.0 User Interactions

- Selecting a margin type should dynamically show/hide the relevant input fields.
- The 'Save Changes' button should be disabled until a change is made to the form.
- Attempting to save with invalid data should trigger clear, inline error messages next to the corresponding fields.

## 4.3.0 Display Requirements

- The page must clearly display the currently saved default margin configuration upon loading.
- The percentage symbol (%) should be displayed next to the percentage input field for clarity.

## 4.4.0 Accessibility Needs

- All form controls must have associated labels (e.g., using `for` attribute).
- The page must be fully navigable and operable using only a keyboard.
- Validation errors must be programmatically associated with their respective inputs for screen reader users (e.g., using `aria-describedby`).
- UI must comply with WCAG 2.1 Level AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Margin values (both percentage and fixed fee) must be positive, non-zero numbers.

### 5.1.3 Enforcement Point

Client-side form validation and server-side API validation.

### 5.1.4 Violation Handling

The save operation is rejected, and a user-friendly error message is returned.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Only one default margin structure (either percentage or fixed fee) can be active at any given time.

### 5.2.3 Enforcement Point

The UI and database schema should enforce this constraint.

### 5.2.4 Violation Handling

N/A - The system design prevents this from occurring.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

Changes to the default margin structure only apply to projects created after the change is saved; they do not retroactively affect existing projects.

### 5.3.3 Enforcement Point

Project creation service logic.

### 5.3.4 Violation Handling

N/A - This is a rule of application logic.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-006

#### 6.1.1.2 Dependency Reason

Requires a logged-in user to perform the action.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-074

#### 6.1.2.2 Dependency Reason

Requires the Role-Based Access Control (RBAC) system to be in place to restrict access to System Admins and Finance Managers.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-086

#### 6.1.3.2 Dependency Reason

Requires the audit trail system to be functional to log the configuration changes as per security requirements.

## 6.2.0.0 Technical Dependencies

- A backend configuration service/module to handle system-wide settings.
- A database table (`system_configuration` or similar) to persist the settings.
- An RBAC guard/middleware for securing the API endpoint.

## 6.3.0.0 Data Dependencies

- A list of supported currencies (ISO 4217) must be available for the fixed-fee currency dropdown.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The configuration page should load in under 2 seconds.
- The save operation (API call) should have a p95 latency of less than 200ms.

## 7.2.0.0 Security

- Access to the configuration page and its corresponding API endpoint must be strictly limited to users with 'System Administrator' or 'Finance Manager' roles (REQ-SEC-001).
- All changes must be logged in the immutable audit trail (REQ-FUN-005).
- Input must be sanitized to prevent XSS and other injection attacks.

## 7.3.0.0 Usability

- The interface for setting the margin should be clear and unambiguous, requiring no external documentation for a user to understand.

## 7.4.0.0 Accessibility

- The feature must meet WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge (REQ-DEP-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires a new frontend component and route.
- Requires a new secured backend endpoint for CRUD operations on a configuration setting.
- Integration with the audit logging service is required.

## 8.3.0.0 Technical Risks

- Minimal risk. The primary concern is ensuring the new default is correctly picked up by the project creation workflow, which requires careful integration testing.

## 8.4.0.0 Integration Points

- Audit Service: To log all configuration changes.
- Project Service: The project creation logic will need to read this configuration value to apply it to new projects.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify a System Admin can set and update both percentage and fixed-fee margins.
- Verify a Finance Manager can set and update both percentage and fixed-fee margins.
- Verify a Vendor Contact cannot access the page or the API.
- Verify all input validation rules (positive numbers, required fields).
- Verify that saving a new margin creates a correct audit log entry.
- Verify that creating a new project (in a separate test) correctly applies the configured default margin.

## 9.3.0.0 Test Data Needs

- User accounts with 'System Administrator', 'Finance Manager', and 'Vendor Contact' roles.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration tests implemented with >= 80% code coverage and all are passing
- E2E tests for the primary happy path and error conditions are created and passing
- User interface is responsive and meets WCAG 2.1 AA accessibility standards
- Security requirements (RBAC, audit logging) are implemented and verified
- All related documentation (e.g., OpenAPI spec) is updated
- Story has been successfully deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational setting for the entire financial workflow. It should be completed early in the development cycle as it is a dependency for project creation and invoicing stories (e.g., US-056).

## 11.4.0.0 Release Impact

- This feature is critical for the initial release (MVP) as it defines a core business rule for profitability.

