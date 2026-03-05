# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-002 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Invitation for Client Contact |
| As A User Story | As a System Administrator, I want to send an email... |
| User Persona | System Administrator. This user has high-level per... |
| Business Value | Enables the secure and controlled onboarding of cl... |
| Functional Area | User and Entity Management |
| Story Theme | User Onboarding and Access Control |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful invitation of a new Client Contact

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

The System Admin is logged in and is viewing the details page of an existing, active Client

### 3.1.5 When

The Admin clicks the 'Invite Contact' button, enters a valid and previously unused email address, and submits the form

### 3.1.6 Then

The system creates a user record with a 'Pending' status associated with the Client, a unique, time-limited registration token is generated and stored, an invitation email containing a registration link with the token is sent to the specified email address, and a success message 'Invitation successfully sent to [email]' is displayed to the Admin. The new pending user appears in the client's contact list with a 'Pending Invitation' status.

### 3.1.7 Validation Notes

Verify in the database that a new user record exists with the correct client_id, a 'pending' status, and a non-null token. Use an email testing tool (e.g., Mailtrap) to confirm the email is sent and the link is correct. Verify the UI update on the client details page.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Attempting to invite a user with an existing email address

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

The System Admin is on the client contact invitation form

### 3.2.5 When

The Admin enters an email address that already belongs to an existing user (in any status: pending, active, etc.) and submits

### 3.2.6 Then

The system prevents the invitation from being sent, no new user record is created, and a clear error message is displayed, such as 'A user with this email address already exists.'

### 3.2.7 Validation Notes

Seed the database with an existing user. Attempt to invite a new contact with the same email and assert that the API returns an error and the UI displays the specified message.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempting to invite a user with an invalid email format

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

The System Admin is on the client contact invitation form

### 3.3.5 When

The Admin enters a string that is not a valid email format (e.g., 'invalid-email' or 'user@domain') and attempts to submit

### 3.3.6 Then

The form's client-side and server-side validation fails, the invitation is not sent, and an inline error message is displayed, such as 'Please enter a valid email address.'

### 3.3.7 Validation Notes

Test with various invalid email formats. Verify that the API rejects the request with a 400-level status code and that the frontend displays the validation message without a full page reload.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Invitation action is recorded in the audit trail

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

The System Admin is logged in

### 3.4.5 When

A new client contact invitation is successfully sent (as in AC-001)

### 3.4.6 Then

A new entry is created in the immutable audit trail log. The entry must contain the timestamp, the Admin's user ID, the action ('CLIENT_CONTACT_INVITED'), the target entity (the new pending user's ID), the associated Client ID, and the Admin's IP address.

### 3.4.7 Validation Notes

After a successful invitation, query the audit log table/service to confirm the presence and correctness of the new log entry.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Handling of email service failure

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

The System Admin submits a valid invitation form

### 3.5.5 When

The backend successfully creates the pending user record but the external email service (AWS SES) fails to send the email

### 3.5.6 Then

The system logs the email sending failure, the user record remains in 'Pending' status, and the UI displays an informative error message like 'User invitation created, but the email could not be sent. Please try resending.' A 'Resend Invitation' option should be available for that pending user.

### 3.5.7 Validation Notes

Mock the email service API to simulate a failure. Verify that the user is still created in the database, the error is logged, and the UI provides the correct feedback and resend option.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- An 'Invite Contact' button on the Client details page.
- A modal or form with a single text input field for the email address.
- A 'Submit' or 'Send Invitation' button.
- A status indicator (e.g., a badge) next to each contact in the client's contact list, showing 'Active' or 'Pending Invitation'.

## 4.2.0 User Interactions

- Clicking 'Invite Contact' opens the invitation form.
- The system provides real-time inline validation for the email format as the user types.
- Upon submission, the form is disabled to prevent duplicate submissions, and a loading indicator is shown.
- Success or error messages are displayed in a non-intrusive way (e.g., a toast notification).

## 4.3.0 Display Requirements

- The client's contact list must be updated in real-time or upon refresh to show the newly invited user with their 'Pending Invitation' status.

## 4.4.0 Accessibility Needs

- The invitation form and all its elements (input field, button, error messages) must be fully accessible via keyboard.
- All form elements must have proper labels for screen readers (WCAG 2.1 AA).

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A user's email address must be unique across the entire system.

### 5.1.3 Enforcement Point

During the creation of a new user record (i.e., upon invitation submission).

### 5.1.4 Violation Handling

The creation request is rejected with a specific error indicating a duplicate entry.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Invitation links/tokens must expire after a configurable period (e.g., 24 hours).

### 5.2.3 Enforcement Point

During the user registration process (covered in US-004), when the user clicks the link.

### 5.2.4 Violation Handling

The user is shown a page indicating the link has expired and is given an option to request a new one.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-012

#### 6.1.1.2 Dependency Reason

A Client entity must exist in the system before a contact can be invited and associated with it.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-004

#### 6.1.2.2 Dependency Reason

This story generates the invitation; US-004 provides the registration flow that consumes the invitation link. The full workflow is incomplete without both.

## 6.2.0.0 Technical Dependencies

- User Service: Must have an endpoint to create a pending user.
- Notification Service: Must be configured with AWS SES to send transactional emails.
- Database Schema: The 'users' table must support a status field, a nullable token field, a token_expiry timestamp, and a foreign key to the 'clients' table.

## 6.3.0.0 Data Dependencies

- Requires at least one existing Client record in the database to test the association.

## 6.4.0.0 External Dependencies

- AWS Simple Email Service (SES) API for sending the invitation email.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response time for the invitation submission should be under 250ms (p95), excluding the latency of the external email service.

## 7.2.0.0 Security

- The generated registration token must be a cryptographically secure, unguessable random string.
- The token must be single-use; once used for registration, it must be invalidated.
- All communication must be over HTTPS (TLS 1.2+).
- The invitation action must be logged in the audit trail as per REQ-FUN-005.

## 7.3.0.0 Usability

- The process for an Admin to invite a contact should be discoverable and require minimal steps.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires inter-service communication (API -> User Service -> Notification Service).
- Secure token generation, storage, and lifecycle management.
- Robust error handling for the external email service dependency.
- Requires creation of a user-facing email template.

## 8.3.0.0 Technical Risks

- Email deliverability issues (e.g., emails being marked as spam). Requires proper SES configuration (SPF, DKIM).
- Race conditions if two admins attempt to invite the same email address simultaneously (should be handled by a unique constraint on the email column in the database).

## 8.4.0.0 Integration Points

- User Service API (to create the user).
- Notification Service API (to trigger the email).
- Audit Service (to log the event).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify successful invitation flow.
- Verify rejection of duplicate email.
- Verify rejection of invalid email format.
- Verify email content and link correctness using an email capture tool.
- Verify audit log creation.
- Verify UI feedback for all success and error states.

## 9.3.0.0 Test Data Needs

- A pre-existing System Admin user.
- A pre-existing Client company record.
- A set of new, unused email addresses for testing.
- An email address that is already registered in the system.

## 9.4.0.0 Testing Tools

- Jest (for unit/integration tests).
- Playwright (for E2E tests).
- Mailtrap or MailHog (for capturing and inspecting outgoing emails in test environments).

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and integration tests implemented with >80% coverage and passing
- E2E tests for the invitation workflow are implemented and passing
- User interface reviewed for usability and adherence to design specs
- Security requirements (token strength, audit logging) validated
- Documentation for the invitation API endpoint is created/updated in OpenAPI spec
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational story for the client-side of the platform. It should be prioritized early in the development cycle.
- The team will need access to a configured AWS SES instance or a suitable mock/trap for development and testing.

## 11.4.0.0 Release Impact

- This feature is critical for the initial pilot (Phase 3: Client Onboarding) and the full rollout.

