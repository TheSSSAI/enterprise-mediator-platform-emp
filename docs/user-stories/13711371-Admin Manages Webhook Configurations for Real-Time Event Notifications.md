# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-073 |
| Elaboration Date | 2025-01-24 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Manages Webhook Configurations for Real-Time... |
| As A User Story | As a System Administrator, I want to create, manag... |
| User Persona | System Administrator. This user is responsible for... |
| Business Value | Improves operational efficiency by integrating the... |
| Functional Area | System Administration & Configuration |
| Story Theme | Notifications and System Integration |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Admin successfully creates a new, active webhook

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

The System Admin is logged in and is on the 'Settings > Webhooks' configuration page

### 3.1.5 When

The Admin clicks 'Add Webhook', provides a unique name (e.g., 'Slack Project Updates'), a valid webhook URL, selects at least one event (e.g., 'Proposal Submitted'), and clicks 'Save'

### 3.1.6 Then

A success notification is displayed, and the new webhook appears in the list with its name, a masked URL, the number of subscribed events, and a status of 'Active'.

### 3.1.7 Validation Notes

Verify the record is created in the database with the URL encrypted. The list view should not display the full URL.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Admin successfully tests a configured webhook

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

A valid webhook configuration exists and is displayed in the list

### 3.2.5 When

The Admin clicks the 'Test' button for that webhook

### 3.2.6 Then

The system sends a predefined, generic test payload to the configured URL, and a success message like 'Test notification sent successfully' is displayed in the UI.

### 3.2.7 Validation Notes

This can be tested by pointing the webhook to a service like webhook.site and verifying the test payload is received. The backend service must handle this asynchronously.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Admin successfully edits an existing webhook

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

An existing webhook configuration is displayed in the list

### 3.3.5 When

The Admin clicks 'Edit', changes the name and adds another event subscription, then clicks 'Save'

### 3.3.6 Then

A success notification is displayed, and the webhook's details are updated in the list view.

### 3.3.7 Validation Notes

Verify the database record reflects the changes.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Admin deactivates and reactivates a webhook

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

An active webhook exists in the list

### 3.4.5 When

The Admin uses the toggle switch to change its status to 'Inactive', and then back to 'Active'

### 3.4.6 Then

The status is updated in the UI and the database accordingly. No notifications are sent for subscribed events while the webhook is inactive.

### 3.4.7 Validation Notes

Trigger a subscribed event while the webhook is inactive and confirm no request is sent. Reactivate it, trigger the event again, and confirm the request is sent.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Admin deletes a webhook

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

An existing webhook is displayed in the list

### 3.5.5 When

The Admin clicks the 'Delete' button and confirms the action in a confirmation modal

### 3.5.6 Then

A success notification is displayed, and the webhook is permanently removed from the list and the database.

### 3.5.7 Validation Notes

Verify the record is hard-deleted from the database.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Admin attempts to save a webhook with an invalid URL

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

The Admin is in the 'Add Webhook' form

### 3.6.5 When

The Admin enters a string that is not a valid URL (e.g., 'my-slack-hook') in the URL field and clicks 'Save'

### 3.6.6 Then

The form is not submitted, and a validation error message 'Please enter a valid URL' is displayed next to the URL field.

### 3.6.7 Validation Notes

Validation should check for a proper URL format (e.g., starts with http:// or https://).

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Admin attempts to save a webhook with no events selected

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

The Admin is in the 'Add Webhook' form and has filled in a name and URL

### 3.7.5 When

The Admin does not select any events from the event list and clicks 'Save'

### 3.7.6 Then

The form is not submitted, and a validation error message 'At least one event must be selected' is displayed.

### 3.7.7 Validation Notes

The 'Save' button may be disabled until at least one event is selected.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Testing a webhook fails due to an invalid endpoint

### 3.8.3 Scenario Type

Error_Condition

### 3.8.4 Given

A webhook is configured with a URL that returns a 4xx or 5xx error or is unreachable

### 3.8.5 When

The Admin clicks the 'Test' button for that webhook

### 3.8.6 Then

An error message is displayed in the UI, such as 'Test failed: Endpoint returned status 404 Not Found'.

### 3.8.7 Validation Notes

The backend service should capture the HTTP response status from the endpoint and relay a user-friendly error message to the frontend.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A table listing configured webhooks with columns for Name, Status (Active/Inactive toggle), and Actions (Edit, Test, Delete).
- An 'Add Webhook' button.
- A modal or form for adding/editing a webhook with fields for Name (text input), URL (text input), and a multi-select checklist or dropdown for 'Subscribed Events'.
- A confirmation modal for the delete action.

## 4.2.0 User Interactions

- The webhook URL in the list view should be masked by default to protect the secret.
- A 'Copy to Clipboard' button should be available for the masked URL.
- The list of available events for subscription should be grouped by functional area (e.g., Project, Finance, User Management) for clarity.

## 4.3.0 Display Requirements

- The system must display clear success or error feedback messages for all CRUD and test operations.
- The list of webhooks should be searchable by name.

## 4.4.0 Accessibility Needs

- All form fields must have associated labels.
- All interactive elements (buttons, toggles) must be keyboard-navigable and have clear focus states, adhering to WCAG 2.1 AA.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A webhook name must be unique within the system.

### 5.1.3 Enforcement Point

Server-side validation upon creating or updating a webhook.

### 5.1.4 Violation Handling

A validation error message 'This name is already in use. Please choose a unique name.' is returned to the user.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Webhook URLs are considered sensitive credentials and must be stored securely.

### 5.2.3 Enforcement Point

Data persistence layer.

### 5.2.4 Violation Handling

URLs must be encrypted at rest in the database. They should never be logged in plain text.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-074

#### 6.1.1.2 Dependency Reason

The concept of a 'System Administrator' role with access to settings must be established.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-SYS-001

#### 6.1.2.2 Dependency Reason

A foundational, event-driven Notification Service (as per REQ-TEC-002) must exist to listen for business events and process the webhook queue. This story builds the configuration UI and logic on top of that service.

## 6.2.0.0 Technical Dependencies

- AWS SNS/SQS for the event bus and webhook processing queue.
- A defined and versioned list of subscribable business events (e.g., 'proposal.submitted', 'payment.completed').
- Backend service must have a secure method for storing secrets (e.g., AWS Secrets Manager or KMS-encrypted database fields).

## 6.3.0.0 Data Dependencies

- A centrally managed enumeration or table of all possible events that can trigger a notification.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- Webhook notification delivery must be asynchronous and not block the user-facing API call that triggered the event.
- The p99 latency for queuing a webhook notification event should be under 50ms.

## 7.2.0.0 Security

- Webhook URLs must be encrypted at rest.
- Webhook URLs must not be exposed in any API responses to the client after initial creation.
- The service sending webhook requests must use TLS 1.2+.
- The system should implement a retry mechanism with exponential backoff for transient failures (e.g., 5xx errors) but should not retry on client errors (4xx errors).

## 7.3.0.0 Usability

- The list of available events should be described in plain, understandable language.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The UI must be fully functional on the latest two versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Backend implementation requires a robust, resilient, and asynchronous architecture using queues (SQS) and a Dead-Letter Queue (DLQ) for permanent failures.
- Requires careful security handling of webhook URLs (encryption at rest, masking in UI).
- Coordination is needed across teams to define and publish the events that this feature will consume.

## 8.3.0.0 Technical Risks

- If the event schema is not well-defined and versioned, future changes could break webhook integrations.
- Poorly handled retry logic could lead to 'thundering herd' problems or duplicate notifications.

## 8.4.0.0 Integration Points

- The Notification Service, which consumes events from the central event bus (SNS/SQS).
- Any service within the microservices architecture that publishes a business event.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- CRUD operations for a webhook configuration.
- Successful and failed 'Test' functionality.
- End-to-end flow: Trigger an action in the application (e.g., submit a proposal), and verify the corresponding webhook payload is received at a mock endpoint.
- Verify that an inactive webhook does not trigger a notification.
- Verify that a failed webhook delivery is retried and eventually sent to a DLQ.

## 9.3.0.0 Test Data Needs

- A System Administrator user account.
- A mock webhook endpoint (e.g., webhook.site) for manual and automated testing.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.
- A mock server or service to act as the webhook receiver during tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other engineer
- Unit and integration tests implemented with >= 80% code coverage for new logic
- E2E test for creating and successfully testing a webhook is implemented and passing
- User interface reviewed for usability and adherence to design standards
- Security requirements (encryption, masking) validated via code review and testing
- Documentation for the feature, including the list of available events and their payload schemas, is added to the administrator help guide
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🟡 Medium

## 11.3.0.0 Sprint Considerations

- This story is dependent on the core Notification Service and event bus infrastructure being in place.
- A definitive list of initial events to support must be agreed upon before starting development.

## 11.4.0.0 Release Impact

- This is a key feature for improving user engagement and integrating the platform into daily workflows. It is a significant value-add for internal operational teams.

