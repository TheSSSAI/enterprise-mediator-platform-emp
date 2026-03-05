# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-069 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Overrides Project Margin with Approval Workf... |
| As A User Story | As a System Admin, I want to override the default ... |
| User Persona | System Administrator (Initiator), Finance Manager ... |
| Business Value | Enables business flexibility to handle non-standar... |
| Functional Area | Financial Management |
| Story Theme | Project Financial Configuration |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Admin successfully requests a percentage-based margin override

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A System Admin is viewing a project in the 'Awarded' state which has a default margin of 15%

### 3.1.5 When

The Admin navigates to the project's financial settings, initiates an override, selects 'Percentage', enters '12.5', provides a valid justification, and submits the request

### 3.1.6 Then

The project's margin status is updated to 'Pending Approval' and the UI reflects this state.

### 3.1.7 And

Any financial calculations, such as draft invoices, continue to use the original 15% margin until the override is approved.

### 3.1.8 Validation Notes

Verify the project's state in the database, check the notification queue/log, and inspect the audit trail for the new entry.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Admin successfully requests a fixed-fee margin override

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

A System Admin is viewing a project in the 'Awarded' state with a vendor cost of $10,000 and a default 15% margin

### 3.2.5 When

The Admin initiates an override, selects 'Fixed Fee', enters '2000', provides a justification, and submits the request

### 3.2.6 Then

The project's margin status is updated to 'Pending Approval'.

### 3.2.7 And

The draft client invoice total remains calculated based on the original margin until approval.

### 3.2.8 Validation Notes

Confirm the margin status change and that financial calculations are not yet affected.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Finance Manager approves a pending margin override request

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

A project has a pending margin override request from 15% to 12.5%, and I am logged in as a Finance Manager

### 3.3.5 When

I navigate to the approval request and click 'Approve'

### 3.3.6 Then

The project's margin is permanently updated to 12.5%.

### 3.3.7 And

The original requesting System Admin receives a notification that the override was approved.

### 3.3.8 Validation Notes

Generate a new draft invoice to verify the total has been recalculated. Check the audit log for the approval event.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Finance Manager rejects a pending margin override request

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

A project has a pending margin override request, and I am logged in as a Finance Manager

### 3.4.5 When

I reject the request and provide a reason, such as 'Discount is not authorized'

### 3.4.6 Then

The project's margin reverts to the original default value.

### 3.4.7 And

The requesting System Admin receives a notification of the rejection, including the reason provided.

### 3.4.8 Validation Notes

Verify the project's margin value in the database has reverted. Check the audit log and notification system.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Admin enters invalid data for the override value

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

A System Admin is on the margin override screen for a project

### 3.5.5 When

The Admin enters a non-numeric value (e.g., 'abc') or a negative number (e.g., '-10') in the margin value field and attempts to submit

### 3.5.6 Then

A clear validation error message is displayed next to the field, and the form submission is blocked.

### 3.5.7 Validation Notes

Test with various invalid inputs like text, negative numbers, and symbols to ensure client-side and server-side validation works.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Admin attempts to submit an override request without a justification

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

A System Admin is on the margin override screen

### 3.6.5 When

The Admin enters a valid new margin value but leaves the justification text field empty and attempts to submit

### 3.6.6 Then

A validation error message is displayed for the justification field, and the form submission is blocked.

### 3.6.7 Validation Notes

Verify that the justification field is marked as required and that the form cannot be submitted if it's empty or contains only whitespace.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Admin attempts to override margin on a project where an invoice has been paid

### 3.7.3 Scenario Type

Edge_Case

### 3.7.4 Given

A project is in the 'Active' state, indicating the initial invoice has been paid

### 3.7.5 When

A System Admin navigates to the project's financial settings

### 3.7.6 Then

The option to override the margin is disabled or hidden.

### 3.7.7 And

If the Admin attempts to trigger the action via an API call, a '403 Forbidden' or '409 Conflict' error is returned with a message stating that the margin cannot be changed at this stage.

### 3.7.8 Validation Notes

Check the UI for the disabled control. Use an API client like Postman to test the endpoint directly for a project in the 'Active' state.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A button or link on the project details page to 'Override Margin'.
- A modal or dedicated form for the override request.
- Radio buttons or a dropdown to select override type ('Percentage' or 'Fixed Fee').
- A numeric input field for the new margin value.
- A required text area for the justification.
- A 'Submit for Approval' button.
- A display area on the project page to show the current margin status (e.g., 'Default: 15%', 'Pending Approval: 12.5%', 'Overridden: 12.5%').
- A section in the Finance Manager's dashboard or a dedicated page to list and manage pending approval requests.

## 4.2.0 User Interactions

- Submitting the form triggers the approval workflow.
- The Finance Manager can approve or reject a request with a single click.
- Rejecting a request may open a small modal to enter the reason for rejection.

## 4.3.0 Display Requirements

- The UI must clearly indicate when a margin is pending approval.
- The justification provided by the Admin must be visible to the Finance Manager reviewing the request.

## 4.4.0 Accessibility Needs

- All form fields must have associated labels.
- All interactive elements must be keyboard accessible.
- Status changes must be communicated to screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-FIN-001

### 5.1.2 Rule Description

A margin override can only be requested for projects in a pre-invoiced state (e.g., 'Awarded'). It cannot be changed once a project is 'Active' (invoice paid).

### 5.1.3 Enforcement Point

API endpoint and UI layer.

### 5.1.4 Violation Handling

The UI control is disabled. The API returns an error response.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-FIN-002

### 5.2.2 Rule Description

All margin overrides require a non-empty justification.

### 5.2.3 Enforcement Point

Client-side and server-side validation.

### 5.2.4 Violation Handling

Form submission is blocked with a user-friendly error message.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-FIN-003

### 5.3.2 Rule Description

An overridden margin value only takes effect after approval by a user with the 'Finance Manager' role.

### 5.3.3 Enforcement Point

Backend financial calculation service.

### 5.3.4 Violation Handling

The system continues to use the default margin for all calculations until the status is 'Overridden'.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-068

#### 6.1.1.2 Dependency Reason

A default margin structure must exist in the system to be overridden.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-074

#### 6.1.2.2 Dependency Reason

The 'System Admin' and 'Finance Manager' roles with distinct permissions must be defined and functional.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-056

#### 6.1.3.2 Dependency Reason

The invoice generation logic must exist to be influenced by this override.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

A-Notification-Story

#### 6.1.4.2 Dependency Reason

A notification system (as per REQ-FUN-005) must be in place to alert Finance Managers.

### 6.1.5.0 Story Id

#### 6.1.5.1 Story Id

An-Audit-Trail-Story

#### 6.1.5.2 Dependency Reason

The audit trail system (as per REQ-FUN-005) must be functional to log all override actions.

## 6.2.0.0 Technical Dependencies

- Project Service API for managing project data.
- Notification Service for sending alerts.
- Audit Log Service for recording actions.
- User Service/Cognito for role-based access control.

## 6.3.0.0 Data Dependencies

- Requires access to Project entity data, including its current status and financial details.
- Requires access to User entity data to identify users with the 'Finance Manager' role.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The override submission API call should respond in under 300ms.
- Loading the list of pending approvals for a Finance Manager should take less than 1 second.

## 7.2.0.0 Security

- Only users with the 'System Admin' role can initiate an override request.
- Only users with the 'Finance Manager' role can approve or reject a request.
- All override actions (request, approve, reject) must be logged in the immutable audit trail as per REQ-FUN-005.
- Input validation must be performed on the server-side to prevent malicious data injection.

## 7.3.0.0 Usability

- The process for requesting and approving an override should be intuitive and require minimal steps.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest two versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires a multi-step workflow (request, approve/reject) instead of a simple CRUD operation.
- Involves state management for the margin (Default, Pending, Overridden).
- Requires inter-service communication (Project -> Notification -> Audit). Asynchronous communication via an event bus (SNS/SQS) is recommended.
- The financial calculation logic must be updated to handle the different margin states.

## 8.3.0.0 Technical Risks

- Potential for race conditions if a project's status changes while an approval is in flight. The workflow must handle this gracefully.
- Ensuring the atomicity of the approval action (updating project state, logging, notifying) is critical.

## 8.4.0.0 Integration Points

- Project Service: To store and manage the override state.
- Notification Service: To trigger alerts to Finance Managers.
- Audit Log Service: To record all state changes.
- Invoice Generation Logic: To consume the final approved margin.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Full E2E test of the request-approve workflow.
- Full E2E test of the request-reject workflow.
- Test API endpoint security to ensure a System Admin cannot approve their own request (unless they also have Finance role) and other roles cannot perform any action.
- Test what happens if a project is cancelled while an override request is pending.
- Test UI validation for all form fields.

## 9.3.0.0 Test Data Needs

- User accounts with 'System Admin' role.
- User accounts with 'Finance Manager' role.
- Projects in 'Awarded' state.
- Projects in 'Active' state.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration tests implemented with at least 80% coverage for new code
- E2E tests for happy path and rejection flow are implemented and passing
- User interface reviewed and approved by UX/Product Owner
- Security requirements validated (role checks, audit logging)
- All related documentation (e.g., OpenAPI spec) has been updated
- Story deployed and verified in the staging environment by QA

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story has several dependencies on core features like roles, projects, and notifications. It should be scheduled after those are completed and stable.
- Requires both backend and frontend development, which should be coordinated.

## 11.4.0.0 Release Impact

- This is a critical feature for business operations and a key requirement for handling real-world client contracts.

