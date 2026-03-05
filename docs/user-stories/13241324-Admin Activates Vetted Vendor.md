# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-026 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Activates Vetted Vendor |
| As A User Story | As a System Administrator, I want to change a vend... |
| User Persona | System Administrator, as defined in REQ-SEC-001, w... |
| Business Value | Enables the final step of the vendor onboarding pr... |
| Functional Area | User and Entity Management |
| Story Theme | Vendor Lifecycle Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Activate vendor from the vendor detail page

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am logged in as a System Administrator and am viewing the detail page for a vendor whose status is 'Pending Vetting'

### 3.1.5 When

I click the 'Activate Vendor' button and confirm the action in the confirmation dialog

### 3.1.6 Then

the system updates the vendor's status to 'Active' in the database

### 3.1.7 And

an entry is created in the audit trail logging the status change from 'Pending Vetting' to 'Active', including my user ID, timestamp, and the vendor ID.

### 3.1.8 Validation Notes

Verify the status change in the UI and by querying the database. Check the audit log table for the corresponding new entry.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Happy Path: Activate vendor from the vendor list view

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am logged in as a System Administrator and am viewing the vendor list

### 3.2.5 And

a success notification toast is displayed.

### 3.2.6 When

I use the row-level action menu to select 'Activate' and confirm the action

### 3.2.7 Then

the vendor's status in that row of the list immediately updates to 'Active'

### 3.2.8 Validation Notes

Verify the status badge/text updates in the list view without a page reload. Confirm the change persists on refresh.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Error Condition: Unauthorized user attempts to activate

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am logged in with a role other than System Administrator (e.g., Finance Manager)

### 3.3.5 When

I navigate to the vendor detail page or vendor list

### 3.3.6 Then

the 'Activate Vendor' button or action menu item must not be visible or must be disabled

### 3.3.7 And

if I attempt to call the activation API endpoint directly, the system must respond with a 403 Forbidden status code.

### 3.3.8 Validation Notes

Test by logging in with different roles. Use a tool like Postman or browser dev tools to attempt a direct API call and verify the 403 response.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Edge Case: Action is not available for an already active vendor

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I am logged in as a System Administrator

### 3.4.5 When

I view the details or list entry for a vendor that is already in 'Active' status

### 3.4.6 Then

the 'Activate Vendor' action must not be available.

### 3.4.7 Validation Notes

Check the UI to ensure the button/option is hidden or replaced (e.g., by a 'Deactivate' option from US-027).

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Error Condition: System fails during activation

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am a System Administrator and I trigger the 'Activate Vendor' action

### 3.5.5 When

the backend service fails to update the database due to a server or network error

### 3.5.6 Then

the vendor's status in the UI must remain 'Pending Vetting'

### 3.5.7 And

a user-friendly error message (e.g., 'Failed to activate vendor. Please try again.') must be displayed.

### 3.5.8 Validation Notes

Simulate a 5xx server response from the API endpoint using a mock server or by temporarily taking the database down in a test environment.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Downstream Effect: Activated vendor is available for matching

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

a vendor's status has been successfully changed to 'Active'

### 3.6.5 When

a new project is created and the system searches for suitable vendors

### 3.6.6 Then

the newly activated vendor must be included in the pool of candidates for semantic search and recommendation, as per REQ-FUN-002.

### 3.6.7 Validation Notes

This is an integration check. After activating a vendor, create a project that matches their skills and verify they appear in the recommended vendor list.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- An 'Activate Vendor' button on the Vendor Detail page (visible only for 'Pending Vetting' status).
- An 'Activate' option within a row-level action menu (e.g., kebab menu) on the Vendor List page.
- A confirmation modal with 'Confirm' and 'Cancel' buttons.
- A non-intrusive success/error notification component (toast).

## 4.2.0 User Interactions

- Clicking 'Activate' should open the confirmation modal.
- Confirming the action triggers the API call and shows a loading state.
- The UI should update dynamically upon successful API response without requiring a full page reload.

## 4.3.0 Display Requirements

- The vendor status should be clearly displayed as a badge or label with distinct styling for 'Pending Vetting' and 'Active'.

## 4.4.0 Accessibility Needs

- All buttons, menus, and modals must be fully keyboard navigable (Tab, Enter, Esc).
- All interactive elements must have appropriate ARIA labels (e.g., `aria-label="Activate vendor [Vendor Name]"`).
- Confirmation dialogs must trap focus.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Only vendors with a status of 'Active' can be matched with or receive project briefs.

### 5.1.3 Enforcement Point

During the vendor matching process (REQ-FUN-002).

### 5.1.4 Violation Handling

Vendors with any other status are excluded from the query for potential project matches.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The transition to 'Active' status is only permitted from the 'Pending Vetting' status.

### 5.2.3 Enforcement Point

Backend API service logic.

### 5.2.4 Violation Handling

The API should return a 409 Conflict or 400 Bad Request error if an attempt is made to activate a vendor from an invalid state (e.g., 'Deactivated').

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-020

#### 6.1.1.2 Dependency Reason

A vendor profile must be created with a 'Pending Vetting' status before it can be activated.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-024

#### 6.1.2.2 Dependency Reason

The Vendor Detail page UI must exist to place the 'Activate Vendor' button.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-074

#### 6.1.3.2 Dependency Reason

The Role-Based Access Control (RBAC) system must be in place to restrict this action to System Administrators.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint (e.g., PATCH /api/v1/vendors/{id}/status) to handle the status update.
- The Audit Logging service must be available to record the action.
- Authentication middleware to validate the user's JWT and role.

## 6.3.0.0 Data Dependencies

- Requires test data for vendors in the 'Pending Vetting' state.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response time for the status update action must be under 250ms (p95) as per REQ-NFR-001.

## 7.2.0.0 Security

- The action must be restricted to the 'System Administrator' role, enforced at the API Gateway and re-verified at the service level (REQ-SEC-001).
- The action must be logged in the immutable audit trail with user, timestamp, and before/after state (REQ-FUN-005).

## 7.3.0.0 Usability

- The action should require explicit confirmation to prevent accidental activation.
- The system must provide immediate visual feedback upon success or failure.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards (REQ-INT-001).

## 7.5.0.0 Compatibility

- Functionality must be consistent across the latest two major versions of Chrome, Firefox, Safari, and Edge (REQ-DEP-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Involves a simple database field update.
- Requires integration with existing authentication and audit logging services.
- Frontend work is limited to adding a button and handling state updates.

## 8.3.0.0 Technical Risks

- Potential for race conditions if multiple admins could act on the same vendor simultaneously, though this risk is very low. The API should handle this gracefully.

## 8.4.0.0 Integration Points

- Authentication Service (for RBAC).
- Vendor Database.
- Audit Log Service.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify successful activation from both the list and detail views.
- Verify unauthorized users (by role) cannot see the action or call the API.
- Verify the action is not available for vendors in 'Active' or 'Deactivated' states.
- Verify the audit log is correctly written upon successful activation.
- Verify UI feedback (success/error messages) works correctly.

## 9.3.0.0 Test Data Needs

- User accounts for 'System Administrator' and 'Finance Manager' roles.
- Vendor records with statuses 'Pending Vetting', 'Active', and 'Deactivated'.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration tests implemented with >80% code coverage for the new logic
- E2E tests for the happy path and authorization failure scenarios are passing
- User interface changes reviewed and approved by UX/Product Owner
- Security requirements (RBAC, Auditing) validated
- All related documentation (e.g., OpenAPI spec) has been updated
- Story has been deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

2

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a core feature of the vendor management workflow and a blocker for making vendors available for projects. It should be prioritized after vendor creation and viewing stories are complete.

## 11.4.0.0 Release Impact

This feature is essential for the MVP release as it enables the core business process of connecting vetted vendors to projects.

