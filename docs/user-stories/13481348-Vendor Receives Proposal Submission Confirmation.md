# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-050 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Vendor Receives Proposal Submission Confirmation |
| As A User Story | As a Vendor Contact, I want to receive an immediat... |
| User Persona | Vendor Contact: An external user representing a ve... |
| Business Value | Improves vendor trust and experience by providing ... |
| Functional Area | Proposal Management |
| Story Theme | Vendor Workflow and Communication |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Successful proposal submission triggers confirmation email

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A Vendor Contact is on the proposal submission page for a project named 'Project Phoenix'

### 3.1.5 When

The vendor successfully submits their proposal through the portal

### 3.1.6 Then

An email is immediately queued to be sent to the Vendor Contact's registered email address.

### 3.1.7 Validation Notes

Verify in a test environment that submitting a proposal triggers an email sending event. Use a tool like Mailtrap or AWS SES logs to confirm the email was sent.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Email content is accurate and informative

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

A confirmation email has been generated for a proposal submitted for 'Project Phoenix' by 'Innovate Solutions Inc.'

### 3.2.5 When

The Vendor Contact opens the confirmation email

### 3.2.6 Then

The email subject line is 'Proposal Submission Confirmation for Project Phoenix'.

### 3.2.7 And

The email includes a professional closing and a message setting expectations, such as 'Our team will review your proposal and you will be notified of any status updates.'

### 3.2.8 Validation Notes

Check the content of the received test email against the requirements. Ensure the timestamp is accurate and in a readable format.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Proposal submission fails, no email is sent

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

A Vendor Contact is on the proposal submission page

### 3.3.5 When

The vendor attempts to submit a proposal, but the submission fails due to a server error or validation failure

### 3.3.6 Then

No confirmation email is sent to the Vendor Contact.

### 3.3.7 And

The user is shown an error message on the UI.

### 3.3.8 Validation Notes

Simulate a submission failure (e.g., by violating a server-side validation rule) and verify that no email sending event is triggered.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Email service is temporarily unavailable

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

A Vendor Contact successfully submits a proposal

### 3.4.5 And

The email is sent successfully once the email service is restored.

### 3.4.6 When

The system attempts to send the confirmation email

### 3.4.7 Then

The proposal is still successfully saved in the database.

### 3.4.8 Validation Notes

In a staging environment, mock the email service to return a transient error (e.g., HTTP 503). Verify that the job is queued (e.g., in SQS) and that logs reflect the failure and retry attempt.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- This story primarily concerns an email, not a UI. However, it is related to the success message on the proposal submission page.

## 4.2.0 User Interactions

- The user action is submitting the proposal, which triggers this backend process.

## 4.3.0 Display Requirements

- The email must be formatted in HTML for professional presentation and readability across common email clients (Gmail, Outlook).
- A plain text version of the email must also be included for compatibility.

## 4.4.0 Accessibility Needs

- The HTML email should use semantic HTML and have sufficient color contrast to be readable.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'Confirmation emails are only sent for successfully processed proposal submissions.', 'enforcement_point': 'The event listener that triggers the notification service.', 'violation_handling': "If a submission fails, the 'ProposalSubmitted' event is not published, thus no email is sent."}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-049

#### 6.1.1.2 Dependency Reason

This story is triggered by the successful completion of the proposal submission process defined in US-049.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-004

#### 6.1.2.2 Dependency Reason

Requires a Vendor Contact to have a registered account with a verified email address to which the confirmation can be sent.

## 6.2.0.0 Technical Dependencies

- A configured and operational Notification Service.
- Integration with an email sending provider (e.g., AWS SES).
- An event bus and message queue system (e.g., AWS SNS/SQS) for asynchronous, reliable processing.
- A centralized email templating engine/service.

## 6.3.0.0 Data Dependencies

- Access to the Vendor Contact's registered email address.
- Access to the Project Name and Vendor Company Name associated with the submitted proposal.

## 6.4.0.0 External Dependencies

- The reliability of the third-party email sending service (e.g., AWS SES).

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The email sending job must be queued within 500ms of a successful proposal submission.
- The email should be delivered to the recipient's mail server within 2 minutes under normal conditions.

## 7.2.0.0 Security

- The email must be sent from a verified, trusted domain with proper SPF, DKIM, and DMARC records to minimize the risk of being marked as spam.
- The email content must not expose any sensitive internal data or PII beyond what is necessary for confirmation.

## 7.3.0.0 Usability

- The email content must be clear, concise, and professional, providing immediate value and setting correct expectations for the vendor.

## 7.4.0.0 Accessibility

- The HTML email must adhere to WCAG 2.1 Level AA guidelines for color contrast and structure.

## 7.5.0.0 Compatibility

- The HTML email should render correctly on major desktop and mobile email clients, including Gmail, Outlook, and Apple Mail.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires asynchronous processing using a message queue to ensure the proposal submission API call remains fast and is not blocked by email sending.
- Implementation of robust retry logic and error handling for the external email service.
- Creation and management of a version-controlled, internationalization-ready email template.
- Ensuring proper email deliverability configurations (SPF, DKIM) are in place.

## 8.3.0.0 Technical Risks

- Poor email deliverability (emails landing in spam folders).
- Failure to handle transient errors from the email service could lead to missed notifications.
- Hard-coded email content would make future changes difficult and error-prone.

## 8.4.0.0 Integration Points

- Project Service: Publishes the 'ProposalSubmitted' event.
- Notification Service: Subscribes to the 'ProposalSubmitted' event.
- External Email Service (AWS SES): The Notification Service calls this API to send the email.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify email is sent upon successful submission.
- Verify email content, including dynamic data (project name, vendor name, timestamp).
- Verify no email is sent upon failed submission.
- Test the retry mechanism by mocking a failure from the email service API.
- Check email rendering in major email clients.

## 9.3.0.0 Test Data Needs

- A test vendor account with a valid, accessible email address.
- A test project to submit a proposal against.
- Sample proposal data.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.
- A mock email service or a tool like Mailtrap/MailHog to intercept and inspect outgoing emails in test environments.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and integration tests implemented with >80% coverage for new code
- E2E test for the happy path scenario is implemented and passing
- Email template is stored in the templating system and has been reviewed for content and branding
- Asynchronous processing with retry logic is implemented and verified
- Logging and monitoring for email sending failures are in place
- Documentation for the new notification event is created/updated
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story provides critical user feedback and should be prioritized to be completed in the same sprint as or immediately after US-049 (Vendor Submits Proposal).
- Requires coordination with DevOps/Platform team if email deliverability records (SPF, DKIM) need to be configured for the first time.

## 11.4.0.0 Release Impact

Enhances the core vendor workflow. Releasing without this feature would create a poor and incomplete user experience for vendors.

