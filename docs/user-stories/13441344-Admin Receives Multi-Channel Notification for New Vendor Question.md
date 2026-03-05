# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-046 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Receives Multi-Channel Notification for New ... |
| As A User Story | As a System Administrator, I want to receive immed... |
| User Persona | System Administrator. This user is responsible for... |
| Business Value | Reduces response latency in the critical proposal ... |
| Functional Area | Project Lifecycle Management |
| Story Theme | Proposal and Award Workflow |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful In-App Notification Generation

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a System Admin is logged into the platform and a project named 'Project Phoenix' exists

### 3.1.5 When

a vendor submits a new question for 'Project Phoenix'

### 3.1.6 Then

the System Admin's in-app notification indicator should display a new unread notification, and the notification list should contain an entry like 'New question received for Project Phoenix' with a direct link to that project's Q&A management page.

### 3.1.7 Validation Notes

Verify via E2E test. Log in as Admin, have a test script submit a question as a vendor, then check the Admin's UI for the notification element and its content.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Successful Email Notification Delivery

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

a System Admin has email notifications enabled for 'New Vendor Questions' and a project named 'Project Phoenix' exists

### 3.2.5 When

a vendor submits a new question for 'Project Phoenix'

### 3.2.6 Then

the System Admin must receive an email via AWS SES with the subject 'New Vendor Question on Project: Project Phoenix'

### 3.2.7 And

the email body must contain the project name and a secure, deep link to the project's Q&A management page.

### 3.2.8 Validation Notes

Integration test using a mock email service (e.g., MailHog) to intercept and validate the email's recipient, subject, and body content, including the link's validity.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Successful Webhook Notification Delivery

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

a valid webhook URL is configured in the system settings (as per REQ-FUN-005) and a project named 'Project Phoenix' exists

### 3.3.5 When

a vendor submits a new question for 'Project Phoenix'

### 3.3.6 Then

the system must send an HTTP POST request to the configured webhook URL

### 3.3.7 And

the JSON payload must contain 'project_id', 'project_name', 'question_id', and a 'deep_link' to the project's Q&A management page.

### 3.3.8 Validation Notes

Integration test using a mock webhook receiver (e.g., webhook.site or a custom endpoint) to verify the request method, headers, and payload structure.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Notification Preferences are Respected

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

a System Admin has disabled email notifications for 'New Vendor Questions' in their user preferences

### 3.4.5 When

a vendor submits a new question

### 3.4.6 Then

the system must NOT send an email notification to that System Admin

### 3.4.7 And

the in-app notification must still be generated as expected.

### 3.4.8 Validation Notes

E2E test. Configure user preferences, trigger the event, and assert that no email is sent while the in-app notification appears.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Email Service Failure

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

the external email service (AWS SES) is unavailable or returns an error

### 3.5.5 When

the system attempts to send a new question notification email

### 3.5.6 Then

the system must log the failure with a correlation ID

### 3.5.7 And

the system must place the notification job in a retry queue (e.g., SQS) to be re-attempted with an exponential backoff strategy.

### 3.5.8 Validation Notes

Integration test by mocking the AWS SES SDK to throw an exception. Verify that an error is logged and a message is sent to the correct SQS retry queue.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Webhook Endpoint Failure

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

the configured webhook endpoint is down or returns a non-2xx status code

### 3.6.5 When

the system attempts to send a webhook notification

### 3.6.6 Then

the system must log the failure, including the endpoint URL and the status code received

### 3.6.7 And

the system must follow a defined retry policy for the failed webhook.

### 3.6.8 Validation Notes

Integration test using a mock server that returns a 500 error. Verify logs and the retry mechanism.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- In-app notification bell/icon with an unread count badge.
- Notification dropdown/panel to list recent notifications.

## 4.2.0 User Interactions

- Clicking the notification bell reveals the list of notifications.
- Clicking a specific notification marks it as read and navigates the user to the relevant page (e.g., the project's Q&A section).

## 4.3.0 Display Requirements

- Email notifications must be professionally formatted in HTML with a plain-text fallback.
- Webhook payload must be well-documented, versioned, and consistently structured JSON.

## 4.4.0 Accessibility Needs

- In-app notification elements must be keyboard accessible and compatible with screen readers (ARIA attributes).
- Email notifications must meet WCAG 2.1 AA standards.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "Notifications for new vendor questions must be sent to all users with the 'System Administrator' role.", 'enforcement_point': "Notification Service, upon receiving a 'VendorQuestionSubmitted' event.", 'violation_handling': 'If user lookup fails, log an error. The process should continue for other valid admins.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-045

#### 6.1.1.2 Dependency Reason

This story is the trigger. The functionality for a vendor to submit a question must exist before notifications can be sent.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-089

#### 6.1.2.2 Dependency Reason

The notification system must be able to read a user's notification preferences to determine which channels to use.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-073

#### 6.1.3.2 Dependency Reason

The system must provide a way for an Admin to configure the webhook URL before webhook notifications can be sent.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-047

#### 6.1.4.2 Dependency Reason

The deep link included in the notification requires a destination page where the Admin can view and answer the question.

## 6.2.0.0 Technical Dependencies

- A dedicated Notification Service microservice.
- An event bus (AWS SNS/SQS) for asynchronous communication between the Project Service and Notification Service.
- Integration with AWS Simple Email Service (SES) for email delivery.

## 6.3.0.0 Data Dependencies

- Access to User data to retrieve email addresses and notification preferences for all System Admins.
- Access to Project data to retrieve the project name for notification content.

## 6.4.0.0 External Dependencies

- Reliability of AWS SES for email delivery.
- Availability and reliability of the customer-configured webhook endpoint.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- Notifications should be dispatched (sent to email/webhook service) within 5 seconds of the question submission event being published.
- The notification process must be asynchronous and not impact the response time of the vendor's question submission action.

## 7.2.0.0 Security

- Deep links in notifications must lead to a page that enforces authentication and authorization.
- No Personally Identifiable Information (PII) or sensitive project data should be included in the notification content itself, only identifiers and links.

## 7.3.0.0 Usability

- Notification content must be clear, concise, and provide immediate context (i.e., which project is involved).

## 7.4.0.0 Accessibility

- All related UI components and email templates must comply with WCAG 2.1 Level AA.

## 7.5.0.0 Compatibility

- Email notifications must render correctly in the latest two versions of major email clients (Gmail, Outlook, Apple Mail).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires asynchronous, event-driven architecture (SNS/SQS).
- Integration with three separate delivery channels (in-app, email, webhook).
- Requires robust error handling and retry logic for external API calls (SES, webhooks).
- Need for a templating engine to manage notification content for different channels.

## 8.3.0.0 Technical Risks

- Potential for notification delays if the event queue processing is slow.
- Unreliable external webhook endpoints can lead to a high volume of retry attempts and log noise.
- Managing different notification templates can become complex without a good system.

## 8.4.0.0 Integration Points

- Project Service: Publishes the 'VendorQuestionSubmitted' event.
- Notification Service: Subscribes to the event and orchestrates sending.
- User Service: Provides user data (email, preferences).
- AWS SES: External service for sending emails.
- External Webhook Endpoint: Customer-defined endpoint for webhook delivery.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify notification generation for each channel (in-app, email, webhook).
- Verify that user notification preferences are correctly applied.
- Test failure and retry logic for both email and webhook services.
- Validate the content and structure of the email and webhook payload.
- Test the deep link functionality to ensure it directs to the correct, authenticated page.

## 9.3.0.0 Test Data Needs

- Test users with the System Admin role.
- Admin users with different notification preferences configured (all on, some off).
- A valid and an invalid webhook endpoint for testing.
- A test project and associated vendor user.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.
- A local email testing tool like MailHog.
- A mock webhook receiver like webhook.site or a custom-built mock server.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing in the staging environment.
- Code has been peer-reviewed and merged into the main branch.
- Unit and integration tests are implemented with >80% code coverage for new logic.
- E2E tests covering the happy path and key alternative flows are passing.
- The webhook payload schema is documented in the API documentation.
- Logging and monitoring for the notification service are in place.
- Documentation for system administrators on how to configure webhooks is created or updated.
- Story has been successfully deployed and verified in the staging environment.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- Requires all prerequisite stories (US-045, US-089, US-073, US-047) to be completed or worked on in parallel.
- Requires AWS SES to be configured and credentials available to the development team.
- The team should agree on the event schema for 'VendorQuestionSubmitted' before implementation begins.

## 11.4.0.0 Release Impact

This is a core feature for the proposal management workflow. Its absence would create a significant manual burden on administrators and slow down the entire process.

