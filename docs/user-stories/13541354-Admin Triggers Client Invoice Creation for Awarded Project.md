# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-056 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Triggers Client Invoice Creation for Awarded... |
| As A User Story | As a System Administrator, I want to trigger the c... |
| User Persona | System Administrator |
| Business Value | Initiates the core revenue collection process for ... |
| Functional Area | Financial Management & Project Lifecycle |
| Story Theme | Project Financial Workflow |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful invoice generation and sending for an awarded project

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in System Administrator viewing a project with the status 'Awarded', which has an associated client with a primary email contact, an accepted proposal amount, and a configured margin/fee.

### 3.1.5 When

I click the 'Generate and Send Invoice' button and confirm the action in the subsequent modal.

### 3.1.6 Then

The system must create a new invoice record linked to the project, calculate the total amount (proposal amount + margin/fee), create a 'Pending' transaction in the internal ledger, send an email to the client's primary contact with a secure Stripe payment link, display a success notification to me, and update the UI to show the invoice status as 'Sent - Awaiting Payment'.

### 3.1.7 Validation Notes

Verify the creation of the invoice record in the database. Check the mock email inbox (e.g., Mailtrap) for the correctly formatted email. Confirm the UI state change and success message. The project status must remain 'Awarded'.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Invoice trigger is not available for projects not in 'Awarded' status

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

I am a logged-in System Administrator viewing a project with a status of 'Proposed', 'Active', or 'Completed'.

### 3.2.5 When

I view the project's available actions.

### 3.2.6 Then

The 'Generate and Send Invoice' button must not be visible or must be disabled.

### 3.2.7 Validation Notes

Check the project workspace UI for projects in various states to ensure the action is only available for 'Awarded' projects.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempting to generate an invoice for a client with no primary email contact

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am a logged-in System Administrator viewing an 'Awarded' project whose associated client profile is missing a primary email contact.

### 3.3.5 When

I click the 'Generate and Send Invoice' button.

### 3.3.6 Then

The system must prevent the action and display a user-friendly error message, such as 'Cannot send invoice: Client contact email is missing. Please update the client profile.'

### 3.3.7 Validation Notes

Set up a test client with a null email field. Attempt the action and verify the specific error message is displayed and no invoice is generated or sent.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Attempting to generate an invoice for a project with missing financial configuration

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am a logged-in System Administrator viewing an 'Awarded' project for which the margin/fee has not been set.

### 3.4.5 When

I click the 'Generate and Send Invoice' button.

### 3.4.6 Then

The system must prevent the action and display an error message, such as 'Cannot generate invoice: Project margin is not set. Please configure the project's financial details.'

### 3.4.7 Validation Notes

Ensure a project's margin field is null. Verify the action is blocked and the correct error message is shown.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

System handles failure of external email or payment gateway service

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am a logged-in System Administrator and I trigger an invoice for a valid 'Awarded' project.

### 3.5.5 When

The external API call to AWS SES or Stripe Connect fails.

### 3.5.6 Then

The entire operation must be rolled back. No invoice record should be persisted in a 'sent' state, and no partial email should be sent. The system must display a clear error message to me, including a correlation ID for support, e.g., 'Failed to send invoice due to a communication error. Please try again later. [Correlation ID]'.

### 3.5.7 Validation Notes

Use API mocks to simulate a 500 error from Stripe or SES. Verify that the database state is unchanged and the UI shows the correct error message.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Preventing duplicate initial invoice generation

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

I am a logged-in System Administrator viewing an 'Awarded' project for which an initial invoice has already been successfully sent.

### 3.6.5 When

I view the project's available actions.

### 3.6.6 Then

The 'Generate and Send Invoice' button must be disabled or replaced with a 'Resend Invoice' option to prevent creating a duplicate initial invoice.

### 3.6.7 Validation Notes

After successfully triggering an invoice once, refresh the page and confirm the original 'Generate' button is no longer active.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A clearly labeled button, 'Generate and Send Invoice', on the project workspace page.
- A confirmation modal with 'Confirm' and 'Cancel' actions that displays the client name and calculated invoice amount.
- A non-intrusive success notification (toast) upon successful sending.
- A clear, modal, or inline error message display area for failures.
- A status indicator on the project page to show 'Invoice Sent on [Date] - Awaiting Payment'.

## 4.2.0 User Interactions

- Clicking the button should open the confirmation modal.
- Confirming the modal triggers the backend process.
- The UI should provide immediate feedback while the backend process is running (e.g., loading spinner on the button).
- The button's state should update to disabled or be replaced by the status indicator after a successful action.

## 4.3.0 Display Requirements

- The confirmation modal must display the final calculated invoice amount and the recipient client's name.
- Error messages must be clear, actionable, and provide a correlation ID for support if applicable.

## 4.4.0 Accessibility Needs

- The button and modal must be fully keyboard accessible (operable with Tab, Enter, Esc).
- All UI elements must have appropriate ARIA labels for screen reader compatibility.
- Success and error notifications must be announced by screen readers using ARIA live regions.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-FIN-01

### 5.1.2 Rule Description

An initial project invoice can only be generated for projects in the 'Awarded' state.

### 5.1.3 Enforcement Point

API endpoint and UI layer.

### 5.1.4 Violation Handling

The action is not available in the UI. The API will return a 403 Forbidden or 400 Bad Request error if called directly for a project in an invalid state.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-FIN-02

### 5.2.2 Rule Description

The invoice amount is calculated as the sum of the accepted proposal amount and the configured project margin/fee.

### 5.2.3 Enforcement Point

Backend Payment Service during invoice creation.

### 5.2.4 Violation Handling

If any component is missing (proposal amount, margin), the operation fails with an internal server error and a descriptive log message.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-AUD-01

### 5.3.2 Rule Description

The action of generating and sending a client invoice must be recorded in the immutable audit trail.

### 5.3.3 Enforcement Point

Backend service after successful completion of the workflow.

### 5.3.4 Violation Handling

If logging fails, the transaction should ideally be rolled back, or at minimum, a high-priority alert should be triggered for manual review.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-029

#### 6.1.1.2 Dependency Reason

A project entity must exist to associate the invoice with.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-054

#### 6.1.2.2 Dependency Reason

A project must be moved to the 'Awarded' status by accepting a proposal, which is the trigger state for this story.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-068

#### 6.1.3.2 Dependency Reason

A mechanism to configure default margins is required for invoice calculation.

## 6.2.0.0 Technical Dependencies

- Payment Service: For creating Stripe payment links and recording transactions.
- Notification Service: For sending emails via AWS SES.
- Project Service: For managing project state.
- Audit Service: For logging the action.

## 6.3.0.0 Data Dependencies

- A project record in 'Awarded' status.
- An associated client record with a valid primary contact email.
- An accepted proposal record with a final amount.
- A configured margin/fee value for the project.

## 6.4.0.0 External Dependencies

- Stripe Connect API: Must be configured with valid API keys to create payment links.
- AWS Simple Email Service (SES) API: Must be configured to send transactional emails.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The synchronous API response (acknowledging the request) must be < 500ms.
- The end-to-end process, including email delivery, should complete within 15 seconds.

## 7.2.0.0 Security

- The action must be restricted to users with the 'System Administrator' role.
- The generated payment link must be a unique, secure, and time-limited token.
- All communication with external APIs (Stripe, SES) must be over HTTPS.
- The invoice generation event must be logged in the audit trail as per REQ-FUN-005.

## 7.3.0.0 Usability

- The process should require no more than three clicks (View Project -> Click Generate -> Confirm).
- Feedback to the user (success/error) must be immediate and clear.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must be fully functional on the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires orchestration across multiple internal services (Project, Payment, Notification).
- Integration with two external APIs (Stripe, SES) introduces external failure points.
- The need for a transactional or Saga pattern to ensure atomicity and handle failures gracefully.
- Requires an email templating solution for the invoice notification.

## 8.3.0.0 Technical Risks

- Potential for race conditions if the user clicks the button multiple times quickly.
- Failure to properly handle API errors from Stripe/SES could lead to inconsistent data states (e.g., invoice recorded internally but never sent).

## 8.4.0.0 Integration Points

- POST /api/v1/projects/{projectId}/invoice
- Stripe Connect API: To create a Payment Link or Payment Intent.
- AWS SES API: To send a templated email.
- Internal Database: To create/update records in `invoices` and `transactions` tables.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify the happy path E2E flow from button click to email receipt.
- Test all error conditions listed in the acceptance criteria (missing data, wrong project state).
- Integration tests with mocked API failures from Stripe and SES to verify rollback logic.
- Security tests to ensure only authorized users can perform the action.
- Manual testing of the UI/UX flow, including the confirmation modal and feedback messages.

## 9.3.0.0 Test Data Needs

- A test project in the 'Awarded' state.
- A test client with a valid email address accessible by QA.
- A test client with a null email address.
- A test project with no margin configured.

## 9.4.0.0 Testing Tools

- Jest (Unit/Integration)
- Playwright (E2E)
- Mailtrap/MailHog (for email verification)
- Postman/Insomnia (for API-level testing)

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration tests implemented with >80% coverage for new code and all are passing
- E2E test for the primary happy path is implemented and passing
- All error conditions and edge cases manually tested and verified by QA
- Performance requirements (API response time) are met under test conditions
- Security requirements (role-based access) are validated
- API documentation (OpenAPI spec) is updated for the new endpoint
- Story deployed and verified in the staging environment by the product owner or QA

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- Requires access to dev/staging credentials for Stripe and AWS SES.
- Dependent on the completion of core project and proposal management stories.
- The team should agree on the Saga implementation or error handling strategy for the distributed transaction before starting development.

## 11.4.0.0 Release Impact

This is a critical feature for the financial workflow. The system cannot go live without this capability.

