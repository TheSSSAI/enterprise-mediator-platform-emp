# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-065 |
| Elaboration Date | 2025-01-24 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Manages Disputed Funds by Releasing or Retur... |
| As A User Story | As a System Administrator, I want to manually reso... |
| User Persona | System Administrator. This user has the highest le... |
| Business Value | Enables the business to act as a trusted mediator ... |
| Functional Area | Financial Management & Project Lifecycle |
| Story Theme | Dispute Resolution and Financial Controls |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Admin releases full escrowed amount to the vendor

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A project is in 'Disputed' status with $10,000 held in escrow, and I am logged in as a System Administrator

### 3.1.5 When

I navigate to the project's 'Dispute Resolution' panel, select 'Release Funds to Vendor', confirm the full amount of $10,000 in the confirmation modal, and submit a mandatory resolution justification note

### 3.1.6 Then

The system must initiate a payout transaction of $10,000 to the awarded vendor via the integrated payment gateway.

### 3.1.7 And

The client and vendor contacts for the project must receive an email notification detailing the resolution and the fund release.

### 3.1.8 Validation Notes

Verify in the payment gateway's sandbox that the payout was initiated. Check the project's status in the UI/DB. Confirm the new transaction and audit log entries. Check mock email service for notifications.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Happy Path: Admin returns full escrowed amount to the client

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

A project is in 'Disputed' status with $10,000 held in escrow, and I am logged in as a System Administrator

### 3.2.5 When

I navigate to the project's 'Dispute Resolution' panel, select 'Return Funds to Client', confirm the full amount of $10,000, and submit a justification note

### 3.2.6 Then

The system must initiate a refund transaction of $10,000 to the client via the integrated payment gateway.

### 3.2.7 And

The client and vendor must be notified via email of the resolution.

### 3.2.8 Validation Notes

Verify in the payment gateway's sandbox that the refund was initiated. Check project status, transaction log, audit trail, and email notifications.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Alternative Flow: Admin resolves dispute with a split payment

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

A project is in 'Disputed' status with $10,000 held in escrow

### 3.3.5 When

I select a 'Resolve with Split Payment' option, enter $7,000 to be released to the vendor and $3,000 to be returned to the client, and submit a justification note

### 3.3.6 Then

The system must initiate two separate transactions: a $7,000 payout to the vendor and a $3,000 refund to the client.

### 3.3.7 And

Notifications sent to the client and vendor must specify the exact amounts they were refunded or paid.

### 3.3.8 Validation Notes

Verify both transactions in the payment gateway sandbox. Confirm project status and the creation of two distinct transaction records.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Error Condition: Attempting to release more funds than available in escrow

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

A project is in 'Disputed' status with $10,000 held in escrow

### 3.4.5 When

I attempt to release or split funds totaling more than $10,000

### 3.4.6 Then

The UI must display a clear validation error message (e.g., 'Total amount cannot exceed funds in escrow: $10,000.00').

### 3.4.7 And

The system must block the transaction from being submitted.

### 3.4.8 Validation Notes

Test by entering an amount like $10,000.01 in the release/return amount field and verify the error message appears and the confirmation button is disabled.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Error Condition: Payment gateway API fails during transaction

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am attempting to resolve a disputed project's funds

### 3.5.5 When

The payment gateway integration returns an error during the payout or refund attempt

### 3.5.6 Then

The system must display a user-friendly error message to me, including a correlation ID for support.

### 3.5.7 And

The failure and the API response from the gateway must be logged for technical investigation.

### 3.5.8 Validation Notes

Use a mock API for the payment gateway that can be configured to return an error. Verify that the project status does not change and no new transaction records are committed to the database.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Edge Case: Dispute resolution controls are not available for non-disputed projects

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

A project is in any status other than 'Disputed' (e.g., 'Active', 'Completed')

### 3.6.5 When

I view the project's details page

### 3.6.6 Then

The 'Dispute Resolution' panel and its associated actions (Release/Return funds) must not be visible or must be disabled.

### 3.6.7 Validation Notes

Check the project pages for projects in various states to ensure the controls only appear for 'Disputed' projects.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A visually distinct 'Dispute Resolution' panel on the project details page, visible only for projects in 'Disputed' status.
- Display of the total amount currently held in escrow.
- Action buttons: 'Release to Vendor', 'Return to Client', 'Resolve with Split Payment'.
- A confirmation modal for each action.
- Input fields within the modal for specifying amounts (for partial/split actions).
- A mandatory, multi-line text area for 'Resolution Justification'.
- A final, clearly labeled confirmation button (e.g., 'Confirm Release of $7,000.00').

## 4.2.0 User Interactions

- Clicking an action button opens the confirmation modal.
- The system must prevent form submission if the justification note is empty.
- The system must validate that the entered amounts do not exceed the escrowed total.
- Upon successful submission, the UI should provide feedback (e.g., a success toast notification) and update the project status on the page.

## 4.3.0 Display Requirements

- The escrowed amount must be clearly displayed and formatted as currency.
- The confirmation modal must explicitly state the action being taken, the amount, the recipient (vendor), and the sender (client).

## 4.4.0 Accessibility Needs

- All buttons, inputs, and modals must be fully keyboard-navigable.
- Confirmation modals must trap focus.
- All UI elements must have appropriate ARIA labels for screen reader compatibility, especially the confirmation dialogs which represent a critical action.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Dispute resolution actions can only be performed on projects with a status of 'Disputed'.

### 5.1.3 Enforcement Point

API Gateway and Backend Service Logic.

### 5.1.4 Violation Handling

API request should be rejected with a 403 Forbidden or 409 Conflict status code if the project is not in the correct state.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The total amount released and/or returned cannot exceed the total amount held in escrow for the project.

### 5.2.3 Enforcement Point

Client-side validation and server-side validation.

### 5.2.4 Violation Handling

Client-side shows an error message. Server-side rejects the request with a 400 Bad Request status code.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

A justification note is mandatory for all dispute resolution actions.

### 5.3.3 Enforcement Point

Client-side validation and server-side validation.

### 5.3.4 Violation Handling

Client-side disables submission button. Server-side rejects the request with a 400 Bad Request status code if the note is missing.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-041

#### 6.1.1.2 Dependency Reason

The ability to set a project's status to 'Disputed' must exist before it can be resolved.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-058

#### 6.1.2.2 Dependency Reason

The workflow for a client to pay an invoice and place funds into escrow must be complete.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-060

#### 6.1.3.2 Dependency Reason

The core technical implementation for vendor payouts via the payment gateway must be complete.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-064

#### 6.1.4.2 Dependency Reason

The core technical implementation for client refunds via the payment gateway must be complete.

### 6.1.5.0 Story Id

#### 6.1.5.1 Story Id

US-086

#### 6.1.5.2 Dependency Reason

The immutable audit trail service must be available to log this critical financial action.

## 6.2.0.0 Technical Dependencies

- Payment Service (Stripe Connect/Wise Integration)
- Project Service (for state management)
- Notification Service (AWS SES)
- Audit Service

## 6.3.0.0 Data Dependencies

- Access to project data, including current status and awarded vendor/client details.
- Access to a ledger or transaction table that accurately reflects the amount held in escrow for a given project.

## 6.4.0.0 External Dependencies

- Stripe Connect API for processing refunds and payouts.
- Wise API for processing international payouts.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to initiate the resolution should respond within 500ms.
- The actual payment processing is asynchronous and dependent on the external gateway.

## 7.2.0.0 Security

- Access to this functionality must be strictly limited to the 'System Administrator' role (and potentially 'Finance Manager') via RBAC, enforced at the API Gateway.
- All actions must be logged in the immutable audit trail as per REQ-FUN-005.
- The justification note must be sanitized to prevent XSS attacks if displayed elsewhere.

## 7.3.0.0 Usability

- The UI must be extremely clear and unambiguous to prevent catastrophic user error. Use of confirmation modals with explicit details is mandatory.

## 7.4.0.0 Accessibility

- Must adhere to WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- Functionality must be verified on the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

High

## 8.2.0.0 Complexity Factors

- Requires a distributed transaction (Saga pattern) to ensure data consistency across Project, Payment, and Audit services as per REQ-TEC-002.
- Complex integration with external payment gateway APIs, including robust error handling and idempotency.
- High security and audit requirements due to direct manipulation of funds.
- The need for a flawless UI/UX to prevent costly mistakes.

## 8.3.0.0 Technical Risks

- Failure to correctly implement the Saga pattern could lead to data inconsistency (e.g., project status changes but payment fails).
- Mismanagement of payment gateway API keys or responses could lead to financial loss or security breaches.
- Potential for race conditions if multiple admins could theoretically act on the same dispute at once (should be mitigated with pessimistic/optimistic locking).

## 8.4.0.0 Integration Points

- Backend API for this feature.
- Stripe Connect / Wise APIs.
- Internal services: Project, Notification, Audit.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Full release to vendor.
- Full return to client.
- Split payment resolution.
- API failure and rollback.
- Validation errors for amount and justification.
- Role-based access control (non-admin user attempts to access).

## 9.3.0.0 Test Data Needs

- A project in 'Disputed' status with a known escrow amount.
- Test user accounts for System Admin, Client Contact, and Vendor Contact.
- Sandbox credentials for the payment gateway.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.
- Mock Service Worker (MSW) or similar to mock payment gateway APIs for integration tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing in a staging environment.
- Code reviewed and approved by at least two team members.
- Unit and integration test coverage meets the 80% project standard.
- E2E tests for all happy paths and key error conditions are implemented and passing.
- UI/UX has been reviewed and approved by the Product Owner.
- Security review completed to ensure RBAC is correctly enforced and audit logging is comprehensive.
- All related documentation (e.g., admin guide) has been updated.
- Story has been successfully deployed and verified in the staging environment using sandbox payment credentials.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

13

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story has significant dependencies and should be scheduled after core payment/refund functionalities are stable.
- Requires a senior developer due to the high complexity and risk.
- Sufficient time must be allocated for thorough manual QA in the staging/sandbox environment.

## 11.4.0.0 Release Impact

This is a critical feature for handling exceptions in the business process. The platform cannot be considered fully operational without a mechanism to resolve financial disputes.

