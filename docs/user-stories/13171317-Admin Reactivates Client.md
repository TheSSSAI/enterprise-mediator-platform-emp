# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-019 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Reactivates Client |
| As A User Story | As a System Administrator, I want to change the st... |
| User Persona | System Administrator. This user has full permissio... |
| Business Value | Enables the business to re-engage with past client... |
| Functional Area | Entity Management |
| Story Theme | Client Lifecycle Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Reactivate a client from the client list view

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a System Administrator logged into the platform and viewing the client list, which is filtered to show inactive clients.

### 3.1.5 When

I locate a specific inactive client and select the 'Reactivate' option from their action menu, and then confirm the action in the confirmation dialog.

### 3.1.6 Then

The system updates the client's status from 'Inactive' to 'Active' in the database.

### 3.1.7 Validation Notes

Verify the client's status is updated in the database. The client should disappear from the 'Inactive' filter view and appear in the 'Active' filter view. A success toast notification should appear. The client should now be selectable when creating a new project.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Reactivate a client from their detail page

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am a System Administrator viewing the detail page of a client whose status is 'Inactive'.

### 3.2.5 When

I click the 'Reactivate Client' button and confirm the action.

### 3.2.6 Then

The client's status on the detail page updates to 'Active' and a success notification is displayed.

### 3.2.7 Validation Notes

Verify the status field on the UI updates. The 'Reactivate Client' button should be replaced by a 'Deactivate Client' button. The database record must reflect the 'Active' status.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Audit trail logs the reactivation event

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

A System Administrator has successfully reactivated a client.

### 3.3.5 When

I view the system's audit trail.

### 3.3.6 Then

A new log entry exists for the client reactivation event, containing the timestamp, the administrator's user ID, the action taken ('Client Reactivated'), the target client ID, and a state snapshot showing the status changed from 'Inactive' to 'Active'.

### 3.3.7 Validation Notes

Query the audit log table or use the audit trail UI to confirm the presence and accuracy of the log entry, as per REQ-FUN-005.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

User cancels the reactivation action

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I am a System Administrator and have initiated the reactivation process for a client, bringing up the confirmation dialog.

### 3.4.5 When

I click the 'Cancel' button in the dialog.

### 3.4.6 Then

The dialog closes, no changes are made to the client's status, and no audit log is created for this attempt.

### 3.4.7 Validation Notes

Verify the client's status remains 'Inactive' in both the UI and the database.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Non-admin user attempts to view reactivation option

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

A user with a role other than System Administrator (e.g., Finance Manager) is logged in.

### 3.5.5 When

They view the client list or an inactive client's detail page.

### 3.5.6 Then

The 'Reactivate' option or button is not visible or is disabled, enforcing Role-Based Access Control.

### 3.5.7 Validation Notes

Log in as a Finance Manager and navigate to the client management screens to confirm the absence of the reactivation controls.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- An action menu item ('Reactivate') on each inactive client row in the client list.
- A primary action button ('Reactivate Client') on the detail page of an inactive client.
- A confirmation modal with a clear message (e.g., 'Reactivate [Client Name]?'), and 'Confirm' and 'Cancel' buttons.
- A non-blocking success toast notification (e.g., '[Client Name] has been successfully reactivated.').

## 4.2.0 User Interactions

- Clicking the 'Reactivate' action triggers the confirmation modal.
- Confirming the action updates the client's status and displays a success message.
- Cancelling the action closes the modal with no side effects.

## 4.3.0 Display Requirements

- The client's status must be clearly displayed on both the list and detail views.
- The UI must update immediately to reflect the new 'Active' status upon successful reactivation.

## 4.4.0 Accessibility Needs

- All buttons and menu items must be keyboard accessible and have appropriate ARIA labels.
- The confirmation modal must trap keyboard focus and be dismissible with the 'Escape' key.
- Status changes should be announced by screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Only clients with a status of 'Inactive' or 'Deactivated' can be reactivated.

### 5.1.3 Enforcement Point

UI and API

### 5.1.4 Violation Handling

The 'Reactivate' option will not be available for clients who are already 'Active'. The API will return an error if a reactivation is attempted on an active client.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Only users with the 'System Administrator' role can reactivate clients.

### 5.2.3 Enforcement Point

API and UI

### 5.2.4 Violation Handling

The UI will not render the control for unauthorized users. The API will return a 403 Forbidden status code for unauthorized requests.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-013

#### 6.1.1.2 Dependency Reason

Requires the client list view to exist to place the action menu.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-016

#### 6.1.2.2 Dependency Reason

Requires the client detail view to exist to place the action button.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-018

#### 6.1.3.2 Dependency Reason

Requires the ability to deactivate a client, as this story is the logical counterpart.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-086

#### 6.1.4.2 Dependency Reason

Requires the audit trail system to be in place to log the reactivation event.

## 6.2.0.0 Technical Dependencies

- Backend API endpoint to handle the status update (e.g., PATCH /api/v1/clients/{id}/status).
- Role-Based Access Control (RBAC) middleware/service.
- Audit logging service.

## 6.3.0.0 Data Dependencies

- The Client data model must include a 'status' field (e.g., ENUM with 'Active', 'Inactive').

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response time for the status update must be < 250ms (p95) as per REQ-NFR-001.

## 7.2.0.0 Security

- The action must be restricted to System Administrators, enforced at the API level (REQ-SEC-001).
- The action must be logged in the immutable audit trail with all required details (REQ-FUN-005).

## 7.3.0.0 Usability

- The action should require explicit confirmation to prevent accidental reactivation.
- The system should provide immediate visual feedback upon successful completion.

## 7.4.0.0 Accessibility

- Must adhere to WCAG 2.1 Level AA standards (REQ-INT-001).

## 7.5.0.0 Compatibility

- Functionality must be consistent across the latest two major versions of supported browsers (Chrome, Firefox, Safari, Edge) as per REQ-DEP-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- This is a simple state change on a single data entity.
- Involves standard UI components (button, modal, notification).
- Requires integration with existing auth and audit logging services.

## 8.3.0.0 Technical Risks

- Potential for race conditions if not handled atomically, though unlikely for this specific action.
- Ensuring the audit log is written within the same transaction as the status update to maintain consistency.

## 8.4.0.0 Integration Points

- Client Management Service (Backend)
- Authentication/Authorization Service
- Audit Log Service
- Project Management Service (to verify reactivated client is available for new projects)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify an admin can reactivate a client from the list view.
- Verify an admin can reactivate a client from the detail view.
- Verify a non-admin user cannot see or perform the action.
- Verify cancelling the action results in no change.
- Verify the audit log is created correctly upon successful reactivation.
- Verify a reactivated client can be assigned to a new project.

## 9.3.0.0 Test Data Needs

- A test user with the 'System Administrator' role.
- A test user with a non-admin role (e.g., 'Finance Manager').
- At least one client with the status 'Inactive'.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration tests achieve >80% code coverage and are passing
- E2E test scenarios are implemented and passing in the CI/CD pipeline
- User interface changes reviewed and approved by UX/Product Owner
- Performance requirements (API latency) are met
- Security requirements (RBAC, Auditing) are validated
- Relevant user and technical documentation is updated
- Story deployed and verified in the staging environment without regressions

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

2

## 11.2.0.0 Priority

🟡 Medium

## 11.3.0.0 Sprint Considerations

- This story should be prioritized alongside its counterpart, US-018 (Deactivate Client), to provide complete lifecycle management.
- Dependent on the core client entity and list/detail views being complete.

## 11.4.0.0 Release Impact

Enhances core administrative functionality for managing client relationships. Not a blocker for initial launch but essential for long-term operational efficiency.

