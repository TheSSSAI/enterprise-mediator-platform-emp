# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-060 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Finance Manager Initiates Vendor Payout for Comple... |
| As A User Story | As a Finance Manager, I want to select a completed... |
| User Persona | Finance Manager: A user responsible for managing t... |
| Business Value | Ensures timely and accurate payment to vendors, wh... |
| Functional Area | Financial Management |
| Story Theme | Vendor Payout Workflow |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path - Initiate Payout for a Completed Project

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in Finance Manager and I am viewing the financial details of a project with status 'Completed' where the client's funds are fully held in escrow.

### 3.1.5 When

I click the 'Initiate Payout' button, review the details in the confirmation modal (Vendor, Amount, Destination Account [masked]), and click 'Confirm Initiation'.

### 3.1.6 Then

A new payout transaction is created in the system with a status of 'Pending Approval', a success notification is displayed, the UI updates to show the 'Pending Approval' status, and the 'Initiate Payout' button is disabled.

### 3.1.7 Validation Notes

Verify the transaction record in the database has the correct amount, vendor ID, project ID, and status. Check the UI for the status change and disabled button.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Happy Path - Initiate Payout for an Approved Milestone

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am a logged-in Finance Manager viewing a project where a specific milestone has been approved by the client and its corresponding funds are held in escrow.

### 3.2.5 When

I click the 'Initiate Payout' button next to the specific milestone and confirm the details.

### 3.2.6 Then

A payout transaction is created for the exact milestone amount with a status of 'Pending Approval', and the UI for that milestone is updated accordingly.

### 3.2.7 Validation Notes

Verify the transaction amount matches the milestone value, not the total project value.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Error Condition - Project Not in a Payable State

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am a logged-in Finance Manager viewing the financial details of a project with a status of 'Active' (not 'Completed' or having an approved milestone).

### 3.3.5 When

I look for the option to initiate a payout.

### 3.3.6 Then

The 'Initiate Payout' button is disabled or not visible, preventing the action.

### 3.3.7 Validation Notes

Check the UI state for projects in various non-payable statuses like 'Active', 'Proposed', 'On Hold'.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Error Condition - Insufficient Escrow Funds

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am a logged-in Finance Manager viewing a completed project, but the client's payment has not yet been received or cleared into escrow.

### 3.4.5 When

I navigate to the financial section.

### 3.4.6 Then

The 'Initiate Payout' button is disabled, and a clear message like 'Awaiting Client Funds' is displayed.

### 3.4.7 Validation Notes

Test with a project record where the associated client invoice is marked 'Unpaid' or 'Processing'.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Error Condition - Missing Vendor Payment Details

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am a logged-in Finance Manager viewing a completed and funded project, but the assigned vendor has not configured their payment details.

### 3.5.5 When

I click the 'Initiate Payout' button.

### 3.5.6 Then

The action is blocked, and an error message is displayed in the UI stating 'Cannot initiate payout: Vendor payment details are missing. Please ask the vendor to update their profile.'

### 3.5.7 Validation Notes

Test with a vendor record where payment details (Wise ID/Bank Info) are null or incomplete.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Edge Case - Attempt to Re-initiate an Existing Payout

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

I am a logged-in Finance Manager viewing a project where a payout has already been initiated and has a status of 'Pending Approval' or 'Completed'.

### 3.6.5 When

I view the payout section for that project or milestone.

### 3.6.6 Then

The 'Initiate Payout' button is disabled or replaced by the current status, preventing a duplicate initiation.

### 3.6.7 Validation Notes

The API endpoint for initiation must be idempotent or the UI must prevent the call.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Audit Trail Logging

### 3.7.3 Scenario Type

Happy_Path

### 3.7.4 Given

A Finance Manager has successfully initiated a payout for a project.

### 3.7.5 When

A System Administrator views the system's audit trail.

### 3.7.6 Then

A new audit log entry exists containing the timestamp, the Finance Manager's user ID, the action 'PAYOUT_INITIATED', the target project ID, vendor ID, the payout amount, and the IP address.

### 3.7.7 Validation Notes

Query the audit log table to confirm the entry is created with all required fields as per REQ-FUN-005.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A 'Financials' or 'Payments' tab on the project details page.
- A list of payable items (milestones or final project payment).
- An 'Initiate Payout' button for each payable item.
- A confirmation modal to display payout details before finalizing.
- Success and error toast notifications to provide immediate feedback.
- Status indicators (e.g., 'Ready for Payout', 'Pending Approval', 'Paid').

## 4.2.0 User Interactions

- The 'Initiate Payout' button must be disabled with a descriptive tooltip if preconditions are not met.
- Clicking 'Initiate Payout' opens a confirmation modal.
- Confirming the modal triggers the API call and updates the UI state without a full page reload.
- Cancelling the modal closes it with no change in state.

## 4.3.0 Display Requirements

- The confirmation modal must display: Vendor Name, Project Name, Payout Amount and Currency, and the destination payment info (e.g., 'Wise ID: ••••••1234') masked for security.
- The UI must clearly indicate the status of each payable item.

## 4.4.0 Accessibility Needs

- All buttons, modals, and form fields must be fully keyboard accessible (tab navigation, enter to activate).
- Modals must trap focus.
- Buttons must have descriptive aria-labels, especially when disabled.
- WCAG 2.1 Level AA compliance is required.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-FIN-01

### 5.1.2 Rule Description

A payout can only be initiated if the corresponding client funds are successfully received and held in escrow.

### 5.1.3 Enforcement Point

Backend service layer before creating the payout transaction record.

### 5.1.4 Violation Handling

The API call fails with a 402 Payment Required or 409 Conflict error, and the UI displays a user-friendly message.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-FIN-02

### 5.2.2 Rule Description

A payout can only be initiated for projects in a 'Completed' state or for milestones that have been explicitly approved.

### 5.2.3 Enforcement Point

Backend service layer and reflected in the frontend UI (disabled button).

### 5.2.4 Violation Handling

API call fails with a 409 Conflict error. UI prevents the action from being triggered.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-FIN-03

### 5.3.2 Rule Description

A payout cannot be initiated for a vendor with incomplete or missing payment details.

### 5.3.3 Enforcement Point

Backend service layer.

### 5.3.4 Violation Handling

API call fails with a 412 Precondition Failed error. UI displays a specific error message.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-058

#### 6.1.1.2 Dependency Reason

Client must have paid the invoice, and funds must be in escrow before a payout can be initiated.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-095

#### 6.1.2.2 Dependency Reason

For milestone-based payouts, the client must have approved the milestone, which triggers its payable status.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-028

#### 6.1.3.2 Dependency Reason

Vendor must have a profile with complete and valid payment information.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-061

#### 6.1.4.2 Dependency Reason

This story creates the 'Pending Approval' state that US-061 (Approve Payout) will act upon. It is a direct prerequisite.

## 6.2.0.0 Technical Dependencies

- Payment Service: To create and manage the state of the payout transaction.
- Project Service: To verify the project/milestone status.
- User Service: To authenticate and authorize the Finance Manager role.
- Audit Service: To log the initiation event.

## 6.3.0.0 Data Dependencies

- Requires access to project status, client transaction status (escrow balance), and vendor payment details.

## 6.4.0.0 External Dependencies

- None for initiation. The subsequent approval/payment steps will depend on Stripe Connect and Wise APIs.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response for initiating a payout must be < 250ms (p95) as per REQ-NFR-001.

## 7.2.0.0 Security

- The action must be strictly limited to users with the 'Finance Manager' role (REQ-SEC-001).
- All financial data must be transmitted over HTTPS/TLS 1.2+.
- The audit trail entry for this action is mandatory and must be immutable (REQ-FUN-005).
- Vendor payment details must be masked on the UI.

## 7.3.0.0 Usability

- The process should be intuitive, with clear feedback at each step (confirmation, success/failure).
- Error messages must be clear and actionable for the user.

## 7.4.0.0 Accessibility

- Must adhere to WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- Must function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires coordination and data validation across multiple services (Project, Payment, User).
- The business logic for checking all preconditions (project status, escrow funds, vendor details) is complex.
- The API endpoint must be idempotent to prevent accidental duplicate payout initiations.
- Database operations for creating the payout record must be transactional to ensure data integrity.

## 8.3.0.0 Technical Risks

- Potential for race conditions if client payment status changes while a payout is being initiated. Pessimistic or optimistic locking should be considered.
- Ensuring data consistency across the distributed services (Project and Payment) requires a robust communication pattern (e.g., Saga pattern).

## 8.4.0.0 Integration Points

- API Gateway for role-based access control.
- Internal API calls from the API Gateway to the Payment Service and Project Service.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify successful payout initiation for a full project.
- Verify successful payout initiation for a single milestone.
- Verify payout initiation is blocked for a project that is not completed.
- Verify payout initiation is blocked if client funds are not in escrow.
- Verify payout initiation is blocked if vendor payment details are missing.
- Verify a user without the Finance Manager role receives a 403 Forbidden error.

## 9.3.0.0 Test Data Needs

- Projects in various statuses ('Active', 'Completed', 'On Hold').
- Projects with and without corresponding client funds in escrow.
- Vendors with complete and incomplete payment details.
- User accounts with 'Finance Manager' and other roles.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other engineer
- Unit and integration tests implemented with >80% coverage for new code
- E2E tests for critical paths (happy path, key error conditions) are implemented and passing
- User interface reviewed for usability and adherence to design specifications
- Performance requirements (API latency) verified under test conditions
- Security requirements (role-based access, audit logging) validated
- API documentation (OpenAPI) is updated for any new or changed endpoints
- Story deployed and verified in the staging environment by QA or the product owner

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a critical part of the core financial workflow.
- It is a blocker for the 'Approve Payout' (US-061) story, which should be planned for the same or a subsequent sprint.

## 11.4.0.0 Release Impact

- Enables a key component of the end-to-end financial lifecycle. The vendor payment feature cannot be released without this functionality.

