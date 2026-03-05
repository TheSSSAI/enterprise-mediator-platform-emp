# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-003 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Invitation for Vendor Contact |
| As A User Story | As a System Administrator, I want to send a secure... |
| User Persona | System Administrator. This is a high-privilege int... |
| Business Value | Enables the secure and efficient onboarding of ven... |
| Functional Area | User and Entity Management |
| Story Theme | Vendor Onboarding and Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Admin successfully invites a new vendor contact

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in System Administrator viewing the details page of an existing Vendor

### 3.1.5 When

I enter a valid, unique email address for a new contact and click the 'Invite Contact' button

### 3.1.6 Then

The system must generate a unique, time-limited registration token, send an invitation email containing a registration link to the provided address, display a success message like 'Invitation sent to [email@address.com]', and log this action in the audit trail.

### 3.1.7 Validation Notes

Verify the success message in the UI. Verify the email is sent (using a test email service like MailHog or checking AWS SES logs). Verify a pending invitation record is created in the database with the correct vendor association, token, and expiry. Verify the audit log entry.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Admin attempts to invite a contact with an invalid email format

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

I am a logged-in System Administrator on the vendor invitation form

### 3.2.5 When

I enter an email address in an invalid format (e.g., 'test@vendor') and attempt to send the invitation

### 3.2.6 Then

The system must display an inline validation error message such as 'Please enter a valid email address' and must not attempt to send an email.

### 3.2.7 Validation Notes

Test with various invalid email formats. Ensure no API call is made to the backend until client-side validation passes. Verify server-side validation also rejects the request.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Admin attempts to invite an email address that already exists in the system

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am a logged-in System Administrator, and an email address 'existing.user@vendor.com' is already registered for any user in the system

### 3.3.5 When

I attempt to invite a new vendor contact with the email 'existing.user@vendor.com'

### 3.3.6 Then

The system must prevent the invitation and display a clear error message like 'This email address is already in use.'

### 3.3.7 Validation Notes

Ensure the check is performed against all users, regardless of their role or status. Verify no invitation email is sent.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Admin attempts to re-invite an email address with a pending invitation

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I am a logged-in System Administrator, and an invitation has already been sent to 'pending.user@vendor.com' which has not yet expired

### 3.4.5 When

I attempt to invite 'pending.user@vendor.com' again

### 3.4.6 Then

The system should display an informative message, such as 'An invitation is already pending for this email address. You can resend the invitation.'

### 3.4.7 Validation Notes

This prevents duplicate pending invitations. The UI should ideally offer a 'Resend Invitation' action as a follow-up story, but for now, blocking a new invite is sufficient.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

The external email sending service fails

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am a logged-in System Administrator and I submit a valid invitation request

### 3.5.5 When

The backend service fails to communicate with the email provider (AWS SES)

### 3.5.6 Then

The system must not create a pending invitation record, must log the service failure internally, and must display a user-friendly error message like 'The invitation could not be sent at this time. Please try again later.'

### 3.5.7 Validation Notes

This can be tested by mocking the email service to return an error. The user should not see a technical error message. The database state should remain unchanged.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- An 'Invite New Contact' button or form within the 'Contacts' section of the Vendor Details page.
- A text input field for the contact's email address.
- A 'Send Invitation' submit button.
- A non-modal notification/toast element to display success or error messages.

## 4.2.0 User Interactions

- The 'Send Invitation' button should be disabled until a value is entered in the email field.
- Client-side validation should provide immediate feedback for invalid email formats.
- After a successful invitation, the UI should update to show the pending status of the invited contact in the vendor's contact list.

## 4.3.0 Display Requirements

- The vendor's contact list should display existing contacts and their status (e.g., 'Active', 'Invitation Sent').

## 4.4.0 Accessibility Needs

- The form must be navigable via keyboard.
- All form fields must have associated labels.
- Error and success messages must be announced by screen readers (e.g., using `aria-live` regions).

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

An invitation link must be unique and expire after a configurable period (e.g., 72 hours).

### 5.1.3 Enforcement Point

System backend during token generation and validation.

### 5.1.4 Violation Handling

An expired or invalid link will lead the user to a page indicating the link is no longer valid, with an option to request a new one.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

An email address can only have one active or pending user account in the system at any time.

### 5.2.3 Enforcement Point

Backend API before creating a pending invitation record.

### 5.2.4 Violation Handling

The API will return an error, and the UI will display a message indicating the email is already in use.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-020

#### 6.1.1.2 Dependency Reason

A Vendor profile must exist before a contact can be invited to it.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-024

#### 6.1.2.2 Dependency Reason

The UI for inviting a contact will be located on the Vendor Details view.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-004

#### 6.1.3.2 Dependency Reason

The invitation is not functional until the invited user can complete the registration process using the provided link.

## 6.2.0.0 Technical Dependencies

- User Service: Must expose an endpoint to check for the existence of an email.
- Notification Service: Must be configured with AWS SES credentials and have an email template for vendor invitations.
- Database Schema: Requires a table to store pending invitations, including a foreign key to the vendors table, the token, and an expiry timestamp.

## 6.3.0.0 Data Dependencies

- Requires at least one existing Vendor record in the database for testing.

## 6.4.0.0 External Dependencies

- AWS Simple Email Service (SES) for sending transactional emails.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response time for the invitation request must be < 250ms (p95). Email delivery is asynchronous and not part of this measurement.

## 7.2.0.0 Security

- The registration token must be a cryptographically secure, randomly generated string.
- The invitation action must be logged in the immutable audit trail, including the Admin's ID, timestamp, and target vendor/email.
- All communication must be over HTTPS.

## 7.3.0.0 Usability

- The process for an Admin to invite a contact should be intuitive and require minimal steps.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Integration with the external AWS SES service.
- Secure generation, storage, and management of time-sensitive registration tokens.
- Database schema design for pending invitations.
- Asynchronous nature of email sending requires robust error handling and logging.

## 8.3.0.0 Technical Risks

- Potential for email delivery issues (e.g., emails being marked as spam). SPF/DKIM records must be correctly configured.
- Race conditions if two admins try to invite the same contact simultaneously (should be handled by database constraints).

## 8.4.0.0 Integration Points

- User Service (for email validation)
- Notification Service (for sending email)
- Audit Service (for logging)
- PostgreSQL Database (for storing pending invitation)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Successfully send an invitation.
- Attempt to send with an invalid email.
- Attempt to send to an existing user's email.
- Verify the content and link in the received email (in a test environment).
- Verify that the registration link expires correctly after the configured duration.

## 9.3.0.0 Test Data Needs

- A test System Administrator account.
- At least one test Vendor company profile.
- A set of test email addresses, including one that is already registered in the system.

## 9.4.0.0 Testing Tools

- Jest (Unit/Integration)
- Playwright (E2E)
- A local email capture tool like MailHog for development/testing environments.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration tests achieve >= 80% code coverage for the new logic
- E2E tests for the happy path and key error conditions are implemented and passing
- UI/UX has been reviewed and approved by the product owner/designer
- Security review confirms secure token handling and audit logging
- The invitation email template is finalized and approved
- All related documentation (e.g., OpenAPI spec) is updated
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a prerequisite for the full vendor onboarding workflow. It should be prioritized early in the development cycle.
- Requires coordination on the email template content with the business/product team.

## 11.4.0.0 Release Impact

This is a foundational feature for enabling vendor interaction with the platform. The platform cannot launch without this capability.

