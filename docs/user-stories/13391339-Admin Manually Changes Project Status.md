# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-041 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Manually Changes Project Status |
| As A User Story | As a System Administrator, I want to manually chan... |
| User Persona | System Administrator. This user has full permissio... |
| Business Value | Provides essential operational control to manage n... |
| Functional Area | Project Lifecycle Management |
| Story Theme | Project Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Admin places an active project on hold

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am logged in as a System Administrator and am viewing the details of a project with 'Active' status

### 3.1.5 When

I select the 'On Hold' status from the status change control and confirm the action

### 3.1.6 Then

The system updates the project's status to 'On Hold', the UI reflects this change immediately, and a new entry is created in the audit log detailing the change (user, project, old status, new status, timestamp).

### 3.1.7 Validation Notes

Verify the project status in the database is 'On Hold'. Check the audit log for the corresponding entry. Any scheduled notifications for this project should be paused.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Admin cancels an awarded project

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am logged in as a System Administrator and am viewing the details of a project with 'Awarded' status

### 3.2.5 When

I select the 'Cancelled' status, a confirmation modal appears, and I confirm the cancellation

### 3.2.6 Then

The project's status is updated to 'Cancelled', the change is recorded in the audit log, and a 'ProjectCancelled' event is published for downstream systems (e.g., finance for refund review).

### 3.2.7 Validation Notes

Verify the project status in the database is 'Cancelled'. Check the audit log. Verify the event is published to the event bus (e.g., SNS/SQS).

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Admin resumes a project that was on hold

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I am logged in as a System Administrator and am viewing the details of a project with 'On Hold' status

### 3.3.5 When

I select the 'Active' status from the status change control and confirm the action

### 3.3.6 Then

The project's status is updated to 'Active', the change is recorded in the audit log, and project workflows are resumed.

### 3.3.7 Validation Notes

Verify the project status in the database is 'Active'. Check the audit log. Verify that any paused automated processes for the project are now un-paused.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Admin attempts an invalid status transition

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am logged in as a System Administrator and am viewing the details of a project with 'Completed' status

### 3.4.5 When

I attempt to change the status to 'On Hold'

### 3.4.6 Then

The system prevents the change and displays a user-friendly error message like 'A completed project cannot be put on hold', and the project's status remains 'Completed'.

### 3.4.7 Validation Notes

The UI control for changing status should ideally not even show 'On Hold' as an option. If it does, test that the API rejects the request with a 400 Bad Request status and a clear error message.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Confirmation modal for critical status changes

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

I am logged in as a System Administrator and am viewing an 'Active' project

### 3.5.5 When

I select a critical status like 'Cancelled' or 'Disputed'

### 3.5.6 Then

A confirmation modal appears with a warning about the implications of the change, requiring me to explicitly confirm before the action is executed.

### 3.5.7 Validation Notes

Test that the modal appears for 'Cancelled' and 'Disputed'. Test that clicking 'Cancel' in the modal aborts the status change.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Audit trail logging

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

A System Administrator is logged in

### 3.6.5 When

They successfully change the status of any project

### 3.6.6 Then

An immutable audit log entry is created containing the timestamp, responsible user ID, IP address, action ('Project Status Change'), target entity (Project ID), and a snapshot of the change (e.g., { 'status': { 'before': 'Active', 'after': 'On Hold' } }).

### 3.6.7 Validation Notes

Query the audit log table/service directly to verify the entry is created with all required fields as per REQ-DAT-001 and REQ-FUN-005.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A status indicator (e.g., a badge) on the project details page.
- A dropdown menu or similar control labeled 'Change Status' available only to System Admins.
- A confirmation modal with a warning message for critical status changes ('Cancelled', 'Disputed').
- A 'Confirm' button within the modal.
- A 'Cancel' button within the modal.
- Toast notifications for success or failure of the status change action.

## 4.2.0 User Interactions

- Admin clicks the 'Change Status' control to reveal available statuses.
- The list of available statuses is dynamically filtered to show only valid transitions from the current state.
- Admin selects a new status and clicks an 'Update' or 'Save' button.
- If the change is critical, the admin must interact with the confirmation modal to proceed.

## 4.3.0 Display Requirements

- The current project status must be clearly and prominently displayed.
- Error messages for invalid transitions must be clear and informative.

## 4.4.0 Accessibility Needs

- All UI controls (dropdown, buttons, modal) must be fully keyboard-navigable and compatible with screen readers, adhering to WCAG 2.1 AA standards (REQ-INT-001).

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Project status transitions must follow a predefined state machine. For example, a 'Completed' project cannot be moved to 'On Hold' or 'Active'. An 'On Hold' project can be moved back to its previous state (e.g., 'Active').

### 5.1.3 Enforcement Point

Backend API (Project Service). The frontend should also reflect these rules by disabling invalid options.

### 5.1.4 Violation Handling

The API will reject the request with a 400-level error code and a descriptive error message. The UI will display this error to the user.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Only users with the 'System Administrator' role can manually change a project's status.

### 5.2.3 Enforcement Point

API Gateway and re-verified at the Project Service level.

### 5.2.4 Violation Handling

The API will reject the request with a 403 Forbidden error.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

Changing a project's status to 'Cancelled' or 'Disputed' must flag the project for financial review and may impact fund handling in escrow.

### 5.3.3 Enforcement Point

The Project Service will publish an event upon status change, which the Payment Service will consume.

### 5.3.4 Violation Handling

N/A - This is a process trigger.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-029

#### 6.1.1.2 Dependency Reason

A project entity must exist to have its status changed.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-074

#### 6.1.2.2 Dependency Reason

The 'System Administrator' role and its permissions must be defined and enforceable.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-086

#### 6.1.3.2 Dependency Reason

The audit trail system must be in place to log the status change action.

## 6.2.0.0 Technical Dependencies

- Project Service microservice with a defined Project data model.
- Authentication service (e.g., AWS Cognito) to verify user roles.
- Asynchronous event bus (e.g., AWS SNS/SQS) for publishing 'ProjectStatusChanged' events.
- A state machine implementation for managing valid project status transitions.

## 6.3.0.0 Data Dependencies

- Existence of the `Project` table with a `status` column.
- Existence of the `AuditLog` table.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to update the project status should have a p95 latency of less than 250ms (REQ-NFR-001).

## 7.2.0.0 Security

- The action must be restricted to authorized roles (System Administrator) as per REQ-SEC-001.
- The status change must be logged in the immutable audit trail as per REQ-FUN-005.

## 7.3.0.0 Usability

- The process of changing a status should be intuitive and require minimal clicks.
- Feedback (success/error) must be immediate and clear.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards (REQ-INT-001).

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge (REQ-DEP-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing a robust, centralized state machine for project status transitions.
- Integrating with the event bus to publish status change events for downstream consumers.
- Developing a context-aware UI that only presents valid state transitions to the user.
- Ensuring the audit log captures a complete and accurate record of the change.

## 8.3.0.0 Technical Risks

- Potential for race conditions if multiple admins try to change the status simultaneously; optimistic locking should be considered.
- Ensuring the event publication is atomic with the database transaction to prevent data inconsistencies.

## 8.4.0.0 Integration Points

- Project Service: Handles the core logic and data update.
- Audit Service: Receives data to create the audit log entry.
- Event Bus (SNS/SQS): Publishes the `ProjectStatusChanged` event.
- Notification Service: May consume the event to notify stakeholders.
- Payment Service: May consume the event to handle financial implications (e.g., for 'Cancelled' status).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Test each valid state transition (e.g., Active -> On Hold, On Hold -> Active, Awarded -> Cancelled).
- Test several invalid state transitions (e.g., Completed -> On Hold, Pending -> Active).
- Test the confirmation modal flow for 'Cancelled' and 'Disputed' statuses.
- Verify that a non-admin user cannot see or use the status change control.
- Verify the content and creation of the audit log entry after a successful change.
- Verify the publication and payload of the `ProjectStatusChanged` event.

## 9.3.0.0 Test Data Needs

- Projects in various statuses (Pending, Awarded, Active, On Hold, Completed).
- User accounts with 'System Administrator' role.
- User accounts with non-admin roles (e.g., 'Finance Manager') for negative testing.

## 9.4.0.0 Testing Tools

- Jest for unit and integration tests.
- Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other engineer
- Unit and integration tests implemented for the state machine and API endpoint, achieving >80% code coverage
- E2E tests for the primary happy path and one error condition are implemented and passing
- User interface is responsive and has been reviewed for usability and accessibility compliance
- Performance of the API endpoint is verified to be under 250ms p95
- Security requirements (RBAC, audit logging) are validated
- Documentation for the project state machine is created or updated
- Story deployed and verified in the staging environment by QA

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a core feature for project management and should be prioritized early in the development of the project module.
- Requires the project entity and role-based access control to be in place before work can begin.

## 11.4.0.0 Release Impact

- Enables fundamental operational control over projects. Without this, the system cannot handle common business exceptions.

