# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-057 |
| Elaboration Date | 2025-01-17 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Client Receives Invoice Email with Secure Payment ... |
| As A User Story | As a Client Contact, I want to receive a professio... |
| User Persona | Client Contact. This is an external user with limi... |
| Business Value | Initiates the payment process, which is critical f... |
| Functional Area | Financial Management & Accounting |
| Story Theme | Project Financial Workflow |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Successful Invoice Email Delivery

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A project is in the 'Awarded' state and has a primary Client Contact with a valid email address

### 3.1.5 When

A System Admin triggers the 'Send Invoice' action for that project

### 3.1.6 Then

An email is asynchronously sent to the primary Client Contact's registered email address via AWS SES.

### 3.1.7 Validation Notes

Verify in a test environment using an email capture tool (e.g., Mailtrap) that the email is generated and sent. The email sending process must not block the Admin's UI.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Email Content and Formatting

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

An invoice email has been generated

### 3.2.5 When

The Client Contact opens the email

### 3.2.6 Then

The email subject line clearly states 'Invoice [Invoice_Number] for Project [Project_Name] from [Our_Company_Name]'.

### 3.2.7 Validation Notes

Check the subject line for correct dynamic data insertion.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Email Body Contains Correct Information

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

An invoice email has been generated

### 3.3.5 When

The Client Contact views the email body

### 3.3.6 Then

The email body must be a responsive HTML template containing the Client's Name, Project Name, Invoice Number, Total Amount Due (with currency symbol/code), and the Due Date.

### 3.3.7 Validation Notes

Verify all specified data points are present and accurate. Check rendering on both desktop and mobile email clients.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Secure Payment Link Generation

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

An invoice email has been generated

### 3.4.5 When

The Client Contact views the email

### 3.4.6 Then

The email contains a prominent Call-to-Action (CTA) button labeled 'View and Pay Invoice' which links to a unique, secure, and time-limited URL.

### 3.4.7 Validation Notes

The URL must be generated using a secure token (e.g., JWT) with a short expiry (e.g., 72 hours) and a specific scope ('invoice:pay'). The token should not expose internal database IDs.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Email Service Transient Failure

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

An Admin triggers an invoice to be sent

### 3.5.5 When

The external email service (AWS SES) is temporarily unavailable or returns a retryable error

### 3.5.6 Then

The email sending job is placed into a dead-letter queue after a configurable number of retries (e.g., 3 attempts over 1 hour).

### 3.5.7 Validation Notes

Simulate an SES API failure. Verify that the job is queued in SQS and retried. After max retries, verify it moves to the DLQ and an alert is triggered for an administrator.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Bounced Email Handling

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

An invoice email has been sent to an invalid email address

### 3.6.5 When

The system receives a hard bounce notification from AWS SES via a configured webhook (SNS/SQS)

### 3.6.6 Then

The system flags the Client Contact's email address as invalid in the database.

### 3.6.7 Validation Notes

Use an AWS SES test email address that simulates a hard bounce. Verify that the webhook is triggered and the system correctly updates the user's status and notifies the Admin.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Branded HTML Email Template
- Company Logo in Header
- Clear Subject Line
- Prominent CTA Button ('View and Pay Invoice')
- Professional Footer with Company Details

## 4.2.0 User Interactions

- The only interaction is the client clicking the secure link in the email.

## 4.3.0 Display Requirements

- Client Name
- Project Name
- Invoice Number
- Total Amount Due (with currency)
- Due Date

## 4.4.0 Accessibility Needs

- The HTML email template must use semantic HTML.
- The CTA button must have a clear, descriptive text.
- Alt text must be provided for images like the company logo.
- Sufficient color contrast must be used in the email design.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

An invoice email can only be sent for a project that is in the 'Awarded' state.

### 5.1.3 Enforcement Point

Backend service logic before queuing the email job.

### 5.1.4 Violation Handling

The 'Send Invoice' action is disabled or returns an error if the project is not in the correct state.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The secure payment link must expire after a configurable period (e.g., 72 hours) to enhance security.

### 5.2.3 Enforcement Point

Token validation logic on the payment page.

### 5.2.4 Violation Handling

If the link is expired, the user is shown a page explaining the link has expired and providing instructions to request a new one.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-012

#### 6.1.1.2 Dependency Reason

A Client profile with a valid contact email address must exist to receive the invoice.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-056

#### 6.1.2.2 Dependency Reason

This story is triggered by the completion of US-056, which creates the invoice data object that this story uses to populate the email.

## 6.2.0.0 Technical Dependencies

- AWS Simple Email Service (SES) must be configured with a verified domain.
- An asynchronous job queue (AWS SQS) is required for reliable, non-blocking email dispatch.
- A secure token generation service (e.g., using JWT) is needed for the payment link.
- A Notification microservice to handle email templating and dispatch logic.

## 6.3.0.0 Data Dependencies

- A finalized Invoice data record containing all necessary financial details.
- A Client Contact record with a valid, verified email address.
- System configuration for email 'from' address and branding assets.

## 6.4.0.0 External Dependencies

- Successful delivery is dependent on the AWS SES platform and the client's email provider not blocking the email.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The email sending job should be queued in under 500ms from the trigger event.
- The email should be delivered to the client's inbox within 60 seconds of being sent by the system under normal conditions.

## 7.2.0.0 Security

- The payment link must use a cryptographically secure, short-lived token (JWT).
- The communication with AWS SES must be over HTTPS.
- The system must not log sensitive PII from the email body in plain text.

## 7.3.0.0 Usability

- The email must be clear, concise, and immediately understandable to a non-technical user.
- The CTA must be obvious and easy to click.

## 7.4.0.0 Accessibility

- The email template must adhere to WCAG 2.1 Level AA guidelines for email.

## 7.5.0.0 Compatibility

- The HTML email must render correctly on the latest versions of major email clients: Gmail (Web/Mobile), Outlook (Web/Desktop), and Apple Mail (Desktop/Mobile).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires creating a robust, cross-client compatible HTML email template.
- Implementation of a secure, time-limited token generation and validation mechanism.
- Asynchronous processing architecture using SQS for reliability and retries.
- Configuration of AWS SES and webhooks for bounce/complaint handling.

## 8.3.0.0 Technical Risks

- Emails being marked as spam by client email providers. Requires proper SES setup (SPF, DKIM, DMARC).
- Difficulty in creating a single HTML template that renders perfectly across all major email clients.

## 8.4.0.0 Integration Points

- Project Service: To get project and invoice data.
- User Service: To get Client Contact email.
- Notification Service: To manage templating and sending.
- AWS SES API: To dispatch the email.
- AWS SQS: To queue the email job.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify email content and link for a standard invoice.
- Test email rendering on different clients (using a tool like Litmus or Email on Acid).
- Test the retry mechanism by mocking an SES API failure.
- Test the bounce handling webhook.
- Test clicking an expired link to ensure the correct error page is shown.

## 9.3.0.0 Test Data Needs

- Test projects in the 'Awarded' state.
- Test client accounts with valid and invalid (bouncing) email addresses.
- Invoice data with different currencies and amounts.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Mailtrap or MailHog for capturing and inspecting emails in test environments.
- Playwright for E2E tests that can retrieve the link from the captured email and navigate to the page.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and integration tests implemented with >80% coverage and passing
- E2E test scenario for receiving email and validating link is implemented and passing
- Email template reviewed and approved for content and cross-client compatibility
- Security requirements for the tokenized link are validated
- Documentation for the notification event and email template is created/updated
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a critical path story for the core financial workflow.
- Requires prior setup and configuration of AWS SES, which may be an out-of-band task.
- Blocks the development of the client payment story (US-058).

## 11.4.0.0 Release Impact

This feature is essential for the Minimum Viable Product (MVP) as it enables the core revenue-generating function of the platform.

