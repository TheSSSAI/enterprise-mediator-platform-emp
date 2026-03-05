# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-036 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Approves Project Brief |
| As A User Story | As a System Administrator, I want to formally appr... |
| User Persona | System Administrator. This user is responsible for... |
| Business Value | This action serves as a critical quality gate, ens... |
| Functional Area | Project Lifecycle Management |
| Story Theme | AI-Powered SOW Processing |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Admin successfully approves the project brief

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a System Admin logged into the system and viewing the 'Review SOW Data' page for a project with a status of 'Pending Review'

### 3.1.5 When

I click the 'Approve and Finalize Brief' button and confirm the action in the confirmation modal

### 3.1.6 Then

The system must change the project's status to 'Brief Approved', the SOW data fields on the page must become read-only, a success notification ('Project Brief approved. Vendor matching has been initiated.') is displayed, an asynchronous job to perform vendor matching is triggered, and an entry is created in the audit log for this action.

### 3.1.7 Validation Notes

Verify the status change in the database and on the UI. Confirm UI fields are no longer editable. Check the SQS queue for the vendor matching event. Query the audit log table for the new entry.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Confirmation modal for approval

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

I am a System Admin on the 'Review SOW Data' page

### 3.2.5 When

I click the 'Approve and Finalize Brief' button

### 3.2.6 Then

A confirmation modal must appear with the text 'Are you sure you want to approve this Project Brief? Once approved, it cannot be edited and vendor matching will begin.' and options for 'Confirm' and 'Cancel'.

### 3.2.7 Validation Notes

Verify the modal appears and that clicking 'Cancel' closes the modal with no state change.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempting to approve with missing required data

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am a System Admin on the 'Review SOW Data' page and have cleared a mandatory field (e.g., 'Required Skills')

### 3.3.5 When

I attempt to click the 'Approve and Finalize Brief' button

### 3.3.6 Then

The button should be disabled, or if clicked, a validation error message ('Please ensure all required fields are complete before approving.') must be displayed, and the project status must not change.

### 3.3.7 Validation Notes

Test by clearing a required field and observing the button's state and/or the error message upon click.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Backend failure during vendor matching trigger

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am a System Admin and I have clicked 'Confirm' in the approval modal

### 3.4.5 When

The backend successfully updates the project status but fails to publish the event to trigger vendor matching

### 3.4.6 Then

The entire transaction must be rolled back, the project status must revert to 'Pending Review', and a user-facing error message ('An error occurred. Please try again or contact support.') must be displayed. The failure must be logged with a correlation ID.

### 3.4.7 Validation Notes

This requires simulating a failure in the event publishing step (e.g., SNS/SQS service unavailable) and verifying that the database state is rolled back and the UI reflects the original state.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

UI state after successful approval

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

A project brief has been successfully approved

### 3.5.5 When

A System Admin reloads or navigates back to the project's brief page

### 3.5.6 Then

The page must display the data in a read-only format, and the 'Approve and Finalize Brief' button must be replaced with a status indicator, such as a 'Brief Approved' badge.

### 3.5.7 Validation Notes

Verify the persistent read-only state of the UI after a page refresh.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Primary button: 'Approve and Finalize Brief'
- Confirmation Modal with 'Confirm' and 'Cancel' buttons
- Success notification toast/banner
- Error notification toast/banner
- Read-only state for all data fields post-approval
- Status indicator/badge (e.g., 'Brief Approved')

## 4.2.0 User Interactions

- Clicking the approve button triggers a confirmation modal.
- Confirming the action locks the form fields and triggers the backend process.
- Cancelling the action closes the modal with no changes.
- Form fields should not be editable after approval.

## 4.3.0 Display Requirements

- The UI must clearly distinguish between the editable state (pre-approval) and the read-only state (post-approval).

## 4.4.0 Accessibility Needs

- The confirmation modal must be focus-trapped.
- All buttons and notifications must be accessible via keyboard and understood by screen readers, adhering to WCAG 2.1 AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A Project Brief can only be approved once.

### 5.1.3 Enforcement Point

Backend API and Frontend UI

### 5.1.4 Violation Handling

The approval action is disabled or hidden on the UI for already-approved briefs. The API will return a 409 Conflict error if an attempt is made to re-approve.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Approval of a Project Brief is the trigger for the vendor matching process.

### 5.2.3 Enforcement Point

Backend Project Service

### 5.2.4 Violation Handling

The vendor matching process cannot be initiated manually or for a project whose brief is not in an 'Approved' state.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-034

#### 6.1.1.2 Dependency Reason

The interface for reviewing AI-extracted SOW data must exist before an approval action can be added to it.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-035

#### 6.1.2.2 Dependency Reason

The ability for an Admin to edit the extracted data is a necessary precursor to finalizing and approving it.

## 6.2.0.0 Technical Dependencies

- Project Service: To handle the state transition of the project entity.
- Event Bus (AWS SNS/SQS): To publish an event that triggers the asynchronous vendor matching process.
- AI/Search Service: A consumer must be ready to listen for the 'BriefApproved' event to start the matching process.
- Audit Log Service: To record the approval action.

## 6.3.0.0 Data Dependencies

- Requires a Project entity with an associated SOW that has been processed and is in a 'Pending Review' state.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call for the approval action must have a p95 latency of < 250ms, as per REQ-NFR-001.

## 7.2.0.0 Security

- The API endpoint must be protected and only accessible by users with the 'System Administrator' role (REQ-SEC-001).
- The approval action must be logged in the immutable audit trail with user ID, timestamp, and project ID (REQ-FUN-005).

## 7.3.0.0 Usability

- The action should be clearly labeled and provide feedback (confirmation, success/error messages) to the user.

## 7.4.0.0 Accessibility

- All UI components must meet WCAG 2.1 Level AA standards (REQ-INT-001).

## 7.5.0.0 Compatibility

- Functionality must be consistent across the latest two major versions of supported browsers (Chrome, Firefox, Safari, Edge) as per REQ-DEP-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires a transactional database operation to ensure the state change and event publication are atomic.
- Involves cross-service communication via an asynchronous event bus, which requires robust error handling and a dead-letter queue (DLQ) strategy.
- Frontend state management needs to handle the transition from an editable form to a read-only view cleanly.

## 8.3.0.0 Technical Risks

- Potential for race conditions if not handled transactionally.
- Failure to publish the event after committing the DB transaction could lead to an inconsistent system state if not handled with a proper rollback or reconciliation mechanism.

## 8.4.0.0 Integration Points

- Project Service API (to update project status).
- AWS SNS/SQS (to publish 'BriefApproved' event).
- Audit Log Service API (to record the action).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify successful approval and all subsequent effects (UI lock, status change, event published, audit log).
- Verify validation for missing required fields.
- Verify the transactional rollback when the event publishing step fails.
- Verify an unauthorized user (e.g., Finance Manager) cannot see or use the approve button.

## 9.3.0.0 Test Data Needs

- A test user with 'System Administrator' role.
- A project entity in the 'Pending Review' state.
- A test user with a non-admin role to test access control.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration tests implemented with >80% code coverage and all are passing
- E2E test scenario for the happy path is implemented and passing
- User interface changes reviewed and approved by the design/product owner
- API endpoint is documented via OpenAPI specifications
- Security requirements (RBAC, auditing) are validated
- The 'BriefApproved' event is correctly published to the event bus with the defined payload
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a critical enabler for the subsequent vendor matching and proposal distribution workflows. It should be prioritized accordingly.
- Requires coordinated effort between frontend and backend developers.

## 11.4.0.0 Release Impact

Unlocks a major part of the core project lifecycle. Its completion is a prerequisite for releasing the vendor proposal feature.

