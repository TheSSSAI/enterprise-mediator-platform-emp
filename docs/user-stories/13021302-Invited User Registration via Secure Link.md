# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-004 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Invited User Registration via Secure Link |
| As A User Story | As an invited user (Internal, Client, or Vendor), ... |
| User Persona | Any new user (Internal, Client Contact, Vendor Con... |
| Business Value | Provides a secure, controlled, and automated onboa... |
| Functional Area | User Management & Authentication |
| Story Theme | User Onboarding |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful registration with a valid link

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An invited user has a valid, unique, and non-expired registration token

### 3.1.5 When

The user navigates to the registration URL containing the token

### 3.1.6 And

The user is redirected to the login page with a success message: 'Registration successful! You can now log in.'

### 3.1.7 Then

The system validates the token and user input successfully

### 3.1.8 Validation Notes

Verify the user can log in with the new credentials. Check the database to confirm the token's status is 'used' and the user's account is 'active'.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Attempting to register with an expired link

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

An invited user has a registration token that is past its expiration date

### 3.2.5 When

The user navigates to the registration URL containing the expired token

### 3.2.6 Then

The system identifies the token as expired

### 3.2.7 And

The registration form is not displayed.

### 3.2.8 Validation Notes

Manually set a token's expiry date in the database to the past and attempt to access the registration URL.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempting to reuse a registration link

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

An invited user has already completed registration using their unique token

### 3.3.5 When

The user navigates to the same registration URL again

### 3.3.6 Then

The system identifies the token as already used

### 3.3.7 And

The registration form is not displayed.

### 3.3.8 Validation Notes

After a successful registration, try to access the registration link a second time.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Attempting to register with an invalid or malformed link

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

A user has a registration URL with a token that does not exist in the system

### 3.4.5 When

The user navigates to the malformed URL

### 3.4.6 Then

The system cannot find a matching token

### 3.4.7 And

The user is shown a generic error page with the message: 'The registration link is invalid. Please ensure you have copied the full link from your invitation email.'

### 3.4.8 Validation Notes

Navigate to the registration endpoint with a random, non-existent token string.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Registration form submission with mismatched passwords

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

An invited user is on the registration page with a valid token

### 3.5.5 When

The user enters a password and a different value in the 'Confirm Password' field

### 3.5.6 And

An inline validation error message is displayed next to the password fields, such as 'Passwords do not match.'

### 3.5.7 Then

The form submission is prevented

### 3.5.8 Validation Notes

Automated UI test to check for the presence and content of the error message.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Registration form submission with a password that fails complexity requirements

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

An invited user is on the registration page with a valid token

### 3.6.5 When

The user enters a password that does not meet the system's configured complexity rules

### 3.6.6 And

An inline validation error message is displayed, clearly stating the requirements (e.g., 'Password must be at least 12 characters and contain an uppercase letter, a number, and a special character.').

### 3.6.7 Then

The form submission is prevented

### 3.6.8 Validation Notes

Test with multiple failing passwords: too short, no number, no uppercase, no special character.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Full Name text input field
- Email address (read-only display)
- Password input field (masked)
- Confirm Password input field (masked)
- Show/hide password toggle icon
- 'Create Account' submit button
- Helper text displaying password complexity requirements

## 4.2.0 User Interactions

- Client-side validation provides real-time feedback on input errors (e.g., password mismatch).
- The submit button is disabled until all required fields are filled.
- On submission failure, focus is returned to the first field with an error.

## 4.3.0 Display Requirements

- The user's email address, associated with the invitation, must be pre-filled and non-editable.
- Clear success and error messages must be displayed to the user upon form submission or link validation failure.

## 4.4.0 Accessibility Needs

- All form fields must have associated labels for screen readers.
- Validation error messages must be programmatically associated with their respective fields.
- The page must be fully navigable and operable via keyboard, adhering to WCAG 2.1 AA standards (REQ-INT-001).

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A registration token must be unique per invitation.

### 5.1.3 Enforcement Point

Backend, during token generation (part of US-001/002/003).

### 5.1.4 Violation Handling

Token generation fails; invitation is not sent.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A registration token must have a configurable, finite lifetime (e.g., 72 hours).

### 5.2.3 Enforcement Point

Backend, when a user attempts to access the registration page.

### 5.2.4 Violation Handling

Access is denied, and the 'expired link' error page is displayed.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

A registration token can only be used once.

### 5.3.3 Enforcement Point

Backend, during the registration form submission process.

### 5.3.4 Violation Handling

The registration process is halted, and an error is returned. If accessed via URL, the 'used link' error page is displayed.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-001

#### 6.1.1.2 Dependency Reason

The Admin invitation process must exist to generate and email the registration link/token for internal users.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-002

#### 6.1.2.2 Dependency Reason

The Admin invitation process must exist to generate and email the registration link/token for client contacts.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-003

#### 6.1.3.2 Dependency Reason

The Admin invitation process must exist to generate and email the registration link/token for vendor contacts.

## 6.2.0.0 Technical Dependencies

- AWS Cognito User Pool must be configured to accept new user sign-ups.
- A backend service (e.g., User Service) is required to manage invitation tokens (create, read, invalidate).
- AWS SES integration for sending the invitation email (handled by prerequisite stories).

## 6.3.0.0 Data Dependencies

- Requires a data store (e.g., a 'invitations' table in PostgreSQL) to persist the token, associated email, expiration timestamp, and status ('pending', 'used', 'expired').

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The registration page (LCP) must load in under 2.5 seconds (REQ-NFR-001).
- The server-side validation and account creation process (API call) should complete in under 500ms (p95).

## 7.2.0.0 Security

- Registration tokens must be cryptographically secure and non-guessable.
- All communication must be over HTTPS (REQ-INT-003).
- Passwords must be hashed and salted using Argon2id before being handled by Cognito (REQ-NFR-003).
- The system must protect against credential stuffing attacks on the registration endpoint.

## 7.3.0.0 Usability

- The registration process should be simple and intuitive, requiring minimal steps.
- Error messages must be clear, concise, and actionable.

## 7.4.0.0 Accessibility

- The registration page must comply with WCAG 2.1 Level AA standards (REQ-INT-001).

## 7.5.0.0 Compatibility

- The page must render correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge (REQ-DEP-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Secure token generation, storage, and lifecycle management.
- Integration with AWS Cognito for user creation, including mapping application roles.
- Handling multiple error states with distinct, user-friendly feedback.
- Ensuring the transactionality of user creation and token invalidation.

## 8.3.0.0 Technical Risks

- Incorrectly configured Cognito policies could prevent user creation.
- A race condition could potentially allow a token to be used twice if the invalidation step is not atomic with user creation.

## 8.4.0.0 Integration Points

- Backend User Service: To validate the token and create the user profile.
- AWS Cognito: To create the authentication principal.
- PostgreSQL Database: To update the invitation token status.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Successful registration flow.
- Attempted registration with expired, used, and invalid tokens.
- Form validation for password mismatch and weak passwords.
- Keyboard-only navigation and form submission.
- API endpoint security testing for token manipulation.

## 9.3.0.0 Test Data Needs

- A set of pre-generated tokens with different statuses: valid, expired, and used.
- A list of passwords that both meet and fail the complexity requirements.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration tests implemented with >80% code coverage
- E2E tests for happy path and key error conditions are implemented and passing
- User interface reviewed and approved by the product owner/designer
- Performance requirements for page load and API response time are met
- Security review completed, and token handling mechanism validated
- User-facing documentation (if any) is updated
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a blocking story for any functionality that requires a new user to be onboarded.
- Requires coordination between frontend and backend development.
- Dependent on the completion of the backend logic for US-001/002/003 to generate tokens.

## 11.4.0.0 Release Impact

Critical for the first release as it is the primary entry point for all new users to the system.

