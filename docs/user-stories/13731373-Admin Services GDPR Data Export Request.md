# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-075 |
| Elaboration Date | 2025-01-24 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Services GDPR Data Export Request |
| As A User Story | As a System Administrator, I want to find a specif... |
| User Persona | System Administrator. This user is responsible for... |
| Business Value | Ensures compliance with GDPR Article 15 (Right of ... |
| Functional Area | System Administration & Compliance |
| Story Theme | Compliance and Data Governance |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Admin successfully initiates a data export for an existing user

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a System Administrator logged into the platform and viewing the 'Data Subject Requests' management page

### 3.1.5 When

I search for an active user by their email address and select them from the results

### 3.1.6 And

An entry is created in the audit log detailing that a DSAR export was initiated for the user by me, including my user ID and IP address.

### 3.1.7 Then

The system confirms the export job has been queued and displays its status as 'Processing'

### 3.1.8 Validation Notes

Verify the UI updates to show 'Processing'. Check the audit log table for the corresponding new entry. Check the SQS queue for the new job.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Admin receives notification and downloads the completed data export

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

A data export for a user has been initiated and is in the 'Processing' state

### 3.2.5 When

The asynchronous export process successfully completes

### 3.2.6 Then

The request status in the UI updates to 'Complete'

### 3.2.7 And

I receive an in-app and email notification that the export is ready for download.

### 3.2.8 Validation Notes

Verify the UI status change. Click the link to confirm it initiates a download. Check email delivery via AWS SES logs.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Verify the content and format of the exported data package

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I have downloaded the data export package for a user

### 3.3.5 When

I extract and inspect the contents of the package

### 3.3.6 Then

The package is a single .zip archive containing one or more JSON files

### 3.3.7 And

The export must NOT contain sensitive credentials such as hashed passwords, session tokens, or API keys.

### 3.3.8 Validation Notes

Manually inspect the downloaded JSON file structure and content against a test user's known data in the database.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Admin searches for a user that does not exist

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am a System Administrator on the 'Data Subject Requests' page

### 3.4.5 When

I search for a user with an email address that is not in the system

### 3.4.6 Then

The UI displays a clear message stating 'User not found. Please check the email address and try again.'

### 3.4.7 Validation Notes

Test with a non-existent email address and verify the specific error message is displayed.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

The asynchronous data export process fails

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

A data export for a user is in the 'Processing' state

### 3.5.5 When

The export job fails due to an internal system error (e.g., a database connection issue)

### 3.5.6 Then

The request status in the UI updates to 'Failed'

### 3.5.7 And

The system logs the detailed error with a correlation ID for support and troubleshooting.

### 3.5.8 Validation Notes

Simulate a failure in a downstream service (e.g., by taking it offline in a test environment). Verify the UI status change and the error log in CloudWatch.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Admin attempts to use an expired download link

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

A data export was completed and a download link was generated more than 24 hours ago

### 3.6.5 When

I click the expired download link

### 3.6.6 Then

I receive an 'Access Denied' or 'Link Expired' error page and the file download does not start.

### 3.6.7 Validation Notes

Generate a link, manually adjust the system clock or wait 24 hours, then attempt to access the link. A pre-signed S3 URL with a short expiry can be used to test this efficiently.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Admin initiates an export for a deactivated user

### 3.7.3 Scenario Type

Edge_Case

### 3.7.4 Given

I am a System Administrator on the 'Data Subject Requests' page

### 3.7.5 When

I search for a user whose account has been deactivated

### 3.7.6 Then

The user is found and I can successfully initiate, process, and download their data export, which includes all their historical data.

### 3.7.7 Validation Notes

Deactivate a test user account, then perform the full export workflow and verify the data is complete.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated 'Data Subject Requests' page under the System Administration section.
- A search input field for finding users by email address.
- A results area to display the found user.
- An 'Initiate Data Export' button for the selected user.
- A table or list to display the history of export requests with columns for: User Email, Requested By, Timestamp, Status (Processing, Complete, Failed), and an Action button (e.g., Download).

## 4.2.0 User Interactions

- Admin enters an email and clicks 'Search'.
- System displays matching user or 'not found' message.
- Admin clicks 'Initiate Data Export', which disables the button and shows a 'Processing' status indicator.
- When complete, a 'Download' link appears in the actions column.

## 4.3.0 Display Requirements

- The status of each export request must be clearly and accurately displayed in real-time (or near real-time).
- The expiry time of the download link should be displayed to the Admin.

## 4.4.0 Accessibility Needs

- All UI elements (search field, buttons, table) must be keyboard-navigable and compatible with screen readers, adhering to WCAG 2.1 AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A data export package must be retained and available for download for a maximum of 7 days after generation, after which it must be automatically and permanently deleted from storage.

### 5.1.3 Enforcement Point

System-level automated cleanup job (e.g., S3 lifecycle policy).

### 5.1.4 Violation Handling

N/A - System enforced.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The download link for an export package must expire 24 hours after it is generated.

### 5.2.3 Enforcement Point

During the generation of the secure link (e.g., creating a pre-signed S3 URL with a 24-hour expiry).

### 5.2.4 Violation Handling

The link becomes invalid and returns an access denied error.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

All DSAR export initiations must be recorded in the immutable audit trail.

### 5.3.3 Enforcement Point

Immediately upon the System Administrator clicking the 'Initiate Data Export' button.

### 5.3.4 Violation Handling

The export process should fail if the audit log entry cannot be created.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-001

#### 6.1.1.2 Dependency Reason

User accounts must exist to be exported.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-086

#### 6.1.2.2 Dependency Reason

The Audit Trail system must be implemented as its data is a required component of the export.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-029

#### 6.1.3.2 Dependency Reason

Project data structures must exist to be included in the export for associated users.

## 6.2.0.0 Technical Dependencies

- Microservice architecture allowing for cross-service data aggregation.
- AWS S3 for secure file storage.
- AWS SQS/SNS for managing the asynchronous export job queue.
- AWS SES for sending email notifications.
- A defined data schema for all personal data across all services.

## 6.3.0.0 Data Dependencies

- Access to User, Client, Vendor, Project, Proposal, and Audit Log databases/services is required.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The UI for initiating the request must respond in under 500ms.
- The asynchronous export process for a user with a large data footprint (e.g., 500 projects) should complete within 15 minutes.

## 7.2.0.0 Security

- Access to this feature must be restricted to the System Administrator role via RBAC.
- Generated export files must be stored encrypted at rest in S3 using AWS KMS.
- Download links must be secure, non-guessable, and time-limited (pre-signed S3 URLs).
- The export process must explicitly exclude sensitive data like passwords and secrets.

## 7.3.0.0 Usability

- The process of finding a user and initiating an export should be intuitive and require minimal steps.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

High

## 8.2.0.0 Complexity Factors

- Cross-service data aggregation is the primary challenge. The export service will need to query multiple independent services (User, Project, etc.) and combine the data.
- Implementing a robust, fault-tolerant asynchronous job processing system with status tracking and notifications.
- Ensuring the data mapping is 100% comprehensive to meet GDPR requirements is a detailed analysis task.
- Secure handling of the generated data package (encryption, access control, automated deletion).

## 8.3.0.0 Technical Risks

- Performance bottlenecks during data aggregation from multiple services could cause job timeouts.
- Incomplete data mapping could lead to non-compliance if some personal data is missed in the export.
- Failure in one of the downstream services could cause the entire export job to fail, requiring robust error handling and retry logic.

## 8.4.0.0 Integration Points

- User Service (to fetch user profile)
- Project Service (to fetch project/proposal data)
- Audit Service (to fetch audit logs)
- Notification Service (to send email/in-app alerts)
- AWS S3 (to store the final export package)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Performance

## 9.2.0.0 Test Scenarios

- Full end-to-end flow for a user with minimal data.
- Full end-to-end flow for a user with extensive data across multiple projects.
- Attempting to download with an expired link.
- Simulating a failure in the Project Service during data aggregation to test the 'Failed' state.
- Verifying the content of the downloaded JSON for accuracy and completeness.
- Role-based access test: ensure a non-Admin user cannot access the feature.

## 9.3.0.0 Test Data Needs

- Test user accounts with varying levels of activity: a new user, a highly active client contact, a vendor with many proposals, and a deactivated user.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.
- Security scanning tools to check for vulnerabilities in the download mechanism.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other engineer
- Unit and integration test coverage meets the 80% project standard
- E2E tests for critical paths are implemented and passing
- Security review of the data handling and download mechanism is complete and signed off
- Performance testing confirms the export completes within the defined time limit for large data sets
- All UI elements meet WCAG 2.1 AA accessibility standards
- User and Administrator documentation for this feature is written and published
- Story deployed and verified in the staging environment by QA and the Product Owner

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

13

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- Due to high complexity, a technical design spike may be required in a preceding sprint to finalize the data aggregation strategy.
- This story is a blocker for achieving full GDPR compliance and should be prioritized accordingly.

## 11.4.0.0 Release Impact

- This is a key feature for the initial production release to ensure legal compliance from day one.

