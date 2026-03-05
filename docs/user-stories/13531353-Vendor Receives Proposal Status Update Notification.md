# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-055 |
| Elaboration Date | 2025-01-18 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Vendor Receives Proposal Status Update Notificatio... |
| As A User Story | As a Vendor Contact, I want to be automatically no... |
| User Persona | Vendor Contact - An external user representing a v... |
| Business Value | Improves vendor experience and relationship manage... |
| Functional Area | Proposal Management & Notifications |
| Story Theme | Project Lifecycle Automation |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Notification for Proposal moved to 'Shortlisted'

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A Vendor Contact has a proposal with the status 'In Review' for 'Project Alpha'

### 3.1.5 When

A System Admin changes the status of that proposal to 'Shortlisted'

### 3.1.6 Then

The system automatically triggers and sends an email to the Vendor Contact's registered address within 2 minutes.

### 3.1.7 Validation Notes

Verify an email is received by a test email account. Check the email's subject and body content against the approved template for the 'Shortlisted' status. Confirm a 'notification_sent' event is logged in the audit trail.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Notification for Proposal 'Accepted'

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

A Vendor Contact has a proposal with the status 'Shortlisted' for 'Project Bravo'

### 3.2.5 When

A System Admin changes the status of that proposal to 'Accepted'

### 3.2.6 Then

The system sends an email with a congratulatory tone to the Vendor Contact.

### 3.2.7 Validation Notes

Verify the email content is specific to the 'Accepted' status, including information about next steps as per the approved template.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Notification for Proposal 'Rejected'

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

A Vendor Contact has a proposal with the status 'In Review' for 'Project Charlie'

### 3.3.5 When

A System Admin changes the status of that proposal to 'Rejected'

### 3.3.6 Then

The system sends a professional and courteous email to the Vendor Contact.

### 3.3.7 Validation Notes

Verify the email content is specific to the 'Rejected' status, thanking the vendor for their submission and encouraging future participation, as per the approved template.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Notification is suppressed based on user preferences

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

A Vendor Contact has opted-out of 'Proposal Status Update' email notifications in their user profile settings

### 3.4.5 When

A System Admin changes the status of their proposal to 'Shortlisted'

### 3.4.6 Then

The system does NOT send an email notification to the Vendor Contact.

### 3.4.7 Validation Notes

Check the notification service logs to confirm that the notification was intentionally suppressed due to user preference. No email should be received by the test account.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Email service provider is temporarily unavailable

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

The external email service (AWS SES) is unresponsive

### 3.5.5 When

A System Admin changes a proposal's status, triggering a notification

### 3.5.6 Then

The notification is placed in a queue for retry.

### 3.5.7 Validation Notes

Using a mock of the email service, simulate a failure. Verify the notification message is added to the SQS queue. After restoring the mock service, verify the message is processed and the email is sent successfully on a subsequent attempt.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Email content is correct and dynamic

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

A proposal for 'Project Delta' submitted by 'Vendor Omega' is moved to a new status

### 3.6.5 When

The notification email is generated

### 3.6.6 Then

The email body correctly includes the Project Name ('Project Delta'), the Vendor's name, and the new proposal status.

### 3.6.7 Validation Notes

Inspect the received email content to ensure all dynamic fields are populated correctly and there are no placeholder texts.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- N/A - This story's primary interface is an email, not an in-app UI.

## 4.2.0 User Interactions

- The user (Vendor Contact) reads the email notification in their email client.

## 4.3.0 Display Requirements

- Email templates must be created for each relevant proposal status change ('Shortlisted', 'Accepted', 'Rejected').
- Emails must be responsive and render correctly on both desktop and mobile email clients.
- Emails must include the company's branding (logo, colors).

## 4.4.0 Accessibility Needs

- Email HTML must use semantic markup (e.g., proper headings) and include 'alt' text for images to be accessible to screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Notifications are only sent for status changes initiated by an internal user (System Admin).

### 5.1.3 Enforcement Point

Notification Service event consumer.

### 5.1.4 Violation Handling

Events triggered by non-admin actions or system processes should not result in a vendor-facing notification.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Notifications must be sent to the primary contact email address associated with the Vendor's profile.

### 5.2.3 Enforcement Point

Notification Service, before sending the email.

### 5.2.4 Violation Handling

If no primary contact email exists, log a warning and alert a System Admin.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-054

#### 6.1.1.2 Dependency Reason

This story is triggered by the functionality in US-054, which allows an Admin to change a proposal's status.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-089

#### 6.1.2.2 Dependency Reason

The system must be able to check a user's notification preferences before sending an email, as defined in US-089.

## 6.2.0.0 Technical Dependencies

- A configured Notification Service capable of processing events and sending emails.
- Integration with AWS SES (Simple Email Service) or a similar transactional email provider.
- An event bus (e.g., AWS SNS/SQS) for decoupled communication between the Project Service and the Notification Service.
- A centralized email templating engine.

## 6.3.0.0 Data Dependencies

- Access to Proposal entity data (ID, status, associated Project Name).
- Access to Vendor Contact entity data (name, email address).
- Access to User Preferences data store.

## 6.4.0.0 External Dependencies

- The availability and API of the external email service provider (e.g., AWS SES).

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- Notifications should be sent within 2 minutes of the status change event.

## 7.2.0.0 Security

- Emails must not contain sensitive PII beyond the recipient's name and project title.
- Any links back to the platform in the email must not contain authentication tokens.

## 7.3.0.0 Usability

- Email content must be clear, concise, and unambiguous.

## 7.4.0.0 Accessibility

- Email templates must adhere to WCAG 2.1 Level AA for content structure and contrast.

## 7.5.0.0 Compatibility

- Emails must render correctly in the latest versions of major email clients (Gmail, Outlook, Apple Mail).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires asynchronous, event-driven communication between microservices.
- Implementation of a robust retry mechanism with a dead-letter queue (DLQ) for handling persistent notification failures.
- Creation and management of multiple, internationalized (i18n) email templates.
- Integration with the user preferences system.

## 8.3.0.0 Technical Risks

- Potential for email deliverability issues (e.g., being marked as spam). Requires proper SPF/DKIM configuration.
- Complexity in testing the end-to-end asynchronous flow.

## 8.4.0.0 Integration Points

- Project Service: Publishes a `proposal.status.changed` event.
- Notification Service: Subscribes to the event and orchestrates the notification.
- AWS SES: External API for sending the final email.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify email is sent for each status change: Submitted -> In Review, In Review -> Shortlisted, Shortlisted -> Accepted, Shortlisted -> Rejected.
- Verify email is NOT sent when user has opted out.
- Verify retry logic works when the email service is mocked to fail.
- Verify email content, including dynamic data, is correct for each template.
- Verify email rendering on major clients using a tool like Litmus or Email on Acid.

## 9.3.0.0 Test Data Needs

- Test users (Vendor Contacts) with valid email addresses.
- A test user who has opted out of notifications.
- Multiple projects and proposals in various states.

## 9.4.0.0 Testing Tools

- Jest for unit tests.
- A local email testing tool like MailHog or a cloud-based one like Mailtrap for E2E tests.
- AWS SDK mocks for integration testing the SES calls.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other engineer
- Unit and integration tests implemented with >80% code coverage for new logic
- E2E tests for the primary notification flows are implemented and passing
- Email templates have been reviewed and approved by the Product Owner/UX designer
- Notification retry and failure logging mechanisms are verified
- Dependencies on other stories are met and integrated
- Documentation for the new notification events and templates is created/updated
- Story deployed and verified in the staging environment by QA

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- Requires collaboration with UX/Product to finalize the copy for each email template.
- The underlying event bus and Notification Service infrastructure must be in place or planned for accordingly.
- Access to a testable email service (e.g., SES sandbox) is required for development and testing.

## 11.4.0.0 Release Impact

This is a high-visibility feature for vendors and significantly improves the user experience of the proposal workflow. It should be included in the next major release to vendors.

