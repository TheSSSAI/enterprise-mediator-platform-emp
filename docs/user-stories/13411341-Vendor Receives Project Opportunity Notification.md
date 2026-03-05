# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-043 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Vendor Receives Project Opportunity Notification |
| As A User Story | As a Vendor Contact, I want to receive an immediat... |
| User Persona | Vendor Contact (External User) |
| Business Value | Initiates the core business workflow of sourcing p... |
| Functional Area | Proposal and Project Workflow |
| Story Theme | Vendor Engagement and Proposal Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Vendor receives notification for a new project opportunity

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A System Admin has approved a Project Brief for 'Project Phoenix'

### 3.1.5 And

The secure link must correctly route the user to the proposal submission portal for 'Project Phoenix'.

### 3.1.6 When

The System Admin distributes the Project Brief to 'Innovate Solutions'

### 3.1.7 Then

The system must generate a unique, cryptographically secure, time-limited (e.g., 14 days) access token associated with 'Innovate Solutions' and 'Project Phoenix'

### 3.1.8 Validation Notes

Verify in a staging environment using an email capture service (e.g., Mailtrap) that the email is received, its content is correct, and the link is functional.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Error Condition: Email service fails to send the notification

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

The System Admin distributes a Project Brief to a vendor

### 3.2.5 And

The project's distribution status for that vendor should indicate a temporary failure (e.g., 'Sending Failed').

### 3.2.6 When

The Notification Service attempts to send the email

### 3.2.7 Then

The system must log the delivery failure with the specific error message and a correlation ID

### 3.2.8 Validation Notes

Mock the AWS SES API to return a 503 error and verify that the retry logic is triggered and the system state is updated correctly.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Edge Case: Vendor has multiple contacts

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

A vendor profile has two active contacts with emails 'contact1@vendor.com' and 'contact2@vendor.com'

### 3.3.5 When

A System Admin distributes a Project Brief to this vendor

### 3.3.6 Then

The system must send the notification email to both 'contact1@vendor.com' and 'contact2@vendor.com'

### 3.3.7 And

Each email must contain the same unique secure link for that project invitation.

### 3.3.8 Validation Notes

Configure a test vendor with multiple contacts and verify that both email addresses receive the notification.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Security: Vendor clicks an expired or invalid link

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

A vendor received a project opportunity email with a secure link that has a 14-day expiry

### 3.4.5 When

The vendor contact clicks the link on the 15th day

### 3.4.6 Then

The system must validate the token, determine it is expired, and redirect the user to a page with a clear error message, such as 'This project invitation link has expired. Please contact the administrator if you believe this is an error.'

### 3.4.7 Validation Notes

Manually expire a token in the database or use a time-manipulation library in an automated test to simulate expiry and verify the user is shown the correct error page.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Security: Vendor was deactivated after receiving the invitation

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

A vendor received a project opportunity email with a valid secure link

### 3.5.5 And

The user must be redirected to a page indicating the opportunity is no longer available.

### 3.5.6 When

The vendor contact clicks the valid, unexpired secure link

### 3.5.7 Then

The system must check the vendor's current status upon link validation and deny access

### 3.5.8 Validation Notes

Follow the steps in an E2E test: invite vendor, deactivate vendor, click link, and assert the correct error page is displayed.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Responsive HTML Email Template
- Clear Subject Line
- Company Branding/Logo in Email Header
- Personalized Greeting (e.g., 'Hello [Vendor Name]')
- Prominent Call-to-Action Button (e.g., 'View Project Details')
- Email Footer with contact information and unsubscribe options (if legally required)

## 4.2.0 User Interactions

- User clicks a link/button within the email to be taken to the proposal portal.

## 4.3.0 Display Requirements

- Email must display the Project Name or a unique Project Identifier.
- Email must clearly state the action required from the vendor.
- If the link has an expiry, the expiry date should be mentioned in the email.

## 4.4.0 Accessibility Needs

- Email HTML must use semantic tags for screen reader compatibility.
- Call-to-action buttons must have sufficient color contrast.
- All images must have descriptive alt-text.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Project opportunity notifications can only be sent to vendors with a status of 'Active'.

### 5.1.3 Enforcement Point

During the vendor selection process in the 'Distribute Project Brief' UI (covered in US-042). This story assumes the check has passed.

### 5.1.4 Violation Handling

The system should not allow non-active vendors to be selected for distribution.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The secure access token must be unique for each combination of project and invited vendor.

### 5.2.3 Enforcement Point

Token generation logic within the Notification or Project Service.

### 5.2.4 Violation Handling

Token generation fails, an error is logged, and the notification is not sent.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-026

#### 6.1.1.2 Dependency Reason

A vendor must be in an 'Active' state to be eligible for project invitations.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-036

#### 6.1.2.2 Dependency Reason

A Project Brief must be finalized and approved before it can be distributed.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-042

#### 6.1.3.2 Dependency Reason

This story is triggered by the 'Distribute Project Brief' action defined in US-042.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-044

#### 6.1.4.2 Dependency Reason

The secure link must lead to the proposal submission portal, which is built in US-044.

## 6.2.0.0 Technical Dependencies

- AWS Simple Email Service (SES) API integration must be configured and available.
- A robust email templating engine (e.g., Handlebars, Pug) must be integrated.
- An asynchronous task queue (e.g., AWS SQS) is required for reliable, non-blocking email dispatch and retries.
- A secure token generation and validation mechanism (e.g., JWT or database-backed tokens).

## 6.3.0.0 Data Dependencies

- Requires access to Vendor profile data, specifically contact email addresses and status.
- Requires access to Project data, specifically the Project Name/ID.

## 6.4.0.0 External Dependencies

- Relies on the availability and performance of AWS SES.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The email notification job should be queued in under 500ms from the admin's action.
- The email should be delivered to the recipient's mail server within 60 seconds of the job being processed.

## 7.2.0.0 Security

- The secure link token must be generated using a cryptographically secure pseudo-random number generator (CSPRNG).
- The token must be invalidated after its first use to access the portal or after its expiry date.
- The communication with AWS SES must be over HTTPS.
- The system must not log the full secure token in plain text.

## 7.3.0.0 Usability

- The email content must be clear, concise, and free of jargon.
- The call-to-action must be obvious and easy to find.

## 7.4.0.0 Accessibility

- The HTML email template must adhere to WCAG 2.1 Level AA guidelines for email.

## 7.5.0.0 Compatibility

- The HTML email must render correctly on the latest versions of major email clients (Gmail, Outlook, Apple Mail) on both desktop and mobile.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Integration with an external service (AWS SES).
- Implementation of a reliable, asynchronous queuing and retry mechanism.
- Secure token generation, storage, and validation logic.
- Cross-client compatibility testing for HTML emails can be time-consuming.

## 8.3.0.0 Technical Risks

- Potential for emails to be marked as spam. Requires proper SES configuration (SPF, DKIM records).
- Handling failures and retries gracefully to avoid duplicate emails or lost notifications.

## 8.4.0.0 Integration Points

- Project Service: Triggers the notification event.
- Notification Service: Consumes the event, generates the token, and sends the email.
- AWS SES: The external service that delivers the email.
- Frontend/API Gateway: The endpoint that validates the secure token when the vendor clicks the link.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify email content and link for a single-contact vendor.
- Verify emails are sent to all contacts for a multi-contact vendor.
- Simulate SES API failure and verify retry logic.
- Test clicking a valid link, an expired link, and an already-used link.
- Test clicking a link for a project whose associated vendor has been deactivated.

## 9.3.0.0 Test Data Needs

- Test vendors with 'Active' and 'Deactivated' statuses.
- Test vendors with single and multiple contact emails.
- An approved Project Brief ready for distribution.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Mailtrap.io or similar for capturing and inspecting emails in staging.
- Playwright for E2E tests that can fetch the link from the captured email and navigate to the portal.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration tests implemented with >80% code coverage for new logic
- E2E test for the happy path is implemented and passing in the CI/CD pipeline
- Email template reviewed and approved by Product/UX for content and responsiveness
- Security review of token generation/validation logic is completed and any findings are addressed
- Documentation for the notification event and email template variables is created/updated
- Story deployed and verified in the staging environment by QA

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a critical enabler for the entire proposal workflow.
- Requires coordination with frontend work for the destination page (US-044).
- DevOps support may be needed to configure DNS (SPF/DKIM) for AWS SES to ensure email deliverability.

## 11.4.0.0 Release Impact

Blocks the ability for vendors to submit proposals. Must be included in the release that enables the proposal workflow.

