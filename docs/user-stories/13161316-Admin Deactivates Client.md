# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-018 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Deactivates Client |
| As A User Story | As a System Administrator, I want to change a clie... |
| User Persona | System Administrator. This user has full CRUD perm... |
| Business Value | Improves data hygiene and operational efficiency b... |
| Functional Area | Entity Management |
| Story Theme | Client Lifecycle Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Admin successfully deactivates a client

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a System Admin logged into the platform and viewing the Client Management page

### 3.1.5 When

I locate an 'Active' client and click the 'Deactivate' action, then confirm the action in the confirmation modal

### 3.1.6 Then

The system updates the client's status to 'Deactivated' in the database, a success notification 'Client [Client Name] has been deactivated.' is displayed, the client's status is visually updated in the UI, and an audit log entry is created for the action.

### 3.1.7 Validation Notes

Verify the status change in the database and on the client list UI. Check the audit log for a new entry with the correct user, target entity, and before/after state ('Active' -> 'Deactivated').

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Deactivated client is excluded from new project assignment

### 3.2.3 Scenario Type

Edge_Case

### 3.2.4 Given

A client has been successfully deactivated

### 3.2.5 When

A System Admin navigates to the 'Create New Project' page and attempts to select a client

### 3.2.6 Then

The deactivated client's name does not appear in the list of available clients to associate with the new project.

### 3.2.7 Validation Notes

Check the API endpoint that populates the client dropdown for new projects. It must filter out clients with a 'Deactivated' status.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Deactivating a client does not affect their active projects

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

An 'Active' client has one or more projects with a status of 'Active', 'Awarded', or 'In Review'

### 3.3.5 When

A System Admin deactivates that client

### 3.3.6 Then

The client's status is changed to 'Deactivated', but the status and data of their existing projects remain unchanged.

### 3.3.7 Validation Notes

Before deactivation, note the status of an active project for the client. After deactivation, verify that the project's status and details are identical.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Admin cancels the deactivation process

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

I am a System Admin and have opened the deactivation confirmation modal for a client

### 3.4.5 When

I click the 'Cancel' button or close the modal without confirming

### 3.4.6 Then

The modal closes, no changes are made to the client's status, and no audit log entry is created.

### 3.4.7 Validation Notes

Verify that the client's status remains 'Active' in both the UI and the database.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

UI prevents deactivating an already deactivated client

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am a System Admin viewing the client list

### 3.5.5 When

I view a client who is already in the 'Deactivated' state

### 3.5.6 Then

The 'Deactivate' action is disabled or replaced with a 'Reactivate' action for that client.

### 3.5.7 Validation Notes

Inspect the UI to ensure the deactivation control is not available for clients with a 'Deactivated' status.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A 'Deactivate' action (e.g., button or menu item) for each active client in the client list and on the client detail page.
- A confirmation modal with a clear warning message, a 'Confirm Deactivation' button, and a 'Cancel' button.
- A status indicator (e.g., a colored badge) on the client list and detail page to show 'Active' or 'Deactivated' status.
- A success toast/notification message upon successful deactivation.

## 4.2.0 User Interactions

- Clicking 'Deactivate' must trigger the confirmation modal.
- The confirmation modal must explain the consequences of deactivation (archiving, prevention from new projects, no impact on active projects).
- The UI should update optimistically or upon API success to reflect the new status without a full page reload.

## 4.3.0 Display Requirements

- The client list must be filterable by status ('Active', 'Deactivated') as per US-015.
- The confirmation modal must display the name of the client being deactivated.

## 4.4.0 Accessibility Needs

- The confirmation modal must be focus-trapped and fully keyboard navigable (Tab, Esc).
- All buttons and controls must have accessible names (aria-label).
- Status indicators must have accessible text alternatives for screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A deactivated client cannot be assigned to any new projects.

### 5.1.3 Enforcement Point

Backend API when fetching the list of clients for new project creation.

### 5.1.4 Violation Handling

The client is filtered out of the result set; they are not shown as an option in the UI.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Deactivating a client is a non-destructive action that preserves all historical data.

### 5.2.3 Enforcement Point

Backend service logic for the deactivation process.

### 5.2.4 Violation Handling

The operation must be an UPDATE of a status field, not a DELETE or a cascade delete of related records.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

All client deactivations must be recorded in the immutable audit trail.

### 5.3.3 Enforcement Point

Backend service logic, immediately after the database transaction for the status update is successfully committed.

### 5.3.4 Violation Handling

If the audit log write fails, the transaction should ideally be rolled back, or a high-priority alert should be triggered for manual investigation.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-012

#### 6.1.1.2 Dependency Reason

A client profile must exist before it can be deactivated.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-013

#### 6.1.2.2 Dependency Reason

The client list view is the primary UI location for the deactivation action.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-086

#### 6.1.3.2 Dependency Reason

The audit trail system must be in place to log this critical action.

## 6.2.0.0 Technical Dependencies

- A client data model with a 'status' field (e.g., ENUM 'Active', 'Deactivated').
- A generic confirmation modal component in the frontend library.
- An audit logging service/module.
- RBAC middleware to secure the API endpoint.

## 6.3.0.0 Data Dependencies

- Requires existing client data in the 'Active' state to test the functionality.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to update the client's status should complete in under 500ms (p95).
- The UI update should feel instantaneous to the user.

## 7.2.0.0 Security

- The API endpoint for deactivating a client must be protected and accessible only by users with the 'System Administrator' role.
- The action must be logged in the immutable audit trail with user, timestamp, and before/after state, as per REQ-FUN-005.

## 7.3.0.0 Usability

- The action should require explicit confirmation to prevent accidental deactivation.
- The system should provide clear feedback (success/error notification) after the action is performed.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Standard CRUD-like update operation.
- Requires modification of an existing query in the project creation flow to filter by client status.
- Integration with the existing audit log service is required.

## 8.3.0.0 Technical Risks

- Risk of forgetting to update all queries that fetch clients for selection, potentially allowing a deactivated client to be assigned to a new entity. A code review should specifically check for this.

## 8.4.0.0 Integration Points

- Client Management API (for the UPDATE operation).
- Project Management API (for filtering the client list).
- Audit Log Service (for recording the event).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify a client can be deactivated successfully.
- Verify a deactivated client does not appear in the 'Create Project' client list.
- Verify deactivating a client with active projects does not alter those projects.
- Verify the audit log correctly records the deactivation event.
- Verify that only a System Admin can perform the deactivation.

## 9.3.0.0 Test Data Needs

- A test user with the 'System Administrator' role.
- An active client with no projects.
- An active client with at least one active project.
- A test user with a non-admin role to test access control.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration tests implemented with >80% code coverage for the new logic
- E2E test scenario for the deactivation flow is implemented and passing
- User interface reviewed and approved by UX/Product Owner
- API endpoint is secured and tested for role-based access
- Audit log integration is verified
- No regressions in the 'Create Project' functionality
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

2

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story impacts the 'Create Project' feature (US-029). It should be coordinated with any work on that story to ensure the client filtering logic is implemented correctly.
- This is a foundational feature for client lifecycle management and should be prioritized early.

## 11.4.0.0 Release Impact

- Enables better data management for administrators. No direct impact on external users (Clients/Vendors).

