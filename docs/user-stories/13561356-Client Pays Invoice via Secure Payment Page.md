# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-058 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Client Pays Invoice via Secure Payment Page |
| As A User Story | As a Client Contact, I want to pay a project invoi... |
| User Persona | Client Contact. This is an external user who is no... |
| Business Value | Enables the core revenue collection mechanism for ... |
| Functional Area | Financial Management & Accounting |
| Story Theme | Project Financial Workflow |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful Payment via Valid Link

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A Client Contact has received an invoice email with a valid, unique, and unexpired payment link for a project in the 'Awarded' state

### 3.1.5 When

The user clicks the link and enters valid payment details into the Stripe-hosted form and clicks 'Pay Now'

### 3.1.6 Then

The payment is successfully processed by Stripe, a success message is displayed on the page, a payment receipt is emailed to the client, the project status is updated to 'Active', a notification is sent to the System Admin, and the transaction is recorded in the audit trail and internal ledger.

### 3.1.7 Validation Notes

Verify in Stripe's dashboard that the payment was received. Check the project's status in the database has changed to 'Active'. Confirm the Admin notification was sent. Check the client's email for the receipt. Verify the audit log contains the transaction record.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Payment Declined by Gateway

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

A Client Contact is on the secure payment page

### 3.2.5 When

The user enters payment details that are declined by the payment gateway (e.g., insufficient funds, incorrect CVC)

### 3.2.6 Then

A clear, user-friendly error message is displayed on the page (e.g., 'Your card was declined. Please check your details or try another card.') without revealing sensitive information, the payment form fields are preserved (except CVC), and the user can attempt payment again.

### 3.2.7 Validation Notes

Use Stripe's test cards for declined payments to trigger this flow. Verify the error message is displayed and the project status remains 'Awarded'.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Accessing with an Invalid or Expired Link

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

A Client Contact has a payment link that is invalid, has expired, or corresponds to an already paid invoice

### 3.3.5 When

The user attempts to access the URL

### 3.3.6 Then

The user is shown a dedicated error page explaining the link is no longer valid and provides clear instructions on how to request a new one from their contact.

### 3.3.7 Validation Notes

Attempt to access the payment URL after the invoice has been paid. Manually invalidate a token in the database and attempt to access the URL.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Network Error During Payment Submission

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

A Client Contact has entered valid payment details

### 3.4.5 When

The user clicks 'Pay Now' and a network interruption occurs between the client and the server, or the server and Stripe

### 3.4.6 Then

The UI displays a non-alarming error message (e.g., 'A network error occurred. Please check your connection and try again.') and the system ensures no duplicate payment is processed upon retry by using an idempotency key with the payment gateway.

### 3.4.7 Validation Notes

Simulate a network failure using browser developer tools. Verify that retrying the submission results in only one successful charge in the Stripe dashboard.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Payment Page Display and Security

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

A Client Contact accesses a valid payment link

### 3.5.5 When

The payment page loads

### 3.5.6 Then

The page is served over HTTPS, is branded with the company logo, is fully responsive, and clearly displays the invoice number, project name, and the total amount due with the correct currency symbol/code.

### 3.5.7 Validation Notes

Inspect the page on desktop and mobile viewport sizes. Verify the SSL certificate is valid. Confirm all required invoice details are present and accurate.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Company Logo Header
- Invoice Details Section (Invoice #, Project Name, Amount Due)
- Stripe Elements integrated payment form (Card Number, Expiry, CVC)
- 'Pay Now' button with a loading/processing indicator
- Success message container
- Error message container

## 4.2.0 User Interactions

- The 'Pay Now' button should be disabled until all required payment fields are validly formatted.
- Upon submission, the 'Pay Now' button should enter a disabled/loading state to prevent multiple submissions.
- Error messages should appear close to the relevant fields or in a prominent summary location.

## 4.3.0 Display Requirements

- The payment amount must be formatted according to the currency's locale (e.g., $1,234.56, €1.234,56).
- The page must be clean, professional, and inspire trust, consistent with the '2040' aesthetic.

## 4.4.0 Accessibility Needs

- The payment form must adhere to WCAG 2.1 Level AA standards.
- All form fields must have associated labels for screen readers.
- Error messages must be programmatically associated with their respective fields.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A project's status can only be moved to 'Active' after the initial client invoice has been successfully paid.

### 5.1.3 Enforcement Point

Backend webhook handler processing the 'payment_intent.succeeded' event from Stripe.

### 5.1.4 Violation Handling

If the payment fails, the status remains 'Awarded'. The system logs the failed attempt but does not change the project state.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A payment link must be single-use. Once an invoice is paid, its associated payment link becomes invalid.

### 5.2.3 Enforcement Point

API endpoint that serves the payment page.

### 5.2.4 Violation Handling

If the invoice status is already 'Paid', the server responds with an error page as defined in AC-003.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-056

#### 6.1.1.2 Dependency Reason

An invoice must be created by an Admin before a client can receive a link to pay it.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-057

#### 6.1.2.2 Dependency Reason

The client must receive the invoice email containing the secure payment link to initiate this workflow.

## 6.2.0.0 Technical Dependencies

- Stripe Connect API integration for PaymentIntents and webhooks.
- AWS SES integration for sending payment receipts.
- Internal Notification Service for alerting Admins.
- Project Service for state transition management.
- Payment Service for internal ledger updates.

## 6.3.0.0 Data Dependencies

- Requires an 'Invoice' entity in the database with a status, amount, currency, and a unique, secure token for the payment link.

## 6.4.0.0 External Dependencies

- Stripe API availability.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The payment page must achieve a Largest Contentful Paint (LCP) of under 2.5 seconds.
- The server-side processing of the payment submission (API call to Stripe) should have a p95 latency of under 500ms.

## 7.2.0.0 Security

- All communication must be over HTTPS (TLS 1.2+).
- The implementation must use Stripe Elements to ensure PCI DSS compliance, meaning no raw credit card data ever touches the application servers.
- The unique payment link token must be cryptographically secure and non-guessable.
- The webhook endpoint receiving updates from Stripe must be secured and verify the authenticity of incoming requests using Stripe's webhook signatures.

## 7.3.0.0 Usability

- The payment process should be completable in minimal steps with clear instructions.
- Error messages must be clear, concise, and helpful.

## 7.4.0.0 Accessibility

- Must meet WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The payment page must be fully functional on the latest two major versions of Chrome, Firefox, Safari, and Edge on both desktop and mobile.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires both frontend (React with Stripe Elements) and backend (NestJS) work.
- Implementing a robust, idempotent webhook handler for asynchronous payment status updates is critical and complex.
- Secure token generation and management for the payment links.
- Orchestration of multiple service updates upon successful payment (Project, Payment, Notification, Audit).

## 8.3.0.0 Technical Risks

- Improperly implemented webhook handler could lead to missed payments or duplicate processing.
- Failure to properly secure the webhook endpoint could allow for fraudulent status updates.

## 8.4.0.0 Integration Points

- Frontend -> Backend API (to get PaymentIntent client secret)
- Frontend -> Stripe API (to confirm payment)
- Stripe -> Backend Webhook (for status updates)
- Backend -> Project Service, Payment Service, Notification Service

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Successful payment with a valid test card.
- Declined payment with various Stripe decline test cards.
- Attempting to access a paid/invalid link.
- Verifying email receipt content and delivery.
- Verifying Admin notification.
- E2E test of the entire flow from clicking the email link to seeing the success page and verifying the project status change in the backend.

## 9.3.0.0 Test Data Needs

- A project in the 'Awarded' state with a generated invoice.
- Stripe API test keys.
- Stripe's official set of test credit card numbers for various scenarios (success, decline, fraud).

## 9.4.0.0 Testing Tools

- Jest (Unit/Integration)
- Playwright (E2E)
- Stripe CLI for local webhook testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing in a staging environment.
- Code has been peer-reviewed and merged into the main branch.
- Unit and integration tests are written and achieve >80% code coverage for new logic.
- E2E tests for all critical paths are implemented and passing.
- UI has been reviewed for responsiveness and adherence to design specifications.
- Security review completed, confirming PCI compliance via Stripe Elements and webhook signature validation.
- Accessibility audit (automated and manual) passed for WCAG 2.1 AA.
- All related documentation (API specs, user guides) has been updated.
- Story has been successfully deployed and verified in the staging environment.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a cornerstone of the financial workflow and has several dependencies. It should be prioritized after its prerequisites (US-056, US-057) are complete.
- Requires a developer with experience in both frontend (React) and backend (NestJS), as well as payment gateway integrations.

## 11.4.0.0 Release Impact

This feature is essential for the Minimum Viable Product (MVP) as it enables the core business function of collecting revenue.

