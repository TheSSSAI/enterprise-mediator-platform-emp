# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-010 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User Feedback for Invalid Login Attempt |
| As A User Story | As a registered user attempting to access the plat... |
| User Persona | Any Registered User (System Administrator, Finance... |
| Business Value | Improves user experience by providing clear, actio... |
| Functional Area | User Authentication |
| Story Theme | Authentication & Security |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

User enters correct email but incorrect password

### 3.1.3 Scenario Type

Error_Condition

### 3.1.4 Given

A registered user is on the login page

### 3.1.5 When

The user enters their correct email, an incorrect password, and submits the login form

### 3.1.6 Then

The system must deny access and the user must remain on the login page

### 3.1.7 And

The email input field retains its value.

### 3.1.8 Validation Notes

Verify that the API returns a 401 Unauthorized status and the UI displays the specified message. The password field must be empty upon page re-render.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

User enters a non-existent email address

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

A user is on the login page

### 3.2.5 When

The user enters an email address that is not registered in the system, any password, and submits the login form

### 3.2.6 Then

The system must deny access and the user must remain on the login page

### 3.2.7 And

The password input field is cleared of its value.

### 3.2.8 Validation Notes

Confirm that the system's response (message and timing) is indistinguishable from the response for an incorrect password to mitigate timing attacks.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Error message is accessible

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

A user who relies on a screen reader is on the login page

### 3.3.5 When

The user submits incorrect credentials and an error message is displayed

### 3.3.6 Then

The screen reader must announce the error message to the user

### 3.3.7 And

The error message element must be programmatically linked to the relevant input fields (e.g., via `aria-describedby`).

### 3.3.8 Validation Notes

Test with screen reader software (e.g., NVDA, VoiceOver) to ensure the error is announced. Inspect the DOM to verify ARIA attributes are correctly implemented.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Rate limiting is triggered after multiple failed attempts

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

The system is configured to lock an account after 5 failed attempts

### 3.4.5 When

A user (or attacker) makes 5 consecutive failed login attempts for the same account or from the same IP address

### 3.4.6 Then

On the 5th failed attempt, the system displays the generic error message

### 3.4.7 And

Any further login attempts for that account or from that IP are blocked for the configured duration.

### 3.4.8 Validation Notes

Automated tests should simulate multiple failed logins and verify that the account/IP is locked out as per the configured security policy in AWS Cognito.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated area on the login form for displaying authentication error messages.

## 4.2.0 User Interactions

- Upon failed login, the error message appears without a full page reload.
- The password field is automatically cleared after a failed attempt.
- The email field value is preserved to allow the user to easily retry.

## 4.3.0 Display Requirements

- The error message must be clearly visible and distinguishable from other text, typically using a contrasting color (e.g., red).
- The message text must be non-technical and easy for a non-expert user to understand.

## 4.4.0 Accessibility Needs

- The error message must meet WCAG 2.1 Level AA standards.
- The error message must have a sufficient color contrast ratio.
- The error message must be associated with the form controls it relates to, allowing screen readers to announce the error in context.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

To prevent user enumeration, the system must always return a generic error message for failed login attempts, regardless of whether the email was invalid or the password was incorrect.

### 5.1.3 Enforcement Point

Backend Authentication Service (API Gateway / AWS Cognito).

### 5.1.4 Violation Handling

A specific error message (e.g., 'User not found') is a security vulnerability and a violation of this rule. The system must never reveal the reason for failure.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Implement rate limiting on the authentication endpoint to mitigate brute-force attacks. After 5 failed attempts from an IP or on an account within 5 minutes, a temporary lockout of 5 minutes is enforced.

### 5.2.3 Enforcement Point

API Gateway and/or AWS Cognito configuration.

### 5.2.4 Violation Handling

Failure to block further attempts after the threshold is a critical security failure.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-006

#### 6.1.1.2 Dependency Reason

This story implements the error handling for the login functionality defined in US-006. The login form UI and basic API endpoint must exist first.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-004

#### 6.1.2.2 Dependency Reason

A user registration flow must exist to create accounts that can be used for testing failed login attempts.

## 6.2.0.0 Technical Dependencies

- AWS Cognito: The authentication service that will handle credential validation and security policies like rate limiting (as per REQ-NFR-003).
- Frontend Login Component: A React component for the login form must be available to integrate the error display logic.

## 6.3.0.0 Data Dependencies

- Requires existing user accounts in the user database (Cognito User Pool) for testing.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The server response time for a failed login attempt must be under 250ms (p95) as per REQ-NFR-001.
- To prevent timing attacks, the response time for a non-existent user should be computationally indistinguishable from the response time for an existing user with an incorrect password.

## 7.2.0.0 Security

- The error message must be generic and not reveal whether the username or password was incorrect (OWASP ASVS V4.0.3 - 2.1.2).
- The password field must be cleared from the UI after a failed submission.
- Rate limiting must be enforced on the authentication endpoint as per REQ-NFR-003.
- All communication must be over HTTPS using TLS 1.2+ as per REQ-INT-003.

## 7.3.0.0 Usability

- The error message should be concise, clear, and provide a path to resolution (i.e., 'try again').

## 7.4.0.0 Accessibility

- Must adhere to WCAG 2.1 Level AA standards as per REQ-INT-001.

## 7.5.0.0 Compatibility

- The error message display and behavior must be consistent across the latest two major versions of Chrome, Firefox, Safari, and Edge as per REQ-DEP-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Frontend state management for displaying the error is straightforward.
- Backend logic involves catching the authentication error from Cognito and returning a standardized 401 response.
- Configuration of Cognito's built-in account lockout policies is required but not complex custom code.

## 8.3.0.0 Technical Risks

- Misconfiguration of Cognito's security policies could lead to ineffective rate limiting.
- Inconsistent response timing from the backend could introduce a timing attack vulnerability.

## 8.4.0.0 Integration Points

- Frontend Login Component <-> Backend API Gateway Endpoint
- Backend API Gateway Endpoint <-> AWS Cognito User Pool

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify error message on incorrect password.
- Verify error message on non-existent email.
- Verify error message on correct email with different casing.
- Verify rate limiting is triggered and blocks subsequent attempts.
- Verify screen reader announces the error correctly.
- Verify response times for invalid user vs. invalid password are not significantly different.

## 9.3.0.0 Test Data Needs

- A set of valid user credentials.
- A set of email addresses that are known not to exist in the system.

## 9.4.0.0 Testing Tools

- Jest for frontend/backend unit tests.
- Playwright for E2E tests.
- Security scanning tools (e.g., OWASP ZAP) to test for user enumeration.
- NVDA or VoiceOver for accessibility testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other engineer
- Unit and integration tests implemented with >80% coverage for new code
- E2E tests for failed login scenarios are implemented and passing
- Security review confirms no user enumeration or timing attack vulnerabilities
- Accessibility review confirms WCAG 2.1 AA compliance for the error message
- AWS Cognito security policies (rate limiting, lockout) are configured and verified
- Documentation for the authentication endpoint's error response is updated
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

2

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational story for the user authentication experience and should be completed in an early sprint.
- Requires access and permissions to configure AWS Cognito security settings.

## 11.4.0.0 Release Impact

Blocks the release of any feature that requires user login. It is a critical path item for the initial MVP release.

